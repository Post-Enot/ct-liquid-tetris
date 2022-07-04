using System;
using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;

namespace LiquidTetris.NetworkCode
{
    [RequireComponent(typeof(SimplifiedPhysics2D))]
    public class NetworkShapeBehaviourModule : MonoBehaviourPun, IShapeBehaviourModule, ISpawnable
    {
        public bool IsTakesQueue => false;
        public bool IsTakesControl => true;

        public event Action FreeUpQueueSpace;
        public event Action SurfaceTouched;
        public event Action<IShapeEntity> UnitedWithShape;

        private IShapeEntity _shapeEntity;
        private SimplifiedPhysics2D _simplifiedPhysics;
        private readonly HashSet<IShapeEntity> _collisionsEntities = new();

        private void Awake()
        {
            _simplifiedPhysics = GetComponent<SimplifiedPhysics2D>();
            _shapeEntity = GetComponent<IShapeEntity>();
        }

        private void OnDestroy()
        {
            FreeUpQueueSpace?.Invoke();
            FreeUpQueueSpace = null;
            SurfaceTouched = null;
            UnitedWithShape = null;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_simplifiedPhysics.IsSimulationEnabled)
            {
                if (collision.gameObject.TryGetComponent(out IShapeEntity shapeEntity)
                    && shapeEntity.Model.Material == _shapeEntity.Model.Material
                    && !_collisionsEntities.Contains(shapeEntity))
                {
                    shapeEntity.BehaviourModule.RegisterUnion(_shapeEntity);
                    RegisterUnion(shapeEntity);
                    if (shapeEntity.Model.Points.Count >= _shapeEntity.Model.Points.Count)
                    {
                        shapeEntity.BehaviourModule.UniteWithShape(_shapeEntity);
                    }
                    else
                    {
                        UniteWithShape(shapeEntity);
                    }
                }
                else
                {
                    SurfaceTouched?.Invoke();
                }
            }
        }

        public void DisablePhysicsSimulation()
        {
            _simplifiedPhysics.DisableSimulation();
        }

        public void EnablePhysicsSimulation()
        {
            _simplifiedPhysics.EnableSimulation();
        }

        public void SetMaxFallSpeed(float maxFallSpeed)
        {
            _simplifiedPhysics.MaxFallSpeed = maxFallSpeed;
        }

        public void MoveHorizontally(float xVelocity)
        {
            _simplifiedPhysics.MoveHorizontally(xVelocity);
        }

        public void RegisterUnion(IShapeEntity otherShape)
        {
            _collisionsEntities.Add(otherShape);
        }

        public void UniteWithShape(IShapeEntity shapeEntity)
        {
            var unitedShapeNetworkModel = shapeEntity.Model as NetworkShapeModel;
            photonView.RPC(
                nameof(RpcUniteWithShape),
                RpcTarget.All,
                unitedShapeNetworkModel.photonView.ViewID,
                new Vector2(
                    shapeEntity.Transform.localPosition.x,
                    shapeEntity.Transform.localPosition.y),
                shapeEntity.Transform.localEulerAngles.z,
                new Vector2(
                    _shapeEntity.Transform.localPosition.x,
                    _shapeEntity.Transform.localPosition.y),
                _shapeEntity.Transform.localEulerAngles.z);
        }

        [PunRPC]
        private void RpcUniteWithShape(
            int unitedShapePhotonViewID,
            Vector2 unitedShapeLocalPosition,
            float unitedShapeLocalEulerAnglesZ,
            Vector2 selfLocalPosition,
            float selfLocalEulerAnglesZ)
        {
            PhotonView unitedShapePhotonView = PhotonNetwork.GetPhotonView(unitedShapePhotonViewID);
            unitedShapePhotonView.gameObject.SetActive(false);
            var unitedShapeEntity = unitedShapePhotonView.GetComponent<IShapeEntity>();
            if (!photonView.IsMine)
            {
                SetTransform2D(unitedShapeEntity.Transform, unitedShapeLocalPosition, unitedShapeLocalEulerAnglesZ);
                SetTransform2D(_shapeEntity.Transform, selfLocalPosition, selfLocalEulerAnglesZ);
            }
            var shapeModel = photonView.GetComponent<ShapeModel>();
            foreach (Point point in unitedShapeEntity.Model.Points.Values.ToList())
            {
                shapeModel.AddPoint(point.WorldPosition, point.Collider.radius);
            }
            if (!photonView.IsMine)
            {
                unitedShapeEntity.Model.Destroy();
            }
            UnitedWithShape?.Invoke(_shapeEntity);
        }

        private static void SetTransform2D(Transform transform, Vector2 localPosition, float localEulerAnglesZ)
        {
            transform.localPosition = new Vector3(
                localPosition.x,
                localPosition.y,
                transform.localPosition.z);
            transform.localEulerAngles = new Vector3(
                transform.localEulerAngles.x,
                transform.localEulerAngles.y,
                localEulerAnglesZ);
        }

        public void Spawn()
        {

        }
    }
}

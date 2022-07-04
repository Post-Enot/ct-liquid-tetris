using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LiquidTetris.NetworkCode
{
    public class NetworkShapeModel : MonoBehaviourPun, IShapeModel
    {
        [SerializeField] private ShapeModel _shapeModel;

        public ShapeMaterial Material => _shapeModel.Material;
        public IReadOnlyDictionary<CircleCollider2D, Point> Points => _shapeModel.Points;
        public float Area => _shapeModel.Area;

        public event Action<float> PointsDestroyed
        {
            add => _shapeModel.PointsDestroyed += value;
            remove => _shapeModel.PointsDestroyed -= value;
        }
        public event Action Destroyed
        {
            add => _shapeModel.Destroyed += value;
            remove => _shapeModel.Destroyed -= value;
        }

        [PunRPC]
        public void EnableGameObject()
        {
            gameObject.SetActive(true);
        }

        [PunRPC]
        public void DisableGameObject()
        {
            gameObject.SetActive(false);
        }

        public void AddPoint(Vector2 worldPosition, float radius)
        {
            photonView.RPC(nameof(RpcAddPoint), RpcTarget.All, worldPosition, radius);
        }

        public void Destroy()
        {
            photonView.RPC(nameof(RpcDestroy), photonView.Owner);
        }

        public void DestroyPoints(params CircleCollider2D[] pointColliders)
        {
            photonView.RPC(nameof(RpcDestroyPoints), RpcTarget.All, pointColliders);
        }

        public void SetMaterial(ShapeMaterial material)
        {
            photonView.RPC(nameof(RpcSetMaterial), RpcTarget.All, material.Layer);
        }

        public void SetScale(float scale)
        {
            photonView.RPC(nameof(RpcSetScale), RpcTarget.All, scale);
        }

        [PunRPC]
        private void RpcAddPoint(Vector2 worldPosition, float radius)
        {
            _shapeModel.AddPoint(worldPosition, radius);
        }

        [PunRPC]
        private void RpcDestroy()
        {
            PhotonNetwork.Destroy(photonView);
        }

        [PunRPC]
        private void RpcDestroyPoints()
        {

        }

        [PunRPC]
        private void RpcSetMaterial(int materialLayer)
        {
            ShapeMaterial shapeMaterial = ShapeMaterials.Instance.GetByLayer(materialLayer);
            _shapeModel.SetMaterial(shapeMaterial);
        }

        [PunRPC]
        private void RpcSetScale(float scale)
        {
            _shapeModel.SetScale(scale);
        }
    }
}

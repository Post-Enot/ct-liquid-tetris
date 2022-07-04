using System;
using System.Collections.Generic;
using UnityEngine;

namespace LiquidTetris.NetworkCode
{
    [RequireComponent(typeof(SimplifiedPhysics2D))]
    public class ShapeBehaviourModule : MonoBehaviour, IShapeBehaviourModule
    {
        public event Action SurfaceTouched;
        public event Action<IShapeEntity> UnitedWithShape;

        private readonly HashSet<IShapeEntity> _collisionsEntities = new HashSet<IShapeEntity>();
        private SimplifiedPhysics2D _simplifiedPhysics;
        private IShapeEntity _shapeEntity;

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

        public void RegisterUnion(IShapeEntity unifyingShape)
        {
            _collisionsEntities.Add(unifyingShape);
            UnitedWithShape?.Invoke(_shapeEntity);
        }

        public void UniteWithShape(IShapeEntity shapeEntity)
        {
            foreach (Point point in shapeEntity.Model.Points.Values)
            {
                shapeEntity.Model.AddPoint(point.WorldPosition, point.Collider.radius);
            }
            UnitedWithShape?.Invoke(_shapeEntity);
            shapeEntity.Model.Destroy();
        }

        private void Awake()
        {
            _simplifiedPhysics = GetComponent<SimplifiedPhysics2D>();
            _shapeEntity = GetComponent<IShapeEntity>();
        }

        private void OnDestroy()
        {
            SurfaceTouched = null;
            UnitedWithShape = null;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_simplifiedPhysics.IsSimulationEnabled)
            {
                if (collision.gameObject.TryGetComponent(out IShapeEntity shapeEntity) &&
                    shapeEntity.Model.Material == _shapeEntity.Model.Material)
                {
                    if (shapeEntity.Model.Points.Count >= shapeEntity.Model.Points.Count)
                    {
                        shapeEntity.BehaviourModule.UniteWithShape(shapeEntity);
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
    }
}

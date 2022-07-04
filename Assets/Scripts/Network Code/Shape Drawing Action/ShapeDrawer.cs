using System;
using UnityEngine;

namespace LiquidTetris
{
    [Serializable]
    public class ShapeDrawer
    {
        [Header("Component references:")]
        [SerializeField] private GameRules _gameRules;
        [SerializeField] private Transform _drawingFieldCenter;

        public Vector2 DrawingFieldCenter => _drawingFieldCenter.position;

        private Vector2 _previousDrawedPointWorldPosition;

        public void HandleTouchDown(Vector2 touchPositionInWorldSpace, IShapeEntity shapeEntity)
        {
            shapeEntity.Model.SetMaterial(ShapeMaterials.Instance.Drawed);
            shapeEntity.DrawModule.DrawPoint(touchPositionInWorldSpace, _gameRules.PointRadiusInDrawingField);
            _previousDrawedPointWorldPosition = touchPositionInWorldSpace;
        }

        public void HandleTouchMove(Vector2 touchPositionInWorldSpace, IShapeEntity shapeEntity)
        {
            _previousDrawedPointWorldPosition = shapeEntity.DrawModule.DrawPointsBetweenTwoPositions(
                startingWorldPosition: _previousDrawedPointWorldPosition,
                endingWorldPosition: touchPositionInWorldSpace,
                _gameRules.PointRadiusInDrawingField,
                _gameRules.ColliderOverlapFactor);
        }
    }
}

using UnityEngine;

namespace LiquidTetris.NetworkCode
{
    public class ShapeDrawModule : MonoBehaviour, IShapeDrawModule
    {
        private IShapeEntity _shapeEntity;

        public bool CanDrawPoint(Vector2 worldPosition, float pointRadius, float colliderOverlapFactor)
        {
            float castedColliderRadius = (pointRadius * 2 * colliderOverlapFactor) - pointRadius;
            return !Physics2D.CircleCast(worldPosition,castedColliderRadius, Vector2.zero, _shapeEntity.Model.Material.Layer);
        }

        public void DrawPoint(Vector2 worldPosition, float pointRadius)
        {
            _shapeEntity.Model.AddPoint(worldPosition, pointRadius);
        }

        public Vector2 DrawPointsBetweenTwoPositions(
            Vector2 startingWorldPosition,
            Vector2 endingWorldPosition,
            float pointRadius,
            float colliderOverlapFactor)
        {
            Vector2 distance = endingWorldPosition - startingWorldPosition;
            float stepLength = pointRadius * 2 * colliderOverlapFactor + 0.001f;
            int pointsCanFit = (int)(distance.magnitude / stepLength);
            Vector2 pointWorldPosition = startingWorldPosition;
            Vector2 lastDrawedPointWorldPosition = startingWorldPosition;
            Vector2 step = distance.normalized * stepLength;
            for (int i = 0; i < pointsCanFit; i += 1)
            {
                pointWorldPosition += step;
                if (CanDrawPoint(pointWorldPosition, pointRadius, colliderOverlapFactor))
                {
                    DrawPoint(pointWorldPosition, pointRadius);
                    lastDrawedPointWorldPosition = pointWorldPosition;
                }
            }
            return lastDrawedPointWorldPosition;
        }

        private void Awake()
        {
            _shapeEntity = GetComponent<IShapeEntity>();
        }
    }
}

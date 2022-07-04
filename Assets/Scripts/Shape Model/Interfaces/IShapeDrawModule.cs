using UnityEngine;

namespace LiquidTetris
{
    public interface IShapeDrawModule
    {
        public void DrawPoint(Vector2 worldPosition, float pointRadius);
        public Vector2 DrawPointsBetweenTwoPositions(
            Vector2 startingWorldPosition,
            Vector2 endingWorldPosition,
            float pointRadius,
            float colliderOverlapFactor);
        public bool CanDrawPoint(
            Vector2 worldPosition,
            float pointRadius,
            float colliderOverlapFactor);
    }
}

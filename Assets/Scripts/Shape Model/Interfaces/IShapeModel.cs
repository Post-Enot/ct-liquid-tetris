using UnityEngine;
using System;
using System.Collections.Generic;

namespace LiquidTetris
{
    public interface IShapeModel
    {
        public ShapeMaterial Material { get; }
        public IReadOnlyDictionary<CircleCollider2D, Point> Points { get; }
        public float Area { get; }

        public event Action<float> PointsDestroyed;
        public event Action Destroyed;

        public void AddPoint(Vector2 worldPosition, float radius);
        public void SetMaterial(ShapeMaterial material);
        public void SetScale(float scale);
        public void Destroy();
        public void DestroyPoints(params CircleCollider2D[] pointColliders);
    }
}

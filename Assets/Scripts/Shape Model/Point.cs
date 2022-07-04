using UnityEngine;

namespace LiquidTetris
{
    public class Point
    {
        public Point(SpriteRenderer spriteRenderer, CircleCollider2D collider, float radius)
        {
            _spriteRenderer = spriteRenderer;
            Collider = collider;
            Collider.radius = radius;
            _spriteRenderer.transform.localScale = new Vector3(radius, radius, radius);
        }

        public CircleCollider2D Collider { get; private set; }
        public Vector2 WorldPosition => _spriteRenderer.transform.position;
        public float Scale => Collider.radius;

        private readonly SpriteRenderer _spriteRenderer;

        public void SetScale(float scale)
        {
            float scaleFactor = scale / Scale;
            _spriteRenderer.transform.localScale = new Vector3(scale, scale, scale);
            _spriteRenderer.transform.localPosition = new Vector3(
                _spriteRenderer.transform.localPosition.x * scaleFactor,
                _spriteRenderer.transform.localPosition.y * scaleFactor,
                _spriteRenderer.transform.localPosition.z);
            Collider.offset *= scaleFactor;
            Collider.radius = scale;
        }

        public void SetMaterial(ShapeMaterial material)
        {
            _spriteRenderer.gameObject.layer = material.Layer;
        }

        public void DestroyRenderer()
        {
            Object.Destroy(_spriteRenderer.gameObject);
        }
    }
}

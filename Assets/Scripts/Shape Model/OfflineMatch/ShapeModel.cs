using System;
using System.Collections.Generic;
using UnityEngine;

namespace LiquidTetris.NetworkCode
{
    public class ShapeModel : MonoBehaviour, IShapeModel
    {
        [SerializeField] private GameObject _pointPrefab;

        public ShapeMaterial Material { get; private set; }
        public IReadOnlyDictionary<CircleCollider2D, Point> Points => _points;
        public float Area { get; private set; }

        public event Action<float> PointsDestroyed;
        public event Action Destroyed;

        private readonly Dictionary<CircleCollider2D, Point> _points = new Dictionary<CircleCollider2D, Point>(capacity: 256);
        private IShapeEntity _shapeEntity;

        public void AddPoint(Vector2 worldPosition, float radius)
        {
            GameObject pointObject = Instantiate(_pointPrefab, worldPosition, Quaternion.identity, transform);
            var spriteRenderer = pointObject.GetComponent<SpriteRenderer>();
            var collider = gameObject.AddComponent<CircleCollider2D>();
            collider.offset = pointObject.transform.localPosition;
            var point = new Point(spriteRenderer, collider, radius);
            point.SetMaterial(Material);
            _points.Add(collider, point);
            Area += radius;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void DestroyPoints(params CircleCollider2D[] pointColliders)
        {
            float destroyedArea = 0;
            var worldPositions = new List<Vector2>();
            foreach (CircleCollider2D collider in pointColliders)
            {
                destroyedArea += collider.radius;
                worldPositions.Add(Points[collider].WorldPosition);
                Points[collider].DestroyRenderer();
                _ = _points.Remove(collider);
                Destroy(collider);
            }
            Area -= destroyedArea;
            if (Points.Count == 0)
            {
                Destroy();
            }
            else
            {
                _shapeEntity.VisualEffectsModule.EmitDestructionParticles(worldPositions.ToArray());
                PointsDestroyed?.Invoke(destroyedArea);
            }
        }

        public void SetMaterial(ShapeMaterial material)
        {
            Material = material;
            gameObject.layer = material.Layer;
            foreach (Point point in _points.Values)
            {
                point.SetMaterial(material);
            }
        }

        public void SetScale(float scale)
        {
            foreach (var point in _points.Values)
            {
                point.SetScale(scale);
            }
            Area = CalculateShapeArea(this);
        }

        public static float CalculateShapeArea(IShapeModel shapeModel)
        {
            float area = 0;
            foreach (Point point in shapeModel.Points.Values)
            {
                area += point.Collider.radius;
            }
            return area;
        }

        private void Awake()
        {
            _shapeEntity = GetComponent<IShapeEntity>();
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke();
            Destroyed = null;
            PointsDestroyed = null;
        }
    }
}

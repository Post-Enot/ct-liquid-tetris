using UnityEngine;

namespace LiquidTetris.NetworkCode
{
    public class ShapeVisualEffectsModule : MonoBehaviour, IShapeVisualEffectsModule
    {
        [SerializeField] private int _particlesCountForOnePoint;

        private IShapeEntity _shapeEntity;

        public void EmitDestructionParticles()
        {
            ParticleSystem.ShapeModule shapeModule = ParticleSystems.Instance.ShapeUnitParticles.shape;
            foreach (Point point in _shapeEntity.Model.Points.Values)
            {
                shapeModule.texture = _shapeEntity.Model.Material.Texture;
                shapeModule.position = point.WorldPosition;
                ParticleSystems.Instance.ShapeUnitParticles.Emit(_particlesCountForOnePoint);
            }
        }

        public void EmitDestructionParticles(params Vector2[] worldPositions)
        {
            ParticleSystem.ShapeModule shapeModule = ParticleSystems.Instance.ShapeUnitParticles.shape;
            foreach (Vector2 worldPosition in worldPositions)
            {
                shapeModule.position = worldPosition;
                ParticleSystems.Instance.ShapeUnitParticles.Emit(_particlesCountForOnePoint);
            }
        }

        private void Awake()
        {
            _shapeEntity = GetComponent<IShapeEntity>();
        }
    }
}

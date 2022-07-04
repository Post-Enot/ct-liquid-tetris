using UnityEngine;

namespace LiquidTetris
{
    public class ParticleSystems : Singleton<ParticleSystems>
    {
        [SerializeField] private GameObject _shapeUnitParticlesPrefab;

        public ParticleSystem ShapeUnitParticles { get; private set; }

        private void Awake()
        {
            InitInstance(this);
            GameObject shapeUnitParticlesObject = Instantiate(_shapeUnitParticlesPrefab, Vector3.zero, Quaternion.identity);
            ShapeUnitParticles = shapeUnitParticlesObject.GetComponent<ParticleSystem>();
        }
    }
}

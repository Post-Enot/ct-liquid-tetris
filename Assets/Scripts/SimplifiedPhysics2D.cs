using CoroutineShells;
using System.Collections;
using UnityEngine;

namespace LiquidTetris
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SimplifiedPhysics2D : MonoBehaviour
    {
        [SerializeField] private float _shapeMaxFallSpeed;

        private const float _speedReductionFactor = Mathf.PI;

        public float MaxFallSpeed
        {
            get => _shapeMaxFallSpeed;
            set => _shapeMaxFallSpeed = value;
        }
        public float MaxHorizontalSpeed { get; set; } = 8;
        public bool IsSimulationEnabled { get; private set; }

        private Rigidbody2D _rigidbody;
        private UniqueCoroutine _fallRoutine;

        public void MoveHorizontally(float xVelocity)
        {
            float updatedVelocity = Mathf.Abs(_rigidbody.velocity.x + xVelocity);
            if (updatedVelocity <= MaxHorizontalSpeed)
            {
                _rigidbody.velocity += new Vector2(xVelocity, 0);
            }
        }

        public void EnableSimulation()
        {
            if (!IsSimulationEnabled)
            {
                _rigidbody.bodyType = RigidbodyType2D.Dynamic;
                _fallRoutine.Start();
                IsSimulationEnabled = true;
            }
        }

        public void DisableSimulation()
        {
            if (IsSimulationEnabled)
            {
                _rigidbody.velocity = Vector2.zero;
                _rigidbody.bodyType = RigidbodyType2D.Static;
                _fallRoutine.Stop();
                IsSimulationEnabled = false;
            }
        }

        private IEnumerator Fall()
        {
            do
            {
                var updatedVelocity = new Vector2(_rigidbody.velocity.x / _speedReductionFactor, _rigidbody.velocity.y);
                if (_rigidbody.velocity.y < -MaxFallSpeed)
                {
                    updatedVelocity.y = -MaxFallSpeed;
                }
                _rigidbody.velocity = updatedVelocity;
                yield return new WaitForFixedUpdate();
            }
            while (IsSimulationEnabled);
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _fallRoutine = new UniqueCoroutine(this, () => Fall());
        }
    }
}

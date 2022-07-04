using CoroutineShells;
using System;
using System.Collections;
using UnityEngine;

namespace LiquidTetris
{
    public class TangibleModule : MonoBehaviour
    {
        [SerializeField] private bool _isTangibleOnAwake = true;

        public bool IsTangible { get; private set; }

        public event Action OnTangible;
        public event Action OnIntangible;

        private UniqueCoroutine<float> _intangibilityRoutine;
        private UniqueCoroutine<float> _tangibilityRoutine;

        private void Awake()
        {
            IsTangible = _isTangibleOnAwake;
            _intangibilityRoutine = new(this, Intangibility);
            _tangibilityRoutine = new(this, Tangibility);
        }

        public void MakeTangible()
        {
            _intangibilityRoutine.Stop();
            _tangibilityRoutine.Stop();
            IsTangible = true;
            OnTangible?.Invoke();
        }

        public void MakeIntangible()
        {
            _intangibilityRoutine.Stop();
            _tangibilityRoutine.Stop();
            IsTangible = false;
            OnIntangible?.Invoke();
        }

        public void MakeTemporarilyTangible(float durationInSeconds)
        {
            _tangibilityRoutine.StartAnyway(durationInSeconds);
        }

        public void MakeTemporarilyIntangible(float durationInSeconds)
        {
            _intangibilityRoutine.StartAnyway(durationInSeconds);
        }

        private IEnumerator Intangibility(float durationInSeconds)
        {
            IsTangible = false;
            yield return new WaitForSeconds(durationInSeconds);
            IsTangible = true;
            OnTangible?.Invoke();
        }

        private IEnumerator Tangibility(float durationInSeconds)
        {
            IsTangible = true;
            yield return new WaitForSeconds(durationInSeconds);
            IsTangible = false;
            OnIntangible?.Invoke();
        }
    }
}

using CoroutineShells;
using FieldIndicators;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LiquidTetris
{
    [RequireComponent(typeof(Collider2D))]
    public class FieldOverflowDetector : MonoBehaviour
    {
        [SerializeField] private GameRules _gameRules;
        [SerializeField] private bool _isEnableOnAwake = true;
        [SerializeField] private FloatReferenceField _overflow;

        public bool IsDetectionEnabled { get; private set; }
        public float Overflow => _overflow.Value;

        public event Action Overflowed;
        public event Action OverflowStarted;
        public event Action OverflowCanceled;

        private readonly HashSet<TangibleModule> _tangibleModules = new();
        private UniqueCoroutine _detectionRoutine;

        private void Awake()
        {
            _detectionRoutine = new(this, Detection);
            if (_isEnableOnAwake)
            {
                EnableDetection();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<TangibleModule>(out var tangibleModule))
            {
                _ = _tangibleModules.Add(tangibleModule);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent<TangibleModule>(out var tangibleModule))
            {
                _ = _tangibleModules.Remove(tangibleModule);
            }
        }

        public void EnableDetection()
        {
            IsDetectionEnabled = true;
            _detectionRoutine.StartAnyway();
        }

        public void DisableDetection()
        {
            IsDetectionEnabled = false;
            _detectionRoutine.Stop();
        }

        private IEnumerator Detection()
        {
            do
            {
                bool isDetected = false;
                foreach (TangibleModule tangibleModule in _tangibleModules)
                {
                    if (tangibleModule.IsTangible)
                    {
                        isDetected = true;
                        break;
                    }
                }
                if (isDetected)
                {
                    if (_overflow.Value < 1)
                    {
                        _overflow.Value += CalculateOverflow();
                        if (_overflow.Value >= 1)
                        {
                            _overflow.Value = 1;
                            Overflowed?.Invoke();
                        }
                    }
                }
                else
                {
                    if (_overflow.Value > 0)
                    {
                        _overflow.Value -= CalculateOverflow();
                        if (_overflow.Value <= 0)
                        {
                            _overflow.Value = 0;
                        }
                    }
                }
                yield return null;
            }
            while (IsDetectionEnabled);
        }

        private float CalculateOverflow()
        {
            return Time.deltaTime / _gameRules.OverflowDurationInSeconds;
        }
    }
}

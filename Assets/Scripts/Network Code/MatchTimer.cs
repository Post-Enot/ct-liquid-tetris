using CoroutineShells;
using FieldIndicators;
using System;
using System.Collections;
using UnityEngine;

namespace LiquidTetris
{
    public class MatchTimer : MonoBehaviour
    {
        [SerializeField] private FloatReferenceField _timeInSeconds;

        public FloatReferenceField TimeInSeconds => _timeInSeconds;

        public event Action TimeLeft;

        private UniqueCoroutine _timer;

        private void Start()
        {
            _timer = new UniqueCoroutine(this, () => Countdown());
        }

        public void StartCountdown()
        {
            _timer.Start();
        }

        public void PauseCountdown()
        {
            _timer.Stop();
        }

        private IEnumerator Countdown()
        {
            do
            {
                yield return null;
                _timeInSeconds.Value -= Time.deltaTime;
            }
            while (_timeInSeconds.Value > 0);
            _timeInSeconds.Value = 0;
            TimeLeft?.Invoke();
        }
    }
}

using System;
using System.Collections;
using UnityEngine;

namespace CoroutineShells
{
    public class UniqueCoroutine
    {
        public UniqueCoroutine(MonoBehaviour performer, Func<IEnumerator> getRoutine)
        {
            Performer = performer;
            _getRoutine = getRoutine;
        }

        public bool IsActive { get; private set; }
        public readonly MonoBehaviour Performer;

        private readonly Func<IEnumerator> _getRoutine;
        private Coroutine _coroutine;

        public void Start()
        {
            if (!IsActive)
            {
                _coroutine = Performer.StartCoroutine(Routine());
                IsActive = true;
            }
        }

        public void StartAnyway()
        {
            if (IsActive)
            {
                Performer.StopCoroutine(_coroutine);
            }
            _coroutine = Performer.StartCoroutine(Routine());
            IsActive = true;
        }

        public void Stop()
        {
            if (IsActive)
            {
                Performer.StopCoroutine(_coroutine);
                IsActive = false;
            }
        }

        private IEnumerator Routine()
        {
            yield return _getRoutine();
            IsActive = false;
        }
    }
}

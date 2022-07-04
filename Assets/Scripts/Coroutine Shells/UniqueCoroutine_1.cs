using System;
using System.Collections;
using UnityEngine;

namespace CoroutineShells
{
    public class UniqueCoroutine<T>
    {
        public UniqueCoroutine(MonoBehaviour performer, Func<T, IEnumerator> getRoutine)
        {
            _uniqueCoroutine = new(performer, () => getRoutine(_arg));
        }

        public bool IsActive => _uniqueCoroutine.IsActive;
        public MonoBehaviour GetPerformer => _uniqueCoroutine.Performer;

        private UniqueCoroutine _uniqueCoroutine;
        private T _arg;

        public void Start(T arg)
        {
            _arg = arg;
            _uniqueCoroutine.Start();
        }

        public void StartAnyway(T arg)
        {
            _arg = arg;
            _uniqueCoroutine.StartAnyway();
        }

        public void Stop()
        {
            _uniqueCoroutine.Stop();
        }
    }
}

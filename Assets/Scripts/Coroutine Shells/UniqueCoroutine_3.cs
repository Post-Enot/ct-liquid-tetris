using System;
using System.Collections;
using UnityEngine;

namespace CoroutineShells
{
    public class UniqueCoroutine<T0, T1, T2>
    {
        public UniqueCoroutine(MonoBehaviour performer, Func<T0, T1, T2, IEnumerator> getRoutine)
        {
            _uniqueCoroutine = new(performer, () => getRoutine(_arg0, _arg1, _arg2));
        }

        public bool IsActive => _uniqueCoroutine.IsActive;
        public MonoBehaviour GetPerformer => _uniqueCoroutine.Performer;

        private UniqueCoroutine _uniqueCoroutine;
        private T0 _arg0;
        private T1 _arg1;
        private T2 _arg2;

        public void Start(T0 arg0, T1 arg1, T2 arg2)
        {
            _arg0 = arg0;
            _arg1 = arg1;
            _arg2 = arg2;
            _uniqueCoroutine.Start();
        }

        public void StartAnyway(T0 arg0, T1 arg1, T2 arg2)
        {
            _arg0 = arg0;
            _arg1 = arg1;
            _arg2 = arg2;
            _uniqueCoroutine.StartAnyway();
        }

        public void Stop()
        {
            _uniqueCoroutine.Stop();
        }
    }
}

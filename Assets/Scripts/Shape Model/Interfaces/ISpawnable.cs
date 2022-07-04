using System;
using UnityEngine;

namespace LiquidTetris
{
    public interface ISpawnable
    {
        public bool IsTakesQueue { get; }
        public bool IsTakesControl { get; }
        public Transform transform { get; }

        public event Action FreeUpQueueSpace;

        public void Spawn();
    }
}

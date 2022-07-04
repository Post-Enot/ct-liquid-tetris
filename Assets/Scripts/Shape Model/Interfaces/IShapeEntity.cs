using UnityEngine;

namespace LiquidTetris
{
    public interface IShapeEntity
    {
        public Transform Transform { get; }
        public IShapeModel Model { get; }
        public IShapeDrawModule DrawModule { get; }
        public IShapeBehaviourModule BehaviourModule { get; }
        public IShapeVisualEffectsModule VisualEffectsModule { get; }
        public ISpawnable Spawnable { get; }
    }
}

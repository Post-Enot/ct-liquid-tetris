using UnityEngine;

namespace LiquidTetris
{
    public interface IShapeVisualEffectsModule
    {
        public void EmitDestructionParticles();
        public void EmitDestructionParticles(params Vector2[] worldPositions);
    }
}

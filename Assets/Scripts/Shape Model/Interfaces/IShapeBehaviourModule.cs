using System;

namespace LiquidTetris
{
    public interface IShapeBehaviourModule
    {
        public event Action SurfaceTouched;
        public event Action<IShapeEntity> UnitedWithShape;

        public void DisablePhysicsSimulation();
        public void EnablePhysicsSimulation();
        public void SetMaxFallSpeed(float maxFallSpeed);
        public void RegisterUnion(IShapeEntity unifyingShape);
        public void UniteWithShape(IShapeEntity shapeEntity);
        public void MoveHorizontally(float xVelocity);
    }
}

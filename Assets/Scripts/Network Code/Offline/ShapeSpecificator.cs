using UnityEngine;

namespace LiquidTetris.NetworkCode
{
    public class ShapeSpecificator : MonoBehaviour
    {
        [SerializeField] private GameRules _gameRules;

        [Header("Scale random borders:")]
        [SerializeField] private float _scaleValueLowRandomBorder;
        [SerializeField] private float _scaleValueUpRandomBorder;

        [Header("Material random borders:")]
        [SerializeField] private int _materialCodeRandomLowBorder = 0;
        [SerializeField] private int _materialCodeRandomUpBorder = 5;

        private float _maxShapeFallSpeed;

        public void SpecificateShape(IShapeEntity shapeEntity)
        {
            SetScale(shapeEntity.Model);
            SetMaterial(shapeEntity.Model);
            SetMaxFallSpeed(shapeEntity.BehaviourModule);
        }

        private void Start()
        {
            _maxShapeFallSpeed = _gameRules.StartMaxShapeFallSpeed;
        }

        private void SetScale(IShapeModel shapeModel)
        {
            float randomScale = Random.Range(_scaleValueLowRandomBorder, _scaleValueUpRandomBorder);
            shapeModel.SetScale(randomScale);
        }

        private void SetMaterial(IShapeModel shapeModel)
        {
            int lot = Random.Range(_materialCodeRandomLowBorder, _materialCodeRandomUpBorder);
            ShapeMaterial material = lot switch
            {
                0 => ShapeMaterials.Instance.Red,
                1 => ShapeMaterials.Instance.Green,
                2 => ShapeMaterials.Instance.Blue,
                3 => ShapeMaterials.Instance.Yellow,
                4 => ShapeMaterials.Instance.Purple,
                _ => throw new System.ArgumentOutOfRangeException()
            };
            shapeModel.SetMaterial(material);
        }

        private void SetMaxFallSpeed(IShapeBehaviourModule shapeBehaviourModule)
        {
            shapeBehaviourModule.SetMaxFallSpeed(_maxShapeFallSpeed);
            _maxShapeFallSpeed *= _gameRules.ShapeMaxSpeedIncreaseFactor;
        }
    }
}

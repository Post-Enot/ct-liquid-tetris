using System;
using UnityEngine;

namespace LiquidTetris
{
    public class ShapeMaterials : Singleton<ShapeMaterials>
    {
        [Header("Game-using materials:")]
        [SerializeField] private ShapeMaterial _redShapeMaterial;
        [SerializeField] private ShapeMaterial _greenShapeMaterial;
        [SerializeField] private ShapeMaterial _blueShapeMaterial;
        [SerializeField] private ShapeMaterial _yellowShapeMaterial;
        [SerializeField] private ShapeMaterial _purpleShapeMaterial;

        [Header("Auxiliary materials:")]
        [SerializeField] private ShapeMaterial _drawedShapeMaterial;

        public ShapeMaterial Red => _redShapeMaterial;
        public ShapeMaterial Green => _greenShapeMaterial;
        public ShapeMaterial Blue => _blueShapeMaterial;
        public ShapeMaterial Yellow => _yellowShapeMaterial;
        public ShapeMaterial Purple => _purpleShapeMaterial;
        public ShapeMaterial Drawed => _drawedShapeMaterial;

        public ShapeMaterial GetByLayer(int layer)
        {
            return layer switch
            {
                Layer.Red => Red,
                Layer.Green => Green,
                Layer.Blue => Blue,
                Layer.Yellow => Yellow,
                Layer.Purple => Purple,
                Layer.Drawed => Drawed,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void Awake()
        {
            InitInstance(this);
        }
    }
}

using UnityEngine;
using System;

namespace LiquidTetris
{
    [Serializable]
    public sealed class ShapeMaterial
    {
        [SerializeField] private Texture2D _textureForParticles;
        [SerializeField] private int _layer;

        public Texture2D Texture => _textureForParticles;
        public int Layer => _layer;

        public override bool Equals(object obj) => base.Equals(obj);
        public override int GetHashCode() => base.GetHashCode();

        public static bool operator ==(ShapeMaterial a, ShapeMaterial b) => a.Layer == b.Layer;
        public static bool operator !=(ShapeMaterial a, ShapeMaterial b) => a.Layer != b.Layer;
    }
}

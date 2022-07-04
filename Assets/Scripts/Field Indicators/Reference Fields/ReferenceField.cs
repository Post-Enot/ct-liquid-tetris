using System;
using UnityEngine;

namespace FieldIndicators
{
    public class ReferenceField<T> : ScriptableObject
    {
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueChanged?.Invoke(_value);
            }
        }

        public event Action<T> ValueChanged;

        [NonSerialized] private T _value;
    }
}

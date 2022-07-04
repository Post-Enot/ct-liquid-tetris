using TMPro;
using UnityEngine;

namespace FieldIndicators
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public abstract class TMProBasedIndicator<T1, T2> : MonoBehaviour where T1 : ReferenceField<T2>
    {
        [SerializeField] private T1 _referenceField;

        private TextMeshProUGUI _label;

        private void Awake()
        {
            _label = GetComponent<TextMeshProUGUI>();
            if (_referenceField != null)
            {
                _referenceField.ValueChanged += UpdateLabel;
                UpdateLabel(_referenceField.Value);
            }
        }

        private void OnDestroy()
        {
            if (_referenceField != null)
            {
                _referenceField.ValueChanged -= UpdateLabel;
            }
        }

        public abstract string FormatValue(T2 value);

        public void UpdateLabel(T2 value)
        {
            string formatedValue = FormatValue(value);
            _label.text = formatedValue;
        }
    }
}

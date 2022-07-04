using UnityEngine;
using TMPro;

namespace FieldIndicators
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class TimeIndicator : TMProBasedIndicator<FloatReferenceField, float>
    {
        public override string FormatValue(float timeInSeconds)
        {
            int intTimeInSeconds = (int)timeInSeconds;
            int minutes = intTimeInSeconds / 60;
            int seconds = intTimeInSeconds % 60;
            return seconds < 10 ? $"{minutes}:0{seconds}" : $"{minutes}:{seconds}";
        }
    }
}

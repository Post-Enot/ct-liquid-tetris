namespace FieldIndicators
{
    public sealed class IntFieldIndicator : TMProBasedIndicator<IntReferenceField, int>
    {
        public override string FormatValue(int value)
        {
            return value.ToString();
        }
    }
}

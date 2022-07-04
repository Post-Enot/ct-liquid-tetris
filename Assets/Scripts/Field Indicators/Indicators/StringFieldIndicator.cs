namespace FieldIndicators
{
    public sealed class StringFieldIndicator : TMProBasedIndicator<StringReferenceField, string>
    {
        public override string FormatValue(string value)
        {
            return value;
        }
    }
}

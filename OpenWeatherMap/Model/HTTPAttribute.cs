namespace Model
{
    public class HTTPAttribute
    {
        public enum AttributeType
        {
            HttpHeader,
            HttpURLParameter
        }

        public virtual AttributeType Type { get; }

        public string Name { get; set; }
        public string Value { get; set; }
        public HTTPAttribute(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
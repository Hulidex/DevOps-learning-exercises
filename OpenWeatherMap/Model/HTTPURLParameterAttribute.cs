namespace Model
{
    public class HTTPURLParameterAttribute : HTTPAttribute
    {
        private readonly AttributeType type = HTTPAttribute.AttributeType.HttpURLParameter;
        public override AttributeType Type
        {
            get { return type; }
        }

        public HTTPURLParameterAttribute(string name, string value)
            : base(name, value) { }
    }
}
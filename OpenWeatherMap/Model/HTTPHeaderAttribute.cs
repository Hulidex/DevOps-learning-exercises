namespace Model
{
    public class HTTPHeaderAttribute : HTTPAttribute
    {
        private readonly AttributeType type = HTTPAttribute.AttributeType.HttpHeader;
        public override AttributeType Type
        {
            get { return type; }
        }

        public HTTPHeaderAttribute(string name, string value) : base(name, value) { }
    }
}
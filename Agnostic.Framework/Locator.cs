namespace Agnostic.Framework
{
    public class Locator
    {
        public string Value { get; init; }
        public LocatorType Type { get; init; }
        internal Locator(string value, LocatorType type)
        {
            Value = value;
            Type = type;
        }
    }
}
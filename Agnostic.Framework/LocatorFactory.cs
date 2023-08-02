namespace Agnostic.Framework
{
    public static class LocatorFactory
    {
        public static Locator Id(string id) => new Locator(id, LocatorType.Id);
        public static Locator Name(string name) => new Locator(name, LocatorType.Name);
        public static Locator Css(string cssSelector) => new Locator(cssSelector, LocatorType.CssSelector);
        public static Locator Xpath(string xpath) => new Locator(xpath, LocatorType.Xpath);
    }
}
using OpenQA.Selenium;
using Agnostic.Framework;

namespace Agnostic.Selenium
{
    internal static class LocatorFactory
    {
        public static By GetBy(this Locator locator)
        {
            return locator.Type switch
            {
                LocatorType.Id => By.Id(locator.Value),
                LocatorType.Name => By.Name(locator.Value),
                LocatorType.CssSelector => By.CssSelector(locator.Value),
                LocatorType.Xpath => By.XPath(locator.Value),
                _ => throw new NotSupportedException($"Locator type {locator.Type} not supported.")
            };
        }
    }
}
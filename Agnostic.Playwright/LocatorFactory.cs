using Agnostic.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agnostic.Playwright
{
    internal static class LocatorFactory
    {
        internal static string GetSelector(this Locator locator)
        {
            return locator.Type switch
            {
                LocatorType.Id => $"#{locator.Value}",
                LocatorType.Name => $"//*[@name='{locator.Value}']",
                LocatorType.CssSelector or LocatorType.Xpath => locator.Value,
                _ => throw new NotSupportedException($"Locator type '{locator.Type}' is not supported!")
            };
        }
    }
}

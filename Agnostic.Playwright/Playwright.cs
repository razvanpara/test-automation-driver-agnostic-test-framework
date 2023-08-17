using Agnostic.Framework;
using Microsoft.Playwright;
using System;

namespace Agnostic.Playwright
{
    public class Playwright : IDriver
    {
        private readonly IPage _page;
        private readonly IBrowser _browser;

        public Playwright(IBrowser browser)
        {
            _browser = browser;
            _page = browser.NewPageAsync().Result;
        }
        public void Click(Locator element)
        {
            _page.ClickAsync(element.GetSelector());
            this.WaitFor(null, TimeSpan.FromMilliseconds(100));
        }

        public void Dispose() => _browser.CloseAsync().Wait();

        public void DragAndDrop(Locator element, Locator target)
        {
            _page.DragAndDropAsync(element.GetSelector(), target.GetSelector());
            WaitFor(null, TimeSpan.FromMilliseconds(250));
        }

        public void EnterText(Locator element, string text) => _page.Locator(element.GetSelector()).TypeAsync(text).Wait();

        public string GetAttribute(Locator element, string attribute)
        {
            var selector = element.GetSelector();
            var xpathExpr = $"() => document.evaluate(`{selector}`, document).iterateNext().{attribute};";
            var other = $"() => document.querySelector('{selector}').{attribute}";
            return _page.EvaluateAsync<string>(element.Type == LocatorType.Xpath ? xpathExpr : other).Result;
        }

        public string GetSelectedText(Locator element) => _page.EvaluateAsync<string>("(selector) => document.querySelector(selector).selectedOptions[0].innerText", element.GetSelector()).Result;

        public string GetText(Locator element) => _page.Locator(element.GetSelector()).TextContentAsync().Result;

        public string GetUrl() => _page.Url;

        public void GoToUrl(string url) => _page.GotoAsync(url).Wait();

        public void SelectIndex(Locator element, int index)
        {
            _page.Locator(element.GetSelector()).SelectOptionAsync(new[]
        {
            new SelectOptionValue()
            {
                Index = index,
            }
        });
            this.WaitFor(null, TimeSpan.FromMilliseconds(100));
        }


        public void SelectValue(Locator element, string value)
        {
            _page.Locator(element.GetSelector()).SelectOptionAsync(new[]
        {
            new SelectOptionValue()
            {
                Value = value,
            }
        });
            this.WaitFor(null, TimeSpan.FromMilliseconds(100));
        }

        public void WaitFor(Locator element, TimeSpan timeout)
        {
            Thread.Sleep(timeout);
        }
    }
}
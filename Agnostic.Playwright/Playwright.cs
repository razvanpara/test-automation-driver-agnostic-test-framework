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
        public void Click(Locator element) => _page.ClickAsync(element.Value);

        public void Dispose() => _browser.CloseAsync().Wait();

        public void DragAndDrop(Locator element, Locator target) => _page.DragAndDropAsync(element.Value, target.Value);

        public void EnterText(Locator element, string text) => _page.Locator(element.Value).TypeAsync(text).Wait();

        public string GetAttribute(Locator element, string attribute) => _page.InputValueAsync(element.Value).Result;

        public string GetSelectedText(Locator element) => _page.InputValueAsync(element.Value).Result;

        public string GetText(Locator element) => _page.Locator(element.Value).TextContentAsync().Result;

        public string GetUrl() => _page.Url;

        public void GoToUrl(string url) => _page.GotoAsync(url).Wait();

        public void SelectIndex(Locator element, int index)
        {
            _page.Locator(element.Value).SelectOptionAsync(new[]
        {
            new SelectOptionValue()
            {
                Index = index,
            }
        });
            Thread.Sleep(100);
        }


        public void SelectValue(Locator element, string value)
        {
            _page.Locator(element.Value).SelectOptionAsync(new[]
        {
            new SelectOptionValue()
            {
                Value = value,
            }
        });
            Thread.Sleep(100);
        }

        public void WaitFor(Locator element, TimeSpan timeout)
        {
            //throw new NotImplementedException();
        }
    }
}
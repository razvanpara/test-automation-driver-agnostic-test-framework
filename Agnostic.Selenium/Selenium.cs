using OpenQA.Selenium;
using Agnostic.Framework;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;

namespace Agnostic.Selenium
{
    public class Selenium : IDriver
    {
        private readonly IWebDriver _driver;

        public Selenium(IWebDriver driver)
        {
            _driver = driver;
        }
        public void Click(Locator element) => _driver.FindElement(element.GetBy());

        public void DragAndDrop(Locator element, Locator target) =>
            new Actions(_driver).DragAndDrop(
                _driver.FindElement(element.GetBy()),
                _driver.FindElement(target.GetBy())
                );

        public void EnterText(Locator element, string text) => _driver.FindElement(element.GetBy()).SendKeys(text);

        public string GetText(Locator element) => _driver.FindElement(element.GetBy()).Text;

        public string GetUrl() => _driver.Url;

        public void GoToUrl(string url) => _driver.Navigate().GoToUrl(url);


        private SelectElement GetSelect(Locator element)
        {
            var webElement = _driver.FindElement(element.GetBy());
            var selectBox = new SelectElement(webElement);
            return selectBox;
        }
        public void SelectIndex(Locator element, int index) => GetSelect(element).SelectByIndex(index);

        public void SelectValue(Locator element, string value) => GetSelect(element).SelectByValue(value);

        public void WaitFor(Locator element, TimeSpan timeout)
        {
            var wait = new WebDriverWait(_driver, timeout);
            wait.Until(drv =>
            {
                IWebElement? e = default;
                try
                {
                    e = drv.FindElement(element.GetBy());
                }
                catch (NoSuchElementException)
                {
                    // nothing to do here, returns null from outer scope
                }
                return e;
            });
            wait.Until(ExpectedConditions.ElementExists(element.GetBy()));
        }
    }
}
using OpenQA.Selenium;
using Agnostic.Framework;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;
using static System.Net.Mime.MediaTypeNames;
using System.Linq.Expressions;

namespace Agnostic.Selenium
{
    public class Selenium : IDriver
    {
        private readonly IWebDriver _driver;

        public Selenium(IWebDriver driver)
        {
            _driver = driver;
        }
        public void Click(Locator element) => _driver.FindElement(element.GetBy()).Click();

        public void DragAndDrop(Locator element, Locator target)
        {
            var dragAndDropScript = @"
function simulateDragDrop(sourceNode, destinationNode) {
    var EVENT_TYPES = {
        DRAG_END: 'dragend',
        DRAG_START: 'dragstart',
        DROP: 'drop'
    }

    function createCustomEvent(type) {
        var event = new CustomEvent(""CustomEvent"")
        event.initCustomEvent(type, true, true, null)
        event.dataTransfer = {
            data: {
            },
            setData: function(type, val) {
                this.data[type] = val
            },
            getData: function(type) {
                return this.data[type]
            }
        }
        return event
    }

    function dispatchEvent(node, type, event) {
        if (node.dispatchEvent) {
            return node.dispatchEvent(event)
        }
        if (node.fireEvent) {
            return node.fireEvent(""on"" + type, event)
        }
    }

    var event = createCustomEvent(EVENT_TYPES.DRAG_START)
    dispatchEvent(sourceNode, EVENT_TYPES.DRAG_START, event)

    var dropEvent = createCustomEvent(EVENT_TYPES.DROP)
    dropEvent.dataTransfer = event.dataTransfer
    dispatchEvent(destinationNode, EVENT_TYPES.DROP, dropEvent)

    var dragEndEvent = createCustomEvent(EVENT_TYPES.DRAG_END)
    dragEndEvent.dataTransfer = event.dataTransfer
    dispatchEvent(sourceNode, EVENT_TYPES.DRAG_END, dragEndEvent)
}
    simulateDragDrop(arguments[0], arguments[1]);
";

            var src = _driver.FindElement(element.GetBy());
            var trg = _driver.FindElement(target.GetBy());

            // selenium bug https://github.com/w3c/webdriver/issues/1488
            //var srcMid = GetMiddleCoords(src.Location, src.Size);
            //var trgMid = GetMiddleCoords(trg.Location, trg.Size);
            //new Actions(_driver)
            //    .MoveToElement(src)
            //    .ClickAndHold()
            //    .MoveToElement(trg)
            //    .Release()
            //    .DragAndDrop(src, trg)
            //    .Build()
            //    .Perform();
            ((IJavaScriptExecutor)_driver).ExecuteScript(dragAndDropScript, src, trg);
        }

        public void EnterText(Locator element, string text) => _driver.FindElement(element.GetBy()).SendKeys(text);

        public string GetText(Locator element) => _driver.FindElement(element.GetBy()).Text;

        public string GetAttribute(Locator element, string attribute)
        {
            var executor = ((IJavaScriptExecutor)_driver);
            var selector = element.GetBy();
            var expr = $"return arguments[0].{attribute};";
            var result = executor.ExecuteScript(expr, _driver.FindElement(selector));
            return result.ToString();
        }

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

        public string GetSelectedText(Locator element) => GetSelect(element).SelectedOption.Text;

        public void Dispose()
        {
            _driver.Quit();
        }

    }
}
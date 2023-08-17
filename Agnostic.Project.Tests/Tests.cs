using Agnostic.Framework;
using Microsoft.Playwright;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Agnostic.Selenium;

namespace Agnostic.Project.Tests
{
    public class Tests
    {
        IDriver _driver;

        static IEnumerable<ITestCaseData> Strategies => new List<ITestCaseData> {
            new TestCaseData(typeof(Agnostic.Selenium.Selenium)),
            new TestCaseData(typeof(Agnostic.Playwright.Playwright))
        };

        [SetUp]
        public void Setup()
        {
            string type = TestContext.CurrentContext.Test.Arguments[0].ToString();
            string seleniumType = typeof(Agnostic.Selenium.Selenium).ToString();
            string playwrightType = typeof(Agnostic.Playwright.Playwright).ToString();

            if (seleniumType == type) _driver = new Selenium.Selenium(new ChromeDriver());
            else if (playwrightType == type) _driver = new Playwright.Playwright(Microsoft.Playwright.Playwright.CreateAsync().Result.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            }).Result);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Dispose();
        }


        [TestCaseSource(nameof(Strategies))]
        public void TextInputFieldAcceptsInput(Type type)
        {
            var text = "Hello World";
            var pom = new PomClass(_driver);

            pom.GoToPage();
            pom.EnterInputFieldText("Hello World");
            var fieldText = pom.GetInputFieldText();

            Assert.Multiple(() =>
            {
                Assert.That(fieldText, Is.EqualTo(text));
            });
        }

        [TestCaseSource(nameof(Strategies))]
        public void SelectDropdownCanSelect(Type type)
        {
            var text = "Set to 50%";
            var metterValueExpected = "0.5";
            var pom = new PomClass(_driver);

            pom.GoToPage();
            pom.SelectDropdownValue("50%");
            var fieldText = pom.GetSelectedText();
            var meterValue = pom.GetMetterValue();

            Assert.Multiple(() =>
            {
                Assert.That(fieldText, Contains.Substring(text), $"Text '{text}' was not contained by element text '{fieldText}'");
                Assert.That(meterValue, Is.EqualTo(metterValueExpected));
            });
        }

        [TestCaseSource(nameof(Strategies))]
        public void ButtonColorChangesOnClick(Type type)
        {
            var colorBefore = "green";
            var colorAfter = "purple";

            var pom = new PomClass(_driver);
            pom.GoToPage();

            Assert.That(pom.GetButtonColor(), Contains.Substring(colorBefore));

            pom.ClickButton();

            Assert.That(pom.GetButtonColor(), Contains.Substring(colorAfter));
        }

        [TestCaseSource(nameof(Strategies))]
        public void DragAndDropTest(Type type)
        {
            var pom = new PomClass(_driver);
            pom.GoToPage();

            pom.ClickCheckbox();

            var (initX, initY) = pom.GetDragableElementCoords();

            pom.DragToRight();
            var (newX, newY) = pom.GetDragableElementCoords();

            Assert.That(int.Parse(newX), Is.GreaterThan(int.Parse(initX)));
        }
    }
}
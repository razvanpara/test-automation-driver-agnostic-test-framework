using Agnostic.Framework;
using Microsoft.Playwright;
using OpenQA.Selenium.Chrome;

namespace Agnostic.Project.Tests
{
    public class Tests
    {
        IDriver _driver;

        [SetUp]
        public void Setup()
        {
            _driver
                = new Selenium.Selenium(new ChromeDriver());
                //= new Playwright.Playwright(Microsoft.Playwright.Playwright.CreateAsync().Result.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                //{
                //    Headless = false
                //}).Result);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Dispose();
        }


        [Test]
        public void TextInputFieldAcceptsInput()
        {
            var text = "Hello World";
            var pom = new PomClass(_driver);

            pom.GoToPage();
            pom.EnterInputFieldText("Hello World");
            var fieldText = pom.GetInputFieldText();


            Assert.That(fieldText, Is.EqualTo(text));
        }

        [Test]
        public void SelectDropdownCanSelect()
        {
            var text = "Set to 50%";
            var pom = new PomClass(_driver);

            pom.GoToPage();
            pom.SelectDropdownValue("50%");
            var fieldText = pom.GetSelectedText();


            Assert.That(text.Contains(fieldText), $"Text '{text}' was not contained by element text '{fieldText}'");
        }
    }
}
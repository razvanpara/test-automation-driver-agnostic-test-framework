using Agnostic.Framework;
using OpenQA.Selenium.Chrome;

namespace Agnostic.Project.Tests
{
    public class Tests
    {
        IDriver _driver;

        [SetUp]
        public void Setup()
        {
            _driver = new Selenium.Selenium(new ChromeDriver());
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


            Assert.That(fieldText, Is.EqualTo(text));
        }
    }
}
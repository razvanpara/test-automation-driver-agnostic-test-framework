using Agnostic.Framework;

namespace Agnostic.Project
{

    public class PomClass
    {
        private readonly string URL = "https://seleniumbase.io/demo_page";
        private readonly IDriver _driver;
        public PomClass(IDriver driver)
        {
            _driver = driver;
        }

        //elements
        private Locator TextInput => LocatorFactory.Css("#myTextInput");
        private Locator TextAreaInput => LocatorFactory.Css("#textareaName");
        private Locator SelectDropdown => LocatorFactory.Css("#mySelect");


        //actions
        public void GoToPage() => _driver.GoToUrl(URL);
        public void EnterInputFieldText(string text) => _driver.EnterText(TextInput, text);
        public string GetInputFieldText() => _driver.GetAttribute(TextInput, "value");
        public void EnterTextAreaText(string text) => _driver.EnterText(TextAreaInput, text);
        public void GetTextAreaText() => _driver.GetText(TextAreaInput);
        public void SelectDropdownValue(string value) => _driver.SelectValue(SelectDropdown, value);
        public string GetSelectedText() => _driver.GetSelectedText(SelectDropdown);
    }
}
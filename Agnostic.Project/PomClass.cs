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
        private Locator TextInput => LocatorFactory.Xpath("//*[@id='myTextInput']");
        private Locator TextAreaInput => LocatorFactory.Name("textareaName");
        private Locator SelectDropdown => LocatorFactory.Id("mySelect");
        private Locator MetterBar => LocatorFactory.Css("#meterBar");
        private Locator Button => LocatorFactory.Id("myButton");
        private Locator DragAndDropSrc => LocatorFactory.Id("drop1");
        private Locator DragAndDropTo => LocatorFactory.Id("drop2");
        private Locator Draggable => LocatorFactory.Id("logo");
        private Locator Checkbox => LocatorFactory.Name("checkBoxName1");


        //actions
        public void GoToPage() => _driver.GoToUrl(URL);
        public void EnterInputFieldText(string text) => _driver.EnterText(TextInput, text);
        public string GetInputFieldText() => _driver.GetAttribute(TextInput, "value");
        public void EnterTextAreaText(string text) => _driver.EnterText(TextAreaInput, text);
        public void GetTextAreaText() => _driver.GetText(TextAreaInput);
        public void SelectDropdownValue(string value) => _driver.SelectValue(SelectDropdown, value);
        public string GetSelectedText() => _driver.GetSelectedText(SelectDropdown);
        public string GetMetterValue() => _driver.GetAttribute(MetterBar, "value");
        public void ClickButton() => _driver.Click(Button);
        public string GetButtonColor() => _driver.GetAttribute(Button, "style.color");
        public void ClickCheckbox() => _driver.Click(Checkbox);
        public void DragToRight() => _driver.DragAndDrop(Draggable, DragAndDropTo);
        public void DragToLeft() => _driver.DragAndDrop(Draggable, DragAndDropSrc);
        public (string, string) GetDragableElementCoords() => (_driver.GetAttribute(Draggable, "x"), _driver.GetAttribute(Draggable, "y"));
    }
}
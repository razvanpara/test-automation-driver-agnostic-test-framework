namespace Agnostic.Framework
{

    public interface IDriver
    {
        void GoToUrl(string url);
        string GetUrl();

        void EnterText(Locator element, string text);
        string GetText(Locator element);

        void Click(Locator element);
        void SelectValue(Locator element, string value);
        void SelectIndex(Locator element, int index);
        string GetSelectedText(Locator element);
        void DragAndDrop(Locator element, Locator target);

        void WaitFor(Locator element, TimeSpan timeout);
    }
}
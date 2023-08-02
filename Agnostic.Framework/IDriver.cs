namespace Agnostic.Framework
{

    public interface IDriver : IDisposable
    {
        void GoToUrl(string url);
        string GetUrl();

        void EnterText(Locator element, string text);
        string GetText(Locator element);
        string GetAttribute(Locator element, string attribute);
        void Click(Locator element);
        void SelectValue(Locator element, string value);
        void SelectIndex(Locator element, int index);
        string GetSelectedText(Locator element);
        void DragAndDrop(Locator element, Locator target);

        void WaitFor(Locator element, TimeSpan timeout);
    }
}
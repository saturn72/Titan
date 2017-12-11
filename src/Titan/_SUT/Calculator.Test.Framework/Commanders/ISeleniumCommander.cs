using Titan.Framework.Commanders;

namespace Calculator.Test.Framework.Commanders
{
    public interface ISeleniumCommander:ICommander
    {
        bool GoToIndexPage();
        bool SetElementText(string elementId, object value);
        bool Click(string elementId);
        string GetElementText(string elementId);
    }
}
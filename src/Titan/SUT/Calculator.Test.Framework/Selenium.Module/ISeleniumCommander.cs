using Titan.Framework.CommandAndQuery;

namespace Calculator.Test.Framework.Selenium.Module
{
    public interface ISeleniumCommander:ICommander
    {
        bool GoToIndexPage();
        bool SetElementText(string elementId, object value);
        bool Click(string elementId);
    }
}
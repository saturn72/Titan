using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace Calculator.Test.Framework.Selenium.Module
{
    public class SeleniumCommander : ISeleniumCommander
    {
        private static readonly RemoteWebDriver Driver = new ChromeDriver();

        public static readonly string WebUiPageUri =
            Path.Combine(TestUtil.GetCurrentWorkingDirectory(), "SUT\\Web\\index.html");

        public bool GoToIndexPage()
        {
            if (!File.Exists(WebUiPageUri))
                return false;
            Driver.Navigate().GoToUrl(WebUiPageUri);
            return true;
        }

        public bool SetElementText(string elementId, object value)
        {
            var elem = FindElementById(elementId);
            if (elem == null)
                return false;

            elem.SendKeys(value.ToString());
            return true;
        }



        public bool Click(string elementId)
        {
            var elem = Driver.FindElementById(elementId);
            if (elem == null)
                return false;
            elem.Click();
            return true;
        }

        #region Utilities
        private static IWebElement FindElementById(string elementId)
        {
            return Driver.FindElementById(elementId);
        }
        #endregion
    }
}
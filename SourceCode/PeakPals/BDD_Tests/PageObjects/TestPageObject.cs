using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PeakPals_BDD_Tests.Shared;
using System;
using System.Collections.ObjectModel;

namespace PeakPals_BDD_Tests.PageObjects
{
    public class TestPageObject : PageObject
    {
        public TestPageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "Test";
        }

        private IWebElement TestHistory => _webDriver.FindElement(By.Id("test-details-div"));
        private IWebElement Graph => _webDriver.FindElement(By.Id("hang-test-graph"));
        private IWebElement Table => _webDriver.FindElement(By.Id("hang-test-table"));

        public bool IsTestHistoryDisplayed()
        {
            System.Threading.Thread.Sleep(100);
            //checks if the tablediv exists
            return TestHistory != null;
        }

        public bool IsGraphDisplayed()
        {
            System.Threading.Thread.Sleep(100);
            return Graph != null;
        }

        public bool IsTableDisplayed()
        {
            System.Threading.Thread.Sleep(100);
            return Table != null;
        }
    }
}
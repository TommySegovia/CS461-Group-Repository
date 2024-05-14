using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PeakPals_BDD_Tests.Shared;
using System;
using System.Collections.ObjectModel;

namespace PeakPals_BDD_Tests.PageObjects
{
    public class ReportPageObject : PageObject
    {
        public ReportPageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "Report";
        }

        private IWebElement PullTestTableDiv => _webDriver.FindElement(By.Id("pull-test-table"));
        private IWebElement HistoryCardButton => _webDriver.FindElement(By.Id("test-expand-button"));

        public bool IsPullTestTableCreated()
        {
            System.Threading.Thread.Sleep(100);
            //checks if the tablediv exists
            return PullTestTableDiv != null;
        }

        public void ClickOnIndividualTestHistoryCard()
        {
            HistoryCardButton.Click();
        }
    }
}
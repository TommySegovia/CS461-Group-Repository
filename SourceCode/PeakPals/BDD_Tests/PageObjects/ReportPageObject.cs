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
        private IWebElement RecommendationsTab => _webDriver.FindElement(By.Id("recommendations-tab"));
        private IWebElement DataMissingMessage => _webDriver.FindElement(By.Id("missing-info-div"));
        private IWebElement RecommendedClimbsDiv => _webDriver.FindElement(By.Id("recommended-climbs-div"));

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

        public void ClickRecommendationsTab()
        {
            RecommendationsTab.Click();
        }

        public bool IsDataMissingMessageDisplayed()
        {
            return DataMissingMessage.Displayed;
        }
        public bool IsClimbRecommended()
        {
            return RecommendedClimbsDiv.Displayed;
        }


    }
}
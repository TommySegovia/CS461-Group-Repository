using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PeakPals_BDD_Tests.Shared;
using System.Collections.ObjectModel;

namespace PeakPals_BDD_Tests.PageObjects
{
    public class RecordPageObject : PageObject
    {
        public RecordPageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "Record";
        }

        private IWebElement PullupWeightField => _webDriver.FindElement(By.Id("pull-test-input"));
        private IWebElement BodyweightField => _webDriver.FindElement(By.Id("pull-test-bodyweight-input"));
        private IWebElement RecordButton => _webDriver.FindElement(By.Id("pull-test-submit-button"));
        private IWebElement PullTestAccordion => _webDriver.FindElement(By.Id("test-accordion-pull"));

        public void OpenPullTestAccordion()
        {
            System.Threading.Thread.Sleep(100);
            PullTestAccordion.Click();
        }
        public void EnterPullupWeight(int pullupWeight)
        {
            PullupWeightField.Clear();
            PullupWeightField.SendKeys(pullupWeight.ToString());
        }

        public void EnterBodyweight(int bodyweight)
        {
            
            System.Threading.Thread.Sleep(200);
            BodyweightField.Clear();
            BodyweightField.SendKeys(bodyweight.ToString());
        }

        public void ClickRecordButton()
        {
            RecordButton.Click();
        }

        public bool IsPullupWeightFieldEmpty()
        {
            return PullupWeightField.GetAttribute("value") == "";
        }

        public bool IsBodyweightFieldEmpty()
        {
            return BodyweightField.GetAttribute("value") == "";
        }

        public bool DoesPullupWeightFieldExist()
        {
            return PullupWeightField != null;
        }

    }
}
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
        
        //hang
        private IWebElement HangTimeField => _webDriver.FindElement(By.Id("hang-test-input"));
        private IWebElement HangBodyweightField => _webDriver.FindElement(By.Id("hang-test-bodyweight-input"));
        private IWebElement HangRecordButton => _webDriver.FindElement(By.Id("hang-test-submit-button"));
        private IWebElement HangTestAccordion => _webDriver.FindElement(By.Id("test-accordion-hang"));

        //pull up
        private IWebElement PullupWeightField => _webDriver.FindElement(By.Id("pull-test-input"));
        private IWebElement BodyweightField => _webDriver.FindElement(By.Id("pull-test-bodyweight-input"));
        private IWebElement RecordButton => _webDriver.FindElement(By.Id("pull-test-submit-button"));
        private IWebElement PullTestAccordion => _webDriver.FindElement(By.Id("test-accordion-pull"));

        //hammerCurl
        private IWebElement HammerCurlWeightField => _webDriver.FindElement(By.Id("hammerCurl-test-input"));
        private IWebElement HammerCurlBodyweightField => _webDriver.FindElement(By.Id("hammerCurl-test-bodyweight-input"));
        private IWebElement HammerCurlRecordButton => _webDriver.FindElement(By.Id("hammerCurl-test-submit-button"));
        private IWebElement HammerCurlTestAccordion => _webDriver.FindElement(By.Id("test-accordion-hammerCurl"));

        //hipFlexibility
        private IWebElement HipFlexibilityField => _webDriver.FindElement(By.Id("hipFlexibility-test-input"));
        private IWebElement HipFlexibilityRecordButton => _webDriver.FindElement(By.Id("hipFlexibility-test-submit-button"));
        private IWebElement HipFlexibilityTestAccordion => _webDriver.FindElement(By.Id("test-accordion-hipFlexibility"));

        //hamstringFlexibility
        private IWebElement HamstringFlexibilityField => _webDriver.FindElement(By.Id("hamstringFlexibility-test-input"));
        private IWebElement HamstringFlexibilityRecordButton => _webDriver.FindElement(By.Id("hamstringFlexibility-test-submit-button"));
        private IWebElement HamstringFlexibilityTestAccordion => _webDriver.FindElement(By.Id("test-accordion-hamstringFlexibility"));

        //repeater
        private IWebElement RepeaterField => _webDriver.FindElement(By.Id("repeater-test-input"));
        private IWebElement RepeaterRecordButton => _webDriver.FindElement(By.Id("repeater-test-submit-button"));
        private IWebElement RepeaterTestAccordion => _webDriver.FindElement(By.Id("test-accordion-repeater"));

        //smallestEdge
        private IWebElement SmallestEdgeField => _webDriver.FindElement(By.Id("smallestEdge-test-input"));
        private IWebElement SmallestEdgeRecordButton => _webDriver.FindElement(By.Id("smallestEdge-test-submit-button"));
        private IWebElement SmallestEdgeTestAccordion => _webDriver.FindElement(By.Id("test-accordion-smallestEdge"));

        //campusBoard
        private IWebElement CampusBoardField => _webDriver.FindElement(By.Id("campusBoard-test-input"));
        private IWebElement CampusBoardRecordButton => _webDriver.FindElement(By.Id("campusBoard-test-submit-button"));
        private IWebElement CampusBoardTestAccordion => _webDriver.FindElement(By.Id("test-accordion-campusBoard"));


        

        //pull test
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

        //hang test
        public void OpenHangTestAccordion()
        {
            System.Threading.Thread.Sleep(100);
            HangTestAccordion.Click();
        }
        public void EnterHangWeight(int hangWeight)
        {
            HangTimeField.Clear();
            HangTimeField.SendKeys(hangWeight.ToString());
        }
        public void EnterHangBodyweight(int bodyweight)
        {
            HangBodyweightField.Clear();
            HangBodyweightField.SendKeys(bodyweight.ToString());
        }
        public void ClickHangRecordButton()
        {
            HangRecordButton.Click();
        }

        //hammer curl test
        public void OpenHammerCurlAccordion()
        {
            System.Threading.Thread.Sleep(100);
            HammerCurlTestAccordion.Click();
        }
        public void EnterHammerCurlWeight(int hammerCurlWeight)
        {
            HammerCurlWeightField.Clear();
            HammerCurlWeightField.SendKeys(hammerCurlWeight.ToString());
        }
        public void EnterHammerCurlBodyweight(int bodyweight)
        {
            HammerCurlBodyweightField.Clear();
            HammerCurlBodyweightField.SendKeys(bodyweight.ToString());
        }
        public void ClickHammerCurlRecordButton()
        {
            HammerCurlRecordButton.Click();
        }

        //hip flexibility test
        public void OpenHipFlexibilityAccordion()
        {
            System.Threading.Thread.Sleep(100);
            HipFlexibilityTestAccordion.Click();
        }
        public void EnterHipFlexibilityDistance(int hipFlexibilityDistance)
        {
            HipFlexibilityField.Clear();
            HipFlexibilityField.SendKeys(hipFlexibilityDistance.ToString());
        }
        public void ClickHipFlexibilityRecordButton()
        {
            HipFlexibilityRecordButton.Click();
        }

        //hamstring flexibility test
        public void OpenHamstringFlexibilityAccordion()
        {
            System.Threading.Thread.Sleep(100);
            HamstringFlexibilityTestAccordion.Click();
        }
        public void EnterHamstringFlexibilityDistance(int hamstringFlexibilityDistance)
        {
            HamstringFlexibilityField.Clear();
            HamstringFlexibilityField.SendKeys(hamstringFlexibilityDistance.ToString());
        }
        public void ClickHamstringFlexibilityRecordButton()
        {
            HamstringFlexibilityRecordButton.Click();
        }

        //repeater test
        public void OpenRepeaterAccordion()
        {
            System.Threading.Thread.Sleep(100);
            RepeaterTestAccordion.Click();
        }
        public void EnterRepeaterTime(int repeater)
        {
            RepeaterField.Clear();
            RepeaterField.SendKeys(repeater.ToString());
        }
        public void ClickRepeaterRecordButton()
        {
            RepeaterRecordButton.Click();
        }

        //smallest edge test
        public void OpenSmallestEdgeAccordion()
        {
            System.Threading.Thread.Sleep(100);
            SmallestEdgeTestAccordion.Click();
        }
        public void EnterSmallestEdgeSize(int smallestEdgeSize)
        {
            SmallestEdgeField.Clear();
            SmallestEdgeField.SendKeys(smallestEdgeSize.ToString());
        }
        public void ClickSmallestEdgeRecordButton()
        {
            SmallestEdgeRecordButton.Click();
        }

        //campus board test
        public void OpenCampusBoardAccordion()
        {
            System.Threading.Thread.Sleep(100);
            CampusBoardTestAccordion.Click();
        }
        public void SelectCampusBoardOption(int option)
        {
            SelectElement select = new SelectElement(CampusBoardField);
            select.SelectByIndex(option);
        }
        public void ClickCampusBoardRecordButton()
        {
            CampusBoardRecordButton.Click();
        }
    }
}
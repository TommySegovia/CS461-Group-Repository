using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PeakPals_BDD_Tests.Shared;
using System.Collections.ObjectModel;

namespace PeakPals_BDD_Tests.PageObjects
{
    public class ClimbPageObject : PageObject
    {
        public ClimbPageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "Climb";
        }
        
        private IWebElement FirstAscent => _webDriver.FindElement(By.Id("first-ascent"));
        private IWebElement TypeButton => _webDriver.FindElement(By.Id("type-button"));
        private IWebElement ClimbLocation => _webDriver.FindElement(By.Id("climb-location"));
        private IWebElement ClimbGrade => _webDriver.FindElement(By.Id("climb-protection"));
        private IWebElement ClimbProtection => _webDriver.FindElement(By.Id("grade-button"));



        public void GoToClimbPage(string climbId)
        {
            System.Threading.Thread.Sleep(3005);
            _webDriver.Navigate().GoToUrl(Common.UrlForClimb(climbId));
        }

        public bool DoesAreaDescriptionNameCoordinatesExist()
        {
            try
            {
                var element = FirstAscent;
                var element2 = TypeButton;
                var element3 = ClimbLocation;
                var element4 = ClimbGrade;
                var element5 = ClimbProtection;
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void ScrollToElement(IWebElement element)
        {
            ((IJavaScriptExecutor)_webDriver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public IWebElement GetFirstAscentElement()
        {
            return FirstAscent;
        }


    }
}
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PeakPals_BDD_Tests.Shared;
using System.Collections.ObjectModel;

namespace PeakPals_BDD_Tests.PageObjects
{
    public class AreaPageObject : PageObject
    {
        public AreaPageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "Area";
        }
        
        private IWebElement AreaDescription => _webDriver.FindElement(By.Id("pages-description"));
        private IWebElement AreaName => _webDriver.FindElement(By.Id("pages-title-name"));
        private IWebElement AreaCoordinates => _webDriver.FindElement(By.Id("pages-coordinates"));
        private IWebElement ChildLink => _webDriver.FindElement(By.Id("child-name"));



        public void GoToAreaPage(string areaId)
        {
            System.Threading.Thread.Sleep(3005);
            _webDriver.Navigate().GoToUrl(Common.UrlForArea(areaId));
        }

        public bool DoesAreaDescriptionNameCoordinatesExist()
        {
            try
            {
                var element = AreaDescription;
                var element2 = AreaName;
                var element3 = AreaCoordinates;
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void ClickChildLink()
        {
            ChildLink.Click();
        }

        public bool IsOnAreaPage(string areaId)
        {
            System.Threading.Thread.Sleep(3000);
            return _webDriver.Url == Common.UrlForArea(areaId);
        }




    }
}
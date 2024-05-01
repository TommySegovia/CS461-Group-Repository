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
        private IWebElement AreaCreationDate => _webDriver.FindElement(By.Id("createdOn"));
        private IWebElement AreaTotalClimbs => _webDriver.FindElement(By.Id("pages-total-climbs"));
        private IWebElement AreaOrganization => _webDriver.FindElement(By.Id("organizations-text"));
        private IWebElement AreaImages => _webDriver.FindElement(By.Id("area-images"));
        private IWebElement AreaImageModal => _webDriver.FindElement(By.Id("modal-view"));
        private IWebElement ModalImages => _webDriver.FindElement(By.Id("modal-image"));


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
            System.Threading.Thread.Sleep(1500);
            return _webDriver.Url == Common.UrlForArea(areaId);
        }

        public bool DoesAreaCreationClimbsOrganizationExist()
        {
            try
            {
                var element = AreaCreationDate;
                var element_two = AreaTotalClimbs;
                var element_three = AreaOrganization;
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

        public IWebElement GetOrganizationElement()
        {
            return AreaOrganization;
        }

        public bool DoesAreaImagesExist()
        {
            try
            {
                var element = AreaImages;
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool DoesImageModalExist()
        {
            
            try
            {
                var element = AreaImageModal;
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool DoesImageModalContainImages()
        {
            
            try
            {
                var element = ModalImages;
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void ClickMoreImagesButton()
        {
            AreaImages.Click();
            System.Threading.Thread.Sleep(3000);
        }



    }
}
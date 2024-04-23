using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PeakPals_BDD_Tests.Shared;
using System.Collections.ObjectModel;

namespace PeakPals_BDD_Tests.PageObjects
{
    public class SearchPageObject : PageObject
    {
        public SearchPageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "Search";
        }

        private IWebElement OpenSearchButton => _webDriver.FindElement(By.Id("searchModalButton"));
        private IWebElement SearchModal => _webDriver.FindElement(By.Id("searchModal"));
        private IWebElement SearchInput => _webDriver.FindElement(By.Id("search-input"));
        private IWebElement SearchButton => _webDriver.FindElement(By.Id("search-button"));
        private IWebElement ErrorMessage => _webDriver.FindElement(By.ClassName("error-message"));
        private IWebElement SearchResults => _webDriver.FindElement(By.Id("areas-div"));

        private IWebElement PullupWeightField => _webDriver.FindElement(By.Id("pull-test-input"));
        private IWebElement BodyweightField => _webDriver.FindElement(By.Id("pull-test-bodyweight-input"));
        private IWebElement RecordButton => _webDriver.FindElement(By.Id("pull-test-submit-button"));
        private IWebElement PullTestAccordion => _webDriver.FindElement(By.Id("test-accordion-pull"));

        public void OpenSearchModal()
        {
            OpenSearchButton.Click();
        }

        public bool DoesSearchModalExist() {
            try {
                var element = SearchModal;
                return true;
            }
            catch (NoSuchElementException) {
                return false;
            }
        }

        public void EnterSearchText(string searchText)
        {
            SearchInput.Clear();
            SearchInput.SendKeys(searchText);
        }

        public void ClickSearchButton()
        {
            SearchButton.Click();
        }

        public bool DoesSearchResultsExist()
        {
            System.Threading.Thread.Sleep(5000);
            try
            {
                var element = SearchResults;
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void ClickSearchResult()
        {
            SearchResults.Click();
            
        }

        public void GoToAreaPage(string areaId)
        {
            _webDriver.Navigate().GoToUrl(Common.UrlForArea(areaId));
        }

        public bool IsOnAreaPage(string areaId)
        {
            return _webDriver.Url == Common.UrlForArea(areaId);
        }

        public bool DoesErrorMessageExist()
        {
            try
            {
                var element = ErrorMessage;
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

  

    }
}
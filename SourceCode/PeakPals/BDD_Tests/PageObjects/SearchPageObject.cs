using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PeakPals_BDD_Tests.Shared;
using System;
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
        private IWebElement ClimbingLogsName => _webDriver.FindElement(By.Id("climb-attempt-name"));
        private IWebElement MapElement => _webDriver.FindElement(By.Id("dynamic-map"));
        private IWebElement PaginationElement => _webDriver.FindElement(By.Id("pagination-area"));
        private IWebElement RightPaginationButton => _webDriver.FindElement(By.Id("next-button"));
        private IWebElement CurrentPageIcon => _webDriver.FindElement(By.Id("current-button"));

        

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

        public bool DoesClimbingLogsExist()
        {
            System.Threading.Thread.Sleep(3000);
            try
            {
                var element = ClimbingLogsName;
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool DoesMapExist()
        {
            try
            {
                var element = MapElement;
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool DoesPaginationExist()
        {
            try
            {
                var element = PaginationElement;
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            } 
        }

        public void ClickPaginationButton()
        {
            RightPaginationButton.Click();
        }

        public bool IsNextPage()
        {
            if(CurrentPageIcon.Text == "2")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
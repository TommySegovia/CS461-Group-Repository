using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PeakPals_BDD_Tests.Shared;
using System.Collections.ObjectModel;
using System;

namespace PeakPals_BDD_Tests.PageObjects
{
    public class CommunityPageObject : PageObject
    {
        public CommunityPageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "Community";
        }

        private IWebElement GroupSearchBar => _webDriver.FindElement(By.Id("community-group-search-input"));
        private IWebElement SearchButton => _webDriver.FindElement(By.Id("community-group-search-button"));
        

        public void SearchForCommunityGroup(string communityGroupName)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_webDriver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
            System.Threading.Thread.Sleep(500);
            GroupSearchBar.SendKeys(communityGroupName);
            System.Threading.Thread.Sleep(500);
            SearchButton.Click();
        }
        public void ClickCommunityGroup(string communityGroupName)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_webDriver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
            System.Threading.Thread.Sleep(500);
            IWebElement communityGroup = _webDriver.FindElement(By.XPath($"//a[text()='{communityGroupName}']"));
            communityGroup.Click();
        }
        public void JoinCommunityGroup()
        {
            IWebElement communityGroupButton = _webDriver.FindElement(By.Id("community-group-button"));
            if (communityGroupButton.Text == "Join Group")
            {
                communityGroupButton.Click();
            }
        }


    }
}
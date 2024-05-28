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
        private IWebElement CommentSection => _webDriver.FindElement(By.Id("card-comments"));
        private IWebElement TextEntryArea => _webDriver.FindElement(By.Id("comment-textarea"));
        private IWebElement SubmitButton => _webDriver.FindElement(By.Id("submitMessageButton"));
        private IWebElement AddCommentButton => _webDriver.FindElement(By.Id("addCommentButton"));
        

        public void SearchForCommunityGroup(string communityGroupName)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_webDriver;
            System.Threading.Thread.Sleep(500);
            GroupSearchBar.SendKeys(communityGroupName);
            System.Threading.Thread.Sleep(500);
            SearchButton.Click();
        }
        public void ClickCommunityGroup(string communityGroupName)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_webDriver;
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

        public void ClickJoinedCommunityGroup(string communityGroupName)
        {
            IWebElement communityGroup = _webDriver.FindElement(By.XPath($"//a[text()='{communityGroupName}']"));
            communityGroup.Click();
        }

        public bool DoesCommentSectionExist()
        {
            try
            {
                var element = CommentSection;
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void EnterTextIntoCommentForm(string text)
        {
            TextEntryArea.SendKeys(text);
        }

        public void SubmitComment()
        {
            SubmitButton.Click();
        }

        public bool DoesCommentExist(string text)
        {
            try
            {
                var element = _webDriver.FindElement(By.XPath($"//p[text()='{text}']"));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void ClickAddCommentButton()
        {
            AddCommentButton.Click();
        }

        public bool DoesClimbLogExist(string climbName)
        {
            try
            {
                var element = _webDriver.FindElement(By.XPath($"//h5[text()='{climbName}']"));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
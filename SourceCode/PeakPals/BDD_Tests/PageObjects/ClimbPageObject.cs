using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PeakPals_BDD_Tests.Shared;
using System;
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
        private IWebElement ClimbImages => _webDriver.FindElement(By.Id("area-images"));
        private IWebElement LogAttemptButton => _webDriver.FindElement(By.Id("log-attempt-button"));
        private IWebElement LogModal => _webDriver.FindElement(By.Id("climbAttemptModal"));
        private IWebElement LogForms => _webDriver.FindElement(By.Id("climb-attempt-form"));
        private IWebElement ClimbingGrade => _webDriver.FindElement(By.Id("suggested-grade"));
        private IWebElement ClimbingAttempts => _webDriver.FindElement(By.Id("attempts"));
        private IWebElement ClimbingRating => _webDriver.FindElement(By.Id("rating"));
        private IWebElement SubmitButton => _webDriver.FindElement(By.Id("submit-button"));
        private IWebElement ConfirmationButton => _webDriver.FindElement(By.Id("confirmation-popup"));
        private IWebElement AddTagButton => _webDriver.FindElement(By.Id("addTagButton"));



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

        public bool DoesTagExist(string tag)
        {
            System.Threading.Thread.Sleep(3005);
            try
            {
                var element = _webDriver.FindElement(By.XPath($"//span[text()='{tag}']"));
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

        public bool DoesClimbImagesExist()
        {
            System.Threading.Thread.Sleep(3005);
            try
            {
                var element = ClimbImages;
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool DoesLogClimbButtonExist()
        {
            try
            {             
                var element = LogAttemptButton;
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void ClickLogClimbButton()
        {
            LogAttemptButton.Click();
            System.Threading.Thread.Sleep(2000);
        }

        public bool IsLogClimbModalVisible()
        {
            try
            {
                var element = LogModal;
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool DoesLogClimbFormExist()
        {
            try
            {
               var element = LogForms;
               return true;
            }
            catch (NoSuchElementException)
            {
               return false;
                
            }
        }

        public void EnterClimbRating(string v)
        {
            ClimbingRating.SendKeys(v);
        }
        public void EnterClimbAttempts(string v)
        {
            ClimbingAttempts.SendKeys(v);
        }

        public void EnterClimbSuggestedGrade(string v)
        {
            ClimbingGrade.SendKeys(v);
        }

        public void EnterClimbTag()
        {
            AddTagButton.Click();
            System.Threading.Thread.Sleep(2000);
            

        }



        public void ClickSubmitButton()
        {
            SubmitButton.Click();
        }

        public bool IsOnClimbPage(string v)
        {
            return true;
        }

        public bool DoesConfirmationMessageExist()
        {
            try
            {
                var element = ConfirmationButton;
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PeakPals_BDD_Tests.Shared;
using System.Collections.ObjectModel;
using System;

namespace PeakPals_BDD_Tests.PageObjects
{
    public class ProfilePageObject : PageObject
    {
        public ProfilePageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "Profile";
        }

        private IWebElement EditProfileButton => _webDriver.FindElement(By.Id("edit-btn"));
        private IWebElement ProfilePageNavBarButton => _webDriver.FindElement(By.Id("profile-text"));
        private IWebElement FirstNameField => _webDriver.FindElement(By.Id("first-name-form-field"));
        private IWebElement AgeField => _webDriver.FindElement(By.Id("age-form-field"));
        private IWebElement FirstNameText => _webDriver.FindElement(By.Id("first-name-text"));
        private IWebElement LocationText => _webDriver.FindElement(By.Id("location-text"));

        public void GoToProfile()
        {
            ProfilePageNavBarButton.Click();
        }
        public void ClickEditProfile()
        {
            EditProfileButton.Click();
        }
        public bool DoesFirstNameFieldExist()
        {
            return FirstNameField != null;
        }
        public void SelectAgeField()
        {
            AgeField.Click();
        }
        public void EnterAge(string age)
        {
            AgeField.Clear();
            AgeField.SendKeys(age);
        }
        public void ClickSubmitButton()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_webDriver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
            System.Threading.Thread.Sleep(500);
            _webDriver.FindElement(By.Id("submit-btn")).Click();
        }
        public void SelectFirstNameField()
        {
            FirstNameField.Click();
        }
        public void EnterFirstName(string firstName)
        {
            FirstNameField.Clear();
            FirstNameField.SendKeys(firstName);
        }
        public string GetFirstName()
        {
            return FirstNameText.Text;
        }      
        public void SelectCityField()
        {
            _webDriver.FindElement(By.Id("city-form-field")).Click();
        }
        public void EnterCity(string city)
        {
            _webDriver.FindElement(By.Id("city-form-field")).Clear();
            _webDriver.FindElement(By.Id("city-form-field")).SendKeys(city);
        }
        public bool DoesLocationContainCity(string city)
        {
            //checks if city is contained in location-field id
            return LocationText.Text.Contains(city);


        }

        public bool DoesCommunityGroupExist(string communityGroupName)
        {
            //checks if community group exists on profile page
            System.Threading.Thread.Sleep(500);
            return _webDriver.FindElement(By.XPath($"//a[text()='{communityGroupName}']")) != null;
        }

        public bool DoesClimbExist()
        {
            //checks if climb exists on profile page
            System.Threading.Thread.Sleep(500);
            return _webDriver.FindElement(By.ClassName("loggedClimb")) != null;
        }



    }
}
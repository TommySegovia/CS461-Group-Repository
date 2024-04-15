using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PeakPals_BDD_Tests.Shared;
using System.Collections.ObjectModel;

namespace PeakPals_BDD_Tests.PageObjects
{
    public class LoginPageObject : PageObject
    {
        public LoginPageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "Login";
        }

        private IWebElement EmailField => _webDriver.FindElement(By.Id("Input_Email"));
        private IWebElement PasswordField => _webDriver.FindElement(By.Id("Input_Password"));
        private IWebElement LoginButton => _webDriver.FindElement(By.CssSelector("button[type='submit']"));
        public IWebElement RememberMeCheck => _webDriver.FindElement(By.Id("Input_RememberMe"));


        public void EnterEmail(string email)
        {
            EmailField.Clear();
            EmailField.SendKeys(email);
        }

        public void EnterPassword(string password)
        {
            PasswordField.Clear();
            PasswordField.SendKeys(password);
        }

        public void ClickLoginButton()
        {
            LoginButton.Click();
        }

        public void SetRememberMe(bool rememberMe)
        {
            if (rememberMe)
            {
                if (!RememberMeCheck.Selected)
                {
                    RememberMeCheck.Click();
                }
            }
            else
            {
                if (RememberMeCheck.Selected)
                {
                    RememberMeCheck.Click();
                }
            }
        }

    }
}
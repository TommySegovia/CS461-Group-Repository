using BoDi;
using PeakPals_BDD_Tests.Drivers;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reqnroll;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using PeakPals_BDD_Tests.PageObjects;
using PeakPals_BDD_Tests.Shared;
using NUnit.Framework;


namespace PeakPals_BDD_Tests.Hooks
{
    /// <summary>
    /// Share the same browser window for all scenarios
    /// </summary>
    /// <remarks>
    /// This makes the sequential execution of scenarios faster (opening a new browser window each time would take more time)
    /// As a tradeoff:
    ///  - we cannot run the tests in parallel
    ///  - we have to "reset" the state of the browser before each scenario
    /// </remarks>
    [Binding]
    public class SharedBrowserHooks
    {
        [BeforeTestRun]
        public static void BeforeTestRun(ObjectContainer testThreadContainer)
        {
            //Initialize a shared BrowserDriver in the global container
            testThreadContainer.BaseContainer.Resolve<BrowserDriver>();
        }
        [AfterScenario]
        public void AfterScenario(BrowserDriver browserDriver)
        {
            //Reset the browser state after each scenario
            browserDriver.Current.Navigate().GoToUrl(Common.UrlFor("Home"));
            //log out
            try
            {
                browserDriver.Current.FindElement(By.Id("logout-button")).Click();
            }
            catch (NoSuchElementException)
            {
                //do nothing
            }
        }
    }
}
using Reqnroll;
using OpenQA.Selenium;
using PeakPals_BDD_Tests.PageObjects;
using PeakPals_BDD_Tests.Shared;
using NUnit.Framework;
using PeakPals_BDD_Tests.Drivers;

namespace PeakPals_BDD_Tests.StepDefinitions;

[Binding]
public sealed class TBD_114StepDefinitions
{
    private readonly AreaPageObject _areaPage;
    private readonly ClimbPageObject _climbPage;
    private readonly SearchPageObject _searchPage;
    private readonly LoginPageObject _loginPage;
    private readonly IWebDriver _webDriver;
    public TBD_114StepDefinitions(BrowserDriver browserDriver)
    {
        _webDriver = browserDriver.Current;
        _searchPage = new SearchPageObject(_webDriver);
        _webDriver.Manage().Window.Maximize();
    }


    [TearDown]
    public void TearDown()
    {
        _webDriver.Quit();
    }

    [Given(@"I am a visitor on this site")]
    public void GivenIAmAVisitorOnThisSite()
    {
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Home"));
    }

    [When(@"I go to the location page")]
    public void WhenIGoToTheLocationPage()
    {
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Search"));
    }

    [Then(@"I will see a map loaded on the page")]
    public void ThenIWillSeeAMapLoadedOnThePage()
    {
        Assert.That(_searchPage.DoesMapExist());
    }
}




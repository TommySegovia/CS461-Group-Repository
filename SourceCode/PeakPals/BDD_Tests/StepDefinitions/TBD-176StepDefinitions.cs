using Reqnroll;
using OpenQA.Selenium;
using PeakPals_BDD_Tests.PageObjects;
using PeakPals_BDD_Tests.Shared;
using NUnit.Framework;
using PeakPals_BDD_Tests.Drivers;

namespace PeakPals_BDD_Tests.StepDefinitions;

[Binding]
public sealed class TBD_176StepDefinitions
{
    private readonly SearchPageObject _searchPage;
    private readonly LoginPageObject _loginPage;
    private readonly IWebDriver _webDriver;
    public TBD_176StepDefinitions(BrowserDriver browserDriver)
    {
        _webDriver = browserDriver.Current;
        _loginPage = new LoginPageObject(_webDriver);
        _searchPage = new SearchPageObject(_webDriver);
        _webDriver.Manage().Window.Maximize();
    }

    [TearDown]
    public void TearDown()
    {
        _webDriver.Quit();
    }

    [Given(@"I am a user with climbs logged")]
    public void GivenIAmAUserWithClimbsLogged()
    {
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Home"));
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Login"));
        _loginPage.EnterEmail("Bob@bobby.com");
        _loginPage.EnterPassword("Abc123!");
        _loginPage.ClickLoginButton();
    }

    [When(@"I move to the location page")]
    public void WhenIGoToTheLocationPage()
    {
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Search"));
    }

    [When(@"I see that my climbing log has multiple entries")]
    public void ThenISeeThatMyClimbingLogHasMultipleEntries()
    {
        Assert.That(_searchPage.DoesClimbingLogsExist());
    }

    [Then(@"I will see pagination buttons below the log")]
    public void ThenIWillSeePaginationButtonsBelowTheLog()
    {
        Assert.That(_searchPage.DoesPaginationExist());
    }

    // s2

    [Given(@"I go to the location page")]
    public void GivenIGoToTheLocationPage()
    {
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Search"));
    }

    [Given(@"I see my climbing log that has multiple entries")]
    public void GivenISeeMyClimbingLogThatHasMultipleEntries()
    {
        Assert.That(_searchPage.DoesClimbingLogsExist());
    }

    [When(@"I click on the pagination buttons below the log")]
    public void WhenWhenIClickOnThePaginationButtonsBelowTheLog()
    {
        _searchPage.ClickPaginationButton();
    }

    [Then(@"I will see that the entries have changed to a new page")]
    public void ThenThenIWillSeeThatTheEntriesHaveChangedToANewPage()
    {
        Assert.That(_searchPage.IsNextPage());
    }

}




using Reqnroll;
using OpenQA.Selenium;
using PeakPals_BDD_Tests.PageObjects;
using PeakPals_BDD_Tests.Shared;
using NUnit.Framework;
using PeakPals_BDD_Tests.Drivers;
using System.Security.Policy;

namespace PeakPals_BDD_Tests.StepDefinitions;

[Binding]
public sealed class TBD_143StepDefinitions
{
    private readonly ReportPageObject _reportPage;
    private readonly LoginPageObject _loginPage;
    private readonly TestPageObject _testPage;
    private readonly IWebDriver _webDriver;
    public TBD_143StepDefinitions(BrowserDriver browserDriver)
    {
        _webDriver = browserDriver.Current;
        _loginPage = new LoginPageObject(_webDriver);
        _reportPage = new ReportPageObject(_webDriver);
        _testPage = new TestPageObject(_webDriver);
        _webDriver.Manage().Window.Maximize();
    }

    [TearDown]
    public void TearDown()
    {
        _webDriver.Quit();
    }


    [Given(@"I am a user who has recorded tests")]
    public void GivenIAmAUserWhoHasRecordedTests()
    {
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Login"));
        _loginPage.EnterEmail("Bob@bobby.com");
        _loginPage.EnterPassword("Abc123!");
        _loginPage.ClickLoginButton();
    }

    [Given(@"I am on the report page")]
    public void GivenIAmOnTheReportPage()
    {
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Report"));
    }

    [When(@"I click on an individual test history card")]
    public void WhenIClickOnAnIndividualTestHistoryCard()
    {
        _reportPage.ClickOnIndividualTestHistoryCard();
    }

    [Then(@"I will see a display that will show the test history in a more clear way")]
    public void ThenIWillSeeADisplayThatWillShowTheTestHistoryInAMoreClearWay()
    {
        Assert.That(_testPage.IsTestHistoryDisplayed());
    }

    //s2

    [Then(@"I will see a graph and a table")]
    public void ThenThenIWillSeeAGraphAndATable()
    {
        Assert.That(_testPage.IsGraphDisplayed());
        Assert.That(_testPage.IsTableDisplayed());
    }
}




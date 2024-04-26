using Reqnroll;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using PeakPals_BDD_Tests.PageObjects;
using PeakPals_BDD_Tests.Shared;
using NUnit.Framework;
using PeakPals_BDD_Tests.Drivers;

namespace PeakPals_BDD_Tests.StepDefinitions;

[Binding]
public sealed class RecordingAndReportSteps
{
    private readonly RecordPageObject _recordPage;
    private readonly ReportPageObject _reportPage;
    private readonly LoginPageObject _loginPage;
    private readonly IWebDriver _webDriver;
    public RecordingAndReportSteps(BrowserDriver browserDriver)
    {
        _webDriver = browserDriver.Current;
        _recordPage = new RecordPageObject(_webDriver);
        _reportPage = new ReportPageObject(_webDriver);
        _loginPage = new LoginPageObject(_webDriver);
    }

    [TearDown]
    public void TearDown()
    {
        _webDriver.Quit();
    }

    [Given(@"I am a logged in user")]
    public void GivenIAmLoggedInUser()
    {
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Login"));
        _loginPage.EnterEmail("john@example.com");
        _loginPage.EnterPassword("Abc123!");
        _loginPage.ClickLoginButton();
    }

    [When("I am on the {string} page")]
    public void WhenIAmOnThePage(string report)
    {
        _webDriver.Navigate().GoToUrl(Common.UrlFor(report));
    }

    [When(@"I enter (-?\d+) into the bodyweight field")]
    public void WhenIEnterBodyweightIntoField(int bodyweight)
    {
        _recordPage.OpenPullTestAccordion();
        _recordPage.EnterBodyweight(bodyweight);
    }

    [When(@"I enter (\d+) into the pullup weight field")]
    public void WhenIEnterPullupWeightIntoField(int pullupWeight)
    {
        _recordPage.EnterPullupWeight(pullupWeight);
    }

    [When(@"I press the record button")]
    public void WhenIPressRecordButton()
    {
        _recordPage.ClickRecordButton();
    }

    [Then("I should see that the form was not submitted")]
    public void ThenIShouldSeeThatTheFormWasNotSubmitted()
    {
        Assert.That(_recordPage.IsPullupWeightFieldEmpty(), Is.False);
        Assert.That(_recordPage.IsBodyweightFieldEmpty(), Is.False);
    }

    [When(@"I have previously logged a pullup test")]
    public void WhenIHavePreviouslyLoggedPullupTest()
    {
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Record"));
        _recordPage.OpenPullTestAccordion();
        _recordPage.EnterBodyweight(150);
        _recordPage.EnterPullupWeight(100);
        _recordPage.ClickRecordButton();
    }

    [Then(@"I should see my previously logged pullup tests on the report page")]
    public void ThenIShouldSeePreviouslyLoggedPullupTests()
    {
        Assert.That(_reportPage.IsPullTestTableCreated(), Is.True);
    }

    [Then(@"I should see a pullup test added weight input field")]
    public void ThenIShouldSeePullupTestAddedWeightField()
    {
        Assert.That(_recordPage.DoesPullupWeightFieldExist(), Is.True);
    }

    [Then(@"my test should be logged")]
    public void ThenMyTestShouldBeLogged()
    {
        Assert.That(_recordPage.IsPullupWeightFieldEmpty(), Is.True);
        Assert.That(_recordPage.IsBodyweightFieldEmpty(), Is.True);
    }
}


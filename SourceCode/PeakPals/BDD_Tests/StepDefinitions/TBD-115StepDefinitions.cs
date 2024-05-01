using Reqnroll;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using PeakPals_BDD_Tests.PageObjects;
using PeakPals_BDD_Tests.Shared;
using NUnit.Framework;
using PeakPals_BDD_Tests.Drivers;
using Humanizer;
using Microsoft.SqlServer.Server;
using Moq;
using System.Security.Policy;
using ScottPlot.Drawing.Colormaps;
using static PeakPals_Project.Models.OpenBetaQueryResult;

namespace PeakPals_BDD_Tests.StepDefinitions;

[Binding]
public sealed class TBD_115StepDefinitions
{
    private readonly AreaPageObject _areaPage;
    private readonly ClimbPageObject _climbPage;
    private readonly SearchPageObject _searchPage;
    private readonly LoginPageObject _loginPage;
    private readonly IWebDriver _webDriver;
    public TBD_115StepDefinitions(BrowserDriver browserDriver)
    {
        _webDriver = browserDriver.Current;
        _searchPage = new SearchPageObject(_webDriver);
        _loginPage = new LoginPageObject(_webDriver);
        _areaPage = new AreaPageObject(_webDriver);
        _climbPage = new ClimbPageObject(_webDriver);
        _webDriver.Manage().Window.Maximize();
    }

    // s1

    [TearDown]
    public void TearDown()
    {
        _webDriver.Quit();
    }

    [Given(@"I am a user")]
    public void GivenIAmAUser()
    {
        System.Threading.Thread.Sleep(1000);
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Login"));
        _loginPage.EnterEmail("mifivok911@em2lab.com");
        _loginPage.EnterPassword("Abc123!");
        _loginPage.ClickLoginButton();
    }

    [Given(@"I access a climb page of my choosing")]
    public void GivenIAccessAClimbPageOfMyChoosing()
    {
        System.Threading.Thread.Sleep(1000);
        _climbPage.GoToClimbPage("1a2bfc70-4c94-51f5-8a9c-572916b860b5");
    }

    [Given(@"I see a button that allows me to log a climb")]
    public void GivenISeeAButtonThatAllowsMeToLogAClimb()
    {
        Assert.That(_climbPage.DoesLogClimbButtonExist(), Is.True);
    }

    [When(@"I click on the log climb button")]
    public void WhenIClickOnTheLogClimbButton()
    {
        _climbPage.ClickLogClimbButton();
    }

    [Then(@"I will be either redirected to a new page or a window will appear")]
    public void ThenIWillBeEitherRedirectedToANewPageOrAWindowWillAppear()
    {
        Assert.That(_climbPage.IsLogClimbModalVisible(), Is.True);
    }

    [Then(@"it will have forms available to fill out")]
    public void ThenItWillHaveFormsAvailableToFillOut()
    {
        Assert.That(_climbPage.DoesLogClimbFormExist(), Is.True);
    }

    // s2

    [Given(@"I clicked on the log climb button")]
    public void GivenIClickedOnTheLogClimbButton()
    {
        _climbPage.ClickLogClimbButton();
    }

    [When(@"I fill out each form with relevant data")]
    public void WhenIFillOutEachFormWithRelevantData()
    {
        _climbPage.EnterClimbAttempts("3");
        _climbPage.EnterClimbSuggestedGrade("V5");
        _climbPage.EnterClimbRating("4");
    }

    [When(@"I hit the submit button on the page")]
    public void WhenIHitTheSubmitButtonOnThePage()
    {
        _climbPage.ClickSubmitButton();
    }

    [Then(@"I should be redirected back to the climb page")]
    public void ThenIShouldBeRedirectedBackToTheClimbPage()
    {
        Assert.That(_climbPage.IsOnClimbPage("1a2bfc70-4c94-51f5-8a9c-572916b860b5"), Is.True);
    }

    [Then(@"I should recieve a confirmation message")]
    public void ThenIShouldRecieveAConfirmationMessage()
    {
        Assert.That(_climbPage.DoesConfirmationMessageExist(), Is.True);
    }

    // s3

    [Given(@"I am a user that has logged a climb before")]
    public void GivenIAmAUserThatHasLoggedAClimbBefore()
    {
        System.Threading.Thread.Sleep(1000);
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Login"));
        _loginPage.EnterEmail("mifivok911@em2lab.com");
        _loginPage.EnterPassword("Abc123!");
        _loginPage.ClickLoginButton();
    }

    [When(@"I access the main locations page")]
    public void WhenIAccessTheMainLocationsPage()
    {
        System.Threading.Thread.Sleep(1000);
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Search"));
    }

    [Then(@"I should see a listing of the climbing logs I have done")]
    public void ThenIShouldSeeAListingOfTheClimbingLogsIHaveDone()
    {
        Assert.That(_searchPage.DoesClimbingLogsExist(), Is.True);
    }

    // s4

    [Given(@"I am a different user that has never logged a climb")]
    public void GivenIAmADifferentUserThatHasNeverLoggedAClimb()
    {
        System.Threading.Thread.Sleep(1000);
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Login"));
        _loginPage.EnterEmail("cringe@cringe.com");
        _loginPage.EnterPassword("Abc123!");
        _loginPage.ClickLoginButton();
    }

    [Then(@"I should a blank listings area that tells me to log a climb to get results")]
    public void ThenIShouldABlankListingsAreaThatTellsMeToLogAClimbToGetResults()
    {
        Assert.That(_searchPage.DoesClimbingLogsExist(), Is.False);
    }
}




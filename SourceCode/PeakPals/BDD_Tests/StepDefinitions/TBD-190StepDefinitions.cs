using Reqnroll;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using PeakPals_BDD_Tests.PageObjects;
using PeakPals_BDD_Tests.Shared;
using NUnit.Framework;
using PeakPals_BDD_Tests.Drivers;

namespace PeakPals_BDD_Tests.StepDefinitions;

[Binding]
public sealed class TBD_190_StepDefinitions
{
    private readonly AreaPageObject _areaPage;
    private readonly ClimbPageObject _climbPage;
    private readonly LoginPageObject _loginPage;
    private readonly CommunityPageObject _communityPage;
    private readonly IWebDriver _webDriver;
    public TBD_190_StepDefinitions(BrowserDriver browserDriver)
    {
        _webDriver = browserDriver.Current;
        _loginPage = new LoginPageObject(_webDriver);
        _climbPage = new ClimbPageObject(_webDriver);
        _communityPage = new CommunityPageObject(_webDriver);
        _webDriver.Manage().Window.Maximize();
    }

    // s1

    [Given(@"that I am a user of this site")]
    public void GivenIAmAUserOfThisSite()
    {
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Home"));
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Login"));
        _loginPage.EnterEmail("ran@ranny.com");
        _loginPage.EnterPassword("Cba123!");
        _loginPage.ClickLoginButton();
    }

    [Given(@"I am viewing a community group page that I am owner of")]
    public void GivenIAmViewingACommunityGroupPageThatIAmOwnerOf()
    {
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Community"));
        System.Threading.Thread.Sleep(3005);
        _communityPage.ClickJoinedCommunityGroup("Boyz");
    }

    [Given(@"I see a climbing log that doesnt have my climb attempt")]
    public void GivenISeeAClimbingLogThatDoesntHaveMyClimbAttempt()
    {
        System.Threading.Thread.Sleep(1005);
        Assert.That(_communityPage.DoesClimbLogExist("Heavy Duty Judy"), Is.False);
    }

    [When(@"I go to log my climb attempt")]
    public void WhenIGoToLogMyClimbAttempt()
    {
        _climbPage.GoToClimbPage("a2406428-bf95-5b6e-a083-440ea230755b");
        System.Threading.Thread.Sleep(2005);
        _climbPage.ClickLogClimbButton();
        _climbPage.EnterClimbRating("5");
        _climbPage.EnterClimbAttempts("1");
        _climbPage.EnterClimbSuggestedGrade("5.10");
        _climbPage.ClickSubmitButton();

    }

    [When(@"I return back to my community group page")]
    public void WhenIReturnBackToMyCommunityGroupPage()
    {
        System.Threading.Thread.Sleep(1005);
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Community"));
        System.Threading.Thread.Sleep(2005);
        _communityPage.ClickJoinedCommunityGroup("Boyz");
    }
  

    [Then(@"the climbing log should have my climb attempt")]
    public void ThenTheClimbingLogShouldHaveMyClimbAttempt()
    {
        System.Threading.Thread.Sleep(3005);
        Assert.That(_communityPage.DoesClimbLogExist("Heavy Duty Judy"), Is.True);
    }

}


using Reqnroll;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using PeakPals_BDD_Tests.PageObjects;
using PeakPals_BDD_Tests.Shared;
using NUnit.Framework;
using PeakPals_BDD_Tests.Drivers;

namespace PeakPals_BDD_Tests.StepDefinitions;

[Binding]
public sealed class TBD_98_StepDefinitions
{
    private readonly AreaPageObject _areaPage;
    private readonly ClimbPageObject _climbPage;
    private readonly LoginPageObject _loginPage;
    private readonly CommunityPageObject _communityPage;
    private readonly IWebDriver _webDriver;
    public TBD_98_StepDefinitions(BrowserDriver browserDriver)
    {
        _webDriver = browserDriver.Current;
        _loginPage = new LoginPageObject(_webDriver);
        _communityPage = new CommunityPageObject(_webDriver);
        _webDriver.Manage().Window.Maximize();
    }

    // s1

    [Given(@"that I am a user")]
    public void GivenIAmAUser()
    {
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Home"));
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Login"));
        _loginPage.EnterEmail("ran@ranny.com");
        _loginPage.EnterPassword("Cba123!");
        _loginPage.ClickLoginButton();
    }

    [Given(@"I am viewing a community page that I joined")]
    public void GivenIAmViewingACommunityPageThatIJoined()
    {
        System.Threading.Thread.Sleep(2005);
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Community"));
        _communityPage.ClickJoinedCommunityGroup("Boyz");
    }

    [Given(@"I see a comment section")]
    public void GivenISeeACommentSection()
    {
        Assert.That(_communityPage.DoesCommentSectionExist(), Is.True);
    }

    [When(@"I enter in text into the comment form")]
    public void WhenIEnterInTextIntoTheCommentForms()
    {
        System.Threading.Thread.Sleep(2005);
        _communityPage.ClickAddCommentButton();
        System.Threading.Thread.Sleep(2005);
        _communityPage.EnterTextIntoCommentForm("BDD");
    }

    [When(@"I hit a submit button")]
    public void WhenIHitASubmitButton()
    {
        _communityPage.SubmitComment();
    }

    [Then(@"the comment will be displayed on the page")]
    public void ThenTheCommentWillBeDisplayedOnThePage()
    {
        System.Threading.Thread.Sleep(3005);
        _communityPage.DoesCommentExist("BDD");
    }

    
}


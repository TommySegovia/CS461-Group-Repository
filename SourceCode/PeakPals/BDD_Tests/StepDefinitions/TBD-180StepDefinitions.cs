using Reqnroll;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using PeakPals_BDD_Tests.PageObjects;
using PeakPals_BDD_Tests.Shared;
using NUnit.Framework;
using PeakPals_BDD_Tests.Drivers;

namespace PeakPals_BDD_Tests.StepDefinitions;

[Binding]
public sealed class TBD_180_StepDefinitions
{
    private readonly ProfilePageObject _profilePageObject;
    private readonly CommunityPageObject _communityPageObject;
    private readonly IWebDriver _webDriver;
    public TBD_180_StepDefinitions(BrowserDriver browserDriver)
    {
        _webDriver = browserDriver.Current;
        _profilePageObject = new ProfilePageObject(_webDriver);
    }

    [TearDown]
    public void TearDown()
    {
        _webDriver.Quit();
    }

    [Then("I should see a climb logged on my profile page")]
    public void ThenIShouldSeeAClimbLoggedOnMyProfilePage()
    {
        Assert.That(_profilePageObject.DoesClimbExist(), Is.True);
    }

}


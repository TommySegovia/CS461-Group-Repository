using Reqnroll;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using PeakPals_BDD_Tests.PageObjects;
using PeakPals_BDD_Tests.Shared;
using NUnit.Framework;
using PeakPals_BDD_Tests.Drivers;

namespace PeakPals_BDD_Tests.StepDefinitions;

[Binding]
public sealed class TBD_181_StepDefinitions
{
    private readonly ProfilePageObject _profilePageObject;
    private readonly CommunityPageObject _communityPageObject;
    private readonly IWebDriver _webDriver;
    public TBD_181_StepDefinitions(BrowserDriver browserDriver)
    {
        _webDriver = browserDriver.Current;
        _profilePageObject = new ProfilePageObject(_webDriver);
        _communityPageObject = new CommunityPageObject(_webDriver);
    }

    [TearDown]
    public void TearDown()
    {
        _webDriver.Quit();
    }

    [When("I have joined {string} community group")]
    public void WhenIHaveJoinedCommunityGroup(string communityGroupName)
    {
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Community"));
        _communityPageObject.SearchForCommunityGroup(communityGroupName);
        _communityPageObject.ClickCommunityGroup(communityGroupName);
        _communityPageObject.JoinCommunityGroup();
    }

    [Then("I should see {string} community group displayed on my profile page as {string}")]
    public void ThenIShouldSeeCommunityGroupDisplayedOnMyProfilePageAs(string p0, string john)
    {
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Profile") + "/" + john);
        Assert.That(_profilePageObject.DoesCommunityGroupExist(p0), Is.True);
    }

}


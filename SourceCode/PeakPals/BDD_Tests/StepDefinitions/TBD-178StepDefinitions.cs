using Reqnroll;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using PeakPals_BDD_Tests.PageObjects;
using PeakPals_BDD_Tests.Shared;
using NUnit.Framework;
using PeakPals_BDD_Tests.Drivers;

namespace PeakPals_BDD_Tests.StepDefinitions;

[Binding]
public sealed class TBD_178_StepDefinitions
{
    private readonly IWebDriver _webDriver;
    private readonly ClimbPageObject _climbPageObject;
    public TBD_178_StepDefinitions(BrowserDriver browserDriver)
    {
        _webDriver = browserDriver.Current;
        _climbPageObject = new ClimbPageObject(_webDriver);
    }

    [TearDown]
    public void TearDown()
    {
        _webDriver.Quit();
    }

    [When("I go to the locations page")]
    public void WhenIGoToTheLocationsPage()
    {
        _webDriver.Navigate().GoToUrl("http://localhost:5044/Locations/Search");
    }

    [When("I go to a climb page")]
    public void WhenIGoToAClimbPage()
    {
        _webDriver.Navigate().GoToUrl("http://localhost:5044/Locations/Climbs/c2bc20b8-eaca-54d6-a1ea-503a0031f0b6");
    }
    [When("I log a climb with the following details")]
    public void WhenILogAClimbWithTheFollowingDetails(DataTable dataTable)
    {
        var climbDetails = dataTable.Rows[0];
        _climbPageObject.ClickLogClimbButton();
        _climbPageObject.EnterClimbSuggestedGrade(climbDetails["Grade"]);
        _climbPageObject.EnterClimbAttempts(climbDetails["Attempts"]);
        _climbPageObject.EnterClimbRating(climbDetails["Rating"]);
        _climbPageObject.EnterClimbTag();
        System.Threading.Thread.Sleep(500);
        _climbPageObject.ClickSubmitButton();
        System.Threading.Thread.Sleep(500);
    }

    [Then("I should see the tag {string} on my logged climb")]
    public void ThenIShouldSeeTheTagOnMyLoggedClimb(string crimpy)
    {
        Assert.That(_climbPageObject.DoesTagExist(crimpy), Is.True);
    }



}


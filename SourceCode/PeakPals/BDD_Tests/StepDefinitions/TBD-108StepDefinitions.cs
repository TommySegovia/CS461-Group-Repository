using Reqnroll;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using PeakPals_BDD_Tests.PageObjects;
using PeakPals_BDD_Tests.Shared;
using NUnit.Framework;
using PeakPals_BDD_Tests.Drivers;

namespace PeakPals_BDD_Tests.StepDefinitions;

[Binding]
public sealed class TBD_108_LocationImagesStepDefinitions
{
    private readonly AreaPageObject _areaPage;
    private readonly ClimbPageObject _climbPage;
    private readonly IWebDriver _webDriver;
    public TBD_108_LocationImagesStepDefinitions(BrowserDriver browserDriver)
    {
        _webDriver = browserDriver.Current;
        _areaPage = new AreaPageObject(_webDriver);
        _climbPage = new ClimbPageObject(_webDriver);
        _webDriver.Manage().Window.Maximize();
    }

    // s1

    [Given(@"I am a visitor of the site")]
    public void GivenIAmAVisitorOfTheSite()
    {
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Home"));
    }

    [When(@"I go to the area page I am interested in")]
    public void WhenIGoToTheAreaPageIAmInterestedIn()
    {
        _areaPage.GoToAreaPage("5da0e075-8ec5-5ad3-8136-49a5d4dacaa5");
    }

    [Then(@"I will see images being displayed on the area page if they are any")]
    public void ThenIWillSeeImagesBeingDisplayedOnTheAreaPageIfTheyAreAny()
    {
        Assert.That(_areaPage.DoesAreaImagesExist(), Is.True);
    }

    // s2

    [When(@"I go to the climb page I am interested in")]
    public void WhenIGoToTheClimbPageIAmInterestedIn()
    {
        _climbPage.GoToClimbPage("1a2bfc70-4c94-51f5-8a9c-572916b860b5");
    }

    [Then(@"I will see images being displayed on the climb page if they are any")]
    public void ThenIWillSeeImagesBeingDisplayedOnTheClimbPageIfTheyAreAny()
    {
        Assert.That(_climbPage.DoesClimbImagesExist(), Is.True);
    }

    // s3

    [Given(@"I am on the area or climb page of my choice")]
    public void WhenAndIAmOnTheAreaOrClimbPageOfMyChoice()
    {
        _areaPage.GoToAreaPage("5da0e075-8ec5-5ad3-8136-49a5d4dacaa5");
    }

    [Given(@"there are images being displayed")]
    public void GivenThereAreImagesBeingDisplayed()
    {
        Assert.That(_areaPage.DoesAreaImagesExist(), Is.True);
    }

    [When(@"I click on the button to display more images")]
    public void WhenIClickOnTheButtonToDisplayMoreImages()
    {
        _areaPage.ClickMoreImagesButton();
    }

    [Then(@"I will see a new window display pop-up to show the rest of the images")]
    public void ThenIWillSeeANewWindowDisplayPop_UpToShowTheRestOfTheImages()
    {
        Assert.That(_areaPage.DoesImageModalExist(), Is.True);
        Assert.That(_areaPage.DoesImageModalContainImages(), Is.True);
    }
}


using Reqnroll;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using PeakPals_BDD_Tests.PageObjects;
using PeakPals_BDD_Tests.Shared;
using NUnit.Framework;
using PeakPals_BDD_Tests.Drivers;

namespace PeakPals_BDD_Tests.StepDefinitions;

[Binding]
public sealed class ClimbAreaInfo
{
    private readonly AreaPageObject _areaPage;
    private readonly ClimbPageObject _climbPage;
    private readonly IWebDriver _webDriver;

    public ClimbAreaInfo(BrowserDriver browserDriver)
    {
        _webDriver = browserDriver.Current;
        _areaPage = new AreaPageObject(_webDriver);
        _climbPage = new ClimbPageObject(_webDriver);
        _webDriver.Manage().Window.Maximize();
    }

    [TearDown]
    public void TearDown()
    {
        _webDriver.Quit();
    }

    // s1

    [Given(@"I am a visitor to this site")]
    public void GivenThatIAmAVisitorToThisSite()
    {
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Home"));
    }

    [When(@"I navigate to any area page")]
    public void WhenINavigateToAnyAreaPage()
    {
        _areaPage.GoToAreaPage("5da0e075-8ec5-5ad3-8136-49a5d4dacaa5");
    }

    [Then (@"I will see detailed information on the area if available")]
    public void ThenIWillSeeDetailedInformationOnTheAreaIfAvailable()
    {
        var element = _areaPage.GetOrganizationElement();
        _areaPage.ScrollToElement(element);
    }

    [Then(@"that will include: author metadata, total climbs, and organizations")]
    public void ThenThatWillIncludeAuthorMetadataTotalClimbsAndOrganizations()
    {
        Assert.That(_areaPage.DoesAreaCreationClimbsOrganizationExist(), Is.True);
    }

    // s2

    [When(@"I navigate to any climb page")]
    public void WhenINavigateToAnyClimbPage()
    {
        _climbPage.GoToClimbPage("a2406428-bf95-5b6e-a083-440ea230755b");
    }

    [Then(@"I will see detailed information on the climb if available")]
    public void ThenIWillSeeDetailedInformationOnTheClimbIfAvailable()
    {
        var element = _climbPage.GetFirstAscentElement();
        _climbPage.ScrollToElement(element);
    }

    [Then(@"that will include: location info, protection info, first ascent, and grade scale")]
    public void ThenThatWillIncludeLocationInfoProtectionInfoFirstAscentAndGradeScale()
    {
        
        Assert.That(_climbPage.DoesAreaDescriptionNameCoordinatesExist(), Is.True);
    }
}


using Reqnroll;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using PeakPals_BDD_Tests.PageObjects;
using PeakPals_BDD_Tests.Shared;
using NUnit.Framework;

namespace PeakPals_BDD_Tests.StepDefinitions;

[Binding]
public sealed class ClimbingAreasSteps
{
    private readonly SearchPageObject _searchPage;
    private readonly AreaPageObject _areaPage;
    private readonly IWebDriver _webDriver;
    public ClimbingAreasSteps()
    {
        _webDriver = new FirefoxDriver();
        _searchPage = new SearchPageObject(_webDriver);
        _areaPage = new AreaPageObject(_webDriver);
        _webDriver.Manage().Window.Maximize();
    }

    [TearDown]
    public void TearDown()
    {
        _webDriver.Quit();
    }
    
    // s1

    [Given(@"I am a visitor on the site")]
    public void GivenThatIAmAVisitorOnTheSite()
    {
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Home"));
    }

    [Given(@"I am on the locations search page")]
    public void GivenThatIAmOnTheLocationsSearchPage()
    {
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Search"));
    }

    [When(@"I click on the 'Search Locations' button")]
    public void WhenIClickOnTheSearchLocationsButton()
    {
        _searchPage.OpenSearchModal();
    }

    [Then(@"I expect a display window to appear, allowing me to search for rock climbing areas")]
    public void ThenIExpectADisplayWindowToAppearAllowingMeToSearchForRockClimbingAreas()
    {
        Assert.That(_searchPage.DoesSearchModalExist(), Is.True);
    }

    // s2

    [Given(@"I click on the 'Search Locations' button")]
    public void GivenIClickOnTheSearchLocationsButton()
    {
        _searchPage.OpenSearchModal();
    }

    [Given(@"a window appears allowing me to search for areas and climbs")]
    public void GivenAWindowAppearsAllowingMeToSearchForAreasAndClimbs()
    {
        Assert.That(_searchPage.DoesSearchModalExist(), Is.True);
    }

    [When(@"I enter in 'Arrow Canyon' into the search bar")]
    public void WhenIEnterInPatrickIntoTheSearchBar()
    {
        _searchPage.EnterSearchText("Arrow Canyon");
    }

    [When(@"I click the search icon button")]
    public void WhenIClickTheSearchIconButton()
    {
        _searchPage.ClickSearchButton();
    }

    [Then(@"I should recieve search results")]
    public void ThenIShouldRecieveSearchResults()
    {
        Assert.That(_searchPage.DoesSearchResultsExist(), Is.True);
    }

    [When(@"I click on a search result")]
    public void WhenIClickOnASearchResult()
    {
        _searchPage.ClickSearchResult();
    }

    [Then(@"I will be redirected to an area page")]
    public void ThenIWillBeRedirectedToAnAreaPage()
    {
        Assert.That(_areaPage.IsOnAreaPage("43c69fe1-4462-5108-a7fd-412aeb024913"), Is.True);
    }

    // s3

    [When(@"I enter in the invalid text '&37!' into the search bar")]
    public void WhenIEnterInInvalidTextIntoTheSearchBar()
    {
        _searchPage.EnterSearchText("&37!");
    }

    [Then(@"I should recieve an error that states the input is invalid")]
    public void ThenIShouldRecieveAnErrorThatStatesTheInputIsInvalid()
    {
        Assert.That(_searchPage.DoesErrorMessageExist(), Is.True);
    }

    // s4

    [Given(@"I am on the 'Arrow Canyon' area page that interests me")]
    public void GivenIAmOnTheArrowCanyonAreaPageThatInterestsMe()
    {
        _areaPage.GoToAreaPage("43c69fe1-4462-5108-a7fd-412aeb024913");
    }

    [Then(@"I should see a page displaying basic information about the selected area including name, coordinates, and description")]
    public void ThenIShouldSeeAPageDisplayingBasicInformationAboutTheSelectedAreaIncludingNameCoordinatesAndDescription()
    {
        Assert.That(_areaPage.DoesAreaDescriptionNameCoordinatesExist(), Is.True);
    }

    [When(@"I click on a child box name that is a sub-area of this page's area")]
    public void WhenIClickOnAChildBoxNameThatIsASub_AreaOfThisPageSArea()
    {
        _areaPage.ClickChildLink();
    }

    [Then(@"I will be redirected to that sub-area page")]
    public void ThenIWillBeRedirectedToThatSub_AreaPage()
    {
        Assert.That(_areaPage.IsOnAreaPage("d42fd859-199d-5c1b-a7cc-c7c3a42189af"), Is.True);
    }

}


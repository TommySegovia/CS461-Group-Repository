using Reqnroll;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using PeakPals_BDD_Tests.PageObjects;
using PeakPals_BDD_Tests.Shared;
using NUnit.Framework;
using PeakPals_BDD_Tests.Drivers;

namespace PeakPals_BDD_Tests.StepDefinitions;

[Binding]
public sealed class TBD_183_StepDefinitions
{
    private readonly ReportPageObject _reportPageObject;
    private readonly IWebDriver _webDriver;
    private RecordPageObject _recordPageObject;
    public TBD_183_StepDefinitions(BrowserDriver browserDriver)
    {
        _webDriver = browserDriver.Current;
        _reportPageObject = new ReportPageObject(_webDriver);
        _recordPageObject = new RecordPageObject(_webDriver);
    }

    [TearDown]
    public void TearDown()
    {
        _webDriver.Quit();
    }

    [Given("I have not logged any tests")]
    public void GivenIHaveNotLoggedAnyTests()
    {

    }

    [When("I go to the analysis page")]
    public void WhenIGoToTheAnalysisPage()
    {
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Report"));
    }

    [When("I click on the recommendations tab")]
    public void WhenIClickOnTheRecommendationsTab()
    {
        System.Threading.Thread.Sleep(500);
        _reportPageObject.ClickRecommendationsTab();
    }

    [Then("I should see a message that I need to log more tests to generate recommendations")]
    public void ThenIShouldSeeAMessageThatINeedToLogMoreTestsToGenerateRecommendations()
    {
        Assert.That(_reportPageObject.IsDataMissingMessageDisplayed(), Is.True);
    }

    [Given("I have logged tests")]
    public void GivenIHaveLoggedTests()
    {
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Record"));
        //hang test
        _recordPageObject.OpenHangTestAccordion();
        System.Threading.Thread.Sleep(200);
        _recordPageObject.EnterHangWeight(10);
        _recordPageObject.EnterHangBodyweight(10);
        _recordPageObject.ClickHangRecordButton();

        //pull up test
        _recordPageObject.OpenPullTestAccordion();
        System.Threading.Thread.Sleep(200);
        _recordPageObject.EnterPullupWeight(10);
        _recordPageObject.EnterBodyweight(10);
        _recordPageObject.ClickRecordButton();

        //hammer curl test
        _recordPageObject.OpenHammerCurlAccordion();
        System.Threading.Thread.Sleep(200);
        _recordPageObject.EnterHammerCurlWeight(10);
        _recordPageObject.EnterHammerCurlBodyweight(10);
        _recordPageObject.ClickHammerCurlRecordButton();

        //hip flexibility test
        _recordPageObject.OpenHipFlexibilityAccordion();
        System.Threading.Thread.Sleep(200);
        _recordPageObject.EnterHipFlexibilityDistance(10);
        _recordPageObject.ClickHipFlexibilityRecordButton();

        //hamstring flexibility test
        _recordPageObject.OpenHamstringFlexibilityAccordion();
        System.Threading.Thread.Sleep(200);
        _recordPageObject.EnterHamstringFlexibilityDistance(10);
        _recordPageObject.ClickHamstringFlexibilityRecordButton();

        //repeater test
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Record"));
        _recordPageObject.OpenRepeaterAccordion();
        System.Threading.Thread.Sleep(200);
        _recordPageObject.EnterRepeaterTime(10);
        _recordPageObject.ClickRepeaterRecordButton();

        //smallest edge test
        _recordPageObject.OpenSmallestEdgeAccordion();
        System.Threading.Thread.Sleep(200);
        _recordPageObject.EnterSmallestEdgeSize(10);
        _recordPageObject.ClickSmallestEdgeRecordButton();

        // campus board test
        _recordPageObject.OpenCampusBoardAccordion();
        System.Threading.Thread.Sleep(200);
        _recordPageObject.ClickCampusBoardRecordButton();
    }

    [Then("I should see that I have recommendations")]
    public void ThenIShouldSeeThatIHaveRecommendations()
    {
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Report"));
        System.Threading.Thread.Sleep(80000);
         _reportPageObject.ClickRecommendationsTab();
        Assert.That(_reportPageObject.IsDataMissingMessageDisplayed(), Is.False);
    }

}


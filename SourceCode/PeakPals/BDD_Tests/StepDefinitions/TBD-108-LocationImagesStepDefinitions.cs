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
    }

    [TearDown]
    public void TearDown()
    {
        _webDriver.Quit();
    }

    
}


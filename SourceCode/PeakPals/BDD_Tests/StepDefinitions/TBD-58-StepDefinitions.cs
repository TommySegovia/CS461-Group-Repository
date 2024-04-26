using Reqnroll;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using PeakPals_BDD_Tests.PageObjects;
using PeakPals_BDD_Tests.Shared;
using NUnit.Framework;
using PeakPals_BDD_Tests.Drivers;

namespace PeakPals_BDD_Tests.StepDefinitions;

[Binding]
public sealed class TBD_58_StepDefinitions
{
    private readonly ProfilePageObject _profilePageObject;
    private readonly IWebDriver _webDriver;
    public TBD_58_StepDefinitions(BrowserDriver browserDriver)
    {
        _webDriver = browserDriver.Current;
        _profilePageObject = new ProfilePageObject(_webDriver);
    }

    [TearDown]
    public void TearDown()
    {
        _webDriver.Quit();
    }

    [When("I go to my profile as {string}")]
    public void WhenIGoToMyProfileAs(string name)
    {
        _webDriver.Navigate().GoToUrl(Common.UrlFor("Profile") + "/" + name);
    }
    [When("I click on edit profile")]
    public void WhenIClickOnEditProfile()
    {
        _profilePageObject.ClickEditProfile();
    }
    [Then("the form should contain field for first name")]
    public void ThenTheFormShouldContainFieldForFirstName()
    {
        Assert.That(_profilePageObject.DoesFirstNameFieldExist(), Is.True);
    }
    [When("I update my age to {string}")]
    public void WhenIUpdateMyAgeTo(string p0)
    {
        _profilePageObject.SelectAgeField();
        _profilePageObject.EnterAge(p0);
        _profilePageObject.ClickSubmitButton();
    }
    [When("I update my first name to {string}")]
    public void WhenIUpdateMyFirstNameTo(string john)
    {
        _profilePageObject.SelectFirstNameField();
        _profilePageObject.EnterFirstName(john);
        _profilePageObject.ClickSubmitButton();
    }
    [Then("I should see that my first name on my profile has been updated to {string}")]
    public void ThenIShouldSeeThatMyFirstNameOnMyProfileHasBeenUpdatedTo(string name)
    {
        Assert.That(_profilePageObject.GetFirstName(), Is.EqualTo(name));
    }
    [When("I update my city to {string}")]
    public void WhenIUpdateMyCityTo(string p0)
    {
        _profilePageObject.SelectCityField();
        _profilePageObject.EnterCity(p0);
        _profilePageObject.ClickSubmitButton();

    }
    [Then("I should see that my city on my profile has been updated to {string}")]
    public void ThenIShouldSeeThatMyCityOnMyProfileHasBeenUpdatedTo(string p0)
    {
        Assert.That(_profilePageObject.DoesLocationContainCity(p0), Is.True);
    }
}


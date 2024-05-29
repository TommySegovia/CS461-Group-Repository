using PeakPals_Project.Controllers;
using PeakPals_Project.DAL.Abstract;
using PeakPals_Project.Models.DTO;
using Moq;
using PeakPals_Project.Services;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using PeakPals_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using PeakPals_Project.Areas.Identity.Data;

namespace NUnit_Tests;

[TestFixture]
public class ProfileControllerTests
{
    private ILogger<ProfileController> _iLogger;
    private Mock<IClimberRepository> _climberRepositoryMock;
    private Mock<IClimberService> _climberServiceMock;
    private Mock<UserManager<ApplicationUser>> _userManagerMock;
    private ProfileController _controller;

    [SetUp]
    public void Setup()
    {
        _climberRepositoryMock = new Mock<IClimberRepository>();
        _climberServiceMock = new Mock<IClimberService>();
        _controller = new ProfileController(_iLogger, _climberRepositoryMock.Object, _climberServiceMock.Object, _userManagerMock.Object);
    }

/*/////// Need to research testing with Identity first.
   [Test]
    public void GetProfile_WhenCalledWithValidUsername_ReturnsExpectedProfile()
    {
        // Arrange
        var username = "testUser";
        var expectedProfile = new Climber { UserName = username };
        _climberRepositoryMock.Setup(repo => repo.GetClimberByUsername(username)).Returns(expectedProfile);

        // Act
        var actionResult = _controller.GetProfile(username);

        // Assert
        var okObjectResult = actionResult as OkObjectResult;
        Assert.IsNotNull(okObjectResult);
        var resultValue = okObjectResult.Value as Climber;
        Assert.That(resultValue, Is.EqualTo(expectedProfile));
    }

*/

// Test for non-existant name, if the user is not found, or the repo throws and exception.
}
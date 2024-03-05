using PeakPals_Project.Controllers;
using PeakPals_Project.DAL.Abstract;
using PeakPals_Project.Models.DTO;
using Moq;
using PeakPals_Project.Services;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using PeakPals_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace NUnit_Tests;

[TestFixture]
public class CommunityApiControllerTests
{
    
    private Mock<IClimberRepository> _climberRepositoryMock;
    private Mock<IClimberService> _climberServiceMock;
    private CommunityApiController _controller;

    [SetUp]
    public void Setup()
    {
        _climberRepositoryMock = new Mock<IClimberRepository>();
        _climberServiceMock = new Mock<IClimberService>();
        _controller = new CommunityApiController(_climberServiceMock.Object, _climberRepositoryMock.Object);
    }

    [Test]
    public void GetUserResults_WhenCalled_ReturnsExpectedResult()
    {   
        // Arrange
        var username = "testUser";
        var climbers = new List<ClimberDTO> { new ClimberDTO { UserName = username }};
        _climberRepositoryMock.Setup(repo => repo.GetClimbersByUsername(username)).Returns(climbers);

        // Act
        var actionResult = _controller.GetUserResults(username);

        // Assert 
        var okObjectResult = actionResult.Result as OkObjectResult;
        Assert.IsNotNull(okObjectResult);
        var resultValue = okObjectResult.Value as List<ClimberDTO>;
        Assert.That(resultValue, Is.EqualTo(climbers));

    }

    [Test]
    public void GetUserResults_WhenCalled_ReturnsBadRequest()
    {
        // Arrange
        var username = "";

        // Act
        var actionResult = _controller.GetUserResults(username);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(actionResult.Result);
        
    }

    [Test]
    public void GetUserResults_WhenCalledWithNonexistentUsername_ReturnsNotFound()
    {
        // Arrange
        var username = "nonexistentUser";
        _climberRepositoryMock.Setup(repo => repo.GetClimbersByUsername(username)).Returns((List<ClimberDTO>)null);

        // Act
        var actionResult = _controller.GetUserResults(username);

        // Assert
        Assert.IsInstanceOf<NotFoundObjectResult>(actionResult.Result);
    }

}
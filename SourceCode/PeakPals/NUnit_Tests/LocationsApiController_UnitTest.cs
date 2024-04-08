using PeakPals_Project.Controllers;
using PeakPals_Project.DAL.Abstract;
using PeakPals_Project.Models.DTO;
using Moq;
using PeakPals_Project.Services;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using PeakPals_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http.HttpResults;
using GraphQL.Client.Serializer.Newtonsoft;
using Moq.Protected;
using System.Text;
using System.Net;
using GraphQL.Client.Http;

namespace NUnit_Tests;

[TestFixture]
public class LocationsApiControllerTests
{
    private Mock<IOpenBetaApiService> _obService;
    private LocationsApiController _apiController;
    private ILogger<LocationsApiController> _logger;

    [SetUp]
    public void SetUp()
    {
        _logger = new Mock<ILogger<LocationsApiController>>().Object;
        _obService = new Mock<IOpenBetaApiService>();
        _apiController = new LocationsApiController(_obService.Object, _logger);
    }

    [Test]
    public void FindAllMatchingAreas_WhenCalledAndWhenGivenNullString_ReturnsWithBadRequest()
    {
        // Arrange
        string query = null;

        // Act
        var response = _apiController.FindAllMatchingAreas(query);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(response.Result.Result);

    }

    [Test]
    public void FindAllMatchingAreas_WhenCalledAndResponseIsNull_ReturnsWithNotFoundError()
    {
        // Arrange
        string query = "192123123391023";


        // Act
        var response = _apiController.FindAllMatchingAreas(query);


        // Assert
        Assert.IsInstanceOf<NotFoundObjectResult>(response.Result.Result);
    }

    [Test]
    public void FindAllMatchingAreas_WhenCalledAndCorrect_ReturnsWithOkObject()
    {

         // Arrange
        var mockService = new Mock<IOpenBetaApiService>();
        var openBetaQueryResult = new OpenBetaQueryResult {};
        mockService.Setup(s => s.FindMatchingAreas(It.IsAny<string>(), 8)).ReturnsAsync(openBetaQueryResult);
        var apiController = new LocationsApiController(mockService.Object, _logger);

        string query = "hello";

        // Act
        var response = apiController.FindAllMatchingAreas(query);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(response.Result.Result);
    }

    [Test]
    public void FindAreaById_WhenCalledAndWhenGivenNullString_ReturnsWithBadRequest()
    {
        // Arrange
        string query = null;

        // Act
        var response = _apiController.FindAreaById(query);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(response.Result.Result);

    }

    [Test]
    public void FindAreaById_WhenCalledAndResponseIsNull_ReturnsWithNotFoundError()
    {
        // Arrange
        string query = "192123123391023";


        // Act
        var response = _apiController.FindAreaById(query);


        // Assert
        Assert.IsInstanceOf<NotFoundObjectResult>(response.Result.Result);
    }

    [Test]
    public void FindAreaById_WhenCalledAndCorrect_ReturnsWithOkObject()
    {
         // Arrange
        var mockService = new Mock<IOpenBetaApiService>();
        var OBArea = new OBArea {};
        mockService.Setup(s => s.FindAreaById(It.IsAny<string>())).ReturnsAsync(OBArea);
        var apiController = new LocationsApiController(mockService.Object, _logger);

        string query = "hello";

        // Act
        var response = apiController.FindAreaById(query);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(response.Result.Result);
    }

    [Test]
    public void FindAreaAncestorsById_WhenCalledAndWhenGivenNullString_ReturnsWithBadRequest()
    {
        // Arrange
        string query = null;

        // Act
        var response = _apiController.FindAncestorNameById(query);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(response.Result.Result);

    }

    [Test]
    public void FindAreaAncestorsById_WhenCalledAndResponseIsNull_ReturnsWithNotFoundError()
    {
        // Arrange
        string query = "192123123391023";


        // Act
        var response = _apiController.FindAncestorNameById(query);


        // Assert
        Assert.IsInstanceOf<NotFoundObjectResult>(response.Result.Result);
    }

    [Test]
    public void FindAreaAncestorsById_WhenCalledAndCorrect_ReturnsWithOkObject()
    {
         // Arrange
        var mockService = new Mock<IOpenBetaApiService>();
        var OBArea = new OBArea {};
        mockService.Setup(s => s.FindAncestorNameByAreaId(It.IsAny<string>())).ReturnsAsync(OBArea);
        var apiController = new LocationsApiController(mockService.Object, _logger);

        string query = "hello";

        // Act
        var response = apiController.FindAncestorNameById(query);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(response.Result.Result);
    }

    [Test]
    public void FindAllMatchingClimbs_WhenCalledAndWhenGivenNullString_ReturnsWithBadRequest()
    {
        // Arrange
        string query = null;

        // Act
        var response = _apiController.FindAllMatchingClimbs(query);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(response.Result.Result);

    }

    [Test]
    public void FindAllMatchingClimbs_WhenCalledAndResponseIsNull_ReturnsWithNotFoundError()
    {
        // Arrange
        string query = "192123123391023";


        // Act
        var response = _apiController.FindAllMatchingClimbs(query);


        // Assert
        Assert.IsInstanceOf<NotFoundObjectResult>(response.Result.Result);
    }

    [Test]
    public void FindAllMatchingClimbs_WhenCalledAndCorrect_ReturnsWithOkObject()
    {
         // Arrange
        var mockService = new Mock<IOpenBetaApiService>();
        var openBetaQueryResult = new OpenBetaQueryResult {};
        mockService.Setup(s => s.FindMatchingAreas(It.IsAny<string>(), 200)).ReturnsAsync(openBetaQueryResult);
        var apiController = new LocationsApiController(mockService.Object, _logger);

        string query = "hello";

        // Act
        var response = apiController.FindAllMatchingClimbs(query);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(response.Result.Result);
    }

    [Test]
    public void FindClimbById_WhenCalledAndWhenGivenNullString_ReturnsWithBadRequest()
    {
        // Arrange
        string query = null;

        // Act
        var response = _apiController.FindClimbById(query);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(response.Result.Result);

    }

    [Test]
    public void FindClimbById_WhenCalledAndResponseIsNull_ReturnsWithNotFoundError()
    {
        // Arrange
        string query = "192123123391023";


        // Act
        var response = _apiController.FindClimbById(query);


        // Assert
        Assert.IsInstanceOf<NotFoundObjectResult>(response.Result.Result);
    }

    [Test]
    public void FindClimbById_WhenCalledAndCorrect_ReturnsWithOkObject()
    {
         // Arrange
        var mockService = new Mock<IOpenBetaApiService>();
        var OBClimb = new OBClimb {};
        mockService.Setup(s => s.FindClimbById(It.IsAny<string>())).ReturnsAsync(OBClimb);
        var apiController = new LocationsApiController(mockService.Object, _logger);

        string query = "hello";

        // Act
        var response = apiController.FindClimbById(query);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(response.Result.Result);
    }

}
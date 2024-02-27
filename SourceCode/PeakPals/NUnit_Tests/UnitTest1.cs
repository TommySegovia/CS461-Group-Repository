using PeakPals_Project.Services;
using PeakPals_Project.DAL.Abstract;
using PeakPals_Project.Models.DTO;
using PeakPals_Project.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;


namespace PeakPals_Project.Tests
{
    public class FitnessDataEntryApiControllerTests
    {
        private FitnessDataEntryApiController _controller;
        private Mock<IFitnessDataEntryRepository> _fitnessDataEntryRepositoryMock;
        private Mock<IClimberRepository> _climberRepositoryMock;
        private Mock<IFitnessDataEntryService> _fitnessDataEntryServiceMock;
        private Mock<IClimberService> _climberServiceMock;

        [SetUp]
        public void Setup()
        {
            // Mocking dependencies
            _fitnessDataEntryRepositoryMock = new Mock<IFitnessDataEntryRepository>();
            _climberRepositoryMock = new Mock<IClimberRepository>();
            _fitnessDataEntryServiceMock = new Mock<IFitnessDataEntryService>();
            _climberServiceMock = new Mock<IClimberService>();

            // Provide the mocked services to the controller
            _controller = new FitnessDataEntryApiController(
                fitnessDataEntryService: _fitnessDataEntryServiceMock.Object,
                climberService: _climberServiceMock.Object,
                fitnessDataEntryRepository: _fitnessDataEntryRepositoryMock.Object,
                climberRepository: _climberRepositoryMock.Object
            );
        }

        [Test]
        public void GetUserResultsWithTimesInChronologicalOrder_AuthenticatedUser_ReturnsOk()
        {
            // Arrange
            var climberId = 1; // example climber id
            var testId = 1; // example test id
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.NameIdentifier, "userId"),
            }, "mock"));

            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            _climberRepositoryMock.Setup(repo => repo.GetClimberByAspNetIdentityId(It.IsAny<string>())).Returns(new ClimberDTO { Id = climberId });
            _fitnessDataEntryRepositoryMock.Setup(repo => repo.GetUserResultsWithTimesInChronologicalOrder(It.IsAny<int>(), It.IsAny<int>())).Returns(new List<FitnessDataEntryDTO>());

            // Act
            var result = _controller.GetUserResultsWithTimesInChronologicalOrder(testId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public void RecordTestResult_AuthenticatedUser_ReturnsOk()
        {
            // Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.NameIdentifier, "userId"),
            }, "mock"));

            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            _climberRepositoryMock.Setup(repo => repo.GetClimberByAspNetIdentityId(It.IsAny<string>())).Returns(new ClimberDTO());
            _fitnessDataEntryRepositoryMock.Setup(repo => repo.GetUserResultsWithTimesInChronologicalOrder(It.IsAny<int>(), It.IsAny<int>())).Returns(new List<FitnessDataEntryDTO>());

            // Act
            var result = _controller.RecordTestResult(new FitnessDataEntryDTO());

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}

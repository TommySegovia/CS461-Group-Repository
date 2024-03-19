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
using PeakPals_Project.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;



namespace PeakPals_Project.Tests
{
    public class FitnessDataEntryApiControllerTests
    {
        private FitnessDataEntryApiController _controller;
        private Mock<IFitnessDataEntryRepository> _fitnessDataEntryRepositoryMock;
        private Mock<IClimberRepository> _climberRepositoryMock;
        private Mock<IFitnessDataEntryService> _fitnessDataEntryServiceMock;
        private Mock<IClimberService> _climberServiceMock;
        private Mock<UserManager<ApplicationUser>> _userManagerMock;

        [SetUp]
        public void Setup()
        {
            // Mocking dependencies
            _fitnessDataEntryRepositoryMock = new Mock<IFitnessDataEntryRepository>();
            _climberRepositoryMock = new Mock<IClimberRepository>();
            _fitnessDataEntryServiceMock = new Mock<IFitnessDataEntryService>();
            _climberServiceMock = new Mock<IClimberService>();

            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            // Provide the mocked services to the controller
            _controller = new FitnessDataEntryApiController(
                fitnessDataEntryService: _fitnessDataEntryServiceMock.Object,
                climberService: _climberServiceMock.Object,
                fitnessDataEntryRepository: _fitnessDataEntryRepositoryMock.Object,
                climberRepository: _climberRepositoryMock.Object,
                userManager: _userManagerMock.Object
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
        public void GetMostRecentUserTestValueAndBodyWeight_AuthenticatedUser_ReturnsOk()
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
            _fitnessDataEntryRepositoryMock.Setup(repo => repo.GetUserResultsWithTimesInChronologicalOrder(It.IsAny<int>(), It.IsAny<int>())).Returns(new List<FitnessDataEntryDTO> { new FitnessDataEntryDTO() });

            // Act
            var result = _controller.GetMostRecentUserTestValueAndBodyWeight(testId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }
    }
}

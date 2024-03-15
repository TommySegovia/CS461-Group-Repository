using NUnit.Framework;
using Moq;
using PeakPals_Project.Services;
using PeakPals_Project.DAL.Abstract;
using PeakPals_Project.Models;
using PeakPals_Project.Data;

namespace NUnit_Tests
{
    [TestFixture]
    public class ClimberServiceTests
    {
        private Mock<IClimberRepository> _mockClimberRepository;
        private Mock<PeakPalsContext> _mockContext;
        private ClimberService _climberService;

        [SetUp]
        public void Setup()
        {
            _mockClimberRepository = new Mock<IClimberRepository>();
            _mockContext = new Mock<PeakPalsContext>();
            _climberService = new ClimberService(_mockClimberRepository.Object, _mockContext.Object);
        }

        [Test]
        public void AddNewClimber_CallsAddOrUpdateAndSaveChanges()
        {
            _climberService.AddNewClimber("AspNetIdentityId", "FirstName", "LastName", "UserName");

            _mockClimberRepository.Verify(r => r.AddOrUpdate(It.IsAny<Climber>()), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Test]
        public void UpdateClimber_CallsAddOrUpdateAndSaveChanges()
        {
            var climber = new Climber { UserName = "TestUser" };

            _climberService.UpdateClimber(climber);

            _mockClimberRepository.Verify(r => r.AddOrUpdate(It.IsAny<Climber>()), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }
    }
}
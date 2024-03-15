using NUnit.Framework;
using Moq;
using Microsoft.EntityFrameworkCore;
using PeakPals_Project.DAL.Concrete;
using PeakPals_Project.Models;
using PeakPals_Project.Data;
using System.Collections.Generic;
using System.Linq;

namespace NUnit_Tests
{
    [TestFixture]
    public class ClimberRepositoryTests
    {
        private Mock<DbSet<Climber>> _mockSet;
        private Mock<PeakPalsContext> _mockContext;
        private ClimberRepository _climberRepository;

        [SetUp]
        public void Setup()
        {
            // Create a list of climbers
            var climbers = new List<Climber>
            {
                new Climber { UserName = "TestUser1" },
                new Climber { UserName = "TestUser2" }
            }.AsQueryable();

            // Create a mock set
            _mockSet = new Mock<DbSet<Climber>>();
            _mockSet.As<IQueryable<Climber>>().Setup(m => m.Provider).Returns(climbers.Provider);
            _mockSet.As<IQueryable<Climber>>().Setup(m => m.Expression).Returns(climbers.Expression);
            _mockSet.As<IQueryable<Climber>>().Setup(m => m.ElementType).Returns(climbers.ElementType);
            _mockSet.As<IQueryable<Climber>>().Setup(m => m.GetEnumerator()).Returns(climbers.GetEnumerator());

            // Create a mock context
            _mockContext = new Mock<PeakPalsContext>();
            _mockContext.Setup(c => c.Climber).Returns(_mockSet.Object);

            // Create the repository
            _climberRepository = new ClimberRepository(_mockContext.Object);
        }

        [Test]
        public void GetClimberByUsername_ReturnsCorrectClimber()
        {
            var climber = _climberRepository.GetClimberByUsername("TestUser1");
            Assert.IsNotNull(climber);
            Assert.AreEqual("TestUser1", climber.UserName);
        }

        [Test]
        public void GetClimbersByUsername_ReturnsCorrectClimbers()
        {
            var climbers = _climberRepository.GetClimbersByUsername("TestUser");
            Assert.IsNotNull(climbers);
            Assert.AreEqual(2, climbers.Count);
        }

        [Test]
        public void GetClimbersByUsername_ReturnsEmptyListWhenNoClimbersMatch()
        {
            var climbers = _climberRepository.GetClimbersByUsername("NonexistentUser");

            Assert.IsNotNull(climbers);
            Assert.AreEqual(0, climbers.Count);
        }
    }
}
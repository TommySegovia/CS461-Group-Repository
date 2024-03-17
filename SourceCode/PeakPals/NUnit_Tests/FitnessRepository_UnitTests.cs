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
    public class FitnessDataEntryRepositoryTests
    {
        private Mock<DbSet<FitnessDataEntry>> _dbSetMock;
        private Mock<PeakPalsContext> _dbContextMock;
        private FitnessDataEntryRepository _fitnessDataEntryRepository;

        [SetUp]
        public void Setup()
        {

            // create a list of fitness data entries
            var fitnessDataEntries = new List<FitnessDataEntry>
            {
                new FitnessDataEntry { ClimberId = 1, TestId = 1, Result = 1, BodyWeight = 1, EntryDate = new System.DateTime(2021, 1, 1) },
                new FitnessDataEntry { ClimberId = 1, TestId = 1, Result = 2, BodyWeight = 1, EntryDate = new System.DateTime(2021, 1, 2) },
                new FitnessDataEntry { ClimberId = 1, TestId = 1, Result = 3, BodyWeight = 1, EntryDate = new System.DateTime(2021, 1, 3) },
                new FitnessDataEntry { ClimberId = 2, TestId = 1, Result = 1, BodyWeight = 1, EntryDate = new System.DateTime(2021, 1, 1) },
                new FitnessDataEntry { ClimberId = 2, TestId = 1, Result = 2, BodyWeight = 1, EntryDate = new System.DateTime(2021, 1, 2) },
                new FitnessDataEntry { ClimberId = 2, TestId = 1, Result = 3, BodyWeight = 1, EntryDate = new System.DateTime(2021, 1, 3) },
                new FitnessDataEntry { ClimberId = 3, TestId = 7, Result = 1, BodyWeight = 1, EntryDate = new System.DateTime(2021, 1, 1) },
                new FitnessDataEntry { ClimberId = 3, TestId = 7, Result = 2, BodyWeight = 1, EntryDate = new System.DateTime(2021, 1, 2) },
                new FitnessDataEntry { ClimberId = 3, TestId = 7, Result = 3, BodyWeight = 1, EntryDate = new System.DateTime(2021, 1, 3) },
                new FitnessDataEntry { ClimberId = 3, TestId = 7, Result = 3, BodyWeight = 1, EntryDate = new System.DateTime(2021, 1, 3) }
            }.AsQueryable();

            // Create a mock set
            _dbSetMock = new Mock<DbSet<FitnessDataEntry>>();
            _dbSetMock.As<IQueryable<FitnessDataEntry>>().Setup(m => m.Provider).Returns(fitnessDataEntries.Provider);
            _dbSetMock.As<IQueryable<FitnessDataEntry>>().Setup(m => m.Expression).Returns(fitnessDataEntries.Expression);
            _dbSetMock.As<IQueryable<FitnessDataEntry>>().Setup(m => m.ElementType).Returns(fitnessDataEntries.ElementType);
            _dbSetMock.As<IQueryable<FitnessDataEntry>>().Setup(m => m.GetEnumerator()).Returns(fitnessDataEntries.GetEnumerator());


            // Create a mock context
            Mock<PeakPalsContext> _dbContextMock = new Mock<PeakPalsContext>();
            _dbContextMock.Setup(c => c.FitnessDataEntry).Returns(_dbSetMock.Object);


            // Create the repository
            _fitnessDataEntryRepository = new FitnessDataEntryRepository(_dbContextMock.Object);

        }

        [Test]
        public void GetAverageResultDividedByBodyweight_ReturnsCorrectResultDividedByBodyweight()
        {
            var result = _fitnessDataEntryRepository.GetAverageResultDividedByBodyweight(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result);
        }
        
        [Test]
        public void GetUserAverageResultDividedByBodyweight_ReturnsCorrectResultDividedByBodyweight()
        {
            var result = _fitnessDataEntryRepository.GetUserAverageResultDividedByBodyweight(1, 1);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result);
        }

        [Test]
        public void GetUserResultsWithTimesInChronologicalOrder_ReturnsCorrectResultsInChronologicalOrder()
        {
            var results = _fitnessDataEntryRepository.GetUserResultsWithTimesInChronologicalOrder(1, 1);
            Assert.IsNotNull(results);
            Assert.AreEqual(3, results.Count);
            Assert.AreEqual(new System.DateTime(2021, 1, 1), results[0].EntryDate);
            Assert.AreEqual(new System.DateTime(2021, 1, 2), results[1].EntryDate);
            Assert.AreEqual(new System.DateTime(2021, 1, 3), results[2].EntryDate);
        }

        [Test]
        public void GetAverageResultFlexibility_ReturnsCorrectAverageResultFlexibility()
        {
            var result = _fitnessDataEntryRepository.GetAverageResultFlexibility(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result);
        }

        [Test]
        public void GetAverageResultRepeater_ReturnsCorrectAverageResultRepeater()
        {
            var result = _fitnessDataEntryRepository.GetAverageResultRepeater(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result);
        }

        [Test]
        public void GetAverageResultSmallestEdge_ReturnsCorrectAverageResultSmallestEdge()
        {
            var result = _fitnessDataEntryRepository.GetAverageResultSmallestEdge(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result);
        }

        [Test]
        public void GetMostCommonResultCampusBoard_ReturnsCorrectMostCommonResultCampusBoard()
        {
            var result = _fitnessDataEntryRepository.GetMostCommonResultCampusBoard(7);
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result);
        }        
    }
}
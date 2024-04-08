using NUnit.Framework;
using Moq;
using PeakPals_Project.DAL.Abstract;
using PeakPals_Project.Models;
using PeakPals_Project.Models.DTO;
using PeakPals_Project.Services;
using System;
using PeakPals_Project.Data;
using System.Collections.Generic;
using System.Linq;

namespace NUnit_Tests
{
    public class FitnessDataEntryServiceTests
    {
        private Mock<IFitnessDataEntryRepository> _mockRepo;
        private Mock<PeakPalsContext> _mockContext;
        private FitnessDataEntryService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IFitnessDataEntryRepository>();
            _mockContext = new Mock<PeakPalsContext>();
            _service = new FitnessDataEntryService(_mockContext.Object, _mockRepo.Object);
        }

        [Test]
        public void RecordTestResult_AddsNewEntry()
        {
            // Arrange
            var climberId = 1;
            var testId = 1;
            var result = 100;
            var bodyWeight = 70;
            var age = 25;
            var gender = "Male";
            var climbingExperience = "Intermediate";
            var climbingGrade = 10;

            // Act
            _service.RecordTestResult(climberId, testId, result, bodyWeight, age, gender, climbingExperience, climbingGrade);

            // Assert
            _mockRepo.Verify(r => r.AddOrUpdate(It.IsAny<FitnessDataEntry>()), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }
        
        [Test]
        public void DeleteTestResult_DeletesEntry()
        {
            // Arrange
            var id = 1;
            var testId = 1;
            var climberId = 1;
            var fitnessDataEntry = new FitnessDataEntry
            {
                Id = id,
                ClimberId = climberId,
                TestId = testId
            };
            _mockRepo.Setup(r => r.FindById(id)).Returns(fitnessDataEntry);

            // Act
            _service.DeleteTestResult(id, testId, climberId);

            // Assert
            _mockRepo.Verify(r => r.Delete(fitnessDataEntry), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }
    }
}
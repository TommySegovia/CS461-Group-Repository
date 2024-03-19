using PeakPals_Project.DAL.Abstract;
using PeakPals_Project.Models;
using PeakPals_Project.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using PeakPals_Project.Data;
using PeakPals_Project.ExtensionMethods;

namespace PeakPals_Project.DAL.Concrete
{
    public class FitnessDataEntryRepository : Repository<FitnessDataEntry>, IFitnessDataEntryRepository
    {
        private DbSet<FitnessDataEntry> _fitnessDataEntry;
        public FitnessDataEntryRepository(PeakPalsContext context) : base(context)
        {
            _fitnessDataEntry = context.FitnessDataEntry;
        }

        public List<FitnessDataEntryDTO> GetUserResultsWithTimesInChronologicalOrder(int climberId, int testId)
        {
            //returns all the results for a climber for a specific test
            return _fitnessDataEntry
                .Where(f => f.ClimberId == climberId && f.TestId == testId)
                .OrderBy(f => f.EntryDate)
                .Select(f => f.ToDTO())
                .ToList();
        }
        public double? GetAverageResultDividedByBodyweight(int testId, int minAge, int maxAge, string gender, string climbingExperience, int minimumClimbingGrade, int maximumClimbingGrade)
        {
            //returns the average result for a climber for a specific test compared to all other climbers where the result is divided by the bodyweight
            // also filters by parameters
            var averageResult = _fitnessDataEntry
                .Where(f => f.TestId == testId && f.Age >= minAge && f.Age <= maxAge && (f.Gender == gender || gender == "All") && (f.ClimbingExperience == climbingExperience || climbingExperience == "All") && f.ClimbingGrade >= minimumClimbingGrade && f.ClimbingGrade <= maximumClimbingGrade)
                .Average(f => f.Result);
            var averageBodyWeight = _fitnessDataEntry
                .Where(f => f.TestId == testId && f.Age >= minAge && f.Age <= maxAge && (f.Gender == gender || gender == "All") && (f.ClimbingExperience == climbingExperience || climbingExperience == "All") && f.ClimbingGrade >= minimumClimbingGrade && f.ClimbingGrade <= maximumClimbingGrade)
                .Average(f => f.BodyWeight);
            if (averageResult == null || averageBodyWeight == null)
            {
                return null;
            }
            return averageResult / averageBodyWeight;
        }

        public double? GetUserAverageResultDividedByBodyweight(int climberId, int testId)
        {
            //returns the result for a climber's average results for a specific test compared to all of their results where the result is divided by the bodyweight
            var averageResult = _fitnessDataEntry
                .Where(f => f.ClimberId == climberId && f.TestId == testId)
                .Average(f => f.Result);
            var averageBodyWeight = _fitnessDataEntry
                .Where(f => f.ClimberId == climberId && f.TestId == testId)
                .Average(f => f.BodyWeight);
            if (averageResult == null || averageBodyWeight == null)
            {
                return null;
            }
            return averageResult / averageBodyWeight;
        }

        public double? GetAverageResult(int testId, int minAge, int maxAge, string gender, string climbingExperience, int minimumClimbingGrade, int maximumClimbingGrade)
        {
            //returns the average result for a climber for a specific test compared to all other climbers who are within the parameters
            var averageResult = _fitnessDataEntry
                .Where(f => f.TestId == testId && f.Age >= minAge && f.Age <= maxAge && (f.Gender == gender || gender == "All") && (f.ClimbingExperience == climbingExperience || climbingExperience == "All") && f.ClimbingGrade >= minimumClimbingGrade && f.ClimbingGrade <= maximumClimbingGrade)
                .Average(f => f.Result);
            if (averageResult == null)
            {
                return null;
            }
            return averageResult;
        }

        public double? GetMostCommonResultCampusBoard(int testId, int minAge, int maxAge, string gender, string climbingExperience, int minimumClimbingGrade, int maximumClimbingGrade)
        {
            //returns the most frequent result for this test, the result that occurs the most, not the average
            var averageResult = _fitnessDataEntry
                .Where(f => f.TestId == testId && f.Age >= minAge && f.Age <= maxAge && (f.Gender == gender || gender == "All") && (f.ClimbingExperience == climbingExperience || climbingExperience == "All") && f.ClimbingGrade >= minimumClimbingGrade && f.ClimbingGrade <= maximumClimbingGrade)
                .GroupBy(f => f.Result)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();
            if (averageResult == null)
            {
                return null;
            }
            return averageResult;
            
        }
    }
}
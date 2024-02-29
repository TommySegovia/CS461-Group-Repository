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
        public FitnessDataEntryRepository(ApplicationDbContext context) : base(context)
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
        public double? GetAverageResultDividedByBodyweight(int testId)
        {
            //returns the average result for a climber for a specific test compared to all other climbers where the result is divided by the bodyweight
            var averageResult = _fitnessDataEntry
                .Where(f => f.TestId == testId)
                .Average(f => f.Result);
            var averageBodyWeight = _fitnessDataEntry
                .Where(f => f.TestId == testId)
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
    }
}
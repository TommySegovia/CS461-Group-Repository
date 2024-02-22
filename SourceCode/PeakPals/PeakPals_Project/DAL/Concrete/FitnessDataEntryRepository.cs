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
    }
}
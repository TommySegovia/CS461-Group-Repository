using PeakPals_Project.DAL.Abstract;
using PeakPals_Project.Models;
using PeakPals_Project.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using PeakPals_Project.Data;
using PeakPals_Project.ExtensionMethods;

namespace PeakPals_Project.DAL.Concrete
{

    public class ClimbTagEntryRepository : Repository<ClimbTagEntry>, IClimbTagEntryRepository
    {
        private readonly DbSet<ClimbTagEntry> _climbTagEntry;
        private readonly PeakPalsContext _context;

        public ClimbTagEntryRepository(PeakPalsContext context) : base(context)
        {
            _climbTagEntry = context.ClimbTagEntry;
            _context = context;
        }

        public new List<ClimbTagEntryDTO> GetAll()
        {
            return _climbTagEntry
                .Select(f => f.ToDTO())
                .ToList();
        }

        public void AddClimbTagEntry(ClimbTagEntryDTO climbTagEntryDTO)
        {
            var newClimbTagEntry = climbTagEntryDTO.ToModel();
            _context.Add(newClimbTagEntry);
            _context.SaveChanges();
        }

        public List<string> GetClimbTagEntryByClimbAttemptID(int climbAttemptID)
        {
            //return the tag strings for a specific climb attempt
            return _climbTagEntry
                .Where(f => f.ClimbAttemptID == climbAttemptID)
                .Select(f => f.Tag.TagName)
                .Distinct() // Add this line to remove duplicates
                .ToList();
        }

        public List<int> GetClimbTagEntryIdByTag(string tag)
        {
            //return the climbTagEntryIDs for a specific tag
            return _climbTagEntry
                .Where(f => f.Tag.TagName == tag)
                .Select(f => f.ClimbAttemptID)
                .ToList();
        }
    }
}
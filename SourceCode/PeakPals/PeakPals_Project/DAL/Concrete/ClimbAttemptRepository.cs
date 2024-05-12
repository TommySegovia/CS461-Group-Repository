using PeakPals_Project.DAL.Abstract;
using PeakPals_Project.Models;
using PeakPals_Project.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using PeakPals_Project.Data;
using PeakPals_Project.ExtensionMethods;

namespace PeakPals_Project.DAL.Concrete
{

  public class ClimbAttemptRepository : Repository<ClimbAttempt>, IClimbAttemptRepository
  {
    private readonly DbSet<ClimbAttempt> _climbAttempt;
    private readonly PeakPalsContext _context;

    public ClimbAttemptRepository(PeakPalsContext context) : base(context)
    {
      _climbAttempt = context.ClimbAttempt;
      _context = context;
    }

    public List<ClimbAttemptDTO> ViewAllClimbingAttempts(int climberId)
    {
      var climbAttempts = _climbAttempt.Where(f => f.ClimberId == climberId).ToList();
      if (climbAttempts != null)
      {
        var climbAttemptDTOs = new List<ClimbAttemptDTO>();
        foreach (var climbAttempt in climbAttempts)
        {
          climbAttemptDTOs.Add(climbAttempt.ToDTO());
        }
        return climbAttemptDTOs;
      }
      else
      {
        return null;
      }
    }

    public ClimbAttempt ViewClimbingAttempt(int climberId, string climbId)
    {
      var climbingAttempt = _climbAttempt.SingleOrDefault(f => f.ClimberId == climberId && f.ClimbId == climbId);
      if (climbingAttempt != null)
      {
        return climbingAttempt;
      }
      else
      {
        return null;
      }
    }

    public void RecordClimbingAttempt(int climberId, string climbId, string? climbName, string? suggestedGrade, DateTime entryDate, int attempts, int rating)
    {
      var newClimbAttempt = ViewClimbingAttempt(climberId, climbId) ?? new ClimbAttempt();

      newClimbAttempt.ClimberId = climberId;
      newClimbAttempt.ClimbId = climbId;
      newClimbAttempt.ClimbName = climbName;
      newClimbAttempt.SuggestedGrade = suggestedGrade;
      newClimbAttempt.EntryDate = entryDate;
      newClimbAttempt.Attempts = attempts;
      newClimbAttempt.Rating = rating;

      var exists = _climbAttempt.Any(e => e.ClimberId == climberId && e.ClimbId == climbId);
      if (!exists)
      {
        _context.Add(newClimbAttempt);
      }
      else
      {
        _context.Update(newClimbAttempt);
      }

      _context.SaveChanges();

    }

    public void RecordClimbAttemptWithTags(int climberId, string climbId, string climbName, string? suggestedGrade, DateTime entryDate, int attempts, int rating, List<ClimbTagEntry> climbTagEntries)
    {
      var newClimbAttempt = ViewClimbingAttempt(climberId, climbId) ?? new ClimbAttempt();

      newClimbAttempt.ClimberId = climberId;
      newClimbAttempt.ClimbId = climbId;
      newClimbAttempt.ClimbName = climbName;
      newClimbAttempt.SuggestedGrade = suggestedGrade;
      newClimbAttempt.EntryDate = entryDate;
      newClimbAttempt.Attempts = attempts;
      newClimbAttempt.Rating = rating;
      newClimbAttempt.ClimbTagEntries = climbTagEntries;

      var exists = _climbAttempt.Any(e => e.ClimberId == climberId && e.ClimbId == climbId);
      if (!exists)
      {
        _context.Add(newClimbAttempt);
      }
      else
      {
        _context.Update(newClimbAttempt);
      }

      _context.SaveChanges();
    }
  }
}

/*
TABLE [ClimbAttempt] (
  [ID]    int     PRIMARY KEY IDENTITY(1, 1),
  [ClimberID]     int,
  [ClimbAttempts]   int, 
  [Rating]  int,
  [ClimbId]     nvarchar(50) NOT NULL, 
  [SuggestedGrade] nvarchar(5) NULL,
  [EntryDate] datetime
)
*/
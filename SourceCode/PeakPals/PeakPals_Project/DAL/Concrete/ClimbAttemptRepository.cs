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
      return _climbAttempt
          .Where(f => f.ClimberId == climberId)
          .OrderBy(f => f.EntryDate)
          .Select(f => f.ToDTO())
          .ToList();
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
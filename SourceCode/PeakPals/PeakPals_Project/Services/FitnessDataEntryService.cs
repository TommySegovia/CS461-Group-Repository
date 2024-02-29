
using PeakPals_Project.DAL.Abstract;
using PeakPals_Project.Models;
using PeakPals_Project.Models.DTO;
using PeakPals_Project.Services;
using System;
using PeakPals_Project.Data;

public class FitnessDataEntryService : IFitnessDataEntryService
{
    private readonly ApplicationDbContext _context;
    private readonly IFitnessDataEntryRepository _fitnessDataEntryRepository;
    public FitnessDataEntryService(ApplicationDbContext context, IFitnessDataEntryRepository fitnessDataEntryRepository)
    {
        _context = context;
        _fitnessDataEntryRepository = fitnessDataEntryRepository;
    }

    public void RecordTestResult(int? climberId, int? testId, int? result, int? bodyWeight)
    {
        FitnessDataEntry fitnessDataEntry = new FitnessDataEntry
        {
            
            ClimberId = climberId,
            TestId = testId,
            Result = result,
            BodyWeight = bodyWeight,
            EntryDate = DateTime.Now //date time.utcnow
        };
        _fitnessDataEntryRepository.AddOrUpdate(fitnessDataEntry);
        _context.SaveChanges();
    }
}

/*
Expected Response Body Example:
{
  "id": 0,
  "climberId": 0,
  "testId": 0,
  "result": 330,
  "bodyWeight": 2000,
  "entryDate": "2024-02-17T00:54:32.674Z"
}
*/
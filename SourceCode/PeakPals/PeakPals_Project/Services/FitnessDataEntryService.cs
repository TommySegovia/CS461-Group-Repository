
using PeakPals_Project.DAL.Abstract;
using PeakPals_Project.Models;
using PeakPals_Project.Models.DTO;
using PeakPals_Project.Services;
using System;
using PeakPals_Project.Data;
using ScottPlot;
using Humanizer;
using ScottPlot.Styles;
using System.Drawing;
using System.Reflection.Emit;

public class FitnessDataEntryService : IFitnessDataEntryService
{
    //private readonly ApplicationDbContext _context;
    private readonly PeakPalsContext _context;

    private readonly IFitnessDataEntryRepository _fitnessDataEntryRepository;
    public FitnessDataEntryService(PeakPalsContext context, IFitnessDataEntryRepository fitnessDataEntryRepository)
    {
        _context = context;
        _fitnessDataEntryRepository = fitnessDataEntryRepository;
    }

    public void RecordTestResult(int? climberId, int? testId, int? result, int? bodyWeight, int? age, string? gender, string? climbingExperience, int? climbingGrade)
    {
        FitnessDataEntry fitnessDataEntry = new FitnessDataEntry
        {

            ClimberId = climberId,
            TestId = testId,
            Result = result,
            BodyWeight = bodyWeight,
            Age = age,
            Gender = gender,
            ClimbingExperience = climbingExperience,
            ClimbingGrade = climbingGrade,
            EntryDate = DateTime.Now //date time.utcnow
        };
        _fitnessDataEntryRepository.AddOrUpdate(fitnessDataEntry);
        _context.SaveChanges();
    }

    public void DeleteTestResult(int id, int testId, int climberId)
    {
        FitnessDataEntry fitnessDataEntry = _fitnessDataEntryRepository.FindById(id);
        if (fitnessDataEntry == null)
        {
            return;
        }
        if (fitnessDataEntry.ClimberId != climberId || fitnessDataEntry.TestId != testId)
        {
            return;
        }
        _fitnessDataEntryRepository.Delete(fitnessDataEntry);
        _context.SaveChanges();
    }

    public void GenerateGraphsWithRecordHistory(List<FitnessDataEntryDTO> fitnessDataEntryListDTO, int testId)
    {
        if (testId == 7) //campus board test
        {
            return;
        }
        
        // Created using ScottPlot library, visit website to learn more.
        // create plot
        var plt = new ScottPlot.Plot(300, 250);

        // convert DateTime to Double for ScottPlot before plotting
        double[] xs = fitnessDataEntryListDTO.Where(x => x.EntryDate.HasValue && x.TestId == testId).Select(x => x.EntryDate.Value.ToOADate()).ToArray();
        if (xs.Length < 2)
        { return; }
        double[] results = fitnessDataEntryListDTO.Where(x => x.Result.HasValue && x.TestId == testId).Select(x => (double)x.Result.Value).ToArray();
        double[] ys = new double[xs.Length];

        // add points to graph
        plt.AddScatter(xs, results);

        // customize graph
        plt.XAxis.DateTimeFormat(true);
        plt.XAxis.Label("Timeline");

        if (testId == 3 || testId == 4) //generate a graph based on flexibility
        {
            plt.YAxis.Label("Flexibility (inches)");

        }
        else if (testId == 5) //repeater test seconds
        {
            plt.YAxis.Label("Repeater Test (seconds)");

        }
        else if (testId == 6) //smallest edge test mm
        {
            plt.YAxis.Label("Smallest Edge Test (mm)");

        }
        
        else
        {
            plt.YAxis.Label("% of Body Weight");
        }
        plt.YAxis.LabelStyle(color: Color.White);
        plt.XAxis.LabelStyle(color: Color.White);
        plt.Style(figureBackground: Color.Black, tick: Color.White);
        plt.Margins(x: .10, y: .20);

        // overwrite existing image for new analytics
        plt.SaveFig($"wwwroot/Images/Test_{testId}_ResultsOverTime.png");

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
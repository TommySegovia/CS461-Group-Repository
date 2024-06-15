
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
#nullable enable

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
        double[] xs = fitnessDataEntryListDTO
            .Where(x => x.EntryDate.HasValue && x.TestId == testId)
            .Select(x => x.EntryDate?.ToOADate() ?? 0) // Use null-conditional operator with a fallback value.
            .ToArray();
        if (xs.Length < 2)
        { return; }
        double[] results = fitnessDataEntryListDTO
            .Where(x => x.Result.HasValue && x.TestId == testId)
            .Select(x => (double)(x.Result ?? 0)) // Use null-coalescing operator directly.
            .ToArray();
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

    public void GenerateRadarChart(List<FitnessDataEntryDTO> userTests, List<double> averageTests, int userId)
    {
        //possilby include a scale factor for each test
        double userHangTestResults = (double)(userTests[0].Result ?? 0) / (userTests[0].BodyWeight ?? 1);
        double userPullTestResults = (double)(userTests[1].Result ?? 0) / (userTests[1].BodyWeight ?? 1);
        double userHammerCurlTestResults = (double)(userTests[2].Result ?? 0) / (userTests[2].BodyWeight ?? 1);
        double HipFlexorTestResults = (double)(userTests[3].Result ?? 0);
        double userHamstringFlexibilityTestResults = (double)(userTests[4].Result ?? 0);
        double userRepeatersTestResults = (double)(userTests[5].Result ?? 0);
        double userSmallestEdgeTestResults = (double)(userTests[6].Result ?? 0);
        double userCampusBoardTestResults = (double)(userTests[7].Result ?? 0);

        double averageHangTestResults = averageTests[0];
        double averagePullTestResults = averageTests[1];
        double averageHammerCurlTestResults = averageTests[2];
        double averageHipFlexorTestResults = averageTests[3];
        double averageHamstringFlexibilityTestResults = averageTests[4];
        double averageRepeatersTestResults = averageTests[5];
        double averageSmallestEdgeTestResults = averageTests[6];
        double averageCampusBoardTestResults = averageTests[7];


        //finger strength stat
        var userFingerStrengthStat = 0.0;
        if (userHangTestResults != 0 && userSmallestEdgeTestResults != 0)
        {
            userFingerStrengthStat = (((userHangTestResults / averageHangTestResults) * 100)
                                               + ((userSmallestEdgeTestResults / averageSmallestEdgeTestResults) * 100)) / 2;
        }

        //pull strength stat
        var userPullStrengthStat = 0.0;
        if (userPullTestResults != 0 && userHammerCurlTestResults != 0)
        {
            userPullStrengthStat = (((userPullTestResults / averagePullTestResults) * 100)
                                        + ((userHammerCurlTestResults / averageHammerCurlTestResults) * 100)) / 2;
        }

        //Power stat
        var userPowerStat = 0.0;
        if (userCampusBoardTestResults != 0 && userPullTestResults != 0)
        {

            double userCampusBoardConversionValue = GetCampusBoardConversionValue(userCampusBoardTestResults);
            double averageCampusBoardConversionValue = GetCampusBoardConversionValue(averageCampusBoardTestResults);
            userPowerStat = (((userCampusBoardConversionValue / averageCampusBoardConversionValue) * 100)
                                        + ((userPullTestResults / averagePullTestResults) * 100)) / 2;
        }

        //Endurance stat
        var userEnduranceStat = 0.0;
        if (userRepeatersTestResults != 0)
        {
            userEnduranceStat = (userRepeatersTestResults / averageRepeatersTestResults) * 100;
        }

        //Flexibility stat
        var userFlexibilityStat = 0.0;
        if (userHamstringFlexibilityTestResults != 0 && HipFlexorTestResults != 0)
        {
            userFlexibilityStat = (((userHamstringFlexibilityTestResults / averageHamstringFlexibilityTestResults) * 100)
                                        + ((HipFlexorTestResults / averageHipFlexorTestResults) * 100)) / 2;
        }

        //generate a 5 point radar chart labeled with the stats above where the user's stats are compared to the average stats where the average stats are the 100% mark
        // Initialize the plot
        var plt = new ScottPlot.Plot(700, 700);

        // Define the values and labels
        double[,] values = { { userFingerStrengthStat, userPullStrengthStat, userPowerStat, userEnduranceStat, userFlexibilityStat },
                            { 100, 100, 100, 100, 100 } };
        string[] labels = { "Finger Strength: " + Math.Round(userFingerStrengthStat, 2), "Pull Strength: " + Math.Round(userPullStrengthStat, 2), "Power: " + Math.Round(userPowerStat, 2), "Endurance: " + Math.Round(userEnduranceStat, 2), "Flexibility: " + Math.Round(userFlexibilityStat, 2) };
        string[] groupLabels = { "User", "Average" };

        // Add a radar plot to the plt object
        var radarPlot = plt.AddRadar(values);
        radarPlot.GroupLabels = groupLabels;
        radarPlot.CategoryLabels = labels;
        radarPlot.ShowAxisValues = false;

        // Customize the plot appearance
        plt.Legend(location: ScottPlot.Alignment.UpperRight);
        plt.XAxis.LabelStyle(color: System.Drawing.Color.White);
        plt.YAxis.LabelStyle(color: System.Drawing.Color.White);
        plt.Style(figureBackground: System.Drawing.Color.Black, tick: System.Drawing.Color.White);
        plt.Margins(x: .10, y: .20);

        // Save the figure
        plt.SaveFig($"wwwroot/Images/User_{userId}_RadarChart.png");

        return;
    }

    public List<string> GetUsersStrongestStats(List<FitnessDataEntryDTO> userTests, List<double> averageTests, int userId)
    {
        //possilby include a scale factor for each test
        double userHangTestResults = (userTests[0].Result.GetValueOrDefault() / userTests[0].BodyWeight.GetValueOrDefault(1));
        double userPullTestResults = (userTests[1].Result.GetValueOrDefault() / userTests[1].BodyWeight.GetValueOrDefault(1));
        double userHammerCurlTestResults = (userTests[2].Result.GetValueOrDefault() / userTests[2].BodyWeight.GetValueOrDefault(1));
        double HipFlexorTestResults = (double)(userTests[3].Result ?? 0);
        double userHamstringFlexibilityTestResults = (double)(userTests[4].Result ?? 0);
        double userRepeatersTestResults = (double)(userTests[5].Result ?? 0);
        double userSmallestEdgeTestResults = (double)(userTests[6].Result ?? 0);
        double userCampusBoardTestResults = (double)(userTests[7].Result ?? 0);

        double averageHangTestResults = averageTests[0];
        double averagePullTestResults = averageTests[1];
        double averageHammerCurlTestResults = averageTests[2];
        double averageHipFlexorTestResults = averageTests[3];
        double averageHamstringFlexibilityTestResults = averageTests[4];
        double averageRepeatersTestResults = averageTests[5];
        double averageSmallestEdgeTestResults = averageTests[6];
        double averageCampusBoardTestResults = averageTests[7];


        //finger strength stat
        var userFingerStrengthStat = 0.0;
        if (userHangTestResults != 0 && userSmallestEdgeTestResults != 0)
        {
            userFingerStrengthStat = (((userHangTestResults / averageHangTestResults) * 100)
                                               + ((userSmallestEdgeTestResults / averageSmallestEdgeTestResults) * 100)) / 2;
        }

        //pull strength stat
        var userPullStrengthStat = 0.0;
        if (userPullTestResults != 0 && userHammerCurlTestResults != 0)
        {
            userPullStrengthStat = (((userPullTestResults / averagePullTestResults) * 100)
                                        + ((userHammerCurlTestResults / averageHammerCurlTestResults) * 100)) / 2;
        }

        //Power stat
        var userPowerStat = 0.0;
        if (userCampusBoardTestResults != 0 && userPullTestResults != 0)
        {

            double userCampusBoardConversionValue = GetCampusBoardConversionValue(userCampusBoardTestResults);
            double averageCampusBoardConversionValue = GetCampusBoardConversionValue(averageCampusBoardTestResults);
            userPowerStat = (((userCampusBoardConversionValue / averageCampusBoardConversionValue) * 100)
                                        + ((userPullTestResults / averagePullTestResults) * 100)) / 2;
        }

        //Endurance stat
        var userEnduranceStat = 0.0;
        if (userRepeatersTestResults != 0)
        {
            userEnduranceStat = (userRepeatersTestResults / averageRepeatersTestResults) * 100;
        }

        //Flexibility stat
        var userFlexibilityStat = 0.0;
        if (userHamstringFlexibilityTestResults != 0 && HipFlexorTestResults != 0)
        {
            userFlexibilityStat = (((userHamstringFlexibilityTestResults / averageHamstringFlexibilityTestResults) * 100)
                                        + ((HipFlexorTestResults / averageHipFlexorTestResults) * 100)) / 2;
        }

        //return the two stats that the user is strongest in
        List<double> values = new List<double> { userFingerStrengthStat, userPullStrengthStat, userPowerStat, userEnduranceStat, userFlexibilityStat };
        List<string> labels = new List<string> { "Finger Strength", "Pull Strength", "Power", "Endurance", "Flexibility" };

        // Create a list of (value, label) pairs and sort it in descending order by value
        var sortedPairs = values.Zip(labels, (value, label) => (value, label))
                                .OrderByDescending(pair => pair.value)
                                .ToList();

        // Take the labels of the first two pairs (which have the highest values)
        List<string> stats = new List<string>();
        stats.Add(sortedPairs[0].label + ": " + Math.Round(sortedPairs[0].value, 2));
        stats.Add(sortedPairs[1].label + ": " + Math.Round(sortedPairs[1].value, 2));

        //return the two stats that the user is strongest in along with their values
        return stats;
    }

    public double GetCampusBoardConversionValue(double testResult)
    {
        switch (testResult)
        {
            case 123:
                return 0.25;
            case 135:
                return 0.5;
            case 147:
                return 0.75;
            case 159:
                return 1;
            default:
                return 0;
        }
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
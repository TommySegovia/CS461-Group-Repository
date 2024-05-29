using PeakPals_Project.Models;
using PeakPals_Project.Models.DTO;

namespace PeakPals_Project.DAL.Abstract
{
    public interface IFitnessDataEntryRepository : IRepository<FitnessDataEntry>
    {
        public List<FitnessDataEntryDTO> GetUserResultsWithTimesInChronologicalOrder(int climberId, int testId);
        public double? GetAverageResultDividedByBodyweight(int testId, int minAge, int maxAge, string gender, string climbingExperience, int minimumClimbingGrade, int maximumClimbingGrade);
        public double? GetUserAverageResultDividedByBodyweight(int climberId, int testId);
        public double? GetAverageResult (int testId, int minAge, int maxAge, string gender, string climbingExperience, int minimumClimbingGrade, int maximumClimbingGrade);
        public double? GetMostCommonResultCampusBoard (int testId, int minAge, int maxAge, string gender, string climbingExperience, int minimumClimbingGrade, int maximumClimbingGrade);
        
    }
}
using PeakPals_Project.Models;
using PeakPals_Project.Models.DTO;

namespace PeakPals_Project.DAL.Abstract
{
    public interface IFitnessDataEntryRepository : IRepository<FitnessDataEntry>
    {
        public List<FitnessDataEntryDTO> GetUserResultsWithTimesInChronologicalOrder(int climberId, int testId);
        public double? GetAverageResultDividedByBodyweight(int testId);
        public double? GetUserAverageResultDividedByBodyweight(int climberId, int testId);
        public double? GetAverageResultFlexibility(int testId);
        public double? GetAverageResultRepeater(int testId);
        public double? GetAverageResultSmallestEdge(int testId);
        public double? GetMostCommonResultCampusBoard(int testId);
    }
}
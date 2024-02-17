using PeakPals_Project.Models;
using PeakPals_Project.Models.DTO;

namespace PeakPals_Project.DAL.Abstract
{
    public interface IFitnessDataEntryRepository : IRepository<FitnessDataEntry>
    {
        //decimal is the result, string is the date
        //searches for the climber's results for a specific test
        //public List<Tuple<decimal, string>> GetUserResultsWithTimes(int climberId, int testId);
        public List<FitnessDataEntryDTO> GetUserResultsWithTimesInChronologicalOrder(int climberId, int testId);
    }
}
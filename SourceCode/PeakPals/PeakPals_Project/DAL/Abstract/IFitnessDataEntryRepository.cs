using PeakPals_Project.Models;
using PeakPals_Project.Models.DTO;

namespace PeakPals_Project.DAL.Abstract
{
    public interface IFitnessDataEntryRepository : IRepository<FitnessDataEntry>
    {
        public List<FitnessDataEntryDTO> GetUserResultsWithTimesInChronologicalOrder(int climberId, int testId);
    }
}
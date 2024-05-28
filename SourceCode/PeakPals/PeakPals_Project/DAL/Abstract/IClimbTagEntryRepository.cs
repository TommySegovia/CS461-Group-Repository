using PeakPals_Project.Models;
using PeakPals_Project.Models.DTO;

namespace PeakPals_Project.DAL.Abstract
{
    public interface IClimbTagEntryRepository
    {
        public List<ClimbTagEntryDTO> GetAll();
        public void AddClimbTagEntry(ClimbTagEntryDTO climbTagEntryDTO);
        public List<string> GetClimbTagEntryByClimbAttemptID(int climbAttemptID);
        public List<int> GetClimbTagEntryIdByTag(string tag);
        
    }
}
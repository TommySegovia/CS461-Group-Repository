using PeakPals_Project.Models;
using PeakPals_Project.Models.DTO;

namespace PeakPals_Project.DAL.Abstract
{
    public interface IClimberRepository : IRepository<Climber>
    {
        public ClimberDTO GetClimberByAspNetIdentityId(string aspNetIdentityId);
        public Climber GetClimberModelByAspNetIdentityId(string aspNetIdentityId);
        public Climber GetClimberByUsername(string username);
        public List<ClimberDTO> GetClimbersByUsername(string username);
        void UpdateUserName(string aspNetIdentityId, string newUserName);
        public Climber GetClimberByClimberId(int climberId);
    }
}
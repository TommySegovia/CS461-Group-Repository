using PeakPals_Project.Models;
using PeakPals_Project.Models.DTO;

namespace PeakPals_Project.DAL.Abstract
{
    public interface IClimberRepository : IRepository<Climber>
    {
        public ClimberDTO GetClimberByAspNetIdentityId(string aspNetIdentityId);
    }
}
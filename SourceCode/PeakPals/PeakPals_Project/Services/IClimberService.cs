
using PeakPals_Project.Models;
using PeakPals_Project.Models.DTO;
using System;
#nullable enable


namespace PeakPals_Project.Services
{
    public interface IClimberService
    {
        public ClimberDTO AddNewClimber(string? aspNetIdentityId, string? userName);
        public void UpdateClimber(Climber climber);
        
    }
}

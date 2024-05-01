
using PeakPals_Project.DAL.Abstract;
using PeakPals_Project.Models;
using PeakPals_Project.Models.DTO;
using PeakPals_Project.Services;
using System;
using PeakPals_Project.Data;
using PeakPals_Project.ExtensionMethods;

namespace PeakPals_Project.Services
{
    public class ClimberService : IClimberService
    {
        private readonly IClimberRepository _climberRepository;
        //private readonly ApplicationDbContext _context;
        private readonly PeakPalsContext _context;

        public ClimberService(IClimberRepository climberRepository, PeakPalsContext context)
        {
            _climberRepository = climberRepository;
            _context = context;
        }

        public ClimberDTO AddNewClimber(string? aspNetIdentityId, string? userName)
        {
            Climber climber = new Climber
            {
                Id = 0,
                AspnetIdentityId = aspNetIdentityId,
                UserName = userName
            };
            _climberRepository.AddOrUpdate(climber);
            _context.SaveChanges();

            return climber.ToDTO();
        }

        public void UpdateClimber(Climber climber)
        {
            _climberRepository.AddOrUpdate(climber);
            _context.SaveChanges();

        }
    }
}
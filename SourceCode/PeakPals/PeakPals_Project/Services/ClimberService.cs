
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
        private readonly ApplicationDbContext _context;

        public ClimberService(IClimberRepository climberRepository, ApplicationDbContext context)
        {
            _climberRepository = climberRepository;
            _context = context;
        }

        public ClimberDTO AddNewClimber(string? aspNetIdentityId, string? firstName, string? lastName)
        {
            Climber climber = new Climber
            {
                Id = 0,
                AspnetIdentityId = aspNetIdentityId,
                FirstName = firstName,
                LastName = lastName
            };
            _climberRepository.AddOrUpdate(climber);
            _context.SaveChanges();

            return climber.ToDTO();
        }
    }
}
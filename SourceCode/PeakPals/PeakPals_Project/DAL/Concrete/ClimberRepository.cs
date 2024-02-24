using PeakPals_Project.DAL.Abstract;
using PeakPals_Project.Models;
using PeakPals_Project.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using PeakPals_Project.Data;
using PeakPals_Project.ExtensionMethods;

namespace PeakPals_Project.DAL.Concrete
{
    public class ClimberRepository : Repository<Climber>, IClimberRepository
    {
        private DbSet<Climber> _climber;
        public ClimberRepository(ApplicationDbContext context) : base(context)
        {
            _climber = context.Climber;
        }

        public ClimberDTO GetClimberByAspNetIdentityId(string aspNetIdentityId)
        {
            // Search the climber table for climbers who have a matching aspnetIdentityId to the logged in user
            // If there are any, return the first one; otherwise, return null

            var climber = _climber.FirstOrDefault(c => c.AspnetIdentityId == aspNetIdentityId);

            if (climber != null)
            {
                return climber.ToDTO();
            }
            else
            {
                return null;
            }
        }

        public Climber GetClimberModelByAspNetIdentityId(string aspNetIdentityId)
        {
            // Search the climber table for climbers who have a matching aspnetIdentityId to the logged in user
            // If there are any, return the first one; otherwise, return null

            var climber = _climber.FirstOrDefault(c => c.AspnetIdentityId == aspNetIdentityId);

            if (climber != null)
            {
                return climber;
            }
            else
            {
                return null;
            }
        }

        public Climber GetClimberByUsername(string username)
        {
            var climber = _climber.FirstOrDefault(c => c.UserName == username);

            if (climber != null) {
                return climber;
            }
            else {
                return null;
            }
        }
    }
}
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
        public ClimberRepository(PeakPalsContext context) : base(context)
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
            return climber;
        }

        public List<ClimberDTO> GetClimbersByUsername(string username)
        {
            var climbers = _climber.Where(c => c.UserName.Contains(username));
            var climbersDTO = new List<ClimberDTO>();
            ClimberDTO temp = new ClimberDTO();

            foreach (Climber c in climbers)
            {
                temp = c.ToDTO();
                climbersDTO.Add(temp);
            }

            if (climbers != null)
            {
                return climbersDTO;
            }
            else
            {
                return null;
            }
        }

        public void UpdateUserName(string aspNetIdentityId, string newUserName)
        {
            var climber = GetClimberModelByAspNetIdentityId(aspNetIdentityId);

            if (climber != null)
            {
                climber.UserName = newUserName;
                AddOrUpdate(climber);
            }
            else
            {
                throw new Exception("Climber not found");
            }
        }

        public Climber GetClimberByClimberId(int climberId)
        {
            var climber = _climber.FirstOrDefault(c => c.Id == climberId);

            if (climber != null)
            {
                return climber;
            }
            else
            {
                return null;
            }
        }
    }
}
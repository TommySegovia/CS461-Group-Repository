using System.ComponentModel.DataAnnotations;

namespace PeakPals_Project.Models.DTO
{
    public class ClimberDTO
    {
        public int Id { get; set; }

        public string AspnetIdentityId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}

namespace PeakPals_Project.ExtensionMethods
{
    public static class ClimberExtensionMethods
    {
        public static Models.DTO.ClimberDTO ToDTO(this Models.Climber climber)
        {
            return new Models.DTO.ClimberDTO
            {
                Id = climber.Id,
                AspnetIdentityId = climber.AspnetIdentityId,
                FirstName = climber.FirstName,
                LastName = climber.LastName
            };
        }
        public static Models.Climber ToModel(this Models.DTO.ClimberDTO climberDTO)
        {
            return new Models.Climber
            {
                Id = climberDTO.Id,
                AspnetIdentityId = climberDTO.AspnetIdentityId,
                FirstName = climberDTO.FirstName,
                LastName = climberDTO.LastName
            };
        }
    }
}
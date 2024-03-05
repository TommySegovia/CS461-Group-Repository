using System.ComponentModel.DataAnnotations;

namespace PeakPals_Project.Models.DTO
{
    public class ClimberDTO
    {
        public int Id { get; set; }
        public string AspnetIdentityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string? DisplayName { get; set; }
        public string? Bio { get; set; }
        public string? ImageLink { get; set; }
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
                LastName = climber.LastName,
                UserName = climber.UserName,
                DisplayName = climber.DisplayName,
                Bio = climber.Bio,
                ImageLink = climber.ImageLink
            };
        }
        public static Models.Climber ToModel(this Models.DTO.ClimberDTO climberDTO)
        {
            return new Models.Climber
            {
                Id = climberDTO.Id,
                AspnetIdentityId = climberDTO.AspnetIdentityId,
                FirstName = climberDTO.FirstName,
                LastName = climberDTO.LastName,
                UserName = climberDTO.UserName,
                DisplayName = climberDTO.DisplayName,
                Bio = climberDTO.Bio,
                ImageLink = climberDTO.ImageLink
            };
        }
    }
}
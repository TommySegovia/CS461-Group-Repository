using System.Collections.Generic;
using PeakPals_Project.Models;
#nullable enable
namespace PeakPals_Project.Models.DTO
{
    public class ClimbAttemptDTO
    {
        public int Id { get; set; }
        public int ClimberId { get; set;}
        public string? ClimberName { get; set; }
        public string ClimbId { get; set; } = null!;
        public string? ClimbName { get; set; }
        public string? SuggestedGrade { get; set; }
        public DateTime EntryDate { get; set; }
        public int Attempts { get; set; }
        public int Rating { get; set; }

    }
}

namespace PeakPals_Project.ExtensionMethods
{
    public static class ClimbAttemptExtensionMethods
    {
        public static Models.DTO.ClimbAttemptDTO ToDTO(this Models.ClimbAttempt climbAttempt)
        {
            return new Models.DTO.ClimbAttemptDTO
            {
                Id = climbAttempt.Id,
                ClimberId = climbAttempt.ClimberId,
                ClimberName = climbAttempt.ClimberName,
                ClimbId = climbAttempt.ClimbId ?? "",
                ClimbName = climbAttempt.ClimbName,
                SuggestedGrade = climbAttempt.SuggestedGrade,
                EntryDate = climbAttempt.EntryDate,
                Attempts = climbAttempt.Attempts,
                Rating = climbAttempt.Rating

            };
        }

        public static Models.ClimbAttempt ToModel(this Models.DTO.ClimbAttemptDTO climbAttemptDTO)
        {
            return new Models.ClimbAttempt
            {
                Id = climbAttemptDTO.Id,
                ClimberId = climbAttemptDTO.ClimberId,
                ClimberName = climbAttemptDTO.ClimberName ?? "",
                ClimbId = climbAttemptDTO.ClimbId,
                ClimbName = climbAttemptDTO.ClimbName ?? "",
                SuggestedGrade = climbAttemptDTO.SuggestedGrade ?? "",
                EntryDate = climbAttemptDTO.EntryDate,
                Attempts = climbAttemptDTO.Attempts,
                Rating = climbAttemptDTO.Rating

            };
        }
    }
}
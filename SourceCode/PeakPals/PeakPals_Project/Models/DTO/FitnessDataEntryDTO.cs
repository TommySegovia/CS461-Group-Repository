using System.ComponentModel.DataAnnotations;
#nullable enable

namespace PeakPals_Project.Models.DTO
{
    public class FitnessDataEntryDTO
    {
        public int Id { get; set; }

        public int? ClimberId { get; set; }

        public int? TestId { get; set; }

        public int? Result { get; set; }
        public int? BodyWeight { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? ClimbingExperience { get; set; }
        public int? ClimbingGrade { get; set; }
        public DateTime? EntryDate { get; set; }
    }
}
namespace PeakPals_Project.ExtensionMethods
{
    public static class FitnessDataEntryExtensionMethods
    {
        public static Models.DTO.FitnessDataEntryDTO ToDTO(this Models.FitnessDataEntry fitnessDataEntry)
        {
            return new Models.DTO.FitnessDataEntryDTO
            {
                Id = fitnessDataEntry.Id,
                ClimberId = fitnessDataEntry.ClimberId,
                TestId = fitnessDataEntry.TestId,
                Result = fitnessDataEntry.Result,
                BodyWeight = fitnessDataEntry.BodyWeight,
                EntryDate = fitnessDataEntry.EntryDate,
                Age = fitnessDataEntry.Age,
                Gender = fitnessDataEntry.Gender,
                ClimbingExperience = fitnessDataEntry.ClimbingExperience,
                ClimbingGrade = fitnessDataEntry.ClimbingGrade
            };
        }
        public static Models.FitnessDataEntry ToModel(this Models.DTO.FitnessDataEntryDTO fitnessDataEntryDTO)
        {
            return new Models.FitnessDataEntry
            {
                Id = fitnessDataEntryDTO.Id,
                ClimberId = fitnessDataEntryDTO.ClimberId,
                TestId = fitnessDataEntryDTO.TestId,
                Result = fitnessDataEntryDTO.Result,
                BodyWeight = fitnessDataEntryDTO.BodyWeight,
                EntryDate = fitnessDataEntryDTO.EntryDate,
                Age = fitnessDataEntryDTO.Age,
                Gender = fitnessDataEntryDTO.Gender,
                ClimbingExperience = fitnessDataEntryDTO.ClimbingExperience,
                ClimbingGrade = fitnessDataEntryDTO.ClimbingGrade
            };
        }
    }
}
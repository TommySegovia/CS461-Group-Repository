using System.ComponentModel.DataAnnotations;

namespace PeakPals_Project.Models.DTO
{
    public class ClimbTagEntryDTO
    {
        public int Id { get; set; }
        public int ClimbAttemptID { get; set; }
        public int TagID { get; set; }

    }
}

namespace PeakPals_Project.ExtensionMethods
{
    public static class ClimbTagEntryExtensionMethods
    {
        public static Models.DTO.ClimbTagEntryDTO ToDTO(this Models.ClimbTagEntry climbTagEntry)
        {
            return new Models.DTO.ClimbTagEntryDTO
            {
                Id = climbTagEntry.ID,
                ClimbAttemptID = climbTagEntry.ClimbAttemptID,
                TagID = climbTagEntry.TagID
            };
        }

        public static Models.ClimbTagEntry ToModel(this Models.DTO.ClimbTagEntryDTO climbTagEntryDTO)
        {
            return new Models.ClimbTagEntry
            {
                ID = climbTagEntryDTO.Id,
                ClimbAttemptID = climbTagEntryDTO.ClimbAttemptID,
                TagID = climbTagEntryDTO.TagID
            };
        }
    }
}
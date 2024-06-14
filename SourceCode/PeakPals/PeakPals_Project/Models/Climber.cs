using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
#nullable enable

namespace PeakPals_Project.Models
{
    [Table("Climber")]
    public partial class Climber
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("ASPNetIdentityId")]
        public string AspnetIdentityId { get; set; } = null!;

        [StringLength(255)]
        public string? FirstName { get; set; }

        [StringLength(255)]
        public string? LastName { get; set; }

        [Required]
        [StringLength(255)]
        public string UserName { get; set; } = null!;

        [StringLength(25)]
        public string? DisplayName { get; set; }

        [StringLength(1600)]
        public string? Bio { get; set; }

        [StringLength(255)]
        public string? ImageLink { get; set; }

        [StringLength(255)]
        public string? CustomLink { get; set; }

        [StringLength(255)]
        public string? LinkText { get; set; }

        [StringLength(255)]
        public string? City { get; set; }

        [StringLength(255)]
        public string? State { get; set; }

        public int? Age { get; set; }

        [Column("GroupListID")]
        public int? GroupListId { get; set; }

        // Navigation property for GroupList
        [JsonIgnore]
        public virtual ICollection<GroupList> GroupLists { get; set; } = new List<GroupList>();

        // Navigation property for FitnessDataEntries
        [JsonIgnore]
        [InverseProperty("Climber")]
        public virtual ICollection<FitnessDataEntry> FitnessDataEntries { get; set; } = new List<FitnessDataEntry>();

        [InverseProperty("Climber")]
        public virtual ICollection<ClimbAttempt> ClimbAttempts { get; set; } = new List<ClimbAttempt>();
    }
}

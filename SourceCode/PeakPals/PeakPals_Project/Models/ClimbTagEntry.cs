using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace PeakPals_Project.Models
{
    [Table("ClimbTagEntry")]
    public partial class ClimbTagEntry
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("ClimbAttemptID")]
        public int ClimbAttemptID { get; set; }
        
        [Column("TagID")]
        public int TagID { get; set; }

        // Navigation property for the related ClimbAttempt
        [ForeignKey("ClimbAttemptID")]
        public virtual ClimbAttempt ClimbAttempt { get; set; }

        // Navigation property for the related Tag
        [ForeignKey("TagID")]
        public virtual Tag Tag { get; set; }
    }
}

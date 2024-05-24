using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace PeakPals_Project.Models
{
    [Table("Tag")]
    public partial class Tag
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }
        
        [Column("TagName")]
        [Required]
        [StringLength(200)]
        public string TagName { get; set; }
    }
}

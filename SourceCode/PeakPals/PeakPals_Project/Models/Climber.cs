using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace PeakPals_Project.Models;

[Table("Climber")]
public partial class Climber
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("ASPNetIdentityId")]
    [StringLength(450)]
    public string AspnetIdentityId { get; set; } = null!;

    [StringLength(255)]
    public string? FirstName { get; set; } = null!;

    [StringLength(255)]
    public string? LastName { get; set; } = null!;

    [JsonIgnore]
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

    [InverseProperty("Climber")]
    public virtual ICollection<FitnessDataEntry> FitnessDataEntries { get; set; } = new List<FitnessDataEntry>();
}

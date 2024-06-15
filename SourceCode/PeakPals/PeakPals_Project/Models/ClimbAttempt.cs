using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

#nullable enable
namespace PeakPals_Project.Models;

[Table("ClimbAttempt")]
public partial class ClimbAttempt
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [JsonIgnore]
    [Column("ClimberID")]
    public int ClimberId { get; set; }

    [StringLength(50)]
    public string? ClimbId { get; set; }

    [JsonIgnore]
    [StringLength(200)]
    public string? ClimbName { get; set; }

    [JsonIgnore]
    [StringLength(8)] 
    public string? SuggestedGrade { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EntryDate { get; set; }

    [JsonIgnore]
    [StringLength(50)]
    public string? ClimberName { get; set; }
    public int Attempts { get; set; }
    public int Rating { get; set; }

    [JsonIgnore]
    [ForeignKey("ClimberId")]
    [InverseProperty("ClimbAttempts")]
    public virtual Climber? Climber { get; set; }

    [JsonIgnore]
    [InverseProperty("ClimbAttempt")]
    public virtual List<ClimbTagEntry>? ClimbTagEntries { get; set; } = new List<ClimbTagEntry>();
}
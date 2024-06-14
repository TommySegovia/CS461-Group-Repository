using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
#nullable enable

namespace PeakPals_Project.Models;

[Table("FitnessDataEntry")]
public partial class FitnessDataEntry
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [JsonIgnore]
    [Column("ClimberID")]
    public int? ClimberId { get; set; }

    [JsonIgnore]
    [Column("TestID")]
    public int? TestId { get; set; }

    public int? Result { get; set; }

    public int? BodyWeight { get; set; }
    public int? Age { get; set; }
    public string? Gender { get; set; }
    public string? ClimbingExperience { get; set; }
    public int? ClimbingGrade { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EntryDate { get; set; }

    [JsonIgnore]
    [ForeignKey("ClimberId")]
    [InverseProperty("FitnessDataEntries")]
    public virtual Climber? Climber { get; set; }

    [JsonIgnore]
    [ForeignKey("TestId")]
    [InverseProperty("FitnessDataEntries")]
    public virtual FitnessTest? Test { get; set; }
}

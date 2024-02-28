using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

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

    [Column(TypeName = "datetime")]
    public DateTime? EntryDate { get; set; }

    [ForeignKey("ClimberId")]
    [InverseProperty("FitnessDataEntries")]
    public virtual Climber? Climber { get; set; }

    [ForeignKey("TestId")]
    [InverseProperty("FitnessDataEntries")]
    public virtual FitnessTest? Test { get; set; }
}

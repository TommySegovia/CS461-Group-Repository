using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

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
    public string FirstName { get; set; } = null!;

    [StringLength(255)]
    public string LastName { get; set; } = null!;

    [InverseProperty("Climber")]
    public virtual ICollection<FitnessDataEntry> FitnessDataEntries { get; set; } = new List<FitnessDataEntry>();
}

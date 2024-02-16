using System;
using System.Collections.Generic;

namespace PeakPals_Project.Models;

public partial class FitnessDataEntry
{
    public int Id { get; set; }

    public int? ClimberId { get; set; }

    public int? TestId { get; set; }

    public string? Result { get; set; }

    public DateTime? EntryDate { get; set; }

    public virtual Climber? Climber { get; set; }

    public virtual FitnessTest? Test { get; set; }
}

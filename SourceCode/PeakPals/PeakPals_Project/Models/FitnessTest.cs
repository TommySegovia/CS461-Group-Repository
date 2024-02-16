using System;
using System.Collections.Generic;

namespace PeakPals_Project.Models;

public partial class FitnessTest
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<FitnessDataEntry> FitnessDataEntries { get; set; } = new List<FitnessDataEntry>();
}

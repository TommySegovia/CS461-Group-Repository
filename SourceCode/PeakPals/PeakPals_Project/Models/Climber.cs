using System;
using System.Collections.Generic;

namespace PeakPals_Project.Models;

public partial class Climber
{
    public int Id { get; set; }

    public string AspnetIdentityId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual ICollection<FitnessDataEntry> FitnessDataEntries { get; set; } = new List<FitnessDataEntry>();
}


using Microsoft.CodeAnalysis;

namespace PeakPals_Project.Models;

public class OpenBetaQueryResult
{
    public List<Area>? Areas { get; set; }

    public class Area
    {
        public string? Area_Name { get; set; }
        public string? Uuid { get; set; }
        public Metadata? Metadata { get; set; }
        public List<string>? Ancestors { get; set; }

    }
    public class Metadata
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
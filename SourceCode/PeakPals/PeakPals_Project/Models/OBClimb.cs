

namespace PeakPals_Project.Models;

public class OBClimb
{
    public ClimbData? Climb { get; set; }

    public class ClimbData
    {
        public string? Uuid { get; set; }
        public string? Name { get; set; }
        public List<string> Ancestors { get; set; }
        public Metadata Metadata { get; set; }
        public Content Content { get; set; } 
    }

    public class Metadata
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class Content
    {
        public string? Description { get; set; }
    }

}
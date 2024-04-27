

namespace PeakPals_Project.Models;

public class OBClimb
{
    public ClimbData? Climb { get; set; }

    public class ClimbData
    {
        public string? Uuid { get; set; }
        public string? Name { get; set; }
        public string? Fa { get; set; }
        public List<string> Ancestors { get; set; }
        public Metadata Metadata { get; set; }
        public Content Content { get; set; } 
        public Grades Grades { get; set; }
        public Type Type { get; set; }
        public List<Media>? Media { get; set; }
    }

    public class Metadata
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class Content
    {
        public string? Description { get; set; }
        public string? Location { get; set; }
        public string? Protection { get; set; }
    }
    
    public class Grades
    {
        public string? Yds { get; set; }
        public string? Vscale { get; set; }
    }

    public class Type
    {
        public bool? Trad { get; set; }
        public bool? Sport { get; set; }
        public bool? Bouldering { get; set; }
        public bool? Deepwatersolo { get; set; }
        public bool? Alpine { get; set; }
        public bool? Snow { get; set; }
        public bool? Ice { get; set; }
        public bool? Mixed { get; set; }
        public bool? Aid { get; set; }
        public bool? Tr { get; set; }
    }

    public class Media
    {
        public string? MediaUrl { get; set; }
    }


}
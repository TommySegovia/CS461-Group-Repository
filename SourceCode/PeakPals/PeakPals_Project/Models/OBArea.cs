

namespace PeakPals_Project.Models;

public class OBArea
{
    public AreaData? Area { get; set; }

    public class AreaData
    {
        public string? Id { get; set; }
        public string? Area_Name { get; set; }
        public List<string>? Ancestors { get; set; }
        public Metadata? Metadata { get; set; }
        public Content? Content { get; set; }
        public List<Children>? Children { get; set; }
        public List<Climb>? Climbs { get; set; }
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

    public class Children
    {
        public string? Id { get; set; }
        public string? Area_Name { get; set; }
        public Metadata? Metadata { get; set; }
    }

    public class Climb
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public Metadata? Metadata { get; set; }
    }
}
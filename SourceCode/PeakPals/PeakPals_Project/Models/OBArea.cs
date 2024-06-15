
#nullable enable
namespace PeakPals_Project.Models;

public class OBArea
{
    public AreaData? Area { get; set; }

    public class AreaData
    {
        public string? Uuid { get; set; }
        public string? Area_Name { get; set; }
        public int TotalClimbs { get; set; }
        public List<string>? Ancestors { get; set; }
        public List<string>? PathTokens { get; set; }
        public Metadata? Metadata { get; set; }
        public AuthorMetadata? AuthorMetadata { get; set; }
        public Content? Content { get; set; }
        public List<Organizations> Organizations { get; set; } = new List<Organizations>();
        public List<Media>? Media { get; set;}
        public List<Children>? Children { get; set; }
        public List<Climb>? Climbs { get; set; }

    }

    public class Metadata
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class AuthorMetadata
    {
        public long? CreatedAt { get; set; }
    }

    public class Organizations
    {
        public string? DisplayName { get; set; }
        public OrganizationContent? Content { get; set; }
    }

    public class Content
    {
        public string? Description { get; set; }
    }

    public class OrganizationContent
    {
        public string Website { get; set; } = "";
    }

    public class Media
    {
        public string? MediaUrl { get; set; }
    }

    public class Children
    {
        public string? Uuid { get; set; }
        public string? Area_Name { get; set; }
        public Metadata? Metadata { get; set; }
        public int TotalClimbs { get; set; }
    }

    public class Climb
    {
        public string? Uuid { get; set; }
        public string? Name { get; set; }
        public Metadata? Metadata { get; set; }
    }
}
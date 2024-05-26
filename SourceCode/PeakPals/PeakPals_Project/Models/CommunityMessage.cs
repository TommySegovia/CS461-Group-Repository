
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PeakPals_Project.Models;

[Table("CommunityMessage")]
public partial class CommunityMessage
{
    [Key]
    [Column("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [JsonIgnore]
    [Required]
    [Column("ClimberID")]
    public int ClimberId { get; set; }

    [JsonIgnore]
    [Required]
    [Column("CommunityGroupID")]
    public int CommunityGroupId { get; set; }

    [StringLength(25)]
    public string DisplayName { get; set; }

    [Required]
    [StringLength(512)]
    public string Message { get; set; }

    [JsonIgnore]
    [ForeignKey("ClimberId")]
    public virtual Climber Climber { get; set; }

    [JsonIgnore]
    [ForeignKey("CommunityGroupId")]
    public virtual CommunityGroup CommunityGroup { get; set; }
}

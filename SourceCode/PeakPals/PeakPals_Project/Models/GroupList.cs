using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeakPals_Project.Models
{
    [Table("GroupList")]
    public partial class GroupList
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("ClimberID")]
        public int ClimberID { get; set; }

        [Column("CommunityGroupID")]
        public int CommunityGroupID { get; set; }

        // Navigation property for the related Climber
        [ForeignKey("ClimberID")]
        [InverseProperty("GroupLists")]
        public virtual Climber Climber { get; set; }

        // Navigation property for the related CommunityGroup
        [ForeignKey("CommunityGroupID")]
        public virtual CommunityGroup CommunityGroup { get; set; }
    }
}
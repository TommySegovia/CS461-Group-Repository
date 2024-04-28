using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PeakPals_Project.Models;

[Table("GroupList")]
public partial class GroupList
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("CommunityGroupID")]
    public int CommunityGroupID { get; set; }
    
    [InverseProperty("GroupList")]
    public virtual ICollection<Climber> Climbers { get; set; } = new List<Climber>();
}

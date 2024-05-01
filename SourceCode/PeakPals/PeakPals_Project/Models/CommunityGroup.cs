using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PeakPals_Project.Models;

[Table("CommunityGroup")]
public partial class CommunityGroup
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("OwnerID")]
    public int OwnerID { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [StringLength(1600)]
    public string? Description { get; set; }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer; // Add this using directive
using PeakPals_Project.Areas.Identity.Data;
using PeakPals_Project.Models;


namespace PeakPals_Project.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    // For Mock Testing
    public ApplicationDbContext() { }

    public virtual DbSet<PeakPals_Project.Models.Climber> Climber { get; set; } = default!;
    public virtual DbSet<PeakPals_Project.Models.FitnessDataEntry> FitnessDataEntry { get; set; } = default!;

}

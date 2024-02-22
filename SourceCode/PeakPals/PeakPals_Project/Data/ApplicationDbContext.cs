using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer; // Add this using directive
using PeakPals_Project.Models;


namespace PeakPals_Project.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    public DbSet<PeakPals_Project.Models.Climber> Climber { get; set; } = default!;
    public DbSet<PeakPals_Project.Models.FitnessDataEntry> FitnessDataEntry { get; set; } = default!;

}

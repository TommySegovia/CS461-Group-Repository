using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PeakPals_Project.Models;

public partial class PeakPalsContext : DbContext
{
    public PeakPalsContext()
    {
    }

    public PeakPalsContext(DbContextOptions<PeakPalsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Climber> Climber { get; set; }

    public virtual DbSet<FitnessDataEntry> FitnessDataEntry { get; set; }

    public virtual DbSet<FitnessTest> FitnessTest { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Server=DESKTOP-S6LLSAN\\SQLEXPRESS;Database=PeakPals;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Climber>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Climber__3214EC27495224C8");
        });

        modelBuilder.Entity<FitnessDataEntry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FitnessD__3214EC27A69CA072");

            entity.HasOne(d => d.Climber).WithMany(p => p.FitnessDataEntries).HasConstraintName("FK_FitnessDataEntry_Climber_ID");

            entity.HasOne(d => d.Test).WithMany(p => p.FitnessDataEntries).HasConstraintName("FK_FitnessDataEntry_Test_ID");
        });

        modelBuilder.Entity<FitnessTest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FitnessT__3214EC27C00405FE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

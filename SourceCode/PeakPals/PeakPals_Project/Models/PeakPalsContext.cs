using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PeakPals_Project.Models;

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
    public virtual DbSet<GroupList> GroupList { get; set; }
    public virtual DbSet<CommunityGroup> CommunityGroup { get; set; }

    public virtual DbSet<CommunityMessage> CommunityMessage { get; set;}

    public virtual DbSet<ClimbAttempt> ClimbAttempt { get; set; }
    

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

        modelBuilder.Entity<ClimbAttempt>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(d => d.Climber).WithMany(p => p.ClimbAttempts).HasConstraintName("FK_ClimbAttempt_Climber_ID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<PeakPals_Project.Models.ClimbTagEntry> ClimbTagEntry { get; set; }


}

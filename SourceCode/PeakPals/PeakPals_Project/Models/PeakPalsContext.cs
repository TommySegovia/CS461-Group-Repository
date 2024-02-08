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

    public virtual DbSet<Climber> Climbers { get; set; }

    public virtual DbSet<FitnessDataEntry> FitnessDataEntries { get; set; }

    public virtual DbSet<FitnessTest> FitnessTests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-S6LLSAN\\SQLEXPRESS;Database=PeakPals;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Climber>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Climber__3214EC27DF8A9A1A");

            entity.ToTable("Climber");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AspnetIdentityId)
                .HasMaxLength(450)
                .HasColumnName("ASPNetIdentityId");
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
        });

        modelBuilder.Entity<FitnessDataEntry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FitnessD__3214EC2711A27F01");

            entity.ToTable("FitnessDataEntry");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ClimberId).HasColumnName("ClimberID");
            entity.Property(e => e.EntryDate).HasColumnType("datetime");
            entity.Property(e => e.Result).HasMaxLength(255);
            entity.Property(e => e.TestId).HasColumnName("TestID");

            entity.HasOne(d => d.Climber).WithMany(p => p.FitnessDataEntries)
                .HasForeignKey(d => d.ClimberId)
                .HasConstraintName("FK_FitnessDataEntry_Climber_ID");

            entity.HasOne(d => d.Test).WithMany(p => p.FitnessDataEntries)
                .HasForeignKey(d => d.TestId)
                .HasConstraintName("FK_FitnessDataEntry_Test_ID");
        });

        modelBuilder.Entity<FitnessTest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FitnessT__3214EC2744284475");

            entity.ToTable("FitnessTest");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-S6LLSAN\\SQLEXPRESS;Database=PeakPals;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False");

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

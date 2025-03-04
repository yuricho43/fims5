using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Fims5.Models;

public partial class FimsDbContext : DbContext
{
    public FimsDbContext()
    {
    }

    public FimsDbContext(DbContextOptions<FimsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Titem> Titems { get; set; }

    public virtual DbSet<Tsheet> Tsheets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database= FimsDb;Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Titem>(entity =>
        {
            entity.ToTable("TItems");

            entity.HasIndex(e => e.TsheetId, "IX_TItems_TSheetId");

            entity.Property(e => e.TsheetId).HasColumnName("TSheetId");

            entity.HasOne(d => d.Tsheet).WithMany(p => p.Titems).HasForeignKey(d => d.TsheetId);
        });

        modelBuilder.Entity<Tsheet>(entity =>
        {
            entity.ToTable("TSheets");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

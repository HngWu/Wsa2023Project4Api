using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Wsa2023Project4Api.Models;

public partial class AdditionalPhContext : DbContext
{
    public AdditionalPhContext()
    {
    }

    public AdditionalPhContext(DbContextOptions<AdditionalPhContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAmenity> TblAmenities { get; set; }

    public virtual DbSet<TblMunicipality> TblMunicipalities { get; set; }

    public virtual DbSet<TblTouristSpot> TblTouristSpots { get; set; }

    public virtual DbSet<Tblrole> Tblroles { get; set; }

    public virtual DbSet<Tbluser> Tblusers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=AdditionalPH;Trusted_Connection=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblAmenity>(entity =>
        {
            entity.ToTable("tblAmenities");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasMaxLength(150);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SpotId).HasColumnName("spotID");

            entity.HasOne(d => d.Spot).WithMany(p => p.TblAmenities)
                .HasForeignKey(d => d.SpotId)
                .HasConstraintName("FK_tblAmenities_tblTouristSpot");
        });

        modelBuilder.Entity<TblMunicipality>(entity =>
        {
            entity.ToTable("tblMunicipality");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MunDescription)
                .HasMaxLength(1500)
                .IsUnicode(false);
            entity.Property(e => e.MunName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblTouristSpot>(entity =>
        {
            entity.ToTable("tblTouristSpot");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Entrancefee)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("entrancefee");
            entity.Property(e => e.MunId).HasColumnName("munID");
            entity.Property(e => e.Picture)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("picture");
            entity.Property(e => e.Rating)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rating");
            entity.Property(e => e.SpotDescription)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("spotDescription");
            entity.Property(e => e.Tname)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Mun).WithMany(p => p.TblTouristSpots)
                .HasForeignKey(d => d.MunId)
                .HasConstraintName("FK_tblTouristSpot_tblMunicipality");
        });

        modelBuilder.Entity<Tblrole>(entity =>
        {
            entity.ToTable("tblrole");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RoleDescription)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("roleDescription");
            entity.Property(e => e.Rolename)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("rolename");
        });

        modelBuilder.Entity<Tbluser>(entity =>
        {
            entity.ToTable("tbluser");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lastname");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Tblusers)
                .HasForeignKey(d => d.Roleid)
                .HasConstraintName("FK_tbluser_tblrole");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

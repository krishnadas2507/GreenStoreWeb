using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GreenStoreWeb.Models;

public partial class UserDetailsContext : DbContext
{
    public UserDetailsContext()
    {
    }

    public UserDetailsContext(DbContextOptions<UserDetailsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Authentication> Authentications { get; set; }

    public virtual DbSet<Vegelist> Vegelists { get; set; }

    public virtual DbSet<Vegetableprice> Vegetableprices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Authentication>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("authentication_pkey");

            entity.ToTable("authentication");

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
        });

        modelBuilder.Entity<Vegelist>(entity =>
        {
            entity.HasKey(e => e.Vegid).HasName("vegelist_pkey");

            entity.ToTable("vegelist");

            entity.Property(e => e.Vegid).HasColumnName("vegid");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Vegname)
                .HasMaxLength(255)
                .HasColumnName("vegname");

            entity.HasOne(d => d.User).WithMany(p => p.Vegelists)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("vegelist_userid_fkey");
        });

        modelBuilder.Entity<Vegetableprice>(entity =>
        {
            entity.HasKey(e => e.Priceid).HasName("vegetableprices_pkey");

            entity.ToTable("vegetableprices");

            entity.Property(e => e.Priceid).HasColumnName("priceid");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Vegid).HasColumnName("vegid");

            entity.HasOne(d => d.User).WithMany(p => p.Vegetableprices)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("fk_user_id");

            entity.HasOne(d => d.Veg).WithMany(p => p.Vegetableprices)
                .HasForeignKey(d => d.Vegid)
                .HasConstraintName("fk_veg_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace online_job_finder.DataBase.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblRole> TblRoles { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=MSI\\SQLEXPRESS2022; Database=onlinejobfinder; User Id=sa; Password=sasa; TrustServerCertificate=True; MultipleActiveResultSets=true;");

        // Open it for your db
        //optionsBuilder.UseSqlServer("Server=.; Database=onlinejobfinder; User Id=sa; Password=sasa; TrustServerCertificate=True; MultipleActiveResultSets=true;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblRole>(entity =>
        {
            entity.HasKey(e => e.RoleId);

            entity.ToTable("Tbl_Role");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("Role_Id");
            entity.Property(e => e.CreatedAt).HasColumnName("Created_at");
            entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");
            entity.Property(e => e.RoleName).HasColumnName("Role_Name");
            entity.Property(e => e.UpdatedAt).HasColumnName("Updated_at");
            entity.Property(e => e.Version)
            .ValueGeneratedOnAddOrUpdate();
            //.ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("Tbl_Users");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("User_Id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Created_at");
            entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");
            entity.Property(e => e.IsInformationCompleted).HasColumnName("Is_Information_Completed");
            entity.Property(e => e.ProfilePhoto).HasColumnName("Profile_Photo");
            entity.Property(e => e.RoleId).HasColumnName("Role_Id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Updated_at");
            entity.Property(e => e.Version).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Role).WithMany(p => p.TblUsers)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_Users_Tbl_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

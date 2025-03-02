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

    public virtual DbSet<CompanyProfile> CompanyProfiles { get; set; }

    public virtual DbSet<TblRole> TblRoles { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public virtual DbSet<TblJobCategory> TblJobCategories { get; set; }

    public virtual DbSet<TblSkill> TblSkills { get; set; }


    public virtual DbSet<TblLocation> TblLocations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=HA\\SQL2019; Database=onlinejobfinder; User Id=sa; Password=sasa; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CompanyProfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__company___3213E83F7BEECB56");

            entity.ToTable("company_profiles");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("company_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.LocationId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("location_id");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("user_id");
            entity.Property(e => e.Version).HasColumnName("version");
            entity.Property(e => e.Website)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("website");
        });

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
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("Tbl_Users");

            entity.HasIndex(e => e.RoleId, "IX_Tbl_Users_Role_Id");

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



        modelBuilder.Entity<TblJobCategory>(entity =>
        {
            entity.HasKey(e => e.JobCategoryID);

            entity.ToTable("Tbl_JobCategories");

           

            entity.Property(e => e.JobCategoryID)
                .ValueGeneratedNever()
                .HasColumnName("JobCategeory_Id");
           
        });

        modelBuilder.Entity<TblLocation>(entity =>
        {
            entity.HasKey(e => e.LocationID);

            entity.ToTable("Tbl_Locations");



            entity.Property(e => e.LocationID)
                .ValueGeneratedNever()
                .HasColumnName("Location_ID");

        });


        modelBuilder.Entity<TblSkill>(entity =>
        {
            entity.HasKey(e => e.SkillID);

            entity.ToTable("Tbl_Skills");



            entity.Property(e => e.SkillID)
                .ValueGeneratedNever()
                .HasColumnName("Skill_Id");

        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace online_job_finder.DataBase.Models;

public partial class OnlinejobfinderContext : DbContext
{
    public OnlinejobfinderContext()
    {
    }

    public OnlinejobfinderContext(DbContextOptions<OnlinejobfinderContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblApplicantEducation> TblApplicantEducations { get; set; }

    public virtual DbSet<TblApplicantExperience> TblApplicantExperiences { get; set; }

    public virtual DbSet<TblApplicantJobCategory> TblApplicantJobCategories { get; set; }

    public virtual DbSet<TblApplicantProfile> TblApplicantProfiles { get; set; }

    public virtual DbSet<TblApplicantSkill> TblApplicantSkills { get; set; }

    public virtual DbSet<TblApplication> TblApplications { get; set; }

    public virtual DbSet<TblCompanyProfile> TblCompanyProfiles { get; set; }

    public virtual DbSet<TblJob> TblJobs { get; set; }

    public virtual DbSet<TblJobCategory> TblJobCategories { get; set; }

    public virtual DbSet<TblJobSkill> TblJobSkills { get; set; }

    public virtual DbSet<TblLocation> TblLocations { get; set; }

    public virtual DbSet<TblResume> TblResumes { get; set; }

    public virtual DbSet<TblReview> TblReviews { get; set; }

    public virtual DbSet<TblRole> TblRoles { get; set; }

    public virtual DbSet<TblSavedJob> TblSavedJobs { get; set; }

    public virtual DbSet<TblSkill> TblSkills { get; set; }

    public virtual DbSet<TblSocialMedium> TblSocialMedia { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Initial Catalog=onlinejobfinder;User ID=sa;Password=sa;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AS");

        modelBuilder.Entity<TblApplicantEducation>(entity =>
        {
            entity.HasKey(e => e.ApplicantEducationsId);

            entity.ToTable("Tbl_Applicant_Educations");

            entity.Property(e => e.ApplicantEducationsId)
                .ValueGeneratedNever()
                .HasColumnName("Applicant_Educations_Id");
            entity.Property(e => e.ApplicantProfilesId).HasColumnName("Applicant_Profiles_Id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Created_at");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.EndDate).HasColumnName("End_Date");
            entity.Property(e => e.FieldOfStudy).HasColumnName("Field_Of_Study");
            entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");
            entity.Property(e => e.SchoolName).HasColumnName("School_Name");
            entity.Property(e => e.StartDate).HasColumnName("Start_Date");
            entity.Property(e => e.StillAttending).HasColumnName("Still_Attending");
            entity.Property(e => e.UpdatedAt).HasColumnName("Updated_at");

            entity.HasOne(d => d.ApplicantProfiles).WithMany(p => p.TblApplicantEducations)
                .HasForeignKey(d => d.ApplicantProfilesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_Applicant_Educations_Tbl_Applicant_Profiles");
        });

        modelBuilder.Entity<TblApplicantExperience>(entity =>
        {
            entity.HasKey(e => e.ApplicantExperiencesId);

            entity.ToTable("Tbl_Applicant_Experiences");

            entity.Property(e => e.ApplicantExperiencesId)
                .ValueGeneratedNever()
                .HasColumnName("Applicant_Experiences_Id");
            entity.Property(e => e.ApplicantProfilesId).HasColumnName("Applicant_Profiles_Id");
            entity.Property(e => e.CompanyName).HasColumnName("Company_Name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Created_at");
            entity.Property(e => e.CurrentlyWorking).HasColumnName("Currently_Working");
            entity.Property(e => e.EndDate).HasColumnName("End_Date");
            entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");
            entity.Property(e => e.JobType).HasColumnName("Job_Type");
            entity.Property(e => e.StartDate).HasColumnName("Start_Date");
            entity.Property(e => e.UpdatedAt).HasColumnName("Updated_at");

            entity.HasOne(d => d.ApplicantProfiles).WithMany(p => p.TblApplicantExperiences)
                .HasForeignKey(d => d.ApplicantProfilesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_Applicant_Experiences_Tbl_Applicant_Profiles");
        });

        modelBuilder.Entity<TblApplicantJobCategory>(entity =>
        {
            entity.HasKey(e => e.ApplicantJobCategoriesId);

            entity.ToTable("Tbl_Applicant_Job_Categories");

            entity.Property(e => e.ApplicantJobCategoriesId)
                .ValueGeneratedNever()
                .HasColumnName("Applicant_Job_Categories_Id");
            entity.Property(e => e.ApplicantProfilesId).HasColumnName("Applicant_Profiles_Id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Created_at");
            entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");
            entity.Property(e => e.JobCategoriesId).HasColumnName("Job_Categories_Id");
            entity.Property(e => e.UpdatedAt).HasColumnName("Updated_at");

            entity.HasOne(d => d.ApplicantProfiles).WithMany(p => p.TblApplicantJobCategories)
                .HasForeignKey(d => d.ApplicantProfilesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_Applicant_Job_Categories_Tbl_Applicant_Profiles");

            entity.HasOne(d => d.JobCategories).WithMany(p => p.TblApplicantJobCategories)
                .HasForeignKey(d => d.JobCategoriesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_Applicant_Job_Categories_Tbl_Job_Categories");
        });

        modelBuilder.Entity<TblApplicantProfile>(entity =>
        {
            entity.HasKey(e => e.ApplicantProfilesId);

            entity.ToTable("Tbl_Applicant_Profiles");

            entity.Property(e => e.ApplicantProfilesId)
                .ValueGeneratedNever()
                .HasColumnName("Applicant_Profiles_Id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Created_at");
            entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");
            entity.Property(e => e.LocationId).HasColumnName("Location_Id");
            entity.Property(e => e.UpdatedAt).HasColumnName("Updated_at");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Location).WithMany(p => p.TblApplicantProfiles)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_Applicant_Profiles_Tbl_Locations");

            entity.HasOne(d => d.User).WithMany(p => p.TblApplicantProfiles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_Applicant_Profiles_Tbl_Users");
        });

        modelBuilder.Entity<TblApplicantSkill>(entity =>
        {
            entity.HasKey(e => e.ApplicantSkillsId);

            entity.ToTable("Tbl_Applicant_Skills");

            entity.Property(e => e.ApplicantSkillsId)
                .ValueGeneratedNever()
                .HasColumnName("Applicant_Skills_Id");
            entity.Property(e => e.ApplicantProfilesId).HasColumnName("Applicant_Profiles_Id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Created_at");
            entity.Property(e => e.ExtraSkills).HasColumnName("Extra_Skills");
            entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");
            entity.Property(e => e.SkillsId).HasColumnName("Skills_Id");
            entity.Property(e => e.UpdatedAt).HasColumnName("Updated_at");

            entity.HasOne(d => d.ApplicantProfiles).WithMany(p => p.TblApplicantSkills)
                .HasForeignKey(d => d.ApplicantProfilesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_Applicant_Skills_Tbl_Applicant_Profiles");

            entity.HasOne(d => d.Skills).WithMany(p => p.TblApplicantSkills)
                .HasForeignKey(d => d.SkillsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_Applicant_Skills_Tbl_Skills");
        });

        modelBuilder.Entity<TblApplication>(entity =>
        {
            entity.HasKey(e => e.ApplicationsId);

            entity.ToTable("Tbl_Applications");

            entity.Property(e => e.ApplicationsId)
                .ValueGeneratedNever()
                .HasColumnName("Applications_Id");
            entity.Property(e => e.ApplicantProfilesId).HasColumnName("Applicant_Profiles_Id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Created_at");
            entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");
            entity.Property(e => e.JobsId).HasColumnName("Jobs_Id");
            entity.Property(e => e.ResumesId).HasColumnName("Resumes_Id");
            entity.Property(e => e.UpdatedAt).HasColumnName("Updated_at");

            entity.HasOne(d => d.ApplicantProfiles).WithMany(p => p.TblApplications)
                .HasForeignKey(d => d.ApplicantProfilesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_Applications_Tbl_Applicant_Profiles");

            entity.HasOne(d => d.Jobs).WithMany(p => p.TblApplications)
                .HasForeignKey(d => d.JobsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_Applications_Tbl_Jobs");

            entity.HasOne(d => d.Resumes).WithMany(p => p.TblApplications)
                .HasForeignKey(d => d.ResumesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_Applications_Tbl_Resumes");
        });

        modelBuilder.Entity<TblCompanyProfile>(entity =>
        {
            entity.HasKey(e => e.CompanyProfilesId);

            entity.ToTable("Tbl_Company_Profiles");

            entity.Property(e => e.CompanyProfilesId)
                .ValueGeneratedNever()
                .HasColumnName("Company_Profiles_Id");
            entity.Property(e => e.CompanyName).HasColumnName("Company_Name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Created_at");
            entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");
            entity.Property(e => e.LocationId).HasColumnName("Location_Id");
            entity.Property(e => e.UpdatedAt).HasColumnName("Updated_at");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Location).WithMany(p => p.TblCompanyProfiles)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_Company_Profiles_Tbl_Locations");

            entity.HasOne(d => d.User).WithMany(p => p.TblCompanyProfiles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_Company_Profiles_Tbl_Users");
        });

        modelBuilder.Entity<TblJob>(entity =>
        {
            entity.HasKey(e => e.JobsId);

            entity.ToTable("Tbl_Jobs");

            entity.Property(e => e.JobsId)
                .ValueGeneratedNever()
                .HasColumnName("Jobs_Id");
            entity.Property(e => e.CompanyProfilesId).HasColumnName("Company_Profiles_Id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Created_at");
            entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");
            entity.Property(e => e.JobCategoriesId).HasColumnName("Job_Categories_Id");
            entity.Property(e => e.LocationId).HasColumnName("Location_Id");
            entity.Property(e => e.NumOfPosts).HasColumnName("Num_Of_Posts");
            entity.Property(e => e.Requirements).HasColumnType("text");
            entity.Property(e => e.Salary).HasColumnType("decimal(20, 2)");
            entity.Property(e => e.UpdatedAt).HasColumnName("Updated_at");

            entity.HasOne(d => d.CompanyProfiles).WithMany(p => p.TblJobs)
                .HasForeignKey(d => d.CompanyProfilesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_Jobs_Tbl_Company_Profiles");

            entity.HasOne(d => d.JobCategories).WithMany(p => p.TblJobs)
                .HasForeignKey(d => d.JobCategoriesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_Jobs_Tbl_Job_Categories");

            entity.HasOne(d => d.Location).WithMany(p => p.TblJobs)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_Jobs_Tbl_Locations");
        });

        modelBuilder.Entity<TblJobCategory>(entity =>
        {
            entity.HasKey(e => e.JobCategoriesId);

            entity.ToTable("Tbl_Job_Categories");

            entity.Property(e => e.JobCategoriesId)
                .ValueGeneratedNever()
                .HasColumnName("Job_Categories_Id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Created_at");
            entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");
            entity.Property(e => e.UpdatedAt).HasColumnName("Updated_at");
        });

        modelBuilder.Entity<TblJobSkill>(entity =>
        {
            entity.HasKey(e => e.JobSkillsId);

            entity.ToTable("Tbl_Job_Skills");

            entity.Property(e => e.JobSkillsId)
                .ValueGeneratedNever()
                .HasColumnName("Job_Skills_Id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Created_at");
            entity.Property(e => e.ExtraSkills).HasColumnName("Extra_Skills");
            entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");
            entity.Property(e => e.JobsId).HasColumnName("Jobs_Id");
            entity.Property(e => e.SkillsId).HasColumnName("Skills_Id");
            entity.Property(e => e.UpdatedAt).HasColumnName("Updated_at");

            entity.HasOne(d => d.Jobs).WithMany(p => p.TblJobSkills)
                .HasForeignKey(d => d.JobsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_Job_Skills_Tbl_Jobs");

            entity.HasOne(d => d.Skills).WithMany(p => p.TblJobSkills)
                .HasForeignKey(d => d.SkillsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_Job_Skills_Tbl_Skills");
        });

        modelBuilder.Entity<TblLocation>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK_Tbl_locations");

            entity.ToTable("Tbl_Locations");

            entity.Property(e => e.LocationId)
                .ValueGeneratedNever()
                .HasColumnName("Location_Id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Created_at");
            entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");
            entity.Property(e => e.LocationName).HasColumnName("Location_Name");
            entity.Property(e => e.UpdatedAt).HasColumnName("Updated_at");
        });

        modelBuilder.Entity<TblResume>(entity =>
        {
            entity.HasKey(e => e.ResumesId);

            entity.ToTable("Tbl_Resumes");

            entity.Property(e => e.ResumesId)
                .ValueGeneratedNever()
                .HasColumnName("Resumes_Id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Created_at");
            entity.Property(e => e.FilePath).HasColumnName("File_Path");
            entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");
            entity.Property(e => e.UpdatedAt).HasColumnName("Updated_at");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.User).WithMany(p => p.TblResumes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_Resumes_Tbl_Users");
        });

        modelBuilder.Entity<TblReview>(entity =>
        {
            entity.HasKey(e => e.ReviewsId);

            entity.ToTable("Tbl_Reviews");

            entity.Property(e => e.ReviewsId)
                .ValueGeneratedNever()
                .HasColumnName("Reviews_Id");
            entity.Property(e => e.ApplicantProfilesId).HasColumnName("Applicant_Profiles_Id");
            entity.Property(e => e.Comments).HasColumnType("text");
            entity.Property(e => e.CompanyProfilesId).HasColumnName("Company_Profiles_Id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Created_at");
            entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");
            entity.Property(e => e.Ratings).HasColumnType("decimal(20, 2)");
            entity.Property(e => e.UpdatedAt).HasColumnName("Updated_at");

            entity.HasOne(d => d.ApplicantProfiles).WithMany(p => p.TblReviews)
                .HasForeignKey(d => d.ApplicantProfilesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_Reviews_Tbl_Applicant_Profiles");

            entity.HasOne(d => d.CompanyProfiles).WithMany(p => p.TblReviews)
                .HasForeignKey(d => d.CompanyProfilesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_Reviews_Tbl_Company_Profiles");
        });

        modelBuilder.Entity<TblRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK_Tbl_Role");

            entity.ToTable("Tbl_Roles");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("Role_Id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Created_at");
            entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");
            entity.Property(e => e.RoleName).HasColumnName("Role_Name");
            entity.Property(e => e.UpdatedAt).HasColumnName("Updated_at");
        });

        modelBuilder.Entity<TblSavedJob>(entity =>
        {
            entity.HasKey(e => e.SavedJobsId);

            entity.ToTable("Tbl_Saved_Jobs");

            entity.Property(e => e.SavedJobsId)
                .ValueGeneratedNever()
                .HasColumnName("Saved_Jobs_Id");
            entity.Property(e => e.ApplicantProfilesId).HasColumnName("Applicant_Profiles_Id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Created_at");
            entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");
            entity.Property(e => e.JobsId).HasColumnName("Jobs_Id");
            entity.Property(e => e.UpdatedAt).HasColumnName("Updated_at");

            entity.HasOne(d => d.ApplicantProfiles).WithMany(p => p.TblSavedJobs)
                .HasForeignKey(d => d.ApplicantProfilesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_Saved_Jobs_Tbl_Applicant_Profiles");

            entity.HasOne(d => d.Jobs).WithMany(p => p.TblSavedJobs)
                .HasForeignKey(d => d.JobsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_Saved_Jobs_Tbl_Jobs");
        });

        modelBuilder.Entity<TblSkill>(entity =>
        {
            entity.HasKey(e => e.SkillsId);

            entity.ToTable("Tbl_Skills");

            entity.Property(e => e.SkillsId)
                .ValueGeneratedNever()
                .HasColumnName("Skills_Id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Created_at");
            entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");
            entity.Property(e => e.SkillsName).HasColumnName("Skills_Name");
            entity.Property(e => e.UpdatedAt).HasColumnName("Updated_at");
        });

        modelBuilder.Entity<TblSocialMedium>(entity =>
        {
            entity.HasKey(e => e.SocialMediaId);

            entity.ToTable("Tbl_Social_Media");

            entity.Property(e => e.SocialMediaId)
                .ValueGeneratedNever()
                .HasColumnName("Social_Media_Id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Created_at");
            entity.Property(e => e.IsDelete).HasColumnName("Is_Delete");
            entity.Property(e => e.Link).HasColumnName("link");
            entity.Property(e => e.UpdatedAt).HasColumnName("Updated_at");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.User).WithMany(p => p.TblSocialMedia)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_Social_Media_Tbl_Users");
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
            entity.Property(e => e.UpdatedAt).HasColumnName("Updated_at");

            entity.HasOne(d => d.Role).WithMany(p => p.TblUsers)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_Users_Tbl_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

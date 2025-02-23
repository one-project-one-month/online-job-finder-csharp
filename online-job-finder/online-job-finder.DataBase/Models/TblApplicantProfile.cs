using System;
using System.Collections.Generic;

namespace online_job_finder.DataBase.Models;

public partial class TblApplicantProfile
{
    public Guid ApplicantProfilesId { get; set; }

    public Guid UserId { get; set; }

    public Guid LocationId { get; set; }

    public string FullName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Description { get; set; }

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDelete { get; set; }

    public virtual TblLocation Location { get; set; } = null!;

    public virtual ICollection<TblApplicantEducation> TblApplicantEducations { get; set; } = new List<TblApplicantEducation>();

    public virtual ICollection<TblApplicantExperience> TblApplicantExperiences { get; set; } = new List<TblApplicantExperience>();

    public virtual ICollection<TblApplicantJobCategory> TblApplicantJobCategories { get; set; } = new List<TblApplicantJobCategory>();

    public virtual ICollection<TblApplicantSkill> TblApplicantSkills { get; set; } = new List<TblApplicantSkill>();

    public virtual ICollection<TblApplication> TblApplications { get; set; } = new List<TblApplication>();

    public virtual ICollection<TblReview> TblReviews { get; set; } = new List<TblReview>();

    public virtual ICollection<TblSavedJob> TblSavedJobs { get; set; } = new List<TblSavedJob>();

    public virtual TblUser User { get; set; } = null!;
}

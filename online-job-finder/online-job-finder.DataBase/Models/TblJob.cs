using System;
using System.Collections.Generic;

namespace online_job_finder.DataBase.Models;

public partial class TblJob
{
    public Guid JobsId { get; set; }

    public Guid CompanyProfilesId { get; set; }

    public Guid JobCategoriesId { get; set; }

    public Guid LocationId { get; set; }

    public string Title { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? Requirements { get; set; }

    public int NumOfPosts { get; set; }

    public decimal Salary { get; set; }

    public string Address { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDelete { get; set; }

    public virtual TblCompanyProfile CompanyProfiles { get; set; } = null!;

    public virtual TblJobCategory JobCategories { get; set; } = null!;

    public virtual TblLocation Location { get; set; } = null!;

    public virtual ICollection<TblApplication> TblApplications { get; set; } = new List<TblApplication>();

    public virtual ICollection<TblJobSkill> TblJobSkills { get; set; } = new List<TblJobSkill>();

    public virtual ICollection<TblSavedJob> TblSavedJobs { get; set; } = new List<TblSavedJob>();
}

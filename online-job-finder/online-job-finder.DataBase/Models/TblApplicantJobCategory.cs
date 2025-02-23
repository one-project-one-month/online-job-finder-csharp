using System;
using System.Collections.Generic;

namespace online_job_finder.DataBase.Models;

public partial class TblApplicantJobCategory
{
    public Guid ApplicantJobCategoriesId { get; set; }

    public Guid ApplicantProfilesId { get; set; }

    public Guid JobCategoriesId { get; set; }

    public string? Reasons { get; set; }

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDelete { get; set; }

    public virtual TblApplicantProfile ApplicantProfiles { get; set; } = null!;

    public virtual TblJobCategory JobCategories { get; set; } = null!;
}

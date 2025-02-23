using System;
using System.Collections.Generic;

namespace online_job_finder.DataBase.Models;

public partial class TblJobCategory
{
    public Guid JobCategoriesId { get; set; }

    public string Industry { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDelete { get; set; }

    public virtual ICollection<TblApplicantJobCategory> TblApplicantJobCategories { get; set; } = new List<TblApplicantJobCategory>();

    public virtual ICollection<TblJob> TblJobs { get; set; } = new List<TblJob>();
}

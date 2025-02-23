using System;
using System.Collections.Generic;

namespace online_job_finder.DataBase.Models;

public partial class TblSavedJob
{
    public Guid SavedJobsId { get; set; }

    public Guid JobsId { get; set; }

    public Guid ApplicantProfilesId { get; set; }

    public string Status { get; set; } = null!;

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDelete { get; set; }

    public virtual TblApplicantProfile ApplicantProfiles { get; set; } = null!;

    public virtual TblJob Jobs { get; set; } = null!;
}

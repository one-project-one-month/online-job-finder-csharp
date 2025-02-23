using System;
using System.Collections.Generic;

namespace online_job_finder.DataBase.Models;

public partial class TblLocation
{
    public Guid LocationId { get; set; }

    public string LocationName { get; set; } = null!;

    public string? Description { get; set; }

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDelete { get; set; }

    public virtual ICollection<TblApplicantProfile> TblApplicantProfiles { get; set; } = new List<TblApplicantProfile>();

    public virtual ICollection<TblCompanyProfile> TblCompanyProfiles { get; set; } = new List<TblCompanyProfile>();

    public virtual ICollection<TblJob> TblJobs { get; set; } = new List<TblJob>();
}

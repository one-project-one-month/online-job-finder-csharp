using System;
using System.Collections.Generic;

namespace online_job_finder.DataBase.Models;

public partial class TblCompanyProfile
{
    public Guid CompanyProfilesId { get; set; }

    public Guid UserId { get; set; }

    public Guid LocationId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Website { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Description { get; set; }

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDelete { get; set; }

    public virtual TblLocation Location { get; set; } = null!;

    public virtual ICollection<TblJob> TblJobs { get; set; } = new List<TblJob>();

    public virtual ICollection<TblReview> TblReviews { get; set; } = new List<TblReview>();

    public virtual TblUser User { get; set; } = null!;
}

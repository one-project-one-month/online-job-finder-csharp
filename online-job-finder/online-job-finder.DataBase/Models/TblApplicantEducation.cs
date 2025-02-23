using System;
using System.Collections.Generic;

namespace online_job_finder.DataBase.Models;

public partial class TblApplicantEducation
{
    public Guid ApplicantEducationsId { get; set; }

    public Guid ApplicantProfilesId { get; set; }

    public string SchoolName { get; set; } = null!;

    public string Degree { get; set; } = null!;

    public string FieldOfStudy { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool StillAttending { get; set; }

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDelete { get; set; }

    public virtual TblApplicantProfile ApplicantProfiles { get; set; } = null!;
}

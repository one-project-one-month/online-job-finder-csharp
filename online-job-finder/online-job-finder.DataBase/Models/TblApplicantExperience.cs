using System;
using System.Collections.Generic;

namespace online_job_finder.DataBase.Models;

public partial class TblApplicantExperience
{
    public Guid ApplicantExperiencesId { get; set; }

    public Guid ApplicantProfilesId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string JobType { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool CurrentlyWorking { get; set; }

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDelete { get; set; }

    public virtual TblApplicantProfile ApplicantProfiles { get; set; } = null!;
}

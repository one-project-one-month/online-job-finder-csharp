using System;
using System.Collections.Generic;

namespace online_job_finder.DataBase.Models;

public partial class TblApplicantSkill
{
    public Guid ApplicantSkillsId { get; set; }

    public Guid ApplicantProfilesId { get; set; }

    public Guid SkillsId { get; set; }

    public string? ExtraSkills { get; set; }

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDelete { get; set; }

    public virtual TblApplicantProfile ApplicantProfiles { get; set; } = null!;

    public virtual TblSkill Skills { get; set; } = null!;
}

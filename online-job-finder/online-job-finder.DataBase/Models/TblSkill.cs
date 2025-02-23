using System;
using System.Collections.Generic;

namespace online_job_finder.DataBase.Models;

public partial class TblSkill
{
    public Guid SkillsId { get; set; }

    public string SkillsName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDelete { get; set; }

    public virtual ICollection<TblApplicantSkill> TblApplicantSkills { get; set; } = new List<TblApplicantSkill>();

    public virtual ICollection<TblJobSkill> TblJobSkills { get; set; } = new List<TblJobSkill>();
}

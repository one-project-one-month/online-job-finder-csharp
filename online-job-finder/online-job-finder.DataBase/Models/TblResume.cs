using System;
using System.Collections.Generic;

namespace online_job_finder.DataBase.Models;

public partial class TblResume
{
    public Guid ResumesId { get; set; }

    public Guid UserId { get; set; }

    public string FilePath { get; set; } = null!;

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDelete { get; set; }

    public virtual ICollection<TblApplication> TblApplications { get; set; } = new List<TblApplication>();

    public virtual TblUser User { get; set; } = null!;
}

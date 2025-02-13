using System;
using System.Collections.Generic;

namespace online_job_finder.DataBase.Models;

public partial class TblRole
{
    public Guid RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDelete { get; set; }

    public virtual ICollection<TblUser> TblUsers { get; set; } = new List<TblUser>();
}

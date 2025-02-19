using System;
using System.Collections.Generic;

namespace online_job_finder.DataBase.Models;

public partial class TblUser
{
    public Guid UserId { get; set; }

    public string Username { get; set; } = null!;

    public string ProfilePhoto { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public Guid RoleId { get; set; }

    public bool IsInformationCompleted { get; set; }

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDelete { get; set; }

    public virtual TblRole Role { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace online_job_finder.DataBase.Models;

public partial class TblSocialMedium
{
    public Guid SocialMediaId { get; set; }

    public Guid UserId { get; set; }

    public string Link { get; set; } = null!;

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDelete { get; set; }

    public virtual TblUser User { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace online_job_finder.DataBase.Models;

public partial class CompanyProfile
{
    public Guid Id { get; set; }

    public string? UserId { get; set; }

    public string? CompanyName { get; set; }

    public string? Phone { get; set; }

    public string? Website { get; set; }

    public string? Address { get; set; }

    public string? LocationId { get; set; }

    public string? Description { get; set; }

    public int? Version { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

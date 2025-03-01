using System;
using System.Collections.Generic;

namespace online_job_finder.DataBase.Models;

public partial class TblUser
{
    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }

    public string Username { get; set; } = null!;

    public string ProfilePhoto { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiryTime { get; set; }

    public bool IsInformationCompleted { get; set; }

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDelete { get; set; }

    public virtual TblRole Role { get; set; } = null!;

    public virtual ICollection<TblApplicantProfile> TblApplicantProfiles { get; set; } = new List<TblApplicantProfile>();

    public virtual ICollection<TblCompanyProfile> TblCompanyProfiles { get; set; } = new List<TblCompanyProfile>();

    public virtual ICollection<TblResume> TblResumes { get; set; } = new List<TblResume>();

    public virtual ICollection<TblSocialMedium> TblSocialMedia { get; set; } = new List<TblSocialMedium>();
}

namespace online_job_finder.DataBase.Models;

public partial class TblReview
{
    public Guid ReviewsId { get; set; }

    public Guid CompanyProfilesId { get; set; }

    public Guid ApplicantProfilesId { get; set; }

    public decimal Ratings { get; set; }

    public string? Comments { get; set; }

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDelete { get; set; }

    public virtual TblApplicantProfile ApplicantProfiles { get; set; } = null!;

    public virtual TblCompanyProfile CompanyProfiles { get; set; } = null!;
}

namespace online_job_finder.Domain.ViewModels;

public class ApplicantProfileViewModel
{
    public Guid ApplicantProfilesId { get; set; }

    public Guid UserId { get; set; }

    public Guid LocationId { get; set; }

    public string FullName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Description { get; set; }

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDelete { get; set; }

    //public virtual TblLocation Location { get; set; } = null!;

    public virtual UsersViewModels User { get; set; } = null!;
}

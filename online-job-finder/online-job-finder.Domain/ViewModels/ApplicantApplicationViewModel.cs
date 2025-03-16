namespace online_job_finder.Domain.ViewModels;

public class ApplicantApplicationViewModel
{
    public Guid JobsId { get; set; }

    public Guid ApplicantProfilesId { get; set; }

    public Guid ResumesId { get; set; }

    public string Status { get; set; } = null!;

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

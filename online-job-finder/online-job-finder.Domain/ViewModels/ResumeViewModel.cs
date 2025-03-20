namespace online_job_finder.Domain.ViewModels;

public class ResumeViewModel
{
    public Guid UserId { get; set; }
    public IFormFile resumeFile { get; set; } = null!;

    public string FilePath { get; set; } = null!;
    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

namespace online_job_finder.Domain.ViewModels;

public class JobCategoryViewModels
{
    public string Industry { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

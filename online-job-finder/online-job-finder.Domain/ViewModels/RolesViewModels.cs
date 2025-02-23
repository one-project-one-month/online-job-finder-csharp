namespace online_job_finder.Domain.ViewModels;

public class RolesViewModels
{
    public string RoleName { get; set; } = null!;

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

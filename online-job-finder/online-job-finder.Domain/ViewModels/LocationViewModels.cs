using online_job_finder.DataBase.Models;

namespace online_job_finder.Domain.ViewModels;

public class LocationViewModels
{
    public string LocationName { get; set; } = null!;

    public string? Description { get; set; }

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

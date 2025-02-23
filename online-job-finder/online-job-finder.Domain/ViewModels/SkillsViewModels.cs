using online_job_finder.DataBase.Models;

namespace online_job_finder.Domain.ViewModels;

public class SkillsViewModels
{
    public string SkillsName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

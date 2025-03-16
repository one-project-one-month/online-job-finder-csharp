namespace online_job_finder.Domain.ViewModels;

public class ExperiencesViewModels
{
    [JsonIgnore]
    public Guid ApplicantProfilesId { get; set; }
    public string CompanyName { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string JobType { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool CurrentlyWorking { get; set; }
    public string? Description { get; set; }
    [JsonIgnore]
    public int Version { get; set; }
    [JsonIgnore]
    public DateTime CreatedAt { get; set; }
    [JsonIgnore]
    public DateTime? UpdatedAt { get; set; }
}


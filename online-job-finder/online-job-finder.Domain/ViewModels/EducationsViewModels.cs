namespace online_job_finder.Domain.ViewModels;

public class EducationsViewModels
{
    [JsonIgnore]
    public Guid ApplicantProfilesId { get; set; }
    public string SchoolName { get; set; } = null!;
    public string Degree { get; set; } = null!;
    public string FieldOfStudy { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool StillAttending { get; set; }
    public string? Description { get; set; }
    [JsonIgnore]
    public int Version { get; set; }
    [JsonIgnore]
    public DateTime CreatedAt { get; set; }
    [JsonIgnore]
    public DateTime? UpdatedAt { get; set; }
}

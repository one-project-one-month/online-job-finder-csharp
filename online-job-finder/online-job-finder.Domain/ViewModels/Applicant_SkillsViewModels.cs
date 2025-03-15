namespace online_job_finder.Domain.ViewModels;

public class Applicant_SkillsViewModels
{
    [JsonIgnore]
    public Guid ApplicantProfilesId { get; set; }

    //public Guid SkillsId { get; set; }
    public List<Guid> SkillsIds { get; set; } = new List<Guid>();

    public string? ExtraSkills { get; set; }
    [JsonIgnore]
    public int Version { get; set; }
    [JsonIgnore]
    public DateTime CreatedAt { get; set; }
    [JsonIgnore]
    public DateTime? UpdatedAt { get; set; }
}

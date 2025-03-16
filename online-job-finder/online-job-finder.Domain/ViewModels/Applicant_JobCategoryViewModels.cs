namespace online_job_finder.Domain.ViewModels;

public class Applicant_JobCategoryViewModels
{
    [JsonIgnore]
    public Guid ApplicantProfilesId { get; set; }
    public List<Guid> JobCategoriesIds { get; set; } = new List<Guid>();
    //public string? JobCategories { get; set; } 
    public string? Reasons { get; set; }
    [JsonIgnore]
    public int Version { get; set; }
    [JsonIgnore]
    public DateTime CreatedAt { get; set; }
    [JsonIgnore]
    public DateTime? UpdatedAt { get; set; }

}

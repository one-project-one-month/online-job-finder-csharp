namespace online_job_finder.Domain.ViewModels;

public class ReviewViewModels
{
    [JsonIgnore]
    public Guid CompanyProfilesId { get; set; }
    [JsonIgnore]
    public Guid ApplicantProfilesId { get; set; }

    public decimal Ratings { get; set; }

    public string? Comments { get; set; }

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsDelete { get; set; }
}

namespace online_job_finder.Domain.ViewModels;

public class ReviewViewModels
{
    [JsonIgnore]
    public Guid ApplicantProfilesId { get; set; }
    //[JsonIgnore]
    public Guid CompanyProfilesId { get; set; }
    [Required]
    [Range(0, 5, ErrorMessage = "Ratings must be between 0 and 5.")]
    public decimal Ratings { get; set; }

    public string? Comments { get; set; }
    [JsonIgnore]
    public int Version { get; set; }
    [JsonIgnore]
    public DateTime CreatedAt { get; set; }
    [JsonIgnore]
    public DateTime? UpdatedAt { get; set; }
}

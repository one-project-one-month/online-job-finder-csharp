namespace online_job_finder.Domain.ViewModels
{
    public class JobsViewModels
    {
        [JsonIgnore]
        public Guid JobsId { get; set; }

        public string Title { get; set; }
        public Guid CompanyProfilesId { get; set; }
        public Guid LocationId { get; set; }
        public Guid JobCategoryId { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string? Requirements { get; set; }
        public string? Industry { get; set; }
        public int NumOfPosts { get; set; }
        public decimal Salary { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public int Version { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDelete { get; set; }

    }
    public class JobSearchParameters
    {
        public string? Title { get; set; }
        public string? Location { get; set; }
        public string? Industry { get; set; }
        public string? Type { get; set; }

    }
}

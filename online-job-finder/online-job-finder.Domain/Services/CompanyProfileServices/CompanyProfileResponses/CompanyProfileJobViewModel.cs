using System;

namespace online_job_finder.Domain.Services.CompanyProfileServices.CompanyProfileResponses;

public class CompanyProfileJobViewModel
{
    public Guid JobsId { get; set; }
    public Guid CompanyProfilesId { get; set; }
    public Guid JobCategoriesId { get; set; }
    public Guid LocationId { get; set; }
    public string Title { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? Requirements { get; set; }
    public int NumOfPosts { get; set; }
    public decimal Salary { get; set; }
    public string Address { get; set; } = null!;
    public string Status { get; set; } = null!;
    public int Version { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public static string GetValidType(string type)
    {
        var validTypes = new[] { "FullTime", "PartTime", "Contract", "Temporary" }; // Replace with actual allowed values
        if (Array.Exists(validTypes, t => t.Equals(type, StringComparison.OrdinalIgnoreCase)))
        {
            return type;
        }
        throw new ArgumentException($"Invalid job type: {type}. Allowed values are: {string.Join(", ", validTypes)}");
    }
}

public class ApplicationViewModel
{
    public Guid ApplicationsId { get; set; }
    public Guid ApplicantProfilesId { get; set; }
    public Guid JobsId { get; set; }
    public Guid ResumesId { get; set; }
    public string Status { get; set; } = "Pending";
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class ReviewViewModel
{
    public Guid ReviewsId { get; set; }
    public Guid CompanyProfilesId { get; set; }
    public Guid ApplicantProfilesId { get; set; }
    public decimal Ratings { get; set; }
    public string Comments { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public static class JobStatus
{
    public const string Open = "Open";
    public const string Close = "Close";

    public static string GetValidStatus(string status)
    {
        return status.ToUpper() switch
        {
            "ACTIVE" => Open,
            "INACTIVE" => Close,
            _ => Open
        };
    }
}

public static class ApplicationStatus
{
    public const string Pending = "Pending";
    public const string Seen = "Seen";
    public const string Accepted = "Accepted";
    public const string Rejected = "Rejected";

    public static string GetValidStatus(string status)
    {
        return status.ToUpper() switch
        {
            "PENDING" => Pending,
            "SEEN" => Seen,
            "ACCEPTED" => Accepted,
            "REJECTED" => Rejected,
            _ => Pending // Default to Pending if invalid status provided
        };
    }

    public static bool IsValidStatus(string status)
    {
        return status is Pending or Seen or Accepted or Rejected;
    }
}
// ([Status]='Rejected' OR [Status]='Accepted' OR [Status]='Seen' OR [Status]='Pending')

public static class JobType
{
    public const string Remote = "Remote";
    public const string OnSite = "OnSite";
    public const string Hybrid = "Hybrid";

    public static string GetValidType(string type)
    {
        return type.ToUpper() switch
        {
            "REMOTE" => Remote,
            "ONSITE" => OnSite,
            "HYBRID" => Hybrid,
            _ => OnSite
        };
    }
}


using System.Text.Json.Serialization;

namespace online_job_finder.Domain.ViewModels;

public class UsersViewModels
{
    //[JsonIgnore]
    public Guid RoleId { get; set; }

    public string Username { get; set; } = null!;

    public string ProfilePhoto { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public bool IsInformationCompleted { get; set; }

    [JsonIgnore]
    public int Version { get; set; }
    [JsonIgnore]
    public DateTime CreatedAt { get; set; }
    [JsonIgnore]
    public DateTime? UpdatedAt { get; set; }

}

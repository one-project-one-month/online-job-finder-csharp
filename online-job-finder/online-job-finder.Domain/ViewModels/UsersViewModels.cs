namespace online_job_finder.Domain.ViewModels;

public class UsersViewModels
{
    //[JsonIgnore]
    public Guid RoleId { get; set; }

    public string Username { get; set; } = null!;

    public string ProfilePhoto { get; set; } = null!;

    public string Email { get; set; } = null!;

    [Required]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{6,16}$",
        ErrorMessage = "Password must be 6-16 characters, with at least one uppercase letter," +
        " one lowercase letter, one number, and one special character.")]
    public string PasswordHash { get; set; } = null!;

    public bool IsInformationCompleted { get; set; }

    //[JsonIgnore]
    public int Version { get; set; }
    //[JsonIgnore]
    public DateTime CreatedAt { get; set; }
    //[JsonIgnore]
    public DateTime? UpdatedAt { get; set; }
}

// Change Pin Request
public class ChangePasswordRequest
{
    [Required(ErrorMessage = "Old Password is required.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{6,16}$",
        ErrorMessage = "Password must be 6-16 characters, with at least one uppercase letter," +
        " one lowercase letter, one number, and one special character.")]
    public required string OldPassword { get; set; }

    [Required(ErrorMessage = "New Password is required.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{6,16}$",
        ErrorMessage = "Password must be 6-16 characters, with at least one uppercase letter," +
        " one lowercase letter, one number, and one special character.")]
    public required string NewPassword { get; set; }
}

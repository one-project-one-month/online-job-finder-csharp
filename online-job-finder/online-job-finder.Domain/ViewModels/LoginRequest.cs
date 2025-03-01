using System.ComponentModel.DataAnnotations;

namespace online_job_finder.Domain.ViewModels;

// Login Request
public class LoginRequest
{
    [Required(ErrorMessage = "Username is required.")]
    public required string Username { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(16, MinimumLength = 1, ErrorMessage = "Password must be 16 characters and lower than this.")]
    public required string Password { get; set; }
}

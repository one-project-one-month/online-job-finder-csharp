namespace online_job_finder.Domain.Services.UsersServices;

public class TokenResponse
{
    public required string AccessToken { get; set; } = string.Empty;
    public required string RefreshToken { get; set; } = string.Empty;
}

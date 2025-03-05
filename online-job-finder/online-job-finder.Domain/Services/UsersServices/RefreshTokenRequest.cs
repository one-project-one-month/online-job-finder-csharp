namespace online_job_finder.Domain.Services.UsersServices;

public class RefreshTokenRequest
{
    public Guid UserId { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
}

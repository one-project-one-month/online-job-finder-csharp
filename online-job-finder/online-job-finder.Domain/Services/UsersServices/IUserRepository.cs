using online_job_finder.Domain.ViewModels;

namespace online_job_finder.Domain.Services.UsersServices;

public interface IUserRepository
{
    Task<UsersViewModels?> RegisterAsync(UsersViewModels request);
    Task<TokenResponse?> LoginAsync(LoginRequest request);
    Task<TokenResponse?> RefreshTokenAsync(RefreshTokenRequest refreshToken);

    //CRUD
    List<UsersViewModels> GetUsers();
    UsersViewModels? GetUser(string id);
    //Task CreateUser(object request);
    UsersViewModels? UpdateUser(string id, UsersViewModels request);
    UsersViewModels? PatchUser(string id, UsersViewModels request);
    bool? DeleteUser(string id);
}

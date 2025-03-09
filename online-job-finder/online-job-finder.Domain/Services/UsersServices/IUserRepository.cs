namespace online_job_finder.Domain.Services.UsersServices;

public interface IUserRepository
{
    Task<UsersViewModels?> RegisterAsync(UsersViewModels request);
    Task<TokenResponse?> LoginAsync(LoginRequest request);
    Task<TokenResponse?> RefreshTokenAsync(RefreshTokenRequest refreshToken);
    Task<UsersViewModels?> ChangePassword(string id, ChangePasswordRequest request);

    //CRUD
    Task<List<UsersViewModels>> GetUsers();
    Task<UsersViewModels?> GetUser(string id);
    //Task CreateUser(object request);
    Task<UsersViewModels?> UpdateUser(string id, UsersViewModels request);
    Task<UsersViewModels?> PatchUser(string id, UsersViewModels request);
    Task<bool?> DeleteUser(string id);
}

namespace online_job_finder.Api.Controllers.Endpoints;


[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserRepository _userRepository) : ControllerBase
{

    #region Authentication & Authorization

    [HttpPost("/api/auth/signup")]
    public async Task<ActionResult<UsersViewModels>> Register(UsersViewModels request)
    {
        var items = await _userRepository.RegisterAsync(request);

        if (items is null)
        {
            return BadRequest("Username already exists.");
        }

        return Ok(items);
    }

    [HttpPost("/api/auth/signin")]
    public async Task<ActionResult<TokenResponse>> Login(LoginRequest request)
    {
        var result = await _userRepository.LoginAsync(request);

        if (result is null)
        {
            return Unauthorized("Invalid username or password.");
        }

        return Ok(result);
    }

    [HttpPost("/api/auth/refresh_token")]
    public async Task<ActionResult<TokenResponse>> RefreshToken(RefreshTokenRequest request)
    {
        var result = await _userRepository.RefreshTokenAsync(request);
        if (result is null || result.AccessToken is null || result.RefreshToken is null)
        {
            return Unauthorized("Invalid refresh token.");
        }
        return Ok(result);
    }

    [HttpPost("/api/auth/password/Change")]
    public async Task<ActionResult<UsersViewModels>> ChangePassword(string id, ChangePasswordRequest request)
    {
        if (request.OldPassword == request.NewPassword)
        {
            return BadRequest("Your new password should not same your old password.");
        }

        var items = await _userRepository.ChangePassword(id, request);

        if (items is null)
        {
            return BadRequest("Your old password was not incorrect.");
        }

        return Ok(items);
    }

    [Authorize(Roles = "Applicants")]
    [HttpGet("applicants_test")]
    public IActionResult AuthenticatedOnlyEndpoint()
    {
        return Ok("You are authenticated as Applicants.");
    }

    [Authorize(Roles = "Admins,Company,Applicants")]
    [HttpGet("all_only_test")]
    public IActionResult AdminOnlyEndpoint()
    {
        return Ok("You are authenticated as all.");
    }

    [Authorize(Roles = "Recruiters")]
    [HttpGet("recruiters_only_test")]
    public IActionResult CompanyOnlyEndpoint()
    {
        return Ok("You are authenticated as Recruiters.");
    }

    

    #endregion


    #region CRUD

    [Authorize(Roles = "Admins")]
    [HttpGet("/api/admins/users/getusers")]
    public async Task<IActionResult> GetUsers()
    {
        var items = await _userRepository.GetUsers();

        return Ok(items);
    }

    [Authorize(Roles = "Admins")]
    [HttpGet("/api/admins/users/getuser")]
    public async Task<IActionResult> GetUser(string id)
    {
        var items = await _userRepository.GetUser(id);

        return Ok(items);
    }

    [Authorize(Roles = "Admins")]
    [HttpPut("/api/admins/users/updateuser")]
    public async Task<IActionResult> UpdateUser(string id, UsersViewModels request)
    {
        var item = await _userRepository.UpdateUser(id, request);

        if (item is null)
        {
            return BadRequest("Don`t have data");
        }
        return Ok(item);
    }

    [Authorize(Roles = "Admins")]
    [HttpPatch("/api/admins/users/patchuser")]
    public async Task<IActionResult> PatchUser(string id, UsersViewModels request)
    {
        var item = await _userRepository.PatchUser(id, request);

        if (item is null)
        {
            return BadRequest("Don`t have data");
        }
        return Ok(item);
    }

    [Authorize(Roles = "Admins")]
    [HttpDelete("/api/admins/users/deleteuser")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var item = await _userRepository.DeleteUser(id);

        if (item is null)
        {
            return BadRequest("Don`t have data");
        }
        return Ok("Deleting success");
    }

    #endregion

}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using online_job_finder.DataBase.Models;
using online_job_finder.Domain.Services.UsersServices;
using online_job_finder.Domain.ViewModels;

namespace online_job_finder.Api.Controllers.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserRepository _userRepository) : ControllerBase
    {

        #region Authentication & Authorization

        [HttpPost("/api/auth/signup")]
        public async Task<ActionResult<TblUser>> Register(UsersViewModels request)
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

        [HttpPost("Refresh-Token")]
        public async Task<ActionResult<TokenResponse>> RefreshToken(RefreshTokenRequest request)
        {
            var result = await _userRepository.RefreshTokenAsync(request);
            if (result is null || result.AccessToken is null || result.RefreshToken is null)
            {
                return Unauthorized("Invalid refresh token.");
            }
            return Ok(result);
        }

        [Authorize(Roles = "Applicants")]
        [HttpGet("Applicants-Test")]
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            return Ok("You are authenticated as Applicants.");
        }

        [Authorize(Roles = "Admins,Company,Applicants")]
        [HttpGet("All-Only-Test")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("You are authenticated as all.");
        }

        [Authorize(Roles = "Company")]
        [HttpGet("Company-Only-Test")]
        public IActionResult CompanyOnlyEndpoint()
        {
            return Ok("You are authenticated as Company.");
        }

        //[HttpPost("/api/auth/password/Change")]
        //public async Task<ActionResult<TokenResponseDto>> ChangePasswordAsync(UsersViewModels request)
        //{
        //    var items = await _userRepository.RegisterAsync(request);

        //    return Ok(items);
        //}



        #endregion


        #region CRUD

        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            var items = _userRepository.GetUsers();

            return Ok(items);
        }


        [HttpGet("GetUser/{id}")]
        public IActionResult GetUser(string id)
        {
            var items = _userRepository.GetUser(id);

            return Ok(items);
        }


        [HttpPut("UpdateUser/{id}")]
        public IActionResult UpdateUser(string id, UsersViewModels request)
        {

            var item = _userRepository.UpdateUser(id, request);

            // need to write reponse & request

            if (item is null)
            {
                return BadRequest("Don`t have data");
            }
            return Ok(item);
        }


        [HttpPatch("PatchUser/{id}")]
        public IActionResult PatchUser(string id, UsersViewModels request)
        {

            var item = _userRepository.PatchUser(id, request);

            // need to write reponse & request

            if (item is null)
            {
                return BadRequest("Don`t have data");
            }
            return Ok(item);
        }


        [HttpDelete("DeleteUser/{id}")]
        public IActionResult DeleteUser(string id)
        {
            var item = _userRepository.DeleteUser(id);

            if (item is null)
            {
                return BadRequest("Don`t have data");
            }
            return Ok("Deleting success");
        }
        #endregion

    }



    // Change Pin Request
    //public class ChangePinRequest
    //{
    //    [Required(ErrorMessage = "Old PIN is required.")]
    //    [StringLength(6, MinimumLength = 6, ErrorMessage = "Old PIN must be exactly 6 digits.")]
    //    [RegularExpression(@"^\d{6}$", ErrorMessage = "Old PIN must contain only numbers.")]
    //    public required string OldPin { get; set; }

    //    [Required(ErrorMessage = "New PIN is required.")]
    //    [StringLength(6, MinimumLength = 6, ErrorMessage = "New PIN must be exactly 6 digits.")]
    //    [RegularExpression(@"^\d{6}$", ErrorMessage = "New PIN must contain only numbers.")]
    //    public required string NewPin { get; set; }

    //    //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    //    //{
    //    //    if (OldPin == NewPin)
    //    //    {
    //    //        yield return new ValidationResult("Old PIN and New PIN cannot be the same.", new[] { nameof(NewPin) });
    //    //    }
    //    //}
    //}
}

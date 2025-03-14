using System.IdentityModel.Tokens.Jwt;

namespace online_job_finder.Api.Controllers.Endpoints;

[Authorize(Roles = "Applicants")]
[Route("api/me/[controller]")]
[ApiController]
public class EducationsController : ControllerBase
{
    private readonly IApplicant_EducationsRepository _applicant_EducationsRepository;
    private const string claimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

    public EducationsController()
    {
        _applicant_EducationsRepository = new Applicant_EducationsRepository();
    }

    [HttpGet("geteducations")]
    public async Task<IActionResult> GetApplicant_Educations()
    {
        var items = await _applicant_EducationsRepository.GetApplicant_Educations();

        return Ok(items);
    }

    [HttpGet("geteducation")]
    public async Task<IActionResult> GetApplicant_Education(string id)
    {
        var items = await _applicant_EducationsRepository.GetApplicant_Education(id);

        return Ok(items);
    }

    [HttpPost("createeducation")]
    public async Task<IActionResult> CreateApplicant_Education(EducationsViewModels models)
    {
        // Retrieve the Bearer token from the Authorization header
        var authorizationHeader = Request.Headers["Authorization"].ToString();

        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
        {
            return Unauthorized("Bearer token is missing or invalid.");
        }

        var token = authorizationHeader.Substring("Bearer ".Length).Trim();

        // Decode the token to get the user ID (it's a JWT token)
        var userId = GetUserIdFromToken(token);

        if (userId == null)
        {
            return Unauthorized("Invalid token or NameIdentifier not found.");
        }

        var items = await _applicant_EducationsRepository.CreateApplicant_Education(models, userId);

        return Ok(items);
    }

    [HttpPut("updateeducation")]
    public async Task<IActionResult> UpdateApplicant_Education(string id, EducationsViewModels models)
    {

        var item = await _applicant_EducationsRepository.UpdateApplicant_Education(id, models);

        if (item is null)
        {
            return BadRequest("Don`t have data");
        }
        return Ok(item);
    }

    [HttpPatch("patcheducation")]
    public async Task<IActionResult> PatchApplicant_Education(string id, EducationsViewModels models)
    {

        var item = await _applicant_EducationsRepository.PatchApplicant_Education(id, models);

        if (item is null)
        {
            return BadRequest("Don`t have data");
        }
        return Ok(item);
    }

    [HttpDelete("deleteeducation")]
    public async Task<IActionResult> DeleteApplicant_Education(string id)
    {
        var item = await _applicant_EducationsRepository.DeleteApplicant_Education(id);
        
        if (item is null)
        {
            return BadRequest("Don`t have data");
        }
        return Ok("Deleting success");
    }

    // Method to decode the JWT token and extract the user ID
    private string? GetUserIdFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jsonToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

        if (jsonToken == null) return null;

        // The user ID claim is stored in "nameIdentifier"
        var nameIdentifier = jsonToken?.Claims
            .FirstOrDefault(c => c.Type == claimType)?.Value;

        if (string.IsNullOrEmpty(nameIdentifier))
        {
            nameIdentifier = null;
        }

        return nameIdentifier;
    }
}

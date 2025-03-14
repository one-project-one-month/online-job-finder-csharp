using System.IdentityModel.Tokens.Jwt;

namespace online_job_finder.Api.Controllers.Endpoints;

[Authorize(Roles = "Applicants")]
[Route("api/me/[controller]")]
[ApiController]
public class ExperiencesController : ControllerBase
{
    private readonly IApplicant_ExperiencesRepository _applicant_ExperiencesRepository;
    private const string claimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

    public ExperiencesController()
    {
        _applicant_ExperiencesRepository = new Applicant_ExperiencesRepository();
    }

    [HttpGet("getexperiences")]
    public async Task<IActionResult> GetApplicant_Experiences()
    {
        var items = await _applicant_ExperiencesRepository.GetApplicant_Experiences();

        return Ok(items);
    }

    [HttpGet("getexperience")]
    public async Task<IActionResult> GetApplicant_Experience(string id)
    {
        var items = await _applicant_ExperiencesRepository.GetApplicant_Experience(id);

        return Ok(items);
    }

    [HttpPost("createexperience")]
    public async Task<IActionResult> CreateApplicant_Experience(ExperiencesViewModels models)
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

        var items = await _applicant_ExperiencesRepository.CreateApplicant_Experience(models, userId);

        return Ok(items);
    }

    [HttpPut("updateexperience")]
    public async Task<IActionResult> UpdateApplicant_Experience(string id, ExperiencesViewModels models)
    {

        var item = await _applicant_ExperiencesRepository.UpdateApplicant_Experience(id, models);

        if (item is null)
        {
            return BadRequest("Don`t have data");
        }
        return Ok(item);
    }

    [HttpPatch("patchexperience")]
    public async Task<IActionResult> PatchApplicant_Experience(string id, ExperiencesViewModels models)
    {

        var item = await _applicant_ExperiencesRepository.PatchApplicant_Experience(id, models);

        if (item is null)
        {
            return BadRequest("Don`t have data");
        }
        return Ok(item);
    }

    [HttpDelete("deleteexperience")]
    public async Task<IActionResult> DeleteApplicant_Experience(string id)
    {
        var item = await _applicant_ExperiencesRepository.DeleteApplicant_Experience(id);
        
        if (item is null)
        {
            return BadRequest("Don`t have data");
        }
        return Ok("Deleting success");
    }

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

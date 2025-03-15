namespace online_job_finder.Api.Controllers.Endpoints;

[Authorize(Roles = "Applicants")]
[Route("api/me/[controller]")]
[ApiController]
public class Applicant_SkillsController : ControllerBase
{
    private readonly IApplicant_SkillsRepository _pplicant_SkillsRepository;
    private const string claimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

    public Applicant_SkillsController()
    {
        _pplicant_SkillsRepository = new Applicant_SkillsRepository();
    }

    [HttpPost("createapplicant_skills")]
    public async Task<IActionResult> CreateApplicant_Skills(Applicant_SkillsViewModels models)
    {
        var authorizationHeader = Request.Headers["Authorization"].ToString();
        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
        {
            return Unauthorized("Bearer token is missing or invalid.");
        }

        var token = authorizationHeader.Substring("Bearer ".Length).Trim();
        var userId = GetUserIdFromToken(token);
        if (userId == null)
        {
            return Unauthorized("Invalid token or NameIdentifier not found.");
        }

        var items = await _pplicant_SkillsRepository.CreateApplicant_Skills(models, userId);
        if (items == null)
        {
            return BadRequest("Invalid skill.");
        }
        return Ok(items);
    }

    [HttpGet("getapplicant_skills")]
    public async Task<IActionResult> GetApplicant_Skills()
    {
        var authorizationHeader = Request.Headers["Authorization"].ToString();
        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
        {
            return Unauthorized("Bearer token is missing or invalid.");
        }

        var token = authorizationHeader.Substring("Bearer ".Length).Trim();
        var userId = GetUserIdFromToken(token);
        if (userId == null)
        {
            return Unauthorized("Invalid token or NameIdentifier not found.");
        }

        var items = await _pplicant_SkillsRepository.GetApplicant_Skills(userId);

        return Ok(items);
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

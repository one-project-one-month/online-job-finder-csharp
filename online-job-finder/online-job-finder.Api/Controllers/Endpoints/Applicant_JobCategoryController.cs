namespace online_job_finder.Api.Controllers.Endpoints;

[Authorize(Roles = "Applicants")]
[Route("api/[controller]")]
[ApiController]
public class Applicant_JobCategoryController : ControllerBase
{
    private readonly IApplicant_JobCategoryRepository _applicant_JobCategoryRepository;
    private const string claimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

    public Applicant_JobCategoryController()
    {
        _applicant_JobCategoryRepository = new Applicant_JobCategoryRepository();
    }

    [HttpPost("createapplicant_jobcategory")]
    public async Task<IActionResult> CreateApplicant_JobCategory(Applicant_JobCategoryViewModels models)
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

        var items = await _applicant_JobCategoryRepository
            .CreateApplicant_JobCategory(models, userId);
        if (items == null)
        {
            return BadRequest("Invalid JobCategory.");
        }
        return Ok(items);
    }

    [HttpGet("getapplicant_jobcategories")]
    public async Task<IActionResult> GetApplicant_JobCategories()
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

        var items = await _applicant_JobCategoryRepository
            .GetApplicant_JobCategories(userId);

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

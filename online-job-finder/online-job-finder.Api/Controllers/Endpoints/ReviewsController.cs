namespace online_job_finder.Api.Controllers.Endpoints;

[Authorize(Roles = "Applicants")]
[Route("api/Applicants/me/[controller]")]
[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly IReviewsRepository _reviewsRepository;
    private const string claimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

    public ReviewsController()
    {
        _reviewsRepository = new ReviewsRepository();
    }
    
    
    [HttpGet("getreviews")]
    public async Task<IActionResult> GetReviews()
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

        var items = await _reviewsRepository.GetReviews(userId);

        return Ok(items);
    }

    [HttpGet("getreview")]
    public async Task<IActionResult> GetReview(string id)
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

        var items = await _reviewsRepository.GetReview(id,userId);

        return Ok(items);
    }

    [HttpPost("createreview")]
    public async Task<IActionResult> CreateReview(ReviewViewModels models)
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

        var items = await _reviewsRepository.CreateReview(models, userId);

        return Ok(items);
    }

    [HttpPut("updatereview")]
    public async Task<IActionResult> UpdateReview(string id, ReviewViewModels models)
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

        var item = await _reviewsRepository.UpdateReview(id, models, userId);

        if (item is null)
        {
            return BadRequest("Don`t have data");
        }
        return Ok(item);
    }

    [HttpPatch("patchreview")]
    public async Task<IActionResult> PatchReview(string id, ReviewViewModels models)
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

        var item = await _reviewsRepository.PatchReview(id, models, userId);

        if (item is null)
        {
            return BadRequest("Don`t have data");
        }
        return Ok(item);
    }

    [HttpDelete("deletereview")]
    public async Task<IActionResult> DeleteReview(string id)
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

        var item = await _reviewsRepository.DeleteReview(id,userId);

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

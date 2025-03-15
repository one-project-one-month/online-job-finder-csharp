using online_job_finder.Domain.Services.ApplicantProfileServices;

namespace online_job_finder.Api.Controllers.Endpoints;

[Route("api/me")]
[ApiController]
public class ApplicantProfileController : ControllerBase
{
    private readonly IApplicantProfileRepository _applicantProfileRepository;

    public ApplicantProfileController(IApplicantProfileRepository applicantProfileRepository)
    {
        _applicantProfileRepository = applicantProfileRepository;
    }

    [HttpGet]
    public IActionResult GetApplicantProfile(string applicantId)
    {
        var item = _applicantProfileRepository.GetApplicantProfile(applicantId);
        return Ok(item);
    }

    [HttpPost]
    public IActionResult CreateApplicantProfile(ApplicantProfileViewModel applicantProfile)
    {
        var item = _applicantProfileRepository.CreateApplicantProfile(applicantProfile);
        return Ok(item);
    }

    [Authorize(Roles = "Applicants")]
    [HttpPut("update")]
    public IActionResult UpdateApplicantProfile(string applicantId, ApplicantProfileViewModel applicantProfile)
    {
        var item = _applicantProfileRepository.UpdateApplicantProfile(applicantId, applicantProfile);
        return Ok(item);
    }

    [Authorize(Roles = "Applicants")]
    [HttpPatch("patch")]
    public IActionResult PatchApplicantProfile(string applicantId, ApplicantProfileViewModel applicantProfile)
    {
        var item = _applicantProfileRepository.UpdateApplicantProfile(applicantId, applicantProfile);
        return Ok(item);
    }

    [Authorize(Roles = "Applicants")]
    [HttpDelete("delete")]
    public IActionResult DeleteApplicantProfile(string applicantId)
    {
        var item = _applicantProfileRepository.DeleteApplicantProfile(applicantId);
        return Ok(item);
    }
}

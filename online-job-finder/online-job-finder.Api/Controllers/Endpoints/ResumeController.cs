namespace online_job_finder.Api.Controllers.Endpoints;

[Authorize(Roles = "Applicants")]
[Route("api/me/resume")]
[ApiController]

public class ResumeController : Controller
{

    private readonly IResumeRepository _resumeRepository;

    public ResumeController(IResumeRepository resumeRepository)
    {
        _resumeRepository = resumeRepository;
    }

    [Authorize(Roles = "Applicants, Recruiters")]
    [HttpGet]
    public IActionResult GetResumes(string applicantId)
    {
        var item = _resumeRepository.GetResumes(applicantId);
        return Ok(item);
    }

    [Authorize(Roles = "Applicants, Recruiters")]
    [HttpGet("id")]
    public IActionResult GetResume(string applicantId, string resumeId)
    {
        var item = _resumeRepository.GetResume(applicantId, resumeId);
        return Ok(item);
    }

    [HttpPost]
    public IActionResult CreateResume(ResumeViewModel requestResume)
    {
        var item = _resumeRepository.CreateResume(requestResume);
        return Ok(item);
    }

    [HttpPut("update")]
    public IActionResult UpdateResume(string applicantId, string resumeId, ResumeViewModel requestResume)
    {
        var item = _resumeRepository.UpdateResume(applicantId, resumeId, requestResume);
        return Ok(item);
    }

    [HttpPatch("patch")]
    public IActionResult PatchResume(string applicantId, string resumeId, ResumeViewModel requestResume)
    {
        var item = _resumeRepository.UpdateResume(applicantId, resumeId, requestResume);
        return Ok(item);
    }

    [HttpDelete("delete")]
    public IActionResult DeleteResume(string applicantId, string resumeId)
    {
        var item = _resumeRepository.DeleteResume(applicantId, resumeId);
        return Ok(item);
    }
}

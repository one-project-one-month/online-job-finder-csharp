using Microsoft.AspNetCore.Mvc;
using online_job_finder.Domain.Services.ApplicationServies;
using online_job_finder.Domain.Services.ResumeServices;

namespace online_job_finder.Api.Controllers.Endpoints;

[Authorize(Roles = "Applicants")]
[Route("api/application")]
[ApiController]

public class ApplicationController : Controller
{
    private readonly IApplicationRepository _applicationRepository;

    public ApplicationController(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    [Authorize(Roles = "Applicants, Recruiters")]
    [HttpGet]
    public IActionResult GetApplications(string applicantId)
    {
        var item = _applicationRepository.GetApplications(applicantId);
        return Ok(item);
    }

    [Authorize(Roles = "Applicants, Recruiters")]
    [HttpGet("id")]
    public IActionResult GetApplication(string applicantId, string applicationId)
    {
        var item = _applicationRepository.GetApplication(applicantId, applicationId);
        return Ok(item);
    }

    [HttpPost]
    public IActionResult CreateApplication(ApplicantApplicationViewModel requestApplication)
    {
        var item = _applicationRepository.CreateApplication(requestApplication);
        return Ok(item);
    }

    [HttpPut("update")]
    public IActionResult UpdateApplication(string applicantId, string applicationId, ApplicantApplicationViewModel requestApplication)
    {
        var item = _applicationRepository.UpdateApplication(applicantId, applicationId, requestApplication);
        return Ok(item);
    }

    [HttpPatch("patch")]
    public IActionResult PatchApplication(string applicantId, string applicationId, ApplicantApplicationViewModel requestApplication)
    {
        var item = _applicationRepository.UpdateApplication(applicantId, applicationId, requestApplication);
        return Ok(item);
    }

    [HttpDelete("delete")]
    public IActionResult DeleteApplication(string applicantId, string applicationId)
    {
        var item = _applicationRepository.DeleteApplication(applicantId, applicationId);
        return Ok(item);
    }

    [HttpPatch("SavedJob")]
    public IActionResult SavedJob(string applicantId, SavedJobViewModel requestSavedJob)
    {
        var item = _applicationRepository.SavedJob(applicantId, requestSavedJob);
        return Ok(item);
    }
}

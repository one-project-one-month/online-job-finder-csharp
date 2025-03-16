using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using online_job_finder.DataBase.Models;
using online_job_finder.Domain.Services.CompanyProfileServices;
using online_job_finder.Domain.Services.CompanyProfileServices.CompanyProfileResponses;

namespace online_job_finder.Api.Controllers.Endpoints;

[Authorize(Roles = "Recruiters")]
[Route("api/recruiter")]
[ApiController]
public class CompanyProfileController : ControllerBase
{
    private readonly ICompanyProfileServices _companyProfileServices;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AppDbContext _db;

    public CompanyProfileController(ICompanyProfileServices companyProfileServices, IHttpContextAccessor httpContextAccessor, AppDbContext db)
    {
        _companyProfileServices = companyProfileServices;
        _httpContextAccessor = httpContextAccessor;
        _db = db;
    }

    private Guid GetCurrentCompanyId()
    {
        var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var companyProfile = _db.TblCompanyProfiles
            // .FirstOrDefault(x => x.UserId == Guid.Parse(userId) && !x.IsDelete);
            .FirstOrDefault(x => x.UserId == Guid.Parse(userId) && !x.IsDelete);
        return companyProfile?.CompanyProfilesId ?? Guid.Empty;
    }
    // jobs management
    [HttpGet("me/jobs")]
    public IActionResult GetMyJobs()
    {
        //in a real application , get the company id from the authenticated user 
        // var companyId = Guid.Parse("your-company-id"); // Replace with actual authentication
        var companyId = GetCurrentCompanyId();
        var jobs = _companyProfileServices.GetCompanyJobs(companyId);
        return Ok(jobs);
    }

    [HttpPost("me/jobs")]
    public IActionResult CreateJob(CompanyProfileJobViewModel job)
    {
        //create job
        var createdjob = _companyProfileServices.CreateJob(job);

        return CreatedAtAction(nameof(GetMyJobs), new { id = createdjob.JobsId }, createdjob);
    }

    [HttpPut("me/jobs/{jobId}")]
    public IActionResult UpdateJob(string jobId, CompanyProfileJobViewModel job)
    {
        //update job
        var companyId = GetCurrentCompanyId();
        if (job.CompanyProfilesId != companyId)
        {
            return Forbid();
        }
        var updatedJob = _companyProfileServices.UpdateJob(jobId, job);
        if (updatedJob is null) return NotFound("Job not found");

        return Ok(updatedJob);

    }

    [HttpDelete("me/jobs/{jobId}")]
    public IActionResult DeleteJob(string jobId)
    {
        //delete job
        var result = _companyProfileServices.DeleteJob(jobId);
        if (!result) return NotFound("Job not found");

        return Ok("Job deleted successfully");

    }

    // Applications Management 
    [HttpGet("me/jobs/{jobId}/applications")]
    public IActionResult GetJobApplications(string jobId)
    {
        // Get all applications for specific job
        var applications = _companyProfileServices.GetJobApplications(jobId);
        return Ok(applications);

    }

    [HttpGet("me/jobs/{jobId}/shortlist")]
    public IActionResult GetShortlistedApplications(string jobId)
    {
        // Get shortlisted candidates
        var shortListedApplications = _companyProfileServices.GetShortlistedApplications(jobId);
        return Ok(shortListedApplications);

    }

    [HttpPatch("me/applications/{applicationId}/status")]
    public IActionResult UpdateApplicationStatus(string applicationId, string status)
    {
        // Update application status (Pending/Seen/Accepted/Rejected)
        // if (!new[] { "Pending", "Seen", "Accepted", "Rejected" }.Contains(status))
        // {
        //     return BadRequest("Invalid status");
        // }

        var validStatus = JobStatus.GetValidStatus(status);
        if (validStatus is null)
        {
            return BadRequest("Invalid status");
        }

        var result = _companyProfileServices.UpdateApplicationStatus(applicationId, status);
        if (!result)
        {
            return NotFound("Application not found");
        }

        return Ok(" Status updated successfully");
    }

    // Company Reviews
    [HttpGet("me/reviews")]
    public IActionResult GetCompanyReviews()
    {
        // Get all reviews for the company
        // Get company ID from authenticated user
        // var companyId = Guid.Parse("your-company-id"); // Replace with actual authentication
        var companyId = GetCurrentCompanyId();
        var reviews = _companyProfileServices.GetCompanyReviews(companyId);
        return Ok(reviews);

    }
}


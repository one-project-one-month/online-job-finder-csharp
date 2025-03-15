using Microsoft.AspNetCore.Mvc;
using online_job_finder.DataBase.Models;
using online_job_finder.Domain.Services.JobServices;
using online_job_finder.Domain.ViewModels;

namespace online_job_finder.Api.Controllers.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : Controller
    {
        private readonly JobRepository _jobRepo;
        private readonly ILogger<JobsController> _logger;
        public JobsController(JobRepository jobRepo, ILogger<JobsController> logger)
        {
            _jobRepo = jobRepo;
            _logger = logger;
        }

        [Authorize(Roles = "Admins,Company,Applicants,Recruiters")]
        [HttpGet("getalljob")]
        public IActionResult GetJobs()
        {
            try
            {
                var jobs = _jobRepo.GetJobsAsync();
                return Ok(jobs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    statusCode = 500,
                    message = ex.Message
                });
            }
        }

        [Authorize(Roles = "Admins,Company,Applicants,Recruiters")]
        [HttpGet("getjob/{id}")]
        public IActionResult GetJob(string id)
        {
            var job = _jobRepo.GetJobById(id);
            return Ok(job);
        }

        [Authorize(Roles = "Recruiters")]
        [HttpPost("createjob")]        
        public IActionResult CreateJob(JobsViewModels models)
        {
            var items = _jobRepo.CreateJob(models);

            return Ok(items);
        }
        
        [Authorize(Roles = "Recruiters")]
        [HttpPut("updatejob/{id}")]
        public IActionResult UpdateJob(string id, JobsViewModels models)
        {

            var item = _jobRepo.UpdateJob(id, models);
            if (item is null)
            {
                return BadRequest("Don`t have data");
            }
            return Ok(item);
        }
        
        [Authorize(Roles = "Recruiters")]
        [HttpPatch("patchjob/{id}")]
        public IActionResult PatchJob(string id, JobsViewModels models)
        {

            var item = _jobRepo.PatchJob(id, models);
            if (item is null)
            {
                return BadRequest("Don`t have data");
            }
            return Ok(item);
        }
        
        [Authorize(Roles = "Recruiters")]
        [HttpDelete("deletejob/{id}")]
        public IActionResult DeleteJob(string id)
        {
            var item = _jobRepo.DeleteJob(id);

            if (item is null)
            {
                return BadRequest("Don`t have data");
            }
            return Ok("Deleting success");
        }
        
        [HttpGet("search")]
        public IActionResult SearchJob([FromQuery] string[] q, [FromQuery] string[] location, [FromQuery] string[] category, [FromQuery] string[] type)
        {
            System.Console.WriteLine($"Query: {string.Join(", ", q)}");
            System.Console.WriteLine($"Location: {string.Join(", ", location)}");
            System.Console.WriteLine($"Category: {string.Join(", ", category)}");
            System.Console.WriteLine($"Type: {string.Join(", ", type)}");

            var requestJob = new JobSearchParameters(q, location, category, type);


            var jobs = _jobRepo.GetJobsAsync(requestJob);

            return Ok(jobs);

        }
        
        [Authorize(Roles = "Applicants")]
        [HttpPost("applyjob")]
        public IActionResult applyJob(ApplyJobViewModels models)
        {
            var items = _jobRepo.applyJob(models);

            if(items is null)
            {
                return BadRequest("Already Applied");
            }
            return Ok("Success!");
        }
        
        [Authorize(Roles = "Applicants")]
        [HttpPost("savejob")]
        public IActionResult saveyJob(SavedJobViewModels models)
        {
            var items = _jobRepo.saveJob(models);

            return Ok("Success!");
        }

    }

}

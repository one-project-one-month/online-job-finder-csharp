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

        [Authorize(Roles = "Admins,Company,Applicants,Recruiters")]
        [HttpPost("search")]
        public IActionResult SearchWithParam([FromBody] JobSearchParameters searchParameters)
        {
            if (String.IsNullOrEmpty(searchParameters.Title) &&
                String.IsNullOrEmpty(searchParameters.Location) &&
                String.IsNullOrEmpty(searchParameters.Industry) &&
                String.IsNullOrEmpty(searchParameters.Type))
            {
                return BadRequest("Don`t have search data");
            }
            var jobs = _jobRepo.GetJobsAsync(searchParameters);
            if (jobs == null)
            {
                return BadRequest("Don`t have query data");
            }
            return Ok(jobs);
        }

        [Authorize(Roles = "Applicants")]
        [HttpPost("applyjob")]
        public IActionResult applyJob(ApplyJobViewModels models)
        {
            var items = _jobRepo.applyJob(models);
            if (items is null)
            {
                return BadRequest("Already applied or Require Data!");
            }
            return Ok("Success!");
        }

        [Authorize(Roles = "Applicants")]
        [HttpGet("getallappliedjob/{applicantProfileId}")]
        public IActionResult GetAppliedJobs(string applicantProfileId)
        {
            var items = _jobRepo.GetAppliedJobs(applicantProfileId);

            return Ok(items);
        }

        [Authorize(Roles = "Applicants")]
        [HttpPost("savejob")]
        public IActionResult saveJob(SavedJobViewModel models)
        {
            if (models is null)
            {
                return BadRequest("Don`t have data");
            }
            var items = _jobRepo.saveJob(models);
            if (items is null)
            {
                return BadRequest("Already saved or Require Data!");
            }
            return Ok("Success!");
        }

        [Authorize(Roles = "Applicants")]
        [HttpGet("getsavedjob/{applicantProfileId}")]
        public IActionResult GetSavedJobs(string applicantProfileId)
        {
            var items = _jobRepo.GetSavedJobs(applicantProfileId);

            return Ok(items);
        }
    }

}

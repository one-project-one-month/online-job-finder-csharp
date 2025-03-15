namespace online_job_finder.Api.Controllers.Endpoints;

[Route("api/[controller]")]
[ApiController]
public class JobsController : Controller
{
    /*
      * 4.Job 
         - /jobs               
         - /jobs/{id} 
         - jobs CRUD => CRD [completed],UpdateJob and PatchJob [in-progress]
         - /jobs/{id}/apply(not started)
         - /jobs/{id}/save (on-hold)
    */
    private readonly JobRepository _jobRepo;
    private readonly ILogger<JobsController> _logger;
    public JobsController(JobRepository jobRepo, ILogger<JobsController> logger)
    {
        _jobRepo = jobRepo;
        _logger = logger;
    }

    [HttpGet]
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
    [HttpGet("GetJob/{id}")]
    public IActionResult GetJob(string id)
    {
        var job = _jobRepo.GetJobById(id);
        return Ok(job);
    }
    [HttpPost("CreateJob")]
    public IActionResult CreateJob(JobsViewModels models)
    {
        var items = _jobRepo.CreateJob(models);

        return Ok(items);
    }
    //Still in progress....
    [HttpPut("UpdateJob/{id}")]
    public IActionResult UpdateJob(string id, JobsViewModels models)
    {

        var item = _jobRepo.UpdateJob(id, models);
        if (item is null)
        {
            return BadRequest("Don`t have data");
        }
        return Ok(item);
    }
    //Still in progress....
    [HttpPatch("PatchJob/{id}")]
    public IActionResult PatchJob(string id, JobsViewModels models)
    {

        var item = _jobRepo.PatchJob(id, models);
        if (item is null)
        {
            return BadRequest("Don`t have data");
        }
        return Ok(item);
    }

    [HttpDelete("DeleteJob/{id}")]
    public IActionResult DeleteJob(string id)
    {
        var item = _jobRepo.DeleteJob(id);

        if (item is null)
        {
            return BadRequest("Don`t have data");
        }
        return Ok("Deleting success");
    }

}

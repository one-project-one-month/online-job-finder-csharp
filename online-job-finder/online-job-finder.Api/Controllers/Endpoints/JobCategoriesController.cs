namespace online_job_finder.Api.Controllers.Endpoints;

[Authorize(Roles = "Admins")]
[Route("api/admins/[controller]")]
[ApiController]
public class jobcategoriesController : ControllerBase
{
    private readonly IJobCategoryRepository _jobCategoryRepository;

    public jobcategoriesController()
    {
        _jobCategoryRepository = new JobCategoryRepository();
    }

    [HttpGet("getjobcategories")]
    public async Task<IActionResult> GetJobCategories()
    {
        var items = await _jobCategoryRepository.GetJobCategories();

        return Ok(items);
    }

    [HttpGet("getjobcategory")]
    public async Task<IActionResult> GetJobCategory(string id)
    {
        var items = await _jobCategoryRepository.GetJobCategory(id);

        return Ok(items);
    }

    [HttpPost("createjobcategory")]
    public async Task<IActionResult> CreateJobCategory(JobCategoryViewModels models)
    {
        var items = await _jobCategoryRepository.CreateJobCategory(models);

        return Ok(items);
    }

    [HttpPut("updatejobcategory")]
    public async Task<IActionResult> UpdateJobCategory(string id, JobCategoryViewModels models)
    {

        var item = await _jobCategoryRepository.UpdateJobCategory(id, models);

        if (item is null)
        {
            return BadRequest("Don`t have data");
        }
        return Ok(item);
    }

    [HttpPatch("patchjobcategory")]
    public async Task<IActionResult> PatchJobCategory(string id, JobCategoryViewModels models)
    {

        var item = await _jobCategoryRepository.PatchJobCategory(id, models);

        if (item is null)
        {
            return BadRequest("Don`t have data");
        }
        return Ok(item);
    }

    [HttpDelete("deletejobcategory")]
    public async Task<IActionResult> DeleteJobCategory(string id)
    {
        var item = await _jobCategoryRepository.DeleteJobCategory(id);
        
        if (item is null)
        {
            return BadRequest("Don`t have data");
        }
        return Ok("Deleting success");
    }
}

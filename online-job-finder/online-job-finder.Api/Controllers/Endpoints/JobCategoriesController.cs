using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using online_job_finder.Domain.Services.JobCategoriesServices;
using online_job_finder.Domain.Services.RoleServices;
using online_job_finder.Domain.ViewModels.JobCategories;
using online_job_finder.Domain.ViewModels.Roles;

namespace online_job_finder.Api.Controllers.Endpoints
{
    //[Route("api/admin/[controller]")]
    [Route("api/admin/job-categories")]
    [ApiController]
    public class JobCategoriesController : ControllerBase
    {
        private readonly IJobCategoryRepository _jobCategoryRepository;

        public JobCategoriesController(IJobCategoryRepository jobCategoryRepository)
        {
            _jobCategoryRepository = jobCategoryRepository;
        }

        [HttpGet("GetJobCategories")]
        public IActionResult GetJobCategories()
        {
            var items = _jobCategoryRepository.GetJobCategories();

            return Ok(items);
        }

        [HttpGet("GetJobCategory/{id}")]
        public IActionResult GetRole(string id)
        {
            var items = _jobCategoryRepository.GetJobCategory(id);

            return Ok(items);
        }

        [HttpPost("CreateJobCategory")]
        public IActionResult CreateJobCategory(JobsCategoriesViewModels jobCategory)
        {
            var items = _jobCategoryRepository.CreateJobCategory(jobCategory);

            return Ok(items);
        }

        [HttpPut("UpdateJobCategory/{id}")]
        public IActionResult UpdateJobCategory(string id, JobsCategoriesViewModels jobCategory)
        {

            var item = _jobCategoryRepository.UpdateJobCategory(id, jobCategory);

            // need to write reponse & request

            if (item is null)
            {
                return BadRequest("Don`t have data");
            }
            return Ok(item);
        }

        [HttpPatch("PatchJobCategory/{id}")]
        public IActionResult PatchJobCategory(string id, JobsCategoriesViewModels jobCategory)
        {

            var item = _jobCategoryRepository.PatchJobCategory(id, jobCategory);

            // need to write reponse & request

            if (item is null)
            {
                return BadRequest("Don`t have data");
            }
            return Ok(item);
        }

        [HttpDelete("DeleteJobCategory/{id}")]
        public IActionResult DeleteJobCategory(string id)
        {
            var item = _jobCategoryRepository.DeleteJobCategory(id);

            if (item is null)
            {
                return BadRequest("Don`t have data");
            }
            return Ok("Deleting success");
        }
    }
}

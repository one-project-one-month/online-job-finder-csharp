using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using online_job_finder.Domain.Services.JobCategoryServices;
using online_job_finder.Domain.ViewModels;

namespace online_job_finder.Api.Controllers.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobCategoriesController : ControllerBase
    {
        private readonly IJobCategoryRepository _jobCategoryRepository;

        public JobCategoriesController()
        {
            _jobCategoryRepository = new JobCategoryRepository();
        }

        [HttpGet("GetJobCategories")]
        public IActionResult GetJobCategories()
        {
            var items = _jobCategoryRepository.GetJobCategories();

            return Ok(items);
        }

        [HttpGet("GetJobCategory/{id}")]
        public IActionResult GetJobCategory(string id)
        {
            var items = _jobCategoryRepository.GetJobCategory(id);

            return Ok(items);
        }

        [HttpPost("CreateJobCategory")]
        public IActionResult CreateJobCategory(JobCategoryViewModels models)
        {
            var items = _jobCategoryRepository.CreateJobCategory(models);

            return Ok(items);
        }

        [HttpPut("UpdateJobCategory/{id}")]
        public IActionResult UpdateJobCategory(string id, JobCategoryViewModels models)
        {

            var item = _jobCategoryRepository.UpdateJobCategory(id, models);

            // need to write reponse & request

            if (item is null)
            {
                return BadRequest("Don`t have data");
            }
            return Ok(item);
        }

        [HttpPatch("PatchJobCategory/{id}")]
        public IActionResult PatchJobCategory(string id, JobCategoryViewModels models)
        {

            var item = _jobCategoryRepository.PatchJobCategory(id, models);

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

        [Authorize(Roles = "Admins")]
        [HttpGet("/admin/job-categories/GetJobCategories")]
        public IActionResult GetForAdminsJobCategories()
        {
            var items = _jobCategoryRepository.GetJobCategories();

            return Ok(items);
        }

        [Authorize(Roles = "Admins")]
        [HttpGet("/admin/job-categories/GetJobCategory/{id}")]
        public IActionResult GetForAdminsJobCategory(string id)
        {
            var items = _jobCategoryRepository.GetJobCategories();

            return Ok(items);
        }

        [Authorize(Roles = "Admins")]
        [HttpPost("/admin/job-categories/CreateJobCategory")]
        public IActionResult CreateForAdminsJobCategory(JobCategoryViewModels models)
        {
            var items = _jobCategoryRepository.CreateJobCategory(models);

            return Ok(items);
        }

        [Authorize(Roles = "Admins")]
        [HttpPut("/admin/job-categories/UpdateJobCategory/{id}")]
        public IActionResult UpdateForAdminsJobCategory(string id, JobCategoryViewModels models)
        {

            var item = _jobCategoryRepository.UpdateJobCategory(id, models);

            // need to write reponse & request

            if (item is null)
            {
                return BadRequest("Don`t have data");
            }
            return Ok(item);
        }

        [Authorize(Roles = "Admins")]
        [HttpPatch("/admin/job-categories/PatchJobCategory/{id}")]
        public IActionResult PatchForAdminsJobCategory(string id, JobCategoryViewModels models)
        {

            var item = _jobCategoryRepository.PatchJobCategory(id, models);

            // need to write reponse & request

            if (item is null)
            {
                return BadRequest("Don`t have data");
            }
            return Ok(item);
        }

        [Authorize(Roles = "Admins")]
        [HttpDelete("/admin/job-categories/DeleteJobCategory/{id}")]
        public IActionResult DeleteForAdminsJobCategory(string id)
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

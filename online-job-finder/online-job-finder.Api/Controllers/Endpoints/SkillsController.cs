using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using online_job_finder.Domain.Services.JobCategoriesServices;
using online_job_finder.Domain.Services.SkillServices;
using online_job_finder.Domain.ViewModels.JobCategories;
using online_job_finder.Domain.ViewModels.Skills;

namespace online_job_finder.Api.Controllers.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillRepository _skillRepository;


        public SkillsController(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        [HttpGet("GetSkills")]
        public IActionResult GetSkills()
        {
            var items = _skillRepository.GetSkills();

            return Ok(items);
        }

        [HttpGet("GetSkill/{id}")]
        public IActionResult GetSkill(string id)
        {
            var items = _skillRepository.GetSkill(id);

            return Ok(items);
        }

        [HttpPost("CreateSkill")]
        public IActionResult CreateSkill(SkillsViewModels skill)
        {
            var items = _skillRepository.CreateSkill(skill);

            return Ok(items);
        }

        [HttpPut("UpdateSkill/{id}")]
        public IActionResult UpdateSkill(string id, SkillsViewModels skill)
        {

            var item = _skillRepository.UpdateSkill(id, skill);

            // need to write reponse & request

            if (item is null)
            {
                return BadRequest("Don`t have data");
            }
            return Ok(item);
        }

        [HttpPatch("PatchSkill/{id}")]
        public IActionResult PatchSkill(string id, SkillsViewModels skill)
        {

            var item = _skillRepository.PatchSkill(id, skill);

            // need to write reponse & request

            if (item is null)
            {
                return BadRequest("Don`t have data");
            }
            return Ok(item);
        }

        [HttpDelete("DeleteSkill/{id}")]
        public IActionResult DeleteSkill(string id)
        {
            var item = _skillRepository.DeleteSkill(id);

            if (item is null)
            {
                return BadRequest("Don`t have data");
            }
            return Ok("Deleting success");
        }
    }
}

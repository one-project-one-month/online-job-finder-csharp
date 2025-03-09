using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using online_job_finder.Domain.Services.SkillServices;
using online_job_finder.Domain.ViewModels;

namespace online_job_finder.Api.Controllers.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillRepository _skillRepository;

        public SkillsController()
        {
            _skillRepository = new SkillRepository();
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
        public IActionResult CreateSkill(SkillsViewModels Skill)
        {
            var items = _skillRepository.CreateSkill(Skill);

            return Ok(items);
        }

        [HttpPut("UpdateSkill/{id}")]
        public IActionResult UpdateSkill(string id, SkillsViewModels Skill)
        {

            var item = _skillRepository.UpdateSkill(id, Skill);

            // need to write reponse & request

            if (item is null)
            {
                return BadRequest("Don`t have data");
            }
            return Ok(item);
        }

        [HttpPatch("PatchSkill/{id}")]
        public IActionResult PatchSkill(string id, SkillsViewModels Skill)
        {

            var item = _skillRepository.PatchSkill(id, Skill);

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

        [Authorize(Roles = "Admins")]
        [HttpGet("admin/skills/GetSkills")]
        public IActionResult GetForAdminsSkills()
        {
            var items = _skillRepository.GetSkills();

            return Ok(items);
        }

        [Authorize(Roles = "Admins")]
        [HttpGet("admin/skills/GetSkill/{id}")]
        public IActionResult GetForAdminsSkill(string id)
        {
            var items = _skillRepository.GetSkill(id);

            return Ok(items);
        }

        [Authorize(Roles = "Admins")]
        [HttpPost("admin/skills/CreateSkill")]
        public IActionResult CreateForAdminsSkill(SkillsViewModels Skill)
        {
            var items = _skillRepository.CreateSkill(Skill);

            return Ok(items);
        }

        [Authorize(Roles = "Admins")]
        [HttpPut("admin/skills/UpdateSkill/{id}")]
        public IActionResult UpdateForAdminsSkill(string id, SkillsViewModels Skill)
        {

            var item = _skillRepository.UpdateSkill(id, Skill);

            // need to write reponse & request

            if (item is null)
            {
                return BadRequest("Don`t have data");
            }
            return Ok(item);
        }

        [Authorize(Roles = "Admins")]        
        [HttpPatch("admin/skills/PatchSkill/{id}")]
        public IActionResult PatchForAdminsSkill(string id, SkillsViewModels Skill)
        {

            var item = _skillRepository.PatchSkill(id, Skill);

            // need to write reponse & request

            if (item is null)
            {
                return BadRequest("Don`t have data");
            }
            return Ok(item);
        }

        [Authorize(Roles = "Admins")]
        [HttpDelete("admin/skills/DeleteSkill/{id}")]
        public IActionResult DeleteForAdminsSkill(string id)
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

namespace online_job_finder.Api.Controllers.Endpoints;


[Authorize(Roles = "Admins")]
[Route("api/admins/[controller]")]
[ApiController]
public class skillsController : ControllerBase
{
    private readonly ISkillRepository _skillRepository;

    public skillsController()
    {
        _skillRepository = new SkillRepository();
    }

    [HttpGet("getskills")]
    public async Task<IActionResult> GetSkills()
    {
        var items = await _skillRepository.GetSkills();

        return Ok(items);
    }

    [HttpGet("getskill")]
    public async Task<IActionResult> GetSkill(string id)
    {
        var items = await _skillRepository.GetSkill(id);

        return Ok(items);
    }

    [HttpPost("createskill")]
    public async Task<IActionResult> CreateSkill(SkillsViewModels Skill)
    {
        var items = await _skillRepository.CreateSkill(Skill);

        return Ok(items);
    }

    [HttpPut("updateskill")]
    public async Task<IActionResult> UpdateSkill(string id, SkillsViewModels Skill)
    {
        var item = await _skillRepository.UpdateSkill(id, Skill);

        if (item is null)
        {
            return BadRequest("Don`t have data");
        }
        return Ok(item);
    }

    [HttpPatch("patchskill")]
    public async Task<IActionResult> PatchSkill(string id, SkillsViewModels Skill)
    {
        var item = await _skillRepository.PatchSkill(id, Skill);

        if (item is null)
        {
            return BadRequest("Don`t have data");
        }
        return Ok(item);
    }

    [HttpDelete("deleteskill")]
    public async Task<IActionResult> DeleteSkill(string id)
    {
        var item = await _skillRepository.DeleteSkill(id);

        if (item is null)
        {
            return BadRequest("Don`t have data");
        }
        return Ok("Deleting success");
    }
}

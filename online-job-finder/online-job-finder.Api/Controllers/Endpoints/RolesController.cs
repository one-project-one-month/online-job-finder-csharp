namespace online_job_finder.Api.Controllers.Endpoints;


[Authorize(Roles = "Admins")]
[Route("api/admins/[controller]")]
[ApiController]
public class rolesController : ControllerBase
{
    private readonly IRoleRepository _roleRepository;

    public rolesController()
    {
        _roleRepository = new RoleRepository();
    }

    [HttpGet("getroles")]
    public async Task<IActionResult> GetRoles()
    {
        var items = await _roleRepository.GetRoles();

        return Ok(items);
    }

    [HttpGet("getrole")]
    public async Task<IActionResult> GetRole(string id)
    {
        var items = await _roleRepository.GetRole(id);

        return Ok(items);
    }

    [HttpPost("createrole")]
    public async Task<IActionResult> CreateRole(RolesViewModels role)
    {
        var items = await _roleRepository.CreateRole(role);

        return Ok(items);
    }

    [HttpPut("updaterole")]
    public async Task<IActionResult> UpdateRole(string id, RolesViewModels role)
    {

        var item = await _roleRepository.UpdateRole(id, role);

        if (item is null)
        {
            return BadRequest("Don`t have data");
        }
        return Ok(item);
    }

    [HttpPatch("patchrole")]
    public async Task<IActionResult> PatchRole(string id, RolesViewModels role)
    {

        var item = await _roleRepository.PatchRole(id, role);

        if (item is null)
        {
            return BadRequest("Don`t have data");
        }
        return Ok(item);
    }

    [HttpDelete("deleterole")]
    public async Task<IActionResult> DeleteRole(string id)
    {
        var item = await _roleRepository.DeleteRole(id);

        if (item is null)
        {
            return BadRequest("Don`t have data");
        }
        return Ok("Deleting success");
    }
}

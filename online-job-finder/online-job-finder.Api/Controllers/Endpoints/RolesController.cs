using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using online_job_finder.Domain.Services.RoleServices;
using online_job_finder.Domain.ViewModels.Roles;
using online_job_finder.Domain.ViewModels.Users;
using System.Data;

namespace online_job_finder.Api.Controllers.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleRepository _roleRepository;

        public RolesController()
        {
            _roleRepository = new RoleRepository();
        }

        [HttpGet("HelloWorld")]
        public IActionResult HelloWorld()
        {
            return Ok("Hello Worlds");
        }

        [HttpGet("GetRoles")]
        public IActionResult GetRoles()
        {
            var items = _roleRepository.GetRoles();

            return Ok(items);
        }

        [HttpGet("GetRole/{id}")]
        public IActionResult GetRole(string id)
        {
            var items = _roleRepository.GetRole(id);

            return Ok(items);
        }

        [HttpPost("CreateRole")]
        public IActionResult CreateRole(RolesViewModels role)
        {
            var items = _roleRepository.CreateRole(role);

            return Ok(items);
        }

        [HttpPut("UpdateRole/{id}")]
        public IActionResult UpdateRole(string id, RolesViewModels role)
        {

            var item = _roleRepository.UpdateRole(id, role);

            // need to write reponse & request

            if (item is null)
            {
                return BadRequest("Don`t have data");
            }
            return Ok(item);
        }

        [HttpPatch("PatchRole/{id}")]
        public IActionResult PatchRole(string id, RolesViewModels role)
        {

            var item = _roleRepository.PatchRole(id, role);

            // need to write reponse & request

            if (item is null)
            {
                return BadRequest("Don`t have data");
            }
            return Ok(item);
        }

        [HttpDelete("DeleteRole/{id}")]
        public IActionResult DeleteRole(string id)
        {
            var item = _roleRepository.DeleteRole(id);

            if (item is null)
            {
                return BadRequest("Don`t have data");
            }
            return Ok("Deleting success");
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using online_job_finder.Domain.Services.LocationServices;
using online_job_finder.Domain.Services.SkillServices;
using online_job_finder.Domain.ViewModels.Locations;
using online_job_finder.Domain.ViewModels.Skills;

namespace online_job_finder.Api.Controllers.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;


        public LocationsController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        [HttpGet("GetLocations")]
        public IActionResult GetLocations()
        {
            var items = _locationRepository.GetLocations();

            return Ok(items);
        }

        [HttpGet("GetLocation/{id}")]
        public IActionResult GetLocation(string id)
        {
            var items = _locationRepository.GetLocation(id);

            return Ok(items);
        }

        [HttpPost("CreateLocation")]
        public IActionResult CreateLocation(LocationViewModels location)
        {
            var items = _locationRepository.CreateLocation(location);

            return Ok(items);
        }

        [HttpPut("UpdateLocation/{id}")]
        public IActionResult UpdateLocation(string id, LocationViewModels location)
        {

            var item = _locationRepository.UpdateLocation(id, location);

            // need to write reponse & request

            if (item is null)
            {
                return BadRequest("Don`t have data");
            }
            return Ok(item);
        }

        [HttpPatch("PatchLocation/{id}")]
        public IActionResult PatchLocation(string id, LocationViewModels location)
        {

            var item = _locationRepository.PatchLocation(id, location);

            // need to write reponse & request

            if (item is null)
            {
                return BadRequest("Don`t have data");
            }
            return Ok(item);
        }

        [HttpDelete("DeleteLocation/{id}")]
        public IActionResult DeleteLocation(string id)
        {
            var item = _locationRepository.DeleteLocation(id);

            if (item is null)
            {
                return BadRequest("Don`t have data");
            }
            return Ok("Deleting success");
        }
    }
}

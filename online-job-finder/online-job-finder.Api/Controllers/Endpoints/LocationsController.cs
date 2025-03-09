using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using online_job_finder.Domain.Services.LocationServices;
using online_job_finder.Domain.ViewModels;

namespace online_job_finder.Api.Controllers.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;

        public LocationsController()
        {
            _locationRepository = new LocationRepository();
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
        public IActionResult CreateLocation(LocationViewModels viewModels)
        {
            var items = _locationRepository.CreateLocation(viewModels);

            return Ok(items);
        }

        [HttpPut("UpdateLocation/{id}")]
        public IActionResult UpdateLocation(string id, LocationViewModels viewModels)
        {

            var item = _locationRepository.UpdateLocation(id, viewModels);

            // need to write reponse & request

            if (item is null)
            {
                return BadRequest("Don`t have data");
            }
            return Ok(item);
        }

        [HttpPatch("PatchLocation/{id}")]
        public IActionResult PatchLocation(string id, LocationViewModels viewModels)
        {

            var item = _locationRepository.PatchLocation(id, viewModels);

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

        
        [Authorize(Roles = "Admins")]
        [HttpGet("/admin/locations/GetLocations")]
        public IActionResult GetForAdminsLocations()
        {
            var items = _locationRepository.GetLocations();

            return Ok(items);
        }

        [Authorize(Roles = "Admins")]
        [HttpGet("/admin/locations/GetLocation/{id}")]
        public IActionResult GetForAdminsLocation(string id)
        {
            var items = _locationRepository.GetLocation(id);

            return Ok(items);
        }

        [Authorize(Roles = "Admins")]
        [HttpPost("/admin/locations/CreateLocation")]
        public IActionResult CreateForAdminsLocation(LocationViewModels viewModels)
        {
            var items = _locationRepository.CreateLocation(viewModels);

            return Ok(items);
        }

        [Authorize(Roles = "Admins")]
        [HttpPut("/admin/locations/UpdateLocation/{id}")]
        public IActionResult UpdateForAdminsLocation(string id, LocationViewModels viewModels)
        {

            var item = _locationRepository.UpdateLocation(id, viewModels);

            // need to write reponse & request

            if (item is null)
            {
                return BadRequest("Don`t have data");
            }
            return Ok(item);
        }

        [Authorize(Roles = "Admins")]
        [HttpPatch("/admin/locations/PatchLocation/{id}")]
        public IActionResult PatchForAdminsLocation(string id, LocationViewModels viewModels)
        {

            var item = _locationRepository.PatchLocation(id, viewModels);

            // need to write reponse & request

            if (item is null)
            {
                return BadRequest("Don`t have data");
            }
            return Ok(item);
        }

        [Authorize(Roles = "Admins")]
        [HttpDelete("/admin/locations/DeleteLocation/{id}")]
        public IActionResult DeleteForAdminsLocation(string id)
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

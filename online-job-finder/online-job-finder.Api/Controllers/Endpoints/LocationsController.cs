namespace online_job_finder.Api.Controllers.Endpoints;

[Authorize(Roles = "Admins")]
[Route("api/admins/[controller]")]
[ApiController]
public class locationsController : ControllerBase
{
    private readonly ILocationRepository _locationRepository;

    public locationsController()
    {
        _locationRepository = new LocationRepository();
    }

    [HttpGet("getlocations")]
    public async Task<IActionResult> GetLocations()
    {
        var items = await _locationRepository.GetLocations();

        return Ok(items);
    }

    [HttpGet("getlocation")]
    public async Task<IActionResult> GetLocation(string id)
    {
        var items = await _locationRepository.GetLocation(id);

        return Ok(items);
    }

    [HttpPost("createlocation")]
    public async Task<IActionResult> CreateLocation(LocationViewModels viewModels)
    {
        var items = await _locationRepository.CreateLocation(viewModels);

        return Ok(items);
    }

    [HttpPut("updatelocation")]
    public async Task<IActionResult> UpdateLocation(string id, LocationViewModels viewModels)
    {

        var item = await _locationRepository.UpdateLocation(id, viewModels);

        if (item is null)
        {
            return BadRequest("Don`t have data");
        }
        return Ok(item);
    }

    [HttpPatch("patchlocation")]
    public async Task<IActionResult> PatchLocation(string id, LocationViewModels viewModels)
    {

        var item = await _locationRepository.PatchLocation(id, viewModels);

        if (item is null)
        {
            return BadRequest("Don`t have data");
        }
        return Ok(item);
    }

    [HttpDelete("deletelocation")]
    public async Task<IActionResult> DeleteLocation(string id)
    {
        var item = await _locationRepository.DeleteLocation(id);

        if (item is null)
        {
            return BadRequest("Don`t have data");
        }
        return Ok("Deleting success");
    }
}

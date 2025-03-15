namespace online_job_finder.Domain.Services.LocationServices;

public interface ILocationRepository
{
    Task<LocationViewModels> CreateLocation(LocationViewModels location);

    Task<List<LocationViewModels>> GetLocations();

    Task<LocationViewModels?> GetLocation(string id);

    Task<LocationViewModels?> UpdateLocation(string id, LocationViewModels location);

    Task<LocationViewModels?> PatchLocation(string id, LocationViewModels location);

    Task<bool?> DeleteLocation(string id);

}

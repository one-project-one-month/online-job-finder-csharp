using online_job_finder.Domain.ViewModels;

namespace online_job_finder.Domain.Services.LocationServices;

public interface ILocationRepository
{
    LocationViewModels CreateLocation(LocationViewModels location);

    List<LocationViewModels> GetLocations();

    LocationViewModels? GetLocation(string id);

    LocationViewModels? UpdateLocation(string id, LocationViewModels location);

    LocationViewModels? PatchLocation(string id, LocationViewModels location);

    bool? DeleteLocation(string id);

}

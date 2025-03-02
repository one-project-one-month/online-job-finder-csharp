using online_job_finder.DataBase.Models;
using online_job_finder.Domain.ViewModels.Locations;
using online_job_finder.Domain.ViewModels.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_job_finder.Domain.Services.LocationServices
{
    public interface ILocationRepository
    {
        TblLocation CreateLocation(LocationViewModels location);

        List<LocationViewModels> GetLocations();

        LocationViewModels? GetLocation(string id);

        LocationViewModels? UpdateLocation(string id, LocationViewModels location);

        LocationViewModels? PatchLocation(string id, LocationViewModels location);

        bool? DeleteLocation(string id);
    }
}

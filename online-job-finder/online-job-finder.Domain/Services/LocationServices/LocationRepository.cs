using Microsoft.EntityFrameworkCore;
using online_job_finder.DataBase.Models;
using online_job_finder.Domain.ViewModels;

namespace online_job_finder.Domain.Services.LocationServices
{
    public class LocationRepository : ILocationRepository
    {
        private readonly AppDbContext _db;

        public LocationRepository()
        {
            _db = new AppDbContext();
        }

        // BLL
        public LocationViewModels CreateLocation(LocationViewModels model)
        {
            model.Version += 1;
            model.UpdatedAt = null;

            TblLocation locations = LocationsMapping(model);

            _db.TblLocations.Add(locations);
            _db.SaveChanges();

            return model;
        }

        public List<LocationViewModels> GetLocations()
        {
            var model = _db.TblLocations
                .AsNoTracking()
                .Where(x => x.IsDelete == false)
                .OrderBy(x => x.Version)
                .ToList();

            var ViewModels = model.Select(LocationsViewModelsMapping).ToList();

            return ViewModels;
        }

        public LocationViewModels? GetLocation(string id)
        {
            var model = _db.TblLocations
                .AsNoTracking()
                .FirstOrDefault(x => x.LocationId.ToString() == id
                && x.IsDelete == false);

            if (model is null) { return null; }

            var mappingModel = LocationsViewModelsMapping(model);

            return mappingModel;
        }

        public LocationViewModels? UpdateLocation(string id, LocationViewModels locations)
        {
            var item = _db.TblLocations
                .AsNoTracking()
                .FirstOrDefault(x => x.LocationId.ToString() == id
                && x.IsDelete == false);
            if (item is null) { return null; }

            if (!string.IsNullOrEmpty(locations.LocationName))
            {
                item.LocationName = locations.LocationName;
            }
            if (!string.IsNullOrEmpty(locations.Description))
            {
                item.Description = locations.Description;
            }

            item.Version += 1;
            item.UpdatedAt = DateTime.UtcNow;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            var model = LocationsViewModelsMapping(item);

            return model;
        }

        public LocationViewModels? PatchLocation(string id, LocationViewModels locations)
        {
            var item = _db.TblLocations
                .AsNoTracking()
                .FirstOrDefault(x => x.LocationId.ToString() == id
                && x.IsDelete == false);
            if (item is null) { return null; }

            if (!string.IsNullOrEmpty(locations.LocationName))
            {
                item.LocationName = locations.LocationName;
            }
            if (!string.IsNullOrEmpty(locations.Description))
            {
                item.Description = locations.Description;
            }

            item.Version += 1;
            item.UpdatedAt = DateTime.UtcNow;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            var model = LocationsViewModelsMapping(item);
            return model;
        }

        public bool? DeleteLocation(string id)
        {
            var item = _db.TblLocations
                .AsNoTracking()
                .FirstOrDefault(x => x.LocationId.ToString() == id
                && x.IsDelete == false);
            if (item is null)
            {
                return null;
            }

            item.Version += 1;
            item.UpdatedAt = DateTime.UtcNow;
            item.IsDelete = true;

            _db.Entry(item).State = EntityState.Modified;
            var result = _db.SaveChanges();

            return result > 0;
        }


        //Can use for every Location
        private static TblLocation LocationsMapping(LocationViewModels locations)
        {
            return new TblLocation
            {
                LocationId = Guid.NewGuid(),
                LocationName = locations.LocationName,
                Description = locations.Description,
                Version = locations.Version,
                CreatedAt = locations.CreatedAt,
                UpdatedAt = locations.UpdatedAt,
                IsDelete = false
            };
        }

        private static LocationViewModels LocationsViewModelsMapping(TblLocation locations)
        {
            return new LocationViewModels
            {
                //LocationId = Guid.NewGuid(),
                LocationName = locations.LocationName,
                Description = locations.Description,
                Version = locations.Version,
                CreatedAt = locations.CreatedAt,
                UpdatedAt = locations.UpdatedAt
                //IsDelete = false
            };
        }
    }
}

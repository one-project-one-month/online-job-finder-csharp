using Microsoft.EntityFrameworkCore;
using online_job_finder.DataBase.Models;
using online_job_finder.Domain.ViewModels.Locations;
using online_job_finder.Domain.ViewModels.Skills;
using online_job_finder.Domain.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_job_finder.Domain.Services.LocationServices
{
    public class LocationRepository:ILocationRepository
    {
        private readonly AppDbContext _db;

        public LocationRepository(AppDbContext db)
        {
            _db = db;
        }

        public TblLocation CreateLocation(LocationViewModels location)
        {

            TblLocation locationTable = LocationsMapping(location);



            _db.TblLocations.Add(locationTable);
            _db.SaveChanges();

            return locationTable;
        }

        public List<LocationViewModels> GetLocations()
        {
            var model = _db.TblLocations
                .AsNoTracking()
                .ToList();

            var locationViewModels = model.Select(LocationViewModelsMapping).ToList();

            return locationViewModels;
        }

        public LocationViewModels? GetLocation(string id)
        {
            var model = _db.TblLocations
                .AsNoTracking()
                .FirstOrDefault(x => x.LocationID.ToString() == id
                );

            if (model is null) { return null; }

            var mappingModel = LocationViewModelsMapping(model);

            return mappingModel;
        }

        public LocationViewModels? UpdateLocation(string id, LocationViewModels skill)
        {
            var item = _db.TblLocations
                .AsNoTracking()
                .FirstOrDefault(x => x.LocationID.ToString() == id
                );
            if (item is null) { return null; }

            if (!string.IsNullOrEmpty(skill.LocationName))
            {
                item.LocationName = skill.LocationName;
            }



            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            var model = LocationViewModelsMapping(item);

            return model;
        }

        public LocationViewModels? PatchLocation(string id, LocationViewModels location)
        {
            var item = _db.TblLocations
                .AsNoTracking()
                .FirstOrDefault(x => x.LocationID.ToString() == id
                );
            if (item is null) { return null; }

            if (!string.IsNullOrEmpty(location.LocationName))
            {
                item.LocationName = location.LocationName;
            }



            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            var model = LocationViewModelsMapping(item);
            return location;
        }

        public bool? DeleteLocation(string id)
        {
            var item = _db.TblLocations
                .AsNoTracking()
                .FirstOrDefault(x => x.LocationID.ToString() == id
                );
            if (item is null)
            {
                return null;
            }




            _db.Entry(item).State = EntityState.Modified;
            var result = _db.SaveChanges();

            return result > 0;
        }


        private static TblLocation LocationsMapping(LocationViewModels location)
        {
            return new TblLocation
            {
                LocationID = Guid.NewGuid(),
                LocationName = location.LocationName
            };
        }


        private static LocationViewModels LocationViewModelsMapping(TblLocation location)
        {
            return new LocationViewModels
            {

                LocationName = location.LocationName,

            };
        }

        public UsersViewModels CreateLocation(UsersViewModels user)
        {
            throw new NotImplementedException();
        }
    }
}

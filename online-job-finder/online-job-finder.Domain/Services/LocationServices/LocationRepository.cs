namespace online_job_finder.Domain.Services.LocationServices;

public class LocationRepository : ILocationRepository
{
    private readonly AppDbContext _db;

    public LocationRepository()
    {
        _db = new AppDbContext();
    }

    public async Task<LocationViewModels> CreateLocation(LocationViewModels model)
    {
        model.Version += 1;
        model.UpdatedAt = null;

        TblLocation locations = LocationsMapping(model);

        await _db.TblLocations.AddAsync(locations);
        await _db.SaveChangesAsync();

        return model;
    }

    public async Task<List<LocationViewModels>> GetLocations()
    {
        var model = await _db.TblLocations
            .AsNoTracking()
            .Where(x => x.IsDelete == false)
            .OrderBy(x => x.Version)
            .ToListAsync();

        var ViewModels = model.Select(LocationsViewModelsMapping).ToList();

        return ViewModels;
    }

    public async Task<LocationViewModels?> GetLocation(string id)
    {
        var model = await _db.TblLocations
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.LocationId.ToString() == id
            && x.IsDelete == false);

        if (model is null) { return null; }

        var mappingModel = LocationsViewModelsMapping(model);

        return mappingModel;
    }

    public async Task<LocationViewModels?> UpdateLocation(string id, LocationViewModels locations)
    {
        var item = await _db.TblLocations
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.LocationId.ToString() == id
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
        await _db.SaveChangesAsync();

        var model = LocationsViewModelsMapping(item);

        return model;
    }

    public async Task<LocationViewModels?> PatchLocation(string id, LocationViewModels locations)
    {
        var item = await _db.TblLocations
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.LocationId.ToString() == id
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
        await _db.SaveChangesAsync();

        var model = LocationsViewModelsMapping(item);
        return model;
    }

    public async Task<bool?> DeleteLocation(string id)
    {
        var item = await _db.TblLocations
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.LocationId.ToString() == id
            && x.IsDelete == false);

        if (item is null)
        {
            return null;
        }

        item.Version += 1;
        item.UpdatedAt = DateTime.UtcNow;
        item.IsDelete = true;

        _db.Entry(item).State = EntityState.Modified;
        var result = await _db.SaveChangesAsync();

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

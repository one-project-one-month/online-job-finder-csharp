namespace online_job_finder.Domain.Services.RoleServices;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _db;

    public RoleRepository()
    {
        _db = new AppDbContext();
    }

    public async Task<RolesViewModels> CreateRole(RolesViewModels model)
    {
        model.Version += 1;
        model.UpdatedAt = null;

        TblRole roles = RolesMapping(model);

        await _db.TblRoles.AddAsync(roles);
        await _db.SaveChangesAsync();

        return model;
    }

    public async Task<List<RolesViewModels>> GetRoles()
    {
        var model = await _db.TblRoles
            .AsNoTracking()
            .Where(x => x.IsDelete == false)
            .OrderBy(x => x.Version)
            .ToListAsync();

        var rolesViewModels = model.Select(RolesViewModelsMapping).ToList();

        return rolesViewModels;
    }

    public async Task<RolesViewModels?> GetRole(string id)
    {
        var model = await _db.TblRoles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.RoleId.ToString() == id
            && x.IsDelete == false);

        if (model is null) { return null; }

        var mappingModel = RolesViewModelsMapping(model);

        return mappingModel;
    }

    public async Task<RolesViewModels?> UpdateRole(string id, RolesViewModels models)
    {
        var item = await _db.TblRoles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.RoleId.ToString() == id
            && x.IsDelete == false);
        if (item is null) { return null; }

        if (!string.IsNullOrEmpty(models.RoleName))
        {
            item.RoleName = models.RoleName;
        }

        item.Version += 1;
        item.UpdatedAt = DateTime.UtcNow;

        _db.Entry(item).State = EntityState.Modified;
        await _db.SaveChangesAsync();

        models = RolesViewModelsMapping(item);

        return models;
    }

    public async Task<RolesViewModels?> PatchRole(string id, RolesViewModels models)
    {
        var item = await _db.TblRoles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.RoleId.ToString() == id
            && x.IsDelete == false);
        if (item is null) { return null; }

        if (!string.IsNullOrEmpty(models.RoleName))
        {
            item.RoleName = models.RoleName;
        }

        item.Version += 1;
        item.UpdatedAt = DateTime.UtcNow;

        _db.Entry(item).State = EntityState.Modified;
        await _db.SaveChangesAsync();

        models = RolesViewModelsMapping(item);
        return models;
    }

    public async Task<bool?> DeleteRole(string id)
    {
        var item = await _db.TblRoles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.RoleId.ToString() == id
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

    //Can use for every roles
    private static TblRole RolesMapping(RolesViewModels roles)
    {
        return new TblRole
        {
            RoleId = Guid.NewGuid(),
            RoleName = roles.RoleName,
            Version = roles.Version,
            CreatedAt = roles.CreatedAt,
            UpdatedAt = roles.UpdatedAt,
            IsDelete = false
        };
    }

    private static RolesViewModels RolesViewModelsMapping(TblRole roles)
    {
        return new RolesViewModels
        {
            //RoleId = Guid.NewGuid(),
            RoleName = roles.RoleName,
            Version = roles.Version,
            CreatedAt = roles.CreatedAt,
            UpdatedAt = roles.UpdatedAt,
            //IsDelete = false
        };
    }

}

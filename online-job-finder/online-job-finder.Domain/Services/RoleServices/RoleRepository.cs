using Microsoft.EntityFrameworkCore;
using online_job_finder.DataBase.Models;
using online_job_finder.Domain.ViewModels;

namespace online_job_finder.Domain.Services.RoleServices;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _db;

    public RoleRepository()
    {
        _db = new AppDbContext();
    }

    // BLL
    public RolesViewModels CreateRole(RolesViewModels model)
    {
        model.Version += 1;
        model.UpdatedAt = null;

        TblRole roles = RolesMapping(model);

        _db.TblRoles.Add(roles);
        _db.SaveChanges();

        return model;
    }

    public List<RolesViewModels> GetRoles()
    {
        var model = _db.TblRoles
            .AsNoTracking()
            .Where(x => x.IsDelete == false)
            .OrderBy(x => x.Version)
            .ToList();

        var rolesViewModels = model.Select(RolesViewModelsMapping).ToList();

        return rolesViewModels;
    }

    public RolesViewModels? GetRole(string id)
    {
        var model = _db.TblRoles
            .AsNoTracking()
            .FirstOrDefault(x => x.RoleId.ToString() == id
            && x.IsDelete == false);

        if (model is null) { return null; }

        var mappingModel = RolesViewModelsMapping(model);

        return mappingModel;
    }

    public RolesViewModels? UpdateRole(string id, RolesViewModels models)
    {
        var item = _db.TblRoles
            .AsNoTracking()
            .FirstOrDefault(x => x.RoleId.ToString() == id
            && x.IsDelete == false);
        if (item is null) { return null; }

        if (!string.IsNullOrEmpty(models.RoleName))
        {
            item.RoleName = models.RoleName;
        }

        item.Version += 1;
        item.UpdatedAt = DateTime.UtcNow;

        _db.Entry(item).State = EntityState.Modified;
        _db.SaveChanges();

        models = RolesViewModelsMapping(item);

        return models;
    }

    public RolesViewModels? PatchRole(string id, RolesViewModels models)
    {
        var item = _db.TblRoles
            .AsNoTracking()
            .FirstOrDefault(x => x.RoleId.ToString() == id
            && x.IsDelete == false);
        if (item is null) { return null; }

        if (!string.IsNullOrEmpty(models.RoleName))
        {
            item.RoleName = models.RoleName;
        }

        item.Version += 1;
        item.UpdatedAt = DateTime.UtcNow;

        _db.Entry(item).State = EntityState.Modified;
        _db.SaveChanges();

        models = RolesViewModelsMapping(item);
        return models;
    }

    public bool? DeleteRole(string id)
    {
        var item = _db.TblRoles
            .AsNoTracking()
            .FirstOrDefault(x => x.RoleId.ToString() == id
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

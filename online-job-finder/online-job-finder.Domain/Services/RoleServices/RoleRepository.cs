using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using online_job_finder.DataBase.Models;
using online_job_finder.Domain.Services.UsersServices;
using online_job_finder.Domain.ViewModels.Roles;
using online_job_finder.Domain.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace online_job_finder.Domain.Services.RoleServices;

public class RoleRepository
{
    private readonly AppDbContext _db;

    public RoleRepository()
    {
        _db = new AppDbContext();
    }

    // BLL
    public TblRole CreateRole(RolesViewModels roles)
    {

        TblRole rolesModel = RolesMapping(roles);

        rolesModel.UpdatedAt = null; // Need to amend and take out

        _db.TblRoles.Add(rolesModel);
        _db.SaveChanges();

        return rolesModel;
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
            && x.IsDelete == false );

        if (model is null) { return null; }

        var mappingModel = RolesViewModelsMapping(model);

        return mappingModel;
    }

    public RolesViewModels? UpdateRole(string id, RolesViewModels roles)
    {
        var item = _db.TblRoles
            .AsNoTracking()
            .FirstOrDefault(x => x.RoleId.ToString() == id
            && x.IsDelete == false);
        if (item is null) { return null; }

        if (!string.IsNullOrEmpty(roles.RoleName))
        {
            item.RoleName = roles.RoleName;
        }

        item.UpdatedAt = DateTime.UtcNow;

        _db.Entry(item).State = EntityState.Modified;
        _db.SaveChanges();

        var model = RolesViewModelsMapping(item);

        return model;
    }

    public RolesViewModels? PatchRole(string id, RolesViewModels roles)
    {
        var item = _db.TblRoles
            .AsNoTracking()
            .FirstOrDefault(x => x.RoleId.ToString() == id
            && x.IsDelete == false);
        if (item is null) { return null; }

        if (!string.IsNullOrEmpty(roles.RoleName))
        {
            item.RoleName = roles.RoleName;
        }

        item.UpdatedAt = DateTime.UtcNow;

        _db.Entry(item).State = EntityState.Modified;
        _db.SaveChanges();

        var model = RolesViewModelsMapping(item);
        return roles;
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
            //Version = roles.Version,
            //CreatedAt = roles.CreatedAt,
            //UpdatedAt = roles.UpdatedAt,
            IsDelete = false
        };
    }

    private static RolesViewModels RolesViewModelsMapping(TblRole roles)
    {
        return new RolesViewModels
        {
            //RoleId = Guid.NewGuid(),
            RoleName = roles.RoleName,
            //Version = roles.Version,
            //CreatedAt = roles.CreatedAt,
            //UpdatedAt = roles.UpdatedAt,
            //IsDelete = false
        };
    }

}

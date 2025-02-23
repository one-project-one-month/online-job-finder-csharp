using online_job_finder.Domain.ViewModels;

namespace online_job_finder.Domain.Services.RoleServices;

public interface IRoleRepository
{
    RolesViewModels CreateRole(RolesViewModels roles);

    List<RolesViewModels> GetRoles();

    RolesViewModels? GetRole(string id);

    RolesViewModels? UpdateRole(string id, RolesViewModels roles);

    RolesViewModels? PatchRole(string id, RolesViewModels roles);

    bool? DeleteRole(string id);
}

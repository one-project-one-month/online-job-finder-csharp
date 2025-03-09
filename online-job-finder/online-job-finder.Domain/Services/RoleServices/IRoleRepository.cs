namespace online_job_finder.Domain.Services.RoleServices;

public interface IRoleRepository
{
    Task<RolesViewModels> CreateRole(RolesViewModels roles);

    Task<List<RolesViewModels>> GetRoles();

    Task<RolesViewModels?> GetRole(string id);

    Task<RolesViewModels?> UpdateRole(string id, RolesViewModels roles);

    Task<RolesViewModels?> PatchRole(string id, RolesViewModels roles);

    Task<bool?> DeleteRole(string id);
}

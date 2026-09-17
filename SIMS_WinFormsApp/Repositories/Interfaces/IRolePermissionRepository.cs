using System.Collections.Generic;
using SIMS_WinFormsApp.Models.Permission;

namespace SIMS_WinFormsApp.Repositories.Interfaces
{

    public interface IRolePermissionRepository
    {
        HashSet<AppPermission> GetPermissionsForRole(int roleId);

        bool SavePermissionsForRole(int roleId, HashSet<AppPermission> permissions);

        void EnsureCatalogSeeded();
    }
}
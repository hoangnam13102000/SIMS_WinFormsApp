using System;
using SIMS_WinFormsApp.Models.Enums;
using SIMS_WinFormsApp.Models.Permission;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.Services.AI.Interfaces;
using SIMS_WinFormsApp.Services.Session;

namespace SIMS_WinFormsApp.Services.AI.Implementations
{
    public sealed class AiToolPermissionPolicy : IAiToolPermissionPolicy
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public AiToolPermissionPolicy(IRolePermissionRepository rolePermissionRepository)
        {
            _rolePermissionRepository = rolePermissionRepository
                ?? throw new ArgumentNullException(nameof(rolePermissionRepository));
        }

        public bool IsAllowed(IUserSession session, AppPermission permission)
        {
            var user = session == null ? null : session.CurrentUser;
            if (user == null) return false;
            if (string.Equals(user.RoleCode, RoleCodes.Admin, StringComparison.OrdinalIgnoreCase))
                return true;

            return _rolePermissionRepository.GetPermissionsForRole(user.RoleId).Contains(permission);
        }
    }
}

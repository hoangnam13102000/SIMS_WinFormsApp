using System.Collections.Generic;
using System.Linq;

namespace SIMS_WinFormsApp.Models.Permission
{
    public sealed class PermissionSet
    {
        /// <summary>Tập rỗng - dùng cho vai trò/người dùng chưa có quyền nào.</summary>
        public static readonly PermissionSet Empty = new PermissionSet(new HashSet<AppPermission>());

        private readonly HashSet<AppPermission> _permissions;

        private PermissionSet(HashSet<AppPermission> permissions)
        {
            _permissions = permissions;
        }

        public static PermissionSet Of(IEnumerable<AppPermission> permissions)
        {
            return new PermissionSet(permissions == null
                ? new HashSet<AppPermission>()
                : new HashSet<AppPermission>(permissions));
        }

        public bool Has(AppPermission permission) => _permissions.Contains(permission);

        public bool HasAny(params AppPermission[] required)
        {
            if (required == null) return false;
            return required.Any(p => _permissions.Contains(p));
        }

        public bool HasAll(params AppPermission[] required)
        {
            if (required == null) return true;
            return required.All(p => _permissions.Contains(p));
        }

        public IReadOnlyCollection<AppPermission> AsCollection() => _permissions;
    }
}
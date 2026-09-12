using System.Collections.Generic;
using System.Linq;
using SIMS_WinFormsApp.DAL.Linq;

namespace SIMS_WinFormsApp.Repositories.Implementations
{
 
    public class PermissionRepository
    {
        public HashSet<string> GetPermissionCodesForRole(int roleId)
        {
            using (var db = new SimsDataContext())
            {
                var codes =
                    from rp in db.RolePermissions
                    join p in db.Permissions on rp.PermissionID equals p.PermissionID
                    where rp.RoleID == roleId
                    select p.PermissionCode;

                return new HashSet<string>(codes);
            }
        }

        public bool RoleHasPermission(int roleId, string permissionCode)
        {
            using (var db = new SimsDataContext())
            {
                return (
                    from rp in db.RolePermissions
                    join p in db.Permissions on rp.PermissionID equals p.PermissionID
                    where rp.RoleID == roleId && p.PermissionCode == permissionCode
                    select rp
                ).Any();
            }
        }
    }
}
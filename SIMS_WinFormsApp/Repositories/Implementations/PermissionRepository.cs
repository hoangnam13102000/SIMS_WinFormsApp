using System.Collections.Generic;
using System.Linq;
using SIMS_WinFormsApp.DAL.Linq;
using SIMS_WinFormsApp.DAL.Linq.Entities.Indetity;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Repositories.Interfaces;

namespace SIMS_WinFormsApp.Repositories.Implementations
{

    public class PermissionRepository : IPermissionRepository
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

        /// <summary>MỚI: toàn bộ danh mục quyền hạn, sắp theo mã quyền - dùng để dựng ma trận
        /// phân quyền ở trang Quản lý phân quyền vai trò.</summary>
        public IReadOnlyList<PermissionDto> GetAllPermissions()
        {
            using (var db = new SimsDataContext())
            {
                return db.Permissions
                    .OrderBy(p => p.PermissionCode)
                    .ToList()
                    .Select(p => new PermissionDto
                    {
                        PermissionId = p.PermissionID,
                        PermissionCode = p.PermissionCode,
                        Description = p.Description
                    })
                    .ToList();
            }
        }

        /// <summary>MỚI: cấp/thu hồi 1 quyền cho 1 vai trò - thêm/xóa đúng 1 dòng trong bảng
        /// RolePermissions (idempotent: gọi lại nhiều lần với cùng trạng thái không gây lỗi).</summary>
        public void SetRolePermission(int roleId, int permissionId, bool granted)
        {
            using (var db = new SimsDataContext())
            {
                var existing = db.RolePermissions
                    .FirstOrDefault(rp => rp.RoleID == roleId && rp.PermissionID == permissionId);

                if (granted)
                {
                    if (existing == null)
                    {
                        db.RolePermissions.InsertOnSubmit(new RolePermissionEntity
                        {
                            RoleID = roleId,
                            PermissionID = permissionId
                        });
                        db.SubmitChanges();
                    }
                }
                else if (existing != null)
                {
                    db.RolePermissions.DeleteOnSubmit(existing);
                    db.SubmitChanges();
                }
            }
        }
    }
}
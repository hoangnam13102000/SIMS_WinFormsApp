using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs;

namespace SIMS_WinFormsApp.Repositories.Interfaces
{
    public interface IPermissionRepository
    {
        /// <summary>Đã có sẵn trong PermissionRepository - hình thức hóa lại contract.</summary>
        HashSet<string> GetPermissionCodesForRole(int roleId);

        /// <summary>Đã có sẵn trong PermissionRepository - hình thức hóa lại contract.</summary>
        bool RoleHasPermission(int roleId, string permissionCode);

        /// <summary>MỚI: toàn bộ danh mục quyền hạn trong hệ thống, dùng để dựng ma trận phân quyền.</summary>
        IReadOnlyList<PermissionDto> GetAllPermissions();

        /// <summary>MỚI: cấp (granted=true) hoặc thu hồi (granted=false) 1 quyền hạn cho 1 vai trò.</summary>
        void SetRolePermission(int roleId, int permissionId, bool granted);
    }
}
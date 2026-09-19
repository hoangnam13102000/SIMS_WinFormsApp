using System;
using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs;

namespace SIMS_WinFormsApp.Views.Interfaces
{
    /// <summary>Dữ liệu kèm theo khi người dùng bật/tắt 1 quyền hạn cho vai trò đang chọn.</summary>
    public sealed class PermissionToggleEventArgs : EventArgs
    {
        public int PermissionId { get; }
        public string PermissionCode { get; }
        public bool Granted { get; }

        public PermissionToggleEventArgs(int permissionId, string permissionCode, bool granted)
        {
            PermissionId = permissionId;
            PermissionCode = permissionCode;
            Granted = granted;
        }
    }

    public interface IRoleManagementView
    {
        /// <summary>Id của vai trò đang được chọn trong danh sách bên trái; 0 nếu chưa chọn gì.</summary>
        int SelectedRoleId { get; }

        void DisplayRoles(IReadOnlyList<RoleOptionDto> roles);

        /// <summary>Hiển thị tên/mô tả vai trò cùng ma trận toàn bộ quyền hạn, đánh dấu sẵn những
        /// quyền vai trò này đang có (grantedCodes).</summary>
        void DisplayPermissions(RoleOptionDto role, IReadOnlyList<PermissionDto> permissions, HashSet<string> grantedCodes);

        /// <summary>Người dùng chọn 1 vai trò khác trong danh sách bên trái.</summary>
        event EventHandler RoleSelected;

        /// <summary>Người dùng bật/tắt 1 quyền hạn cho vai trò đang chọn.</summary>
        event EventHandler<PermissionToggleEventArgs> PermissionToggled;

        void ShowError(string message);
        void ShowSuccess(string message);
    }
}
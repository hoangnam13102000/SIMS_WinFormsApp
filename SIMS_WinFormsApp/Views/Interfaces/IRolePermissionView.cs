using System;
using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs.Permission;
using SIMS_WinFormsApp.Models.Permission;

namespace SIMS_WinFormsApp.Views.Interfaces
{
    public interface IRolePermissionView
    {
        /// <summary>View đã dựng xong control và sẵn sàng nhận dữ liệu.</summary>
        event EventHandler ViewReady;

        /// <summary>Người dùng chọn 1 vai trò ở danh sách bên trái (tham số: RoleId).</summary>
        event EventHandler<int> RoleSelected;

        /// <summary>Người dùng gõ vào ô tìm kiếm vai trò (tham số: từ khoá).</summary>
        event EventHandler<string> RoleSearchTextChanged;

        /// <summary>Người dùng bật/tắt 1 quyền bất kỳ trên danh sách quyền bên phải.</summary>
        event EventHandler<PermissionToggledEventArgs> PermissionToggled;

        /// <summary>Người dùng bấm nút "Lưu thay đổi".</summary>
        event EventHandler SaveRequested;

        /// <summary>Người dùng bấm nút "Khôi phục mặc định".</summary>
        event EventHandler ResetRequested;

        /// <summary>Hiển thị danh sách vai trò bên trái (đã lọc/sắp xếp sẵn từ Presenter).</summary>
        void ShowRoles(IReadOnlyList<RolePermissionRoleRowDto> roles);

        /// <summary>Hiển thị danh sách nhóm quyền bên phải cho vai trò đang chọn.</summary>
        void ShowPermissionGroups(string roleTitle, bool isAdminRole, IReadOnlyList<PermissionGroupViewModel> groups);

        /// <summary>Bật/tắt trạng thái đang tải (overlay + khoá nút Lưu/Khôi phục/ô tìm kiếm).</summary>
        void SetBusy(bool isBusy, string message);

        void ShowError(string title, string message);

        void ShowSuccess(string title, string message);

        void ShowInfo(string title, string message);
    }

    /// <summary>Tham số sự kiện khi người dùng bật/tắt 1 quyền trên UI.</summary>
    public sealed class PermissionToggledEventArgs : EventArgs
    {
        public AppPermission Permission { get; }
        public bool IsChecked { get; }

        public PermissionToggledEventArgs(AppPermission permission, bool isChecked)
        {
            Permission = permission;
            IsChecked = isChecked;
        }
    }
}
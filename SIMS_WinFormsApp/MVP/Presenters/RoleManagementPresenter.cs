using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.UI.Controls.Loading;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.MVP.Presenters
{
    /// <summary>
    /// Điều phối trang "Quản lý phân quyền vai trò": tải danh sách vai trò + toàn bộ quyền hạn +
    /// quyền hạn hiện có của vai trò đang chọn, và xử lý việc cấp/thu hồi quyền khi người dùng
    /// bật/tắt toggle. Không biết gì về UI cụ thể (chỉ phụ thuộc <see cref="IRoleManagementView"/>
    /// và <see cref="ILoadingIndicator"/>) - đúng tinh thần Dependency Inversion của mô hình MVP
    /// đang dùng xuyên suốt dự án.
    /// </summary>
    public sealed class RoleManagementPresenter
    {
        private readonly IRoleManagementView _view;
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly ILoadingIndicator _loadingIndicator;

        private IReadOnlyList<RoleOptionDto> _roles = Array.Empty<RoleOptionDto>();
        private IReadOnlyList<PermissionDto> _allPermissions = Array.Empty<PermissionDto>();

        public RoleManagementPresenter(
            IRoleManagementView view,
            IRoleRepository roleRepository,
            IPermissionRepository permissionRepository,
            ILoadingIndicator loadingIndicator = null)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _permissionRepository = permissionRepository ?? throw new ArgumentNullException(nameof(permissionRepository));
            _loadingIndicator = loadingIndicator; // optional - null vẫn chạy được, chỉ là không có lớp phủ

            _view.RoleSelected += OnRoleSelected;
            _view.PermissionToggled += OnPermissionToggled;
        }

        /// <summary>Tải lần đầu: danh sách vai trò + toàn bộ danh mục quyền hạn (2 câu truy vấn
        /// có thể mất vài giây tùy số lượng bản ghi) + quyền hạn của vai trò đầu tiên.</summary>
        public void Load()
        {
            _loadingIndicator?.ShowLoading("Đang tải danh sách vai trò và quyền hạn...");
            Application.DoEvents(); // ép vẽ lớp phủ trước khi các câu truy vấn đồng bộ bên dưới chạy
            try
            {
                _roles = _roleRepository.GetAssignableRoles();
                _allPermissions = _permissionRepository.GetAllPermissions();

                _view.DisplayRoles(_roles);

                var firstRole = _roles.FirstOrDefault();
                if (firstRole != null)
                {
                    var granted = _permissionRepository.GetPermissionCodesForRole(firstRole.RoleId);
                    _view.DisplayPermissions(firstRole, _allPermissions, granted);
                }
            }
            catch (Exception ex)
            {
                _view.ShowError("Không thể tải danh sách vai trò/quyền hạn: " + ex.Message);
            }
            finally
            {
                _loadingIndicator?.HideLoading();
            }
        }

        private void OnRoleSelected(object sender, EventArgs e)
        {
            int roleId = _view.SelectedRoleId;
            if (roleId <= 0) return;

            _loadingIndicator?.ShowLoading("Đang tải quyền hạn của vai trò...");
            Application.DoEvents();
            try
            {
                var role = _roles.FirstOrDefault(r => r.RoleId == roleId);
                var granted = _permissionRepository.GetPermissionCodesForRole(roleId);
                _view.DisplayPermissions(role, _allPermissions, granted);
            }
            catch (Exception ex)
            {
                _view.ShowError("Không thể tải quyền hạn của vai trò: " + ex.Message);
            }
            finally
            {
                _loadingIndicator?.HideLoading();
            }
        }

        private void OnPermissionToggled(object sender, PermissionToggleEventArgs e)
        {
            int roleId = _view.SelectedRoleId;
            if (roleId <= 0) return;

            try
            {
                _permissionRepository.SetRolePermission(roleId, e.PermissionId, e.Granted);
                _view.ShowSuccess(e.Granted
                    ? $"Đã cấp quyền \"{e.PermissionCode}\"."
                    : $"Đã thu hồi quyền \"{e.PermissionCode}\".");
            }
            catch (Exception ex)
            {
                _view.ShowError("Không thể cập nhật quyền hạn: " + ex.Message);
            }
        }
    }
}
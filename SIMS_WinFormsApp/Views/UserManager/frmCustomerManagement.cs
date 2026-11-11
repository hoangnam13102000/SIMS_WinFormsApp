using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.Enums;
using SIMS_WinFormsApp.Models.Mapping;
using SIMS_WinFormsApp.Models.Permission;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Toast;
using SIMS_WinFormsApp.Views.Dialogs;

namespace SIMS_WinFormsApp.Views.UserManager
{
    public partial class frmCustomerManagement : Form
    {
        private readonly List<UserManagementRowDto> _currentRows = new List<UserManagementRowDto>();
        private readonly UserManagementPager _pager;
        private IUserManagementService _service;
        private bool _canEdit;
        private bool _canManage;
        private int _loadGeneration;

        public frmCustomerManagement()
        {
            InitializeComponent();
            if (!DesignMode && LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                _viewDetailsMenuItem.Visible = false;
                _editUserMenuItem.Visible = false;
                _toggleLockMenuItem.Visible = false;
            }
            _pager = new UserManagementPager();
            _contentLayout.Controls.Add(_pager, 0, 3);
            _pager.PageChanged += (sender, args) => ReloadUsers();
            _statusFilter.SelectedIndex = 0;
            AddUserRow("customer01", "Khách hàng mẫu", "customer@example.com", "Khách hàng", "Đang hoạt động", false);
            ClearUserSelection();
        }

        public frmCustomerManagement(IUserManagementService service, PermissionSet permissions) : this()
        {
            if (service == null) throw new ArgumentNullException(nameof(service));
            TopLevel = false;
            FormBorderStyle = FormBorderStyle.None;
            Dock = DockStyle.Fill;
            _searchBox.Clear();
            _service = service;
            ConfigurePermissions(permissions);
        }

        private void ConfigurePermissions(PermissionSet permissions)
        {
            permissions = permissions ?? PermissionSet.Empty;
            _canManage = permissions.Has(AppPermission.CUSTOMER_MANAGE);
            _canEdit = _canManage || permissions.Has(AppPermission.CUSTOMER_EDIT);
            _viewDetailsMenuItem.Visible = true;
            _editUserMenuItem.Visible = _canEdit;
            _toggleLockMenuItem.Visible = _canManage;
            _editButton.Visible = _canEdit;
            _lockButton.Visible = _canManage;
            UpdateActionButtons();
        }

        // ===== XEM CHI TIẾT =====
        private void _viewButton_Click(object sender, EventArgs e)
        {
            UserManagementRowDto user = SelectedUser();
            if (user == null)
            {
                AppToast.Warning(this, "Vui lòng chọn một khách hàng.");
                return;
            }
            if (_service == null) return;
            frmUserAccountDetail.Show(this, UserDetailMapper.FromRow(user), _service);
        }

        // ===== SỬA =====
        private void _editButton_Click(object sender, EventArgs e)
        {
            if (!_canEdit)
            {
                AppToast.Warning(this, "Bạn không có quyền sửa thông tin khách hàng.");
                return;
            }
            UserManagementRowDto user = SelectedUser();
            if (user == null)
            {
                AppToast.Warning(this, "Vui lòng chọn khách hàng cần sửa.");
                return;
            }
            if (frmEditUserAccount.Show(this, UserDetailMapper.FromRow(user), _service) == DialogResult.OK)
                ReloadUsers();
        }

        // ===== KHÓA / MỞ KHÓA =====
        private void _lockButton_Click(object sender, EventArgs e)
        {
            if (!_canManage)
            {
                AppToast.Warning(this, "Bạn không có quyền khóa hoặc mở khóa khách hàng.");
                return;
            }
            UserManagementRowDto user = SelectedUser();
            if (user == null)
            {
                AppToast.Warning(this, "Vui lòng chọn khách hàng.");
                return;
            }
            bool shouldLock = !user.IsLocked;
            string action = shouldLock ? "khóa" : "mở khóa";
            if (!DialogHelper.Confirm(this, "Xác nhận thao tác",
                string.Format("Bạn có chắc muốn {0} tài khoản '{1}' không?", action, user.Username)))
                return;
            if (_service.SetAccountLocked(user.UserId, shouldLock) == SetAccountLockResult.Success)
            {
                AppToast.Success(this, "Đã " + action + " tài khoản " + user.Username + ".");
                ReloadUsers();
            }
            else
                AppToast.Warning(this, "Không tìm thấy tài khoản.");
        }

        // ===== TÌM KIẾM / LỌC / HIỆN FORM =====
        private void _searchBox_TextChanged(object sender, EventArgs e)
        {
            ReloadUsers(true);
        }

        private void _statusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadUsers(true);
        }

        private void _usersGrid_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible) ReloadUsers();
        }

        // ===== LƯỚI =====
        private void UsersGrid_SelectionChanged(object sender, EventArgs e)
        {
            UpdateActionButtons();
        }

        private void UsersGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _currentRows.Count)
                return;
            SelectUserRow(e.RowIndex);
            _viewButton_Click(sender, e);
        }

        private void UsersGrid_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || e.RowIndex < 0) return;
            _usersGrid.ClearSelection();
            _usersGrid.Rows[e.RowIndex].Selected = true;
            _usersGrid.CurrentCell = _usersGrid.Rows[e.RowIndex].Cells[0];
        }

        // ===== TẢI DỮ LIỆU =====
        private async void ReloadUsers(bool resetPage = false)
        {
            if (_service == null || IsDisposed) return;
            if (resetPage) _pager.ResetPage();
            int generation = ++_loadGeneration;
            IUserManagementService service = _service;
            int pageIndex = _pager.PageIndex;
            string searchTerm = _searchBox.Text;
            string status = _statusFilter.SelectedIndex == 1 ? "UNLOCKED" :
            _statusFilter.SelectedIndex == 2 ? "LOCKED" : null;

            try
            {
                UserManagementPageDto page = await Task.Run(() =>
                    service.GetPage(pageIndex, _pager.PageSize, searchTerm, RoleCodes.Customer, status));
                if (IsDisposed || generation != _loadGeneration) return;
                if (_pager.SetTotalCount(page.TotalCount))
                {
                    ReloadUsers();
                    return;
                }

                _currentRows.Clear();
                _currentRows.AddRange(page.Rows);
                _usersGrid.Rows.Clear();
                foreach (UserManagementRowDto user in _currentRows)
                {
                    AddUserRow(user.Username, user.FullName, user.Email, user.RoleName,
                        string.Equals(user.Status, "ACTIVE", StringComparison.OrdinalIgnoreCase) ? "Đang hoạt động" : "Vô hiệu hóa",
                        user.IsLocked);
                }
                ClearUserSelection();
            }
            catch (Exception ex)
            {
                if (!IsDisposed && generation == _loadGeneration)
                    AppToast.Error(this, "Lỗi tải danh sách khách hàng", ex.Message);
            }
        }

        // ===== HÀM PHỤ =====
        private void SelectUserRow(int rowIndex)
        {
            _usersGrid.CurrentCell = _usersGrid.Rows[rowIndex].Cells[0];
            _usersGrid.Rows[rowIndex].Selected = true;
        }

        private void ClearUserSelection()
        {
            _usersGrid.ClearSelection();
            _usersGrid.CurrentCell = null;
            UpdateActionButtons();
        }

        private void UpdateActionButtons()
        {
            UserManagementRowDto user = SelectedUser();
            bool hasSelection = user != null;
            _viewButton.Enabled = hasSelection;
            _editButton.Enabled = hasSelection && _canEdit;
            _lockButton.Enabled = hasSelection && _canManage;
            _lockButton.Text = user != null && user.IsLocked ? "Mở khóa" : "Khóa";
            _lockButton.ForeColor = user != null && user.IsLocked ? AppColors.Success : AppColors.Error;
        }

        private void AddUserRow(string username, string fullName, string email, string role, string status, bool isLocked)
        {
            int index = _usersGrid.Rows.Add(username, fullName, email, role, status,
                isLocked ? "✕ Đang khóa" : "✓ Bình thường");
            DataGridViewCell lockCell = _usersGrid.Rows[index].Cells["Lock"];
            lockCell.Style.ForeColor = isLocked ? AppColors.Error : AppColors.Success;
            lockCell.Style.SelectionForeColor = lockCell.Style.ForeColor;
        }

        private UserManagementRowDto SelectedUser()
        {
            int index = _usersGrid.SelectedRows.Count == 0 ? -1 : _usersGrid.SelectedRows[0].Index;
            return index >= 0 && index < _currentRows.Count ? _currentRows[index] : null;
        }

        // TẠM: xóa hàm này sau khi đã xử lý nút button1 trong Designer
        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}

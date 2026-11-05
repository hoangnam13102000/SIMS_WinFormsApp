using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.Mapping;
using SIMS_WinFormsApp.Models.Enums;
using SIMS_WinFormsApp.Models.Permission;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Toast;
using SIMS_WinFormsApp.Views.Dialogs;

namespace SIMS_WinFormsApp.Views.UserManager
{
    public partial class frmUserManagement : Form
    {
        private readonly List<UserManagementRowDto> _currentRows = new List<UserManagementRowDto>();
        private readonly ContextMenuStrip _rowMenu;
        private readonly UserManagementPager _pager;
        private IUserManagementService _service;
        private bool _canEdit;
        private bool _canManage;
        private int _loadGeneration;

        public frmUserManagement()
        {
            InitializeComponent();
            _pager = new UserManagementPager();
            _contentLayout.Controls.Add(_pager, 0, 3);
            _headerPanel.SizeChanged += (sender, args) => LayoutHeader();
            _toolbar.SizeChanged += (sender, args) => LayoutFilters();
            _refreshButton.Click += (sender, args) => ReloadUsers(true);
            LayoutHeader();
            LayoutFilters();
            _pager.PageChanged += (sender, args) => ReloadUsers();
            _rowMenu = new ContextMenuStrip(components);
            AddUserRow("admin", "Quản trị hệ thống", "admin@example.com", "Quản trị viên", "Đang hoạt động", false);
            AddUserRow("nhanvien01", "Nhân viên mẫu", "staff@example.com", "Nhân viên", "Đang hoạt động", false);

            _statusFilter.SelectedIndex = 0;
            _searchBox.TextChanged += (sender, args) => ReloadUsers(true);
            _statusFilter.SelectedIndexChanged += (sender, args) => ReloadUsers(true);
            _usersGrid.CellDoubleClick += UsersGrid_CellDoubleClick;
            _usersGrid.CellMouseDown += UsersGrid_CellMouseDown;
            _usersGrid.CellMouseClick += UsersGrid_CellMouseClick;
            VisibleChanged += (sender, args) =>
            {
                if (Visible)
                    ReloadUsers();
            };
            _usersGrid.ContextMenuStrip = _rowMenu;
        }

        private void LayoutHeader()
        {
            _refreshButton.Location = new Point(_headerPanel.ClientSize.Width - 146, 28);
            _titleLabel.Width = Math.Max(180, _refreshButton.Left - _titleLabel.Left - 16);
        }

        private void LayoutFilters()
        {
            int statusLeft = Math.Max(252, _toolbar.ClientSize.Width - 340);
            _searchBox.Width = Math.Max(140, statusLeft - 32);
            _statusLabel.Location = new Point(statusLeft, 8);
            _statusFilter.Location = new Point(statusLeft, 31);
            _statusFilter.Width = Math.Max(180, _toolbar.ClientSize.Width - statusLeft - 32);
        }

        public frmUserManagement(IUserManagementService service, PermissionSet permissions) : this()
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

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
            _canManage = permissions.Has(AppPermission.USER_MANAGE);
            _canEdit = _canManage || permissions.Has(AppPermission.USER_EDIT);

            DataGridViewColumn actionsColumn = _usersGrid.Columns["Actions"];
            actionsColumn.Visible = _canEdit || _canManage;
            actionsColumn.ToolTipText = _canManage
                ? "Xem, sửa hoặc khóa / mở khóa"
                : _canEdit ? "Xem hoặc sửa" : "Xem chi tiết";
            _rowMenu.Items.Insert(0, new ToolStripMenuItem("Xem chi tiết", null, (sender, args) => ShowSelectedUser()));
            if (_canEdit)
                _rowMenu.Items.Add("Sửa tài khoản", null, (sender, args) => EditSelectedUser());
            if (_canManage)
                _rowMenu.Items.Add("Khóa / mở khóa", null, (sender, args) => ToggleSelectedUserLock());
        }

        private async void ReloadUsers(bool resetPage = false)
        {
            if (_service == null || IsDisposed)
                return;

            if (resetPage)
                _pager.ResetPage();
            int generation = ++_loadGeneration;
            IUserManagementService service = _service;
            int pageIndex = _pager.PageIndex;
            string searchTerm = _searchBox.Text;
            string statusFilter = null;
            if (_statusFilter.SelectedIndex == 1)
                statusFilter = "ACTIVE";
            else if (_statusFilter.SelectedIndex == 2)
                statusFilter = "INACTIVE";

            try
            {
                UserManagementPageDto page = await Task.Run(() =>
                    service.GetPage(pageIndex, _pager.PageSize, searchTerm, UserManagementFilters.NonCustomer, statusFilter));
                if (IsDisposed || generation != _loadGeneration)
                    return;
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
                    AddUserRow(
                        user.Username,
                        user.FullName,
                        user.Email,
                        user.RoleName,
                        string.Equals(user.Status, "ACTIVE", StringComparison.OrdinalIgnoreCase)
                            ? "Đang hoạt động"
                            : "Vô hiệu hóa",
                        user.IsLocked);
                }
            }
            catch (Exception ex)
            {
                if (!IsDisposed && generation == _loadGeneration)
                    AppToast.Error(this, "Lỗi tải danh sách tài khoản", ex.Message);
            }
        }

        private void UsersGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                ShowSelectedUser(e.RowIndex);
        }

        private void UsersGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || e.RowIndex < 0 || e.RowIndex >= _currentRows.Count ||
                _usersGrid.Columns[e.ColumnIndex].Name != "Actions" || (!_canEdit && !_canManage))
                return;

            _usersGrid.CurrentCell = _usersGrid.Rows[e.RowIndex].Cells[0];
            const int iconSize = 24;
            const int gap = 8;
            int stripWidth = iconSize * 3 + gap * 2;
            int offset = e.X - (_usersGrid.Columns[e.ColumnIndex].Width - stripWidth) / 2;
            int actionIndex = offset / (iconSize + gap);
            if (offset < 0 || actionIndex > 2 || offset % (iconSize + gap) >= iconSize)
                return;

            if (actionIndex == 0)
                ShowSelectedUser(e.RowIndex);
            else if (actionIndex == 1 && _canEdit)
                EditSelectedUser();
            else if (actionIndex == 2 && _canManage)
                ToggleSelectedUserLock();
        }

        private void AddUserRow(string username, string fullName, string email, string role, string status, bool isLocked)
        {
            _usersGrid.Rows.Add(
                username,
                fullName,
                email,
                role,
                status,
                IconBitmapCache.GetActionStrip(_canEdit, _canManage, isLocked));
        }

        private void UsersGrid_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || e.RowIndex < 0)
                return;

            _usersGrid.ClearSelection();
            _usersGrid.Rows[e.RowIndex].Selected = true;
            _usersGrid.CurrentCell = _usersGrid.Rows[e.RowIndex].Cells[0];
        }

        private UserManagementRowDto GetSelectedUser()
        {
            int rowIndex = _usersGrid.CurrentRow == null ? -1 : _usersGrid.CurrentRow.Index;
            return rowIndex >= 0 && rowIndex < _currentRows.Count
                ? _currentRows[rowIndex]
                : null;
        }

        private void ShowSelectedUser()
        {
            ShowSelectedUser(_usersGrid.CurrentRow == null ? -1 : _usersGrid.CurrentRow.Index);
        }

        private void ShowSelectedUser(int rowIndex)
        {
            if (_service == null || rowIndex < 0 || rowIndex >= _currentRows.Count)
                return;

            frmUserAccountDetail.Show(
                this,
                UserDetailMapper.FromRow(_currentRows[rowIndex]),
                _service);
        }

        private void EditSelectedUser()
        {
            if (!_canEdit)
            {
                AppToast.Warning(this, "Bạn không có quyền sửa thông tin tài khoản.");
                return;
            }
            UserManagementRowDto user = GetSelectedUser();
            if (_service == null || user == null)
                return;

            UserDetailDto detail = UserDetailMapper.FromRow(user);
            if (frmEditUserAccount.Show(this, detail, _service) == DialogResult.OK)
            {
                ReloadUsers();
                AppToast.Success(this, "Cập nhật tài khoản thành công.");
            }
        }

        private void ToggleSelectedUserLock()
        {
            if (!_canManage)
            {
                AppToast.Warning(this, "Bạn không có quyền khóa hoặc mở khóa tài khoản.");
                return;
            }
            UserManagementRowDto user = GetSelectedUser();
            if (_service == null || user == null)
                return;

            bool lockAccount = !user.IsLocked;
            string action = lockAccount ? "khóa" : "mở khóa";
            if (!DialogHelper.Confirm(
                    this,
                    "Xác nhận thao tác",
                    string.Format("Bạn có chắc muốn {0} tài khoản '{1}' không?", action, user.Username)))
            {
                return;
            }

            SetAccountLockResult result = _service.SetAccountLocked(user.UserId, lockAccount);
            if (result == SetAccountLockResult.Success)
                ReloadUsers();
            else
                AppToast.Warning(this, "Không tìm thấy tài khoản.");
        }
    }
}

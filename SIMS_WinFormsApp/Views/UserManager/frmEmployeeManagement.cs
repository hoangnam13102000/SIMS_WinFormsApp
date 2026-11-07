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
using SIMS_WinFormsApp.Services.Implementations.Export;
using SIMS_WinFormsApp.Services.Implementations.Import;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Toast;
using SIMS_WinFormsApp.Views.Dialogs;
using SIMS_WinFormsApp.Views.SystemManagement;

namespace SIMS_WinFormsApp.Views.UserManager
{
    public partial class frmEmployeeManagement : Form
    {
        private readonly List<UserManagementRowDto> _currentRows = new List<UserManagementRowDto>();
        private readonly UserManagementPager _pager;
        private IUserManagementService _service;
        private bool _canEdit;
        private bool _canManage;
        private int _loadGeneration;

        public frmEmployeeManagement()
        {
            InitializeComponent();
            if (!DesignMode && LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                _viewDetailsMenuItem.Visible = false;
                _editUserMenuItem.Visible = false;
                _toggleLockMenuItem.Visible = false;
                _importMenuItem.Visible = false;
            }
            _pager = new UserManagementPager();
            _contentLayout.Controls.Add(_pager, 0, 3);
            _headerPanel.SizeChanged += (sender, args) => LayoutHeader();
            _toolbar.SizeChanged += (sender, args) => LayoutFilters();
            LayoutHeader();
            LayoutFilters();
            _pager.PageChanged += (sender, args) => ReloadUsers();
            _statusFilter.SelectedIndex = 0;
            _searchBox.TextChanged += (sender, args) => ReloadUsers(true);
            _statusFilter.SelectedIndexChanged += (sender, args) => ReloadUsers(true);
            VisibleChanged += (sender, args) =>
            {
                if (Visible)
                    ReloadUsers();
            };
            _usersGrid.CellMouseClick += UsersGrid_CellMouseClick;
            _usersGrid.CellMouseDown += UsersGrid_CellMouseDown;
            DataGridViewCodeCopy.Attach(_usersGrid, "EmployeeId");
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            ReloadUsers(true);
        }

        private void OptionsButton_Click(object sender, EventArgs e)
        {
            _optionsMenu.Show(_optionsButton, 0, _optionsButton.Height);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            AddEmployee();
        }

        private void LockButton_Click(object sender, EventArgs e)
        {
            ToggleSelectedUserLock();
        }

        private void UsersGrid_SelectionChanged(object sender, EventArgs e)
        {
            UpdateActionButtons();
        }

        private void UsersGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _usersGrid.Rows.Count) return;
            SelectUserRow(e.RowIndex);
            ShowSelectedUser();
        }

        private void ViewDetailsMenuItem_Click(object sender, EventArgs e)
        {
            ShowSelectedUser();
        }

        private void EditUserMenuItem_Click(object sender, EventArgs e)
        {
            EditSelectedUser();
        }

        private void ToggleLockMenuItem_Click(object sender, EventArgs e)
        {
            ToggleSelectedUserLock();
        }

        private void ExportCsvMenuItem_Click(object sender, EventArgs e)
        {
            ExportUsers(new CsvTableExporter());
        }

        private void ExportExcelMenuItem_Click(object sender, EventArgs e)
        {
            ExportUsers(new ExcelTableExporter());
        }

        private void ImportMenuItem_Click(object sender, EventArgs e)
        {
            ImportUsers();
        }

        private void LayoutHeader()
        {
            int right = _headerPanel.ClientSize.Width - 16;
            bool compact = _headerPanel.ClientSize.Width < 900;
            int top = compact ? 10 : 29;
            _refreshButton.Location = new Point(right - _refreshButton.Width, top);
            right = _refreshButton.Left - 8;
            _optionsButton.Location = new Point(right - _optionsButton.Width, top);
            right = _optionsButton.Left - 8;
            _addButton.Location = new Point(right - _addButton.Width, top);
            if (compact)
            {
                right = _headerPanel.ClientSize.Width - 16;
                _lockButton.Location = new Point(right - _lockButton.Width, 52);
                _subtitleLabel.Visible = false;
            }
            else
            {
                right = _addButton.Left - 8;
                _lockButton.Location = new Point(right - _lockButton.Width, 29);
                _subtitleLabel.Visible = true;
                _subtitleLabel.Location = new Point(96, 70);
            }
            int titleRight = compact
                ? _addButton.Left
                : _lockButton.Left;
            _titleLabel.Width = Math.Max(0, titleRight - _titleLabel.Left - 16);
            _optionsButton.BringToFront();
            _addButton.BringToFront();
            _lockButton.BringToFront();
            _refreshButton.BringToFront();
        }

        private void LayoutFilters()
        {
            int statusLeft = Math.Max(252, _toolbar.ClientSize.Width - 340);
            _searchBox.Width = Math.Max(140, statusLeft - 32);
            _statusLabel.Location = new Point(statusLeft, 8);
            _statusFilter.Location = new Point(statusLeft, 31);
            _statusFilter.Width = Math.Max(180, _toolbar.ClientSize.Width - statusLeft - 32);
        }

        public frmEmployeeManagement(IUserManagementService service, PermissionSet permissions) : this()
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
            _canManage = permissions.Has(AppPermission.USER_MANAGE);
            _canEdit = _canManage || permissions.Has(AppPermission.USER_EDIT);

            _addButton.Visible = _canManage;
            DataGridViewColumn actionsColumn = _usersGrid.Columns["Actions"];
            actionsColumn.Visible = _canEdit || _canManage;
            actionsColumn.ToolTipText = _canManage
                ? "Xem, sửa hoặc khóa / mở khóa"
                : _canEdit ? "Xem hoặc sửa" : "Xem chi tiết";
            _viewDetailsMenuItem.Visible = true;
            _editUserMenuItem.Visible = _canEdit;
            _toggleLockMenuItem.Visible = _canManage;
            _importMenuItem.Visible = _canManage;
            UpdateActionButtons();
            LayoutHeader();
        }

        private async void ReloadUsers(bool resetPage = false)
        {
            if (_service == null || IsDisposed) return;
            if (resetPage) _pager.ResetPage();
            int generation = ++_loadGeneration;
            IUserManagementService service = _service;
            int pageIndex = _pager.PageIndex;
            string searchTerm = _searchBox.Text;
            string status = _statusFilter.SelectedIndex == 1 ? "ACTIVE" :
                _statusFilter.SelectedIndex == 2 ? "INACTIVE" : null;

            try
            {
                UserManagementPageDto page = await Task.Run(() =>
                    service.GetPage(pageIndex, _pager.PageSize, searchTerm, UserManagementFilters.NonCustomer, status));
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
                    AddUserRow(user.Username, user.FullName, user.EmployeeId, user.Email, user.RoleName,
                        string.Equals(user.Status, "ACTIVE", StringComparison.OrdinalIgnoreCase) ? "Đang hoạt động" : "Vô hiệu hóa",
                        user.IsLocked);
                }
                ClearUserSelection();
            }
            catch (Exception ex)
            {
                if (!IsDisposed && generation == _loadGeneration)
                    AppToast.Error(this, "Lỗi tải danh sách nhân viên", ex.Message);
            }
        }

        private void UsersGrid_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || e.RowIndex < 0) return;
            SelectUserRow(e.RowIndex);
        }

        private void UsersGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || e.RowIndex < 0 || e.RowIndex >= _currentRows.Count ||
                e.ColumnIndex < 0 || e.ColumnIndex >= _usersGrid.Columns.Count ||
                _usersGrid.Columns[e.ColumnIndex].Name != "Actions" || (!_canEdit && !_canManage)) return;
            SelectUserRow(e.RowIndex);
            const int iconSize = 24;
            const int gap = 8;
            int stripWidth = iconSize * 3 + gap * 2;
            int offset = e.X - (_usersGrid.Columns[e.ColumnIndex].Width - stripWidth) / 2;
            int actionIndex = offset / (iconSize + gap);
            if (offset < 0 || actionIndex > 2 || offset % (iconSize + gap) >= iconSize) return;
            if (actionIndex == 0)
                ShowSelectedUser();
            else if (actionIndex == 1 && _canEdit)
                EditSelectedUser();
            else if (actionIndex == 2 && _canManage)
                ToggleSelectedUserLock();
        }

        private void AddUserRow(string username, string fullName, string employeeId, string email,
            string role, string status, bool isLocked)
        {
            _usersGrid.Rows.Add(
                username,
                fullName,
                string.IsNullOrWhiteSpace(employeeId) ? "—" : employeeId,
                email,
                role,
                status,
                isLocked ? "Đang khóa" : "Bình thường",
                IconBitmapCache.GetActionStrip(_canEdit, _canManage, isLocked));
        }

        private UserManagementRowDto SelectedUser()
        {
            int index = _usersGrid.SelectedRows.Count == 0 ? -1 : _usersGrid.SelectedRows[0].Index;
            return index >= 0 && index < _currentRows.Count ? _currentRows[index] : null;
        }

        private void ShowSelectedUser()
        {
            UserManagementRowDto user = SelectedUser();
            if (_service != null && user != null)
                frmUserAccountDetail.Show(this, UserDetailMapper.FromRow(user), _service);
        }

        private void EditSelectedUser()
        {
            if (!_canEdit)
            {
                AppToast.Warning(this, "Bạn không có quyền sửa thông tin tài khoản.");
                return;
            }
            UserManagementRowDto user = SelectedUser();
            // Edit: open the account editor -> EditUserAccountPresenter -> UpdateAccount service -> repository.
            if (user != null && frmEditUserAccount.Show(this, UserDetailMapper.FromRow(user), _service) == DialogResult.OK)
                ReloadUsers();
        }

        private void ToggleSelectedUserLock()
        {
            if (!_canManage)
            {
                AppToast.Warning(this, "Bạn không có quyền khóa hoặc mở khóa tài khoản.");
                return;
            }
            UserManagementRowDto user = SelectedUser();
            if (user == null) return;
            bool shouldLock = !user.IsLocked;
            if (!DialogHelper.Confirm(this, "Xác nhận thao tác",
                string.Format("Bạn có chắc muốn {0} tài khoản '{1}' không?", shouldLock ? "khóa" : "mở khóa", user.Username)))
                return;
            // Lock/unlock: confirm -> SetAccountLocked service -> reload the employee list.
            if (_service.SetAccountLocked(user.UserId, shouldLock) == SetAccountLockResult.Success)
                ReloadUsers();
            else
                AppToast.Warning(this, "Không tìm thấy tài khoản.");
        }

        private void AddEmployee()
        {
            if (!_canManage)
            {
                AppToast.Warning(this, "Bạn không có quyền thêm nhân viên.");
                return;
            }
            if (frmAddEmployee.Show(this, _service) == DialogResult.OK)
                ReloadUsers();
        }

        private void SelectUserRow(int rowIndex)
        {
            _usersGrid.ClearSelection();
            _usersGrid.CurrentCell = _usersGrid.Rows[rowIndex].Cells["FullName"];
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
            _lockButton.Enabled = hasSelection && _canManage;
            _lockButton.Text = user == null || !user.IsLocked ? "Khóa" : "Mở khóa";
        }

        private void ExportUsers(ITableDataExporter exporter)
        {
            if (_service == null) return;
            TableExportRunner.Run(this, exporter, "Nhân viên",
                () => new[] { "Tên đăng nhập", "Họ và tên", "Mã nhân viên", "Email", "Vai trò", "Trạng thái", "Khóa" },
                () =>
                {
                    UserManagementPageDto page = _service.GetPage(0, 100000, null, UserManagementFilters.NonCustomer, null);
                    var rows = new List<object[]>();
                    foreach (UserManagementRowDto user in page.Rows)
                        rows.Add(new object[] { user.Username, user.FullName, user.EmployeeId, user.Email, user.RoleName,
                            string.Equals(user.Status, "ACTIVE", StringComparison.OrdinalIgnoreCase) ? "Đang hoạt động" : "Vô hiệu hóa",
                            user.IsLocked ? "Đang khóa" : "Bình thường" });
                    return rows;
                });
        }

        private void ImportUsers()
        {
            if (!_canManage)
            {
                AppToast.Warning(this, "Bạn không có quyền nhập dữ liệu nhân viên.");
                return;
            }
            var handler = new EmployeeImportRowHandler(_service);
            DialogResult result = frmImportData.Show(this, "Nhân viên",
                EmployeeImportRowHandler.ExpectedColumns,
                "Ngày sinh/Ngày vào làm định dạng dd/MM/yyyy. Giới tính: Nam/Nữ/Khác. Để trống Lương nếu chưa xác định.",
                DefaultSpreadsheetImporters.All, handler.Handle);
            if (result == DialogResult.OK) ReloadUsers();
        }
    }
}

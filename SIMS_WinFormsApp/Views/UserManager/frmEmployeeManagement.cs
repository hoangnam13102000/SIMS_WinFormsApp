using System;
using System.Collections.Generic;
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
        private readonly ContextMenuStrip _rowMenu;
        private readonly ContextMenuStrip _optionsMenu;
        private readonly UserManagementPager _pager;
        private IUserManagementService _service;
        private bool _canEdit;
        private bool _canManage;
        private int _loadGeneration;

        public frmEmployeeManagement()
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
            _optionsMenu = new ContextMenuStrip(components);
            _statusFilter.SelectedIndex = 0;
            _searchBox.TextChanged += (sender, args) => ReloadUsers(true);
            _statusFilter.SelectedIndexChanged += (sender, args) => ReloadUsers(true);
            VisibleChanged += (sender, args) =>
            {
                if (Visible)
                    ReloadUsers();
            };
            _usersGrid.CellDoubleClick += (sender, args) => { if (args.RowIndex >= 0) ShowSelectedUser(args.RowIndex); };
            _usersGrid.CellMouseClick += UsersGrid_CellMouseClick;
            _usersGrid.CellMouseDown += UsersGrid_CellMouseDown;
            DataGridViewCodeCopy.Attach(_usersGrid, "EmployeeId");
            _usersGrid.ContextMenuStrip = _rowMenu;
            _optionsMenu.Items.Add("Xuất CSV", null, (sender, args) => ExportUsers(new CsvTableExporter()));
            _optionsMenu.Items.Add("Xuất Excel", null, (sender, args) => ExportUsers(new ExcelTableExporter()));
            _optionsButton.Click += (sender, args) => _optionsMenu.Show(_optionsButton, 0, _optionsButton.Height);
            _addButton.Click += (sender, args) => AddEmployee();
        }

        private void LayoutHeader()
        {
            int right = _headerPanel.ClientSize.Width - 16;
            _refreshButton.Location = new Point(right - _refreshButton.Width, 29);
            right = _refreshButton.Left - 8;
            _optionsButton.Location = new Point(right - _optionsButton.Width, 29);
            if (_canManage)
            {
                right = _optionsButton.Left - 8;
                _addButton.Location = new Point(right - _addButton.Width, 29);
            }
            _titleLabel.Width = Math.Max(180, (_canManage ? _addButton.Left : _optionsButton.Left) - _titleLabel.Left - 16);
            _addButton.BringToFront();
            _optionsButton.BringToFront();
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
            if (_canEdit)
                _rowMenu.Items.Add("Sửa tài khoản", null, (sender, args) => EditSelectedUser());
            if (_canManage)
            {
                _rowMenu.Items.Add("Khóa / mở khóa", null, (sender, args) => ToggleSelectedUserLock());
                _optionsMenu.Items.Add("Nhập dữ liệu", null, (sender, args) => ImportUsers());
            }
            _rowMenu.Items.Insert(0, new ToolStripMenuItem("Xem chi tiết", null, (sender, args) => ShowSelectedUser()));
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
            _usersGrid.ClearSelection();
            _usersGrid.Rows[e.RowIndex].Selected = true;
            _usersGrid.CurrentCell = _usersGrid.Rows[e.RowIndex].Cells["FullName"];
        }

        private void UsersGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || e.RowIndex < 0 || e.RowIndex >= _currentRows.Count ||
                _usersGrid.Columns[e.ColumnIndex].Name != "Actions" || (!_canEdit && !_canManage)) return;
            _usersGrid.CurrentCell = _usersGrid.Rows[e.RowIndex].Cells["FullName"];
            const int iconSize = 24;
            const int gap = 8;
            int stripWidth = iconSize * 3 + gap * 2;
            int offset = e.X - (_usersGrid.Columns[e.ColumnIndex].Width - stripWidth) / 2;
            int actionIndex = offset / (iconSize + gap);
            if (offset < 0 || actionIndex > 2 || offset % (iconSize + gap) >= iconSize) return;
            if (actionIndex == 0)
                ShowSelectedUser(e.RowIndex);
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
            int index = _usersGrid.CurrentRow == null ? -1 : _usersGrid.CurrentRow.Index;
            return index >= 0 && index < _currentRows.Count ? _currentRows[index] : null;
        }

        private void ShowSelectedUser()
        {
            if (_usersGrid.CurrentRow != null) ShowSelectedUser(_usersGrid.CurrentRow.Index);
        }

        private void ShowSelectedUser(int index)
        {
            if (_service != null && index >= 0 && index < _currentRows.Count)
                frmUserAccountDetail.Show(this, UserDetailMapper.FromRow(_currentRows[index]), _service);
        }

        private void EditSelectedUser()
        {
            if (!_canEdit)
            {
                AppToast.Warning(this, "Bạn không có quyền sửa thông tin tài khoản.");
                return;
            }
            UserManagementRowDto user = SelectedUser();
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

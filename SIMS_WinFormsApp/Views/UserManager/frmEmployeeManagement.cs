using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.Enums;
using SIMS_WinFormsApp.Models.Mapping;
using SIMS_WinFormsApp.Services.Implementations.Export;
using SIMS_WinFormsApp.Services.Implementations.Import;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Controls.Filter;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.UI.Controls.Toast;
using SIMS_WinFormsApp.Views.SystemManagement;

namespace SIMS_WinFormsApp.Views.UserManager
{
    public partial class frmEmployeeManagement : Form
    {
        private const int PageSize = 1000;
        private readonly List<UserManagementRowDto> _currentRows = new List<UserManagementRowDto>();
        private readonly ContextMenuStrip _rowMenu;
        private readonly ContextMenuStrip _optionsMenu;
        private IUserManagementService _service;
        private int _loadGeneration;

        public frmEmployeeManagement()
        {
            InitializeComponent();
            _rowMenu = new ContextMenuStrip(components);
            _optionsMenu = new ContextMenuStrip(components);
            _statusFilter.SelectedIndex = 0;
            AddUserRow("staff01", "Nhân viên mẫu", "staff@example.com", "Nhân viên", "Đang hoạt động", false);
            _searchBox.TextChanged += (sender, args) => ReloadUsers();
            _statusFilter.SelectedIndexChanged += (sender, args) => ReloadUsers();
            VisibleChanged += (sender, args) =>
            {
                if (Visible)
                    ReloadUsers();
            };
            _usersGrid.CellDoubleClick += (sender, args) => { if (args.RowIndex >= 0) ShowSelectedUser(args.RowIndex); };
            _usersGrid.CellMouseClick += UsersGrid_CellMouseClick;
            _usersGrid.CellMouseDown += UsersGrid_CellMouseDown;
            _rowMenu.Items.Add("Xem chi tiết", null, (sender, args) => ShowSelectedUser());
            _rowMenu.Items.Add("Sửa tài khoản", null, (sender, args) => EditSelectedUser());
            _rowMenu.Items.Add("Khóa / mở khóa", null, (sender, args) => ToggleSelectedUserLock());
            _usersGrid.ContextMenuStrip = _rowMenu;
            _optionsMenu.Items.Add("Xuất CSV", null, (sender, args) => ExportUsers(new CsvTableExporter()));
            _optionsMenu.Items.Add("Xuất Excel", null, (sender, args) => ExportUsers(new ExcelTableExporter()));
            _optionsMenu.Items.Add("Nhập dữ liệu", null, (sender, args) => ImportUsers());
            _optionsButton.Click += (sender, args) => _optionsMenu.Show(_optionsButton, 0, _optionsButton.Height);
            _addButton.Click += (sender, args) => AddEmployee();
        }

        public frmEmployeeManagement(IUserManagementService service) : this()
        {
            if (service == null) throw new ArgumentNullException(nameof(service));
            TopLevel = false;
            FormBorderStyle = FormBorderStyle.None;
            Dock = DockStyle.Fill;
            _searchBox.Clear();
            _service = service;
        }

        private async void ReloadUsers()
        {
            if (_service == null || IsDisposed) return;
            int generation = ++_loadGeneration;
            IUserManagementService service = _service;
            string searchTerm = _searchBox.Text;
            string status = _statusFilter.SelectedIndex == 1 ? "ACTIVE" :
                _statusFilter.SelectedIndex == 2 ? "INACTIVE" : null;

            try
            {
                UserManagementPageDto page = await Task.Run(() =>
                    service.GetPage(0, PageSize, searchTerm, UserManagementFilters.NonCustomer, status));
                if (IsDisposed || generation != _loadGeneration) return;

                _currentRows.Clear();
                _currentRows.AddRange(page.Rows);
                _usersGrid.Rows.Clear();
                foreach (UserManagementRowDto user in _currentRows)
                {
                    AddUserRow(user.Username, user.FullName, user.Email, user.RoleName,
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
            _usersGrid.CurrentCell = _usersGrid.Rows[e.RowIndex].Cells[0];
        }

        private void UsersGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || e.RowIndex < 0 || e.RowIndex >= _currentRows.Count ||
                _usersGrid.Columns[e.ColumnIndex].Name != "Actions") return;
            _usersGrid.CurrentCell = _usersGrid.Rows[e.RowIndex].Cells[0];
            const int iconSize = 24;
            const int gap = 8;
            int stripWidth = iconSize * 3 + gap * 2;
            int offset = e.X - (_usersGrid.Columns[e.ColumnIndex].Width - stripWidth) / 2;
            int actionIndex = offset / (iconSize + gap);
            if (offset < 0 || actionIndex > 2 || offset % (iconSize + gap) >= iconSize) return;
            if (actionIndex == 0)
                ShowSelectedUser(e.RowIndex);
            else if (actionIndex == 1)
                EditSelectedUser();
            else
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
                isLocked ? "Đang khóa" : "Bình thường",
                IconBitmapCache.GetActionStrip(isLocked));
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
            UserManagementRowDto user = SelectedUser();
            if (user != null && frmEditUserAccount.Show(this, UserDetailMapper.FromRow(user), _service) == DialogResult.OK)
                ReloadUsers();
        }

        private void ToggleSelectedUserLock()
        {
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
            if (frmAddEmployee.Show(this, _service) == DialogResult.OK)
                ReloadUsers();
        }

        private void ExportUsers(ITableDataExporter exporter)
        {
            if (_service == null) return;
            TableExportRunner.Run(this, exporter, "Nhân viên",
                () => new[] { "Tên đăng nhập", "Họ và tên", "Email", "Vai trò", "Trạng thái", "Khóa" },
                () =>
                {
                    UserManagementPageDto page = _service.GetPage(0, 100000, null, UserManagementFilters.NonCustomer, null);
                    var rows = new List<object[]>();
                    foreach (UserManagementRowDto user in page.Rows)
                        rows.Add(new object[] { user.Username, user.FullName, user.Email, user.RoleName,
                            string.Equals(user.Status, "ACTIVE", StringComparison.OrdinalIgnoreCase) ? "Đang hoạt động" : "Vô hiệu hóa",
                            user.IsLocked ? "Đang khóa" : "Bình thường" });
                    return rows;
                });
        }

        private void ImportUsers()
        {
            var handler = new EmployeeImportRowHandler(_service);
            DialogResult result = frmImportData.Show(this, "Nhân viên",
                EmployeeImportRowHandler.ExpectedColumns,
                "Ngày sinh/Ngày vào làm định dạng dd/MM/yyyy. Giới tính: Nam/Nữ/Khác. Để trống Lương nếu chưa xác định.",
                DefaultSpreadsheetImporters.All, handler.Handle);
            if (result == DialogResult.OK) ReloadUsers();
        }
    }
}

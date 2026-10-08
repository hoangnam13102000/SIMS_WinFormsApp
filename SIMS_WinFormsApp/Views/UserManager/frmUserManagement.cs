using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.Mapping;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Controls.Filter;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.UI.Controls.Toast;

namespace SIMS_WinFormsApp.Views.UserManager
{
    public partial class frmUserManagement : Form
    {
        private const int PageSize = 1000;
        private readonly List<UserManagementRowDto> _currentRows = new List<UserManagementRowDto>();
        private readonly ContextMenuStrip _rowMenu;
        private IUserManagementService _service;
        private int _loadGeneration;

        public frmUserManagement()
        {
            InitializeComponent();
            _rowMenu = new ContextMenuStrip(components);
            AddUserRow("admin", "Quản trị hệ thống", "admin@example.com", "Quản trị viên", "Đang hoạt động", false);
            AddUserRow("nhanvien01", "Nhân viên mẫu", "staff@example.com", "Nhân viên", "Đang hoạt động", false);

            _statusFilter.SelectedIndex = 0;
            _searchBox.TextChanged += (sender, args) => ReloadUsers();
            _statusFilter.SelectedIndexChanged += (sender, args) => ReloadUsers();
            _usersGrid.CellDoubleClick += UsersGrid_CellDoubleClick;
            _usersGrid.CellMouseDown += UsersGrid_CellMouseDown;
            _usersGrid.CellMouseClick += UsersGrid_CellMouseClick;
            VisibleChanged += (sender, args) =>
            {
                if (Visible)
                    ReloadUsers();
            };
            _rowMenu.Items.Add("Xem chi tiết", null, (sender, args) => ShowSelectedUser());
            _rowMenu.Items.Add("Sửa tài khoản", null, (sender, args) => EditSelectedUser());
            _rowMenu.Items.Add("Khóa / mở khóa", null, (sender, args) => ToggleSelectedUserLock());
            _usersGrid.ContextMenuStrip = _rowMenu;
        }

        public frmUserManagement(IUserManagementService service) : this()
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            TopLevel = false;
            FormBorderStyle = FormBorderStyle.None;
            Dock = DockStyle.Fill;
            _searchBox.Clear();
            _service = service;
        }

        private async void ReloadUsers()
        {
            if (_service == null || IsDisposed)
                return;

            int generation = ++_loadGeneration;
            IUserManagementService service = _service;
            string searchTerm = _searchBox.Text;
            string statusFilter = null;
            if (_statusFilter.SelectedIndex == 1)
                statusFilter = "ACTIVE";
            else if (_statusFilter.SelectedIndex == 2)
                statusFilter = "INACTIVE";

            try
            {
                UserManagementPageDto page = await Task.Run(() =>
                    service.GetPage(0, PageSize, searchTerm, null, statusFilter));
                if (IsDisposed || generation != _loadGeneration)
                    return;

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
                _usersGrid.Columns[e.ColumnIndex].Name != "Actions")
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
                IconBitmapCache.GetActionStrip(isLocked));
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

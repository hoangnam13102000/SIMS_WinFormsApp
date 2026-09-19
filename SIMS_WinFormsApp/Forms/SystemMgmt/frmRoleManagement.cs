using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Controls.Loading;
using SIMS_WinFormsApp.UI.Controls.Toast;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.Forms.SystemMgmt
{
    /// <summary>
    /// Trang "Quản lý phân quyền vai trò" - trước đây là 1 Form trống hoàn toàn (chỉ có
    /// InitializeComponent() rỗng, chưa được gọi từ bất kỳ đâu trong project). Hoàn thiện theo
    /// đúng khuôn mẫu MVP + kiểu dialog đang dùng cho frmEditUserAccount/frmChangePassword:
    /// kế thừa <see cref="BaseFormDialogForm"/> (header/footer/bo góc chung), tự tạo
    /// <see cref="RoleManagementPresenter"/> trong constructor.
    ///
    /// Ma trận quyền hạn (tất cả Permission x trạng thái được cấp cho vai trò đang chọn) thường
    /// mất 2-3 giây để tải (2 câu truy vấn: toàn bộ Permissions + toàn bộ RolePermissions của vai
    /// trò), nên toàn bộ nội dung được bọc trong <see cref="LoadingOverlayHost"/> đã dựng sẵn ở
    /// UI/Controls/Loading - không phải tự vẽ spinner riêng.
    /// </summary>
    public sealed partial class frmRoleManagement : BaseFormDialogForm, IRoleManagementView
    {
        private static readonly Size DialogSize = new Size(980, 640);

        private readonly RoleManagementPresenter _presenter;

        private ListBox _lstRoles;
        private Label _lblSelectedRoleName;
        private Label _lblSelectedRoleDescription;
        private Panel _permissionListPanel;
        private LoadingOverlayHost _overlayHost;

        public frmRoleManagement(
            IWin32Window owner,
            IRoleRepository roleRepository = null,
            IPermissionRepository permissionRepository = null)
            : base(owner)
        {
            Size = DialogSize;

            BuildContent();

            CloseRequested += (s, e) => Close();
            AddFooterButton("Đóng", isPrimary: true, onClick: (s, e) => RaiseCloseRequested());

            _presenter = new RoleManagementPresenter(
                this,
                roleRepository ?? AppComposition.CreateRoleRepository(),
                permissionRepository ?? AppComposition.CreatePermissionRepository(),
                _overlayHost);

            // Tải dữ liệu SAU KHI dialog đã thực sự hiển thị (Shown), không phải trong
            // constructor hay Load - nếu tải ngay trong constructor, người dùng sẽ không thấy gì
            // trong 2-3 giây (dialog chưa kịp vẽ) và lớp phủ loading mất hết tác dụng.
            Shown += (s, e) => _presenter.Load();
        }

        /// <summary>Cách gọi nhanh, gọn cho nơi khác trong ứng dụng:
        /// <c>frmRoleManagement.Show(this);</c> - xem ManagementTablePage.Accounts().</summary>
        public new static DialogResult Show(IWin32Window owner)
        {
            using (var dialog = new frmRoleManagement(owner))
            {
                return dialog.ShowDialog(owner);
            }
        }

        #region IRoleManagementView
        public int SelectedRoleId => (_lstRoles.SelectedItem as RoleOptionDto)?.RoleId ?? 0;

        public event EventHandler RoleSelected;
        public event EventHandler<PermissionToggleEventArgs> PermissionToggled;

        public void DisplayRoles(IReadOnlyList<RoleOptionDto> roles)
        {
            // Gỡ handler tạm thời để việc gán DataSource/SelectedIndex không tự phát sinh thêm 1
            // lần RoleSelected thừa - Presenter.Load() đã tự hiển thị quyền hạn của vai trò đầu
            // tiên ngay sau lời gọi này rồi.
            _lstRoles.SelectedIndexChanged -= LstRoles_SelectedIndexChanged;
            _lstRoles.DataSource = null;
            _lstRoles.DataSource = roles?.ToList() ?? new List<RoleOptionDto>();
            if (_lstRoles.Items.Count > 0) _lstRoles.SelectedIndex = 0;
            _lstRoles.SelectedIndexChanged += LstRoles_SelectedIndexChanged;
        }

        public void DisplayPermissions(RoleOptionDto role, IReadOnlyList<PermissionDto> permissions, HashSet<string> grantedCodes)
        {
            _lblSelectedRoleName.Text = role?.RoleName ?? string.Empty;
            _lblSelectedRoleDescription.Text = role != null ? "Mã vai trò: " + role.RoleCode : string.Empty;

            _permissionListPanel.SuspendLayout();
            _permissionListPanel.Controls.Clear();

            // add ngược thứ tự hiển thị (Dock=Top: control add SAU CÙNG lên vị trí TRÊN CÙNG) -
            // cùng quy ước đang dùng ở frmEditUserAccount/ucMyProfile.
            for (int i = permissions.Count - 1; i >= 0; i--)
            {
                var permission = permissions[i];
                bool granted = grantedCodes != null && grantedCodes.Contains(permission.PermissionCode);
                _permissionListPanel.Controls.Add(CreatePermissionRow(permission, granted));
            }

            _permissionListPanel.ResumeLayout(true);
        }

        public void ShowError(string message) => AppToast.Error(this, message);

        public void ShowSuccess(string message) => AppToast.Success(this, message);
        #endregion

        private void LstRoles_SelectedIndexChanged(object sender, EventArgs e) =>
            RoleSelected?.Invoke(this, EventArgs.Empty);

        #region Dựng giao diện (chỉ chạy 1 lần lúc khởi tạo)
        private void BuildContent()
        {
            HeaderTitle = "Quản lý phân quyền vai trò";
            SetHeaderIcon(IconChar.UserShield, AppColors.Accent);

            // Trang này là 1 khối 2 cột cố định (danh sách vai trò + ma trận quyền), không phải
            // các field xếp dọc như popup "Cập nhật tài khoản" - bỏ padding mặc định của
            // ContentHost để 2 cột dùng trọn khung nội dung.
            ContentHost.Padding = new Padding(0);

            var split = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.Transparent
            };
            split.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220f));
            split.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            split.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            split.Controls.Add(BuildRolesColumn(), 0, 0);
            split.Controls.Add(BuildPermissionsColumn(), 1, 0);

            // Bọc toàn bộ khối 2 cột trong LoadingOverlayHost (UI/Controls/Loading) - hiện lớp
            // phủ "Đang tải..." trong lúc Presenter gọi DB, không cần tự vẽ spinner riêng cho
            // trang này.
            _overlayHost = new LoadingOverlayHost(split);
            ContentHost.Controls.Add(_overlayHost);
        }

        private Control BuildRolesColumn()
        {
            var leftPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(20, 20, 12, 20)
            };

            var lblTitle = new Label
            {
                Dock = DockStyle.Top,
                Height = 26,
                Text = "Vai trò",
                Font = AppFonts.SmallBold,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent
            };

            _lstRoles = new ListBox
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                Font = AppFonts.Body,
                IntegralHeight = false
            };
            _lstRoles.SelectedIndexChanged += LstRoles_SelectedIndexChanged;

            leftPanel.Controls.Add(_lstRoles);
            leftPanel.Controls.Add(lblTitle);
            return leftPanel;
        }

        private Control BuildPermissionsColumn()
        {
            var rightPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(12, 20, 20, 20)
            };

            _lblSelectedRoleName = new Label
            {
                Dock = DockStyle.Top,
                Height = 28,
                Font = AppFonts.Subtitle,
                ForeColor = AppColors.TextTitle,
                BackColor = Color.Transparent
            };

            _lblSelectedRoleDescription = new Label
            {
                Dock = DockStyle.Top,
                Height = 22,
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent
            };

            var separator = new Panel
            {
                Dock = DockStyle.Top,
                Height = 12,
                BackColor = Color.Transparent
            };
            separator.Paint += (s, e) =>
            {
                using (var pen = new Pen(AppColors.Border))
                    e.Graphics.DrawLine(pen, 0, separator.Height - 1, separator.Width, separator.Height - 1);
            };

            _permissionListPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.Transparent
            };

            rightPanel.Controls.Add(_permissionListPanel);
            rightPanel.Controls.Add(separator);
            rightPanel.Controls.Add(_lblSelectedRoleDescription);
            rightPanel.Controls.Add(_lblSelectedRoleName);
            return rightPanel;
        }

        /// <summary>1 dòng trong ma trận quyền: mã quyền + mô tả bên trái, ToggleSwitchControl
        /// (control có sẵn của dự án, dùng lại nguyên vẹn) bên phải.</summary>
        private Control CreatePermissionRow(PermissionDto permission, bool granted)
        {
            var row = new Panel { Dock = DockStyle.Top, Height = 52, BackColor = Color.Transparent };

            var lblCode = new Label
            {
                AutoSize = true,
                Text = permission.PermissionCode,
                Font = AppFonts.BodyBold,
                ForeColor = AppColors.TextTitle,
                BackColor = Color.Transparent,
                Location = new Point(4, 6)
            };

            var lblDesc = new Label
            {
                AutoSize = true,
                Text = permission.Description ?? string.Empty,
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent,
                Location = new Point(4, 26)
            };

            var toggle = new ToggleSwitchControl();
            toggle.SetCheckedSilently(granted);
            toggle.CheckedChanged += (s, e) =>
                PermissionToggled?.Invoke(this, new PermissionToggleEventArgs(permission.PermissionId, permission.PermissionCode, toggle.Checked));

            void PositionToggle() =>
                toggle.Location = new Point(Math.Max(0, row.Width - toggle.Width - 8), (row.Height - toggle.Height) / 2);
            row.Resize += (s, e) => PositionToggle();
            PositionToggle();

            row.Paint += (s, e) =>
            {
                using (var pen = new Pen(AppColors.Border))
                    e.Graphics.DrawLine(pen, 4, row.Height - 1, row.Width - 4, row.Height - 1);
            };

            row.Controls.Add(toggle);
            row.Controls.Add(lblDesc);
            row.Controls.Add(lblCode);
            return row;
        }
        #endregion
    }
}
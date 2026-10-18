using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.Views.Toast;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Dialogs;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.Views.UserManager
{
    /// <summary>
    /// Trang "Quản lý phân quyền vai trò" - trước đây là 1 Form trống hoàn toàn (chỉ có
    /// InitializeComponent() rỗng, chưa được gọi từ bất kỳ đâu trong project). Hoàn thiện theo
    /// đúng khuôn mẫu MVP + kiểu dialog đang dùng cho frmEditUserAccount/frmChangePassword:
    /// kế thừa <see cref="BaseFormDialogForm"/> (header/footer/bo góc chung), tự tạo
    /// <see cref="RoleManagementPresenter"/> trong constructor.
    ///
    /// Ma trận quyền hạn (tất cả Permission x trạng thái được cấp cho vai trò đang chọn) thường
    /// mất vài giây để tải, nên dùng Panel, Label và ProgressBar WinForms chuẩn làm lớp phủ
    /// trong chính view.
    /// </summary>
    public sealed partial class frmRoleManagement : BaseFormDialogForm, IRoleManagementView, ILoadingIndicator
    {
        private static readonly Size DialogSize = new Size(980, 640);

        private readonly RoleManagementPresenter _presenter;
        private Panel _loadingOverlay;
        private Label _loadingMessage;

        public frmRoleManagement() : this(null, null, null)
        {
        }

        public frmRoleManagement(
            IWin32Window owner,
            IRoleRepository roleRepository = null,
            IPermissionRepository permissionRepository = null)
            : base(owner)
        {
            InitializeComponent();
            Size = DialogSize;
            HeaderTitle = "Quản lý phân quyền vai trò";
            SetHeaderIcon(IconChar.UserShield, AppColors.Accent);

            _roleLayout.Dock = DockStyle.Fill;
            ContentHost.Controls.Add(_roleLayout);
            CreateLoadingOverlay();

            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                _presenter = null;
                return;
            }

            CloseRequested += FrmRoleManagement_CloseRequested;
            AddFooterButton("Đóng", isPrimary: true, onClick: FooterCloseButton_Click);

            _presenter = new RoleManagementPresenter(
                this,
                roleRepository ?? AppComposition.CreateRoleRepository(),
                permissionRepository ?? AppComposition.CreatePermissionRepository(),
                this);

            // Tải dữ liệu SAU KHI dialog đã thực sự hiển thị (Shown), không phải trong
            // constructor hay Load - nếu tải ngay trong constructor, người dùng sẽ không thấy gì
            // trong 2-3 giây (dialog chưa kịp vẽ) và lớp phủ loading mất hết tác dụng.
            Shown += FrmRoleManagement_Shown;
        }

        public void ShowLoading(string message = null)
        {
            _loadingMessage.Text = string.IsNullOrWhiteSpace(message) ? "Đang tải dữ liệu..." : message;
            _loadingOverlay.Visible = true;
            _loadingOverlay.BringToFront();
            _roleLayout.Enabled = false;
        }

        public void HideLoading()
        {
            _loadingOverlay.Visible = false;
            _roleLayout.Enabled = true;
        }

        private void CreateLoadingOverlay()
        {
            var progress = new ProgressBar
            {
                Style = ProgressBarStyle.Marquee,
                MarqueeAnimationSpeed = 30,
                Size = new Size(220, 16)
            };
            _loadingMessage = new Label
            {
                AutoSize = false,
                Font = AppFonts.SmallBold,
                ForeColor = AppColors.TextTitle,
                Text = "Đang tải dữ liệu...",
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(280, 28)
            };
            _loadingOverlay = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = AppColors.White,
                Visible = false
            };
            _loadingOverlay.Controls.Add(progress);
            _loadingOverlay.Controls.Add(_loadingMessage);
            _loadingOverlay.Resize += (sender, args) =>
            {
                progress.Location = new Point((_loadingOverlay.ClientSize.Width - progress.Width) / 2,
                    (_loadingOverlay.ClientSize.Height - progress.Height) / 2 - 12);
                _loadingMessage.Location = new Point((_loadingOverlay.ClientSize.Width - _loadingMessage.Width) / 2,
                    progress.Bottom + 4);
            };
            ContentHost.Controls.Add(_loadingOverlay);
            _loadingOverlay.BringToFront();
        }

        private void FrmRoleManagement_CloseRequested(object sender, EventArgs e)
        {
            Close();
        }

        private void FooterCloseButton_Click(object sender, EventArgs e)
        {
            RaiseCloseRequested();
        }

        private void FrmRoleManagement_Shown(object sender, EventArgs e)
        {
            _presenter.Load();
        }

        /// <summary>Cách gọi nhanh, gọn cho nơi khác trong ứng dụng:
        /// <c>frmRoleManagement.Show(this);</c> để mở dialog từ một View khác.</summary>
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

        private void Separator_Paint(object sender, PaintEventArgs e)
        {
            using (var pen = new Pen(AppColors.Border))
                e.Graphics.DrawLine(pen, 0, _separator.Height - 1, _separator.Width, _separator.Height - 1);
        }

        /// <summary>1 dòng trong ma trận quyền: mã quyền + mô tả bên trái, toggle bên phải.</summary>
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

            bool isChecked = granted;
            bool isHovered = false;
            var toggle = new Panel
            {
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Size = new Size(44, 24),
                TabStop = false,
                AccessibleRole = AccessibleRole.CheckButton,
                AccessibleName = permission.PermissionCode,
                AccessibleDescription = (permission.Description ?? string.Empty) + (granted ? " - Được cấp" : " - Chưa cấp")
            };
            toggle.Click += (s, e) =>
            {
                isChecked = !isChecked;
                toggle.AccessibleDescription = (permission.Description ?? string.Empty) + (isChecked ? " - Được cấp" : " - Chưa cấp");
                toggle.Invalidate();
                PermissionToggled?.Invoke(
                    this,
                    new PermissionToggleEventArgs(permission.PermissionId, permission.PermissionCode, isChecked));
            };
            toggle.MouseEnter += (s, e) => { isHovered = true; toggle.Invalidate(); };
            toggle.MouseLeave += (s, e) => { isHovered = false; toggle.Invalidate(); };
            toggle.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var track = new Rectangle(0, 0, toggle.Width - 1, toggle.Height - 1);
                Color trackColor = !toggle.Enabled
                    ? AppColors.DisabledBtn
                    : (isChecked ? (isHovered ? AppColors.AccentHover : AppColors.Accent) : AppColors.Border);

                using (var path = AppRadius.GetRoundedPath(track, toggle.Height / 2))
                using (var brush = new SolidBrush(trackColor))
                    e.Graphics.FillPath(brush, path);

                int thumbDiameter = Math.Max(8, toggle.Height - 6);
                int thumbX = isChecked ? toggle.Width - thumbDiameter - 3 : 3;
                int thumbY = (toggle.Height - thumbDiameter) / 2;
                using (var brush = new SolidBrush(Color.White))
                    e.Graphics.FillEllipse(brush, thumbX, thumbY, thumbDiameter, thumbDiameter);
            };

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
    }
}
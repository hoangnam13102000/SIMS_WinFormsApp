using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.Enums;
using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.Forms.SystemMgmt
{
    /// <summary>
    /// Popup "Cập nhật tài khoản" - dùng chung cho cả nhân viên lẫn khách hàng (cả hai đều là
    /// User, chỉ khác RoleCode). Kế thừa khung sườn <see cref="BaseFormDialogForm"/> (khác với
    /// <see cref="BaseDetailDialogForm"/> mà popup "Xem chi tiết" đang dùng, vì đây là popup
    /// FORM nhập liệu chứ không phải xem chi tiết có sidebar/tab) và đóng vai trò View trong mô
    /// hình MVP (implement <see cref="IEditUserAccountView"/>). Form chỉ lo dựng control và
    /// đọc/ghi giá trị field - việc validate + gọi Service do <see cref="EditUserAccountPresenter"/>
    /// đảm nhiệm.
    /// </summary>
    public sealed class frmEditUserAccount : BaseFormDialogForm, IEditUserAccountView
    {
        private readonly EditUserAccountPresenter _presenter;

        private LabeledIconField _fieldFullName;
        private LabeledIconField _fieldEmail;
        private LabeledIconField _fieldPhone;
        private PrimaryButton _btnSave;

        public frmEditUserAccount(UserDetailDto user, IWin32Window owner, IUserManagementService userManagementService)
            : base(owner)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (userManagementService == null) throw new ArgumentNullException(nameof(userManagementService));

            BuildContent(user);

            CloseRequested += (s, e) => Close();

            _presenter = new EditUserAccountPresenter(this, userManagementService, user.UserId);
        }

        /// <summary>Cách gọi nhanh, gọn cho nơi khác trong ứng dụng (ví dụ khi bấm "Sửa" trên
        /// lưới quản lý tài khoản): <c>frmEditUserAccount.Show(this, user, service);</c></summary>
        public static DialogResult Show(IWin32Window owner, UserDetailDto user, IUserManagementService userManagementService)
        {
            using (var dialog = new frmEditUserAccount(user, owner, userManagementService))
            {
                return dialog.ShowDialog(owner);
            }
        }

        #region Dựng giao diện (chỉ chạy 1 lần lúc khởi tạo)
        private void BuildContent(UserDetailDto user)
        {
            string accountType = GetAccountTypeLabel(user);
            HeaderTitle = "Cập nhật tài khoản " + accountType;
            SetHeaderIcon(IconChar.UserPen, AppColors.Accent);

            // Với các control con đều Dock = Top trong cùng 1 Panel, WinForms xếp control
            // ADD SAU CÙNG lên vị trí TRÊN CÙNG (đúng cách BaseDetailDialogForm đang xếp
            // header/footer/body). Vì thứ tự hiển thị mong muốn là banner -> thông tin readonly
            // -> Họ tên -> Email -> SĐT (từ trên xuống), ta phải add theo thứ tự NGƯỢC LẠI:
            // phone trước tiên, banner sau cùng.
            var fieldPhone = CreatePhoneField();
            var fieldEmail = CreateEmailField();
            var fieldFullName = CreateFullNameField();
            var readOnlyRow = CreateReadOnlyInfoRow(user);
            var banner = CreateBanner(accountType);

            ContentHost.Controls.Add(fieldPhone);
            ContentHost.Controls.Add(fieldEmail);
            ContentHost.Controls.Add(fieldFullName);
            ContentHost.Controls.Add(readOnlyRow);
            ContentHost.Controls.Add(banner);

            _fieldFullName = fieldFullName;
            _fieldEmail = fieldEmail;
            _fieldPhone = fieldPhone;

            _fieldFullName.Value = user.FullName;
            _fieldEmail.Value = user.Email;
            _fieldPhone.Value = user.Phone;

            AddFooterButton("Hủy", false, (s, e) => RaiseCloseRequested());
            _btnSave = AddFooterButton("Lưu thay đổi", true, (s, e) => RaiseSaveRequested());
        }

        private static string GetAccountTypeLabel(UserDetailDto user)
        {
            return string.Equals(user.RoleCode, RoleCodes.Customer, StringComparison.OrdinalIgnoreCase)
                ? "khách hàng"
                : "nhân viên";
        }

        private static InfoBannerPanel CreateBanner(string accountType)
        {
            return new InfoBannerPanel
            {
                Icon = IconChar.UserGear,
                TitleText = "Cập nhật tài khoản " + accountType,
                DescriptionText = "Chỉnh thông tin liên hệ, vai trò hoặc trạng thái tài khoản.",
                Margin = new Padding(0, 0, 0, 16)
            };
        }

        private LabeledIconField CreateFullNameField()
        {
            return new LabeledIconField
            {
                LabelText = "Họ và tên",
                IsRequired = true,
                Icon = IconChar.User,
                PlaceholderText = "Nhập họ và tên",
                HintText = "Họ tên hiển thị trên hệ thống.",
                MaxLength = 100
            };
        }

        private LabeledIconField CreateEmailField()
        {
            return new LabeledIconField
            {
                LabelText = "Email",
                IsRequired = true,
                Icon = IconChar.Envelope,
                PlaceholderText = "Nhập email",
                HintText = "Dùng để nhận thông báo / khôi phục tài khoản.",
                MaxLength = 150
            };
        }

        private LabeledIconField CreatePhoneField()
        {
            return new LabeledIconField
            {
                LabelText = "Số điện thoại",
                IsRequired = false,
                Icon = IconChar.Phone,
                PlaceholderText = "Nhập số điện thoại",
                HintText = "VD: 09xxxxxxxx (tùy chọn).",
                MaxLength = 15
            };
        }

        /// <summary>Khối xám bo góc hiển thị "Mã tài khoản" / "Tên đăng nhập" - chỉ đọc, không
        /// cho sửa (giống hình mẫu). Chỉ dùng 1 lần ở form này nên dựng trực tiếp ở đây thay vì
        /// tách thành 1 control tái sử dụng riêng.</summary>
        private static Panel CreateReadOnlyInfoRow(UserDetailDto user)
        {
            var box = new Panel
            {
                Dock = DockStyle.Top,
                Height = 88,
                Margin = new Padding(0, 0, 0, 16),
                BackColor = Color.Transparent
            };
            box.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, box.Width - 1, box.Height - 1);
                using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Medium))
                using (var brush = new SolidBrush(AppColors.BgLighter))
                    e.Graphics.FillPath(brush, path);
            };

            var grid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.Transparent,
                Padding = new Padding(16, 14, 16, 14)
            };
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            grid.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            box.Controls.Add(grid);

            grid.Controls.Add(CreateReadOnlyChip(IconChar.Hashtag, "Mã tài khoản", user.UserId.ToString()), 0, 0);
            grid.Controls.Add(CreateReadOnlyChip(IconChar.UserTag, "Tên đăng nhập", user.Username), 1, 0);

            return box;
        }

        private static Control CreateReadOnlyChip(IconChar icon, string caption, string value)
        {
            var host = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.Transparent
            };
            host.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 22f));
            host.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));

            var iconBox = new IconPictureBox
            {
                IconChar = icon,
                IconColor = AppColors.TextMuted,
                IconSize = 14,
                Size = new Size(14, 14),
                Anchor = AnchorStyles.Top | AnchorStyles.Left,
                Margin = new Padding(0, 4, 6, 0),
                BackColor = Color.Transparent
            };

            var textStack = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = Color.Transparent
            };
            textStack.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            textStack.RowStyles.Add(new RowStyle(SizeType.Absolute, 26f));

            var lblCaption = new Label
            {
                AutoSize = true,
                Text = caption,
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent
            };
            var lblValue = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                Text = value,
                Font = AppFonts.BodyBold,
                ForeColor = AppColors.TextTitle,
                BackColor = Color.Transparent,
                AutoEllipsis = false,
                UseMnemonic = false
            };

            textStack.Controls.Add(lblCaption, 0, 0);
            textStack.Controls.Add(lblValue, 0, 1);

            host.Controls.Add(iconBox, 0, 0);
            host.Controls.Add(textStack, 1, 0);
            return host;
        }
        #endregion

        #region IEditUserAccountView
        public string FullName
        {
            get => _fieldFullName.Value;
            set => _fieldFullName.Value = value;
        }

        public string Email
        {
            get => _fieldEmail.Value;
            set => _fieldEmail.Value = value;
        }

        public string Phone
        {
            get => _fieldPhone.Value;
            set => _fieldPhone.Value = value;
        }

        public event EventHandler SaveRequested;

        // "Hủy" chỉ cần đóng popup, không cần Presenter can thiệp - chuyển tiếp thẳng vào sự
        // kiện CloseRequested đã có sẵn từ BaseFormDialogForm (dùng chung với nút X/phím Esc)
        // thay vì tự quản lý thêm 1 field sự kiện mới.
        public event EventHandler CancelRequested
        {
            add => CloseRequested += value;
            remove => CloseRequested -= value;
        }

        public void ShowError(string message) => DialogHelper.ShowError(this, message);

        public void ShowSuccess(string message) => DialogHelper.ShowSuccess(this, message);

        public void SetSaving(bool isSaving)
        {
            SetFooterButtonsEnabled(!isSaving);
            _btnSave.Text = isSaving ? "Đang lưu..." : "Lưu thay đổi";
        }

        public void CloseOnSuccess()
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void RaiseSaveRequested() => SaveRequested?.Invoke(this, EventArgs.Empty);
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _presenter?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
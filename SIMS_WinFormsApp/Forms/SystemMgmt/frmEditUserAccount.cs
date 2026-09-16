using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.Enums;
using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Controls.Toast;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.Forms.SystemMgmt
{
    public sealed class frmEditUserAccount : BaseFormDialogForm, IEditUserAccountView
    {
        /// <summary>Kích thước popup khi chuyển sang layout ngang (avatar + 2 cột) - rộng hơn
        /// DefaultDialogSize (620x720) của BaseFormDialogForm vốn dành cho layout 1 cột dọc.</summary>
        private static readonly Size HorizontalDialogSize = new Size(900, 560);

        private readonly EditUserAccountPresenter _presenter;

        private LabeledIconField _fieldFullName;
        private LabeledIconField _fieldEmail;
        private LabeledIconField _fieldPhone;
        private AvatarUploadPanel _avatarPanel;
        private ThreeColumnFieldsPanel _fieldsGrid;
        private PrimaryButton _btnSave;

        public frmEditUserAccount(UserDetailDto user, IWin32Window owner, IUserManagementService userManagementService)
            : base(owner)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (userManagementService == null) throw new ArgumentNullException(nameof(userManagementService));

            // Đặt kích thước popup TRƯỚC khi dựng nội dung để ThreeColumnFieldsPanel tính đúng
            // độ rộng từng cột ngay từ lần layout đầu tiên (ContentHost đã có ClientSize đúng).
            Size = HorizontalDialogSize;

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

            var banner = CreateBanner(accountType);

            var fieldFullName = CreateFullNameField();
            var fieldEmail = CreateEmailField();
            var fieldPhone = CreatePhoneField();

            // Layout ngang: cột trái là ảnh đại diện, cột giữa "Thông tin cá nhân" (các trường có
            // thể sửa), cột phải "Thông tin tài khoản" (Mã tài khoản/Tên đăng nhập - chỉ đọc,
            // giống hình mẫu). Thay cho layout xếp dọc 1 cột + khối readonly nằm ngang trước đây.
            var fieldsGrid = new ThreeColumnFieldsPanel { Margin = new Padding(0, 0, 0, 4) };
            fieldsGrid.SetSecondColumnFields(
                CreatePersonalInfoHeader(), fieldFullName, fieldEmail, fieldPhone);
            fieldsGrid.SetThirdColumnFields(
                CreateAccountInfoHeader(), CreateReadOnlyInfoBox(user));

            var avatarPanel = fieldsGrid.Avatar;
            avatarPanel.Initial = GetInitial(user.FullName);
            if (!string.IsNullOrWhiteSpace(user.AvatarUrl) && File.Exists(user.AvatarUrl))
            {
                try
                {
                    using (var source = Image.FromFile(user.AvatarUrl))
                        avatarPanel.SetImage(new Bitmap(source), user.AvatarUrl);
                }
                catch (Exception)
                {
                    // File avatar không hợp lệ thì giữ lại chữ cái đại diện.
                }
            }

            var headerContentGap = new Panel
            {
                Dock = DockStyle.Top,
                Height = 16,
                BackColor = Color.Transparent,
                Margin = Padding.Empty
            };

            // Với các control con đều Dock=Top trong cùng 1 Panel, WinForms xếp control ADD SAU
            // CÙNG lên vị trí TRÊN CÙNG, nên add theo thứ tự NGƯỢC LẠI với thứ tự hiển thị mong
            // muốn (banner -> khoảng cách -> fieldsGrid, từ trên xuống).
            ContentHost.Controls.Add(fieldsGrid);
            ContentHost.Controls.Add(headerContentGap);
            ContentHost.Controls.Add(banner);

            // KHÔNG gọi fieldsGrid.Reflow() ở đây: tại thời điểm BuildContent() chạy (bên trong
            // constructor), Form CHƯA có handle cửa sổ nên ContentHost/fieldsGrid có thể chưa
            // mang đúng Width theo HorizontalDialogSize (900x560) - gọi Reflow() sớm với Width
            // "rác" là nguyên nhân khiến popup từng bị cắt/co 2 cột nội dung xuống vài px. Việc
            // Reflow lần đầu được dời sang OnContentReady() (chạy trong OnLoad, sau khi handle đã
            // tạo và ClientSize đã chắc chắn đúng) - xem BaseFormDialogForm.OnContentReady.
            _fieldsGrid = fieldsGrid;
            _fieldFullName = fieldFullName;
            _fieldEmail = fieldEmail;
            _fieldPhone = fieldPhone;
            _avatarPanel = avatarPanel;

            _fieldFullName.Value = user.FullName;
            _fieldEmail.Value = user.Email;
            _fieldPhone.Value = user.Phone;

            AddFooterButton("Hủy", false, (s, e) => RaiseCloseRequested());
            _btnSave = AddFooterButton("Lưu thay đổi", true, (s, e) => RaiseSaveRequested());
        }

        /// <summary>Layout ngang (avatar + 2 cột) chỉ được tính lại lần đầu ở đây - SAU KHI Form
        /// đã có handle cửa sổ và ContentHost đã có ClientSize thật theo HorizontalDialogSize.
        /// Xem giải thích chi tiết ở BaseFormDialogForm.OnContentReady.</summary>
        protected override void OnContentReady()
        {
            base.OnContentReady();
            _fieldsGrid.Reflow();
            // Co Height Form theo đúng nội dung thật (banner + fieldsGrid) - tránh hiện scroll
            // bar không cần thiết khi HorizontalDialogSize.Height (560) chỉ là số đoán trước, có
            // thể lệch với chiều cao thật của nội dung 3 cột.
            FitHeightToContent();
        }

        private static string GetAccountTypeLabel(UserDetailDto user)
        {
            return string.Equals(user.RoleCode, RoleCodes.Customer, StringComparison.OrdinalIgnoreCase)
                ? "khách hàng"
                : "nhân viên";
        }

        /// <summary>Chữ cái đại diện hiển thị trên avatar khi chưa chọn ảnh - ký tự đầu họ tên,
        /// cùng cách suy ra đang dùng ở HeaderControl.SetUser cho khối avatar góc trên bên phải.</summary>
        private static string GetInitial(string fullName)
        {
            return string.IsNullOrWhiteSpace(fullName)
                ? "?"
                : fullName.Trim().Substring(0, 1).ToUpperInvariant();
        }

        private static InfoBannerPanel CreateBanner(string accountType)
        {
            return new InfoBannerPanel
            {
                Icon = IconChar.UserGear,
                TitleText = "Cập nhật tài khoản " + accountType,
                DescriptionText = "Chỉnh thông tin liên hệ, vai trò hoặc trạng thái tài khoản.",
                Margin = Padding.Empty
            };
        }

        private static FieldGroupHeader CreatePersonalInfoHeader()
        {
            return new FieldGroupHeader { Icon = IconChar.IdCard, HeaderText = "Thông tin cá nhân" };
        }

        private static FieldGroupHeader CreateAccountInfoHeader()
        {
            return new FieldGroupHeader { Icon = IconChar.UserTag, HeaderText = "Thông tin tài khoản" };
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
        /// cho sửa (giống hình mẫu). Trước đây 2 chip này nằm CẠNH NHAU trong 1 hàng ngang riêng;
        /// giờ xếp CHỒNG lên nhau (Dock=Top) để vừa vặn làm nội dung của 1 cột dọc trong
        /// ThreeColumnFieldsPanel. Chỉ dùng 1 lần ở form này nên dựng trực tiếp thay vì tách
        /// thành 1 control tái sử dụng riêng.</summary>
        private static Panel CreateReadOnlyInfoBox(UserDetailDto user)
        {
            const int Padding = 14;
            const int ChipGap = 14;

            var chipMaNhanVien = CreateReadOnlyChip(IconChar.Hashtag, "Mã tài khoản", user.UserId.ToString());
            var chipTenDangNhap = CreateReadOnlyChip(IconChar.UserTag, "Tên đăng nhập", user.Username);

            var box = new Panel
            {
                Dock = DockStyle.Top,
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

            void LayoutChips()
            {
                int contentWidth = Math.Max(10, box.Width - Padding * 2);
                chipMaNhanVien.Location = new Point(Padding, Padding);
                chipMaNhanVien.Width = contentWidth;

                chipTenDangNhap.Location = new Point(Padding, chipMaNhanVien.Bottom + ChipGap);
                chipTenDangNhap.Width = contentWidth;

                box.Height = chipTenDangNhap.Bottom + Padding;
            }

            box.Controls.Add(chipTenDangNhap);
            box.Controls.Add(chipMaNhanVien);
            box.Resize += (s, e) => LayoutChips();

            return box;
        }

        private static Control CreateReadOnlyChip(IconChar icon, string caption, string value)
        {
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

            // Đo đúng chiều cao 1 dòng chữ theo Font/DPI THẬT đang dùng (thay vì đoán cố định
            // 20f/26f trước đây) - số cố định là nguyên nhân "10"/"khach le" bị cắt chân khi dòng
            // chữ thật cao hơn khoảng đã đoán (đặc biệt ở DPI > 100%).
            int captionHeight = Math.Max(16, TextRenderer.MeasureText(
                caption, lblCaption.Font, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding).Height);
            int valueHeight = Math.Max(20, TextRenderer.MeasureText(
                value, lblValue.Font, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding).Height);

            var host = new TableLayoutPanel
            {
                Height = captionHeight + valueHeight,
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
            textStack.RowStyles.Add(new RowStyle(SizeType.Absolute, captionHeight));
            textStack.RowStyles.Add(new RowStyle(SizeType.Absolute, valueHeight));

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

        public string AvatarFilePath => _avatarPanel?.SelectedFilePath;

        public event EventHandler SaveRequested;

        // "Hủy" chỉ cần đóng popup, không cần Presenter can thiệp - chuyển tiếp thẳng vào sự
        // kiện CloseRequested đã có sẵn từ BaseFormDialogForm (dùng chung với nút X/phím Esc)
        // thay vì tự quản lý thêm 1 field sự kiện mới.
        public event EventHandler CancelRequested
        {
            add => CloseRequested += value;
            remove => CloseRequested -= value;
        }

        public void ShowError(string message) => AppToast.Error(this, message);

        public void ShowSuccess(string message) => AppToast.Success(this, message);

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
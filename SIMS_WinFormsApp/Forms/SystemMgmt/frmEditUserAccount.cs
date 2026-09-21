using System;
using System.Drawing;
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
        /// <summary>Độ rộng mong muốn của popup (avatar + lưới 3 cột) - đủ rộng để hiện trọn
        /// nội dung không cần cuộn; tự thu lại nếu màn hình nhỏ hơn.</summary>
        private const int PreferredDialogWidth = 1240;

        private readonly EditUserAccountPresenter _presenter;

        private LabeledIconField _fieldFullName;
        private LabeledIconField _fieldEmail;
        private LabeledIconField _fieldPhone;
        private LabeledIconField _fieldSalary;
        private LabeledDateField _fieldDateOfBirth;
        private LabeledDateField _fieldHireDate;
        private LabeledComboField _fieldGender;
        private FieldGridPanel _personalGrid;
        private FieldGridPanel _workGrid;
        private AvatarSectionFormPanel _formLayout;
        private PrimaryButton _btnSave;

        public frmEditUserAccount(UserDetailDto user, IWin32Window owner, IUserManagementService userManagementService)
            : base(owner)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (userManagementService == null) throw new ArgumentNullException(nameof(userManagementService));

            // Đặt kích thước popup TRƯỚC khi dựng nội dung để layout tính đúng độ rộng ngay từ
            // lần đầu. Chiều cao sẽ được co vừa khít nội dung ở OnContentReady().
            Size = new Size(DialogSizing.FitWidthToScreen(owner, PreferredDialogWidth), DefaultDialogSize.Height);

            BuildContent(user);

            CloseRequested += (s, e) => Close();

            // Presenter nạp hồ sơ nhân viên (nếu có) và ẩn/hiện các trường tương ứng ngay khi khởi tạo.
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
            var fieldDob = CreateDateOfBirthField();
            var fieldGender = CreateGenderField();
            var fieldHireDate = CreateHireDateField();
            var fieldSalary = CreateSalaryField();

            // Nhóm "Thông tin cá nhân" - lưới 3 cột:
            //   Hàng 1: Họ tên | Email (chiếm 2 cột để hint hiện trên 1 dòng)
            //   Hàng 2: Số điện thoại | Ngày sinh | Giới tính
            // (Ngày sinh/Giới tính chỉ hiện với tài khoản nhân viên - xem SetEmployeeProfileVisible.)
            var personalGrid = new FieldGridPanel(3);
            personalGrid.AddField(fieldFullName);
            personalGrid.AddField(fieldEmail, 2);
            personalGrid.AddField(fieldPhone);
            personalGrid.AddField(fieldDob);
            personalGrid.AddField(fieldGender);

            // Nhóm "Thông tin công việc" (chỉ nhân viên): Ngày vào làm | Lương.
            var workGrid = new FieldGridPanel(3);
            workGrid.AddField(fieldHireDate);
            workGrid.AddField(fieldSalary);

            // Nhóm "Thông tin tài khoản": khối chỉ đọc trải hết chiều ngang.
            var accountGrid = new FieldGridPanel(3);
            accountGrid.AddField(CreateAccountInfoBox(user), 3);

            var formLayout = new AvatarSectionFormPanel();
            formLayout.AddSection(CreatePersonalInfoHeader(), personalGrid);
            formLayout.AddSection(CreateWorkInfoHeader(), workGrid);
            formLayout.AddSection(CreateAccountInfoHeader(), accountGrid);

            var avatarPanel = formLayout.Avatar;
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

            // Với các control con đều Dock=Top trong cùng 1 Panel, WinForms xếp control ADD SAU
            // CÙNG lên vị trí TRÊN CÙNG, nên add theo thứ tự NGƯỢC LẠI với thứ tự hiển thị mong
            // muốn (banner -> formLayout, từ trên xuống). Khoảng cách giữa banner và formLayout
            // do Padding.Top của AvatarSectionFormPanel đảm nhiệm (Dock bỏ qua Margin).
            ContentHost.Controls.Add(formLayout);
            ContentHost.Controls.Add(banner);

            // KHÔNG gọi formLayout.Reflow() ở đây: Form CHƯA có handle cửa sổ tại thời điểm
            // BuildContent() chạy trong constructor - xem BaseFormDialogForm.OnContentReady.
            _formLayout = formLayout;
            _personalGrid = personalGrid;
            _workGrid = workGrid;
            _fieldFullName = fieldFullName;
            _fieldEmail = fieldEmail;
            _fieldPhone = fieldPhone;
            _fieldDateOfBirth = fieldDob;
            _fieldGender = fieldGender;
            _fieldHireDate = fieldHireDate;
            _fieldSalary = fieldSalary;

            _fieldGender.SetItems(GenderOption.All);
            _fieldDateOfBirth.AllowEmpty = true;
            _fieldHireDate.MaxDate = DateTime.Today.AddYears(1);

            _fieldFullName.Value = user.FullName;
            _fieldEmail.Value = user.Email;
            _fieldPhone.Value = user.Phone;

            AddFooterButton("Hủy", false, (s, e) => RaiseCloseRequested());
            _btnSave = AddFooterButton("Lưu thay đổi", true, (s, e) => RaiseSaveRequested());
        }

        /// <summary>Reflow lần đầu phải chạy sau khi Form đã có handle cửa sổ (xem
        /// BaseFormDialogForm.OnContentReady), rồi co chiều cao popup vừa khít nội dung để
        /// không xuất hiện thanh cuộn.</summary>
        protected override void OnContentReady()
        {
            base.OnContentReady();
            _formLayout.Reflow();
            ContentHost.PerformLayout();
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
                DescriptionText = "Chỉnh thông tin liên hệ, vai trò hoặc trạng thái tài khoản."
            };
        }

        private static FieldGroupHeader CreatePersonalInfoHeader()
        {
            return new FieldGroupHeader { Icon = IconChar.IdCard, HeaderText = "Thông tin cá nhân" };
        }

        private static FieldGroupHeader CreateWorkInfoHeader()
        {
            return new FieldGroupHeader { Icon = IconChar.Briefcase, HeaderText = "Thông tin công việc" };
        }

        private static FieldGroupHeader CreateAccountInfoHeader()
        {
            return new FieldGroupHeader { Icon = IconChar.UserTag, HeaderText = "Thông tin tài khoản" };
        }

        private static LabeledIconField CreateFullNameField()
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

        private static LabeledIconField CreateEmailField()
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

        private static LabeledIconField CreatePhoneField()
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

        private static LabeledDateField CreateDateOfBirthField()
        {
            return new LabeledDateField { LabelText = "Ngày sinh", IsRequired = false };
        }

        private static LabeledComboField CreateGenderField()
        {
            return new LabeledComboField { LabelText = "Giới tính", IsRequired = false };
        }

        private static LabeledDateField CreateHireDateField()
        {
            return new LabeledDateField { LabelText = "Ngày vào làm", IsRequired = true };
        }

        private static LabeledIconField CreateSalaryField()
        {
            return new LabeledIconField
            {
                LabelText = "Lương (VNĐ)",
                IsRequired = false,
                Icon = IconChar.MoneyBillWave,
                PlaceholderText = "Để trống nếu chưa có",
                MaxLength = 15
            };
        }

        private static ReadOnlyInfoBox CreateAccountInfoBox(UserDetailDto user)
        {
            var box = new ReadOnlyInfoBox(3);
            box.AddItem(IconChar.Hashtag, "Mã tài khoản", user.UserId.ToString());
            box.AddItem(IconChar.UserTag, "Tên đăng nhập", user.Username);
            box.AddItem(IconChar.UserShield, "Vai trò", user.RoleName);
            return box;
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

        public string AvatarFilePath => _formLayout?.Avatar?.SelectedFilePath;

        public DateTime? DateOfBirth
        {
            get => _fieldDateOfBirth.Value;
            set => _fieldDateOfBirth.Value = value;
        }

        public Gender? SelectedGender
        {
            get => (_fieldGender.SelectedItem as GenderOption)?.Value;
            set => _fieldGender.SelectedItem = GenderOption.From(value);
        }

        public DateTime HireDate
        {
            get => _fieldHireDate.Value ?? DateTime.Today;
            set => _fieldHireDate.Value = value;
        }

        public string SalaryText
        {
            get => _fieldSalary.Value;
            set => _fieldSalary.Value = value;
        }

        public void SetEmployeeProfileVisible(bool visible)
        {
            _personalGrid.SetFieldVisible(_fieldDateOfBirth, visible);
            _personalGrid.SetFieldVisible(_fieldGender, visible);
            _formLayout.SetSectionVisible(_workGrid, visible);
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
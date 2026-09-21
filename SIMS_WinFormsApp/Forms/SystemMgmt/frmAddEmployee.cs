using System;
using System.Collections.Generic;
using System.Linq;
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

    public sealed class frmAddEmployee : BaseFormDialogForm, IAddEmployeeView
    {
        /// <summary>Độ rộng mong muốn của popup (avatar + lưới 3 cột) - đủ rộng để hiện trọn
        /// nội dung không cần cuộn; tự thu lại nếu màn hình nhỏ hơn.</summary>
        private const int PreferredDialogWidth = 1240;
        private const int ScreenEdgeMargin = 48;

        private readonly AddEmployeePresenter _presenter;

        private LabeledIconField _fieldFullName;
        private LabeledIconField _fieldEmail;
        private LabeledIconField _fieldPhone;
        private LabeledIconField _fieldSalary;
        private LabeledDateField _fieldDateOfBirth;
        private LabeledDateField _fieldHireDate;
        private LabeledComboField _fieldGender;
        private LabeledComboField _fieldRole;
        private AvatarSectionFormPanel _formLayout;
        private PrimaryButton _btnSave;
        private IReadOnlyList<RoleOptionDto> _roleOptions = Array.Empty<RoleOptionDto>();

        /// <summary>Item hiển thị trong ComboBox "Giới tính" - chỉ dùng nội bộ ở form này nên
        /// không tách thành DTO riêng như RoleOptionDto.</summary>
        private sealed class GenderOption
        {
            public Gender Value { get; }
            private readonly string _label;
            public GenderOption(Gender value, string label) { Value = value; _label = label; }
            public override string ToString() => _label;
        }

        private static readonly GenderOption[] GenderOptions =
        {
            new GenderOption(Gender.Male, "Nam"),
            new GenderOption(Gender.Female, "Nữ"),
            new GenderOption(Gender.Other, "Khác")
        };

        public frmAddEmployee(IWin32Window owner, IUserManagementService userManagementService) : base(owner)
        {
            if (userManagementService == null) throw new ArgumentNullException(nameof(userManagementService));

            // Đặt kích thước popup TRƯỚC khi dựng nội dung để layout tính đúng độ rộng ngay từ
            // lần đầu. Chiều cao sẽ được co vừa khít nội dung ở OnContentReady().
            Size = new System.Drawing.Size(ResolveDialogWidth(owner), DefaultDialogSize.Height);

            BuildContent();
            CloseRequested += (s, e) => Close();

            // Presenter tự gọi BindRoles(service.GetAssignableRoles()) ngay khi khởi tạo.
            _presenter = new AddEmployeePresenter(this, userManagementService);
        }

        /// <summary>Cách gọi nhanh, gọn: <c>frmAddEmployee.Show(this, service);</c> - trả về
        /// DialogResult.OK nếu đã tạo tài khoản thành công (để nơi gọi Reload lại bảng).</summary>
        public static DialogResult Show(IWin32Window owner, IUserManagementService userManagementService)
        {
            using (var dialog = new frmAddEmployee(owner, userManagementService))
            {
                return dialog.ShowDialog(owner);
            }
        }

        private static int ResolveDialogWidth(IWin32Window owner)
        {
            Screen screen = owner is Control control
                ? Screen.FromControl(control)
                : Screen.PrimaryScreen;
            return Math.Min(PreferredDialogWidth, screen.WorkingArea.Width - ScreenEdgeMargin);
        }

        #region Dựng giao diện (chỉ chạy 1 lần lúc khởi tạo)
        private void BuildContent()
        {
            HeaderTitle = "Thêm nhân viên";
            SetHeaderIcon(IconChar.UserPlus, AppColors.Accent);

            var banner = CreateBanner();

            var fieldFullName = CreateFullNameField();
            var fieldEmail = CreateEmailField();
            var fieldPhone = CreatePhoneField();
            var fieldDob = CreateDateOfBirthField();
            var fieldGender = CreateGenderField();

            var fieldRole = CreateRoleField();
            var fieldHireDate = CreateHireDateField();
            var fieldSalary = CreateSalaryField();

            // Nhóm "Thông tin cá nhân" - lưới 3 cột:
            //   Hàng 1: Họ tên | Email (chiếm 2 cột để hint hiện trên 1 dòng)
            //   Hàng 2: Số điện thoại | Ngày sinh | Giới tính
            // Thứ tự AddField cũng là thứ tự Tab (Họ tên -> Email -> SĐT -> Ngày sinh -> Giới tính).
            var personalGrid = new FieldGridPanel(3);
            personalGrid.AddField(fieldFullName);
            personalGrid.AddField(fieldEmail, 2);
            personalGrid.AddField(fieldPhone);
            personalGrid.AddField(fieldDob);
            personalGrid.AddField(fieldGender);

            // Nhóm "Thông tin công việc" - 1 hàng 3 cột: Vai trò | Ngày vào làm | Lương.
            var workGrid = new FieldGridPanel(3);
            workGrid.AddField(fieldRole);
            workGrid.AddField(fieldHireDate);
            workGrid.AddField(fieldSalary);

            // Cột trái: ảnh đại diện. Cột phải: 2 nhóm xếp chồng, ngăn cách bằng đường kẻ mảnh.
            var formLayout = new AvatarSectionFormPanel();
            formLayout.AddSection(CreatePersonalInfoHeader(), personalGrid);
            formLayout.AddSection(CreateWorkInfoHeader(), workGrid);

            // Với các control con đều Dock=Top trong cùng 1 Panel, WinForms xếp control ADD SAU
            // CÙNG lên vị trí TRÊN CÙNG, nên add theo thứ tự NGƯỢC LẠI với thứ tự hiển thị mong
            // muốn (banner -> formLayout, từ trên xuống). Khoảng cách giữa banner và formLayout
            // do Padding.Top của AvatarSectionFormPanel đảm nhiệm (Dock bỏ qua Margin).
            ContentHost.Controls.Add(formLayout);
            ContentHost.Controls.Add(banner);

            // KHÔNG gọi formLayout.Reflow() ở đây: Form CHƯA có handle cửa sổ tại thời điểm
            // BuildContent() chạy trong constructor - xem BaseFormDialogForm.OnContentReady.
            _formLayout = formLayout;
            _fieldFullName = fieldFullName;
            _fieldEmail = fieldEmail;
            _fieldPhone = fieldPhone;
            _fieldDateOfBirth = fieldDob;
            _fieldGender = fieldGender;
            _fieldRole = fieldRole;
            _fieldHireDate = fieldHireDate;
            _fieldSalary = fieldSalary;

            _fieldGender.SetItems(GenderOptions);
            _fieldDateOfBirth.AllowEmpty = true;
            _fieldHireDate.Value = DateTime.Today;
            _fieldHireDate.MaxDate = DateTime.Today.AddYears(1);

            AddFooterButton("Hủy", false, (s, e) => RaiseCloseRequested());
            _btnSave = AddFooterButton("Thêm mới", true, (s, e) => RaiseSaveRequested());
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

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            _fieldFullName.FocusInput();
        }

        private static InfoBannerPanel CreateBanner()
        {
            return new InfoBannerPanel
            {
                Icon = IconChar.UserPlus,
                TitleText = "Thêm nhân viên mới",
                DescriptionText = "Tài khoản đăng nhập sẽ được tạo tự động và gửi qua email sau khi lưu."
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

        private static LabeledIconField CreateFullNameField()
        {
            return new LabeledIconField
            {
                LabelText = "Họ và tên",
                IsRequired = true,
                Icon = IconChar.User,
                PlaceholderText = "Nhập họ và tên",
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
                HintText = "Tên đăng nhập và mật khẩu sẽ được gửi tới email này.",
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

        private static LabeledComboField CreateRoleField()
        {
            return new LabeledComboField { LabelText = "Vai trò", IsRequired = true };
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
        #endregion

        #region IAddEmployeeView
        public string FullName { get => _fieldFullName.Value; set => _fieldFullName.Value = value; }
        public string Email { get => _fieldEmail.Value; set => _fieldEmail.Value = value; }
        public string Phone { get => _fieldPhone.Value; set => _fieldPhone.Value = value; }

        public string AvatarFilePath => _formLayout?.Avatar?.SelectedFilePath;
        public DateTime? DateOfBirth { get => _fieldDateOfBirth.Value; set => _fieldDateOfBirth.Value = value; }

        public Gender? SelectedGender
        {
            get => (_fieldGender.SelectedItem as GenderOption)?.Value;
            set => _fieldGender.SelectedItem = value.HasValue
                ? GenderOptions.FirstOrDefault(g => g.Value == value.Value)
                : null;
        }

        public int? SelectedRoleId
        {
            get => (_fieldRole.SelectedItem as RoleOptionDto)?.RoleId;
            set => _fieldRole.SelectedItem = value.HasValue
                ? _roleOptions.FirstOrDefault(r => r.RoleId == value.Value)
                : null;
        }

        public DateTime HireDate
        {
            get => _fieldHireDate.Value ?? DateTime.Today;
            set => _fieldHireDate.Value = value;
        }

        public string SalaryText { get => _fieldSalary.Value; set => _fieldSalary.Value = value; }

        public void BindRoles(IReadOnlyList<RoleOptionDto> roles)
        {
            _roleOptions = roles ?? Array.Empty<RoleOptionDto>();
            _fieldRole.SetItems(_roleOptions);
        }

        public event EventHandler SaveRequested;

        // "Hủy" chỉ cần đóng popup, không cần Presenter can thiệp - chuyển tiếp thẳng vào sự
        // kiện CloseRequested đã có sẵn từ BaseFormDialogForm (dùng chung với nút X/phím Esc).
        public event EventHandler CancelRequested
        {
            add => CloseRequested += value;
            remove => CloseRequested -= value;
        }

        public void ShowError(string message) => AppToast.Error(this, message);

        public void SetSaving(bool isSaving)
        {
            SetFooterButtonsEnabled(!isSaving);
            _btnSave.Text = isSaving ? "Đang lưu..." : "Thêm mới";
        }

        public void CloseOnSuccess()
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        public void ShowCreationResult(string username, bool emailSent, string emailError, string rawPassword)
        {
            string info = "Tên đăng nhập: " + username + "\n\n";
            if (!emailSent)
            {

                DialogHelper.ShowError(this,
                    info + "Đã tạo tài khoản nhưng gửi email thất bại"
                    + (string.IsNullOrEmpty(emailError) ? "." : ": " + emailError) + "\n\n"
                    + "Mật khẩu tạm thời: " + rawPassword + "\n"
                    + "Vui lòng cung cấp mật khẩu này cho nhân viên thủ công.");
            }
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
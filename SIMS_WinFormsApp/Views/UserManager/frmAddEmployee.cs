using System;
using System.Collections.Generic;
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
using SIMS_WinFormsApp.Views.Toast;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Dialogs;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.Views.UserManager
{
    public sealed partial class frmAddEmployee : BaseFormDialogForm, IAddEmployeeView
    {
        private const int PreferredDialogWidth = 1240;
        private const int ScreenEdgeMargin = 48;
        private const long MaxAvatarSize = 5 * 1024 * 1024;

        private readonly AddEmployeePresenter _presenter;
        private IReadOnlyList<RoleOptionDto> _roleOptions = Array.Empty<RoleOptionDto>();
        private string _avatarFilePath;

        private sealed class GenderOption
        {
            public Gender Value { get; }

            private readonly string _label;

            public GenderOption(Gender value, string label)
            {
                Value = value;
                _label = label;
            }

            public override string ToString() => _label;
        }

        private static readonly GenderOption[] GenderOptions =
        {
            new GenderOption(Gender.Male, "Nam"),
            new GenderOption(Gender.Female, "Nữ"),
            new GenderOption(Gender.Other, "Khác")
        };

        public frmAddEmployee()
        {
            InitializeComponent();
            InitializeView();
            HeaderTitle = "Thêm nhân viên";
            _designerFullName.Text = "Nguyễn Văn An";
            _designerEmail.Text = "an@example.com";
            _designerGender.SelectedItem = GenderOptions[0];
            BindRoles(new[]
            {
                new RoleOptionDto { RoleId = 1, RoleCode = "SALES_STAFF", RoleName = "Nhân viên bán hàng" }
            });
        }

        public frmAddEmployee(IWin32Window owner, IUserManagementService userManagementService) : base(owner)
        {
            if (userManagementService == null) throw new ArgumentNullException(nameof(userManagementService));

            InitializeComponent();
            InitializeView();
            Size = new Size(ResolveDialogWidth(owner), DefaultDialogSize.Height);
            HeaderTitle = "Thêm nhân viên";
            CloseRequested += (s, e) => Close();
            _presenter = new AddEmployeePresenter(this, userManagementService);
        }

        public static DialogResult Show(IWin32Window owner, IUserManagementService userManagementService)
        {
            using (var dialog = new frmAddEmployee(owner, userManagementService))
                return dialog.ShowDialog(owner);
        }

        private static int ResolveDialogWidth(IWin32Window owner)
        {
            Screen screen = owner is Control control
                ? Screen.FromControl(control)
                : Screen.PrimaryScreen;
            return Math.Min(PreferredDialogWidth, screen.WorkingArea.Width - ScreenEdgeMargin);
        }

        private void InitializeView()
        {
            SetHeaderIcon(IconChar.UserPlus, AppColors.Accent);
            _designerFullName.MaxLength = 100;
            _designerEmail.MaxLength = 150;
            _designerPhone.MaxLength = 15;
            _designerSalary.MaxLength = 19;
            _designerBirthDate.Format = DateTimePickerFormat.Custom;
            _designerBirthDate.CustomFormat = "dd/MM/yyyy";
            _designerBirthDate.ShowCheckBox = true;
            _designerBirthDate.Checked = false;
            _designerBirthDate.MaxDate = DateTime.Today;
            _designerHireDate.Format = DateTimePickerFormat.Custom;
            _designerHireDate.CustomFormat = "dd/MM/yyyy";
            _designerHireDate.MaxDate = DateTime.Today.AddYears(1);
            _designerHireDate.Value = DateTime.Today;
            _designerGender.Items.Clear();
            foreach (GenderOption option in GenderOptions)
                _designerGender.Items.Add(option);
            _designerGender.SelectedIndex = -1;
            NumberInputFormatter.Attach(_designerSalary);
            _designerChooseAvatarButton.Click += ChooseAvatarButton_Click;
            _designerCancelButton.Click += (s, e) => RaiseCloseRequested();
            _designerSaveButton.Click += (s, e) => RaiseSaveRequested();
            _designerFullName.TextChanged += (s, e) => UpdateAvatarInitial();
            _designerAvatarCircle.Resize += AvatarCircle_Resize;
            ApplyAvatarCircleRegion();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            _designerFullName.Focus();
        }

        private void ChooseAvatarButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog
            {
                Title = "Chọn ảnh đại diện",
                Filter = "Tệp ảnh (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp",
                CheckFileExists = true
            })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                string extension = Path.GetExtension(dialog.FileName);
                if (!string.Equals(extension, ".jpg", StringComparison.OrdinalIgnoreCase)
                    && !string.Equals(extension, ".jpeg", StringComparison.OrdinalIgnoreCase)
                    && !string.Equals(extension, ".png", StringComparison.OrdinalIgnoreCase)
                    && !string.Equals(extension, ".bmp", StringComparison.OrdinalIgnoreCase))
                {
                    ShowError("Chỉ chấp nhận các định dạng ảnh: JPG, PNG, BMP.");
                    return;
                }

                if (new FileInfo(dialog.FileName).Length > MaxAvatarSize)
                {
                    ShowError("Kích thước ảnh vượt quá giới hạn cho phép (tối đa 5MB).");
                    return;
                }

                try
                {
                    SetAvatarImage(ReadAvatarImage(dialog.FileName));
                    _avatarFilePath = dialog.FileName;
                }
                catch (Exception ex)
                {
                    ShowError("Không thể đọc ảnh đã chọn: " + ex.Message);
                }
            }
        }

        private static Image ReadAvatarImage(string filePath)
        {
            using (var stream = new MemoryStream(File.ReadAllBytes(filePath)))
            using (var source = Image.FromStream(stream))
                return new Bitmap(source);
        }

        private void SetAvatarImage(Image image)
        {
            Image previousImage = _designerAvatarImage.Image;
            _designerAvatarImage.Image = image;
            _designerAvatarImage.Visible = image != null;
            _designerAvatarLabel.Visible = image == null;
            if (previousImage != null) previousImage.Dispose();
        }

        private void UpdateAvatarInitial()
        {
            string fullName = _designerFullName.Text;
            _designerAvatarLabel.Text = string.IsNullOrWhiteSpace(fullName)
                ? "?"
                : fullName.Trim().Substring(0, 1).ToUpperInvariant();
        }

        private void AvatarCircle_Resize(object sender, EventArgs e) => ApplyAvatarCircleRegion();

        private void ApplyAvatarCircleRegion()
        {
            if (_designerAvatarCircle.Width <= 0 || _designerAvatarCircle.Height <= 0) return;
            using (var path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, _designerAvatarCircle.Width, _designerAvatarCircle.Height);
                Region previousRegion = _designerAvatarCircle.Region;
                _designerAvatarCircle.Region = new Region(path);
                if (previousRegion != null) previousRegion.Dispose();
            }
        }

        public string FullName
        {
            get => _designerFullName.Text.Trim();
            set => _designerFullName.Text = value ?? string.Empty;
        }

        public string Email
        {
            get => _designerEmail.Text.Trim();
            set => _designerEmail.Text = value ?? string.Empty;
        }

        public string Phone
        {
            get => _designerPhone.Text.Trim();
            set => _designerPhone.Text = value ?? string.Empty;
        }

        public string AvatarFilePath => _avatarFilePath;

        public DateTime? DateOfBirth
        {
            get => _designerBirthDate.Checked ? _designerBirthDate.Value.Date : (DateTime?)null;
            set
            {
                _designerBirthDate.Checked = value.HasValue;
                if (value.HasValue) _designerBirthDate.Value = value.Value;
            }
        }

        public Gender? SelectedGender
        {
            get => (_designerGender.SelectedItem as GenderOption)?.Value;
            set => _designerGender.SelectedItem = value.HasValue
                ? Array.Find(GenderOptions, option => option.Value == value.Value)
                : null;
        }

        public int? SelectedRoleId
        {
            get => (_designerRole.SelectedItem as RoleOptionDto)?.RoleId;
            set => _designerRole.SelectedItem = value.HasValue
                ? FindRole(value.Value)
                : null;
        }

        public DateTime HireDate
        {
            get => _designerHireDate.Value.Date;
            set => _designerHireDate.Value = value;
        }

        public string SalaryText
        {
            get => NumberInputFormatter.DigitsOnly(_designerSalary.Text);
            set => _designerSalary.Text = NumberInputFormatter.FormatInteger(value);
        }

        public void BindRoles(IReadOnlyList<RoleOptionDto> roles)
        {
            _roleOptions = roles ?? Array.Empty<RoleOptionDto>();
            _designerRole.BeginUpdate();
            try
            {
                _designerRole.Items.Clear();
                foreach (RoleOptionDto role in _roleOptions)
                    _designerRole.Items.Add(role);
                _designerRole.SelectedIndex = -1;
            }
            finally
            {
                _designerRole.EndUpdate();
            }
        }

        private RoleOptionDto FindRole(int roleId)
        {
            foreach (RoleOptionDto role in _roleOptions)
                if (role.RoleId == roleId)
                    return role;
            return null;
        }

        public event EventHandler SaveRequested;

        public event EventHandler CancelRequested
        {
            add => CloseRequested += value;
            remove => CloseRequested -= value;
        }

        public void ShowError(string message) => AppToast.Error(this, message);

        public void SetSaving(bool isSaving)
        {
            _designerCancelButton.Enabled = !isSaving;
            _designerSaveButton.Enabled = !isSaving;
            _designerSaveButton.Text = isSaving ? "Đang lưu..." : "Thêm mới";
        }

        public void CloseOnSuccess()
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        public void ShowCreationResult(string username, bool emailSent, string emailError, string rawPassword)
        {
            if (emailSent) return;
            string info = "Tên đăng nhập: " + username + "\n\n";
            DialogHelper.ShowError(this,
                info + "Đã tạo tài khoản nhưng gửi email thất bại"
                + (string.IsNullOrEmpty(emailError) ? "." : ": " + emailError) + "\n\n"
                + "Mật khẩu tạm thời: " + rawPassword + "\n"
                + "Vui lòng cung cấp mật khẩu này cho nhân viên thủ công.");
        }

        private void RaiseSaveRequested() => SaveRequested?.Invoke(this, EventArgs.Empty);

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _presenter?.Dispose();
                if (_designerAvatarImage.Image != null)
                    _designerAvatarImage.Image.Dispose();
            }
            base.Dispose(disposing);
        }

        private void _designerPhone_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

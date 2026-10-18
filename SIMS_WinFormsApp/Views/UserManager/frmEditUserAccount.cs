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

namespace SIMS_WinFormsApp.Views.UserManager
{
    public sealed partial class frmEditUserAccount : BaseFormDialogForm, IEditUserAccountView
    {
        private const int PreferredDialogWidth = 1240;
        private const long MaxAvatarSize = 5 * 1024 * 1024;

        private readonly EditUserAccountPresenter _presenter;
        private string _avatarFilePath;

        public frmEditUserAccount()
        {
            InitializeComponent();
            InitializeView();
            DisplayUser(new UserDetailDto
            {
                UserId = 1,
                Username = "nhanvien",
                FullName = "Nguyễn Văn An",
                Email = "an@example.com",
                Phone = "0901234567",
                RoleCode = RoleCodes.SalesStaff,
                RoleName = "Nhân viên bán hàng",
                Status = UserStatus.Active
            });
        }

        public frmEditUserAccount(
            UserDetailDto user,
            IWin32Window owner,
            IUserManagementService userManagementService) : base(owner)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (userManagementService == null) throw new ArgumentNullException(nameof(userManagementService));

            InitializeComponent();
            InitializeView();
            Size = new Size(DialogSizing.FitWidthToScreen(owner, PreferredDialogWidth), DefaultDialogSize.Height);
            DisplayUser(user);
            CloseRequested += (s, e) => Close();
            _presenter = new EditUserAccountPresenter(this, userManagementService, user.UserId);
        }

        public static DialogResult Show(
            IWin32Window owner,
            UserDetailDto user,
            IUserManagementService userManagementService)
        {
            using (var dialog = new frmEditUserAccount(user, owner, userManagementService))
                return dialog.ShowDialog(owner);
        }

        private void InitializeView()
        {
            SetHeaderIcon(IconChar.UserPen, AppColors.Accent);
            foreach (GenderOption option in GenderOption.All)
                _designerGender.Items.Add(option);
            _designerBirthDate.MaxDate = DateTime.Today;
            _designerBirthDate.Checked = false;
            _designerHireDate.MaxDate = DateTime.Today.AddYears(1);
            _designerSalary.KeyPress += Salary_KeyPress;
            _designerSalary.Leave += Salary_Leave;
            _designerChooseAvatarButton.Click += ChooseAvatarButton_Click;
            _designerCancelButton.Click += (s, e) => RaiseCloseRequested();
            _designerSaveButton.Click += (s, e) => RaiseSaveRequested();
            _designerFullName.TextChanged += (s, e) => UpdateAvatarInitial();
            _designerAvatarCircle.Resize += AvatarCircle_Resize;
            ApplyAvatarCircleRegion();
        }

        private void DisplayUser(UserDetailDto user)
        {
            HeaderTitle = "Cập nhật tài khoản " + GetAccountTypeLabel(user);
            _designerFullName.Text = user.FullName ?? string.Empty;
            _designerEmail.Text = user.Email ?? string.Empty;
            _designerPhone.Text = user.Phone ?? string.Empty;
            _designerAccountIdValue.Text = user.UserId.ToString();
            _designerUsernameValue.Text = user.Username ?? string.Empty;
            _designerRoleValue.Text = user.RoleName ?? string.Empty;
            UpdateAvatarInitial();
            LoadAvatar(user.AvatarUrl);
        }

        private static string GetAccountTypeLabel(UserDetailDto user)
        {
            return string.Equals(user.RoleCode, RoleCodes.Customer, StringComparison.OrdinalIgnoreCase)
                ? "khách hàng"
                : "nhân viên";
        }

        private void UpdateAvatarInitial()
        {
            string fullName = _designerFullName.Text;
            _designerAvatarInitial.Text = string.IsNullOrWhiteSpace(fullName)
                ? "?"
                : fullName.Trim().Substring(0, 1).ToUpperInvariant();
        }

        private void LoadAvatar(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath)) return;

            try
            {
                SetAvatarImage(ReadAvatarImage(filePath));
            }
            catch (Exception)
            {
                _designerAvatarImage.Image = null;
                _designerAvatarImage.Visible = false;
                _designerAvatarInitial.Visible = true;
            }
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

                var fileInfo = new FileInfo(dialog.FileName);
                if (fileInfo.Length > MaxAvatarSize)
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
            _designerAvatarInitial.Visible = image == null;
            if (previousImage != null) previousImage.Dispose();
        }

        private void AvatarCircle_Resize(object sender, EventArgs e) => ApplyAvatarCircleRegion();

        private static void Salary_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void Salary_Leave(object sender, EventArgs e)
        {
            _designerSalary.Text = NumberInputFormatter.FormatInteger(_designerSalary.Text);
        }

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
            set => _designerGender.SelectedItem = GenderOption.From(value);
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

        public void SetEmployeeProfileVisible(bool visible)
        {
            _designerBirthDateLabel.Visible = visible;
            _designerBirthDate.Visible = visible;
            _designerGenderLabel.Visible = visible;
            _designerGender.Visible = visible;
            _designerHireDateLabel.Visible = visible;
            _designerHireDate.Visible = visible;
            _designerSalaryLabel.Visible = visible;
            _designerSalary.Visible = visible;
            _designerWorkHeader.Visible = visible;
            _designerWorkSeparator.Visible = visible;
        }

        public event EventHandler SaveRequested;

        public event EventHandler CancelRequested
        {
            add => CloseRequested += value;
            remove => CloseRequested -= value;
        }

        public void ShowError(string message) => AppToast.Error(this, message);

        public void ShowSuccess(string message) => AppToast.Success(this, message);

        public void SetSaving(bool isSaving)
        {
            _designerCancelButton.Enabled = !isSaving;
            _designerSaveButton.Enabled = !isSaving;
            _designerSaveButton.Text = isSaving ? "Đang lưu..." : "Lưu thay đổi";
        }

        public void CloseOnSuccess()
        {
            DialogResult = DialogResult.OK;
            Close();
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

        private void _designerAccountIdValue_Click(object sender, EventArgs e)
        {

        }

        private void _designerHireDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void _designerGenderLabel_Click(object sender, EventArgs e)
        {

        }

        private void _designerBirthDateLabel_Click(object sender, EventArgs e)
        {

        }
    }
}

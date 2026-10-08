using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Models.Enums;
using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.UI.Controls.Toast;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.Views.Profile
{
    public partial class ucMyProfile : UserControl, IEditUserAccountView, IChangePasswordView
    {
        private const long MaxAvatarFileSizeBytes = 5 * 1024 * 1024;

        private IUserManagementService _userManagementService;
        private IAuthService _authService;
        private IUserRepository _userRepository;

        private EditUserAccountPresenter _profilePresenter;
        private ChangePasswordPresenter _passwordPresenter;
        private bool _runtimeInitialized;

        private string _avatarFilePath;

        // The profile page shares the account presenter, which may preserve employee data
        // even though those fields are not part of this view.
        private DateTime? _dateOfBirth;
        private Gender? _selectedGender;
        private DateTime _hireDate = DateTime.Today;
        private string _salaryText = string.Empty;

        public event EventHandler ProfileUpdated;

        public ucMyProfile() : this(null, null, null)
        {
        }

        public ucMyProfile(
            IUserManagementService userManagementService = null,
            IAuthService authService = null,
            IUserRepository userRepository = null)
        {
            InitializeComponent();

            _userManagementService = userManagementService;
            _authService = authService;
            _userRepository = userRepository;
            btnChooseAvatar.Click += ChooseAvatar_Click;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            InitializeRuntime();
        }

        private void InitializeRuntime()
        {
            if (_runtimeInitialized)
                return;

            _userManagementService = _userManagementService ?? AppComposition.CreateUserManagementService();
            _authService = _authService ?? AppComposition.CreateAuthService();
            _userRepository = _userRepository ?? AppComposition.CreateUserRepository();

            LoadCurrentUser();

            _profilePresenter = new EditUserAccountPresenter(this, _userManagementService, CurrentUserId);
            _passwordPresenter = new ChangePasswordPresenter(this, _authService);
            Disposed += (s, e) => _profilePresenter?.Dispose();

            btnSaveProfile.Click += (s, e) => SaveRequested?.Invoke(this, EventArgs.Empty);
            btnChangePassword.Click += async (s, e) => await _passwordPresenter.ChangeAsync();
            _runtimeInitialized = true;
        }

        private void LoadCurrentUser()
        {
            var user = UserSession.Instance.CurrentUser;
            if (user == null)
                return;

            lblAvatarInitial.Text = GetInitial(user.FullName);
            SetAvatarPreview(null, null);
            if (!string.IsNullOrWhiteSpace(user.AvatarUrl) && File.Exists(user.AvatarUrl))
            {
                try
                {
                    SetAvatarPreview(LoadImageWithoutLockingFile(user.AvatarUrl), user.AvatarUrl);
                }
                catch (Exception)
                {
                    // A missing or invalid avatar falls back to the user's initial.
                }
            }
            lblFullName.Text = user.FullName;
            lblRoleBadge.Text = string.IsNullOrWhiteSpace(user.RoleName) ? user.RoleCode : user.RoleName;
            lblSidebarEmail.Text = "Email: " + (user.Email ?? string.Empty);
            lblSidebarPhone.Text = "Số điện thoại: " +
                (string.IsNullOrWhiteSpace(user.Phone) ? "Chưa cập nhật" : user.Phone);
            lblJoinedAt.Text = "Tham gia từ " + user.CreatedAt.ToString("d/M/yyyy");

            txtFullName.Text = user.FullName;
            txtPhone.Text = user.Phone;
            txtEmail.Text = user.Email;
        }

        private void ChooseAvatar_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog
            {
                Title = "Chọn ảnh đại diện",
                Filter = "Tệp ảnh (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp",
                CheckFileExists = true
            })
            {
                if (dialog.ShowDialog(FindForm()) != DialogResult.OK)
                    return;

                string extension = Path.GetExtension(dialog.FileName)?.ToLowerInvariant() ?? string.Empty;
                if (extension != ".jpg" && extension != ".jpeg" &&
                    extension != ".png" && extension != ".bmp")
                {
                    MessageBox.Show(FindForm(),
                        "Chỉ chấp nhận các định dạng ảnh: JPG, PNG, BMP.",
                        "Ảnh không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (new FileInfo(dialog.FileName).Length > MaxAvatarFileSizeBytes)
                {
                    MessageBox.Show(FindForm(),
                        "Kích thước ảnh vượt quá giới hạn cho phép (tối đa 5MB).",
                        "Ảnh quá lớn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    SetAvatarPreview(LoadImageWithoutLockingFile(dialog.FileName), dialog.FileName);
                }
                catch (Exception)
                {
                    MessageBox.Show(FindForm(),
                        "Không thể đọc ảnh đã chọn, vui lòng thử lại với ảnh khác.",
                        "Lỗi đọc ảnh", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private static Image LoadImageWithoutLockingFile(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            using (var stream = new MemoryStream(bytes))
            using (var source = Image.FromStream(stream))
                return new Bitmap(source);
        }

        private void SetAvatarPreview(Image image, string sourceFilePath)
        {
            if (!ReferenceEquals(pictureAvatar.Image, image))
                pictureAvatar.Image?.Dispose();

            pictureAvatar.Image = image;
            pictureAvatar.Visible = image != null;
            lblAvatarInitial.Visible = image == null;
            _avatarFilePath = sourceFilePath;
        }

        private static string GetInitial(string fullName) =>
            string.IsNullOrWhiteSpace(fullName) ? "?" : fullName.Trim().Substring(0, 1).ToUpperInvariant();

        #region IEditUserAccountView
        public string FullName
        {
            get => txtFullName.Text;
            set => txtFullName.Text = value;
        }

        public string Email
        {
            get => txtEmail.Text;
            set => txtEmail.Text = value;
        }

        public string Phone
        {
            get => txtPhone.Text;
            set => txtPhone.Text = value;
        }

        public string AvatarFilePath => _avatarFilePath;

        public DateTime? DateOfBirth
        {
            get => _dateOfBirth;
            set => _dateOfBirth = value;
        }

        public Gender? SelectedGender
        {
            get => _selectedGender;
            set => _selectedGender = value;
        }

        public DateTime HireDate
        {
            get => _hireDate;
            set => _hireDate = value;
        }

        public string SalaryText
        {
            get => _salaryText;
            set => _salaryText = value;
        }

        public void SetEmployeeProfileVisible(bool visible)
        {
            // Employee fields are not displayed here; their values remain available to the presenter.
        }

        public event EventHandler SaveRequested;

        event EventHandler IEditUserAccountView.CancelRequested
        {
            add { }
            remove { }
        }

        void IEditUserAccountView.ShowError(string message) => ShowProfileError(message);

        void IEditUserAccountView.ShowSuccess(string message) => AppToast.Success(this, message);

        public void SetSaving(bool isSaving)
        {
            txtFullName.Enabled = !isSaving;
            txtPhone.Enabled = !isSaving;
            btnSaveProfile.Enabled = !isSaving;
            btnSaveProfile.Text = isSaving ? "Đang lưu..." : "Lưu thay đổi";
        }

        void IEditUserAccountView.CloseOnSuccess() => OnProfileSavedSuccessfully();

        private void ShowProfileError(string message)
        {
            lblProfileError.Text = message ?? string.Empty;
            lblProfileError.Visible = !string.IsNullOrEmpty(message);
        }

        private void OnProfileSavedSuccessfully()
        {
            var refreshed = _userRepository.FindById(CurrentUserId);
            if (refreshed != null)
            {
                UserSession.Instance.SignIn(refreshed);
                LoadCurrentUser();
            }

            AppToast.Success(this, "Cập nhật thông tin cá nhân thành công.");
            ProfileUpdated?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region IChangePasswordView
        public string CurrentPassword => txtCurrentPassword.Text;
        public string NewPassword => txtNewPassword.Text;
        public string Confirmation => txtConfirmPassword.Text;
        public int CurrentUserId => UserSession.Instance.CurrentUser?.UserId ?? 0;

        public void ClearError() => ShowPasswordError(string.Empty);

        void IChangePasswordView.ShowError(string message) => ShowPasswordError(message);

        public void SetLoading(bool isBusy)
        {
            txtCurrentPassword.Enabled = !isBusy;
            txtNewPassword.Enabled = !isBusy;
            txtConfirmPassword.Enabled = !isBusy;
            btnChangePassword.Enabled = !isBusy;
            btnChangePassword.Text = isBusy ? "Đang xử lý..." : "Đổi mật khẩu";
        }

        void IChangePasswordView.ShowSuccess(string message) => AppToast.Success(this, message);

        void IChangePasswordView.CloseOnSuccess() => OnPasswordChangedSuccessfully();

        private void ShowPasswordError(string message)
        {
            lblPasswordError.Text = message ?? string.Empty;
            lblPasswordError.Visible = !string.IsNullOrEmpty(message);
        }

        private void OnPasswordChangedSuccessfully()
        {
            txtCurrentPassword.Clear();
            txtNewPassword.Clear();
            txtConfirmPassword.Clear();
            AppToast.Success(this, "Đổi mật khẩu thành công.");
        }
        #endregion
    }
}

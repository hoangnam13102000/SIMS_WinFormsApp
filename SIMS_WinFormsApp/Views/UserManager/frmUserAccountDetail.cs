using System;
using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.Enums;
using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.MVP.ViewModels;
using SIMS_WinFormsApp.Services.Implementations;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.Views.UserManager
{
    public sealed partial class frmUserAccountDetail : Form, IUserAccountDetailView
    {
        private static readonly Size DetailDialogSize = new Size(1150, 660);

        private readonly UserAccountDetailPresenter _presenter;
        private readonly UserDetailDto _user;

        public event EventHandler CloseRequested;

        public frmUserAccountDetail()
        {
            InitializeComponent();
            ConfigureDialog(null);
            _user = CreateDesignSampleUser();
            Render(new UserAccountDetailViewModelBuilder().Build(_user));
        }

        public frmUserAccountDetail(UserDetailDto user)
            : this(user, null, null, new UserAccountDetailViewModelBuilder())
        {
        }

        public frmUserAccountDetail(UserDetailDto user, IWin32Window owner)
            : this(user, owner, null, new UserAccountDetailViewModelBuilder())
        {
        }

        public frmUserAccountDetail(
            UserDetailDto user,
            IWin32Window owner,
            IUserManagementService userManagementService)
            : this(user, owner, userManagementService, new UserAccountDetailViewModelBuilder())
        {
        }

        public frmUserAccountDetail(
            UserDetailDto user,
            IWin32Window owner,
            IUserManagementService userManagementService,
            IUserAccountDetailViewModelBuilder viewModelBuilder)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (viewModelBuilder == null) throw new ArgumentNullException(nameof(viewModelBuilder));

            InitializeComponent();
            ConfigureDialog(owner);
            _user = user;
            ConfigureAccountFields();

            _presenter = new UserAccountDetailPresenter(this, viewModelBuilder, userManagementService);
            _presenter.Load(user);
        }

        public static DialogResult Show(
            IWin32Window owner,
            UserDetailDto user,
            IUserManagementService userManagementService = null)
        {
            using (var dialog = new frmUserAccountDetail(user, owner, userManagementService))
                return dialog.ShowDialog(owner);
        }

        private static UserDetailDto CreateDesignSampleUser()
        {
            return new UserDetailDto
            {
                Username = "nguyenvana",
                FullName = "Nguyễn Văn A",
                Email = "nguyenvana@example.com",
                Phone = "0901 234 567",
                RoleCode = "STAFF",
                RoleName = "Nhân viên",
                Status = UserStatus.Active,
                CreatedAt = new DateTime(2025, 1, 15),
                FailedLoginCount = 0
            };
        }

        private void ConfigureDialog(IWin32Window owner)
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = owner == null ? FormStartPosition.CenterScreen : FormStartPosition.Manual;
            ShowInTaskbar = false;
            KeyPreview = true;
            Size = DetailDialogSize;
            MinimumSize = DetailDialogSize;
            MaximumSize = DetailDialogSize;
            BackColor = DialogTheme.SurfaceColor;

            _btnClose.Click += (s, e) => RaiseCloseRequested();
            _btnFooterClose.Click += (s, e) => RaiseCloseRequested();
            _tabAccount.Click += (s, e) => SelectDetailTab(showAccount: true);
            _tabSecurity.Click += (s, e) => SelectDetailTab(showAccount: false);
            KeyDown += Form_KeyDown;
            Shown += (s, e) => CenterOnOwner(owner);

            _lblDialogTitle.Text = "Chi tiết tài khoản";
            ConfigureAccountFields();
            SelectDetailTab(showAccount: true);
        }

        private void CenterOnOwner(IWin32Window owner)
        {
            if (owner is Control control)
            {
                Rectangle ownerBounds = owner is Form ownerForm
                    ? ownerForm.Bounds
                    : new Rectangle(control.PointToScreen(Point.Empty), control.Size);
                Location = new Point(
                    Math.Max(0, ownerBounds.Left + (ownerBounds.Width - Width) / 2),
                    Math.Max(0, ownerBounds.Top + (ownerBounds.Height - Height) / 2));
                return;
            }

            Rectangle workingArea = Screen.FromControl(this).WorkingArea;
            Location = new Point(
                workingArea.Left + (workingArea.Width - Width) / 2,
                workingArea.Top + (workingArea.Height - Height) / 2);
        }

        private void ConfigureAccountFields()
        {
            if (_user == null) return;
            bool isEmployeeAccount = !string.Equals(
                _user.RoleCode,
                RoleCodes.Customer,
                StringComparison.OrdinalIgnoreCase);
            _itemGender.Visible = isEmployeeAccount;
            _itemSalary.Visible = isEmployeeAccount;
            _designerAccountGrid.RowStyles[3].Height = isEmployeeAccount ? 96F : 0F;
        }

        private void SelectDetailTab(bool showAccount)
        {
            _designerAccountGrid.Visible = showAccount;
            _designerSecurityGrid.Visible = !showAccount;

            _tabAccount.BackColor = showAccount ? AppColors.AccentBgSoft : AppColors.BgLighter;
            _tabAccount.ForeColor = showAccount ? AppColors.Accent : AppColors.TextSecondary;
            _tabSecurity.BackColor = showAccount ? AppColors.BgLighter : AppColors.AccentBgSoft;
            _tabSecurity.ForeColor = showAccount ? AppColors.TextSecondary : AppColors.Accent;
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Escape) return;
            e.Handled = true;
            RaiseCloseRequested();
        }

        private void RaiseCloseRequested() => CloseRequested?.Invoke(this, EventArgs.Empty);

        public void Render(UserAccountDetailViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

            _lblAvatarInitial.Text = viewModel.AvatarInitial;
            _lblAvatarName.Text = viewModel.FullName;
            _lblAvatarSubtitle.Text = viewModel.HandleText;
            _lblDialogTitle.Text = "Chi tiết tài khoản " + GetAccountTypeLabel(viewModel.RoleCode);

            _itemAccountCode.ValueText = viewModel.AccountCode;
            _itemRole.ValueText = viewModel.RoleName;
            _itemEmail.ValueText = viewModel.Email;
            _itemPhone.ValueText = viewModel.Phone;
            _itemCreatedAt.ValueText = viewModel.CreatedAtText;
            _itemAccountStatus.ValueText = viewModel.AccountStatus.Text;
            _itemAccountStatus.Icon = viewModel.AccountStatus.Icon;
            _itemAccountStatus.IconColor = viewModel.AccountStatus.Foreground;
            _itemAccountStatus.IconBackground = viewModel.AccountStatus.Background;
            _itemLockStatus.ValueText = viewModel.LockStatus.Text;
            _itemLockStatus.Icon = viewModel.LockStatus.Icon;
            _itemLockStatus.IconColor = viewModel.LockStatus.Foreground;
            _itemLockStatus.IconBackground = viewModel.LockStatus.Background;
            _itemFailedLogin.ValueText = viewModel.FailedLoginCountText;
            _itemGender.ValueText = viewModel.GenderText;
            _itemSalary.ValueText = viewModel.SalaryText;
        }

        private static string GetAccountTypeLabel(string roleCode)
        {
            return string.Equals(roleCode, RoleCodes.Customer, StringComparison.OrdinalIgnoreCase)
                ? "khách hàng"
                : "nhân viên";
        }

        public void CloseView() => Close();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _presenter?.Dispose();
            base.Dispose(disposing);
        }

        private void _tabAccount_Click(object sender, EventArgs e)
        {

        }

        private void _tabSecurity_Click(object sender, EventArgs e)
        {

        }
    }
}

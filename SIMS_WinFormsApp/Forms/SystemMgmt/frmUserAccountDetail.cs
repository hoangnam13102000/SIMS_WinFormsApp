using System;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.Enums;
using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.MVP.ViewModels;
using SIMS_WinFormsApp.Services.Implementations;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.Forms.SystemMgmt
{
    public sealed class frmUserAccountDetail : BaseDetailDialogForm, IUserAccountDetailView
    {
        private readonly UserAccountDetailPresenter _presenter;
        private readonly UserDetailDto _user;

        private DetailInfoItemControl _itemAccountCode;
        private DetailInfoItemControl _itemRole;
        private DetailInfoItemControl _itemEmail;
        private DetailInfoItemControl _itemPhone;
        private DetailInfoItemControl _itemCreatedAt;
        private DetailInfoItemControl _itemAccountStatus;
        private DetailInfoItemControl _itemLockStatus;
        private DetailInfoItemControl _itemFailedLogin;

        public frmUserAccountDetail(UserDetailDto user)
            : this(user, null, new UserAccountDetailViewModelBuilder())
        {
        }

        public frmUserAccountDetail(UserDetailDto user, IWin32Window owner)
            : this(user, owner, new UserAccountDetailViewModelBuilder())
        {
        }

        /// <summary>Constructor cho phép tiêm 1 builder khác (ví dụ khi test, hoặc khi muốn
        /// đổi quy tắc trình bày mà không sửa form này).</summary>
        public frmUserAccountDetail(UserDetailDto user, IWin32Window owner, IUserAccountDetailViewModelBuilder viewModelBuilder)
            : base(owner)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (viewModelBuilder == null) throw new ArgumentNullException(nameof(viewModelBuilder));

            _user = user;
            BuildContent();

            _presenter = new UserAccountDetailPresenter(this, viewModelBuilder);
            _presenter.Load(user);
        }

        /// <summary>Cách gọi nhanh, gọn cho nơi khác trong ứng dụng (ví dụ khi bấm "Xem" trên
        /// lưới quản lý tài khoản): <c>frmUserAccountDetail.Show(this, user);</c></summary>
        public static DialogResult Show(IWin32Window owner, UserDetailDto user)
        {
            using (var dialog = new frmUserAccountDetail(user, owner))
            {
                return dialog.ShowDialog(owner);
            }
        }

        #region Dựng giao diện (chỉ chạy 1 lần lúc khởi tạo)
        private void BuildContent()
        {
            HeaderTitle = "Chi tiết tài khoản " + GetAccountTypeLabel(_user);
            SetHeaderIcon(IconChar.UsersCog, AppColors.Accent);

            var accountTab = AddTab("account", "Tài khoản");
            BuildAccountTab(accountTab);

            var securityTab = AddTab("security", "Bảo mật");
            BuildSecurityTab(securityTab);
        }

        private static string GetAccountTypeLabel(UserDetailDto user)
        {
            return string.Equals(user.RoleCode, RoleCodes.Customer, StringComparison.OrdinalIgnoreCase)
                ? "khách hàng"
                : "nhân viên";
        }

        private void BuildAccountTab(Panel host)
        {
            var grid = CreateGrid(3);
            host.Controls.Add(grid);

            _itemAccountCode = CreateInfoItem(IconChar.IdCard, AppColors.Accent, AppColors.AccentBgSoft, "Mã tài khoản");
            _itemRole = CreateInfoItem(IconChar.Briefcase, AppColors.Accent, AppColors.AccentBgSoft, "Vai trò");
            _itemEmail = CreateInfoItem(IconChar.Envelope, AppColors.Accent, AppColors.AccentBgSoft, "Email");
            _itemPhone = CreateInfoItem(IconChar.Phone, AppColors.Accent, AppColors.AccentBgSoft, "Số điện thoại");
            _itemCreatedAt = CreateInfoItem(IconChar.Calendar, AppColors.Accent, AppColors.AccentBgSoft, "Ngày tạo tài khoản");
            _itemAccountStatus = CreateInfoItem(IconChar.CircleQuestion, AppColors.TextMuted, AppColors.BgLighter, "Trạng thái tài khoản");

            grid.Controls.Add(_itemAccountCode, 0, 0);
            grid.Controls.Add(_itemEmail, 1, 0);
            grid.Controls.Add(_itemRole, 0, 1);
            grid.Controls.Add(_itemPhone, 1, 1);
            grid.Controls.Add(_itemCreatedAt, 0, 2);
            grid.Controls.Add(_itemAccountStatus, 1, 2);
        }

        private void BuildSecurityTab(Panel host)
        {
            var grid = CreateGrid(1);
            host.Controls.Add(grid);

            _itemLockStatus = CreateInfoItem(IconChar.LockOpen, AppColors.Success, AppColors.SuccessBg, "Trạng thái khóa");
            _itemFailedLogin = CreateInfoItem(IconChar.TriangleExclamation, AppColors.Warning, AppColors.WarningBg, "Số lần đăng nhập sai");

            grid.Controls.Add(_itemLockStatus, 0, 0);
            grid.Controls.Add(_itemFailedLogin, 1, 0);
        }

        private static TableLayoutPanel CreateGrid(int rowCount)
        {
            var grid = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 2,
                RowCount = rowCount,
                BackColor = Color.Transparent
            };
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            for (int i = 0; i < rowCount; i++)
                grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 108f));
            return grid;
        }

        private static DetailInfoItemControl CreateInfoItem(IconChar icon, Color iconColor, Color iconBackground, string label)
        {
            return new DetailInfoItemControl
            {
                Icon = icon,
                IconColor = iconColor,
                IconBackground = iconBackground,
                LabelText = label,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 16, 16)
            };
        }
        #endregion

        #region IUserAccountDetailView
        // Sự kiện CloseRequested đã có sẵn từ BaseDetailDialogForm (được nút X ở header và
        // nút "Đóng" ở footer bắn ra) - thỏa mãn interface bằng kế thừa, không cần khai lại.

        public void Render(UserAccountDetailViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

            Avatar.Initial = viewModel.AvatarInitial;
            Avatar.AvatarColor = viewModel.AvatarColor;
            Avatar.NameText = viewModel.FullName;
            Avatar.SubtitleText = viewModel.HandleText;

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
        }

        public void CloseView() => Close();
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
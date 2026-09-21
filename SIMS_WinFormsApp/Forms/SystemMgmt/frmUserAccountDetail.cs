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
        // MỚI: kích thước riêng cho popup này (lớn hơn DefaultDialogSize/MinimumDialogSize mặc
        // định của BaseDetailDialogForm). Lý do: tab "Tài khoản" của tài khoản NHÂN VIÊN có tới
        // 4 hàng (Mã tài khoản/Vai trò/Ngày tạo tài khoản + Email/Số điện thoại/Trạng thái tài
        // khoản + Giới tính/Lương), mỗi hàng cao 108px (xem CreateGrid()) => tổng 432px nội dung,
        // trong khi chiều cao popup mặc định (600px) chỉ còn lại khoảng 387px cho phần nội dung
        // sau khi trừ header/footer/dải tab/khoảng đệm - khiến panel tab tự cuộn (AutoScroll)
        // và hàng "Giới tính"/"Lương" bị che khuất, phải kéo thanh cuộn dọc mới xem hết (đúng lỗi
        // trong ảnh chụp popup "Chi tiết tài khoản nhân viên"). Tăng chiều cao đủ để 4 hàng đó
        // hiện trọn vẹn không cần cuộn, đồng thời tăng thêm chiều rộng để các giá trị dài (ví dụ
        // Email) có nhiều chỗ hơn, đỡ bị AutoEllipsis cắt bớt thành "...".
        //
        // Chỉnh Size/MinimumSize ngay tại lớp con (sau khi lớp cha BaseDetailDialogForm đã khởi
        // tạo xong) thay vì sửa DefaultDialogSize/MinimumDialogSize dùng chung trong
        // BaseDetailDialogForm, để không ảnh hưởng tới các popup chi tiết khác có thể kế thừa
        // lớp đó sau này - tuân thủ nguyên tắc Open/Closed (mở rộng hành vi riêng của popup này,
        // không sửa lớp cha dùng chung).
        private static readonly Size DetailDialogSize = new Size(1150, 660);

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

        // MỚI: chỉ được tạo (khác null) khi tài khoản là nhân viên - xem BuildAccountTab().
        private DetailInfoItemControl _itemGender;
        private DetailInfoItemControl _itemSalary;

        public frmUserAccountDetail(UserDetailDto user)
            : this(user, null, null, new UserAccountDetailViewModelBuilder())
        {
        }

        public frmUserAccountDetail(UserDetailDto user, IWin32Window owner)
            : this(user, owner, null, new UserAccountDetailViewModelBuilder())
        {
        }

        /// <summary>Kèm <paramref name="userManagementService"/> để popup có thể tự nạp thêm hồ
        /// sơ nhân viên (Giới tính/Lương) qua GetEmployeeProfile() - truyền null (mặc định ở các
        /// constructor khác) thì 2 ô này chỉ hiện "Chưa cập nhật".</summary>
        public frmUserAccountDetail(UserDetailDto user, IWin32Window owner, IUserManagementService userManagementService)
            : this(user, owner, userManagementService, new UserAccountDetailViewModelBuilder())
        {
        }

        /// <summary>Constructor cho phép tiêm 1 builder khác (ví dụ khi test, hoặc khi muốn
        /// đổi quy tắc trình bày mà không sửa form này).</summary>
        public frmUserAccountDetail(
            UserDetailDto user,
            IWin32Window owner,
            IUserManagementService userManagementService,
            IUserAccountDetailViewModelBuilder viewModelBuilder)
            : base(owner)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (viewModelBuilder == null) throw new ArgumentNullException(nameof(viewModelBuilder));

            _user = user;
            BuildContent();

            _presenter = new UserAccountDetailPresenter(this, viewModelBuilder, userManagementService);
            _presenter.Load(user);
        }

        /// <summary>Cách gọi nhanh, gọn cho nơi khác trong ứng dụng (ví dụ khi bấm "Xem" trên
        /// lưới quản lý tài khoản): <c>frmUserAccountDetail.Show(this, user, service);</c>.
        /// <paramref name="userManagementService"/> có thể để trống nếu nơi gọi không có sẵn
        /// service (khi đó Giới tính/Lương hiện "Chưa cập nhật").</summary>
        public static DialogResult Show(IWin32Window owner, UserDetailDto user, IUserManagementService userManagementService = null)
        {
            using (var dialog = new frmUserAccountDetail(user, owner, userManagementService))
            {
                return dialog.ShowDialog(owner);
            }
        }

        #region Dựng giao diện (chỉ chạy 1 lần lúc khởi tạo)
        private void BuildContent()
        {
            // Ghi đè kích thước mặc định của BaseDetailDialogForm - xem giải thích ở
            // DetailDialogSize phía trên (loại bỏ thanh cuộn dọc + rộng hơn cho nội dung dài).
            Size = DetailDialogSize;
            MinimumSize = DetailDialogSize;

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
            // Giới tính/Lương chỉ áp dụng cho hồ sơ nhân viên (Employee), khách hàng không có
            // 2 thuộc tính này - dùng lại đúng điều kiện đang xét ở GetAccountTypeLabel().
            bool isEmployeeAccount = !string.Equals(_user.RoleCode, RoleCodes.Customer, StringComparison.OrdinalIgnoreCase);

            var grid = CreateGrid(isEmployeeAccount ? 4 : 3);
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

            if (isEmployeeAccount)
            {
                _itemGender = CreateInfoItem(IconChar.VenusMars, AppColors.Accent, AppColors.AccentBgSoft, "Giới tính");
                _itemSalary = CreateInfoItem(IconChar.MoneyBillWave, AppColors.Accent, AppColors.AccentBgSoft, "Lương");

                grid.Controls.Add(_itemGender, 0, 3);
                grid.Controls.Add(_itemSalary, 1, 3);
            }
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

            if (_itemGender != null) _itemGender.ValueText = viewModel.GenderText;
            if (_itemSalary != null) _itemSalary.ValueText = viewModel.SalaryText;
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
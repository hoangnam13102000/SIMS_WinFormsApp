using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Forms.Dashboard;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Models.Enums;
using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Controls.Toast;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.Forms.Profile
{
    public partial class ucMyProfile : UserControl, IEditUserAccountView, IChangePasswordView
    {
        private readonly IUserManagementService _userManagementService;
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        private readonly EditUserAccountPresenter _profilePresenter;
        private readonly ChangePasswordPresenter _passwordPresenter;

        // ----- Khối tóm tắt bên trái -----
        private AvatarUploadPanel _avatarPanel;
        private Label _lblFullName;
        private Label _lblRoleBadge;
        private Label _lblSidebarEmail;
        private Label _lblSidebarPhone;
        private Label _lblJoinedAt;

        // ----- Khối "Thông tin cá nhân" (IEditUserAccountView) -----
        private LabeledIconField _fieldFullName;
        private LabeledIconField _fieldPhone;
        private LabeledIconField _fieldEmail;
        private Label _lblProfileError;
        private PrimaryButton _btnSaveProfile;

        // Trang cá nhân không hiển thị các trường hồ sơ nhân viên, nhưng Presenter dùng
        // chung IEditUserAccountView nên vẫn cần giữ các giá trị này trong suốt vòng đời view.
        private DateTime? _dateOfBirth;
        private Gender? _selectedGender;
        private DateTime _hireDate = DateTime.Today;
        private string _salaryText = string.Empty;

        // ----- Khối "Đổi mật khẩu" (IChangePasswordView) -----
        private RoundedPasswordTextBox _txtCurrentPassword;
        private RoundedPasswordTextBox _txtNewPassword;
        private RoundedPasswordTextBox _txtConfirmPassword;
        private Label _lblPasswordError;
        private PrimaryButton _btnChangePassword;

        public event EventHandler ProfileUpdated;

        public ucMyProfile(
            IUserManagementService userManagementService = null,
            IAuthService authService = null,
            IUserRepository userRepository = null)
        {
            InitializeComponent();

            AutoScaleMode = AutoScaleMode.None;
            Font = new Font("Segoe UI", 9f);
            DoubleBuffered = true;
            Dock = DockStyle.Fill;
            BackColor = AppColors.PageBg;
            Padding = new Padding(20, 16, 20, 20);
            Margin = new Padding(0);

            _userManagementService = userManagementService ?? AppComposition.CreateUserManagementService();
            _authService = authService ?? AppComposition.CreateAuthService();
            _userRepository = userRepository ?? AppComposition.CreateUserRepository();

            BuildUI();
            LoadCurrentUser();

            // Ghép Presenter có sẵn - KHÔNG viết lại validate/nghiệp vụ nào mới. Giống hệt cách
            // frmEditUserAccount/frmChangePassword tự tạo Presenter của mình trong constructor.
            _profilePresenter = new EditUserAccountPresenter(this, _userManagementService, CurrentUserId);
            _passwordPresenter = new ChangePasswordPresenter(this, _authService);
            Disposed += (s, e) => _profilePresenter.Dispose();

            _btnSaveProfile.Click += (s, e) => SaveRequested?.Invoke(this, EventArgs.Empty);
            _btnChangePassword.Click += async (s, e) => await _passwordPresenter.ChangeAsync();
        }

        // Nạp dữ liệu người dùng hiện tại từ UserSession - cùng cách đã dùng ở
        // frmMain/frmChangePassword (UserSession.Instance.CurrentUser), không đụng tới UserSession.cs.
        #region Nạp dữ liệu người dùng hiện tại
        private void LoadCurrentUser()
        {
            var user = UserSession.Instance.CurrentUser;
            if (user == null) return;

            _avatarPanel.Initial = GetInitial(user.FullName);
            _avatarPanel.ClearImage();
            if (!string.IsNullOrWhiteSpace(user.AvatarUrl) && File.Exists(user.AvatarUrl))
            {
                try
                {
                    using (var source = Image.FromFile(user.AvatarUrl))
                        _avatarPanel.SetImage(new Bitmap(source), user.AvatarUrl);
                }
                catch (Exception)
                {
                    // Hiển thị chữ cái đại diện nếu file ảnh không còn hợp lệ.
                }
            }
            _lblFullName.Text = user.FullName;
            _lblRoleBadge.Text = string.IsNullOrWhiteSpace(user.RoleName) ? user.RoleCode : user.RoleName;
            _lblSidebarEmail.Text = user.Email;
            _lblSidebarPhone.Text = string.IsNullOrWhiteSpace(user.Phone) ? "Chưa cập nhật" : user.Phone;
            _lblJoinedAt.Text = "Tham gia từ " + user.CreatedAt.ToString("d/M/yyyy");

            _fieldFullName.Value = user.FullName;
            _fieldPhone.Value = user.Phone;

            // Email đăng nhập không cho sửa tại trang này (đúng như hình mẫu) - vẫn giữ giá trị
            // trong field để EditUserAccountPresenter gửi lại email KHÔNG ĐỔI khi lưu, tránh bị
            // validate "thiếu email".
            _fieldEmail.Value = user.Email;
            _fieldEmail.Enabled = false;
        }

        private static string GetInitial(string fullName) =>
            string.IsNullOrWhiteSpace(fullName) ? "?" : fullName.Trim().Substring(0, 1).ToUpperInvariant();
        #endregion

        #region IEditUserAccountView - dùng bởi EditUserAccountPresenter (khối "Thông tin cá nhân")
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
            // Các trường hồ sơ nhân viên không có trên trang cá nhân; dữ liệu vẫn được
            // giữ qua các property ở trên để Presenter không làm mất dữ liệu khi lưu.
        }

        public event EventHandler SaveRequested;

        // Trang cá nhân là một tab thường trực, không có thao tác "Hủy" như popup.
        event EventHandler IEditUserAccountView.CancelRequested
        {
            add { }
            remove { }
        }

        void IEditUserAccountView.ShowError(string message) => ShowProfileError(message);

        void IEditUserAccountView.ShowSuccess(string message) => AppToast.Success(this, message);

        public void SetSaving(bool isSaving)
        {
            _fieldFullName.Enabled = !isSaving;
            _fieldPhone.Enabled = !isSaving;
            _btnSaveProfile.Enabled = !isSaving;
            _btnSaveProfile.Text = isSaving ? "Đang lưu..." : "Lưu thay đổi";
        }

        void IEditUserAccountView.CloseOnSuccess() => OnProfileSavedSuccessfully();

        private void ShowProfileError(string message)
        {
            _lblProfileError.Text = message ?? string.Empty;
            _lblProfileError.Visible = !string.IsNullOrEmpty(message);
            ReflowCard((Panel)_lblProfileError.Parent);
        }

        private void OnProfileSavedSuccessfully()
        {
            // Đồng bộ lại UserSession từ DB (đề phòng có thay đổi khác song song) rồi báo cho
            // MainPresenter làm mới tên/avatar trên Header + Sidebar - chỉ gọi lại các API công
            // khai đã có sẵn (IUserRepository.FindById, UserSession.SignIn), không thêm logic
            // nghiệp vụ mới.
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

        #region IChangePasswordView - dùng bởi ChangePasswordPresenter (khối "Đổi mật khẩu")
        public string CurrentPassword => _txtCurrentPassword.Text;
        public string NewPassword => _txtNewPassword.Text;
        public string Confirmation => _txtConfirmPassword.Text;

        public int CurrentUserId => UserSession.Instance.CurrentUser?.UserId ?? 0;

        public void ClearError() => ShowPasswordError(string.Empty);

        void IChangePasswordView.ShowError(string message) => ShowPasswordError(message);

        public void SetLoading(bool isBusy)
        {
            _txtCurrentPassword.Enabled = !isBusy;
            _txtNewPassword.Enabled = !isBusy;
            _txtConfirmPassword.Enabled = !isBusy;
            _btnChangePassword.Enabled = !isBusy;
            _btnChangePassword.Text = isBusy ? "Đang xử lý..." : "Đổi mật khẩu";
        }

        void IChangePasswordView.ShowSuccess(string message) => AppToast.Success(this, message);

        void IChangePasswordView.CloseOnSuccess() => OnPasswordChangedSuccessfully();

        private void ShowPasswordError(string message)
        {
            _lblPasswordError.Text = message ?? string.Empty;
            _lblPasswordError.Visible = !string.IsNullOrEmpty(message);
            ReflowCard((Panel)_lblPasswordError.Parent);
        }

        private void OnPasswordChangedSuccessfully()
        {
            _txtCurrentPassword.Text = string.Empty;
            _txtNewPassword.Text = string.Empty;
            _txtConfirmPassword.Text = string.Empty;
            AppToast.Success(this, "Đổi mật khẩu thành công.");
        }
        #endregion

        #region Dựng giao diện (chỉ chạy 1 lần lúc khởi tạo)
        private void BuildUI()
        {
            SuspendLayout();

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = AppColors.PageBg,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            var lblPageTitle = new Label
            {
                AutoSize = true,
                Text = "Trang cá nhân",
                Font = AppFonts.Title,
                ForeColor = AppColors.TextTitle,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 0, 0, 16)
            };
            root.Controls.Add(lblPageTitle, 0, 0);

            var split = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1,
                BackColor = AppColors.PageBg,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            // MỚI: tách "Thông tin cá nhân" và "Đổi mật khẩu" thành 2 cột riêng (thay vì xếp
            // chồng dọc trong cùng 1 cột như trước) - tận dụng chiều ngang còn dư của trang thay
            // vì kéo dài xuống dưới, để toàn bộ nội dung vừa đủ trong màn hình mà không cần
            // thanh cuộn dọc như trước (khi 2 card cộng dồn chiều cao vượt quá vùng hiển thị).
            split.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 320f));
            split.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            split.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            split.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

            var leftHost = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = AppColors.PageBg,
                Padding = new Padding(0, 0, 12, 0)
            };
            leftHost.Controls.Add(BuildLeftCard());
            split.Controls.Add(leftHost, 0, 0);

            // MỚI: mỗi card giờ nằm trong 1 host riêng (Fill theo đúng cột của nó) thay vì cùng
            // xếp Dock=Top chồng lên nhau trong 1 host duy nhất như trước - AutoScroll vẫn giữ
            // lại trên từng host để làm lưới an toàn (chỉ hiện thanh cuộn nếu cửa sổ bị thu nhỏ
            // bất thường), không phải điều kiện bình thường sau khi bố cục đã gọn lại.
            var profileHost = new BufferedPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = AppColors.PageBg,
                Padding = new Padding(0, 0, 8, 0)
            };
            profileHost.Controls.Add(BuildProfileCard());
            split.Controls.Add(profileHost, 1, 0);

            var passwordHost = new BufferedPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = AppColors.PageBg,
                Padding = new Padding(8, 0, 0, 0)
            };
            passwordHost.Controls.Add(BuildPasswordCard());
            split.Controls.Add(passwordHost, 2, 0);

            root.Controls.Add(split, 0, 1);

            Controls.Add(root);
            ResumeLayout(true);
        }

        private Panel BuildLeftCard()
        {
            var card = new Panel
            {
                Width = 320,
                Dock = DockStyle.Top,
                BackColor = Color.Transparent,
                Padding = new Padding(24, 28, 24, 24)
            };
            card.Paint += (s, e) => PaintCardBackground(card, e);

            _avatarPanel = new AvatarUploadPanel { HintText = "Tuỳ chọn · tối đa 5MB" };

            _lblFullName = new Label
            {
                AutoSize = false,
                Height = 32,
                TextAlign = ContentAlignment.TopCenter,
                Font = AppFonts.Subtitle,
                ForeColor = AppColors.TextTitle,
                BackColor = Color.Transparent
            };

            // MỚI: badge vai trò trước đây là 1 Label cao cố định 24px, TopCenter - dấu tiếng
            // Việt (ví dụ "Quản trị viên" có dấu hỏi/nặng) cần nhiều hơn 24px theo chiều dọc cho
            // 1 dòng chữ đậm, nên bị cắt ngang qua giữa chữ như trong ảnh chụp thực tế. Đổi
            // sang dạng "pill" bo tròn hoàn toàn (nền AccentBgSoft, chữ Accent) tự đo đúng kích
            // thước cần thiết theo nội dung thay vì đặt cứng - vừa hết cảnh bị cắt, vừa đẹp hơn
            // (đây cũng là màu/kiểu badge đã dùng nhất quán ở nhiều nơi khác trong dự án, xem
            // CreateInfoItem() trong frmUserAccountDetail.cs).
            _lblRoleBadge = new Label
            {
                AutoSize = false,
                Font = AppFonts.SmallBold,
                ForeColor = AppColors.Accent,
                BackColor = Color.Transparent,
                Margin = Padding.Empty,
                Padding = Padding.Empty,
                TextAlign = ContentAlignment.MiddleCenter
            };

            const int roleBadgePadH = 16;
            const int roleBadgePadV = 6;
            var roleBadgeHost = new Panel { AutoSize = false, BackColor = Color.Transparent };
            roleBadgeHost.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, roleBadgeHost.Width - 1, roleBadgeHost.Height - 1);
                using (var path = AppRadius.GetRoundedPath(rect, roleBadgeHost.Height / 2))
                using (var brush = new SolidBrush(AppColors.AccentBgSoft))
                    g.FillPath(brush, path);
            };
            roleBadgeHost.Controls.Add(_lblRoleBadge);

            var separator1 = CreateSeparator();
            var rowEmail = CreateContactRow(IconChar.Envelope, "Email", out _lblSidebarEmail);
            var rowPhone = CreateContactRow(IconChar.Phone, "Số điện thoại", out _lblSidebarPhone);
            var separator2 = CreateSeparator();

            _lblJoinedAt = new Label
            {
                AutoSize = true,
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent
            };

            var separator3 = CreateSeparator();

            void Layout()
            {
                int contentWidth = Math.Max(10, card.Width - card.Padding.Horizontal);
                int y = card.Padding.Top;

                _avatarPanel.Width = contentWidth;
                _avatarPanel.Location = new Point(card.Padding.Left, y);
                y = _avatarPanel.Bottom + 14;

                _lblFullName.Width = contentWidth;
                int fullNameHeight = TextRenderer.MeasureText(
                    _lblFullName.Text ?? string.Empty,
                    _lblFullName.Font,
                    new Size(contentWidth, int.MaxValue),
                    TextFormatFlags.WordBreak | TextFormatFlags.NoPadding).Height;
                _lblFullName.Height = Math.Max(32, fullNameHeight + 4);
                _lblFullName.Location = new Point(card.Padding.Left, y);
                y = _lblFullName.Bottom + 2;

                // "Pill" badge vai trò: đo đúng kích thước chữ (không ngắt dòng - badge chỉ có
                // 1 dòng ngắn) rồi cộng thêm khoảng đệm đều 2 bên, canh giữa theo chiều ngang.
                Size roleTextSize = TextRenderer.MeasureText(
                    _lblRoleBadge.Text ?? string.Empty,
                    _lblRoleBadge.Font,
                    new Size(int.MaxValue, int.MaxValue),
                    TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
                int badgeWidth = roleTextSize.Width + roleBadgePadH * 2;
                int badgeHeight = roleTextSize.Height + roleBadgePadV * 2;
                roleBadgeHost.Size = new Size(badgeWidth, badgeHeight);
                roleBadgeHost.Location = new Point(card.Padding.Left + Math.Max(0, (contentWidth - badgeWidth) / 2), y);
                _lblRoleBadge.Size = roleBadgeHost.Size;
                _lblRoleBadge.Location = Point.Empty;
                y = roleBadgeHost.Bottom + 16;

                y = PlaceSeparator(separator1, card.Padding.Left, contentWidth, y) + 16;

                rowEmail.Width = contentWidth;
                rowEmail.Location = new Point(card.Padding.Left, y);
                y = rowEmail.Bottom + 12;

                rowPhone.Width = contentWidth;
                rowPhone.Location = new Point(card.Padding.Left, y);
                y = rowPhone.Bottom + 16;

                y = PlaceSeparator(separator2, card.Padding.Left, contentWidth, y) + 16;

                _lblJoinedAt.Location = new Point(card.Padding.Left, y);
                y = _lblJoinedAt.Bottom + 16;

                // MỚI: bỏ khối "Mã vạch thành viên" (không dùng đến ở trang cá nhân nhân
                // viên/quản trị viên - dự án cũng chưa có trường dữ liệu này, xem ghi chú cũ đã
                // xoá). separator3 giữ lại làm đường kẻ kết thúc card cho cân đối, không kèm nội
                // dung phía sau.
                y = PlaceSeparator(separator3, card.Padding.Left, contentWidth, y);

                int newHeight = y + card.Padding.Bottom;
                if (card.Height != newHeight) card.Height = newHeight;
            }

            // Text vai trò được LoadCurrentUser() gán SAU khi BuildLeftCard() đã dựng xong -
            // phải tính lại kích thước "pill" mỗi khi Text đổi (kể cả sau lần lưu hồ sơ khiến
            // LoadCurrentUser() chạy lại), không chỉ lúc Resize.
            _lblRoleBadge.TextChanged += (s, e) => Layout();

            card.Controls.AddRange(new Control[]
            {
                _avatarPanel, _lblFullName, roleBadgeHost, separator1, rowEmail, rowPhone,
                separator2, _lblJoinedAt, separator3
            });

            card.Resize += (s, e) => Layout();
            Layout();

            return card;
        }

        private Panel BuildProfileCard()
        {
            var card = new Panel
            {
                Dock = DockStyle.Top,
                BackColor = Color.Transparent,
                Padding = new Padding(24)
            };
            card.Paint += (s, e) => PaintCardBackground(card, e);

            var header = new FieldGroupHeader { Icon = IconChar.IdCard, HeaderText = "Thông tin cá nhân", Dock = DockStyle.Top };

            _fieldFullName = new LabeledIconField
            {
                Dock = DockStyle.Top,
                LabelText = "Họ và tên",
                IsRequired = true,
                Icon = IconChar.User,
                PlaceholderText = "Nhập họ và tên",
                MaxLength = 100
            };

            _fieldPhone = new LabeledIconField
            {
                Dock = DockStyle.Top,
                LabelText = "Số điện thoại",
                Icon = IconChar.Phone,
                PlaceholderText = "Nhập số điện thoại",
                HintText = "VD: 09xxxxxxxx (tùy chọn).",
                MaxLength = 15
            };

            _fieldEmail = new LabeledIconField
            {
                Dock = DockStyle.Top,
                LabelText = "Email",
                Icon = IconChar.Envelope,
                HintText = "Email đăng nhập không thể thay đổi tại đây.",
                MaxLength = 150
            };

            _lblProfileError = new Label
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                Font = AppFonts.Small,
                ForeColor = AppColors.Error,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 0, 0, 10),
                Visible = false
            };

            var footer = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.Transparent };
            _btnSaveProfile = new PrimaryButton { Text = "Lưu thay đổi", Width = 160, Height = 42 };
            footer.Controls.Add(_btnSaveProfile);
            void PlaceSaveButton() => _btnSaveProfile.Location = new Point(footer.Width - _btnSaveProfile.Width, 4);
            footer.Resize += (s, e) => PlaceSaveButton();
            PlaceSaveButton();

            // add ngược thứ tự hiển thị (xem giải thích ở BuildUI()).
            card.Controls.Add(footer);
            card.Controls.Add(_lblProfileError);
            card.Controls.Add(_fieldEmail);
            card.Controls.Add(_fieldPhone);
            card.Controls.Add(_fieldFullName);
            card.Controls.Add(header);

            card.Resize += (s, e) => ReflowCard(card);
            ReflowCard(card);

            return card;
        }

        private Panel BuildPasswordCard()
        {
            var card = new Panel
            {
                Dock = DockStyle.Top,
                BackColor = Color.Transparent,
                Padding = new Padding(24)
            };
            card.Paint += (s, e) => PaintCardBackground(card, e);

            var header = new FieldGroupHeader { Icon = IconChar.Lock, HeaderText = "Đổi mật khẩu", Dock = DockStyle.Top };

            var fieldCurrent = CreatePasswordField("Mật khẩu hiện tại", out _txtCurrentPassword);
            var fieldNew = CreatePasswordField("Mật khẩu mới", out _txtNewPassword);
            var fieldConfirm = CreatePasswordField("Xác nhận mật khẩu mới", out _txtConfirmPassword);

            _lblPasswordError = new Label
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                Font = AppFonts.Small,
                ForeColor = AppColors.Error,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 0, 0, 10),
                Visible = false
            };

            var footer = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.Transparent };
            _btnChangePassword = new PrimaryButton { Text = "Đổi mật khẩu", Width = 160, Height = 42 };
            footer.Controls.Add(_btnChangePassword);
            void PlaceChangeButton() => _btnChangePassword.Location = new Point(footer.Width - _btnChangePassword.Width, 4);
            footer.Resize += (s, e) => PlaceChangeButton();
            PlaceChangeButton();

            card.Controls.Add(footer);
            card.Controls.Add(_lblPasswordError);
            card.Controls.Add(fieldConfirm);
            card.Controls.Add(fieldNew);
            card.Controls.Add(fieldCurrent);
            card.Controls.Add(header);

            card.Resize += (s, e) => ReflowCard(card);
            ReflowCard(card);

            return card;
        }

        /// <summary>Nhãn + <see cref="RoundedPasswordTextBox"/> xếp dọc, tự co giãn theo chiều
        /// rộng cha khi Dock=Top - cùng nguyên lý với <see cref="LabeledIconField"/> nhưng dùng
        /// ô mật khẩu (có nút ẩn/hiện) thay vì ô có icon, nên dựng riêng thay vì sửa
        /// LabeledIconField hiện có.</summary>
        private static Panel CreatePasswordField(string labelText, out RoundedPasswordTextBox textBox)
        {
            const int SpacingAfterLabel = 6;
            const int SpacingAfterBlock = 18;

            var wrap = new Panel { Dock = DockStyle.Top, BackColor = Color.Transparent };

            var lbl = new Label
            {
                AutoSize = true,
                Text = labelText,
                Font = AppFonts.SmallBold,
                ForeColor = AppColors.TextTitle,
                BackColor = Color.Transparent,
                Location = new Point(0, 0)
            };

            var box = new RoundedPasswordTextBox
            {
                Height = 46,
                ShowTooltip = "Hiện mật khẩu",
                HideTooltip = "Ẩn mật khẩu"
            };
            textBox = box;

            void Layout()
            {
                box.Width = Math.Max(10, wrap.Width);
                box.Location = new Point(0, lbl.Bottom + SpacingAfterLabel);
                int preferredHeight = box.Bottom + SpacingAfterBlock;
                if (wrap.Height != preferredHeight) wrap.Height = preferredHeight;
            }

            wrap.Controls.Add(box);
            wrap.Controls.Add(lbl);
            wrap.Resize += (s, e) => Layout();
            Layout();

            return wrap;
        }

        private Control CreateContactRow(IconChar icon, string caption, out Label valueLabel)
        {
            var host = new Panel { Height = 48, BackColor = Color.Transparent };

            var iconBox = new IconPictureBox
            {
                IconChar = icon,
                IconColor = AppColors.TextMuted,
                IconSize = 16,
                Size = new Size(16, 16),
                Location = new Point(0, 9),
                BackColor = Color.Transparent
            };

            var lblCaption = new Label
            {
                AutoSize = true,
                Text = caption,
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent,
                Location = new Point(26, 0)
            };

            var value = new Label
            {
                AutoSize = false,
                Height = 24,
                Location = new Point(26, 20),
                Font = AppFonts.BodyBold,
                ForeColor = AppColors.TextTitle,
                BackColor = Color.Transparent,
                AutoEllipsis = false,
                TextAlign = ContentAlignment.TopLeft
            };
            valueLabel = value;

            Action layoutValue = () =>
            {
                int width = Math.Max(10, host.Width - 26);
                lblCaption.Width = width;
                value.Width = width;

                int valueHeight = TextRenderer.MeasureText(
                    value.Text ?? string.Empty,
                    value.Font,
                    new Size(width, int.MaxValue),
                    TextFormatFlags.WordBreak | TextFormatFlags.NoPadding).Height;
                value.Height = Math.Max(24, valueHeight + 4);

                int newHeight = Math.Max(48, value.Bottom + 4);
                if (host.Height != newHeight) host.Height = newHeight;
            };

            host.Controls.Add(iconBox);
            host.Controls.Add(lblCaption);
            host.Controls.Add(value);
            host.Resize += (s, e) =>
            {
                layoutValue();
            };
            value.TextChanged += (s, e) =>
            {
                layoutValue();
                host.Parent?.PerformLayout();
            };
            layoutValue();

            return host;
        }

        private static Panel CreateSeparator() => new Panel { BackColor = AppColors.Border, Height = 1 };

        private static int PlaceSeparator(Panel separator, int x, int width, int y)
        {
            separator.Location = new Point(x, y);
            separator.Width = width;
            return separator.Bottom;
        }

        /// <summary>Nền bo góc dùng chung cho các "card" của trang - cùng kỹ thuật vẽ đã dùng ở
        /// ucDashboard.CreateSectionCard/frmEditUserAccount.CreateReadOnlyInfoBox, viết lại tại
        /// đây vì dự án hiện chưa tách control Card dùng chung.</summary>
        private static void PaintCardBackground(Control card, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, card.Width - 1, card.Height - 1);
            using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Large))
            using (var brush = new SolidBrush(AppColors.White))
            using (var pen = new Pen(AppColors.Border, 1f))
            {
                g.FillPath(brush, path);
                g.DrawPath(pen, path);
            }
        }

        /// <summary>Co giãn chiều cao 1 "card" (Dock=Top, chứa các control con cũng Dock=Top)
        /// theo đúng nội dung thật - cùng kỹ thuật với ThreeColumnFieldsPanel.LayoutColumn.</summary>
        private static void ReflowCard(Panel card)
        {
            if (card == null || card.Width <= card.Padding.Horizontal) return;

            card.PerformLayout();
            int bottom = 0;
            foreach (Control child in card.Controls)
            {
                if (child.Visible) bottom = Math.Max(bottom, child.Bottom);
            }

            int newHeight = bottom + card.Padding.Bottom;
            if (card.Height != newHeight) card.Height = newHeight;
        }
        #endregion

    }
}
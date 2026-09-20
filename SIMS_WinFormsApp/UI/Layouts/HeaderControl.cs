using System;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.MVP.ViewModels;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.UI.Layouts
{
    /// <summary>
    /// Header của shell chính (View thụ động): chỉ hiển thị và phát sự kiện,
    /// mọi xử lý (mở hồ sơ, đăng xuất...) do Presenter đảm nhiệm qua MainLayoutControl/IMainView.
    /// </summary>
    public class HeaderControl : UserControl, IHeaderView
    {
        private const int PadX = 24;
        private const int LogoSize = 52;
        private const int LogoTextGap = 14;
        private const int ClusterGap = 16;
        private const int DividerHeight = 40;
        private const int NarrowBreakpoint = 900;
        private const int MediumBreakpoint = 1100;

        private readonly PictureBox _logo;
        private readonly Label _titleLabel;
        private readonly Label _subtitleLabel;
        private readonly NotificationBellButton _bell;
        private readonly HeaderAccountButton _account;

        private readonly Font _titleFont = new Font("Segoe UI Semibold", 20f, FontStyle.Bold);
        private readonly Font _subtitleFont = new Font("Segoe UI", 10.5f);

        private HeaderUserViewModel _user = new HeaderUserViewModel("Admin", "admin@sims.local");
        private int _dividerX;

        private Form _openPopup;
        private Control _openPopupTrigger;

        public event EventHandler ProfileClicked;
        public event EventHandler LogoutClicked;

        public HeaderControl() : this("Cửa hàng điện thoại trực tuyến") { }

        public HeaderControl(string subtitle)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);
            AutoScaleMode = AutoScaleMode.None;
            Font = new Font("Segoe UI", 9f);

            Height = LayoutColors.HeaderHeight;
            MinimumSize = new Size(0, LayoutColors.HeaderHeight);
            BackColor = LayoutColors.HeaderBg;
            Padding = Padding.Empty;

            // ===== Thương hiệu (trái) =====
            _logo = new PictureBox
            {
                Size = new Size(LogoSize, LogoSize),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };
            try { _logo.Image = Properties.Resources.logo_icon; }
            catch { _logo.BackColor = LayoutColors.Accent; }

            _titleLabel = new Label
            {
                AutoSize = false,
                UseMnemonic = false,
                Text = "SIMS",
                Font = _titleFont,
                ForeColor = LayoutColors.HeaderText,
                BackColor = Color.Transparent,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };

            _subtitleLabel = new Label
            {
                AutoSize = false,
                UseMnemonic = false,
                Text = subtitle ?? string.Empty,
                Font = _subtitleFont,
                ForeColor = LayoutColors.HeaderSubtitle,
                BackColor = Color.Transparent,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };

            // ===== Cụm bên phải: chuông + tài khoản =====
            _bell = new NotificationBellButton();
            _bell.Click += (_, __) => ShowNotificationMenu();

            _account = new HeaderAccountButton();
            _account.Bind(_user);
            _account.Click += (_, __) => ShowAccountMenu();

            Controls.Add(_logo);
            Controls.Add(_titleLabel);
            Controls.Add(_subtitleLabel);
            Controls.Add(_bell);
            Controls.Add(_account);

            FitLabel(_titleLabel);
            FitLabel(_subtitleLabel);

            Resize += (_, __) => LayoutHeader();
            LayoutHeader();
        }

        // ================= IHeaderView =================

        public void SetSubtitle(string subtitle)
        {
            _subtitleLabel.Text = subtitle ?? string.Empty;
            FitLabel(_subtitleLabel);
            LayoutHeader();
        }

        public void SetUser(HeaderUserViewModel user)
        {
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _account.Bind(_user);
            LayoutHeader();
        }

        /// <summary>Giữ chữ ký cũ để MainLayoutControl/frmMain không phải sửa.</summary>
        public void SetUser(string displayName, string email, string avatarInitial = null,
            string role = null, string avatarPath = null)
            => SetUser(new HeaderUserViewModel(displayName, email, role, avatarInitial, avatarPath));

        public void SetUnreadCount(int count)
        {
            _bell.Count = count;
        }

        // ================= Layout =================

        private static void FitLabel(Label label)
        {
            Size s = TextRenderer.MeasureText(label.Text ?? string.Empty, label.Font,
                Size.Empty, TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
            label.Size = new Size(s.Width + 6, s.Height + 2);
        }

        private void LayoutHeader()
        {
            int h = Height > 0 ? Height : LayoutColors.HeaderHeight;
            int w = Width > 0 ? Width : 1200;

            bool narrow = w < NarrowBreakpoint;
            bool medium = w < MediumBreakpoint;

            _subtitleLabel.Visible = !narrow;
            _account.ShowEmail = !narrow;
            _account.MaxTextWidth = medium ? 180 : 280;

            // --- Trái: logo + tên + phụ đề ---
            _logo.Location = new Point(PadX, (h - _logo.Height) / 2);
            int textX = _logo.Right + LogoTextGap;

            if (_subtitleLabel.Visible)
            {
                int blockH = _titleLabel.Height + _subtitleLabel.Height - 2;
                int y = Math.Max(0, (h - blockH) / 2);
                _titleLabel.Location = new Point(textX, y);
                _subtitleLabel.Location = new Point(textX, _titleLabel.Bottom - 2);
            }
            else
            {
                _titleLabel.Location = new Point(textX, (h - _titleLabel.Height) / 2);
            }

            // --- Phải: [chuông] | [tài khoản] ---
            _account.Location = new Point(
                Math.Max(0, w - PadX - _account.Width),
                (h - _account.Height) / 2);

            _dividerX = _account.Left - ClusterGap;

            _bell.Location = new Point(
                Math.Max(0, _dividerX - ClusterGap - _bell.Width),
                (h - _bell.Height) / 2);

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (var pen = new Pen(LayoutColors.HeaderBorder))
                e.Graphics.DrawLine(pen, 0, Height - 1, Width, Height - 1);

            int y = (Height - DividerHeight) / 2;
            using (var pen = new Pen(LayoutColors.HeaderDivider))
                e.Graphics.DrawLine(pen, _dividerX, y, _dividerX, y + DividerHeight);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            BeginInvoke(new Action(() =>
            {
                if (IsDisposed) return;
                LayoutHeader();
                BeginInvoke(new Action(() => { if (!IsDisposed) LayoutHeader(); }));
            }));
        }

        // ================= Popup =================

        private bool ToggleIfSameTrigger(Control trigger)
        {
            if (_openPopup != null && !_openPopup.IsDisposed && ReferenceEquals(_openPopupTrigger, trigger))
            {
                _openPopup.Close();
                return true;
            }
            return false;
        }

        private void CloseOpenPopup()
        {
            if (_openPopup != null && !_openPopup.IsDisposed)
                _openPopup.Close();
            _openPopup = null;
            _openPopupTrigger = null;
        }

        private void RegisterOpenPopup(Form popup, Control trigger)
        {
            _openPopup = popup;
            _openPopupTrigger = trigger;
            popup.FormClosed += (_, __) =>
            {
                if (ReferenceEquals(_openPopup, popup))
                {
                    _openPopup = null;
                    _openPopupTrigger = null;
                }
            };
        }

        private void ShowAccountMenu()
        {
            if (ToggleIfSameTrigger(_account)) return;
            CloseOpenPopup();

            var menu = new ModernDropdownMenu { Width = 340 };
            menu.AddHeader(BuildAccountHeader());
            menu.AddSeparator();
            menu.AddItem("profile", Lang.Get("header.dropdown.profile"), IconChar.UserCircle)
                .AddItem("logout", Lang.Get("header.dropdown.logout"), IconChar.SignOutAlt, isDanger: true);

            menu.ItemClicked += (_, key) =>
            {
                if (string.Equals(key, "profile", StringComparison.OrdinalIgnoreCase))
                    ProfileClicked?.Invoke(this, EventArgs.Empty);
                else if (string.Equals(key, "logout", StringComparison.OrdinalIgnoreCase))
                    LogoutClicked?.Invoke(this, EventArgs.Empty);
            };

            RegisterOpenPopup(menu, _account);
            menu.ShowBelow(_account, offsetY: 8);
        }

        private Control BuildAccountHeader()
        {
            const int avatarSize = 56;
            const int leftPad = 8;
            const int gap = 14;
            const int rightPad = 12;
            const TextFormatFlags measureFlags = TextFormatFlags.NoPadding | TextFormatFlags.SingleLine;

            var nameFont = new Font("Segoe UI Semibold", 11f, FontStyle.Bold);
            var emailFont = new Font("Segoe UI", 9f);
            var roleFont = new Font("Segoe UI", 8.5f, FontStyle.Bold);

            Size nameSz = TextRenderer.MeasureText(_user.DisplayName, nameFont, Size.Empty, measureFlags);
            Size emailSz = TextRenderer.MeasureText(_user.Email, emailFont, Size.Empty, measureFlags);
            Size roleSz = TextRenderer.MeasureText(_user.Role, roleFont, Size.Empty, measureFlags);

            bool hasRole = _user.HasRole;
            int widest = Math.Max(nameSz.Width, Math.Max(emailSz.Width, hasRole ? roleSz.Width : 0));
            int textW = Math.Max(widest + 16, 180);
            int contentW = leftPad + avatarSize + gap + textW + rightPad;

            int textBlockH = nameSz.Height + 3 + emailSz.Height + (hasRole ? 3 + roleSz.Height : 0);
            int headerH = Math.Max(avatarSize + 20, textBlockH + 20);

            var panel = new Panel
            {
                Size = new Size(contentW, headerH),
                MinimumSize = new Size(contentW, headerH),
                BackColor = Color.Transparent
            };
            panel.Disposed += (_, __) =>
            {
                nameFont.Dispose();
                emailFont.Dispose();
                roleFont.Dispose();
            };

            var avatar = new AvatarControl
            {
                Size = new Size(avatarSize, avatarSize),
                Location = new Point(leftPad, (headerH - avatarSize) / 2),
                Initial = _user.AvatarInitial
            };
            avatar.TryLoadImage(_user.AvatarPath);
            panel.Controls.Add(avatar);

            int textX = leftPad + avatarSize + gap;
            int textY = Math.Max(10, (headerH - textBlockH) / 2);

            panel.Controls.Add(new Label
            {
                AutoSize = false,
                UseMnemonic = false,
                Text = _user.DisplayName,
                Font = nameFont,
                ForeColor = LayoutColors.DropdownText,
                BackColor = Color.Transparent,
                Location = new Point(textX, textY),
                Size = new Size(textW, nameSz.Height + 2)
            });

            panel.Controls.Add(new Label
            {
                AutoSize = false,
                UseMnemonic = false,
                Text = _user.Email,
                Font = emailFont,
                ForeColor = LayoutColors.TextMuted,
                BackColor = Color.Transparent,
                Location = new Point(textX, textY + nameSz.Height + 3),
                Size = new Size(textW, emailSz.Height + 2)
            });

            if (hasRole)
            {
                panel.Controls.Add(new Label
                {
                    AutoSize = false,
                    UseMnemonic = false,
                    Text = _user.Role,
                    Font = roleFont,
                    ForeColor = LayoutColors.Accent,
                    BackColor = Color.Transparent,
                    Location = new Point(textX, textY + nameSz.Height + 3 + emailSz.Height + 3),
                    Size = new Size(textW, roleSz.Height + 2)
                });
            }

            return panel;
        }

        private void ShowNotificationMenu()
        {
            if (ToggleIfSameTrigger(_bell)) return;
            CloseOpenPopup();

            var panel = new NotificationDropdownControl();
            panel.SetItems(null);
            RegisterOpenPopup(panel, _bell);
            panel.ShowBelow(_bell, offsetY: 8);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _titleFont.Dispose();
                _subtitleFont.Dispose();
            }
        }
    }
}
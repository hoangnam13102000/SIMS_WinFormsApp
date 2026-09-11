using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Layouts
{
    public class HeaderControl : UserControl
    {
        private readonly Panel _leftPanel;
        private readonly Panel _rightPanel;

        private readonly PictureBox _headerLogo;
        private readonly Label _titleLabel;
        private readonly Label _subtitleLabel;

        private readonly Panel _bellPanel;
        private readonly IconPictureBox _bellIcon;
        private readonly Label _badgeDot;

        private readonly Panel _accountPanel;
        private readonly Label _avatarLabel;
        private readonly Label _userNameLabel;
        private readonly Label _userEmailLabel;

        private int _unreadCount;
        private bool _bellHover;
        private bool _accountHover;
        private string _displayName = "Admin";
        private string _email = "admin@sims.local";
        private string _role = string.Empty;
        private string _avatarInitial = "A";

        private Form _openPopup;
        private Control _openPopupTrigger;

        public event EventHandler ProfileClicked;
        public event EventHandler LogoutClicked;
        public event EventHandler BellClicked;

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
            Padding = new Padding(0);

            // ===== Logo =====
            _headerLogo = new PictureBox
            {
                Size = new Size(42, 42),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };
            try { _headerLogo.Image = Properties.Resources.logo_icon; }
            catch { _headerLogo.BackColor = LayoutColors.Accent; }

            _titleLabel = new Label
            {
                AutoSize = false,
                Text = "SIMS",
                Font = new Font("Segoe UI Semibold", 14f, FontStyle.Bold),
                ForeColor = LayoutColors.TextWhite,
                BackColor = Color.Transparent
            };

            _subtitleLabel = new Label
            {
                AutoSize = false,
                Text = subtitle ?? string.Empty,
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = LayoutColors.HeaderSubtitle,
                BackColor = Color.Transparent
            };

            _leftPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 420,
                BackColor = LayoutColors.HeaderBg
            };
            _leftPanel.Controls.Add(_headerLogo);
            _leftPanel.Controls.Add(_titleLabel);
            _leftPanel.Controls.Add(_subtitleLabel);

            // ===== Bell =====
            _bellIcon = new IconPictureBox
            {
                IconChar = IconChar.Bell,
                IconFont = IconFont.Solid,
                IconColor = LayoutColors.TextWhite,
                IconSize = 26,
                Size = new Size(32, 32),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Location = new Point(14, 14)
            };
            _bellIcon.Click += (_, __) => ShowNotificationMenu();

            _badgeDot = new Label
            {
                AutoSize = false,
                Size = new Size(22, 22),
                Location = new Point(38, 2),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = LayoutColors.RedDot,
                Text = "0",
                Visible = false,
                Cursor = Cursors.Hand
            };
            using (var badgePath = new GraphicsPath())
            {
                badgePath.AddEllipse(0, 0, 21, 21);
                _badgeDot.Region = new Region(badgePath);
            }
            _badgeDot.Click += (_, __) => ShowNotificationMenu();

            _bellPanel = new Panel
            {
                Size = new Size(64, 60),
                BackColor = LayoutColors.HeaderBg,
                Cursor = Cursors.Hand
            };
            // Do not clip the parent to an ellipse: the badge sits near the
            // top-right edge and must remain a complete circle.
            _bellPanel.Controls.Add(_bellIcon);
            _bellPanel.Controls.Add(_badgeDot);
            // The badge must always remain above the bell glyph and receive clicks.
            _badgeDot.BringToFront();
            _bellPanel.Click += (_, __) => ShowNotificationMenu();
            _bellPanel.Paint += PaintBellPanel;
            _bellPanel.MouseEnter += (_, __) => { _bellHover = true; _bellPanel.Invalidate(); };
            _bellPanel.MouseLeave += (_, __) => { _bellHover = false; _bellPanel.Invalidate(); };

            // ===== Account =====
            _avatarLabel = new Label
            {
                Size = new Size(42, 42),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI Semibold", 12f),
                ForeColor = Color.White,
                BackColor = LayoutColors.Accent,
                Text = _avatarInitial,
                Cursor = Cursors.Hand
            };
            using (var path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, 41, 41);
                _avatarLabel.Region = new Region(path);
            }
            _avatarLabel.Click += (_, __) => ShowAccountMenu();

            _userNameLabel = new Label
            {
                AutoSize = false,
                AutoEllipsis = false,
                Text = _displayName,
                Font = new Font("Segoe UI Semibold", 10f, FontStyle.Bold),
                ForeColor = LayoutColors.TextWhite,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };
            _userNameLabel.Click += (_, __) => ShowAccountMenu();

            _userEmailLabel = new Label
            {
                AutoSize = false,
                AutoEllipsis = false,
                Text = _email,
                Font = new Font("Segoe UI", 8f),
                ForeColor = LayoutColors.HeaderSubtitle,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };
            _userEmailLabel.Click += (_, __) => ShowAccountMenu();

            _accountPanel = new Panel
            {
                Height = 52,
                BackColor = LayoutColors.HeaderBg,
                Cursor = Cursors.Hand
            };
            _accountPanel.Controls.Add(_avatarLabel);
            _accountPanel.Controls.Add(_userNameLabel);
            _accountPanel.Controls.Add(_userEmailLabel);
            _accountPanel.Click += (_, __) => ShowAccountMenu();
            _accountPanel.Paint += PaintAccountPanel;
            _accountPanel.MouseEnter += (_, __) => { _accountHover = true; _accountPanel.Invalidate(); };
            _accountPanel.MouseLeave += (_, __) => { _accountHover = false; _accountPanel.Invalidate(); };

            _rightPanel = new Panel
            {
                Dock = DockStyle.Right,
                Width = 400,
                BackColor = LayoutColors.HeaderBg
            };
            _rightPanel.Controls.Add(_bellPanel);
            _rightPanel.Controls.Add(_accountPanel);

            Controls.Add(_rightPanel);
            Controls.Add(_leftPanel);

            Resize += (_, __) => LayoutHeader();
            LayoutHeader();
        }

        private void LayoutHeader()
        {
            int h = Height > 0 ? Height : LayoutColors.HeaderHeight;
            int clientW = Width > 0 ? Width : 1200;

            bool narrow = clientW < 900;
            bool medium = clientW < 1100;

            _userEmailLabel.Visible = !narrow;
            _subtitleLabel.Visible = !narrow;

            const int padLeft = 20;
            int logoY = (h - _headerLogo.Height) / 2;
            _headerLogo.Location = new Point(padLeft, logoY);

            Size titleSize = TextRenderer.MeasureText(
                _titleLabel.Text ?? "", _titleLabel.Font,
                Size.Empty, TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
            Size subSize = TextRenderer.MeasureText(
                _subtitleLabel.Text ?? "", _subtitleLabel.Font,
                Size.Empty, TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);

            int textX = _headerLogo.Right + 12;
            if (_subtitleLabel.Visible)
            {
                int blockH = titleSize.Height + 3 + subSize.Height;
                int textY = Math.Max(0, (h - blockH) / 2);
                _titleLabel.Size = new Size(titleSize.Width + 8, titleSize.Height + 2);
                _titleLabel.Location = new Point(textX, textY);
                _subtitleLabel.Size = new Size(subSize.Width + 8, subSize.Height + 2);
                _subtitleLabel.Location = new Point(textX, textY + titleSize.Height + 3);
            }
            else
            {
                _titleLabel.Size = new Size(titleSize.Width + 8, titleSize.Height + 2);
                _titleLabel.Location = new Point(textX, (h - titleSize.Height) / 2);
            }

            int leftNeeded = Math.Max(_titleLabel.Right,
                _subtitleLabel.Visible ? _subtitleLabel.Right : 0) + 24;
            _leftPanel.Width = Math.Max(220, Math.Min(leftNeeded, clientW / 2));

            const int padRight = 20;

            Size nameSize = TextRenderer.MeasureText(
                _userNameLabel.Text ?? "", _userNameLabel.Font,
                Size.Empty, TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
            Size emailSize = TextRenderer.MeasureText(
                _userEmailLabel.Text ?? "", _userEmailLabel.Font,
                Size.Empty, TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);

            int textW = _userEmailLabel.Visible
                ? Math.Max(nameSize.Width, emailSize.Width) + 16
                : nameSize.Width + 16;

            if (medium && !narrow)
                textW = Math.Min(textW, 180);

            int accountW = 8 + 42 + 12 + textW + 16;
            _accountPanel.Width = accountW;
            _accountPanel.Height = 52;

            int rightNeeded = padRight + accountW + 20 + _bellPanel.Width + 16;
            _rightPanel.Width = Math.Max(200, rightNeeded);

            int accountX = _rightPanel.Width - padRight - accountW;
            int accountY = (h - _accountPanel.Height) / 2;
            _accountPanel.Location = new Point(Math.Max(0, accountX), accountY);

            _avatarLabel.Location = new Point(8, (_accountPanel.Height - 42) / 2);

            int utX = _avatarLabel.Right + 12;
            if (_userEmailLabel.Visible)
            {
                int utBlockH = nameSize.Height + 2 + emailSize.Height;
                int utY = Math.Max(0, (_accountPanel.Height - utBlockH) / 2);
                _userNameLabel.Size = new Size(nameSize.Width + 8, nameSize.Height + 2);
                _userNameLabel.Location = new Point(utX, utY);
                _userEmailLabel.Size = new Size(emailSize.Width + 12, emailSize.Height + 2);
                _userEmailLabel.Location = new Point(utX, utY + nameSize.Height + 2);
            }
            else
            {
                _userNameLabel.Size = new Size(nameSize.Width + 8, nameSize.Height + 2);
                _userNameLabel.Location = new Point(utX, (_accountPanel.Height - nameSize.Height) / 2);
            }

            int bellX = _accountPanel.Left - 20 - _bellPanel.Width;
            int bellY = (h - _bellPanel.Height) / 2;
            _bellPanel.Location = new Point(Math.Max(0, bellX), bellY);
        }

        public void SetSubtitle(string subtitle)
        {
            _subtitleLabel.Text = subtitle ?? string.Empty;
            LayoutHeader();
        }

        public void SetUser(string displayName, string email, string avatarInitial = null, string role = null)
        {
            _displayName = string.IsNullOrWhiteSpace(displayName) ? "User" : displayName;
            _email = email ?? string.Empty;
            _role = role ?? string.Empty;
            _userNameLabel.Text = _displayName;
            _userEmailLabel.Text = _email;

            if (string.IsNullOrWhiteSpace(avatarInitial))
            {
                avatarInitial = string.IsNullOrEmpty(_displayName)
                    ? "?"
                    : _displayName.Trim().Substring(0, 1).ToUpperInvariant();
            }
            _avatarInitial = avatarInitial;
            _avatarLabel.Text = avatarInitial;
            LayoutHeader();
        }

        public void SetUnreadCount(int count)
        {
            _unreadCount = Math.Max(0, count);
            if (_unreadCount <= 0)
                _badgeDot.Visible = false;
            else
            {
                _badgeDot.Text = _unreadCount > 9 ? "9+" : _unreadCount.ToString();
                _badgeDot.Visible = true;
            }
        }

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
            if (ToggleIfSameTrigger(_accountPanel)) return;
            CloseOpenPopup();

            var menu = new ModernDropdownMenu { Width = 320 };
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

            RegisterOpenPopup(menu, _accountPanel);
            menu.ShowBelow(_accountPanel, offsetY: 8);
        }

        private Control BuildAccountHeader()
        {
            var nameFont = new Font("Segoe UI Semibold", 10f, FontStyle.Bold);
            var emailFont = new Font("Segoe UI", 8.5f);
            var roleFont = new Font("Segoe UI", 8f, FontStyle.Bold);

            Size nameSz = TextRenderer.MeasureText(
                _displayName ?? "", nameFont,
                Size.Empty, TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
            Size emailSz = TextRenderer.MeasureText(
                _email ?? "", emailFont,
                Size.Empty, TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
            Size roleSz = TextRenderer.MeasureText(
                _role ?? "", roleFont,
                Size.Empty, TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);

            int textW = Math.Max(nameSz.Width, Math.Max(emailSz.Width, roleSz.Width)) + 16;
            textW = Math.Max(textW, 180);

            const int avatarSize = 42;
            const int leftPad = 6;
            const int gap = 12;
            const int rightPad = 12;
            int contentW = leftPad + avatarSize + gap + textW + rightPad;

            bool hasRole = !string.IsNullOrEmpty(_role);
            int line1 = nameSz.Height;
            int line2 = emailSz.Height;
            int line3 = hasRole ? roleSz.Height : 0;
            int textBlockH = line1 + 3 + line2 + (hasRole ? 3 + line3 : 0);
            int headerH = Math.Max(avatarSize + 20, textBlockH + 20);

            var panel = new Panel
            {
                Height = headerH,
                Width = contentW,
                BackColor = Color.Transparent,
                MinimumSize = new Size(contentW, headerH)
            };

            var avatar = new Label
            {
                Size = new Size(avatarSize, avatarSize),
                Location = new Point(leftPad, (headerH - avatarSize) / 2),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI Semibold", 12f),
                ForeColor = Color.White,
                BackColor = LayoutColors.Accent,
                Text = _avatarInitial
            };
            using (var path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, avatarSize - 1, avatarSize - 1);
                avatar.Region = new Region(path);
            }

            int textX = leftPad + avatarSize + gap;
            int textY = Math.Max(10, (headerH - textBlockH) / 2);

            var name = new Label
            {
                AutoSize = false,
                AutoEllipsis = false,
                Text = _displayName,
                Font = nameFont,
                ForeColor = LayoutColors.TextWhite,
                BackColor = Color.Transparent,
                Location = new Point(textX, textY),
                Size = new Size(textW, nameSz.Height + 2)
            };

            var email = new Label
            {
                AutoSize = false,
                AutoEllipsis = false,
                Text = _email,
                Font = emailFont,
                ForeColor = LayoutColors.TextMuted,
                BackColor = Color.Transparent,
                Location = new Point(textX, textY + line1 + 3),
                Size = new Size(textW, emailSz.Height + 2)
            };

            panel.Controls.Add(avatar);
            panel.Controls.Add(name);
            panel.Controls.Add(email);

            if (hasRole)
            {
                var role = new Label
                {
                    AutoSize = false,
                    AutoEllipsis = false,
                    Text = _role,
                    Font = roleFont,
                    ForeColor = LayoutColors.Accent,
                    BackColor = Color.Transparent,
                    Location = new Point(textX, textY + line1 + 3 + line2 + 3),
                    Size = new Size(textW, roleSz.Height + 2)
                };
                panel.Controls.Add(role);
            }

            return panel;
        }

        private void ShowNotificationMenu()
        {
            if (ToggleIfSameTrigger(_bellPanel)) return;
            CloseOpenPopup();
            var panel = new NotificationDropdownControl();
            panel.SetItems(null);
            RegisterOpenPopup(panel, _bellPanel);
            panel.ShowBelow(_bellPanel, offsetY: 8);
        }

        private void PaintBellPanel(object sender, PaintEventArgs e)
        {
            if (!_bellHover) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (var brush = new SolidBrush(LayoutColors.HeaderIconBgHover))
                e.Graphics.FillEllipse(brush, 0, 0, _bellPanel.Width - 1, _bellPanel.Height - 1);
        }

        private void PaintAccountPanel(object sender, PaintEventArgs e)
        {
            if (!_accountHover) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (var brush = new SolidBrush(LayoutColors.HeaderAccountHover))
                e.Graphics.FillRoundedRectangle(brush, 0, 0, _accountPanel.Width - 1, _accountPanel.Height - 1, 12);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var pen = new Pen(LayoutColors.HeaderBorder))
                e.Graphics.DrawLine(pen, 0, Height - 1, Width, Height - 1);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            BeginInvoke(new Action(() =>
            {
                if (IsDisposed) return;
                _bellIcon.Invalidate();
                LayoutHeader();
                BeginInvoke(new Action(() => { if (!IsDisposed) LayoutHeader(); }));
            }));
        }
    }

    internal static class GraphicsExtensions
    {
        public static void FillRoundedRectangle(this Graphics g, Brush brush, int x, int y, int w, int h, int radius)
        {
            using (var path = new GraphicsPath())
            {
                int d = radius * 2;
                path.AddArc(x, y, d, d, 180, 90);
                path.AddArc(x + w - d, y, d, d, 270, 90);
                path.AddArc(x + w - d, y + h - d, d, d, 0, 90);
                path.AddArc(x, y + h - d, d, d, 90, 90);
                path.CloseFigure();
                g.FillPath(brush, path);
            }
        }
    }
}
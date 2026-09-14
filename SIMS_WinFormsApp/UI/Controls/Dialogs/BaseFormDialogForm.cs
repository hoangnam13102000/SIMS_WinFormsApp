using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{

    public class BaseFormDialogForm : Form
    {
        #region Constants & Layout
        private const int HeaderHeight = 64;
        private const int FooterHeight = 76;
        private const int HeaderIconContainerSize = 40;
        private const int HeaderIconSize = 20;
        private const int CloseButtonSize = 32;
        private const int CornerRadius = 16;
        private const int ContentPadding = 28;

        private const int FooterButtonHeight = 44;
        private const int FooterButtonHorizontalPadding = 40;
        private const int FooterButtonMinWidth = 112;
        private const int FooterButtonSpacing = 12;
        private const int FooterButtonRightMargin = 24;

        public static readonly Size DefaultDialogSize = new Size(620, 720);
        public static readonly Size MinimumDialogSize = new Size(560, 620);
        #endregion

        #region Fields
        private readonly Panel _headerPanel;
        private readonly Panel _bodyScrollPanel;
        private readonly Panel _footerPanel;

        private readonly IconPictureBox _headerIconBox;
        private readonly Label _titleLabel;
        private readonly Button _closeButton;
        private readonly IconPictureBox _closeIconBox;
        private bool _isCloseHover;

        private readonly List<PrimaryButton> _footerButtons = new List<PrimaryButton>();
        #endregion

        #region Public API
        /// <summary>Người dùng bấm nút đóng (nút X ở header hoặc phím Esc).</summary>
        public event EventHandler CloseRequested;

        public string HeaderTitle
        {
            get => _titleLabel.Text;
            set => _titleLabel.Text = value ?? string.Empty;
        }

        protected Panel ContentHost => _bodyScrollPanel;

        protected void SetHeaderIcon(IconChar icon, Color accentColor)
        {
            _headerIconBox.IconChar = icon;
            _headerIconBox.IconColor = accentColor;
        }
        #endregion

        #region Constructor
        public BaseFormDialogForm()
        {
            AutoScaleMode = AutoScaleMode.None;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterParent;
            ShowInTaskbar = false;
            DoubleBuffered = true;
            KeyPreview = true;
            MinimizeBox = false;
            MaximizeBox = false;
            Size = DefaultDialogSize;
            MinimumSize = MinimumDialogSize;
            Font = AppFonts.Body;
            BackColor = DialogTheme.SurfaceColor;
            Padding = new Padding((int)DialogTheme.BorderWidth);

            // ===== Header (icon + tiêu đề + nút đóng) =====
            _headerPanel = new Panel { Dock = DockStyle.Top, Height = HeaderHeight, BackColor = DialogTheme.SurfaceColor };
            _headerPanel.Paint += HeaderPanel_Paint;

            var headerIconHolder = new Panel
            {
                Size = new Size(HeaderIconContainerSize, HeaderIconContainerSize),
                Location = new Point(20, (HeaderHeight - HeaderIconContainerSize) / 2),
                BackColor = Color.Transparent
            };
            headerIconHolder.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, headerIconHolder.Width - 1, headerIconHolder.Height - 1);
                using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Medium))
                using (var brush = new SolidBrush(AppColors.AccentBgSoft))
                    e.Graphics.FillPath(brush, path);
            };

            _headerIconBox = new IconPictureBox
            {
                Size = new Size(HeaderIconSize + 2, HeaderIconSize + 2),
                Location = new Point((HeaderIconContainerSize - HeaderIconSize - 2) / 2, (HeaderIconContainerSize - HeaderIconSize - 2) / 2),
                BackColor = Color.Transparent,
                IconChar = IconChar.UserPen,
                IconColor = AppColors.Accent,
                IconSize = HeaderIconSize,
                SizeMode = PictureBoxSizeMode.CenterImage
            };
            headerIconHolder.Controls.Add(_headerIconBox);

            _titleLabel = new Label
            {
                AutoSize = true,
                Location = new Point(headerIconHolder.Right + 12, (HeaderHeight - 20) / 2),
                Font = AppFonts.Subtitle,
                ForeColor = AppColors.TextTitle,
                BackColor = Color.Transparent,
                UseMnemonic = false
            };

            _closeButton = new Button
            {
                Size = new Size(CloseButtonSize, CloseButtonSize),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Text = string.Empty,
                TabStop = false
            };
            _closeButton.FlatAppearance.BorderSize = 0;
            _closeButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            _closeButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            _closeButton.Click += (s, e) => RaiseCloseRequested();
            _closeButton.Paint += CloseButton_Paint;
            _closeButton.MouseEnter += (s, e) => { _isCloseHover = true; UpdateCloseIconColor(); _closeButton.Invalidate(); };
            _closeButton.MouseLeave += (s, e) => { _isCloseHover = false; UpdateCloseIconColor(); _closeButton.Invalidate(); };

            _closeIconBox = new IconPictureBox
            {
                Size = new Size(16, 16),
                Location = new Point((CloseButtonSize - 16) / 2, (CloseButtonSize - 16) / 2),
                BackColor = Color.Transparent,
                IconChar = IconChar.Xmark,
                IconColor = AppColors.TextMuted,
                IconSize = 16
            };
            _closeButton.Controls.Add(_closeIconBox);

            _headerPanel.Controls.Add(_titleLabel);
            _headerPanel.Controls.Add(headerIconHolder);
            _headerPanel.Controls.Add(_closeButton);

            // ===== Body: 1 vùng cuộn dọc duy nhất (không sidebar/tab) =====
            _bodyScrollPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = DialogTheme.SurfaceColor,
                Padding = new Padding(ContentPadding, 20, ContentPadding, 20)
            };

            // ===== Footer =====
            _footerPanel = new Panel { Dock = DockStyle.Bottom, Height = FooterHeight, BackColor = DialogTheme.SurfaceColor };
            _footerPanel.Paint += FooterPanel_Paint;

            // ===== Ráp control =====
            Controls.Add(_bodyScrollPanel);
            Controls.Add(_footerPanel);
            Controls.Add(_headerPanel);

            KeyDown += BaseFormDialogForm_KeyDown;
            Resize += (s, e) => { RepositionHeaderControls(); RepositionFooterButtons(); ApplyRoundedRegion(); Invalidate(); };
            ThemeManager.Instance.ThemeChanged += ThemeManager_ThemeChanged;

            RepositionHeaderControls();
            RepositionFooterButtons();
            ApplyRoundedRegion();
        }

        public BaseFormDialogForm(IWin32Window owner) : this()
        {
            if (owner is Control control)
            {
                Rectangle ownerRect = owner is Form ownerForm
                    ? ownerForm.Bounds
                    : new Rectangle(control.PointToScreen(Point.Empty), control.Size);

                StartPosition = FormStartPosition.Manual;
                Load += (s, e) =>
                {
                    int x = ownerRect.Left + (ownerRect.Width - Width) / 2;
                    int y = ownerRect.Top + (ownerRect.Height - Height) / 2;
                    Location = new Point(Math.Max(0, x), Math.Max(0, y));
                };
            }
        }
        #endregion

        #region Footer buttons (extension point cho lớp con)

        protected PrimaryButton AddFooterButton(string text, bool isPrimary, EventHandler onClick)
        {
            int textWidth;
            using (var g = CreateGraphics())
                textWidth = TextRenderer.MeasureText(g, text, AppFonts.Button).Width;

            var button = new PrimaryButton
            {
                Text = text,
                IsPrimary = isPrimary,
                Size = new Size(Math.Max(FooterButtonMinWidth, textWidth + FooterButtonHorizontalPadding), FooterButtonHeight),
                CornerRadius = AppRadius.Medium,
                Font = AppFonts.Button
            };
            if (onClick != null) button.Click += onClick;

            _footerButtons.Add(button);
            _footerPanel.Controls.Add(button);
            RepositionFooterButtons();
            return button;
        }

        /// <summary>Bật/tắt toàn bộ nút footer - dùng khi đang lưu để tránh bấm lặp.</summary>
        protected void SetFooterButtonsEnabled(bool enabled)
        {
            foreach (var btn in _footerButtons) btn.Enabled = enabled;
        }

        private void RepositionFooterButtons()
        {
            if (_footerButtons.Count == 0) return;

            int y = (FooterHeight - FooterButtonHeight) / 2 + 6; // +6 chừa chỗ cho đường kẻ phân cách phía trên
            int totalWidth = _footerButtons.Sum(b => b.Width) + (_footerButtons.Count - 1) * FooterButtonSpacing;
            int startX = _footerPanel.ClientSize.Width - FooterButtonRightMargin - totalWidth;
            if (startX < FooterButtonRightMargin) startX = FooterButtonRightMargin;

            int currentX = startX;
            foreach (var btn in _footerButtons)
            {
                btn.Location = new Point(currentX, y);
                currentX += btn.Width + FooterButtonSpacing;
            }
        }
        #endregion

        #region Close handling
        protected void RaiseCloseRequested() => CloseRequested?.Invoke(this, EventArgs.Empty);

        private void BaseFormDialogForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                RaiseCloseRequested();
            }
        }
        #endregion

        #region Painting & theming
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            var contentRect = new Rectangle(1, 1, Math.Max(1, ClientSize.Width - 2), Math.Max(1, ClientSize.Height - 2));
            using (var path = AppRadius.GetRoundedPath(contentRect, CornerRadius - 2))
            using (var pen = new Pen(DialogTheme.BorderColor, DialogTheme.BorderWidth))
            {
                g.DrawPath(pen, path);
            }
        }

        private void HeaderPanel_Paint(object sender, PaintEventArgs e)
        {
            using (var pen = new Pen(DialogTheme.BorderColor, 1f))
                e.Graphics.DrawLine(pen, 20, HeaderHeight - 1, _headerPanel.ClientSize.Width - 20, HeaderHeight - 1);
        }

        private void FooterPanel_Paint(object sender, PaintEventArgs e)
        {
            using (var pen = new Pen(DialogTheme.BorderColor, 1f))
                e.Graphics.DrawLine(pen, 20, 0, _footerPanel.ClientSize.Width - 20, 0);
        }

        private void CloseButton_Paint(object sender, PaintEventArgs e)
        {
            if (!_isCloseHover) return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, CloseButtonSize, CloseButtonSize);
            using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Small))
            using (var brush = new SolidBrush(AppColors.CancelHover))
                e.Graphics.FillPath(brush, path);
        }

        private void UpdateCloseIconColor()
        {
            _closeIconBox.IconColor = _isCloseHover ? AppColors.Error : AppColors.TextMuted;
        }

        private void RepositionHeaderControls()
        {
            _closeButton.Location = new Point(_headerPanel.ClientSize.Width - CloseButtonSize - 12, (HeaderHeight - CloseButtonSize) / 2);
        }

        private void ApplyRoundedRegion()
        {
            if (Width <= 0 || Height <= 0) return;
            var rect = new Rectangle(0, 0, Math.Max(1, ClientSize.Width - 1), Math.Max(1, ClientSize.Height - 1));
            using (var path = AppRadius.GetRoundedPath(rect, CornerRadius + 2))
                Region = new Region(path);
        }

        private void ThemeManager_ThemeChanged(object sender, EventArgs e)
        {
            BackColor = DialogTheme.SurfaceColor;
            _headerPanel.BackColor = DialogTheme.SurfaceColor;
            _bodyScrollPanel.BackColor = DialogTheme.SurfaceColor;
            _footerPanel.BackColor = DialogTheme.SurfaceColor;
            _titleLabel.ForeColor = AppColors.TextTitle;
            UpdateCloseIconColor();
            Invalidate(true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.Instance.ThemeChanged -= ThemeManager_ThemeChanged;
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
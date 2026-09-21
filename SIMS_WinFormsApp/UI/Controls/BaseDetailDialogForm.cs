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

    public class BaseDetailDialogForm : Form
    {
        #region Constants & Layout
        private const int HeaderHeight = 64;
        private const int FooterHeight = 72;
        private const int HeaderIconContainerSize = 40;
        private const int HeaderIconSize = 20;
        private const int CloseButtonSize = 40;          // [FIX-X] 32 -> 40 cho dễ nhìn, dễ bấm
        private const int CloseButtonRightMargin = 16;   // [FIX-X]
        private const int CornerRadius = 18;
        private const int SidebarWidth = 208;
        private const int TabStripHeight = 44;
        private const int TabButtonHeight = 30;
        private const int TabButtonGap = 10;
        private const int ContentPadding = 24;

        private const int FooterButtonWidth = 112;
        private const int FooterButtonHeight = 40;
        private const int FooterButtonSpacing = 12;
        private const int FooterButtonRightMargin = 24;

        public static readonly Size DefaultDialogSize = new Size(1040, 600);
        public static readonly Size MinimumDialogSize = new Size(940, 540);
        #endregion

        #region Fields
        // Khung bo góc + viền được giao cho 1 lớp riêng (SRP).
        private readonly RoundedDialogFrame _frame = new RoundedDialogFrame(CornerRadius, DialogTheme.BorderWidth);

        private readonly Panel _headerPanel;
        private readonly Panel _bodyPanel;
        private readonly Panel _footerPanel;
        private readonly Panel _sidebarPanel;
        private readonly Panel _contentPanel;
        private readonly Panel _tabStripPanel;
        private readonly Panel _tabStripDivider;
        private readonly Panel _tabContentHost;

        private readonly IconPictureBox _headerIconBox;
        private readonly Label _titleLabel;

        // [FIX-X] Nút đóng là control riêng, form không còn tự vẽ/theo dõi hover nữa.
        private readonly DialogCloseButton _closeButton;

        private readonly DetailAvatarPanel _avatarPanel;

        private readonly List<PillTabButton> _tabButtons = new List<PillTabButton>();
        private readonly Dictionary<string, Panel> _tabPanels = new Dictionary<string, Panel>();
        private readonly List<PrimaryButton> _footerButtons = new List<PrimaryButton>();
        private string _selectedTabKey;
        #endregion

        #region Public API

        public event EventHandler CloseRequested;

        public string HeaderTitle
        {
            get => _titleLabel.Text;
            set => _titleLabel.Text = value ?? string.Empty;
        }

        protected DetailAvatarPanel Avatar => _avatarPanel;

        protected void SetHeaderIcon(IconChar icon, Color accentColor)
        {
            _headerIconBox.IconChar = icon;
            _headerIconBox.IconColor = accentColor;
        }
        #endregion

        #region Constructor
        public BaseDetailDialogForm()
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

            // ===== Header =====
            // Header TRONG SUỐT: panel này phủ 2 góc trên (cung bo góc nằm lọt trong vùng của nó).
            // Nếu để nền đặc, nó sẽ vẽ đè lên nét viền do form vẽ => mất viền ở góc.
            _headerPanel = new Panel { Dock = DockStyle.Top, Height = HeaderHeight, BackColor = Color.Transparent };
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
                IconChar = IconChar.IdCard,
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

            // [FIX-X] Nút đóng mới: to hơn, có nền, dấu X vẽ vector.
            _closeButton = new DialogCloseButton
            {
                Size = new Size(CloseButtonSize, CloseButtonSize)
            };
            _closeButton.Click += (s, e) => RaiseCloseRequested();

            _headerPanel.Controls.Add(_titleLabel);
            _headerPanel.Controls.Add(headerIconHolder);
            _headerPanel.Controls.Add(_closeButton);

            // ===== Body: sidebar (avatar) + content (tabs) =====
            _bodyPanel = new Panel { Dock = DockStyle.Fill, BackColor = DialogTheme.SurfaceColor };

            _avatarPanel = new DetailAvatarPanel { Dock = DockStyle.Top, Height = 220 };

            _sidebarPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = SidebarWidth,
                BackColor = DialogTheme.SurfaceColor,
                Padding = new Padding(8, 24, 8, 8)
            };
            _sidebarPanel.Controls.Add(_avatarPanel);

            _tabStripPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = TabStripHeight,
                BackColor = Color.Transparent,
                Padding = new Padding(ContentPadding, 8, ContentPadding, 4)
            };

            _tabStripDivider = new Panel { Dock = DockStyle.Top, Height = 1, BackColor = DialogTheme.BorderColor };

            _tabContentHost = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(ContentPadding, 16, ContentPadding, 16)
            };

            _contentPanel = new Panel { Dock = DockStyle.Fill, BackColor = DialogTheme.SurfaceColor };
            _contentPanel.Controls.Add(_tabContentHost);
            _contentPanel.Controls.Add(_tabStripDivider);
            _contentPanel.Controls.Add(_tabStripPanel);

            _bodyPanel.Controls.Add(_contentPanel);
            _bodyPanel.Controls.Add(_sidebarPanel);

            // ===== Footer =====
            // Footer cũng TRONG SUỐT vì lý do như header (phủ 2 góc dưới).
            _footerPanel = new Panel { Dock = DockStyle.Bottom, Height = FooterHeight, BackColor = Color.Transparent };
            _footerPanel.Paint += FooterPanel_Paint;
            AddFooterButton("Đóng", false, (s, e) => RaiseCloseRequested());

            // ===== Ráp control =====
            Controls.Add(_bodyPanel);
            Controls.Add(_footerPanel);
            Controls.Add(_headerPanel);

            KeyDown += BaseDetailDialogForm_KeyDown;

            // Invalidate(true): control con trong suốt phải được vẽ lại theo nền/viền của form cha.
            Resize += (s, e) =>
            {
                RepositionHeaderControls();
                RepositionFooterButtons();
                _frame.ApplyClip(this);
                Invalidate(true);
            };
            ThemeManager.Instance.ThemeChanged += ThemeManager_ThemeChanged;

            RepositionHeaderControls();
            RepositionFooterButtons();
            _frame.ApplyClip(this);
        }

        /// <summary>Constructor với owner - căn giữa chính xác theo form cha (form không viền
        /// nên CenterParent mặc định của WinForms không đủ tin cậy).</summary>
        public BaseDetailDialogForm(IWin32Window owner) : this()
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

        #region Tab management (extension point cho lớp con)
        /// <summary>
        /// Tạo 1 tab mới (nút pill trên dải tab + panel nội dung tương ứng) và trả về panel đó
        /// để lớp con tự đổ control vào (khuyến khích dùng <see cref="DetailInfoItemControl"/>
        /// xếp trong TableLayoutPanel 2 cột). Tab đầu tiên được tạo sẽ tự động là tab đang chọn.
        /// </summary>
        protected Panel AddTab(string key, string label)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("Tab key is required.", nameof(key));
            if (_tabPanels.ContainsKey(key)) throw new InvalidOperationException($"Tab '{key}' đã tồn tại.");

            var pill = new PillTabButton { Text = label, Height = TabButtonHeight, Tag = key };
            pill.Click += (s, e) => SelectTab(key);
            _tabButtons.Add(pill);
            _tabStripPanel.Controls.Add(pill);
            LayoutTabButtons();

            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.Transparent,
                Visible = false
            };
            _tabContentHost.Controls.Add(panel);
            _tabPanels[key] = panel;

            if (_selectedTabKey == null) SelectTab(key);
            return panel;
        }

        /// <summary>Chuyển sang tab tương ứng theo key đã dùng khi gọi <see cref="AddTab"/>.</summary>
        protected void SelectTab(string key)
        {
            if (!_tabPanels.ContainsKey(key)) return;
            _selectedTabKey = key;

            foreach (var kv in _tabPanels)
                kv.Value.Visible = kv.Key == key;

            foreach (var btn in _tabButtons)
                btn.Selected = key.Equals(btn.Tag as string, StringComparison.Ordinal);
        }

        private void LayoutTabButtons()
        {
            // Panel.Padding không tự áp dụng cho control đặt bằng Location tuyệt đối, nên
            // phải tự cộng padding vào tọa độ (xem ghi chú tương tự trong BaseTable).
            int availableHeight = TabStripHeight - _tabStripPanel.Padding.Top - _tabStripPanel.Padding.Bottom;
            int y = _tabStripPanel.Padding.Top + Math.Max(0, (availableHeight - TabButtonHeight) / 2);
            int x = _tabStripPanel.Padding.Left;

            foreach (var btn in _tabButtons)
            {
                using (var g = CreateGraphics())
                {
                    int textWidth = TextRenderer.MeasureText(g, btn.Text, btn.Font).Width;
                    btn.Width = textWidth + 28;
                }
                btn.Height = TabButtonHeight;
                btn.Location = new Point(x, y);
                x += btn.Width + TabButtonGap;
            }
        }
        #endregion

        #region Footer buttons (extension point cho lớp con)

        protected PrimaryButton AddFooterButton(string text, bool isPrimary, EventHandler onClick)
        {
            var button = new PrimaryButton
            {
                Text = text,
                IsPrimary = isPrimary,
                Size = new Size(FooterButtonWidth, FooterButtonHeight),
                CornerRadius = AppRadius.Medium,
                Font = AppFonts.Button
            };
            if (onClick != null) button.Click += onClick;

            _footerButtons.Insert(0, button);
            _footerPanel.Controls.Add(button);
            RepositionFooterButtons();
            return button;
        }

        private void RepositionFooterButtons()
        {
            if (_footerButtons.Count == 0) return;

            int y = (FooterHeight - FooterButtonHeight) / 2 + 6; // +6 để chừa chỗ cho đường kẻ phân cách phía trên
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

        private void BaseDetailDialogForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                RaiseCloseRequested();
            }
        }
        #endregion

        #region Painting & theming
        /// <summary>
        /// Vẽ viền bo góc. Vì header/footer trong suốt, khi vẽ nền chúng gọi ngược lên
        /// OnPaint của form này, nên nét viền ở 4 góc luôn hiện đầy đủ (kể cả phần cung tròn).
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            _frame.PaintBorder(e.Graphics, ClientSize, DialogTheme.BorderColor);
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

        private void RepositionHeaderControls()
        {
            // [FIX-X] Canh theo CloseButtonSize / CloseButtonRightMargin mới.
            _closeButton.Location = new Point(
                _headerPanel.ClientSize.Width - CloseButtonSize - CloseButtonRightMargin,
                (HeaderHeight - CloseButtonSize) / 2);
        }

        private void ThemeManager_ThemeChanged(object sender, EventArgs e)
        {
            // _headerPanel / _footerPanel giữ Transparent nên không gán lại BackColor ở đây.
            // _closeButton tự đọc AppColors lúc vẽ nên chỉ cần Invalidate(true).
            BackColor = DialogTheme.SurfaceColor;
            _bodyPanel.BackColor = DialogTheme.SurfaceColor;
            _contentPanel.BackColor = DialogTheme.SurfaceColor;
            _sidebarPanel.BackColor = DialogTheme.SurfaceColor;
            _tabStripDivider.BackColor = DialogTheme.BorderColor;
            _titleLabel.ForeColor = AppColors.TextTitle;
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

        #region PillTabButton (control nội bộ, không dùng lại ngoài lớp này)
        private sealed class PillTabButton : Control
        {
            private bool _selected;

            public bool Selected
            {
                get => _selected;
                set { _selected = value; Invalidate(); }
            }

            public PillTabButton()
            {
                SetStyle(ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.UserPaint |
                         ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.ResizeRedraw |
                         ControlStyles.SupportsTransparentBackColor, true);

                Cursor = Cursors.Hand;
                Font = AppFonts.SmallBold;
                BackColor = Color.Transparent;
            }

            protected override void OnPaintBackground(PaintEventArgs pevent) { }

            protected override void OnPaint(PaintEventArgs e)
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                Color bg = _selected ? AppColors.AccentBgSoft : AppColors.BgLighter;
                Color fg = _selected ? AppColors.Accent : AppColors.TextSecondary;

                var rect = new Rectangle(0, 0, Width - 1, Height - 1);
                using (var path = AppRadius.GetRoundedPath(rect, Height / 2))
                using (var brush = new SolidBrush(bg))
                    g.FillPath(brush, path);

                TextRenderer.DrawText(g, Text, Font, ClientRectangle, fg,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
            }
        }
        #endregion
    }
}
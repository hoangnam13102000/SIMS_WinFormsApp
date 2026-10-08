using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{

    public partial class BaseFormDialogForm : Form
    {
        #region Constants & Layout
        private const int HeaderHeight = 64;
        private const int FooterHeight = 76;
        private const int HeaderIconContainerSize = 40;
        private const int HeaderIconSize = 20;
        private const int CloseButtonSize = 40;
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
        private readonly RoundedDialogFrame _frame = new RoundedDialogFrame(CornerRadius, DialogTheme.BorderWidth);

        private Panel _headerPanel;
        private Panel _headerIconHolder;
        private Panel _bodyScrollPanel;
        private Panel _footerPanel;

        private IconPictureBox _headerIconBox;
        private Label _titleLabel;
        private DialogCloseButton _closeButton;

        private readonly List<PrimaryButton> _footerButtons = new List<PrimaryButton>();
        private Rectangle _ownerBounds;
        private bool _centerOnOwner;
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

        #region Content lifecycle
        /// <summary>
        /// Được gọi đúng 1 lần trong <see cref="OnLoad"/>, tức là SAU KHI handle cửa sổ đã được
        /// tạo và <see cref="ContentHost"/> (Panel AutoScroll=true, Dock=Fill) đã có ClientSize /
        /// DisplayRectangle CHÍNH XÁC theo Size thật sự của form (900x560, 960x780...).
        ///
        /// Đây chính là nguyên nhân popup bị "cắt" nội dung (chỉ thấy avatar, 2 cột còn lại co
        /// rúm lại vài px): trước đây các lớp con gọi thẳng <c>fieldsGrid.Reflow()</c> ngay trong
        /// constructor/BuildContent(), tức là TRƯỚC KHI Form có handle. Tại thời điểm đó,
        /// ContentHost/fieldsGrid có thể vẫn đang mang Width mặc định do control-tree chưa được
        /// WinForms "chốt" kích thước theo ClientSize thật của Form (việc gán Size trong
        /// constructor không đảm bảo cascade Dock đồng bộ 100% khi Form chưa được tạo handle,
        /// đặc biệt với Panel AutoScroll lồng nhau). Reflow() vì vậy chạy với 1 Width "rác" nhỏ
        /// hơn nhiều so với thực tế, khiến 2 cột nội dung bị ép xuống mức tối thiểu 10px.
        ///
        /// Mọi layout phụ thuộc độ rộng ContentHost tại thời điểm khởi tạo (gọi
        /// ThreeColumnFieldsPanel.Reflow() lần đầu, đo chữ để MeasureText...) PHẢI đặt ở đây thay
        /// vì gọi trực tiếp trong BuildContent(). Mặc định không làm gì - lớp con override nếu
        /// cần.
        /// </summary>
        protected virtual void OnContentReady()
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            // Gọi OnContentReady() TRƯỚC base.OnLoad(e): base.OnLoad(e) chính là nơi sự kiện
            // Load được phát ra, và constructor BaseFormDialogForm(IWin32Window) đăng ký 1 handler
            // Load để canh giữa popup dựa trên Width/Height HIỆN TẠI của Form. Nếu OnContentReady()
            // (nơi FitHeightToContent() có thể đổi Height) chạy SAU khi Load đã phát ra, handler
            // canh giữa sẽ dùng Height CŨ (trước khi co giãn) - khiến popup lệch tâm theo trục dọc.
            OnContentReady();
            base.OnLoad(e);
        }
        #endregion

        #region Content sizing
        /// <summary>
        /// Co Height của Form vừa khít với chiều cao nội dung THẬT SỰ đang có trong ContentHost
        /// (đo bằng Bottom của control thấp nhất, đúng cách LayoutColumn/Reflow đang dùng), thay
        /// vì giữ nguyên 1 con số cố định đoán trước (rất dễ dư/thiếu tuỳ nội dung từng popup,
        /// dẫn tới hiện scroll bar không cần thiết như popup Cập nhật tài khoản từng gặp).
        ///
        /// PHẢI gọi ở cuối OnContentReady() (sau khi đã Reflow layout ngang nếu có) - lúc đó
        /// ContentHost đã có ClientSize thật và mọi control con đã có Bounds cuối cùng. Nếu nội
        /// dung cao hơn không gian màn hình cho phép, Height chỉ tăng tới giới hạn màn hình - khi
        /// đó ContentHost.AutoScroll vẫn hoạt động bình thường như lưới an toàn, không có gì vỡ.
        /// </summary>
        protected void FitHeightToContent(int screenEdgeMargin = 60)
        {
            if (ContentHost.Controls.Count == 0) return;

            int contentHeight = 0;
            foreach (Control child in ContentHost.Controls)
            {
                if (child.Visible) contentHeight = Math.Max(contentHeight, child.Bottom);
            }

            int desiredHeight = HeaderHeight + FooterHeight
                + ContentHost.Padding.Top + contentHeight + ContentHost.Padding.Bottom
                + Padding.Top + Padding.Bottom;

            int maxScreenHeight = Screen.FromControl(this).WorkingArea.Height - screenEdgeMargin;
            desiredHeight = Math.Max(MinimumSize.Height, Math.Min(desiredHeight, maxScreenHeight));

            if (Height != desiredHeight) Height = desiredHeight;
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

            InitializeComponent();
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            ApplyThemeColors();
            KeyDown += BaseFormDialogForm_KeyDown;
            Resize += BaseFormDialogForm_Resize;
            ThemeManager.Instance.ThemeChanged += ThemeManager_ThemeChanged;

            RepositionHeaderControls();
            RepositionFooterButtons();
            _frame.ApplyClip(this);
        }

        public BaseFormDialogForm(IWin32Window owner) : this()
        {
            if (owner is Control control)
            {
                _ownerBounds = owner is Form ownerForm
                    ? ownerForm.Bounds
                    : new Rectangle(control.PointToScreen(Point.Empty), control.Size);

                StartPosition = FormStartPosition.Manual;
                _centerOnOwner = true;
                Load += CenterOnOwner;
            }
        }
        #endregion

        private void BaseFormDialogForm_Resize(object sender, EventArgs e)
        {
            RepositionHeaderControls();
            RepositionFooterButtons();
            _frame.ApplyClip(this);
            Invalidate(true);
        }

        private void HeaderIconHolder_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, _headerIconHolder.Width - 1, _headerIconHolder.Height - 1);
            using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Medium))
            using (var brush = new SolidBrush(AppColors.AccentBgSoft))
                e.Graphics.FillPath(brush, path);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            RaiseCloseRequested();
        }

        private void CenterOnOwner(object sender, EventArgs e)
        {
            if (!_centerOnOwner) return;
            int x = _ownerBounds.Left + (_ownerBounds.Width - Width) / 2;
            int y = _ownerBounds.Top + (_ownerBounds.Height - Height) / 2;
            Location = new Point(Math.Max(0, x), Math.Max(0, y));
        }

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

            _frame.PaintBorder(g, ClientSize, DialogTheme.BorderColor);
        }

        private void FooterPanel_Paint(object sender, PaintEventArgs e)
        {
            using (var pen = new Pen(DialogTheme.BorderColor, 1f))
                e.Graphics.DrawLine(pen, 20, 0, _footerPanel.ClientSize.Width - 20, 0);
        }

        private void RepositionHeaderControls()
        {
            _closeButton.Location = new Point(_headerPanel.ClientSize.Width - CloseButtonSize - 16, (HeaderHeight - CloseButtonSize) / 2);
        }

        private void ThemeManager_ThemeChanged(object sender, EventArgs e)
        {
            ApplyThemeColors();
            _frame.ApplyClip(this);
            Invalidate(true);
        }

        private void ApplyThemeColors()
        {
            BackColor = DialogTheme.SurfaceColor;
            _headerPanel.BackColor = DialogTheme.SurfaceColor;
            _bodyScrollPanel.BackColor = DialogTheme.SurfaceColor;
            _footerPanel.BackColor = DialogTheme.SurfaceColor;
            _titleLabel.Font = AppFonts.Subtitle;
            _titleLabel.ForeColor = AppColors.TextTitle;
            _headerIconBox.IconColor = AppColors.Accent;
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
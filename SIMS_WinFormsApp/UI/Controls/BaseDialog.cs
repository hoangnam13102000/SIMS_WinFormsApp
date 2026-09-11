using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    /// <summary>
    /// BaseDialog reusable - Form modal hiện đại dùng chung toàn bộ ứng dụng.
    /// Hỗ trợ 5 loại dialog (Info/Warning/Error/Success/Question),
    /// Light/Dark theme tự động theo ThemeManager, keyboard navigation,
    /// rounded corners, icon semantic, auto-resize theo nội dung.
    /// 
    /// KHÔNG chứa business logic, database, service - chỉ UI và dialog state.
    /// </summary>
    public class BaseDialog : Form
    {
        #region Constants & Layout
        private const int HeaderHeight = 56;
        private const int FooterHeight = 72;
        private const int IconSize = 56;
        private const int HeaderIconSize = 18;
        private const int CloseButtonSize = 32;
        private const int CornerRadius = 16;
        private const int MinWidth = 440;
        private const int MinHeight = 220;
        private const int MaxWidth = 680;
        private const int ButtonWidth = 110;
        private const int ButtonHeight = 40;
        private const int ButtonSpacing = 12;
        private const int ButtonRightMargin = 24;
        #endregion

        #region Fields
        private DialogType _iconType = DialogType.Info;
        private DialogButtons _buttons = DialogButtons.OK;
        private DialogResult _defaultButton = DialogResult.OK;
        private string _message = string.Empty;
        private bool _isCloseHover;
        private bool _isAppearanceInitialized;

        private readonly Panel _headerPanel;
        private readonly Panel _bodyPanel;
        private readonly Panel _footerPanel;
        private readonly Panel _iconPanel;
        private readonly Label _messageLabel;
        private readonly Label _titleLabel;
        private readonly IconPictureBox _headerIconBox;    // 🆕 Dùng IconPictureBox
        private readonly IconPictureBox _bodyIconBox;      // 🆕 Dùng IconPictureBox
        private readonly IconPictureBox _closeIconBox;     // 🆕 Dùng IconPictureBox cho close
        private readonly Button _closeButton;
        private readonly List<PrimaryButton> _dialogButtons = new List<PrimaryButton>();
        #endregion

        #region Public Properties
        // 🆕 Bỏ từ khóa "new" - không ẩn thành phần nào
        public string Title
        {
            get => _titleLabel.Text;
            set => _titleLabel.Text = value ?? string.Empty;
        }

        public string Message
        {
            get => _message;
            set
            {
                _message = value ?? string.Empty;
                _messageLabel.Text = _message;
                AdjustSizeToContent();
            }
        }

        public DialogType IconType
        {
            get => _iconType;
            set
            {
                _iconType = value;
                UpdateDialogAppearance();
            }
        }

        public DialogButtons Buttons
        {
            get => _buttons;
            set
            {
                _buttons = value;
                RebuildButtons();
            }
        }

        public DialogResult DefaultButton
        {
            get => _defaultButton;
            set
            {
                _defaultButton = value;
                UpdateDefaultButtonFocus();
            }
        }
        #endregion

        #region Constructor
        public BaseDialog()
        {
            // ===== Cấu hình Form cơ bản =====
            // QUAN TRỌNG: BaseDialog trước đây không set AutoScaleMode, nên nó dùng
            // mặc định AutoScaleMode.Inherit -> hoạt động như Font-based autoscale.
            // Form này được ShowDialog() ngay giữa luồng Logout (frmMain -> BaseDialog
            // -> frmLogin mới), và việc nó tự tính lại "current autoscale dimensions"
            // theo Font/DPI hiện tại của tiến trình (khác quy ước AutoScaleMode.None đã
            // dùng ở frmLogin/frmMain) chính là nguyên nhân khiến ngữ cảnh DPI của process
            // bị lệch, làm frmLogin được tạo NGAY SAU ĐÓ hiển thị nhỏ/lệch hơn lần đầu dù
            // code InitializeComponent() của frmLogin không đổi. Set None ở đây để toàn bộ
            // Form trong app nhất quán 1 quy ước, không còn Form nào tự autoscale nữa.
            AutoScaleMode = AutoScaleMode.None;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterParent;
            ShowInTaskbar = false;
            DoubleBuffered = true;
            KeyPreview = true;
            MinimizeBox = false;
            MaximizeBox = false;
            MinimumSize = new Size(MinWidth, MinHeight);
            Font = AppFonts.Body;
            BackColor = AppColors.White;

            // ===== Tạo các control con =====
            _headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = HeaderHeight,
                BackColor = AppColors.White
            };

            _bodyPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = AppColors.White
            };

            _footerPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = FooterHeight,
                BackColor = AppColors.White
            };

            // 🆕 Icon header dùng IconPictureBox
            _headerIconBox = new IconPictureBox
            {
                Size = new Size(HeaderIconSize, HeaderIconSize),
                Location = new Point(20, (HeaderHeight - HeaderIconSize) / 2),
                BackColor = Color.Transparent,
                IconChar = IconChar.CircleInfo,
                IconColor = AppColors.Info,
                IconSize = HeaderIconSize
            };

            _titleLabel = new Label
            {
                AutoSize = true,
                Location = new Point(48, 18),
                Font = AppFonts.Subtitle,
                BackColor = Color.Transparent,
                UseMnemonic = false
            };

            // Close button wrapper
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
            _closeButton.Click += (s, e) => CloseWithResult(DialogResult.Cancel);
            _closeButton.Paint += CloseButton_Paint;
            _closeButton.MouseEnter += (s, e) => { _isCloseHover = true; UpdateCloseIconColor(); };
            _closeButton.MouseLeave += (s, e) => { _isCloseHover = false; UpdateCloseIconColor(); };

            // 🆕 Close icon dùng IconPictureBox
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

            _iconPanel = new Panel
            {
                Size = new Size(IconSize + 24, IconSize + 24),
                Location = new Point(24, 20),
                BackColor = Color.Transparent
            };
            _iconPanel.Paint += IconPanel_Paint;

            // 🆕 Body icon dùng IconPictureBox
            _bodyIconBox = new IconPictureBox
            {
                Size = new Size(IconSize, IconSize),
                Location = new Point(12, 12),
                BackColor = Color.Transparent,
                IconChar = IconChar.CircleInfo,
                IconColor = AppColors.Info,
                IconSize = IconSize
            };
            _iconPanel.Controls.Add(_bodyIconBox);

            _messageLabel = new Label
            {
                AutoSize = false,
                Location = new Point(110, 20),
                Font = AppFonts.Body,
                BackColor = Color.Transparent,
                UseMnemonic = false
            };

            // ===== Sắp xếp control =====
            _headerPanel.Controls.Add(_closeButton);
            _headerPanel.Controls.Add(_titleLabel);
            _headerPanel.Controls.Add(_headerIconBox);

            _bodyPanel.Controls.Add(_iconPanel);
            _bodyPanel.Controls.Add(_messageLabel);

            Controls.Add(_bodyPanel);
            Controls.Add(_footerPanel);
            Controls.Add(_headerPanel);

            // ===== Sự kiện =====
            _headerPanel.Paint += HeaderPanel_Paint;
            _footerPanel.Paint += FooterPanel_Paint;
            KeyDown += BaseDialog_KeyDown;
            Resize += BaseDialog_Resize;
            Shown += BaseDialog_Shown;

            // ===== Theme =====
            ApplyThemeColors();
            ThemeManager.Instance.ThemeChanged += ThemeManager_ThemeChanged;

            // ===== Khởi tạo mặc định =====
            _isAppearanceInitialized = true;
            UpdateDialogAppearance();
            RebuildButtons();
            AdjustSizeToContent();
            ApplyRoundedRegion();
        }

        /// <summary>
        /// Constructor với owner - giúp căn giữa chính xác theo form cha.
        /// </summary>
        public BaseDialog(IWin32Window owner) : this()
        {
            if (owner is Control control)
            {
                Rectangle ownerRect;
                if (owner is Form ownerForm)
                    ownerRect = ownerForm.Bounds;
                else
                    ownerRect = new Rectangle(control.PointToScreen(Point.Empty), control.Size);

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

        #region Protected Overrides
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            // ===== Vẽ viền nhẹ xung quanh dialog =====
            var contentRect = new Rectangle(0, 0, Width - 1, Height - 1);

            using (var bgPath = AppRadius.GetRoundedPath(contentRect, CornerRadius))
            using (var borderPen = new Pen(AppColors.Border, 1f))
            {
                g.DrawPath(borderPen, bgPath);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyRoundedRegion();
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

        #region Private Methods - UI Update
        private void ApplyRoundedRegion()
        {
            if (Width <= 0 || Height <= 0) return;

            var rect = new Rectangle(0, 0, Width, Height);
            using (var path = AppRadius.GetRoundedPath(rect, CornerRadius))
            {
                Region = new Region(path);
            }
        }

        private void ApplyThemeColors()
        {
            BackColor = AppColors.White;
            _headerPanel.BackColor = AppColors.White;
            _bodyPanel.BackColor = AppColors.White;
            _footerPanel.BackColor = AppColors.White;
            _titleLabel.ForeColor = AppColors.TextTitle;
            _messageLabel.ForeColor = AppColors.TextPrimary;
            UpdateCloseIconColor();
            Invalidate(true);
        }

        private void UpdateCloseIconColor()
        {
            if (_closeIconBox != null)
            {
                _closeIconBox.IconColor = _isCloseHover ? AppColors.Error : AppColors.TextMuted;
            }
        }

        private void UpdateDialogAppearance()
        {
            if (!_isAppearanceInitialized) return;

            Color accent = DialogTypeMetadata.GetAccentColor(_iconType);
            IconChar iconChar = DialogTypeMetadata.GetIcon(_iconType);

            // Cập nhật title mặc định nếu chưa đặt
            if (string.IsNullOrWhiteSpace(_titleLabel.Text))
                _titleLabel.Text = DialogTypeMetadata.GetDefaultTitle(_iconType);

            // 🆕 Cập nhật icon header (dùng IconPictureBox properties)
            _headerIconBox.IconChar = iconChar;
            _headerIconBox.IconColor = accent;

            // 🆕 Cập nhật icon body
            _bodyIconBox.IconChar = iconChar;
            _bodyIconBox.IconColor = accent;

            // Đổi màu nền icon panel
            _iconPanel.Invalidate();

            // Cập nhật accent color cho các nút primary
            foreach (var btn in _dialogButtons.Where(b => b.IsPrimary))
            {
                btn.CustomAccentColor = accent;
                btn.Invalidate();
            }

            Invalidate(true);
        }

        private void RebuildButtons()
        {
            // Xóa các nút cũ
            foreach (var btn in _dialogButtons)
            {
                btn.Click -= DialogButton_Click;
                btn.Dispose();
            }
            _dialogButtons.Clear();
            _footerPanel.Controls.Clear();

            var buttonInfos = DialogButtonsFactory.CreateButtons(_buttons, _iconType);
            Color accent = DialogTypeMetadata.GetAccentColor(_iconType);

            int y = (FooterHeight - ButtonHeight) / 2 + 12; // +12 cho separator ở trên

            // Tính toán vị trí X bắt đầu từ bên phải
            int totalButtonsWidth = buttonInfos.Count * ButtonWidth + (buttonInfos.Count - 1) * ButtonSpacing;
            int startX = _footerPanel.ClientSize.Width - ButtonRightMargin - totalButtonsWidth;
            if (startX < ButtonRightMargin) startX = ButtonRightMargin;

            int currentX = startX;

            foreach (var info in buttonInfos)
            {
                var button = new PrimaryButton
                {
                    Text = info.Text,
                    DialogResult = info.Result,
                    IsPrimary = info.IsPrimary,
                    Size = new Size(ButtonWidth, ButtonHeight),
                    Location = new Point(currentX, y),
                    CornerRadius = AppRadius.Medium,
                    Font = AppFonts.Button,
                    TabStop = true
                };

                if (info.IsPrimary)
                    button.CustomAccentColor = accent;

                button.Click += DialogButton_Click;
                _dialogButtons.Add(button);
                _footerPanel.Controls.Add(button);

                currentX += (ButtonWidth + ButtonSpacing);
            }

            UpdateDefaultButtonFocus();
        }

        private void UpdateDefaultButtonFocus()
        {
            var defaultBtn = _dialogButtons.FirstOrDefault(b => b.DialogResult == _defaultButton);
            if (defaultBtn != null && Visible)
            {
                defaultBtn.Select();
            }
        }

        private void AdjustSizeToContent()
        {
            if (_messageLabel == null || !_isAppearanceInitialized) return;

            using (var g = CreateGraphics())
            {
                var proposedSize = new Size(MaxWidth - 150, int.MaxValue);
                var textSize = TextRenderer.MeasureText(g, string.IsNullOrEmpty(_message) ? " " : _message,
                    _messageLabel.Font, proposedSize,
                    TextFormatFlags.WordBreak | TextFormatFlags.Left | TextFormatFlags.TextBoxControl);

                int contentWidth = Math.Max(MinWidth, textSize.Width + 170); // 110 icon area + 60 padding
                contentWidth = Math.Min(contentWidth, MaxWidth);

                int bodyHeight = Math.Max(IconSize + 40, textSize.Height + 40);
                int totalHeight = HeaderHeight + bodyHeight + FooterHeight;
                totalHeight = Math.Max(totalHeight, MinHeight);

                // Cập nhật kích thước message label
                _messageLabel.Width = contentWidth - 150;
                _messageLabel.Height = textSize.Height;

                Size = new Size(contentWidth, totalHeight);
            }
        }

        private void RepositionControlsAfterResize()
        {
            // Cập nhật vị trí close button
            _closeButton.Location = new Point(
                _headerPanel.ClientSize.Width - CloseButtonSize - 12,
                (HeaderHeight - CloseButtonSize) / 2);

            // Cập nhật vị trí các nút trong footer
            if (_dialogButtons.Count > 0)
            {
                int totalButtonsWidth = _dialogButtons.Count * ButtonWidth + (_dialogButtons.Count - 1) * ButtonSpacing;
                int startX = _footerPanel.ClientSize.Width - ButtonRightMargin - totalButtonsWidth;
                if (startX < ButtonRightMargin) startX = ButtonRightMargin;

                int currentX = startX;
                foreach (var btn in _dialogButtons)
                {
                    btn.Location = new Point(currentX, btn.Location.Y);
                    currentX += (ButtonWidth + ButtonSpacing);
                }
            }

            // Cập nhật vị trí icon panel và message label để căn giữa theo chiều cao body
            if (_bodyPanel != null && _iconPanel != null)
            {
                int bodyHeight = _bodyPanel.ClientSize.Height;
                _iconPanel.Location = new Point(24, Math.Max(0, (bodyHeight - _iconPanel.Height) / 2));

                if (_messageLabel != null)
                {
                    _messageLabel.Width = _bodyPanel.ClientSize.Width - 130;
                    _messageLabel.Location = new Point(110,
                        Math.Max(20, (bodyHeight - _messageLabel.Height) / 2));
                }
            }
        }

        private void CloseWithResult(DialogResult result)
        {
            DialogResult = result;
            Close();
        }
        #endregion

        #region Event Handlers
        private void ThemeManager_ThemeChanged(object sender, EventArgs e)
        {
            ApplyThemeColors();
            UpdateDialogAppearance();
        }

        private void BaseDialog_Shown(object sender, EventArgs e)
        {
            UpdateDefaultButtonFocus();
        }

        private void BaseDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                CloseWithResult(DialogResult.Cancel);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                // Nếu focus đang ở close button, không xử lý Enter
                if (ActiveControl == _closeButton) return;

                e.Handled = true;
                var defaultBtn = _dialogButtons.FirstOrDefault(b => b.DialogResult == _defaultButton)
                               ?? _dialogButtons.FirstOrDefault(b => b.IsPrimary)
                               ?? _dialogButtons.FirstOrDefault();
                defaultBtn?.PerformClick();
            }
        }

        private void DialogButton_Click(object sender, EventArgs e)
        {
            if (sender is PrimaryButton btn)
            {
                CloseWithResult(btn.DialogResult);
            }
        }

        private void BaseDialog_Resize(object sender, EventArgs e)
        {
            RepositionControlsAfterResize();
            Invalidate();
        }

        private void HeaderPanel_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Vẽ separator nhẹ dưới header
            using (var pen = new Pen(AppColors.Border, 1f))
            {
                g.DrawLine(pen, 20, HeaderHeight - 1, _headerPanel.ClientSize.Width - 20, HeaderHeight - 1);
            }
        }

        private void FooterPanel_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Vẽ separator nhẹ trên footer
            using (var pen = new Pen(AppColors.Border, 1f))
            {
                g.DrawLine(pen, 20, 12, _footerPanel.ClientSize.Width - 20, 12);
            }
        }

        private void IconPanel_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color softBg = DialogTypeMetadata.GetSoftBgColor(_iconType);
            var rect = new Rectangle(0, 0, _iconPanel.Width, _iconPanel.Height);

            using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Large))
            using (var brush = new SolidBrush(softBg))
            {
                g.FillPath(brush, path);
            }
        }

        private void CloseButton_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var rect = new Rectangle(0, 0, CloseButtonSize, CloseButtonSize);

            // Nền hover
            if (_isCloseHover)
            {
                using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Small))
                using (var brush = new SolidBrush(AppColors.CancelHover))
                {
                    g.FillPath(brush, path);
                }
            }
        }
        #endregion
    }
}
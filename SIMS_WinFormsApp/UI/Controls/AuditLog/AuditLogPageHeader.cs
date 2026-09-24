using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;
using OverflowMenuButton = SIMS_WinFormsApp.UI.Controls.OverflowMenuButton;

namespace SIMS_WinFormsApp.UI.Controls.AuditLog
{
    /// <summary>
    /// Header trang nhật ký: icon + tiêu đề + mô tả + nút Tùy chọn.
    /// Nút được đo theo chữ và trừ khỏi bề ngang mô tả, nên không đè chữ khi cửa sổ hẹp.
    /// </summary>
    public sealed class AuditLogPageHeader : Control
    {
        private readonly IconPictureBox _icon;
        private readonly Label _title;
        private readonly Label _subtitle;
        private readonly ToolTip _tooltip;
        public OverflowMenuButton OptionsButton { get; }

        public AuditLogPageHeader()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            Height = 112;
            BackColor = AppColors.PageBg;

            _icon = new IconPictureBox
            {
                IconChar = IconChar.ClockRotateLeft,
                IconColor = AppColors.Accent,
                IconSize = 22,
                Size = new Size(28, 28),
                BackColor = Color.Transparent
            };

            _title = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI Semibold", 16f, FontStyle.Bold),
                ForeColor = AppColors.TextTitle,
                BackColor = Color.Transparent,
                AutoEllipsis = true,
                TextAlign = ContentAlignment.MiddleLeft
            };
            _subtitle = new Label
            {
                AutoSize = false,
                Font = new Font("Segoe UI", 9.5f),
                ForeColor = AppColors.TextSecondary,
                BackColor = Color.Transparent,
                AutoEllipsis = true,
                TextAlign = ContentAlignment.MiddleLeft
            };
            OptionsButton = new OverflowMenuButton { Height = 44 };
            _tooltip = new ToolTip { InitialDelay = 400, AutoPopDelay = 6000 };

            Controls.Add(_subtitle);
            Controls.Add(_title);
            Controls.Add(_icon);
            Controls.Add(OptionsButton);
            OptionsButton.BringToFront();

            Resize += (_, __) => LayoutContent();
            ThemeManager.Instance.ThemeChanged += OnThemeChanged;
            LayoutContent();
        }

        public void ApplyLocalization()
        {
            _title.Text = Lang.Get("audit.page.title");
            _subtitle.Text = Lang.Get("audit.page.subtitle");
            OptionsButton.Text = Lang.Get("audit.export.options");
            _tooltip.SetToolTip(_subtitle, _subtitle.Text);
            _tooltip.SetToolTip(_title, _title.Text);
            LayoutContent();
        }

        private void LayoutContent()
        {
            if (Width <= 0) return;
            int buttonLeft = Math.Max(160, Width - OptionsButton.Width - 22);
            OptionsButton.Location = new Point(buttonLeft, (Height - OptionsButton.Height) / 2);

            int iconBox = 46;
            int iconX = 20;
            int iconY = (Height - iconBox) / 2;
            _icon.Location = new Point(iconX + 9, iconY + 9);

            int textLeft = 78;
            int textWidth = Math.Max(80, buttonLeft - textLeft - 16);
            int titleH = TextRenderer.MeasureText("Ẵợgqy", _title.Font, Size.Empty, TextFormatFlags.NoPadding).Height + 4;
            int subtitleH = TextRenderer.MeasureText("Ẵợgqy", _subtitle.Font, Size.Empty, TextFormatFlags.NoPadding).Height + 4;
            int block = titleH + 4 + subtitleH;
            int textTop = Math.Max(12, (Height - block) / 2);
            _title.SetBounds(textLeft, textTop, textWidth, titleH);
            _subtitle.SetBounds(textLeft, textTop + titleH + 4, textWidth, subtitleH);
            Invalidate();
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.Clear(Parent != null ? Parent.BackColor : AppColors.PageBg);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Width < 16 || Height < 16) return;
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Large))
            using (var brush = new SolidBrush(AppColors.White))
            using (var pen = new Pen(AppColors.Border))
            {
                g.FillPath(brush, path);
                g.DrawPath(pen, path);
            }

            int iconBox = 46;
            var circle = new Rectangle(20, (Height - iconBox) / 2, iconBox, iconBox);
            using (var brush = new SolidBrush(AppColors.AccentBgSoft))
                g.FillEllipse(brush, circle);
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            BackColor = AppColors.PageBg;
            _title.ForeColor = AppColors.TextTitle;
            _subtitle.ForeColor = AppColors.TextSecondary;
            _icon.IconColor = AppColors.Accent;
            Invalidate(true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.Instance.ThemeChanged -= OnThemeChanged;
                _tooltip.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

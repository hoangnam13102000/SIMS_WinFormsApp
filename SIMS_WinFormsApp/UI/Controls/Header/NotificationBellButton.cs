using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    /// <summary>Nút chuông thông báo: hover hình tròn + badge số chưa đọc (pill/tròn thật).</summary>
    public class NotificationBellButton : HeaderInteractiveControl
    {
        private const int ButtonSize = 52;
        private const int GlyphSize = 26;
        private const int BadgeHeight = 20;

        private readonly IconPictureBox _icon;
        private readonly BadgeControl _badge;
        private int _count;

        public NotificationBellButton()
        {
            Size = new Size(ButtonSize, ButtonSize);

            _icon = new IconPictureBox
            {
                IconChar = IconChar.Bell,
                IconFont = IconFont.Solid,
                IconColor = LayoutColors.TextWhite,
                IconSize = GlyphSize,
                Size = new Size(GlyphSize + 6, GlyphSize + 6),
                BackColor = Color.Transparent
            };
            _badge = new BadgeControl { Visible = false };

            Controls.Add(_icon);
            Controls.Add(_badge);
            _badge.BringToFront();

            WireChild(_icon);
            WireChild(_badge);
            LayoutChildren();
        }

        public int Count
        {
            get => _count;
            set
            {
                int v = Math.Max(0, value);
                if (v == _count) return;

                _count = v;
                _badge.SetText(v > 9 ? "9+" : v.ToString());
                _badge.Visible = v > 0;
                LayoutChildren();
                Invalidate(true);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            LayoutChildren();
        }

        private void LayoutChildren()
        {
            if (_icon == null || _badge == null) return;

            _icon.Location = new Point((Width - _icon.Width) / 2, (Height - _icon.Height) / 2);
            _badge.Location = new Point(Width - _badge.Width - 2, 2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (IsHover)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(LayoutColors.HeaderIconBgHover))
                    e.Graphics.FillEllipse(brush, 0, 0, Width - 1, Height - 1);
            }
            base.OnPaint(e);
        }

        /// <summary>Badge tự vẽ: hình tròn (1 chữ số) hoặc viên thuốc ("9+") - luôn bo tròn hoàn toàn.</summary>
        private sealed class BadgeControl : Control
        {
            private readonly Font _font = new Font("Segoe UI", 8f, FontStyle.Bold);
            private string _text = string.Empty;

            public BadgeControl()
            {
                SetStyle(ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.UserPaint |
                         ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.ResizeRedraw |
                         ControlStyles.SupportsTransparentBackColor, true);
                SetStyle(ControlStyles.Selectable, false);

                BackColor = Color.Transparent;
                Size = new Size(BadgeHeight, BadgeHeight);
            }

            public void SetText(string text)
            {
                _text = text ?? string.Empty;
                Width = _text.Length > 1 ? BadgeHeight + 8 : BadgeHeight;
                Invalidate();
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                var rect = new Rectangle(0, 0, Width - 1, Height - 1);
                using (var brush = new SolidBrush(LayoutColors.RedDot))
                using (var path = AppRadius.GetRoundedPath(rect, rect.Height / 2))
                    g.FillPath(brush, path);

                TextRenderer.DrawText(g, _text, _font, rect, Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
                    TextFormatFlags.SingleLine | TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix);
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing) _font.Dispose();
                base.Dispose(disposing);
            }
        }
    }
}
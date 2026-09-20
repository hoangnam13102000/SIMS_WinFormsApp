using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    /// <summary>
    /// Nút kích hoạt (trigger) menu "Tùy chọn" (Xuất CSV/Xuất Excel/Nhập dữ liệu...) hiển thị
    /// cạnh nút "+ Thêm..." trên header các trang quản lý (BaseTable). Bản thân control này
    /// KHÔNG biết nội dung menu là gì - chỉ là 1 nút bấm dạng viền (outline) phát ra sự kiện
    /// Click bình thường; BaseTable.AttachOverflowMenu chịu trách nhiệm mở ModernDropdownMenu
    /// tương ứng khi bấm (SRP).
    /// </summary>
    public sealed class OverflowMenuButton : Control
    {
        private const int HorizontalPadding = 22;
        private const int ChevronWidth = 11;
        private const int ChevronHeight = 7;
        private const int TextChevronGap = 12;

        private bool _isHover;
        private bool _isPressed;

        public OverflowMenuButton()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);

            BackColor = Color.Transparent;
            Font = AppFonts.Button;
            Text = "Tùy chọn";
            Cursor = Cursors.Hand;
            Height = 48;
            TabStop = true;
            RecalculateWidth();

            ThemeManager.Instance.ThemeChanged += (s, e) => Invalidate(true);
        }

        protected override void OnTextChanged(EventArgs e) { base.OnTextChanged(e); RecalculateWidth(); }
        protected override void OnFontChanged(EventArgs e) { base.OnFontChanged(e); RecalculateWidth(); }

        private void RecalculateWidth()
        {
            int textWidth = TextRenderer.MeasureText(Text, Font, Size.Empty, TextFormatFlags.NoPadding).Width;
            Width = HorizontalPadding + textWidth + TextChevronGap + ChevronWidth + HorizontalPadding;
        }

        // Invalidate(true) - không chỉ Invalidate() - để đảm bảo toàn bộ vùng vẽ được yêu cầu vẽ
        // lại ngay khi hover, tránh sót lại "bóng ma" từ lần vẽ trước.
        protected override void OnMouseEnter(EventArgs e) { base.OnMouseEnter(e); _isHover = true; Invalidate(true); }
        protected override void OnMouseLeave(EventArgs e) { base.OnMouseLeave(e); _isHover = false; _isPressed = false; Invalidate(true); }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left) { _isPressed = true; Invalidate(true); }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left) { _isPressed = false; Invalidate(true); }
        }

        // Tự vẽ nền = màu nền của Parent trước khi vẽ nút, để tránh "bóng ma" (chữ/nền lần vẽ
        // trước còn sót lại) mỗi khi Invalidate() lúc hover - cùng cách HeaderSection xử lý
        // (control có ControlStyles.SupportsTransparentBackColor nhưng KHÔNG được để trống
        // OnPaintBackground, nếu không GDI+ không xóa được vùng vẽ cũ).
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            Color backgroundColor = Parent != null ? Parent.BackColor : AppColors.White;
            if (backgroundColor == Color.Transparent) backgroundColor = AppColors.White;
            pevent.Graphics.Clear(backgroundColor);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Color accent = AppColors.Accent;
            Color fill = _isPressed
                ? Color.FromArgb(36, accent.R, accent.G, accent.B)
                : _isHover
                    ? Color.FromArgb(18, accent.R, accent.G, accent.B)
                    : AppColors.White;

            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Medium))
            using (var brush = new SolidBrush(fill))
            using (var pen = new Pen(accent, 1.5f))
            {
                g.FillPath(brush, path);
                g.DrawPath(pen, path);
            }

            var textRect = new Rectangle(
                HorizontalPadding, 0,
                Width - HorizontalPadding * 2 - ChevronWidth - TextChevronGap,
                Height);

            TextRenderer.DrawText(g, Text, Font, textRect, accent,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter |
                TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);

            DrawChevron(g, accent);
        }

        /// <summary>Chevron mảnh dạng chữ V (thay cho tam giác đặc trước đây), khớp mẫu thiết kế.</summary>
        private void DrawChevron(Graphics g, Color color)
        {
            int cx = Width - HorizontalPadding - ChevronWidth / 2;
            int cy = Height / 2;

            using (var pen = new Pen(color, 2f)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round,
                LineJoin = LineJoin.Round
            })
            {
                g.DrawLines(pen, new[]
                {
                    new Point(cx - ChevronWidth / 2, cy - ChevronHeight / 2 + 1),
                    new Point(cx, cy + ChevronHeight / 2 - 1),
                    new Point(cx + ChevronWidth / 2, cy - ChevronHeight / 2 + 1)
                });
            }
        }
    }
}
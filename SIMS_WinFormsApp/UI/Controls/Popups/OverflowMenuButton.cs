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
        private const int HorizontalPadding = 14;
        private const int ChevronSize = 8;
        private const int TextChevronGap = 8;

        private bool _isHover;

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
            Height = 42;
            TabStop = true;
            RecalculateWidth();

            ThemeManager.Instance.ThemeChanged += (s, e) => Invalidate(true);
        }

        protected override void OnTextChanged(EventArgs e) { base.OnTextChanged(e); RecalculateWidth(); }
        protected override void OnFontChanged(EventArgs e) { base.OnFontChanged(e); RecalculateWidth(); }

        private void RecalculateWidth()
        {
            int textWidth = TextRenderer.MeasureText(Text, Font, Size.Empty, TextFormatFlags.NoPadding).Width;
            Width = textWidth + TextChevronGap + ChevronSize + HorizontalPadding * 2;
        }

        // Invalidate(true) - không chỉ Invalidate() - để đảm bảo toàn bộ vùng vẽ được yêu cầu vẽ
        // lại ngay khi hover, tránh sót lại "bóng ma" từ lần vẽ trước.
        protected override void OnMouseEnter(EventArgs e) { base.OnMouseEnter(e); _isHover = true; Invalidate(true); }
        protected override void OnMouseLeave(EventArgs e) { base.OnMouseLeave(e); _isHover = false; Invalidate(true); }

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

            Color accent = AppColors.Accent;
            Color bg = _isHover ? Color.FromArgb(30, accent.R, accent.G, accent.B) : AppColors.White;

            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Medium))
            using (var brush = new SolidBrush(bg))
            using (var pen = new Pen(accent, 1f))
            {
                g.FillPath(brush, path);
                g.DrawPath(pen, path);
            }

            var textRect = new Rectangle(HorizontalPadding, 0, Width - HorizontalPadding * 2 - ChevronSize - TextChevronGap, Height);
            TextRenderer.DrawText(g, Text, Font, textRect, accent,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);

            DrawChevron(g, accent);
        }

        private void DrawChevron(Graphics g, Color color)
        {
            int cx = Width - HorizontalPadding - ChevronSize / 2;
            int cy = Height / 2;
            var points = new[]
            {
                new Point(cx - ChevronSize / 2, cy - ChevronSize / 4),
                new Point(cx + ChevronSize / 2, cy - ChevronSize / 4),
                new Point(cx, cy + ChevronSize / 3)
            };
            using (var brush = new SolidBrush(color))
                g.FillPolygon(brush, points);
        }
    }
}
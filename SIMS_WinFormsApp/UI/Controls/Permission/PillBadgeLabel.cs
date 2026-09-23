using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls.Permission
{
    public sealed class PillBadgeLabel : Label
    {
        /// <summary>Màu nền của viên pill (khác ForeColor - màu chữ).</summary>
        public Color PillBackColor { get; set; }

        public PillBadgeLabel()
        {
            AutoSize = false;
            BackColor = Color.Transparent;
            TextAlign = ContentAlignment.MiddleCenter;
            UseMnemonic = false;
            // Vẫn giữ AutoEllipsis làm lưới an toàn cuối cùng - nhưng với Width được tính đúng
            // bằng MeasureWidth (đã có đệm an toàn), trường hợp thực tế cần dùng tới ellipsis
            // gần như không xảy ra.
            AutoEllipsis = true;
            PillBackColor = AppColors.BgLighter;
        }

        /// <summary>
        /// Tính độ rộng (px) đủ để hiển thị TRỌN VẸN <paramref name="text"/> trong pill này,
        /// gồm khoảng đệm 2 bên. Gọi trước khi gán Width cho badge.
        ///
        /// horizontalPadding/minWidth mặc định nới rộng hơn trước (20→28, 48→60) để có dư
        /// khoảng đệm an toàn, tránh lặp lại lỗi badge bị cắt chữ (vd "53 qu...") khi độ rộng
        /// chữ đo được và độ rộng chữ vẽ thật lệch nhau vài px giữa các máy/mức DPI.
        /// </summary>
        public static int MeasureWidth(string text, Font font, int horizontalPadding = 28, int minWidth = 60)
        {
            int textWidth = PermissionUiHelpers.MeasureTextWidth(text, font);
            return Math.Max(minWidth, textWidth + horizontalPadding);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, Width, Height);
            int radius = Math.Max(1, Math.Min(rect.Height, rect.Width) / 2);
            using (var path = AppRadius.GetRoundedPath(rect, radius))
            using (var brush = new SolidBrush(PillBackColor))
            {
                g.FillPath(brush, path);
            }

            base.OnPaint(e);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
        }
    }
}
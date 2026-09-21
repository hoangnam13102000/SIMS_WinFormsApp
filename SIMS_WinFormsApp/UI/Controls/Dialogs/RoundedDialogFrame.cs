using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    internal sealed class RoundedDialogFrame
    {
        private readonly int _cornerRadius;
        private readonly float _borderWidth;

        public RoundedDialogFrame(int cornerRadius, float borderWidth)
        {
            if (cornerRadius <= 0) throw new ArgumentOutOfRangeException(nameof(cornerRadius));
            if (borderWidth <= 0f) throw new ArgumentOutOfRangeException(nameof(borderWidth));

            _cornerRadius = cornerRadius;
            _borderWidth = borderWidth;
        }

        /// <summary>Cắt form theo đúng hình bo góc, để không control con nào hiện ra ngoài đường cong.</summary>
        public void ApplyClip(Form form)
        {
            if (form == null) throw new ArgumentNullException(nameof(form));
            if (form.Width <= 0 || form.Height <= 0) return;

            AppRadius.ApplyRoundedCorners(form, _cornerRadius);
        }

        /// <summary>Vẽ nét viền bo góc quanh toàn bộ <paramref name="size"/>.</summary>
        public void PaintBorder(Graphics g, Size size, Color borderColor)
        {
            if (g == null) throw new ArgumentNullException(nameof(g));
            if (size.Width <= 0 || size.Height <= 0) return;

            // Nét vẽ có tâm nằm cách mép nửa độ dày viền => toàn bộ nét nằm trong Region.
            // Bán kính của đường tâm nét = bán kính Region - inset => hai cung đồng tâm.
            int inset = (int)(_borderWidth / 2f);
            var strokeBounds = new Rectangle(
                inset,
                inset,
                size.Width - inset * 2,
                size.Height - inset * 2);
            int strokeRadius = Math.Max(0, _cornerRadius - inset);

            GraphicsState state = g.Save();
            try
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (GraphicsPath path = AppRadius.GetRoundedPath(strokeBounds, strokeRadius))
                using (var pen = new Pen(borderColor, _borderWidth))
                {
                    g.DrawPath(pen, path);
                }
            }
            finally
            {
                g.Restore(state);
            }
        }
    }
}
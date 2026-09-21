using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    /// <summary>
    /// Nút đóng (X) dùng cho các dialog không viền. Tự vẽ toàn bộ (nền bo góc + dấu X bằng
    /// đường vector) nên không phụ thuộc font icon, luôn sắc nét và dễ nhìn.
    /// Chỉ làm 1 việc: hiển thị nút và báo <see cref="Control.Click"/>; việc đóng dialog
    /// do form/presenter quyết định.
    /// </summary>
    public sealed class DialogCloseButton : Control
    {
        private const int DefaultButtonSize = 40;
        private const int GlyphSize = 14;
        private const float GlyphStrokeWidth = 2.2f;

        private bool _isHover;
        private bool _isPressed;

        public DialogCloseButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Selectable, false);

            TabStop = false;
            Cursor = Cursors.Hand;
            BackColor = Color.Transparent;
            Size = new Size(DefaultButtonSize, DefaultButtonSize);

            AccessibleRole = AccessibleRole.PushButton;
            AccessibleName = "Đóng";
        }

        #region Trạng thái chuột
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHover = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHover = false;
            _isPressed = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left) return;

            _isPressed = true;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _isPressed = false;
            Invalidate();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }
        #endregion

        #region Vẽ
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            var bounds = new Rectangle(0, 0, Width - 1, Height - 1);
            using (GraphicsPath path = AppRadius.GetRoundedPath(bounds, AppRadius.Medium))
            using (var brush = new SolidBrush(ResolveBackground()))
            {
                g.FillPath(brush, path);
            }

            DrawGlyph(g, ResolveGlyphColor());
        }

        private void DrawGlyph(Graphics g, Color color)
        {
            float centerX = Width / 2f;
            float centerY = Height / 2f;
            float half = GlyphSize / 2f;

            using (var pen = new Pen(color, GlyphStrokeWidth))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;

                g.DrawLine(pen, centerX - half, centerY - half, centerX + half, centerY + half);
                g.DrawLine(pen, centerX - half, centerY + half, centerX + half, centerY - half);
            }
        }

        // Màu đọc từ AppColors tại thời điểm vẽ => tự đúng khi đổi Light/Dark.
        private Color ResolveBackground()
        {
            if (!Enabled) return AppColors.CancelBg;
            if (_isPressed) return AppColors.ErrorHover;
            if (_isHover) return AppColors.Error;
            return AppColors.CancelHover; // nền luôn hiện => người dùng thấy rõ vùng bấm
        }

        private Color ResolveGlyphColor()
        {
            if (!Enabled) return AppColors.TextDisabled;
            if (_isPressed || _isHover) return AppColors.White; // đảo màu để nổi trên nền đỏ
            return AppColors.TextSecondary;
        }
        #endregion
    }
}
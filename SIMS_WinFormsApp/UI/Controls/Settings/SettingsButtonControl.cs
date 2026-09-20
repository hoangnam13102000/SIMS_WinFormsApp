using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    public class SettingsButtonControl : UserControl
    {
        private readonly IconPictureBox _icon;
        private readonly ToolTip _toolTip;
        private bool _hover;
        private SettingsPopupControl _openPopup;

        public SettingsButtonControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            Size = new Size(72, 72);
            BackColor = Color.Transparent;
            ForeColor = Color.Transparent;
            Cursor = Cursors.Hand;

            _icon = new IconPictureBox
            {
                IconChar = IconChar.Gear,
                IconFont = IconFont.Solid,
                IconColor = Color.White,
                IconSize = 30,
                Size = new Size(36, 36),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };
            Controls.Add(_icon);
            CenterIcon();
            ApplyCircularRegion();

            _toolTip = new ToolTip();
            _toolTip.SetToolTip(this, Lang.Get("settings.tooltip"));
            _toolTip.SetToolTip(_icon, Lang.Get("settings.tooltip"));

            Resize += (_, __) =>
            {
                CenterIcon();
                ApplyCircularRegion();
            };

            Click += (_, __) => TogglePopup();
            _icon.Click += (_, __) => TogglePopup();

            MouseEnter += (_, __) => SetHover(true);
            MouseLeave += (_, __) => SetHover(false);
            _icon.MouseEnter += (_, __) => SetHover(true);
            _icon.MouseLeave += (_, __) => SetHover(ClientRectangle.Contains(PointToClient(Cursor.Position)));
        }

        private void SetHover(bool hover)
        {
            if (_hover == hover) return;
            _hover = hover;
            // true: vẽ lại cả icon con (trong suốt) để nó lấy đúng màu nền mới của nút.
            Invalidate(true);
        }

        private void CenterIcon()
        {
            _icon.Location = new Point((Width - _icon.Width) / 2, (Height - _icon.Height) / 2);
        }

        /// <summary>
        /// Cắt control thành hình tròn. Nút này nổi đè lên các control khác nên KHÔNG thể dựa vào
        /// nền trong suốt (WinForms không thấy control anh em nằm dưới) - phải bỏ hẳn phần góc vuông.
        /// </summary>
        private void ApplyCircularRegion()
        {
            if (Width <= 0 || Height <= 0) return;

            using (var path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, Width, Height);
                Region old = Region;
                Region = new Region(path);
                old?.Dispose();
            }
        }

        private void TogglePopup()
        {
            if (_openPopup != null && !_openPopup.IsDisposed)
            {
                _openPopup.Close();
                return;
            }

            var popup = new SettingsPopupControl();
            popup.FormClosed += (_, __) => _openPopup = null;
            _openPopup = popup;
            popup.ShowAbove(this, 12);
        }

        private Color CurrentColor => _hover ? AppColors.AccentHover : AppColors.Accent;

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            
            using (var brush = new SolidBrush(CurrentColor))
                pevent.Graphics.FillRectangle(brush, ClientRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (var brush = new SolidBrush(CurrentColor))
                g.FillEllipse(brush, 0, 0, Width - 1, Height - 1);

            base.OnPaint(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Region?.Dispose();
                _toolTip?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
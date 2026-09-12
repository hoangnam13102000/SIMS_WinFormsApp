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

            // Kích thước gốc là 52. Đã thử tăng gấp đôi (104) nhưng quá to, giảm lại
            // còn 72 - vẫn lớn hơn rõ rệt so với bản gốc nhưng cân đối hơn với layout.
            Size = new Size(72, 72);
            BackColor = Color.Transparent;
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
            UpdateRegion();

            _toolTip = new ToolTip();
            _toolTip.SetToolTip(this, Lang.Get("settings.tooltip"));
            _toolTip.SetToolTip(_icon, Lang.Get("settings.tooltip"));

            Resize += (_, __) => { CenterIcon(); UpdateRegion(); };

            Click += (_, __) => TogglePopup();
            _icon.Click += (_, __) => TogglePopup();

            MouseEnter += (_, __) => { _hover = true; Invalidate(); };
            MouseLeave += (_, __) => { _hover = false; Invalidate(); };
        }

        private void CenterIcon()
        {
            _icon.Location = new Point((Width - _icon.Width) / 2, (Height - _icon.Height) / 2);
        }

        // Bo tròn đúng vùng (Region) của control theo hình tròn thay vì chỉ vẽ ellipse
        // lên nền vuông: nhờ vậy phần góc vuông bên ngoài đường tròn không còn thuộc
        // vùng client của control nữa (không bị OS/GDI tô đè), nên không còn viền/khối
        // vuông lộ ra quanh nút tròn dù nó nổi (FAB) trên bất kỳ nền nào phía sau.
        private void UpdateRegion()
        {
            if (Width <= 0 || Height <= 0) return;

            using (var path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, Width - 1, Height - 1);
                Region?.Dispose();
                Region = new Region(path);
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

        protected override void OnPaintBackground(PaintEventArgs pevent) { }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color bg = _hover ? AppColors.AccentHover : AppColors.Accent;
            using (var shadow = new SolidBrush(Color.FromArgb(60, 0, 0, 0)))
                g.FillEllipse(shadow, 1, 2, Width - 2, Height - 2);
            using (var brush = new SolidBrush(bg))
                g.FillEllipse(brush, 0, 0, Width - 2, Height - 2);

            base.OnPaint(e);
        }
    }
}
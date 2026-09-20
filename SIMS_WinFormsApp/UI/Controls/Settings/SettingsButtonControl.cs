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

            _toolTip = new ToolTip();
            _toolTip.SetToolTip(this, Lang.Get("settings.tooltip"));
            _toolTip.SetToolTip(_icon, Lang.Get("settings.tooltip"));

            Resize += (_, __) => { CenterIcon(); };

            Click += (_, __) => TogglePopup();
            _icon.Click += (_, __) => TogglePopup();

            MouseEnter += (_, __) => { _hover = true; Invalidate(); };
            MouseLeave += (_, __) => { _hover = false; Invalidate(); };
        }

        private void CenterIcon()
        {
            _icon.Location = new Point((Width - _icon.Width) / 2, (Height - _icon.Height) / 2);
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

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Control này là FAB nổi trên nền trang, không được vẽ nền đen/opacity giả.
            // Chỉ vẽ hình tròn của chính nó trong OnPaint, phần nền phía sau phải thấy rõ.
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color bg = _hover ? AppColors.AccentHover : AppColors.Accent;
            using (var shadow = new SolidBrush(Color.FromArgb(38, 0, 0, 0)))
                g.FillEllipse(shadow, 2, 4, Width - 4, Height - 4);
            using (var brush = new SolidBrush(bg))
                g.FillEllipse(brush, 0, 0, Width - 2, Height - 2);

            base.OnPaint(e);
        }
    }
}
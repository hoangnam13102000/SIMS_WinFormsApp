using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    public sealed class ChatbotButtonControl : UserControl
    {
        private readonly IconPictureBox _icon;
        private readonly ToolTip _toolTip;
        private bool _hover;

        public event EventHandler Clicked;

        public ChatbotButtonControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            Size = new Size(60, 60);
            BackColor = Color.Transparent;
            ForeColor = Color.Transparent;
            Cursor = Cursors.Hand;

            _icon = new IconPictureBox
            {
                IconChar = IconChar.Robot,
                IconFont = IconFont.Solid,
                IconColor = Color.White,
                IconSize = 25,
                Size = new Size(30, 30),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };
            Controls.Add(_icon);
            CenterIcon();
            ApplyCircularRegion();

            _toolTip = new ToolTip();
            _toolTip.SetToolTip(this, Lang.Get("ai.chatButton.tooltip"));
            _toolTip.SetToolTip(_icon, Lang.Get("ai.chatButton.tooltip"));

            Resize += (sender, args) =>
            {
                CenterIcon();
                ApplyCircularRegion();
            };

            Click += (sender, args) => Clicked?.Invoke(this, EventArgs.Empty);
            _icon.Click += (sender, args) => Clicked?.Invoke(this, EventArgs.Empty);
            MouseEnter += (sender, args) => SetHover(true);
            MouseLeave += (sender, args) => SetHover(false);
            _icon.MouseEnter += (sender, args) => SetHover(true);
            _icon.MouseLeave += (sender, args) =>
                SetHover(ClientRectangle.Contains(PointToClient(Cursor.Position)));
        }

        private void SetHover(bool hover)
        {
            if (_hover == hover) return;
            _hover = hover;
            Invalidate(true);
        }

        private void CenterIcon()
        {
            _icon.Location = new Point((Width - _icon.Width) / 2, (Height - _icon.Height) / 2);
        }

        private void ApplyCircularRegion()
        {
            if (Width <= 0 || Height <= 0) return;

            using (var path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, Width, Height);
                Region oldRegion = Region;
                Region = new Region(path);
                oldRegion?.Dispose();
            }
        }

        private Color CurrentColor => _hover ? AppColors.AccentHover : AppColors.Accent;

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (var brush = new SolidBrush(CurrentColor))
                e.Graphics.FillRectangle(brush, ClientRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (var brush = new SolidBrush(CurrentColor))
                e.Graphics.FillEllipse(brush, 0, 0, Width - 1, Height - 1);
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

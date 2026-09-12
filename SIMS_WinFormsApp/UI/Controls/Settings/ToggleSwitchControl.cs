using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    public class ToggleSwitchControl : Control
    {
        private bool _checked;
        private bool _hover;

        public event EventHandler CheckedChanged;

        public bool Checked
        {
            get => _checked;
            set
            {
                if (_checked == value) return;
                _checked = value;
                Invalidate();
                CheckedChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public ToggleSwitchControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            BackColor = Color.Transparent;
            Cursor = Cursors.Hand;
            Size = new Size(44, 24);
            TabStop = false;
        }

        public void SetCheckedSilently(bool value)
        {
            if (_checked == value) return;
            _checked = value;
            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (Enabled) Checked = !Checked;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _hover = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _hover = false;
            Invalidate();
        }

        protected override void OnPaintBackground(PaintEventArgs pevent) { }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int w = Width, h = Height;
            var trackRect = new Rectangle(0, 0, w - 1, h - 1);

            Color trackColor = !Enabled
                ? AppColors.DisabledBtn
                : (_checked ? (_hover ? AppColors.AccentHover : AppColors.Accent) : AppColors.Border);

            using (var path = AppRadius.GetRoundedPath(trackRect, h / 2))
            using (var brush = new SolidBrush(trackColor))
            {
                g.FillPath(brush, path);
            }

            int thumbD = Math.Max(8, h - 6);
            int thumbX = _checked ? w - thumbD - 3 : 3;
            int thumbY = (h - thumbD) / 2;
            using (var thumbBrush = new SolidBrush(Color.White))
                g.FillEllipse(thumbBrush, thumbX, thumbY, thumbD, thumbD);
        }
    }
}
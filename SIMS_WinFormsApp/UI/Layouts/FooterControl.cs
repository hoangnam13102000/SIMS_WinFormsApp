using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Layouts
{
    public class FooterControl : UserControl
    {
        private readonly Label _statusLabel;
        private readonly Label _copyLabel;
        private readonly Panel _dot;

        public FooterControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);
            AutoScaleMode = AutoScaleMode.None;
            Font = new Font("Segoe UI", 9f);

            Height = LayoutColors.FooterHeight;
            MinimumSize = new Size(0, LayoutColors.FooterHeight);
            MaximumSize = new Size(int.MaxValue, LayoutColors.FooterHeight);
            BackColor = LayoutColors.FooterBg;
            Padding = new Padding(0);

            _dot = new Panel
            {
                Size = new Size(8, 8),
                BackColor = LayoutColors.FooterBg,
                Location = new Point(20, 14)
            };
            _dot.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var b = new SolidBrush(LayoutColors.FooterDotOnline))
                    e.Graphics.FillEllipse(b, 0, 0, 7, 7);
            };

            _statusLabel = new Label
            {
                AutoSize = true,
                Text = "Hệ thống đang hoạt động",
                Font = new Font("Segoe UI", 8.25f),
                ForeColor = LayoutColors.FooterText,
                BackColor = Color.Transparent,
                Location = new Point(36, 10)
            };

            _copyLabel = new Label
            {
                AutoSize = true,
                Text = $"© {DateTime.Now.Year} SIMS — Sales & Inventory Management System",
                Font = new Font("Segoe UI", 8.25f),
                ForeColor = LayoutColors.FooterText,
                BackColor = Color.Transparent,
                Anchor = AnchorStyles.Right | AnchorStyles.Top,
                Location = new Point(0, 10)
            };

            Controls.Add(_dot);
            Controls.Add(_statusLabel);
            Controls.Add(_copyLabel);
        }

        public void SetStatus(string text, bool online = true)
        {
            _statusLabel.Text = text ?? string.Empty;
            _dot.Tag = online;
            _dot.Invalidate();
            PerformLayout();
        }

        public void SetCopyright(string text)
        {
            _copyLabel.Text = text ?? string.Empty;
            PerformLayout();
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);

            if (Height <= 0 || _dot == null || _statusLabel == null || _copyLabel == null)
                return;

            int centerY = Height / 2;
            _dot.Location = new Point(20, centerY - 4);
            _statusLabel.Location = new Point(36, centerY - (_statusLabel.Height / 2));
            _copyLabel.Location = new Point(Width - _copyLabel.Width - 20, centerY - (_copyLabel.Height / 2));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var pen = new Pen(LayoutColors.FooterBorder))
                e.Graphics.DrawLine(pen, 0, 0, Width, 0);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            PerformLayout();
        }
    }
}
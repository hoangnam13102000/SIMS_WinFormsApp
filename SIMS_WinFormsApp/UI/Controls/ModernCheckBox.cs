using SIMS_WinFormsApp.UI.Theme;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Controls
{
    public class ModernCheckBox : Control
    {
        private const int BoxSize = 18;
        private bool _checked;

        public event EventHandler CheckedChanged;

        public bool Checked
        {
            get { return _checked; }
            set
            {
                if (_checked == value) return;
                _checked = value;
                Invalidate();
                CheckedChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public ModernCheckBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                      ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            BackColor = AppColors.White;
            Font = AppFonts.Small;
            ForeColor = AppColors.TextMuted;
            Cursor = Cursors.Hand;
            AutoSize = true;
            TabStop = false;
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            Size textSize = TextRenderer.MeasureText(Text ?? string.Empty, Font);
            return new Size(BoxSize + 8 + textSize.Width, Math.Max(BoxSize + 4, textSize.Height + 4));
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left)
                Checked = !Checked;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Color backgroundColor = AppColors.White;
            g.Clear(backgroundColor);

            int y = (Height - BoxSize) / 2;
            var boxRect = new Rectangle(0, y, BoxSize, BoxSize);

            using (var path = AppRadius.GetRoundedPath(boxRect, 5))
            {
                if (_checked)
                {
                    using (var brush = new SolidBrush(AppColors.Accent))
                        g.FillPath(brush, path);
                }
                else
                {
                    using (var brush = new SolidBrush(AppColors.White))
                        g.FillPath(brush, path);
                    using (var pen = new Pen(AppColors.FieldBorder, 1.4f))
                        g.DrawPath(pen, path);
                }
            }

            if (_checked)
            {
                using (var pen = new Pen(Color.White, 2f) { StartCap = LineCap.Round, EndCap = LineCap.Round, LineJoin = LineJoin.Round })
                {
                    var p1 = new Point(boxRect.X + 4, boxRect.Y + 9);
                    var p2 = new Point(boxRect.X + 7, boxRect.Y + 13);
                    var p3 = new Point(boxRect.X + 14, boxRect.Y + 5);
                    g.DrawLines(pen, new[] { p1, p2, p3 });
                }
            }

            var textRect = new Rectangle(BoxSize + 8, 0, Math.Max(0, Width - BoxSize - 8), Height);
            TextRenderer.DrawText(g, Text, Font, textRect, ForeColor, backgroundColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.NoPadding);
        }
    }
}
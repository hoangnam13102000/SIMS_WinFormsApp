using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls.AuditLog
{
    /// <summary>
    /// Công tắc 2 tab dạng viên thuốc. Bề ngang luôn đo theo chữ để nhãn không bị cắt.
    /// </summary>
    public sealed class SegmentedTabControl : Control
    {
        private readonly string[] _items = { string.Empty, string.Empty };
        private int _selectedIndex;
        private int _hoverIndex = -1;

        public event EventHandler SelectedIndexChanged;

        public SegmentedTabControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            Cursor = Cursors.Hand;
            Height = 40;
            Font = AppFonts.SmallBold;
            BackColor = Color.Transparent;
            ThemeManager.Instance.ThemeChanged += OnThemeInvalidate;
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (value < 0 || value > 1 || value == _selectedIndex) return;
                _selectedIndex = value;
                Invalidate();
                SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public void SetItems(string left, string right)
        {
            _items[0] = left ?? string.Empty;
            _items[1] = right ?? string.Empty;
            Size = new Size(MeasureWidth(), 40);
            Invalidate();
        }

        public int PreferredWidth => MeasureWidth();

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            int hover = HitTest(e.Location);
            if (hover == _hoverIndex) return;
            _hoverIndex = hover;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _hoverIndex = -1;
            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            int hit = HitTest(e.Location);
            if (hit >= 0) SelectedIndex = hit;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Width < 8 || Height < 8) return;
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var track = new Rectangle(0, 0, Math.Max(1, Width - 1), Math.Max(1, Height - 1));
            using (var path = AppRadius.GetRoundedPath(track, AppRadius.Medium))
            using (var brush = new SolidBrush(AppColors.BgLighter))
            using (var pen = new Pen(AppColors.Border))
            {
                g.FillPath(brush, path);
                g.DrawPath(pen, path);
            }

            int split = SegmentWidth(0);
            if (_hoverIndex >= 0 && _hoverIndex != _selectedIndex)
                FillSegment(g, _hoverIndex, split, AppColors.AccentBgSoft);

            FillSegment(g, _selectedIndex, split, AppColors.Accent);

            DrawLabel(g, 0, split, _selectedIndex == 0 ? Color.White : AppColors.TextSecondary);
            DrawLabel(g, 1, split, _selectedIndex == 1 ? Color.White : AppColors.TextSecondary);
        }

        private void FillSegment(Graphics g, int index, int split, Color color)
        {
            var rect = index == 0
                ? new Rectangle(3, 3, Math.Max(1, split - 4), Height - 7)
                : new Rectangle(split + 1, 3, Math.Max(1, Width - split - 5), Height - 7);
            using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Small + 2))
            using (var brush = new SolidBrush(color))
                g.FillPath(brush, path);
        }

        private void DrawLabel(Graphics g, int index, int split, Color color)
        {
            var rect = index == 0
                ? new Rectangle(0, 0, split, Height)
                : new Rectangle(split, 0, Width - split, Height);
            TextRenderer.DrawText(g, _items[index], Font, rect, color,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
                TextFormatFlags.SingleLine | TextFormatFlags.NoPadding);
        }

        private int HitTest(Point point)
        {
            if (!ClientRectangle.Contains(point)) return -1;
            return point.X < SegmentWidth(0) ? 0 : 1;
        }

        private int SegmentWidth(int index)
        {
            int left = TextWidth(_items[0]) + 36;
            int right = TextWidth(_items[1]) + 36;
            int total = Math.Max(Width, left + right);
            if (total <= 0) return 0;
            return index == 0 ? (int)Math.Round(total * (left / (double)(left + right))) : total - (int)Math.Round(total * (left / (double)(left + right)));
        }

        private int MeasureWidth()
        {
            return Math.Max(220, TextWidth(_items[0]) + TextWidth(_items[1]) + 72);
        }

        private static int TextWidth(string text)
        {
            return TextRenderer.MeasureText(text ?? string.Empty, AppFonts.SmallBold, Size.Empty,
                TextFormatFlags.NoPadding | TextFormatFlags.SingleLine).Width;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                ThemeManager.Instance.ThemeChanged -= OnThemeInvalidate;
            base.Dispose(disposing);
        }

        private void OnThemeInvalidate(object sender, EventArgs e) => Invalidate();
    }
}

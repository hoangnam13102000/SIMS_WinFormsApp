using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using SIMS_WinFormsApp.Models.DTOs.Dashboard;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls.Dashboard
{
    public sealed class RevenueTrendChart : Control
    {
        private IReadOnlyList<DashboardRevenuePointDto> _points =
            new List<DashboardRevenuePointDto>();
        private string _errorMessage;

        public RevenueTrendChart()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);
            BackColor = AppColors.White;
            MinimumSize = new Size(280, 220);
            ThemeManager.Instance.ThemeChanged += OnThemeChanged;
        }

        public void SetData(IReadOnlyList<DashboardRevenuePointDto> points)
        {
            _points = points ?? new List<DashboardRevenuePointDto>();
            _errorMessage = null;
            Invalidate();
        }

        public void SetError(string message)
        {
            _errorMessage = message;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            if (!string.IsNullOrEmpty(_errorMessage))
            {
                DrawCenteredMessage(graphics, "Không thể tải biểu đồ doanh thu.", AppColors.Error);
                return;
            }

            if (_points.Count == 0)
            {
                DrawCenteredMessage(graphics, "Chưa có dữ liệu doanh thu trong 7 ngày gần đây.", AppColors.TextMuted);
                return;
            }

            var values = _points.Select(point => Math.Max(0m, point.Revenue)).ToArray();
            decimal maximum = NiceMaximum(values.Max());
            var plot = new RectangleF(82, 12, Math.Max(1, Width - 102), Math.Max(1, Height - 58));
            var gridColor = AppColors.Border;
            var labelColor = AppColors.TextMuted;
            using (var gridPen = new Pen(gridColor, 1f))
            using (var labelFont = new Font("Segoe UI", 8.5f))
            using (var labelBrush = new SolidBrush(labelColor))
            using (var linePen = new Pen(AppColors.Accent, 3f))
            {
                linePen.LineJoin = LineJoin.Round;
                linePen.StartCap = LineCap.Round;
                linePen.EndCap = LineCap.Round;

                for (int tick = 0; tick <= 4; tick++)
                {
                    float y = plot.Top + plot.Height * tick / 4f;
                    graphics.DrawLine(gridPen, plot.Left, y, plot.Right, y);
                    decimal value = maximum * (4 - tick) / 4m;
                    string label = FormatAxisValue(value);
                    var labelSize = graphics.MeasureString(label, labelFont);
                    graphics.DrawString(label, labelFont, labelBrush,
                        plot.Left - labelSize.Width - 10, y - labelSize.Height / 2f);
                }

                var points = new PointF[_points.Count];
                for (int i = 0; i < _points.Count; i++)
                {
                    float x = _points.Count == 1
                        ? plot.Left + plot.Width / 2f
                        : plot.Left + plot.Width * i / (_points.Count - 1f);
                    float y = plot.Bottom - (float)(values[i] / maximum) * plot.Height;
                    points[i] = new PointF(x, y);

                    string dateLabel = _points[i].Date.ToString("dd/MM");
                    var dateSize = graphics.MeasureString(dateLabel, labelFont);
                    graphics.DrawString(dateLabel, labelFont, labelBrush,
                        x - dateSize.Width / 2f, plot.Bottom + 10);
                }

                if (points.Length > 1)
                {
                    using (var area = new GraphicsPath())
                    {
                        area.AddCurve(points, 0.22f);
                        area.AddLine(points[points.Length - 1].X, points[points.Length - 1].Y,
                            points[points.Length - 1].X, plot.Bottom);
                        area.AddLine(points[points.Length - 1].X, plot.Bottom,
                            points[0].X, plot.Bottom);
                        area.CloseFigure();

                        using (var fill = new LinearGradientBrush(
                            new RectangleF(plot.Left, plot.Top, plot.Width, plot.Height),
                            Color.FromArgb(76, AppColors.Accent),
                            Color.FromArgb(4, AppColors.Accent),
                            LinearGradientMode.Vertical))
                            graphics.FillPath(fill, area);
                    }
                    graphics.DrawCurve(linePen, points, 0.22f);
                }
                else
                {
                    graphics.DrawEllipse(linePen,
                        points[0].X - 4, points[0].Y - 4, 8, 8);
                }

                using (var dotBrush = new SolidBrush(AppColors.Accent))
                using (var dotPen = new Pen(AppColors.White, 2f))
                {
                    foreach (var point in points)
                    {
                        graphics.FillEllipse(dotBrush, point.X - 4, point.Y - 4, 8, 8);
                        graphics.DrawEllipse(dotPen, point.X - 4, point.Y - 4, 8, 8);
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                ThemeManager.Instance.ThemeChanged -= OnThemeChanged;
            base.Dispose(disposing);
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            BackColor = AppColors.White;
            Invalidate();
        }

        private void DrawCenteredMessage(Graphics graphics, string message, Color color)
        {
            using (var font = new Font("Segoe UI", 9.5f))
            using (var brush = new SolidBrush(color))
            {
                var size = graphics.MeasureString(message, font);
                graphics.DrawString(message, font, brush,
                    Math.Max(0, (Width - size.Width) / 2f),
                    Math.Max(0, (Height - size.Height) / 2f));
            }
        }

        private static decimal NiceMaximum(decimal value)
        {
            if (value <= 0) return 1m;
            decimal roughStep = value / 4m;
            decimal magnitude = (decimal)Math.Pow(10, Math.Floor(Math.Log10((double)roughStep)));
            decimal normalizedStep = roughStep / magnitude;
            decimal step = normalizedStep <= 1m ? 1m
                : normalizedStep <= 2m ? 2m
                : normalizedStep <= 5m ? 5m
                : 10m;
            return step * magnitude * 4m;
        }

        private static string FormatAxisValue(decimal value)
        {
            if (value >= 1000000000m) return (value / 1000000000m).ToString("0.#") + " tỷ";
            if (value >= 1000000m) return (value / 1000000m).ToString("0.#") + " tr";
            if (value >= 1000m) return (value / 1000m).ToString("0.#") + "k";
            return value.ToString("0");
        }
    }
}

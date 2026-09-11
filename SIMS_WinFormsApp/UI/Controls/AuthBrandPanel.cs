using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{

    public class AuthBrandPanel : Panel
    {
        private string _brandName = "SIMS";
        private string _tagline = "";
        private string[] _features = new string[0];
        private string _footerText = "";
        private Image _logo;
        private int _contentTop = -1;

        public AuthBrandPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                      ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;
            Dock = DockStyle.Fill;
        }

        public string BrandName
        {
            get => _brandName;
            set { _brandName = value ?? ""; Invalidate(); }
        }

        public string Tagline
        {
            get => _tagline;
            set { _tagline = value ?? ""; Invalidate(); }
        }

        public string[] Features
        {
            get => _features;
            set { _features = value ?? new string[0]; Invalidate(); }
        }

        public string FooterText
        {
            get => _footerText;
            set { _footerText = value ?? ""; Invalidate(); }
        }

        public Image Logo
        {
            get => _logo;
            set { _logo = value; Invalidate(); }
        }

        public int ContentTop
        {
            get => _contentTop;
            set { _contentTop = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            var bounds = new Rectangle(0, 0, Width, Height);

       
            using (var brush = new LinearGradientBrush(bounds, AppColors.DarkTop, AppColors.DarkBottom, 55f))
            {
                g.FillRectangle(brush, bounds);
            }

            DrawDecorCircle(g, Width - 60, 40, 260, 28);
            DrawDecorCircle(g, -80, Height - 120, 320, 22);

            int paddingX = 56;
            int cursorY = _contentTop >= 0 ? _contentTop : (int)(Height * 0.20);

            var logoRect = new Rectangle(paddingX, cursorY, 64, 64);
            using (var logoPath = AppRadius.GetRoundedPath(logoRect, 18))
            using (var logoBrush = new SolidBrush(Color.White))
            {
                g.FillPath(logoBrush, logoPath);

                if (_logo != null)
                {
                    var oldClip = g.Clip;
                    g.SetClip(logoPath, CombineMode.Intersect);

                    int inset = 10;
                    var innerRect = new Rectangle(
                        logoRect.X + inset, logoRect.Y + inset,
                        logoRect.Width - inset * 2, logoRect.Height - inset * 2);
                    g.DrawImage(_logo, innerRect);

                    g.Clip = oldClip;
                }
            }

            if (_logo == null)
            {
                DrawCheckGlyph(g, logoRect, AppColors.DarkTop);
            }

            cursorY += 64 + 24;

            using (var titleBrush = new SolidBrush(Color.White))
            {
                // The embedded Inter font has a taller ascent than Segoe UI.
                // Keep extra vertical bounds so the top and bottom of the brand
                // name remain visible after the form is recreated.
                var titleRect = new Rectangle(paddingX, cursorY, Width - paddingX * 2, 60);
                g.DrawString(_brandName, AppFonts.Brand, titleBrush, titleRect);
            }
            cursorY += 66;

        
            if (!string.IsNullOrEmpty(_tagline))
            {
                using (var taglineBrush = new SolidBrush(AppColors.DarkTextMuted))
                {
                    int textWidth = System.Math.Max(80, Width - paddingX * 2 - 16);
                    var taglineRect = new Rectangle(paddingX, cursorY, textWidth, 120);
                    g.DrawString(_tagline, AppFonts.Body, taglineBrush, taglineRect);
                    cursorY += MeasureHeight(g, _tagline, AppFonts.Body, taglineRect.Width) + 28;
                }
            }

            foreach (var feature in _features)
            {
                int featureWidth = System.Math.Max(80, Width - paddingX * 2);
                int rowHeight = DrawFeatureRow(g, paddingX, cursorY, featureWidth, feature);
                cursorY += rowHeight + 10;
            }

    
            if (!string.IsNullOrEmpty(_footerText))
            {
                using (var footerBrush = new SolidBrush(AppColors.DarkFooter))
                {
                    var footerRect = new Rectangle(paddingX, Height - 46, Width - paddingX * 2, 24);
                    g.DrawString(_footerText, AppFonts.Small, footerBrush, footerRect);
                }
            }
        }

        private static void DrawDecorCircle(Graphics g, int centerX, int centerY, int diameter, int alpha)
        {
            var rect = new Rectangle(centerX - diameter / 2, centerY - diameter / 2, diameter, diameter);
            using (var brush = new SolidBrush(Color.FromArgb(alpha, Color.White)))
            {
                g.FillEllipse(brush, rect);
            }
        }

        private static void DrawCheckGlyph(Graphics g, Rectangle logoRect, Color color)
        {
            using (var pen = new Pen(color, 4f) { StartCap = LineCap.Round, EndCap = LineCap.Round, LineJoin = LineJoin.Round })
            {
                int cx = logoRect.X, cy = logoRect.Y;
                var p1 = new Point(cx + 16, cy + 33);
                var p2 = new Point(cx + 27, cy + 44);
                var p3 = new Point(cx + 48, cy + 20);
                g.DrawLines(pen, new[] { p1, p2, p3 });
            }
        }

        private int DrawFeatureRow(Graphics g, int x, int y, int width, string text)
        {
            const int iconSize = 22;
            var iconRect = new Rectangle(x, y, iconSize, iconSize);

            using (var circleBrush = new SolidBrush(Color.FromArgb(40, Color.White)))
            {
                g.FillEllipse(circleBrush, iconRect);
            }
            using (var pen = new Pen(Color.White, 2f) { StartCap = LineCap.Round, EndCap = LineCap.Round, LineJoin = LineJoin.Round })
            {
                var p1 = new Point(x + 6, y + 11);
                var p2 = new Point(x + 9, y + 15);
                var p3 = new Point(x + 16, y + 7);
                g.DrawLines(pen, new[] { p1, p2, p3 });
            }

            using (var textBrush = new SolidBrush(AppColors.DarkFeatureText))
            {
                int textWidth = System.Math.Max(40, width - iconSize - 12);
                int textHeight = MeasureHeight(g, text, AppFonts.Feature, textWidth);
                var textRect = new Rectangle(x + iconSize + 12, y - 2, textWidth, textHeight + 6);
                g.DrawString(text, AppFonts.Feature, textBrush, textRect);
                return System.Math.Max(iconSize, textHeight);
            }
        }

        private static int MeasureHeight(Graphics g, string text, Font font, int width)
        {
            var size = g.MeasureString(text, font, width);
            return (int)System.Math.Ceiling(size.Height);
        }

        public void SetFeatures(IEnumerable<string> features)
        {
            var list = new List<string>(features);
            Features = list.ToArray();
        }
    }
}
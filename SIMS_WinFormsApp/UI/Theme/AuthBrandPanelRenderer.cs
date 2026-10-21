using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Theme
{
    internal sealed class AuthBrandPanelRenderer
    {
        private const int FooterBottomMargin = 22;
        private readonly Panel _panel;
        private string _brandName = "SIMS";
        private string _tagline = "";
        private string[] _features = new string[0];
        private string _footerText = "";
        private Image _logo;
        private int _contentTop = -1;

        public AuthBrandPanelRenderer(Panel panel)
        {
            _panel = panel ?? throw new ArgumentNullException(nameof(panel));
            _panel.Paint += Panel_Paint;
        }

        public string BrandName
        {
            get => _brandName;
            set { _brandName = value ?? ""; _panel.Invalidate(); }
        }

        public string Tagline
        {
            get => _tagline;
            set { _tagline = value ?? ""; _panel.Invalidate(); }
        }

        public string[] Features
        {
            get => _features;
            set { _features = value ?? new string[0]; _panel.Invalidate(); }
        }

        public string FooterText
        {
            get => _footerText;
            set { _footerText = value ?? ""; _panel.Invalidate(); }
        }

        public Image Logo
        {
            get => _logo;
            set { _logo = value; _panel.Invalidate(); }
        }

        public int ContentTop
        {
            get => _contentTop;
            set { _contentTop = value; _panel.Invalidate(); }
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            Render(e.Graphics, _panel.ClientSize);
        }

        private void Render(Graphics g, Size clientSize)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            int width = clientSize.Width;
            int height = clientSize.Height;
            var bounds = new Rectangle(0, 0, width, height);
            using (var brush = new LinearGradientBrush(bounds, AppColors.DarkTop, AppColors.DarkBottom, 55f))
                g.FillRectangle(brush, bounds);

            DrawDecorCircle(g, width - 60, 40, 260, 28);
            DrawDecorCircle(g, -80, height - 120, 320, 22);

            const int paddingX = 56;
            int cursorY = _contentTop >= 0 ? _contentTop : (int)(height * 0.20);
            var logoRect = new Rectangle(paddingX, cursorY, 64, 64);
            if (_logo != null)
                g.DrawImage(_logo, logoRect);
            else
                DrawCheckGlyph(g, logoRect, AppColors.DarkTop);

            cursorY += 64 + 24;
            using (var titleBrush = new SolidBrush(Color.White))
            {
                var titleRect = new Rectangle(paddingX, cursorY, width - paddingX * 2, 90);
                using (var format = new StringFormat(StringFormatFlags.NoClip | StringFormatFlags.NoWrap))
                {
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Near;
                    g.DrawString(_brandName, AppFonts.Brand, titleBrush, titleRect, format);
                }
            }
            cursorY += 90;

            if (!string.IsNullOrEmpty(_tagline))
            {
                using (var taglineBrush = new SolidBrush(AppColors.DarkTextMuted))
                {
                    int textWidth = Math.Max(80, width - paddingX * 2 - 16);
                    var taglineRect = new Rectangle(paddingX, cursorY, textWidth, 130);
                    using (var format = new StringFormat(StringFormatFlags.NoClip | StringFormatFlags.NoWrap))
                    {
                        format.LineAlignment = StringAlignment.Near;
                        format.Alignment = StringAlignment.Near;
                        g.DrawString(_tagline, AppFonts.Body, taglineBrush, taglineRect, format);
                    }
                    cursorY += MeasureHeight(g, _tagline, AppFonts.Body, taglineRect.Width) + 32;
                }
            }

            foreach (string feature in _features)
            {
                int featureWidth = Math.Max(80, width - paddingX * 2);
                cursorY += DrawFeatureRow(g, paddingX, cursorY, featureWidth, feature) + 10;
            }

            DrawFooter(g, paddingX, width, height);
        }

        private void DrawFooter(Graphics g, int paddingX, int width, int height)
        {
            if (string.IsNullOrEmpty(_footerText)) return;

            int footerWidth = Math.Max(80, width - paddingX * 2);
            using (var footerBrush = new SolidBrush(AppColors.DarkFooter))
            using (var format = new StringFormat(StringFormatFlags.NoClip))
            {
                int footerHeight = (int)Math.Ceiling(
                    g.MeasureString(_footerText, AppFonts.Small, footerWidth, format).Height);
                var footerRect = new Rectangle(
                    paddingX,
                    height - FooterBottomMargin - footerHeight,
                    footerWidth,
                    footerHeight);
                g.DrawString(_footerText, AppFonts.Small, footerBrush, footerRect, format);
            }
        }

        private static void DrawDecorCircle(Graphics g, int centerX, int centerY, int diameter, int alpha)
        {
            var rect = new Rectangle(centerX - diameter / 2, centerY - diameter / 2, diameter, diameter);
            using (var brush = new SolidBrush(Color.FromArgb(alpha, Color.White)))
                g.FillEllipse(brush, rect);
        }

        private static void DrawCheckGlyph(Graphics g, Rectangle logoRect, Color color)
        {
            using (var pen = new Pen(color, 4f)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round,
                LineJoin = LineJoin.Round
            })
            {
                int x = logoRect.X;
                int y = logoRect.Y;
                g.DrawLines(pen, new[]
                {
                    new Point(x + 16, y + 33),
                    new Point(x + 27, y + 44),
                    new Point(x + 48, y + 20)
                });
            }
        }

        private static int DrawFeatureRow(Graphics g, int x, int y, int width, string text)
        {
            const int iconSize = 22;
            var iconRect = new Rectangle(x, y, iconSize, iconSize);
            using (var circleBrush = new SolidBrush(Color.FromArgb(40, Color.White)))
                g.FillEllipse(circleBrush, iconRect);

            using (var pen = new Pen(Color.White, 2f)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round,
                LineJoin = LineJoin.Round
            })
            {
                g.DrawLines(pen, new[]
                {
                    new Point(x + 6, y + 11),
                    new Point(x + 9, y + 15),
                    new Point(x + 16, y + 7)
                });
            }

            using (var textBrush = new SolidBrush(AppColors.DarkFeatureText))
            {
                int textWidth = Math.Max(40, width - iconSize - 12);
                int textHeight = MeasureHeight(g, text, AppFonts.Feature, textWidth);
                var textRect = new Rectangle(x + iconSize + 12, y - 2, textWidth, textHeight + 6);
                g.DrawString(text, AppFonts.Feature, textBrush, textRect);
                return Math.Max(iconSize, textHeight);
            }
        }

        private static int MeasureHeight(Graphics g, string text, Font font, int width)
        {
            return (int)Math.Ceiling(g.MeasureString(text, font, width).Height);
        }
    }
}

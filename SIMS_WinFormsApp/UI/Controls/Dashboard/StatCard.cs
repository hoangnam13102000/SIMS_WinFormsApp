using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    [ToolboxItem(false)]
    [DesignerCategory("Code")]
    public class StatCard : Control
    {
        private IconPictureBox _iconBox;
        private Panel _iconCircle;
        private Label _lblValue;
        private Label _lblTitle;
        private Label _lblTrend;

        private string _valueText = "0";
        private string _titleText = "Title";
        private string _trendText = "";
        private IconChar _icon = IconChar.ChartLine;
        private Color _iconColor = AppColors.Blue;
        private Color _iconBg = Color.FromArgb(219, 234, 254);
        private Color _trendColor = AppColors.Success;

        public string ValueText
        {
            get => _valueText;
            set
            {
                _valueText = value ?? "0";
                if (_lblValue != null)
                {
                    _lblValue.Text = _valueText;
                    FitValueFont();
                }
            }
        }

        public string TitleText
        {
            get => _titleText;
            set
            {
                _titleText = value ?? "";
                if (_lblTitle != null) _lblTitle.Text = _titleText;
            }
        }

        public string TrendText
        {
            get => _trendText;
            set
            {
                _trendText = value ?? "";
                if (_lblTrend != null) _lblTrend.Text = _trendText;
            }
        }

        public IconChar Icon
        {
            get => _icon;
            set
            {
                _icon = value;
                if (_iconBox != null) _iconBox.IconChar = _icon;
            }
        }

        public Color IconColor
        {
            get => _iconColor;
            set
            {
                _iconColor = value;
                if (_iconBox != null) _iconBox.IconColor = _iconColor;
            }
        }

        public Color IconBackground
        {
            get => _iconBg;
            set
            {
                _iconBg = value;
                _iconCircle?.Invalidate();
            }
        }

        public Color TrendColor
        {
            get => _trendColor;
            set
            {
                _trendColor = value;
                if (_lblTrend != null) _lblTrend.ForeColor = _trendColor;
            }
        }

        public StatCard()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);

            Size = new Size(260, 136);
            MinimumSize = new Size(180, 124);
            BackColor = AppColors.PageBg;
            BuildUI();
        }

        private void BuildUI()
        {
            // Icon circle
            _iconCircle = new Panel
            {
                Size = new Size(40, 40),
                Location = new Point(14, 14),
                BackColor = Color.Transparent
            };
            _iconCircle.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = new GraphicsPath())
                {
                    path.AddEllipse(0, 0, 39, 39);
                    using (var br = new SolidBrush(_iconBg))
                        e.Graphics.FillPath(br, path);
                }
            };
            Controls.Add(_iconCircle);

            _iconBox = new IconPictureBox
            {
                IconChar = _icon,
                IconColor = _iconColor,
                IconSize = 18,
                Size = new Size(22, 22),
                Location = new Point(9, 9),
                BackColor = Color.Transparent
            };
            _iconCircle.Controls.Add(_iconBox);

            var valueFont = new Font("Segoe UI Semibold", 15f, FontStyle.Bold);
            var titleFont = new Font("Segoe UI", 9f);
            var trendFont = new Font("Segoe UI", 8.5f);

            const string sample = "Ẵợgqy";
            int valueH = TextRenderer.MeasureText(sample, valueFont, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding).Height + 6;
            int titleH = TextRenderer.MeasureText(sample, titleFont, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding).Height + 6;
            int trendH = TextRenderer.MeasureText(sample, trendFont, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding).Height + 6;

            _lblValue = new Label
            {
                AutoSize = false,
                Text = _valueText,
                Font = valueFont,
                ForeColor = AppColors.TextTitle,
                Location = new Point(64, 10),
                Size = new Size(Math.Max(40, Width - 78), valueH),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent,
                AutoEllipsis = true
            };
            Controls.Add(_lblValue);

            _lblTitle = new Label
            {
                AutoSize = false,
                Text = _titleText,
                Font = titleFont,
                ForeColor = AppColors.TextSecondary,
                Location = new Point(14, _lblValue.Bottom + 8),
                Size = new Size(Math.Max(60, Width - 28), titleH),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent,
                AutoEllipsis = true
            };
            Controls.Add(_lblTitle);

            // Trend
            _lblTrend = new Label
            {
                AutoSize = false,
                Text = _trendText,
                Font = trendFont,
                ForeColor = _trendColor,
                Location = new Point(14, _lblTitle.Bottom + 6),
                Size = new Size(Math.Max(60, Width - 28), trendH),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent,
                AutoEllipsis = true
            };
            Controls.Add(_lblTrend);
        }

        private void FitValueFont()
        {
            if (_lblValue == null || string.IsNullOrEmpty(_valueText)) return;
            if (!IsHandleCreated) return;

            float size = 15f;
            const float minSize = 8f;
            try
            {
                using (var g = CreateGraphics())
                {
                    while (size >= minSize)
                    {
                        using (var f = new Font("Segoe UI Semibold", size, FontStyle.Bold))
                        {
                            if (g.MeasureString(_valueText, f).Width <= _lblValue.Width - 2)
                            {
                                _lblValue.Font = (Font)f.Clone();
                                return;
                            }
                        }
                        size -= 0.5f;
                    }
                }
            }
            catch
            {
                // design-time / no handle
            }
            _lblValue.Font = new Font("Segoe UI Semibold", minSize, FontStyle.Bold);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (_lblValue == null) return;

            const int valueLeftOffset = 64;
            const int rightPadding = 14;
            _lblValue.Width = Math.Max(40, Width - valueLeftOffset - rightPadding);

            int fullW = Math.Max(60, Width - 28);
            _lblTitle.Width = fullW;
            _lblTrend.Width = fullW;

            FitValueFont();
            Invalidate();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            FitValueFont();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            if (_lblValue != null) _lblValue.ForeColor = AppColors.TextTitle;
            if (_lblTitle != null) _lblTitle.ForeColor = AppColors.TextSecondary;

            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Large))
            using (var brush = new SolidBrush(AppColors.White))
            {
                g.FillPath(brush, path);
            }

            base.OnPaint(e);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

            pevent.Graphics.Clear(AppColors.PageBg);
        }
    }
}
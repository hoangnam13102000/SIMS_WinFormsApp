using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SIMS_WinFormsApp.MVP.ViewModels;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{

    public class HeaderAccountButton : HeaderInteractiveControl
    {
        private const int ButtonHeight = 72;
        private const int AvatarSize = 56;
        private const int PadLeft = 8;
        private const int TextGap = 14;
        private const int ChevronArea = 34;

        private const TextFormatFlags MeasureFlags =
            TextFormatFlags.NoPadding | TextFormatFlags.SingleLine;

        private const TextFormatFlags DrawFlags =
            TextFormatFlags.NoPadding | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis |
            TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.NoPrefix;

        private readonly AvatarControl _avatar;
        private readonly Font _nameFont = new Font("Segoe UI Semibold", 12f, FontStyle.Bold);
        private readonly Font _emailFont = new Font("Segoe UI", 9.5f);

        private string _name = string.Empty;
        private string _email = string.Empty;
        private bool _showEmail = true;
        private int _maxTextWidth = 280;

        public HeaderAccountButton()
        {
            _avatar = new AvatarControl
            {
                Size = new Size(AvatarSize, AvatarSize),
                RingWidth = 2,
                RingColor = Color.FromArgb(96, 255, 255, 255)
            };
            Controls.Add(_avatar);
            WireChild(_avatar);

            Height = ButtonHeight;
            RecalculateSize();
        }

        public bool ShowEmail
        {
            get => _showEmail;
            set
            {
                if (_showEmail == value) return;
                _showEmail = value;
                RecalculateSize();
            }
        }

        /// <summary>Chiều rộng tối đa của khối chữ (vượt quá sẽ hiện dấu "...").</summary>
        public int MaxTextWidth
        {
            get => _maxTextWidth;
            set
            {
                int v = Math.Max(80, value);
                if (_maxTextWidth == v) return;
                _maxTextWidth = v;
                RecalculateSize();
            }
        }

        public void Bind(HeaderUserViewModel user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            _name = user.DisplayName;
            _email = user.Email;
            _avatar.Initial = user.AvatarInitial;
            _avatar.TryLoadImage(user.AvatarPath);
            RecalculateSize();
        }

        private void RecalculateSize()
        {
            int nameW = MeasureWidth(_name, _nameFont);
            int emailW = _showEmail ? MeasureWidth(_email, _emailFont) : 0;
            int textW = Math.Min(Math.Max(nameW, emailW) + 4, _maxTextWidth);

            Width = PadLeft + AvatarSize + TextGap + textW + ChevronArea;
            Invalidate(true);
        }

        private static int MeasureWidth(string text, Font font)
            => TextRenderer.MeasureText(text ?? string.Empty, font, Size.Empty, MeasureFlags).Width;

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (_avatar == null) return;
            _avatar.Location = new Point(PadLeft, (Height - _avatar.Height) / 2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (IsHover)
            {
                using (var brush = new SolidBrush(LayoutColors.HeaderAccountHover))
                using (var path = AppRadius.GetRoundedPath(
                    new Rectangle(0, 0, Width - 1, Height - 1), AppRadius.Large))
                    g.FillPath(brush, path);
            }

            int textX = PadLeft + AvatarSize + TextGap;
            int textW = Math.Max(0, Width - textX - ChevronArea);
            int nameH = _nameFont.Height;
            int emailH = _emailFont.Height;

            bool showEmail = _showEmail && _email.Length > 0;
            int blockH = nameH + (showEmail ? 2 + emailH : 0);
            int y = (Height - blockH) / 2;

            TextRenderer.DrawText(g, _name, _nameFont,
                new Rectangle(textX, y, textW, nameH), LayoutColors.HeaderText, DrawFlags);

            if (showEmail)
                TextRenderer.DrawText(g, _email, _emailFont,
                    new Rectangle(textX, y + nameH + 2, textW, emailH), LayoutColors.HeaderSubtitle, DrawFlags);

            DrawChevron(g);
            base.OnPaint(e);
        }

        private void DrawChevron(Graphics g)
        {
            int cx = Width - 17;
            int cy = Height / 2;

            using (var pen = new Pen(IsHover ? LayoutColors.HeaderText : LayoutColors.HeaderSubtitle, 2f))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                pen.LineJoin = LineJoin.Round;
                g.DrawLines(pen, new[]
                {
                    new Point(cx - 5, cy - 2),
                    new Point(cx, cy + 3),
                    new Point(cx + 5, cy - 2)
                });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _nameFont.Dispose();
                _emailFont.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
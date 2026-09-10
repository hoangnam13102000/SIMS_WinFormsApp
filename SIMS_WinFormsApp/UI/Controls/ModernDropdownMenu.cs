using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Controls
{
    public class ModernDropdownMenu : PopupFormBase
    {
        private readonly Panel _host;
        private readonly List<Control> _ordered = new List<Control>();
        private readonly List<DropdownItemControl> _items = new List<DropdownItemControl>();

        public event EventHandler<string> ItemClicked;

        public ModernDropdownMenu()
        {
            BackColor = LayoutColors.DropdownBg;
            Padding = Padding.Empty;
            Size = new Size(280, 56);

            _host = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = LayoutColors.DropdownBg,
                Padding = new Padding(8),
                AutoScroll = false
            };
            Controls.Add(_host);
        }

        public ModernDropdownMenu AddItem(string key, string text, IconChar? icon = null, bool isDanger = false)
        {
            var item = new DropdownItemControl(key, text, icon, isDanger);
            item.Clicked += OnItemClicked;
            _items.Add(item);
            _ordered.Add(item);
            Relayout();
            return this;
        }

        public ModernDropdownMenu AddHeader(Control content)
        {
            if (content == null) return this;
            _ordered.Add(content);
            Relayout();
            return this;
        }

        public ModernDropdownMenu AddSeparator()
        {
            var sep = new Panel
            {
                Height = 9,
                BackColor = Color.Transparent
            };
            sep.Paint += (s, e) =>
            {
                using (var pen = new Pen(LayoutColors.DropdownBorder))
                    e.Graphics.DrawLine(pen, 8, 4, sep.Width - 8, 4);
            };
            _ordered.Add(sep);
            Relayout();
            return this;
        }

        private void OnItemClicked(object sender, EventArgs e)
        {
            if (sender is DropdownItemControl item)
            {
                ItemClicked?.Invoke(this, item.Key);
                Close();
            }
        }

        private void Relayout()
        {
            _host.SuspendLayout();
            _host.Controls.Clear();

            int minContentW = 240;
            foreach (var c in _ordered)
            {
                if (c is DropdownItemControl di)
                {
                    minContentW = Math.Max(minContentW, di.PreferredContentWidth + 24);
                }
                else if (c is Panel p)
                {
                    minContentW = Math.Max(minContentW, Math.Max(p.Width, p.MinimumSize.Width));
                    foreach (Control child in p.Controls)
                    {
                        if (child is Label lbl && !string.IsNullOrEmpty(lbl.Text))
                        {
                            var sz = TextRenderer.MeasureText(lbl.Text, lbl.Font,
                                Size.Empty, TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
                            minContentW = Math.Max(minContentW, lbl.Left + sz.Width + 24);
                        }
                    }
                }
            }

            Width = Math.Max(Math.Max(Width, minContentW + 20), 280);

            int y = 8;
            foreach (var c in _ordered)
            {
                c.Location = new Point(8, y);
                c.Width = Width - 16;

                if (c is DropdownItemControl)
                    c.Height = 44;

                _host.Controls.Add(c);
                y += c.Height + 4;
            }

            Height = Math.Max(56, y + 8);

            try
            {
                Region?.Dispose();
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width + 1, Height + 1, 12, 12));
            }
            catch { }

            _host.ResumeLayout(true);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var pen = new Pen(LayoutColors.DropdownBorder, 1f))
            using (var path = RoundedRect(new Rectangle(0, 0, Width - 1, Height - 1), 11))
                g.DrawPath(pen, path);
        }

        private static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse);
    }

    public class DropdownItemControl : UserControl
    {
        private readonly IconPictureBox _iconBox;
        private readonly Label _textLabel;
        private bool _hover;
        private readonly bool _isDanger;
        private readonly string _text;

        public string Key { get; }
        public event EventHandler Clicked;

        public int PreferredContentWidth
        {
            get
            {
                var sz = TextRenderer.MeasureText(_text ?? "",
                    _textLabel?.Font ?? new Font("Segoe UI", 10f),
                    Size.Empty, TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
                return 48 + sz.Width + 20;
            }
        }

        public DropdownItemControl(string key, string text, IconChar? icon = null, bool isDanger = false)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            _isDanger = isDanger;
            _text = text ?? string.Empty;

            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            Height = 44;
            Cursor = Cursors.Hand;
            BackColor = Color.Transparent;
            Padding = new Padding(0);

            Color fg = isDanger ? LayoutColors.Danger : LayoutColors.TextWhite;

            // Icon — không Dock, luôn hiện
            _iconBox = new IconPictureBox
            {
                Size = new Size(24, 24),
                Location = new Point(12, 10),
                IconColor = fg,
                IconSize = 18,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                IconChar = icon ?? IconChar.Circle
            };

            // Text — không Dock.Fill (tránh che icon)
            _textLabel = new Label
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 10f),
                ForeColor = fg,
                BackColor = Color.Transparent,
                Text = _text,
                Cursor = Cursors.Hand,
                Location = new Point(44, 0),
                Size = new Size(200, 44)
            };

            Controls.Add(_textLabel);
            Controls.Add(_iconBox);
            _iconBox.BringToFront();

            Resize += (_, __) =>
            {
                _textLabel.Size = new Size(Math.Max(40, Width - 52), Height);
                _iconBox.Location = new Point(12, Math.Max(0, (Height - _iconBox.Height) / 2));
            };

            MouseEnter += OnHover;
            MouseLeave += OnLeave;
            Click += (_, __) => Clicked?.Invoke(this, EventArgs.Empty);
            _iconBox.Click += (_, __) => Clicked?.Invoke(this, EventArgs.Empty);
            _textLabel.Click += (_, __) => Clicked?.Invoke(this, EventArgs.Empty);
            _iconBox.MouseEnter += OnHover;
            _textLabel.MouseEnter += OnHover;
            _iconBox.MouseLeave += OnLeave;
            _textLabel.MouseLeave += OnLeave;
        }

        private void OnHover(object sender, EventArgs e)
        {
            _hover = true;
            Invalidate();
        }

        private void OnLeave(object sender, EventArgs e)
        {
            if (!ClientRectangle.Contains(PointToClient(Cursor.Position)))
            {
                _hover = false;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (_hover)
            {
                using (var brush = new SolidBrush(_isDanger
                    ? Color.FromArgb(40, LayoutColors.Danger)
                    : LayoutColors.RowHover))
                using (var path = RoundedRect(new Rectangle(0, 0, Width - 1, Height - 1), 8))
                    g.FillPath(brush, path);
            }

            base.OnPaint(e);
        }

        private static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
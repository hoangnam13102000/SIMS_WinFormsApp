using System;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Layouts
{
    public class SidebarItemControl : UserControl
    {
        private const int IconLeft = 16;
        private const int IconSize = 22;
        private const int TextGap = 12;
        private const int RightPadding = 12;
        private const int BadgeWidth = 22;

        private readonly IconPictureBox _iconBox;
        private readonly Label _textLabel;
        private readonly Label _badgeLabel;
        private readonly ToolTip _toolTip;
        private readonly string _label;
        private readonly IconChar _iconChar;
        private string _pageKey;
        private bool _isActive;
        private bool _collapsed;
        private int _badgeCount;

        public event EventHandler<string> ItemClicked;

        public bool IsActive
        {
            get => _isActive;
            set { _isActive = value; UpdateVisualState(); }
        }

        public string PageKey => _pageKey;

        public SidebarItemControl(string pageKey, string label, IconChar icon)
        {
            _pageKey = pageKey;
            _label = label ?? string.Empty;
            _iconChar = icon;

            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);
            AutoScaleMode = AutoScaleMode.None;
            Height = LayoutColors.SidebarItemHeight;
            MinimumSize = new Size(0, LayoutColors.SidebarItemHeight);
            BackColor = Color.Transparent;
            Cursor = Cursors.Hand;
            Margin = new Padding(0);
            Padding = new Padding(0);

            _iconBox = new IconPictureBox
            {
                IconChar = icon,
                IconColor = LayoutColors.SidebarTextInactive,
                IconSize = 18,
                Size = new Size(IconSize, IconSize),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };

            _textLabel = new Label
            {
                Text = _label,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = LayoutColors.SidebarTextInactive,
                Font = new Font("Segoe UI", 9.5f),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                AutoEllipsis = true
            };

            _badgeLabel = new Label
            {
                Text = string.Empty,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = LayoutColors.Accent,
                Font = new Font("Segoe UI", 7.5f, FontStyle.Bold),
                Size = new Size(BadgeWidth, 20),
                Visible = false,
                Cursor = Cursors.Hand
            };
            using (var path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                path.AddArc(0, 0, 19, 19, 0, 360);
                _badgeLabel.Region = new Region(path);
            }

            Controls.Add(_iconBox);
            Controls.Add(_textLabel);
            Controls.Add(_badgeLabel);

            HookChildInteraction(this);
            HookChildInteraction(_iconBox);
            HookChildInteraction(_textLabel);
            HookChildInteraction(_badgeLabel);

            _toolTip = new ToolTip { InitialDelay = 400, ReshowDelay = 100, AutoPopDelay = 4000 };

            UpdateVisualState();
            LayoutChildren();
        }

        private void HookChildInteraction(Control c)
        {
            c.Click += (_, __) => ItemClicked?.Invoke(this, _pageKey);
            c.MouseEnter += (_, __) => EnterHover();
            c.MouseLeave += (_, __) => LeaveHoverIfOutside();
        }

        private void EnterHover()
        {
            if (_isActive) return;
            _iconBox.IconColor = LayoutColors.SidebarTextHover;
            _textLabel.ForeColor = LayoutColors.SidebarTextHover;
            BackColor = LayoutColors.SidebarItemHover;
            Invalidate();
        }

        private void LeaveHoverIfOutside()
        {
            if (ClientRectangle.Contains(PointToClient(Cursor.Position))) return;
            UpdateVisualState();
        }

        public void SetBadge(int count)
        {
            _badgeCount = Math.Max(0, count);
            if (_badgeCount <= 0)
            {
                _badgeLabel.Visible = false;
                _badgeLabel.Text = string.Empty;
            }
            else
            {
                _badgeLabel.Text = _badgeCount > 99 ? "99+" : _badgeCount.ToString();
                _badgeLabel.Visible = !_collapsed;
            }
            LayoutChildren();
            Invalidate();
        }

        public void SetCollapsed(bool collapsed)
        {
            _collapsed = collapsed;

            string tip = collapsed ? _label : string.Empty;
            _toolTip.SetToolTip(this, tip);
            _toolTip.SetToolTip(_iconBox, tip);
            _toolTip.SetToolTip(_textLabel, string.Empty);
            _toolTip.SetToolTip(_badgeLabel, string.Empty);

            _iconBox.Visible = true;
            _iconBox.IconChar = _iconChar;
            LayoutChildren();
            _iconBox.Invalidate();
            Invalidate();
        }

        private void LayoutChildren()
        {
            if (_iconBox == null || _textLabel == null || _badgeLabel == null) return;
            if (Width <= 0 || Height <= 0) return;

            Height = LayoutColors.SidebarItemHeight;
            int centerY = Math.Max(0, (Height - IconSize) / 2);

            if (_collapsed)
            {
                int iconX = Math.Max(0, (Width - IconSize) / 2);
                _iconBox.Location = new Point(iconX, centerY);
                _iconBox.Visible = true;
                _iconBox.IconSize = 18;
                _iconBox.IconColor = _isActive
                    ? LayoutColors.SidebarTextActive
                    : LayoutColors.SidebarTextInactive;

                _textLabel.Visible = false;
                _badgeLabel.Visible = false;
            }
            else
            {
                _iconBox.Location = new Point(IconLeft, centerY);
                _iconBox.Visible = true;
                _textLabel.Visible = true;

                bool showBadge = _badgeCount > 0;
                int reserved = showBadge ? BadgeWidth + 8 : 0;
                int textX = IconLeft + IconSize + TextGap;
                int textW = Math.Max(0, Width - textX - RightPadding - reserved);

                _textLabel.Location = new Point(textX, 0);
                _textLabel.Size = new Size(textW, Height);

                if (showBadge)
                {
                    _badgeLabel.Location = new Point(
                        Width - RightPadding - BadgeWidth,
                        (Height - _badgeLabel.Height) / 2);
                    _badgeLabel.Visible = true;
                }
                else
                {
                    _badgeLabel.Visible = false;
                }
            }
        }

        private void UpdateVisualState()
        {
            _iconBox.IconColor = _isActive
                ? LayoutColors.SidebarTextActive
                : LayoutColors.SidebarTextInactive;
            _textLabel.ForeColor = _isActive
                ? LayoutColors.SidebarTextActive
                : LayoutColors.SidebarTextInactive;
            BackColor = _isActive ? LayoutColors.SidebarItemHover : Color.Transparent;
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            LayoutChildren();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_isActive)
            {
                using (var pen = new Pen(LayoutColors.Accent, 3))
                    e.Graphics.DrawLine(pen, 0, 4, 0, Height - 4);
            }

            if (_collapsed && _badgeCount > 0)
            {
                var dot = new Rectangle(_iconBox.Right - 6, _iconBox.Top - 2, 9, 9);
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(LayoutColors.RedDot))
                    e.Graphics.FillEllipse(brush, dot);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _toolTip?.Dispose();
            base.Dispose(disposing);
        }
    }
}
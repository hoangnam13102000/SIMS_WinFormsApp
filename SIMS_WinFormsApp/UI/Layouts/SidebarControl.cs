using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Layouts
{
    public class SidebarControl : UserControl
    {
        private const int GroupHeaderHeight = 36;
        private const int MenuBarHeight = 48;
        private const int ScrollBarWidth = 6;
        private const int ScrollBarMargin = 3;

        private readonly List<GroupInfo> _groups = new List<GroupInfo>();
        private string _activePageKey;
        private bool _collapsed;

        private readonly Panel _menuBar;
        private readonly Label _menuLabel;
        private readonly Panel _togglePanel;
        private readonly IconPictureBox _toggleIcon;
        private bool _toggleHover;

        private readonly Panel _viewport;
        private readonly Panel _content;
        private int _scrollY;
        private int _contentHeight;
        private bool _thumbHover;
        private bool _thumbDragging;
        private int _dragStartY;
        private int _dragStartScrollY;
        private Rectangle _thumbRect = Rectangle.Empty;

        public event EventHandler<string> ItemClicked;
        public event EventHandler SidebarToggleRequested;

        private class GroupInfo
        {
            public string Key { get; set; }
            public string Header { get; set; }
            public List<SidebarItemControl> Items { get; set; } = new List<SidebarItemControl>();
            public bool Expanded { get; set; } = true;
        }

        public SidebarControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);
            AutoScaleMode = AutoScaleMode.None;
            BackColor = LayoutColors.SidebarBg;
            Width = LayoutColors.SidebarWidth;
            DoubleBuffered = true;

            _menuBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = MenuBarHeight,
                BackColor = LayoutColors.SidebarBg,
                Margin = new Padding(0)
            };
            _menuBar.Paint += (s, e) =>
            {
                using (var pen = new Pen(LayoutColors.SidebarBorder))
                    e.Graphics.DrawLine(pen, 0, _menuBar.Height - 1, _menuBar.Width, _menuBar.Height - 1);
            };

            _menuLabel = new Label
            {
                Text = "MENU",
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                ForeColor = LayoutColors.SidebarTextMuted,
                AutoSize = true,
                BackColor = Color.Transparent
            };

            _toggleIcon = new IconPictureBox
            {
                IconChar = IconChar.Bars,
                IconFont = IconFont.Solid,
                IconColor = LayoutColors.TextWhite,
                IconSize = 20,
                Size = new Size(24, 24),
                SizeMode = PictureBoxSizeMode.CenterImage,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Location = new Point(10, 10)
            };

            _togglePanel = new Panel
            {
                Size = new Size(40, 40),
                BackColor = LayoutColors.SidebarBg,
                Cursor = Cursors.Hand
            };
            _togglePanel.Controls.Add(_toggleIcon);
            _togglePanel.Paint += (s, e) =>
            {
                if (!_toggleHover) return;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(LayoutColors.HeaderIconBgHover))
                    FillRoundRect(e.Graphics, brush,
                        new Rectangle(0, 0, _togglePanel.Width - 1, _togglePanel.Height - 1), 8);
            };

            EventHandler onToggle = (_, __) => SidebarToggleRequested?.Invoke(this, EventArgs.Empty);
            _togglePanel.Click += onToggle;
            _toggleIcon.Click += onToggle;
            _togglePanel.MouseEnter += (_, __) => { _toggleHover = true; _togglePanel.Invalidate(); };
            _togglePanel.MouseLeave += (_, __) => { _toggleHover = false; _togglePanel.Invalidate(); };

            var tip = new ToolTip { InitialDelay = 400 };
            tip.SetToolTip(_togglePanel, "Thu gọn / Mở rộng menu");
            tip.SetToolTip(_toggleIcon, "Thu gọn / Mở rộng menu");

            _menuBar.Controls.Add(_menuLabel);
            _menuBar.Controls.Add(_togglePanel);
            _menuBar.Resize += (_, __) => LayoutMenuBar();

            _viewport = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = LayoutColors.SidebarBg,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            typeof(Panel).GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(_viewport, true, null);

            _content = new Panel
            {
                Location = Point.Empty,
                BackColor = LayoutColors.SidebarBg,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            _viewport.Controls.Add(_content);
            _viewport.Paint += Viewport_Paint;
            _viewport.MouseDown += Viewport_MouseDown;
            _viewport.MouseMove += Viewport_MouseMove;
            _viewport.MouseUp += Viewport_MouseUp;
            _viewport.MouseLeave += (_, __) => { _thumbHover = false; _viewport.Invalidate(); };
            _viewport.MouseWheel += Viewport_MouseWheel;
            _viewport.Resize += (_, __) =>
            {
                ClampScroll();
                ApplyScroll();
                _viewport.Invalidate();
            };

            Controls.Add(_viewport);
            Controls.Add(_menuBar);
            LayoutMenuBar();
        }

        // ---------- Scroll ----------
        private bool NeedScroll => _contentHeight > _viewport.Height && _viewport.Height > 0;

        private void ClampScroll()
        {
            int max = Math.Max(0, _contentHeight - _viewport.Height);
            if (_scrollY < 0) _scrollY = 0;
            if (_scrollY > max) _scrollY = max;
        }

        private void ApplyScroll()
        {
            _content.Location = new Point(0, -_scrollY);
            _viewport.Invalidate();
        }

        private Rectangle GetTrackRect()
        {
            int x = _viewport.Width - ScrollBarWidth - ScrollBarMargin;
            return new Rectangle(x, ScrollBarMargin, ScrollBarWidth,
                Math.Max(0, _viewport.Height - ScrollBarMargin * 2));
        }

        private Rectangle GetThumbRect()
        {
            if (!NeedScroll) return Rectangle.Empty;
            var track = GetTrackRect();
            float ratio = (float)_viewport.Height / _contentHeight;
            int thumbH = Math.Max(28, (int)(track.Height * ratio));
            int travel = track.Height - thumbH;
            int maxScroll = Math.Max(1, _contentHeight - _viewport.Height);
            int thumbY = track.Y + (int)((float)_scrollY / maxScroll * travel);
            _thumbRect = new Rectangle(track.X, thumbY, track.Width, thumbH);
            return _thumbRect;
        }

        private void Viewport_Paint(object sender, PaintEventArgs e)
        {
            if (!NeedScroll) return;
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var track = GetTrackRect();
            using (var b = new SolidBrush(Color.FromArgb(40, 255, 255, 255)))
                FillRoundRect(g, b, track, 3);
            var thumb = GetThumbRect();
            if (thumb.IsEmpty) return;
            Color tc = _thumbDragging ? Color.FromArgb(140, 255, 255, 255)
                : _thumbHover ? Color.FromArgb(110, 255, 255, 255)
                : Color.FromArgb(70, 255, 255, 255);
            using (var b = new SolidBrush(tc))
                FillRoundRect(g, b, thumb, 3);
        }

        private static void FillRoundRect(Graphics g, Brush brush, Rectangle r, int radius)
        {
            if (r.Width <= 0 || r.Height <= 0) return;
            using (var path = new GraphicsPath())
            {
                int d = Math.Min(radius * 2, Math.Min(r.Width, r.Height));
                path.AddArc(r.X, r.Y, d, d, 180, 90);
                path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
                path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
                path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
                path.CloseFigure();
                g.FillPath(brush, path);
            }
        }

        private void Viewport_MouseDown(object sender, MouseEventArgs e)
        {
            if (!NeedScroll || e.Button != MouseButtons.Left) return;
            var thumb = GetThumbRect();
            if (thumb.Contains(e.Location))
            {
                _thumbDragging = true;
                _dragStartY = e.Y;
                _dragStartScrollY = _scrollY;
                _viewport.Capture = true;
                _viewport.Invalidate();
                return;
            }
            var track = GetTrackRect();
            if (track.Contains(e.Location))
            {
                _scrollY += e.Y < thumb.Y ? -_viewport.Height : _viewport.Height;
                ClampScroll();
                ApplyScroll();
            }
        }

        private void Viewport_MouseMove(object sender, MouseEventArgs e)
        {
            if (_thumbDragging)
            {
                var track = GetTrackRect();
                var thumb = GetThumbRect();
                int travel = Math.Max(1, track.Height - thumb.Height);
                int maxScroll = Math.Max(1, _contentHeight - _viewport.Height);
                _scrollY = _dragStartScrollY + (int)((float)(e.Y - _dragStartY) / travel * maxScroll);
                ClampScroll();
                ApplyScroll();
                return;
            }
            bool hover = NeedScroll && GetThumbRect().Contains(e.Location);
            if (hover != _thumbHover)
            {
                _thumbHover = hover;
                _viewport.Cursor = hover ? Cursors.Hand : Cursors.Default;
                _viewport.Invalidate();
            }
        }

        private void Viewport_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_thumbDragging) return;
            _thumbDragging = false;
            _viewport.Capture = false;
            _viewport.Invalidate();
        }

        private void Viewport_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!NeedScroll) return;
            _scrollY -= e.Delta / 3;
            ClampScroll();
            ApplyScroll();
        }

        // ---------- Menu bar ----------
        private void LayoutMenuBar()
        {
            if (_togglePanel == null) return;
            if (_collapsed)
            {
                _menuLabel.Visible = false;
                _togglePanel.Location = new Point(
                    Math.Max(0, (_menuBar.Width - _togglePanel.Width) / 2),
                    (_menuBar.Height - _togglePanel.Height) / 2);
            }
            else
            {
                _menuLabel.Visible = true;
                _menuLabel.Location = new Point(16, (_menuBar.Height - _menuLabel.Height) / 2);
                _togglePanel.Location = new Point(
                    _menuBar.Width - _togglePanel.Width - 8,
                    (_menuBar.Height - _togglePanel.Height) / 2);
            }
        }

        // ---------- Items ----------
        public void AddItem(string groupKey, string groupHeader, string pageKey, string label, IconChar icon)
        {
            if (string.IsNullOrWhiteSpace(groupKey)) groupKey = "__root__";
            var group = _groups.FirstOrDefault(g => g.Key == groupKey);
            if (group == null)
            {
                group = new GroupInfo { Key = groupKey, Header = groupHeader, Expanded = true };
                _groups.Add(group);
            }
            var item = new SidebarItemControl(pageKey, label, icon);
            item.ItemClicked += (_, key) =>
            {
                SetActive(key);
                ItemClicked?.Invoke(this, key);
            };
            item.SetCollapsed(_collapsed);
            group.Items.Add(item);
            BuildLayout();
        }

        private void BuildLayout()
        {
            if (IsDisposed) return;

            _content.SuspendLayout();
            _content.Controls.Clear();

            int usableWidth = Math.Max(Width, 1);
            int y = 8;

            foreach (var group in _groups)
            {
                if (!_collapsed && usableWidth >= 160 && !string.IsNullOrWhiteSpace(group.Header))
                {
                    y = AddGroupHeader(group, usableWidth, y);
                }
                else if (_collapsed && group != _groups.FirstOrDefault())
                {
                    _content.Controls.Add(new Panel
                    {
                        Location = new Point(Math.Max(8, (usableWidth - 24) / 2), y + 4),
                        Size = new Size(24, 1),
                        BackColor = LayoutColors.SidebarBorder
                    });
                    y += 12;
                }

                bool showItems = _collapsed || group.Expanded;
                if (showItems)
                {
                    foreach (var item in group.Items)
                    {
                        item.SuspendLayout();
                        item.Width = usableWidth;
                        item.Height = LayoutColors.SidebarItemHeight;
                        item.SetCollapsed(_collapsed);
                        item.Location = new Point(0, y);
                        item.ResumeLayout(true);
                        _content.Controls.Add(item);
                        y += item.Height;
                    }
                }
            }

            _contentHeight = y + 12;
            _content.Width = usableWidth;
            _content.Height = Math.Max(_contentHeight, Math.Max(_viewport.ClientSize.Height, 1));

            ClampScroll();
            ApplyScroll();

            _content.ResumeLayout(true);
            LayoutMenuBar();
            _viewport.Invalidate();
        }

        private int AddGroupHeader(GroupInfo group, int usableWidth, int y)
        {
            const int chevronSize = 18;
            var headerPanel = new Panel
            {
                Location = new Point(0, y),
                Size = new Size(usableWidth, GroupHeaderHeight),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };

            var lblHeader = new Label
            {
                Text = group.Header,
                ForeColor = LayoutColors.SidebarTextMuted,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                AutoSize = false,
                AutoEllipsis = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Location = new Point(16, 0),
                Size = new Size(Math.Max(0, usableWidth - 16 - chevronSize - 14), GroupHeaderHeight),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };

            var chevron = new IconPictureBox
            {
                IconChar = group.Expanded ? IconChar.ChevronDown : IconChar.ChevronRight,
                IconFont = IconFont.Solid,
                IconColor = LayoutColors.SidebarTextMuted,
                IconSize = 11,
                Size = new Size(chevronSize, chevronSize),
                SizeMode = PictureBoxSizeMode.CenterImage,
                Location = new Point(usableWidth - 8 - chevronSize, (GroupHeaderHeight - chevronSize) / 2),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };

            EventHandler toggle = (_, __) =>
            {
                group.Expanded = !group.Expanded;
                BuildLayout();
            };

            void Hook(Control c)
            {
                c.Click += toggle;
                c.MouseEnter += (_, __) =>
                {
                    lblHeader.ForeColor = LayoutColors.SidebarTextHover;
                    chevron.IconColor = LayoutColors.SidebarTextHover;
                    headerPanel.BackColor = LayoutColors.SidebarSectionHover;
                };
                c.MouseLeave += (_, __) =>
                {
                    if (headerPanel.IsDisposed) return;
                    if (headerPanel.ClientRectangle.Contains(headerPanel.PointToClient(Cursor.Position)))
                        return;
                    lblHeader.ForeColor = LayoutColors.SidebarTextMuted;
                    chevron.IconColor = LayoutColors.SidebarTextMuted;
                    headerPanel.BackColor = Color.Transparent;
                };
            }
            Hook(headerPanel);
            Hook(lblHeader);
            Hook(chevron);

            headerPanel.Controls.Add(lblHeader);
            headerPanel.Controls.Add(chevron);
            _content.Controls.Add(headerPanel);
            return y + GroupHeaderHeight;
        }

        public void SetActive(string pageKey)
        {
            _activePageKey = pageKey;
            foreach (var group in _groups)
                foreach (var item in group.Items)
                    item.IsActive = string.Equals(item.PageKey, pageKey, StringComparison.OrdinalIgnoreCase);
        }

        public void SetBadge(string pageKey, int count)
        {
            foreach (var group in _groups)
            {
                var item = group.Items.FirstOrDefault(i => i.PageKey == pageKey);
                if (item != null) { item.SetBadge(count); break; }
            }
        }

        /// <summary>
        /// collapsed = mode icon-only / full.
        /// rebuild = true → BuildLayout; false → chỉ đổi cờ item.
        /// </summary>
        public void SetCollapsed(bool collapsed, bool rebuild = true)
        {
            _collapsed = collapsed;

            int target = collapsed
                ? LayoutColors.SidebarWidthCollapsed
                : LayoutColors.SidebarWidth;

            MinimumSize = Size.Empty;
            MaximumSize = Size.Empty;
            Width = target;

            if (!collapsed)
            {
                foreach (var g in _groups)
                    g.Expanded = true;
            }

            if (rebuild)
            {
                MinimumSize = new Size(target, 0);
                MaximumSize = new Size(target, 0);
                _scrollY = 0;
                LayoutMenuBar();
                BuildLayout();
                Invalidate(true);
            }
            else
            {
                foreach (var g in _groups)
                    foreach (var item in g.Items)
                        item.SetCollapsed(collapsed);
                LayoutMenuBar();
                Invalidate(true);
            }
        }

        /// <summary>
        /// Gọi TRƯỚC khi bắt đầu animation đóng/mở. Chỉ cập nhật cờ trạng thái
        /// (_collapsed, group.Expanded) và mở khóa Min/Max Size để Width có thể
        /// tự do thay đổi mượt trong suốt animation — KHÔNG snap Width, KHÔNG
        /// rebuild layout ngay (tránh giật/nhảy khung hình trước khi animation chạy).
        /// </summary>
        public void PrepareAnimatedCollapse(bool collapsed)
        {
            _collapsed = collapsed;

            if (!collapsed)
            {
                foreach (var g in _groups)
                    g.Expanded = true;
            }

            // Bỏ khóa kích thước để ApplyAnimatedWidth có thể set Width mượt mỗi frame
            MinimumSize = Size.Empty;
            MaximumSize = Size.Empty;
        }

        /// <summary>
        /// Gọi mỗi frame animation — chỉ đổi Width, không rebuild.
        /// </summary>
        public void ApplyAnimatedWidth(int width)
        {
            if (width < 1) width = 1;

            MinimumSize = Size.Empty;
            MaximumSize = Size.Empty;
            Width = width;

            // Trong lúc animate: width hẹp → luôn icon-only (tránh chữ cắt)
            bool showAsCollapsed = _collapsed || width < 160;

            foreach (var g in _groups)
                foreach (var item in g.Items)
                {
                    item.Width = width;
                    item.SetCollapsed(showAsCollapsed);
                }

            LayoutMenuBar();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var pen = new Pen(LayoutColors.SidebarBorder))
                e.Graphics.DrawLine(pen, Width - 1, 0, Width - 1, Height);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            LayoutMenuBar();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Layouts
{
    public class MainLayoutControl : UserControl
    {
        private readonly HeaderControl _header;
        private readonly SidebarControl _sidebar;
        private readonly FooterControl _footer;
        private readonly Panel _contentHost;
        private readonly TableLayoutPanel _root;

        private readonly Dictionary<string, Control> _pages =
            new Dictionary<string, Control>(StringComparer.OrdinalIgnoreCase);
        private string _currentSectionKey;
        private string _currentSectionHeader;
        private string _currentPageKey;
        private int _sectionCounter;
        private bool _sidebarCollapsed;

        // Animation
        private System.Windows.Forms.Timer _sidebarAnimTimer;
        private float _sidebarAnimFrom;
        private float _sidebarAnimTo;
        private float _sidebarAnimProgress;
        private const int SidebarAnimDurationMs = 180;
        private const int SidebarAnimIntervalMs = 12;

        public HeaderControl Header => _header;
        public SidebarControl Sidebar => _sidebar;
        public FooterControl Footer => _footer;
        public Panel ContentHost => _contentHost;
        public string CurrentPageKey => _currentPageKey;

        public event EventHandler LogoutRequested;
        public event EventHandler ProfileRequested;
        public event EventHandler<string> PageChanged;

        public MainLayoutControl() : this("Cửa hàng điện thoại trực tuyến") { }

        public MainLayoutControl(string headerSubtitle)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);

            AutoScaleMode = AutoScaleMode.None;
            DoubleBuffered = true;
            Dock = DockStyle.Fill;
            BackColor = Color.FromArgb(245, 247, 250);
            Font = new Font("Segoe UI", 9f);
            Padding = new Padding(0);
            Margin = new Padding(0);

            _header = new HeaderControl(headerSubtitle)
            {
                Dock = DockStyle.Fill,
                AutoScaleMode = AutoScaleMode.None,
                Margin = new Padding(0)
            };
            _header.LogoutClicked += (_, __) => LogoutRequested?.Invoke(this, EventArgs.Empty);
            _header.ProfileClicked += (_, __) => ProfileRequested?.Invoke(this, EventArgs.Empty);

            _sidebar = new SidebarControl
            {
                Dock = DockStyle.Fill,
                AutoScaleMode = AutoScaleMode.None,
                Margin = new Padding(0)
            };
            _sidebar.ItemClicked += (_, key) =>
            {
                ShowPage(key);
                PageChanged?.Invoke(this, key);
            };
            _sidebar.SidebarToggleRequested += (_, __) =>
                SetSidebarCollapsed(!_sidebarCollapsed);

            _footer = new FooterControl
            {
                Dock = DockStyle.Fill,
                AutoScaleMode = AutoScaleMode.None,
                Margin = new Padding(0)
            };

            _contentHost = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(245, 247, 250),
                Padding = new Padding(16),
                Margin = new Padding(0)
            };

            _root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 2,
                BackColor = Color.FromArgb(245, 247, 250),
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            // Double-buffer TableLayoutPanel để giảm flicker
            typeof(TableLayoutPanel).GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(_root, true, null);

            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, LayoutColors.HeaderHeight));
            _root.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            _root.RowStyles.Add(new RowStyle(SizeType.Absolute, LayoutColors.FooterHeight));
            _root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, LayoutColors.SidebarWidth));
            _root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));

            _root.Controls.Add(_header, 0, 0);
            _root.SetColumnSpan(_header, 2);

            _root.Controls.Add(_sidebar, 0, 1);
            _root.Controls.Add(_contentHost, 1, 1);

            _root.Controls.Add(_footer, 0, 2);
            _root.SetColumnSpan(_footer, 2);

            Controls.Add(_root);
        }

        public void AddSection(string label)
        {
            _sectionCounter++;
            _currentSectionKey = $"__section_{_sectionCounter}";
            _currentSectionHeader = label;
        }

        public void AddPage(string key, string label, Control page, IconChar iconChar = IconChar.Circle)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("key is required", nameof(key));
            if (page == null)
                throw new ArgumentNullException(nameof(page));

            if (page is UserControl uc)
            {
                uc.AutoScaleMode = AutoScaleMode.None;
                uc.Font = new Font("Segoe UI", 9f);
            }
            page.Dock = DockStyle.Fill;
            page.Visible = false;
            page.Margin = new Padding(0);

            _pages[key] = page;
            _contentHost.Controls.Add(page);

            string groupKey = string.IsNullOrEmpty(_currentSectionKey) ? "__root__" : _currentSectionKey;
            string groupHeader = string.IsNullOrEmpty(_currentSectionHeader) ? "KHÁC" : _currentSectionHeader;
            _sidebar.AddItem(groupKey, groupHeader.ToUpperInvariant(), key, label, iconChar);
        }

        public void ShowPage(string key)
        {
            if (!_pages.ContainsKey(key)) return;

            foreach (var kv in _pages)
                kv.Value.Visible = string.Equals(kv.Key, key, StringComparison.OrdinalIgnoreCase);

            _currentPageKey = key;
            _sidebar.SetActive(key);
            _contentHost.PerformLayout();

            if (_pages.TryGetValue(key, out var page))
            {
                page.BringToFront();
                page.PerformLayout();
            }
        }

        public void SetSidebarCollapsed(bool collapsed)
        {
            _sidebarCollapsed = collapsed;

            float target = collapsed
                ? LayoutColors.SidebarWidthCollapsed
                : LayoutColors.SidebarWidth;

            // Chỉ cập nhật cờ trạng thái + mở khóa Min/Max, KHÔNG snap Width/rebuild
            // ngay — để animation chạy mượt từ kích thước hiện tại, tránh giật khung
            // hình / icon dồn lên trên ngay khi vừa bấm toggle.
            _sidebar.PrepareAnimatedCollapse(collapsed);

            _sidebarAnimFrom = _root.ColumnStyles[0].Width;
            if (_sidebarAnimFrom < 1f)
                _sidebarAnimFrom = collapsed ? LayoutColors.SidebarWidth : LayoutColors.SidebarWidthCollapsed;

            _sidebarAnimTo = target;
            _sidebarAnimProgress = 0f;

            if (_sidebarAnimTimer == null)
            {
                _sidebarAnimTimer = new System.Windows.Forms.Timer { Interval = SidebarAnimIntervalMs };
                _sidebarAnimTimer.Tick += SidebarAnim_Tick;
            }

            _sidebarAnimTimer.Stop();
            _sidebarAnimTimer.Start();
        }

        private void SidebarAnim_Tick(object sender, EventArgs e)
        {
            _sidebarAnimProgress += (float)SidebarAnimIntervalMs / SidebarAnimDurationMs;
            if (_sidebarAnimProgress >= 1f)
                _sidebarAnimProgress = 1f;

            // Ease-out cubic
            float t = _sidebarAnimProgress;
            float eased = 1f - (1f - t) * (1f - t) * (1f - t);

            float w = _sidebarAnimFrom + (_sidebarAnimTo - _sidebarAnimFrom) * eased;
            _root.ColumnStyles[0].SizeType = SizeType.Absolute;
            _root.ColumnStyles[0].Width = w;

            // Chỉ đổi width, không BuildLayout mỗi frame
            _sidebar.ApplyAnimatedWidth((int)Math.Round(w));

            if (_sidebarAnimProgress >= 1f)
            {
                _sidebarAnimTimer.Stop();

                int finalW = (int)_sidebarAnimTo;
                _root.ColumnStyles[0].Width = finalW;
                _sidebar.SetCollapsed(_sidebarCollapsed, rebuild: true);
                _root.PerformLayout();
            }
        }

        public void SetUser(string displayName, string email, string avatarInitial = null, string role = null)
            => _header.SetUser(displayName, email, avatarInitial, role);

        public void SetBadge(string key, int count)
            => _sidebar.SetBadge(key, count);

        public void SetUnreadCount(int count)
            => _header.SetUnreadCount(count);

        public bool TryGetPage<T>(string key, out T page) where T : Control
        {
            page = null;
            if (_pages.TryGetValue(key, out var ctrl) && ctrl is T typed)
            {
                page = typed;
                return true;
            }
            return false;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            _root?.PerformLayout();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _sidebarAnimTimer?.Stop();
                _sidebarAnimTimer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
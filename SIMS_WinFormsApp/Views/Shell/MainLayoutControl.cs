using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Layouts
{
    public partial class MainLayoutControl : UserControl
    {
        private readonly Dictionary<string, Control> _pages =
            new Dictionary<string, Control>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, TreeNode> _pageNodes =
            new Dictionary<string, TreeNode>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, string> _pageLabels =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private readonly List<TreeNode> _sectionNodes = new List<TreeNode>();
        private TreeNode _currentSection;
        private string _currentPageKey;
        private int _unreadCount;
        private int _orderBadgeCount;
        private bool _sidebarCollapsed;
        private int _expandedSidebarWidth = 240;

        public Panel ContentHost => _contentHost;
        public string CurrentPageKey => _currentPageKey;
        public Control ChatbotButton => _chatbotButton;

        public event EventHandler LogoutRequested;
        public event EventHandler ProfileRequested;
        public event EventHandler<string> PageChanged;
        public event EventHandler AiChatRequested;

        public MainLayoutControl() : this("Cửa hàng điện thoại trực tuyến")
        {
        }

        public MainLayoutControl(string headerSubtitle)
        {
            InitializeComponent();
            _subtitleLabel.Text = headerSubtitle;
            _navigationTree.AfterSelect += NavigationTree_AfterSelect;
            _profileButton.Click += (sender, args) => ProfileRequested?.Invoke(this, EventArgs.Empty);
            _logoutButton.Click += (sender, args) => LogoutRequested?.Invoke(this, EventArgs.Empty);
            _chatbotButton.Click += (sender, args) => AiChatRequested?.Invoke(this, EventArgs.Empty);
            _settingsButton.Click += (sender, args) =>
            {
                if (_pages.ContainsKey("settings"))
                    ShowPage("settings");
            };
            _sidebarToggleButton.Click += (sender, args) => SetSidebarCollapsed(!_sidebarCollapsed);
            _navigationTree.NodeMouseDoubleClick += (sender, args) =>
            {
                if (args.Node != null && args.Node.Tag is string)
                    ShowPage((string)args.Node.Tag);
            };
            ThemeManager.Instance.ThemeChanged += OnAppearanceChanged;
            LanguageManager.Instance.LanguageChanged += OnAppearanceChanged;
            ApplyAppearance();
        }

        public void SetSubtitle(string subtitle)
        {
            _subtitleLabel.Text = subtitle ?? string.Empty;
        }

        public void AddSection(string label)
        {
            string text = (label ?? string.Empty).ToUpperInvariant();
            _currentSection = new TreeNode(text) { Tag = null };
            _sectionNodes.Add(_currentSection);
            _navigationTree.Nodes.Add(_currentSection);
            _currentSection.Expand();
        }

        public void AddPage(
            string key,
            string label,
            Control page,
            IconChar iconChar = IconChar.Circle,
            bool showInSidebar = true)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("key is required", nameof(key));
            if (page == null)
                throw new ArgumentNullException(nameof(page));

            if (_pages.TryGetValue(key, out Control existingPage))
            {
                _contentHost.Controls.Remove(existingPage);
                existingPage.Dispose();
            }

            if (page is UserControl userControl)
            {
                userControl.AutoScaleMode = AutoScaleMode.None;
                userControl.Font = new Font("Segoe UI", 9f);
            }

            page.Dock = DockStyle.Fill;
            page.Visible = false;
            page.Margin = Padding.Empty;
            _pages[key] = page;
            _pageLabels[key] = label ?? key;
            _contentHost.Controls.Add(page);

            if (!showInSidebar)
                return;

            TreeNode parent = _currentSection;
            if (parent == null)
            {
                parent = new TreeNode("KHÁC");
                _sectionNodes.Add(parent);
                _navigationTree.Nodes.Add(parent);
            }

            var node = new TreeNode(_pageLabels[key]) { Tag = key };
            parent.Nodes.Add(node);
            parent.Expand();
            _pageNodes[key] = node;
        }

        public void AddLazyPage(
            string key,
            string label,
            Func<Control> createPage,
            IconChar iconChar = IconChar.Circle,
            bool showInSidebar = true)
        {
            if (createPage == null)
                throw new ArgumentNullException(nameof(createPage));

            AddPage(key, label, new LazyPageHost(createPage), iconChar, showInSidebar);
        }

        public void ShowPage(string key)
        {
            if (!_pages.ContainsKey(key))
                return;

            foreach (KeyValuePair<string, Control> page in _pages)
                page.Value.Visible = string.Equals(page.Key, key, StringComparison.OrdinalIgnoreCase);

            _currentPageKey = key;
            if (_pageNodes.TryGetValue(key, out TreeNode node))
            {
                _navigationTree.AfterSelect -= NavigationTree_AfterSelect;
                _navigationTree.SelectedNode = node;
                _navigationTree.AfterSelect += NavigationTree_AfterSelect;
                node.EnsureVisible();
            }

            _contentHost.PerformLayout();
            Control activePage = _pages[key];
            activePage.BringToFront();
            activePage.PerformLayout();
        }

        public void SetUser(
            string displayName,
            string email,
            string avatarInitial = null,
            string role = null,
            string avatarPath = null)
        {
            _userNameLabel.Text = string.IsNullOrWhiteSpace(displayName) ? "Người dùng" : displayName;
            _userEmailLabel.Text = email ?? string.Empty;
            _profileButton.Text = string.IsNullOrWhiteSpace(avatarInitial)
                ? "Hồ sơ"
                : avatarInitial.Trim().Substring(0, 1).ToUpperInvariant();
        }

        public void SetBadge(string key, int count)
        {
            if (string.Equals(key, "orders", StringComparison.OrdinalIgnoreCase))
                _orderBadgeCount = Math.Max(0, count);

            if (_pageNodes.TryGetValue(key, out TreeNode node))
                node.Text = FormatPageLabel(key);
        }

        public void SetUnreadCount(int count)
        {
            _unreadCount = Math.Max(0, count);
            _notificationsButton.Text = _unreadCount == 0 ? "Thông báo" : "Thông báo (" + _unreadCount + ")";
        }

        public void SetSidebarCollapsed(bool collapsed)
        {
            if (_sidebarCollapsed == collapsed)
                return;

            if (!collapsed)
                _expandedSidebarWidth = Math.Max(180, _expandedSidebarWidth);
            else
                _expandedSidebarWidth = _sidebarPanel.Width;

            _sidebarCollapsed = collapsed;
            _root.ColumnStyles[0].Width = collapsed ? 56F : _expandedSidebarWidth;
            _sidebarPanel.Width = collapsed ? 56 : _expandedSidebarWidth;
            _navigationTree.Visible = !collapsed;
            _menuLabel.Visible = !collapsed;
            _sidebarToggleButton.IconChar = collapsed ? IconChar.AngleRight : IconChar.Bars;
            _sidebarToggleButton.Left = collapsed ? 8 : _sidebarPanel.Width - _sidebarToggleButton.Width - 8;
            _root.PerformLayout();
        }

        public void ApplyLabels(
            string menuLabel,
            string toggleTooltip,
            string[] sectionLabels,
            IDictionary<string, string> itemLabels)
        {
            if (!string.IsNullOrEmpty(menuLabel))
                _menuLabel.Text = menuLabel;
            if (itemLabels != null)
            {
                foreach (KeyValuePair<string, TreeNode> item in _pageNodes)
                {
                    if (itemLabels.TryGetValue(item.Key, out string translated))
                        _pageLabels[item.Key] = translated;
                    item.Value.Text = FormatPageLabel(item.Key);
                }
            }

            if (sectionLabels != null)
            {
                for (int i = 0; i < _sectionNodes.Count && i < sectionLabels.Length; i++)
                    _sectionNodes[i].Text = (sectionLabels[i] ?? string.Empty).ToUpperInvariant();
            }
        }

        public bool TryGetPage<T>(string key, out T page) where T : Control
        {
            page = null;
            if (_pages.TryGetValue(key, out Control control) && control is T typed)
            {
                page = typed;
                return true;
            }

            return false;
        }

        private string FormatPageLabel(string key)
        {
            string label = _pageLabels.TryGetValue(key, out string value) ? value : key;
            return string.Equals(key, "orders", StringComparison.OrdinalIgnoreCase) && _orderBadgeCount > 0
                ? label + " (" + _orderBadgeCount + ")"
                : label;
        }

        private void NavigationTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is string key)
            {
                ShowPage(key);
                PageChanged?.Invoke(this, key);
            }
        }

        private void OnAppearanceChanged(object sender, EventArgs e)
        {
            if (!IsDisposed)
                ApplyAppearance();
        }

        private void ApplyAppearance()
        {
            BackColor = AppColors.PageBg;
            _root.BackColor = AppColors.PageBg;
            _contentHost.BackColor = AppColors.PageBg;
            _navigationTree.BackColor = Color.FromArgb(24, 35, 52);
            Refresh();
        }

        private sealed class LazyPageHost : UserControl
        {
            private readonly Func<Control> _createPage;
            private bool _created;

            public LazyPageHost(Func<Control> createPage)
            {
                _createPage = createPage;
                BackColor = AppColors.PageBg;
                VisibleChanged += (sender, args) =>
                {
                    if (Visible)
                        EnsurePageCreated();
                };
            }

            private void EnsurePageCreated()
            {
                if (_created)
                    return;

                try
                {
                    Control page = _createPage();
                    if (page == null)
                        throw new InvalidOperationException("Page factory returned null.");
                    page.Dock = DockStyle.Fill;
                    Controls.Clear();
                    Controls.Add(page);
                    page.Visible = true;
                    _created = true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("[MainLayout] Page creation failed: " + ex);
                    ShowCreationError(ex);
                }
            }

            private void ShowCreationError(Exception exception)
            {
                Controls.Clear();
                var errorLabel = new Label
                {
                    AutoSize = false,
                    Dock = DockStyle.Top,
                    Height = 72,
                    Padding = new Padding(20),
                    ForeColor = Color.FromArgb(153, 27, 27),
                    Text = "Không thể mở trang này.\r\n" + exception.Message
                };
                var retryButton = new Button
                {
                    AutoSize = true,
                    Text = "Thử lại",
                    Location = new Point(20, 82)
                };
                retryButton.Click += (sender, args) =>
                {
                    Controls.Clear();
                    EnsurePageCreated();
                };
                Controls.Add(errorLabel);
                Controls.Add(retryButton);
            }
        }

    }
}

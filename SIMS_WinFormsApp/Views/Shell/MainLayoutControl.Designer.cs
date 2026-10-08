using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Layouts
{
    partial class MainLayoutControl
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel _root;
        private Panel _headerPanel;
        private Label _brandLabel;
        private Label _subtitleLabel;
        private Label _userNameLabel;
        private Label _userEmailLabel;
        private Button _notificationsButton;
        private Button _profileButton;
        private Button _logoutButton;
        private Panel _sidebarPanel;
        private Label _menuLabel;
        private IconButton _sidebarToggleButton;
        private TreeView _navigationTree;
        private Panel _contentHost;
        private Panel _footerPanel;
        private Label _footerStatusLabel;
        private Button _settingsButton;
        private Button _chatbotButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                ThemeManager.Instance.ThemeChanged -= OnAppearanceChanged;
                LanguageManager.Instance.LanguageChanged -= OnAppearanceChanged;
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._root = new TableLayoutPanel();
            this._headerPanel = new Panel();
            this._brandLabel = new Label();
            this._subtitleLabel = new Label();
            this._userNameLabel = new Label();
            this._userEmailLabel = new Label();
            this._notificationsButton = new Button();
            this._profileButton = new Button();
            this._logoutButton = new Button();
            this._sidebarPanel = new Panel();
            this._menuLabel = new Label();
            this._sidebarToggleButton = new IconButton();
            this._navigationTree = new TreeView();
            this._contentHost = new Panel();
            this._footerPanel = new Panel();
            this._footerStatusLabel = new Label();
            this._settingsButton = new Button();
            this._chatbotButton = new Button();
            this._root.SuspendLayout();
            this._headerPanel.SuspendLayout();
            this._sidebarPanel.SuspendLayout();
            this._footerPanel.SuspendLayout();
            this.SuspendLayout();
            //
            // _root
            //
            this._root.BackColor = Color.FromArgb(245, 247, 250);
            this._root.ColumnCount = 2;
            this._root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 240F));
            this._root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this._root.Dock = DockStyle.Fill;
            this._root.Margin = Padding.Empty;
            this._root.Name = "_root";
            this._root.Padding = Padding.Empty;
            this._root.RowCount = 3;
            this._root.RowStyles.Add(new RowStyle(SizeType.Absolute, 86F));
            this._root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this._root.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
            this._root.Controls.Add(this._headerPanel, 0, 0);
            this._root.SetColumnSpan(this._headerPanel, 2);
            this._root.Controls.Add(this._sidebarPanel, 0, 1);
            this._root.Controls.Add(this._contentHost, 1, 1);
            this._root.Controls.Add(this._footerPanel, 0, 2);
            this._root.SetColumnSpan(this._footerPanel, 2);
            //
            // _headerPanel
            //
            this._headerPanel.BackColor = Color.FromArgb(15, 23, 42);
            this._headerPanel.Controls.Add(this._logoutButton);
            this._headerPanel.Controls.Add(this._profileButton);
            this._headerPanel.Controls.Add(this._userEmailLabel);
            this._headerPanel.Controls.Add(this._userNameLabel);
            this._headerPanel.Controls.Add(this._notificationsButton);
            this._headerPanel.Controls.Add(this._subtitleLabel);
            this._headerPanel.Controls.Add(this._brandLabel);
            this._headerPanel.Dock = DockStyle.Fill;
            this._headerPanel.Margin = Padding.Empty;
            this._headerPanel.Name = "_headerPanel";
            //
            // _brandLabel
            //
            this._brandLabel.AutoSize = true;
            this._brandLabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            this._brandLabel.ForeColor = Color.White;
            this._brandLabel.Location = new Point(22, 10);
            this._brandLabel.Name = "_brandLabel";
            this._brandLabel.Text = "SIMS";
            //
            // _subtitleLabel
            //
            this._subtitleLabel.AutoSize = true;
            this._subtitleLabel.Font = new Font("Segoe UI", 9F);
            this._subtitleLabel.ForeColor = Color.FromArgb(148, 163, 184);
            this._subtitleLabel.Location = new Point(25, 53);
            this._subtitleLabel.Name = "_subtitleLabel";
            this._subtitleLabel.Text = "Cửa hàng điện thoại trực tuyến";
            //
            // _notificationsButton
            //
            this._notificationsButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this._notificationsButton.FlatAppearance.BorderSize = 0;
            this._notificationsButton.FlatStyle = FlatStyle.Flat;
            this._notificationsButton.ForeColor = Color.White;
            this._notificationsButton.Location = new Point(760, 20);
            this._notificationsButton.Name = "_notificationsButton";
            this._notificationsButton.Size = new Size(54, 42);
            this._notificationsButton.Text = "0";
            this._notificationsButton.UseVisualStyleBackColor = false;
            //
            // _userNameLabel
            //
            this._userNameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this._userNameLabel.AutoSize = true;
            this._userNameLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this._userNameLabel.ForeColor = Color.White;
            this._userNameLabel.Location = new Point(850, 18);
            this._userNameLabel.Name = "_userNameLabel";
            this._userNameLabel.Text = "Người dùng";
            //
            // _userEmailLabel
            //
            this._userEmailLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this._userEmailLabel.AutoSize = true;
            this._userEmailLabel.Font = new Font("Segoe UI", 8F);
            this._userEmailLabel.ForeColor = Color.FromArgb(148, 163, 184);
            this._userEmailLabel.Location = new Point(850, 43);
            this._userEmailLabel.Name = "_userEmailLabel";
            this._userEmailLabel.Text = "";
            //
            // _profileButton
            //
            this._profileButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this._profileButton.FlatAppearance.BorderSize = 0;
            this._profileButton.FlatStyle = FlatStyle.Flat;
            this._profileButton.ForeColor = Color.White;
            this._profileButton.Location = new Point(1120, 20);
            this._profileButton.Name = "_profileButton";
            this._profileButton.Size = new Size(56, 42);
            this._profileButton.Text = "Hồ sơ";
            this._profileButton.UseVisualStyleBackColor = false;
            //
            // _logoutButton
            //
            this._logoutButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this._logoutButton.FlatAppearance.BorderColor = Color.FromArgb(71, 85, 105);
            this._logoutButton.FlatStyle = FlatStyle.Flat;
            this._logoutButton.ForeColor = Color.White;
            this._logoutButton.Location = new Point(1182, 20);
            this._logoutButton.Name = "_logoutButton";
            this._logoutButton.Size = new Size(80, 42);
            this._logoutButton.Text = "Đăng xuất";
            this._logoutButton.UseVisualStyleBackColor = false;
            //
            // _sidebarPanel
            //
            this._sidebarPanel.BackColor = Color.FromArgb(24, 35, 52);
            this._sidebarPanel.Controls.Add(this._navigationTree);
            this._sidebarPanel.Controls.Add(this._sidebarToggleButton);
            this._sidebarPanel.Controls.Add(this._menuLabel);
            this._sidebarPanel.Dock = DockStyle.Fill;
            this._sidebarPanel.Margin = Padding.Empty;
            this._sidebarPanel.Name = "_sidebarPanel";
            //
            // _menuLabel
            //
            this._menuLabel.AutoSize = true;
            this._menuLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            this._menuLabel.ForeColor = Color.FromArgb(148, 163, 184);
            this._menuLabel.Location = new Point(16, 17);
            this._menuLabel.Name = "_menuLabel";
            this._menuLabel.Text = "MENU";
            //
            // _sidebarToggleButton
            //
            this._sidebarToggleButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this._sidebarToggleButton.FlatAppearance.BorderSize = 0;
            this._sidebarToggleButton.FlatStyle = FlatStyle.Flat;
            this._sidebarToggleButton.ForeColor = Color.White;
            this._sidebarToggleButton.Location = new Point(190, 6);
            this._sidebarToggleButton.Name = "_sidebarToggleButton";
            this._sidebarToggleButton.Size = new Size(42, 36);
            this._sidebarToggleButton.IconChar = IconChar.Bars;
            this._sidebarToggleButton.IconColor = Color.White;
            this._sidebarToggleButton.IconFont = IconFont.Solid;
            this._sidebarToggleButton.IconSize = 18;
            this._sidebarToggleButton.UseVisualStyleBackColor = false;
            //
            // _navigationTree
            //
            this._navigationTree.BackColor = Color.FromArgb(24, 35, 52);
            this._navigationTree.BorderStyle = BorderStyle.None;
            this._navigationTree.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this._navigationTree.Dock = DockStyle.None;
            this._navigationTree.Font = new Font("Segoe UI", 9F);
            this._navigationTree.ForeColor = Color.White;
            this._navigationTree.FullRowSelect = true;
            this._navigationTree.HideSelection = false;
            this._navigationTree.Indent = 18;
            this._navigationTree.ItemHeight = 34;
            this._navigationTree.Location = new Point(0, 48);
            this._navigationTree.Name = "_navigationTree";
            this._navigationTree.ShowLines = false;
            this._navigationTree.ShowPlusMinus = false;
            this._navigationTree.ShowRootLines = false;
            this._navigationTree.Size = new Size(240, 556);
            //
            // _contentHost
            //
            this._contentHost.BackColor = Color.FromArgb(244, 247, 250);
            this._contentHost.Dock = DockStyle.Fill;
            this._contentHost.Margin = Padding.Empty;
            this._contentHost.Name = "_contentHost";
            //
            // _footerPanel
            //
            this._footerPanel.BackColor = Color.White;
            this._footerPanel.Controls.Add(this._footerStatusLabel);
            this._footerPanel.Dock = DockStyle.Fill;
            this._footerPanel.Margin = Padding.Empty;
            this._footerPanel.Name = "_footerPanel";
            //
            // _footerStatusLabel
            //
            this._footerStatusLabel.AutoSize = true;
            this._footerStatusLabel.Font = new Font("Segoe UI", 8F);
            this._footerStatusLabel.ForeColor = Color.FromArgb(100, 116, 139);
            this._footerStatusLabel.Location = new Point(16, 9);
            this._footerStatusLabel.Name = "_footerStatusLabel";
            this._footerStatusLabel.Text = "●  Hệ thống đang hoạt động";
            //
            // _settingsButton
            //
            this._settingsButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this._settingsButton.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this._settingsButton.Location = new Point(1190, 650);
            this._settingsButton.Name = "_settingsButton";
            this._settingsButton.Size = new Size(54, 54);
            this._settingsButton.Text = "⚙";
            this._settingsButton.UseVisualStyleBackColor = true;
            //
            // _chatbotButton
            //
            this._chatbotButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this._chatbotButton.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this._chatbotButton.Location = new Point(1126, 650);
            this._chatbotButton.Name = "_chatbotButton";
            this._chatbotButton.Size = new Size(54, 54);
            this._chatbotButton.Text = "AI";
            this._chatbotButton.UseVisualStyleBackColor = true;
            //
            // MainLayoutControl
            //
            this.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.FromArgb(244, 247, 250);
            this.Controls.Add(this._root);
            this.Controls.Add(this._chatbotButton);
            this.Controls.Add(this._settingsButton);
            this.Dock = DockStyle.Fill;
            this.Font = new Font("Segoe UI", 9F);
            this.Margin = Padding.Empty;
            this.Name = "MainLayoutControl";
            this.Size = new Size(1280, 720);
            this._root.ResumeLayout(false);
            this._headerPanel.ResumeLayout(false);
            this._headerPanel.PerformLayout();
            this._sidebarPanel.ResumeLayout(false);
            this._sidebarPanel.PerformLayout();
            this._footerPanel.ResumeLayout(false);
            this._footerPanel.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}

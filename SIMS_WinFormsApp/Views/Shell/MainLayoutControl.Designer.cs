using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Shell
{
    partial class MainLayoutControl
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel _root;
        private Panel _headerPanel;
        private PictureBox _logoIcon;
        private Label _brandLabel;
        private Label _subtitleLabel;
        private NotificationBellButton _notificationsButton;
        private Label _userNameLabel;
        private Label _userEmailLabel;
        private AvatarControl _avatarControl;
        private IconButton _profileButton;
        private ContextMenuStrip _accountContextMenu;
        private ToolStripMenuItem _profileMenuItem;
        private ToolStripMenuItem _logoutMenuItem;
        private ContextMenuStrip _notificationsContextMenu;
        private ToolStripMenuItem _notificationsTitleMenuItem;
        private ToolStripMenuItem _notificationsEmptyMenuItem;
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
            this._root = new System.Windows.Forms.TableLayoutPanel();
            this._headerPanel = new System.Windows.Forms.Panel();
            this._profileButton = new FontAwesome.Sharp.IconButton();
            this._avatarControl = new SIMS_WinFormsApp.UI.Controls.AvatarControl();
            this._userEmailLabel = new System.Windows.Forms.Label();
            this._userNameLabel = new System.Windows.Forms.Label();
            this._notificationsButton = new SIMS_WinFormsApp.UI.Controls.NotificationBellButton();
            this._subtitleLabel = new System.Windows.Forms.Label();
            this._brandLabel = new System.Windows.Forms.Label();
            this._logoIcon = new System.Windows.Forms.PictureBox();
            this._sidebarPanel = new System.Windows.Forms.Panel();
            this._navigationTree = new System.Windows.Forms.TreeView();
            this._sidebarToggleButton = new FontAwesome.Sharp.IconButton();
            this._menuLabel = new System.Windows.Forms.Label();
            this._contentHost = new System.Windows.Forms.Panel();
            this._footerPanel = new System.Windows.Forms.Panel();
            this._footerStatusLabel = new System.Windows.Forms.Label();
            this._accountContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._profileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._logoutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._notificationsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._notificationsTitleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._notificationsEmptyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._settingsButton = new System.Windows.Forms.Button();
            this._chatbotButton = new System.Windows.Forms.Button();
            this._root.SuspendLayout();
            this._headerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._logoIcon)).BeginInit();
            this._sidebarPanel.SuspendLayout();
            this._footerPanel.SuspendLayout();
            this._accountContextMenu.SuspendLayout();
            this._notificationsContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // _root
            // 
            this._root.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this._root.ColumnCount = 2;
            this._root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 240F));
            this._root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Controls.Add(this._headerPanel, 0, 0);
            this._root.Controls.Add(this._sidebarPanel, 0, 1);
            this._root.Controls.Add(this._contentHost, 1, 1);
            this._root.Controls.Add(this._footerPanel, 0, 2);
            this._root.Dock = System.Windows.Forms.DockStyle.Fill;
            this._root.Location = new System.Drawing.Point(0, 0);
            this._root.Margin = new System.Windows.Forms.Padding(0);
            this._root.Name = "_root";
            this._root.RowCount = 3;
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this._root.Size = new System.Drawing.Size(1264, 774);
            this._root.TabIndex = 0;
            // 
            // _headerPanel
            // 
            this._headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._root.SetColumnSpan(this._headerPanel, 2);
            this._headerPanel.Controls.Add(this._profileButton);
            this._headerPanel.Controls.Add(this._avatarControl);
            this._headerPanel.Controls.Add(this._userEmailLabel);
            this._headerPanel.Controls.Add(this._userNameLabel);
            this._headerPanel.Controls.Add(this._notificationsButton);
            this._headerPanel.Controls.Add(this._subtitleLabel);
            this._headerPanel.Controls.Add(this._brandLabel);
            this._headerPanel.Controls.Add(this._logoIcon);
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._headerPanel.Location = new System.Drawing.Point(0, 0);
            this._headerPanel.Margin = new System.Windows.Forms.Padding(0);
            this._headerPanel.Name = "_headerPanel";
            this._headerPanel.Size = new System.Drawing.Size(1264, 86);
            this._headerPanel.TabIndex = 0;
            // 
            // _profileButton
            // 
            this._profileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._profileButton.FlatAppearance.BorderSize = 0;
            this._profileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._profileButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this._profileButton.IconChar = FontAwesome.Sharp.IconChar.ChevronDown;
            this._profileButton.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this._profileButton.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this._profileButton.IconSize = 14;
            this._profileButton.Location = new System.Drawing.Point(1224, 22);
            this._profileButton.Name = "_profileButton";
            this._profileButton.Size = new System.Drawing.Size(30, 42);
            this._profileButton.TabIndex = 0;
            this._profileButton.UseVisualStyleBackColor = false;
            // 
            // _avatarControl
            // 
            this._avatarControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._avatarControl.BackColor = System.Drawing.Color.Transparent;
            this._avatarControl.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(58)))), ((int)(((byte)(237)))));
            this._avatarControl.ForeColor = System.Drawing.Color.White;
            this._avatarControl.Initial = "A";
            this._avatarControl.Location = new System.Drawing.Point(1174, 22);
            this._avatarControl.Name = "_avatarControl";
            this._avatarControl.RingColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._avatarControl.RingWidth = 2;
            this._avatarControl.Size = new System.Drawing.Size(42, 42);
            this._avatarControl.TabIndex = 1;
            // 
            // _userEmailLabel
            // 
            this._userEmailLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._userEmailLabel.AutoEllipsis = true;
            this._userEmailLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._userEmailLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this._userEmailLabel.Location = new System.Drawing.Point(944, 43);
            this._userEmailLabel.Name = "_userEmailLabel";
            this._userEmailLabel.Size = new System.Drawing.Size(220, 20);
            this._userEmailLabel.TabIndex = 3;
            this._userEmailLabel.Text = "admin@sims.local";
            this._userEmailLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _userNameLabel
            // 
            this._userNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._userNameLabel.AutoEllipsis = true;
            this._userNameLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._userNameLabel.ForeColor = System.Drawing.Color.White;
            this._userNameLabel.Location = new System.Drawing.Point(944, 18);
            this._userNameLabel.Name = "_userNameLabel";
            this._userNameLabel.Size = new System.Drawing.Size(220, 24);
            this._userNameLabel.TabIndex = 4;
            this._userNameLabel.Text = "Admin";
            this._userNameLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // _notificationsButton
            // 
            this._notificationsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._notificationsButton.BackColor = System.Drawing.Color.Transparent;
            this._notificationsButton.Count = 2;
            this._notificationsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this._notificationsButton.Location = new System.Drawing.Point(884, 17);
            this._notificationsButton.Name = "_notificationsButton";
            this._notificationsButton.Size = new System.Drawing.Size(52, 52);
            this._notificationsButton.TabIndex = 5;
            // 
            // _subtitleLabel
            // 
            this._subtitleLabel.AutoSize = true;
            this._subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this._subtitleLabel.Location = new System.Drawing.Point(89, 53);
            this._subtitleLabel.Name = "_subtitleLabel";
            this._subtitleLabel.Size = new System.Drawing.Size(257, 25);
            this._subtitleLabel.TabIndex = 7;
            this._subtitleLabel.Text = "Cửa hàng điện thoại trực tuyến";
            // 
            // _brandLabel
            // 
            this._brandLabel.AutoSize = true;
            this._brandLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this._brandLabel.ForeColor = System.Drawing.Color.White;
            this._brandLabel.Location = new System.Drawing.Point(86, 10);
            this._brandLabel.Name = "_brandLabel";
            this._brandLabel.Size = new System.Drawing.Size(118, 54);
            this._brandLabel.TabIndex = 8;
            this._brandLabel.Text = "SIMS";
            // 
            // _logoIcon
            // 
            this._logoIcon.BackColor = System.Drawing.Color.Transparent;
            this._logoIcon.Image = global::SIMS_WinFormsApp.Properties.Resources.logo_icon;
            this._logoIcon.Location = new System.Drawing.Point(24, 19);
            this._logoIcon.Name = "_logoIcon";
            this._logoIcon.Size = new System.Drawing.Size(48, 48);
            this._logoIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._logoIcon.TabIndex = 9;
            this._logoIcon.TabStop = false;
            // 
            // _sidebarPanel
            // 
            this._sidebarPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(35)))), ((int)(((byte)(52)))));
            this._sidebarPanel.Controls.Add(this._navigationTree);
            this._sidebarPanel.Controls.Add(this._sidebarToggleButton);
            this._sidebarPanel.Controls.Add(this._menuLabel);
            this._sidebarPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._sidebarPanel.Location = new System.Drawing.Point(0, 86);
            this._sidebarPanel.Margin = new System.Windows.Forms.Padding(0);
            this._sidebarPanel.Name = "_sidebarPanel";
            this._sidebarPanel.Size = new System.Drawing.Size(240, 654);
            this._sidebarPanel.TabIndex = 1;
            // 
            // _navigationTree
            // 
            this._navigationTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._navigationTree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(35)))), ((int)(((byte)(52)))));
            this._navigationTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._navigationTree.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._navigationTree.ForeColor = System.Drawing.Color.White;
            this._navigationTree.FullRowSelect = true;
            this._navigationTree.HideSelection = false;
            this._navigationTree.Indent = 18;
            this._navigationTree.ItemHeight = 34;
            this._navigationTree.Location = new System.Drawing.Point(0, 48);
            this._navigationTree.Name = "_navigationTree";
            this._navigationTree.ShowLines = false;
            this._navigationTree.ShowPlusMinus = false;
            this._navigationTree.ShowRootLines = false;
            this._navigationTree.Size = new System.Drawing.Size(280, 1110);
            this._navigationTree.TabIndex = 0;
            // 
            // _sidebarToggleButton
            // 
            this._sidebarToggleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._sidebarToggleButton.FlatAppearance.BorderSize = 0;
            this._sidebarToggleButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._sidebarToggleButton.ForeColor = System.Drawing.Color.White;
            this._sidebarToggleButton.IconChar = FontAwesome.Sharp.IconChar.Navicon;
            this._sidebarToggleButton.IconColor = System.Drawing.Color.White;
            this._sidebarToggleButton.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this._sidebarToggleButton.IconSize = 18;
            this._sidebarToggleButton.Location = new System.Drawing.Point(230, 6);
            this._sidebarToggleButton.Name = "_sidebarToggleButton";
            this._sidebarToggleButton.Size = new System.Drawing.Size(42, 36);
            this._sidebarToggleButton.TabIndex = 1;
            this._sidebarToggleButton.UseVisualStyleBackColor = false;
            // 
            // _menuLabel
            // 
            this._menuLabel.AutoSize = true;
            this._menuLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._menuLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this._menuLabel.Location = new System.Drawing.Point(16, 17);
            this._menuLabel.Name = "_menuLabel";
            this._menuLabel.Size = new System.Drawing.Size(59, 21);
            this._menuLabel.TabIndex = 2;
            this._menuLabel.Text = "MENU";
            // 
            // _contentHost
            // 
            this._contentHost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this._contentHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentHost.Location = new System.Drawing.Point(240, 86);
            this._contentHost.Margin = new System.Windows.Forms.Padding(0);
            this._contentHost.Name = "_contentHost";
            this._contentHost.Size = new System.Drawing.Size(1024, 654);
            this._contentHost.TabIndex = 2;
            // 
            // _footerPanel
            // 
            this._footerPanel.BackColor = System.Drawing.Color.White;
            this._root.SetColumnSpan(this._footerPanel, 2);
            this._footerPanel.Controls.Add(this._footerStatusLabel);
            this._footerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._footerPanel.Location = new System.Drawing.Point(0, 740);
            this._footerPanel.Margin = new System.Windows.Forms.Padding(0);
            this._footerPanel.Name = "_footerPanel";
            this._footerPanel.Size = new System.Drawing.Size(1264, 34);
            this._footerPanel.TabIndex = 3;
            // 
            // _footerStatusLabel
            // 
            this._footerStatusLabel.AutoSize = true;
            this._footerStatusLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._footerStatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._footerStatusLabel.Location = new System.Drawing.Point(16, 9);
            this._footerStatusLabel.Name = "_footerStatusLabel";
            this._footerStatusLabel.Size = new System.Drawing.Size(206, 21);
            this._footerStatusLabel.TabIndex = 0;
            this._footerStatusLabel.Text = "●  Hệ thống đang hoạt động";
            // 
            // _accountContextMenu
            // 
            this._accountContextMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._accountContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._profileMenuItem,
            this._logoutMenuItem});
            this._accountContextMenu.Name = "_accountContextMenu";
            this._accountContextMenu.Size = new System.Drawing.Size(199, 68);
            // 
            // _profileMenuItem
            // 
            this._profileMenuItem.Name = "_profileMenuItem";
            this._profileMenuItem.Size = new System.Drawing.Size(198, 32);
            this._profileMenuItem.Text = "Hồ sơ cá nhân";
            // 
            // _logoutMenuItem
            // 
            this._logoutMenuItem.Name = "_logoutMenuItem";
            this._logoutMenuItem.Size = new System.Drawing.Size(198, 32);
            this._logoutMenuItem.Text = "Đăng xuất";
            // 
            // _notificationsContextMenu
            // 
            this._notificationsContextMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this._notificationsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._notificationsTitleMenuItem,
            this._notificationsEmptyMenuItem});
            this._notificationsContextMenu.Name = "_notificationsContextMenu";
            this._notificationsContextMenu.Size = new System.Drawing.Size(286, 68);
            // 
            // _notificationsTitleMenuItem
            // 
            this._notificationsTitleMenuItem.Enabled = false;
            this._notificationsTitleMenuItem.Name = "_notificationsTitleMenuItem";
            this._notificationsTitleMenuItem.Size = new System.Drawing.Size(285, 32);
            this._notificationsTitleMenuItem.Text = "Thông báo";
            // 
            // _notificationsEmptyMenuItem
            // 
            this._notificationsEmptyMenuItem.Enabled = false;
            this._notificationsEmptyMenuItem.Name = "_notificationsEmptyMenuItem";
            this._notificationsEmptyMenuItem.Size = new System.Drawing.Size(285, 32);
            this._notificationsEmptyMenuItem.Text = "Không có thông báo mới";
            // 
            // _settingsButton
            // 
            this._settingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._settingsButton.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this._settingsButton.Location = new System.Drawing.Point(1174, 704);
            this._settingsButton.Name = "_settingsButton";
            this._settingsButton.Size = new System.Drawing.Size(54, 54);
            this._settingsButton.TabIndex = 2;
            this._settingsButton.Text = "⚙";
            this._settingsButton.UseVisualStyleBackColor = true;
            // 
            // _chatbotButton
            // 
            this._chatbotButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._chatbotButton.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this._chatbotButton.Location = new System.Drawing.Point(1110, 704);
            this._chatbotButton.Name = "_chatbotButton";
            this._chatbotButton.Size = new System.Drawing.Size(54, 54);
            this._chatbotButton.TabIndex = 1;
            this._chatbotButton.Text = "AI";
            this._chatbotButton.UseVisualStyleBackColor = true;
            // 
            // MainLayoutControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.Controls.Add(this._root);
            this.Controls.Add(this._chatbotButton);
            this.Controls.Add(this._settingsButton);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MainLayoutControl";
            this.Size = new System.Drawing.Size(1264, 774);
            this._root.ResumeLayout(false);
            this._headerPanel.ResumeLayout(false);
            this._headerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._logoIcon)).EndInit();
            this._sidebarPanel.ResumeLayout(false);
            this._sidebarPanel.PerformLayout();
            this._footerPanel.ResumeLayout(false);
            this._footerPanel.PerformLayout();
            this._accountContextMenu.ResumeLayout(false);
            this._notificationsContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}

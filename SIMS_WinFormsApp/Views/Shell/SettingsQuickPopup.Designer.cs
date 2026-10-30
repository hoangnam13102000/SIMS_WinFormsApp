using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Shell
{
    partial class SettingsQuickPopup
    {
        private Panel _headerIconPanel;
        private IconPictureBox _headerIcon;
        private Label _titleLabel;
        private Label _subtitleLabel;
        private Label _sectionLabel;
        private Panel _contentPanel;
        private Panel _rowsPanel;
        private Panel _separator;
        private Panel _languageRow;
        private IconPictureBox _languageIcon;
        private Label _languageLabel;
        private Label _languageValue;
        private Button _languageButton;
        private Panel _notificationsRow;
        private IconPictureBox _notificationsIcon;
        private Label _notificationsLabel;
        private Label _notificationsHint;
        private SettingsToggleSwitch _notificationsToggle;
        private Label _footerLabel;

        private void InitializeComponent()
        {
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this.header = new System.Windows.Forms.Panel();
            this._headerIconPanel = new System.Windows.Forms.Panel();
            this._headerIcon = new FontAwesome.Sharp.IconPictureBox();
            this._titleLabel = new System.Windows.Forms.Label();
            this._subtitleLabel = new System.Windows.Forms.Label();
            this._sectionLabel = new System.Windows.Forms.Label();
            this._rowsPanel = new System.Windows.Forms.Panel();
            this._notificationsRow = new System.Windows.Forms.Panel();
            this._separator = new System.Windows.Forms.Panel();
            this._notificationsIcon = new FontAwesome.Sharp.IconPictureBox();
            this._notificationsLabel = new System.Windows.Forms.Label();
            this._notificationsHint = new System.Windows.Forms.Label();
            this._notificationsToggle = new SIMS_WinFormsApp.Views.Shell.SettingsToggleSwitch();
            this._languageRow = new System.Windows.Forms.Panel();
            this._languageIcon = new FontAwesome.Sharp.IconPictureBox();
            this._languageLabel = new System.Windows.Forms.Label();
            this._languageValue = new System.Windows.Forms.Label();
            this._languageButton = new System.Windows.Forms.Button();
            this._footerLabel = new System.Windows.Forms.Label();
            this._contentPanel = new System.Windows.Forms.Panel();
            this.layout.SuspendLayout();
            this.header.SuspendLayout();
            this._headerIconPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).BeginInit();
            this._rowsPanel.SuspendLayout();
            this._notificationsRow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._notificationsIcon)).BeginInit();
            this._languageRow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._languageIcon)).BeginInit();
            this._contentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.ColumnCount = 1;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout.Controls.Add(this.header, 0, 0);
            this.layout.Controls.Add(this._sectionLabel, 0, 1);
            this.layout.Controls.Add(this._rowsPanel, 0, 2);
            this.layout.Controls.Add(this._footerLabel, 0, 3);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(20, 17);
            this.layout.Margin = new System.Windows.Forms.Padding(0);
            this.layout.Name = "layout";
            this.layout.RowCount = 4;
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.layout.Size = new System.Drawing.Size(338, 287);
            this.layout.TabIndex = 0;
            // 
            // header
            // 
            this.header.BackColor = System.Drawing.Color.Transparent;
            this.header.Controls.Add(this._headerIconPanel);
            this.header.Controls.Add(this._titleLabel);
            this.header.Controls.Add(this._subtitleLabel);
            this.header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Margin = new System.Windows.Forms.Padding(0);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(338, 50);
            this.header.TabIndex = 0;
            // 
            // _headerIconPanel
            // 
            this._headerIconPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(243)))), ((int)(((byte)(251)))));
            this._headerIconPanel.Controls.Add(this._headerIcon);
            this._headerIconPanel.Location = new System.Drawing.Point(0, 3);
            this._headerIconPanel.Name = "_headerIconPanel";
            this._headerIconPanel.Size = new System.Drawing.Size(40, 40);
            this._headerIconPanel.TabIndex = 0;
            // 
            // _headerIcon
            // 
            this._headerIcon.BackColor = System.Drawing.Color.Transparent;
            this._headerIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._headerIcon.IconChar = FontAwesome.Sharp.IconChar.Cog;
            this._headerIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._headerIcon.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this._headerIcon.IconSize = 20;
            this._headerIcon.Location = new System.Drawing.Point(10, 10);
            this._headerIcon.Name = "_headerIcon";
            this._headerIcon.Size = new System.Drawing.Size(20, 20);
            this._headerIcon.TabIndex = 0;
            this._headerIcon.TabStop = false;
            // 
            // _titleLabel
            // 
            this._titleLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._titleLabel.Location = new System.Drawing.Point(52, 0);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(280, 23);
            this._titleLabel.TabIndex = 1;
            this._titleLabel.Text = "Cài đặt nhanh";
            // 
            // _subtitleLabel
            // 
            this._subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._subtitleLabel.Location = new System.Drawing.Point(53, 24);
            this._subtitleLabel.Name = "_subtitleLabel";
            this._subtitleLabel.Size = new System.Drawing.Size(280, 20);
            this._subtitleLabel.TabIndex = 2;
            this._subtitleLabel.Text = "Cá nhân hóa trải nghiệm của bạn";
            // 
            // _sectionLabel
            // 
            this._sectionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._sectionLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._sectionLabel.Location = new System.Drawing.Point(3, 50);
            this._sectionLabel.Name = "_sectionLabel";
            this._sectionLabel.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this._sectionLabel.Size = new System.Drawing.Size(332, 28);
            this._sectionLabel.TabIndex = 1;
            this._sectionLabel.Text = "TÙY CHỈNH";
            this._sectionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _rowsPanel
            // 
            this._rowsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._rowsPanel.Controls.Add(this._notificationsRow);
            this._rowsPanel.Controls.Add(this._languageRow);
            this._rowsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rowsPanel.Location = new System.Drawing.Point(0, 78);
            this._rowsPanel.Margin = new System.Windows.Forms.Padding(0);
            this._rowsPanel.Name = "_rowsPanel";
            this._rowsPanel.Size = new System.Drawing.Size(338, 178);
            this._rowsPanel.TabIndex = 2;
            // 
            // _notificationsRow
            // 
            this._notificationsRow.BackColor = System.Drawing.Color.Transparent;
            this._notificationsRow.Controls.Add(this._separator);
            this._notificationsRow.Controls.Add(this._notificationsIcon);
            this._notificationsRow.Controls.Add(this._notificationsLabel);
            this._notificationsRow.Controls.Add(this._notificationsHint);
            this._notificationsRow.Controls.Add(this._notificationsToggle);
            this._notificationsRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this._notificationsRow.Location = new System.Drawing.Point(0, 72);
            this._notificationsRow.Name = "_notificationsRow";
            this._notificationsRow.Size = new System.Drawing.Size(338, 106);
            this._notificationsRow.TabIndex = 0;
            // 
            // _separator
            // 
            this._separator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._separator.Dock = System.Windows.Forms.DockStyle.Top;
            this._separator.Location = new System.Drawing.Point(0, 0);
            this._separator.Name = "_separator";
            this._separator.Size = new System.Drawing.Size(338, 1);
            this._separator.TabIndex = 0;
            // 
            // _notificationsIcon
            // 
            this._notificationsIcon.BackColor = System.Drawing.Color.Transparent;
            this._notificationsIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._notificationsIcon.IconChar = FontAwesome.Sharp.IconChar.Bell;
            this._notificationsIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._notificationsIcon.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this._notificationsIcon.IconSize = 18;
            this._notificationsIcon.Location = new System.Drawing.Point(10, 25);
            this._notificationsIcon.Name = "_notificationsIcon";
            this._notificationsIcon.Size = new System.Drawing.Size(18, 18);
            this._notificationsIcon.TabIndex = 1;
            this._notificationsIcon.TabStop = false;
            // 
            // _notificationsLabel
            // 
            this._notificationsLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._notificationsLabel.Location = new System.Drawing.Point(39, 13);
            this._notificationsLabel.Name = "_notificationsLabel";
            this._notificationsLabel.Size = new System.Drawing.Size(180, 20);
            this._notificationsLabel.TabIndex = 2;
            this._notificationsLabel.Text = "Thông báo";
            this._notificationsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _notificationsHint
            // 
            this._notificationsHint.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._notificationsHint.Location = new System.Drawing.Point(39, 34);
            this._notificationsHint.Name = "_notificationsHint";
            this._notificationsHint.Size = new System.Drawing.Size(180, 18);
            this._notificationsHint.TabIndex = 3;
            this._notificationsHint.Text = "Nhận cập nhật mới";
            this._notificationsHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _notificationsToggle
            // 
            this._notificationsToggle.AccessibleName = "Thông báo";
            this._notificationsToggle.AccessibleRole = System.Windows.Forms.AccessibleRole.CheckButton;
            this._notificationsToggle.Checked = true;
            this._notificationsToggle.Cursor = System.Windows.Forms.Cursors.Hand;
            this._notificationsToggle.Location = new System.Drawing.Point(278, 25);
            this._notificationsToggle.Name = "_notificationsToggle";
            this._notificationsToggle.Size = new System.Drawing.Size(42, 24);
            this._notificationsToggle.TabIndex = 4;
            // 
            // _languageRow
            // 
            this._languageRow.BackColor = System.Drawing.Color.Transparent;
            this._languageRow.Controls.Add(this._languageIcon);
            this._languageRow.Controls.Add(this._languageLabel);
            this._languageRow.Controls.Add(this._languageValue);
            this._languageRow.Controls.Add(this._languageButton);
            this._languageRow.Dock = System.Windows.Forms.DockStyle.Top;
            this._languageRow.Location = new System.Drawing.Point(0, 0);
            this._languageRow.Name = "_languageRow";
            this._languageRow.Size = new System.Drawing.Size(338, 72);
            this._languageRow.TabIndex = 1;
            // 
            // _languageIcon
            // 
            this._languageIcon.BackColor = System.Drawing.Color.Transparent;
            this._languageIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._languageIcon.IconChar = FontAwesome.Sharp.IconChar.Globe;
            this._languageIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._languageIcon.IconFont = FontAwesome.Sharp.IconFont.Solid;
            this._languageIcon.IconSize = 18;
            this._languageIcon.Location = new System.Drawing.Point(10, 23);
            this._languageIcon.Name = "_languageIcon";
            this._languageIcon.Size = new System.Drawing.Size(18, 18);
            this._languageIcon.TabIndex = 0;
            this._languageIcon.TabStop = false;
            // 
            // _languageLabel
            // 
            this._languageLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._languageLabel.Location = new System.Drawing.Point(39, 12);
            this._languageLabel.Name = "_languageLabel";
            this._languageLabel.Size = new System.Drawing.Size(170, 20);
            this._languageLabel.TabIndex = 1;
            this._languageLabel.Text = "Ngôn ngữ";
            this._languageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _languageValue
            // 
            this._languageValue.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._languageValue.Location = new System.Drawing.Point(39, 33);
            this._languageValue.Name = "_languageValue";
            this._languageValue.Size = new System.Drawing.Size(170, 18);
            this._languageValue.TabIndex = 2;
            this._languageValue.Text = "Tiếng Việt";
            this._languageValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _languageButton
            // 
            this._languageButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this._languageButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this._languageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._languageButton.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._languageButton.Location = new System.Drawing.Point(261, 19);
            this._languageButton.Name = "_languageButton";
            this._languageButton.Size = new System.Drawing.Size(72, 36);
            this._languageButton.TabIndex = 3;
            this._languageButton.Text = "VI";
            this._languageButton.UseVisualStyleBackColor = false;
            // 
            // _footerLabel
            // 
            this._footerLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._footerLabel.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._footerLabel.Location = new System.Drawing.Point(3, 256);
            this._footerLabel.Name = "_footerLabel";
            this._footerLabel.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this._footerLabel.Size = new System.Drawing.Size(332, 31);
            this._footerLabel.TabIndex = 3;
            this._footerLabel.Text = "Thay đổi được lưu tự động";
            this._footerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _contentPanel
            // 
            this._contentPanel.BackColor = System.Drawing.Color.White;
            this._contentPanel.Controls.Add(this.layout);
            this._contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentPanel.Location = new System.Drawing.Point(1, 1);
            this._contentPanel.Name = "_contentPanel";
            this._contentPanel.Padding = new System.Windows.Forms.Padding(20, 17, 20, 14);
            this._contentPanel.Size = new System.Drawing.Size(378, 318);
            this._contentPanel.TabIndex = 0;
            // 
            // SettingsQuickPopup
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(380, 320);
            this.Controls.Add(this._contentPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsQuickPopup";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Cài đặt nhanh";
            this.layout.ResumeLayout(false);
            this.header.ResumeLayout(false);
            this._headerIconPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).EndInit();
            this._rowsPanel.ResumeLayout(false);
            this._notificationsRow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._notificationsIcon)).EndInit();
            this._languageRow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._languageIcon)).EndInit();
            this._contentPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private TableLayoutPanel layout;
        private Panel header;
    }
}

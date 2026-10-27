using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Chat
{
    partial class ucChat
    {
        private Panel _leftPanel;
        private Panel _rightPanel;
        private ListBox _userList;
        private Label _statusLabel;
        private Label _peerTitleLabel;
        private FlowLayoutPanel _messagesPanel;
        private TextBox _inputBox;
        private Button _btnSend;
        private Label _emptyHint;
        private Label _leftTitleLabel;
        private Panel _designerHeader;
        private Panel _designerInputBar;

        private void InitializeComponent()
        {
            this._leftPanel = new System.Windows.Forms.Panel();
            this._userList = new System.Windows.Forms.ListBox();
            this._emptyHint = new System.Windows.Forms.Label();
            this._statusLabel = new System.Windows.Forms.Label();
            this._leftTitleLabel = new System.Windows.Forms.Label();
            this._rightPanel = new System.Windows.Forms.Panel();
            this._messagesPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._designerInputBar = new System.Windows.Forms.Panel();
            this._inputBox = new System.Windows.Forms.TextBox();
            this._btnSend = new System.Windows.Forms.Button();
            this._designerHeader = new System.Windows.Forms.Panel();
            this._peerTitleLabel = new System.Windows.Forms.Label();
            this._leftPanel.SuspendLayout();
            this._rightPanel.SuspendLayout();
            this._designerInputBar.SuspendLayout();
            this._designerHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // _leftPanel
            // 
            this._leftPanel.BackColor = System.Drawing.SystemColors.Window;
            this._leftPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._leftPanel.Controls.Add(this._userList);
            this._leftPanel.Controls.Add(this._emptyHint);
            this._leftPanel.Controls.Add(this._statusLabel);
            this._leftPanel.Controls.Add(this._leftTitleLabel);
            this._leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this._leftPanel.Location = new System.Drawing.Point(0, 0);
            this._leftPanel.Name = "_leftPanel";
            this._leftPanel.Padding = new System.Windows.Forms.Padding(12);
            this._leftPanel.Size = new System.Drawing.Size(320, 600);
            this._leftPanel.TabIndex = 1;
            // 
            // _userList
            // 
            this._userList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._userList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._userList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this._userList.IntegralHeight = false;
            this._userList.ItemHeight = 66;
            this._userList.Location = new System.Drawing.Point(12, 62);
            this._userList.Name = "_userList";
            this._userList.Size = new System.Drawing.Size(294, 484);
            this._userList.TabIndex = 0;
            // 
            // _emptyHint
            // 
            this._emptyHint.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._emptyHint.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._emptyHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._emptyHint.Location = new System.Drawing.Point(12, 546);
            this._emptyHint.Name = "_emptyHint";
            this._emptyHint.Size = new System.Drawing.Size(294, 40);
            this._emptyHint.TabIndex = 1;
            this._emptyHint.Text = "Chưa có người dùng trực tuyến";
            this._emptyHint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _statusLabel
            // 
            this._statusLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._statusLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._statusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._statusLabel.Location = new System.Drawing.Point(12, 40);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(294, 22);
            this._statusLabel.TabIndex = 2;
            this._statusLabel.Text = "Đang kết nối...";
            // 
            // _leftTitleLabel
            // 
            this._leftTitleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._leftTitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._leftTitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._leftTitleLabel.Location = new System.Drawing.Point(12, 12);
            this._leftTitleLabel.Name = "_leftTitleLabel";
            this._leftTitleLabel.Size = new System.Drawing.Size(294, 28);
            this._leftTitleLabel.TabIndex = 3;
            this._leftTitleLabel.Text = "Người dùng trực tuyến";
            // 
            // _rightPanel
            // 
            this._rightPanel.Controls.Add(this._messagesPanel);
            this._rightPanel.Controls.Add(this._designerInputBar);
            this._rightPanel.Controls.Add(this._designerHeader);
            this._rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rightPanel.Location = new System.Drawing.Point(320, 0);
            this._rightPanel.Name = "_rightPanel";
            this._rightPanel.Size = new System.Drawing.Size(580, 600);
            this._rightPanel.TabIndex = 0;
            // 
            // _messagesPanel
            // 
            this._messagesPanel.AutoScroll = true;
            this._messagesPanel.BackColor = System.Drawing.SystemColors.Control;
            this._messagesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._messagesPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this._messagesPanel.Location = new System.Drawing.Point(0, 56);
            this._messagesPanel.Name = "_messagesPanel";
            this._messagesPanel.Size = new System.Drawing.Size(580, 480);
            this._messagesPanel.TabIndex = 0;
            this._messagesPanel.WrapContents = false;
            // 
            // _designerInputBar
            // 
            this._designerInputBar.BackColor = System.Drawing.SystemColors.Window;
            this._designerInputBar.Controls.Add(this._inputBox);
            this._designerInputBar.Controls.Add(this._btnSend);
            this._designerInputBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._designerInputBar.Location = new System.Drawing.Point(0, 536);
            this._designerInputBar.Name = "_designerInputBar";
            this._designerInputBar.Padding = new System.Windows.Forms.Padding(12, 10, 12, 10);
            this._designerInputBar.Size = new System.Drawing.Size(580, 64);
            this._designerInputBar.TabIndex = 1;
            // 
            // _inputBox
            // 
            this._inputBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._inputBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._inputBox.Location = new System.Drawing.Point(12, 10);
            this._inputBox.Name = "_inputBox";
            this._inputBox.Size = new System.Drawing.Size(456, 26);
            this._inputBox.TabIndex = 0;
            // 
            // _btnSend
            // 
            this._btnSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._btnSend.Dock = System.Windows.Forms.DockStyle.Right;
            this._btnSend.FlatAppearance.BorderSize = 0;
            this._btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnSend.ForeColor = System.Drawing.Color.White;
            this._btnSend.Location = new System.Drawing.Point(468, 10);
            this._btnSend.Name = "_btnSend";
            this._btnSend.Size = new System.Drawing.Size(100, 44);
            this._btnSend.TabIndex = 1;
            this._btnSend.Text = "Gửi";
            this._btnSend.UseVisualStyleBackColor = false;
            // 
            // _designerHeader
            // 
            this._designerHeader.BackColor = System.Drawing.SystemColors.Window;
            this._designerHeader.Controls.Add(this._peerTitleLabel);
            this._designerHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._designerHeader.Location = new System.Drawing.Point(0, 0);
            this._designerHeader.Name = "_designerHeader";
            this._designerHeader.Padding = new System.Windows.Forms.Padding(16, 0, 16, 0);
            this._designerHeader.Size = new System.Drawing.Size(580, 56);
            this._designerHeader.TabIndex = 2;
            // 
            // _peerTitleLabel
            // 
            this._peerTitleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._peerTitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._peerTitleLabel.Location = new System.Drawing.Point(16, 0);
            this._peerTitleLabel.Name = "_peerTitleLabel";
            this._peerTitleLabel.Size = new System.Drawing.Size(548, 56);
            this._peerTitleLabel.TabIndex = 0;
            this._peerTitleLabel.Text = "Chọn cuộc hội thoại";
            this._peerTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucChat
            // 
            this.Controls.Add(this._rightPanel);
            this.Controls.Add(this._leftPanel);
            this.Name = "ucChat";
            this.Size = new System.Drawing.Size(900, 600);
            this._leftPanel.ResumeLayout(false);
            this._rightPanel.ResumeLayout(false);
            this._designerInputBar.ResumeLayout(false);
            this._designerInputBar.PerformLayout();
            this._designerHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}

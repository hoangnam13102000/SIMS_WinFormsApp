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
            _leftPanel = new Panel();
            _rightPanel = new Panel();
            _userList = new ListBox();
            _statusLabel = new Label();
            _peerTitleLabel = new Label();
            _messagesPanel = new FlowLayoutPanel();
            _inputBox = new TextBox();
            _btnSend = new Button();
            _emptyHint = new Label();
            _leftTitleLabel = new Label();
            _designerHeader = new Panel();
            _designerInputBar = new Panel();
            SuspendLayout();
            _leftPanel.SuspendLayout();
            _rightPanel.SuspendLayout();
            _designerHeader.SuspendLayout();
            _designerInputBar.SuspendLayout();

            _leftPanel.BackColor = SystemColors.Window;
            _leftPanel.Dock = DockStyle.Left;
            _leftPanel.Padding = new Padding(12);
            _leftPanel.Name = "onlineUsersPanel";
            _leftPanel.BorderStyle = BorderStyle.FixedSingle;
            _leftPanel.Size = new Size(320, 520);

            _leftTitleLabel.Dock = DockStyle.Top;
            _leftTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _leftTitleLabel.ForeColor = Color.FromArgb(100, 116, 139);
            _leftTitleLabel.Height = 28;
            _leftTitleLabel.Name = "onlineUsersTitle";
            _leftTitleLabel.Text = "Người dùng trực tuyến";

            _statusLabel.Dock = DockStyle.Top;
            _statusLabel.Height = 22;
            _statusLabel.Font = new Font("Segoe UI", 8.5F);
            _statusLabel.ForeColor = Color.FromArgb(100, 116, 139);
            _statusLabel.Text = "Đang kết nối...";
            _statusLabel.Name = "connectionStatus";

            _userList.BorderStyle = BorderStyle.None;
            _userList.Dock = DockStyle.Fill;
            _userList.DrawMode = DrawMode.OwnerDrawFixed;
            _userList.ItemHeight = 66;
            _userList.IntegralHeight = false;
            _userList.Name = "onlineUsers";

            _emptyHint.Dock = DockStyle.Bottom;
            _emptyHint.Height = 40;
            _emptyHint.Font = new Font("Segoe UI", 8.5F);
            _emptyHint.ForeColor = Color.FromArgb(100, 116, 139);
            _emptyHint.Text = "Chưa có người dùng trực tuyến";
            _emptyHint.TextAlign = ContentAlignment.MiddleCenter;
            _leftPanel.Controls.Add(_userList);
            _leftPanel.Controls.Add(_emptyHint);
            _leftPanel.Controls.Add(_statusLabel);
            _leftPanel.Controls.Add(_leftTitleLabel);

            _rightPanel.Dock = DockStyle.Fill;
            _rightPanel.Name = "conversationPanel";

            _designerHeader.Dock = DockStyle.Top;
            _designerHeader.BackColor = SystemColors.Window;
            _designerHeader.Padding = new Padding(16, 0, 16, 0);
            _designerHeader.Height = 56;
            _peerTitleLabel.Dock = DockStyle.Fill;
            _peerTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _peerTitleLabel.Text = "Chọn cuộc hội thoại";
            _peerTitleLabel.TextAlign = ContentAlignment.MiddleLeft;
            _designerHeader.Controls.Add(_peerTitleLabel);

            _messagesPanel.AutoScroll = true;
            _messagesPanel.BackColor = SystemColors.Control;
            _messagesPanel.Dock = DockStyle.Fill;
            _messagesPanel.FlowDirection = FlowDirection.TopDown;
            _messagesPanel.Name = "messagesPanel";
            _messagesPanel.WrapContents = false;

            _designerInputBar.Dock = DockStyle.Bottom;
            _designerInputBar.BackColor = SystemColors.Window;
            _designerInputBar.Padding = new Padding(12, 10, 12, 10);
            _designerInputBar.Height = 64;
            _inputBox.Dock = DockStyle.Fill;
            _inputBox.BorderStyle = BorderStyle.FixedSingle;
            _inputBox.Name = "messageInput";
            _btnSend.Dock = DockStyle.Right;
            _btnSend.BackColor = Color.FromArgb(37, 99, 235);
            _btnSend.FlatAppearance.BorderSize = 0;
            _btnSend.FlatStyle = FlatStyle.Flat;
            _btnSend.ForeColor = Color.White;
            _btnSend.Text = "Gửi";
            _btnSend.Width = 100;
            _designerInputBar.Controls.Add(_inputBox);
            _designerInputBar.Controls.Add(_btnSend);

            _rightPanel.Controls.Add(_messagesPanel);
            _rightPanel.Controls.Add(_designerInputBar);
            _rightPanel.Controls.Add(_designerHeader);
            Controls.Add(_rightPanel);
            Controls.Add(_leftPanel);
            Name = "ucChat";
            Size = new Size(900, 600);
            _designerInputBar.ResumeLayout(false);
            _designerInputBar.PerformLayout();
            _designerHeader.ResumeLayout(false);
            _rightPanel.ResumeLayout(false);
            _leftPanel.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}

using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Chat
{
    partial class ucAiChat
    {
        private FlowLayoutPanel _messagesPanel;
        private TextBox _inputBox;
        private Button _sendButton;
        private Label _busyLabel;
        private Panel _designerHeader;
        private Panel _designerInputPanel;
        private Label _designerHeaderTitle;
        private Label _designerHeaderSubtitle;
        private Label _designerAvatar;
        private Button _designerCloseButton;

        private void InitializeComponent()
        {
            _messagesPanel = new FlowLayoutPanel();
            _inputBox = new TextBox();
            _sendButton = new Button();
            _busyLabel = new Label();
            _designerHeader = new Panel();
            _designerInputPanel = new Panel();
            _designerHeaderTitle = new Label();
            _designerHeaderSubtitle = new Label();
            _designerAvatar = new Label();
            _designerCloseButton = new Button();
            SuspendLayout();
            _designerHeader.SuspendLayout();
            _designerInputPanel.SuspendLayout();

            _designerHeader.BackColor = SystemColors.Window;
            _designerHeader.Dock = DockStyle.Top;
            _designerHeader.Height = 76;
            _designerHeader.Name = "aiChatHeader";
            _designerAvatar.BackColor = Color.FromArgb(219, 234, 254);
            _designerAvatar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _designerAvatar.ForeColor = Color.FromArgb(37, 99, 235);
            _designerAvatar.Location = new Point(16, 17);
            _designerAvatar.Name = "aiAvatar";
            _designerAvatar.Size = new Size(42, 42);
            _designerAvatar.Text = "AI";
            _designerAvatar.TextAlign = ContentAlignment.MiddleCenter;
            _designerHeaderTitle.AutoSize = true;
            _designerHeaderTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _designerHeaderTitle.Location = new Point(70, 17);
            _designerHeaderTitle.Name = "aiTitle";
            _designerHeaderTitle.Text = "Trợ lý AI";
            _designerHeaderSubtitle.AutoSize = true;
            _designerHeaderSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            _designerHeaderSubtitle.Location = new Point(71, 42);
            _designerHeaderSubtitle.Name = "aiSubtitle";
            _designerHeaderSubtitle.Text = "Hỏi đáp và hỗ trợ";
            _designerCloseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _designerCloseButton.FlatStyle = FlatStyle.Flat;
            _designerCloseButton.FlatAppearance.BorderSize = 0;
            _designerCloseButton.Font = new Font("Segoe UI", 14F);
            _designerCloseButton.ForeColor = Color.FromArgb(100, 116, 139);
            _designerCloseButton.Location = new Point(356, 17);
            _designerCloseButton.Name = "closeButton";
            _designerCloseButton.Size = new Size(36, 36);
            _designerCloseButton.Text = "×";
            _designerHeader.Controls.Add(_designerAvatar);
            _designerHeader.Controls.Add(_designerHeaderTitle);
            _designerHeader.Controls.Add(_designerHeaderSubtitle);
            _designerHeader.Controls.Add(_designerCloseButton);

            _messagesPanel.AutoScroll = true;
            _messagesPanel.BackColor = SystemColors.Control;
            _messagesPanel.Dock = DockStyle.Fill;
            _messagesPanel.FlowDirection = FlowDirection.TopDown;
            _messagesPanel.Name = "messagesPanel";
            _messagesPanel.WrapContents = false;

            _designerInputPanel.BackColor = SystemColors.Window;
            _designerInputPanel.Dock = DockStyle.Bottom;
            _designerInputPanel.Height = 88;
            _designerInputPanel.Padding = new Padding(14, 12, 14, 12);
            _designerInputPanel.Name = "aiChatInputPanel";

            _sendButton.Dock = DockStyle.Right;
            _sendButton.BackColor = Color.FromArgb(37, 99, 235);
            _sendButton.FlatAppearance.BorderSize = 0;
            _sendButton.FlatStyle = FlatStyle.Flat;
            _sendButton.ForeColor = Color.White;
            _sendButton.Name = "sendButton";
            _sendButton.Size = new Size(84, 42);
            _sendButton.Text = "Gửi";

            _inputBox.Dock = DockStyle.Fill;
            _inputBox.BorderStyle = BorderStyle.FixedSingle;
            _inputBox.Multiline = true;
            _inputBox.Name = "messageInput";
            _inputBox.ScrollBars = ScrollBars.Vertical;

            _busyLabel.Dock = DockStyle.Bottom;
            _busyLabel.Height = 24;
            _busyLabel.BackColor = SystemColors.Window;
            _busyLabel.ForeColor = Color.FromArgb(100, 116, 139);
            _busyLabel.Name = "busyStatus";
            _busyLabel.Padding = new Padding(16, 0, 0, 2);
            _designerInputPanel.Controls.Add(_inputBox);
            _designerInputPanel.Controls.Add(_sendButton);
            Controls.Add(_messagesPanel);
            Controls.Add(_busyLabel);
            Controls.Add(_designerInputPanel);
            Controls.Add(_designerHeader);
            Name = "ucAiChat";
            Size = new Size(410, 590);
            _designerInputPanel.ResumeLayout(false);
            _designerInputPanel.PerformLayout();
            _designerHeader.ResumeLayout(false);
            _designerHeader.PerformLayout();
            _designerInputPanel.PerformLayout();
            ResumeLayout(false);
        }
    }
}

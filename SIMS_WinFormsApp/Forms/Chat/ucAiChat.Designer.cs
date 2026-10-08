using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Forms.Chat
{
    partial class ucAiChat
    {
        private Panel _designerHeader;
        private Panel _designerInputPanel;

        private void InitializeComponent()
        {
            _messagesPanel = new FlowLayoutPanel();
            _inputBox = new TextBox();
            _sendButton = new Button();
            _busyLabel = new Label();
            _designerHeader = new Panel();
            _designerInputPanel = new Panel();
            SuspendLayout();
            _designerHeader.SuspendLayout();
            _designerInputPanel.SuspendLayout();

            _designerHeader.BackColor = SystemColors.Window;
            _designerHeader.Dock = DockStyle.Top;
            _designerHeader.Height = 76;
            _designerHeader.Name = "aiChatHeader";
            _designerHeader.Controls.Add(new Label
            {
                AutoSize = true,
                Location = new Point(70, 26),
                Text = "Trợ lý AI"
            });

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
            _sendButton.Name = "sendButton";
            _sendButton.Size = new Size(84, 42);
            _sendButton.Text = "Gửi";

            _inputBox.Dock = DockStyle.Fill;
            _inputBox.Multiline = true;
            _inputBox.Name = "messageInput";

            _busyLabel.Dock = DockStyle.Bottom;
            _busyLabel.Height = 24;
            _busyLabel.Name = "busyStatus";
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
            ResumeLayout(false);
        }
    }
}

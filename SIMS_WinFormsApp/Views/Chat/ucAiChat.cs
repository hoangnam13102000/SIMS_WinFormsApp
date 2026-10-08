using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.MVP.Presenters.AI;
using SIMS_WinFormsApp.Services.AI.Interfaces;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.Views.Chat
{
    public sealed partial class ucAiChat : UserControl, IAiChatView
    {
        public event Action<string> SendRequested;
        public event Action CloseRequested;

        private AiChatPresenter _presenter;
        private bool _busy;

        public ucAiChat()
        {
            InitializeComponent();
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
        }

        public ucAiChat(IAiChatService chatService)
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            BackColor = AppColors.PageBg;
            _designerHeaderTitle.Text = Lang.Get("ai.title");
            _designerHeaderSubtitle.Text = Lang.Get("ai.subtitle");
            _messagesPanel.Padding = new Padding(14, 18, 14, 16);
            _messagesPanel.Resize += (sender, args) => ResizeMessageRows();
            _sendButton.Text = Lang.Get("ai.send");
            _sendButton.Click += (sender, args) => SendCurrentMessage();
            _inputBox.KeyDown += InputBox_KeyDown;
            _designerCloseButton.Click += (sender, args) => CloseRequested?.Invoke();

            _presenter = new AiChatPresenter(this, chatService);
            Disposed += (sender, args) => _presenter.Dispose();
        }

        public void AppendUserMessage(string message)
        {
            AppendMessage(message, true);
            _inputBox.Clear();
        }

        public void AppendBotMessage(string message)
        {
            AppendMessage(message, false);
        }

        public void SetBusy(bool busy)
        {
            _busy = busy;
            _inputBox.Enabled = !busy;
            _sendButton.Enabled = !busy;
            _sendButton.Text = busy ? Lang.Get("ai.working") : Lang.Get("ai.send");
            _busyLabel.Text = busy ? Lang.Get("ai.busy") : string.Empty;
            _sendButton.BackColor = busy ? AppColors.TextMuted : AppColors.Accent;
        }

        public void ShowError(string message)
        {
            MessageBox.Show(this, message, Lang.Get("common.error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || e.Shift) return;
            e.SuppressKeyPress = true;
            SendCurrentMessage();
        }

        private void SendCurrentMessage()
        {
            if (_busy) return;
            string message = (_inputBox.Text ?? string.Empty).Trim();
            if (message.Length == 0) return;
            SendRequested?.Invoke(message);
        }

        private void AppendMessage(string message, bool isUser)
        {
            var row = new Panel
            {
                BackColor = AppColors.PageBg,
                Margin = new Padding(0, 0, 0, 12),
                Width = Math.Max(180, _messagesPanel.ClientSize.Width - _messagesPanel.Padding.Horizontal),
                Tag = isUser
            };
            var bubble = new Label
            {
                AutoSize = true,
                BackColor = isUser ? AppColors.AccentBgSoft : AppColors.White,
                ForeColor = AppColors.TextPrimary,
                Font = new Font("Segoe UI", 9f),
                Padding = new Padding(12),
                Text = message
            };
            row.Controls.Add(bubble);
            row.Resize += (sender, args) => PositionBubble(row, bubble);
            row.Width = Math.Max(180, _messagesPanel.ClientSize.Width - _messagesPanel.Padding.Horizontal);
            _messagesPanel.Controls.Add(row);
            PositionBubble(row, bubble);
            ResizeMessageRows();
            _messagesPanel.ScrollControlIntoView(row);
        }

        private void ResizeMessageRows()
        {
            if (_messagesPanel == null) return;
            int width = Math.Max(180, _messagesPanel.ClientSize.Width - _messagesPanel.Padding.Horizontal);
            foreach (Control row in _messagesPanel.Controls)
            {
                row.Width = width;
                if (row.Controls.Count == 1 && row.Controls[0] is Label bubble)
                    PositionBubble(row, bubble);
            }
        }

        private static void PositionBubble(Control row, Label bubble)
        {
            bubble.MaximumSize = new Size(Math.Max(180, row.ClientSize.Width * 3 / 4), 0);
            bool isUser = row.Tag is bool && (bool)row.Tag;
            bubble.Location = new Point(isUser ? Math.Max(0, row.ClientSize.Width - bubble.Width) : 0, 0);
            row.Height = bubble.Height;
        }

    }
}

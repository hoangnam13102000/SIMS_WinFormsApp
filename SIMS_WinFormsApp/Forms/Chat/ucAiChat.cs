using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SIMS_WinFormsApp.MVP.Presenters.AI;
using SIMS_WinFormsApp.Services.AI.Interfaces;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.Forms.Chat
{
    public sealed partial class ucAiChat : UserControl, IAiChatView
    {
        public event Action<string> SendRequested;
        public event Action CloseRequested;

        private AiChatPresenter _presenter;
        private FlowLayoutPanel _messagesPanel;
        private TextBox _inputBox;
        private Button _sendButton;
        private Label _busyLabel;
        private bool _busy;

        public ucAiChat()
        {
            InitializeComponent();
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
        }

        public ucAiChat(IAiChatService chatService)
        {
            InitializeComponent();
            Controls.Clear();
            Dock = DockStyle.Fill;
            BackColor = AppColors.PageBg;

            var header = CreateHeader();
            _messagesPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(14, 18, 14, 16),
                BackColor = AppColors.PageBg
            };
            _messagesPanel.Resize += (sender, args) => ResizeMessageRows();

            var inputPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 88,
                Padding = new Padding(14, 12, 14, 12),
                BackColor = AppColors.White
            };
            inputPanel.Paint += (sender, args) =>
            {
                using (var pen = new Pen(AppColors.Border))
                    args.Graphics.DrawLine(pen, 0, 0, inputPanel.Width, 0);
            };

            _sendButton = new Button
            {
                Text = Lang.Get("ai.send"),
                Width = 84,
                Height = 42,
                Dock = DockStyle.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = AppColors.Accent,
                ForeColor = Color.White,
                Font = new Font("Segoe UI Semibold", 9f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Margin = new Padding(8, 0, 0, 0)
            };
            _sendButton.FlatAppearance.BorderSize = 0;
            _sendButton.Click += (sender, args) => SendCurrentMessage();

            _inputBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Segoe UI", 10f),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = AppColors.BgLighter,
                ForeColor = AppColors.TextPrimary,
                Margin = new Padding(0, 0, 10, 0)
            };
            _inputBox.KeyDown += InputBox_KeyDown;

            var inputHost = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 0, 10, 0)
            };
            inputHost.Controls.Add(_inputBox);
            inputPanel.Controls.Add(inputHost);
            inputPanel.Controls.Add(_sendButton);

            _busyLabel = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 24,
                Padding = new Padding(16, 0, 0, 2),
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = AppColors.TextMuted,
                BackColor = AppColors.White,
                Text = string.Empty
            };

            Controls.Add(_messagesPanel);
            Controls.Add(_busyLabel);
            Controls.Add(inputPanel);
            Controls.Add(header);

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

        private Control CreateHeader()
        {
            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 76,
                Padding = new Padding(16, 12, 14, 10),
                BackColor = AppColors.White
            };
            header.Paint += (sender, args) =>
            {
                using (var pen = new Pen(AppColors.Border))
                    args.Graphics.DrawLine(pen, 0, header.Height - 1, header.Width, header.Height - 1);
            };

            var avatar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 44,
                Height = 44,
                BackColor = AppColors.AccentBgSoft
            };
            avatar.Paint += (sender, args) =>
            {
                args.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(AppColors.AccentBgSoft))
                    args.Graphics.FillEllipse(brush, 1, 1, 40, 40);
                using (var font = new Font("Segoe UI Semibold", 10f, FontStyle.Bold))
                {
                    TextRenderer.DrawText(
                        args.Graphics,
                        "AI",
                        font,
                        new Rectangle(1, 1, 40, 40),
                        AppColors.Accent,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }
            };

            var textPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(12, 3, 0, 0),
                BackColor = AppColors.White
            };
            textPanel.Controls.Add(new Label
            {
                Text = Lang.Get("ai.title"),
                Dock = DockStyle.Top,
                Height = 27,
                Font = new Font("Segoe UI Semibold", 11f, FontStyle.Bold),
                ForeColor = AppColors.TextPrimary
            });
            textPanel.Controls.Add(new Label
            {
                Text = Lang.Get("ai.subtitle"),
                Dock = DockStyle.Top,
                Height = 24,
                Font = new Font("Segoe UI", 8f),
                ForeColor = AppColors.TextMuted
            });

            var closeButton = new Button
            {
                Text = "×",
                Dock = DockStyle.Right,
                Width = 32,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = AppColors.TextMuted,
                Font = new Font("Segoe UI", 16f),
                Cursor = Cursors.Hand
            };
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.Click += (sender, args) => CloseRequested?.Invoke();

            header.Controls.Add(textPanel);
            header.Controls.Add(closeButton);
            header.Controls.Add(avatar);
            return header;
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
            var row = new AiChatMessageRow(message, isUser);
            row.Width = Math.Max(180, _messagesPanel.ClientSize.Width - _messagesPanel.Padding.Horizontal);
            row.Margin = new Padding(0, 0, 0, 12);
            _messagesPanel.Controls.Add(row);
            ResizeMessageRows();
            _messagesPanel.ScrollControlIntoView(row);
        }

        private void ResizeMessageRows()
        {
            if (_messagesPanel == null) return;
            int width = Math.Max(180, _messagesPanel.ClientSize.Width - _messagesPanel.Padding.Horizontal);
            foreach (Control row in _messagesPanel.Controls)
                row.Width = width;
        }

    }
}

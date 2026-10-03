using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SIMS_WinFormsApp.MVP.Presenters.AI;
using SIMS_WinFormsApp.Services.AI.Interfaces;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.Forms.Chat
{
    public sealed class ucAiChat : UserControl, IAiChatView
    {
        private const int MessageHorizontalPadding = 14;
        private const int MessageVerticalPadding = 10;

        public event Action<string> SendRequested;
        public event Action CloseRequested;

        private readonly AiChatPresenter _presenter;
        private readonly FlowLayoutPanel _messagesPanel;
        private readonly TextBox _inputBox;
        private readonly Button _sendButton;
        private readonly Label _busyLabel;
        private bool _busy;

        public ucAiChat(IAiChatService chatService)
        {
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
            var row = new MessageRow(message, isUser);
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

        private sealed class MessageRow : Panel
        {
            private readonly Panel _bubble;
            private readonly Label _senderLabel;
            private readonly Label _messageLabel;
            private readonly bool _isUser;

            public MessageRow(string message, bool isUser)
            {
                _isUser = isUser;
                Height = 60;
                BackColor = Color.Transparent;

                _bubble = new Panel
                {
                    Padding = new Padding(
                        MessageHorizontalPadding,
                        MessageVerticalPadding,
                        MessageHorizontalPadding,
                        MessageVerticalPadding),
                    BackColor = isUser ? AppColors.Accent : AppColors.White
                };
                _bubble.Paint += Bubble_Paint;
                _bubble.Resize += (sender, args) => ApplyBubbleRegion();

                var bubbleContent = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.Transparent
                };
                _senderLabel = new Label
                {
                    Dock = DockStyle.Top,
                    Height = 18,
                    Font = new Font("Segoe UI Semibold", 8f, FontStyle.Bold),
                    ForeColor = isUser ? Color.FromArgb(220, 235, 255) : AppColors.Accent,
                    Text = Lang.Get(isUser ? "ai.message.you" : "ai.message.assistant")
                };
                _messageLabel = new Label
                {
                    Dock = DockStyle.Top,
                    AutoSize = false,
                    Font = new Font("Segoe UI", 9.5f),
                    ForeColor = isUser ? Color.White : AppColors.TextPrimary,
                    Text = message ?? string.Empty
                };
                bubbleContent.Controls.Add(_messageLabel);
                bubbleContent.Controls.Add(_senderLabel);
                _bubble.Controls.Add(bubbleContent);
                Controls.Add(_bubble);

                Resize += (sender, args) => LayoutBubble();
                LayoutBubble();
            }

            private void LayoutBubble()
            {
                if (_bubble == null || _messageLabel == null || Width <= 0) return;

                int maxBubbleWidth = Math.Max(150, (int)(Width * 0.78));
                int maxTextWidth = Math.Max(110, maxBubbleWidth - MessageHorizontalPadding * 2);
                Size measured = TextRenderer.MeasureText(
                    _messageLabel.Text,
                    _messageLabel.Font,
                    new Size(maxTextWidth, int.MaxValue),
                    TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl);

                int textHeight = Math.Max(_messageLabel.Font.Height + 4, measured.Height);
                int bubbleWidth = Math.Min(
                    maxBubbleWidth,
                    Math.Max(116, measured.Width + MessageHorizontalPadding * 2));
                int bubbleHeight = MessageVerticalPadding * 2 + _senderLabel.Height + textHeight;

                _bubble.Size = new Size(bubbleWidth, bubbleHeight);
                _bubble.Location = new Point(_isUser ? Width - bubbleWidth : 0, 0);
                _messageLabel.Height = textHeight;
                Height = bubbleHeight;
                ApplyBubbleRegion();
            }

            private void ApplyBubbleRegion()
            {
                if (_bubble == null || _bubble.Width <= 0 || _bubble.Height <= 0) return;

                using (var path = CreateRoundedPath(
                    new Rectangle(0, 0, _bubble.Width, _bubble.Height),
                    Math.Min(12, Math.Min(_bubble.Width, _bubble.Height) / 2)))
                {
                    Region oldRegion = _bubble.Region;
                    _bubble.Region = new Region(path);
                    oldRegion?.Dispose();
                }
            }

            private void Bubble_Paint(object sender, PaintEventArgs e)
            {
                using (var path = CreateRoundedPath(new Rectangle(0, 0, _bubble.Width - 1, _bubble.Height - 1), 12))
                using (var brush = new SolidBrush(_bubble.BackColor))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.FillPath(brush, path);
                }
            }

            private static GraphicsPath CreateRoundedPath(Rectangle bounds, int radius)
            {
                int diameter = radius * 2;
                var path = new GraphicsPath();
                path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
                path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
                path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
                path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
                path.CloseFigure();
                return path;
            }
        }
    }
}

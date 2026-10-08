using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    public sealed class AiChatMessageRow : Panel
    {
        private const int MessageHorizontalPadding = 14;
        private const int MessageVerticalPadding = 10;

        private readonly Panel _bubble;
        private readonly Label _senderLabel;
        private readonly Label _messageLabel;
        private bool _isUser;

        public string Message
        {
            get { return _messageLabel.Text; }
            set { _messageLabel.Text = value ?? string.Empty; LayoutBubble(); }
        }

        public bool IsUser
        {
            get { return _isUser; }
            set
            {
                _isUser = value;
                ApplyAppearance();
                LayoutBubble();
            }
        }

        public AiChatMessageRow() : this(string.Empty, false)
        {
        }

        public AiChatMessageRow(string message, bool isUser)
        {
            BackColor = Color.Transparent;
            Height = 60;

            _bubble = new Panel
            {
                Padding = new Padding(
                    MessageHorizontalPadding,
                    MessageVerticalPadding,
                    MessageHorizontalPadding,
                    MessageVerticalPadding)
            };
            _bubble.Paint += Bubble_Paint;
            _bubble.Resize += Bubble_Resize;

            var bubbleContent = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };
            _senderLabel = new Label
            {
                Dock = DockStyle.Top,
                Height = 18,
                Font = new Font("Segoe UI Semibold", 8f, FontStyle.Bold)
            };
            _messageLabel = new Label
            {
                Dock = DockStyle.Top,
                AutoSize = false,
                Font = new Font("Segoe UI", 9.5f)
            };
            bubbleContent.Controls.Add(_messageLabel);
            bubbleContent.Controls.Add(_senderLabel);
            _bubble.Controls.Add(bubbleContent);
            Controls.Add(_bubble);
            Resize += AiChatMessageRow_Resize;
            _isUser = isUser;
            _messageLabel.Text = message ?? string.Empty;
            ApplyAppearance();
            LayoutBubble();
        }

        private void ApplyAppearance()
        {
            _bubble.BackColor = _isUser ? AppColors.Accent : AppColors.White;
            _senderLabel.ForeColor = _isUser ? Color.FromArgb(220, 235, 255) : AppColors.Accent;
            _senderLabel.Text = Lang.Get(_isUser ? "ai.message.you" : "ai.message.assistant");
            _messageLabel.ForeColor = _isUser ? Color.White : AppColors.TextPrimary;
            _bubble.Invalidate();
        }

        private void AiChatMessageRow_Resize(object sender, EventArgs e)
        {
            LayoutBubble();
        }

        private void Bubble_Resize(object sender, EventArgs e)
        {
            ApplyBubbleRegion();
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

            using (GraphicsPath path = CreateRoundedPath(
                new Rectangle(0, 0, _bubble.Width, _bubble.Height),
                Math.Min(12, Math.Min(_bubble.Width, _bubble.Height) / 2)))
            {
                Region oldRegion = _bubble.Region;
                _bubble.Region = new Region(path);
                if (oldRegion != null) oldRegion.Dispose();
            }
        }

        private void Bubble_Paint(object sender, PaintEventArgs e)
        {
            using (GraphicsPath path = CreateRoundedPath(
                new Rectangle(0, 0, _bubble.Width - 1, _bubble.Height - 1), 12))
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

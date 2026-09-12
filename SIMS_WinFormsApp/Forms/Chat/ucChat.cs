using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.Models.Chat;
using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.MVP.Views;
using SIMS_WinFormsApp.Services.Chat;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Forms.Chat
{
    /// <summary>
    /// View chat nội bộ staff – implement IChatView. Presenter gắn trong constructor.
    /// </summary>
    public sealed class ucChat : UserControl, IChatView
    {
        public event Action ViewLoaded;
        public event Action ViewUnloaded;
        public event Action<int, string> SendRequested;
        public event Action<int> ConversationSelected;

        private readonly ChatPresenter _presenter;

        private Panel _leftPanel;
        private Panel _rightPanel;
        private ListBox _userList;
        private Label _statusLabel;
        private Label _peerTitleLabel;
        private FlowLayoutPanel _messagesPanel;
        private TextBox _inputBox;
        private Button _btnSend;
        private Label _emptyHint;

        private readonly Dictionary<int, OnlineUserInfo> _onlineMap = new Dictionary<int, OnlineUserInfo>();
        private int _selectedPeerId;
        private bool _suppressSelectionEvent;

        // Chiều cao 1 dòng chữ đo thực tế theo font đang dùng (không đoán số px cứng),
        // để không bị cắt dấu tiếng Việt dù DPI/scale màn hình khác nhau.
        private int _rowNameH;
        private int _rowSmallH;

        public ucChat()
        {
            Dock = DockStyle.Fill;
            BackColor = AppColors.PageBg;
            DoubleBuffered = true;

            BuildUi();
            _presenter = new ChatPresenter(this, this);

            Load += (_, __) =>
            {
                ApplyThemeColors();
                ViewLoaded?.Invoke();
            };
            HandleDestroyed += (_, __) => ViewUnloaded?.Invoke();
            Disposed += (_, __) => _presenter.Dispose();
        }

        // ===================== IChatView =====================

        public void SetConnectionStatus(bool connected, string statusText)
        {
            if (_statusLabel == null) return;
            _statusLabel.Text = statusText ?? string.Empty;
            _statusLabel.ForeColor = connected ? AppColors.Success : AppColors.TextMuted;
        }

        public void SetOnlineUsers(IReadOnlyList<OnlineUserInfo> users, int currentUserId)
        {
            _onlineMap.Clear();
            _userList.Items.Clear();
            if (users == null) return;

            UserListItem toReselect = null;
            foreach (var u in users)
            {
                if (u == null || u.UserId == currentUserId) continue;
                _onlineMap[u.UserId] = u;
                var item = new UserListItem(u);
                _userList.Items.Add(item);
                if (u.UserId == _selectedPeerId) toReselect = item;
            }

            if (_userList.Items.Count == 0)
            {
                _emptyHint.Visible = true;
                _emptyHint.Text = Lang.Get("chat.empty.noOnline");
            }
            else
            {
                _emptyHint.Visible = false;
            }

            // Danh sách được vẽ lại mỗi khi trạng thái online thay đổi — giữ nguyên lựa chọn
            // hiện tại (không phát lại ConversationSelected) để không làm mất hội thoại đang xem.
            if (toReselect != null)
            {
                _suppressSelectionEvent = true;
                _userList.SelectedItem = toReselect;
                _suppressSelectionEvent = false;
            }
        }

        public void SetSelectedPeer(int userId, string displayName)
        {
            _selectedPeerId = userId;
            if (userId <= 0)
            {
                _peerTitleLabel.Text = Lang.Get("chat.peer.none");
                return;
            }
            if (_onlineMap.TryGetValue(userId, out var info))
                _peerTitleLabel.Text = info.UserName + (string.IsNullOrEmpty(info.RoleCode) ? "" : $" · {info.RoleCode}");
            else
                _peerTitleLabel.Text = string.IsNullOrEmpty(displayName) ? ("#" + userId) : displayName;
        }

        public void AppendMessage(ChatMessageDto message, bool isMine)
        {
            if (message == null) return;
            var bubble = CreateBubble(message, isMine);
            _messagesPanel.Controls.Add(bubble);
            _messagesPanel.ScrollControlIntoView(bubble);
        }

        public void ClearMessages()
        {
            _messagesPanel.Controls.Clear();
        }

        public void ShowInfo(string message)
        {
            if (!string.IsNullOrEmpty(message))
                _statusLabel.Text = message;
        }

        public void ShowError(string message)
        {
            MessageBox.Show(this, message, Lang.Get("common.error"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void SetInputEnabled(bool enabled)
        {
            _inputBox.Enabled = enabled;
            _btnSend.Enabled = enabled;
        }

        public void ClearInput()
        {
            _inputBox.Clear();
            _inputBox.Focus();
        }

        // ===================== UI build =====================

        private void BuildUi()
        {
            _leftPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 320,
                BackColor = AppColors.White,
                Padding = new Padding(12)
            };

            var leftTitle = new Label
            {
                Text = Lang.Get("chat.online.title"),
                Font = AppFonts.SmallBold,
                ForeColor = AppColors.TextMuted,
                Dock = DockStyle.Top,
                Height = 28
            };

            _statusLabel = new Label
            {
                Text = Lang.Get("chat.status.connecting"),
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                Dock = DockStyle.Top,
                Height = 22
            };

            // Đo chiều cao dòng chữ thật theo font (bao gồm dấu tiếng Việt phía trên/dưới)
            // thay vì dùng số px cố định, để không bị cắt chữ khi DPI/scale khác nhau.
            const string heightProbe = "Ầệgy";
            _rowNameH = TextRenderer.MeasureText(heightProbe, AppFonts.Body).Height;
            _rowSmallH = TextRenderer.MeasureText(heightProbe, AppFonts.Small).Height;
            int listItemHeight = 6 + _rowNameH + 4 + _rowSmallH + 2 + _rowSmallH + 6;

            _userList = new ListBox
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                Font = AppFonts.Body,
                IntegralHeight = false,
                DrawMode = DrawMode.OwnerDrawFixed,
                ItemHeight = Math.Max(56, listItemHeight)
            };
            _userList.DrawItem += UserList_DrawItem;
            _userList.SelectedIndexChanged += UserList_SelectedIndexChanged;

            _emptyHint = new Label
            {
                Text = Lang.Get("chat.empty.noOnline"),
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                Dock = DockStyle.Bottom,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter
            };

            _leftPanel.Controls.Add(_userList);
            _leftPanel.Controls.Add(_emptyHint);
            _leftPanel.Controls.Add(_statusLabel);
            _leftPanel.Controls.Add(leftTitle);

            var divider = new Panel
            {
                Dock = DockStyle.Left,
                Width = 1,
                BackColor = AppColors.Border
            };

            _rightPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = AppColors.PageBg,
                Padding = new Padding(0)
            };

            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 56,
                BackColor = AppColors.White,
                Padding = new Padding(16, 0, 16, 0)
            };
            _peerTitleLabel = new Label
            {
                Text = Lang.Get("chat.peer.none"),
                Font = AppFonts.BodyBold,
                ForeColor = AppColors.TextPrimary,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            header.Controls.Add(_peerTitleLabel);
            header.Paint += (s, e) =>
            {
                using (var pen = new Pen(AppColors.Border))
                    e.Graphics.DrawLine(pen, 0, header.Height - 1, header.Width, header.Height - 1);
            };

            _messagesPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(16, 12, 16, 12),
                BackColor = AppColors.PageBg
            };
            _messagesPanel.Resize += (_, __) =>
            {
                foreach (Control c in _messagesPanel.Controls)
                    c.Width = Math.Max(120, _messagesPanel.ClientSize.Width - 36);
            };

            var inputBar = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 64,
                BackColor = AppColors.White,
                Padding = new Padding(12, 10, 12, 10)
            };
            inputBar.Paint += (s, e) =>
            {
                using (var pen = new Pen(AppColors.Border))
                    e.Graphics.DrawLine(pen, 0, 0, inputBar.Width, 0);
            };

            _inputBox = new TextBox
            {
                Font = AppFonts.Body,
                BorderStyle = BorderStyle.FixedSingle,
                Multiline = false,
                Dock = DockStyle.Fill
            };
            _inputBox.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    DoSend();
                }
            };

            _btnSend = new Button
            {
                Text = Lang.Get("chat.btn.send"),
                Width = 100,
                Dock = DockStyle.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = AppColors.Accent,
                ForeColor = Color.White,
                Font = AppFonts.BodyBold,
                Cursor = Cursors.Hand
            };
            _btnSend.FlatAppearance.BorderSize = 0;
            _btnSend.Click += (_, __) => DoSend();

            var inputInner = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0, 0, 8, 0) };
            inputInner.Controls.Add(_inputBox);
            inputBar.Controls.Add(inputInner);
            inputBar.Controls.Add(_btnSend);

            _rightPanel.Controls.Add(_messagesPanel);
            _rightPanel.Controls.Add(inputBar);
            _rightPanel.Controls.Add(header);

            Controls.Add(_rightPanel);
            Controls.Add(divider);
            Controls.Add(_leftPanel);
        }

        private void ApplyThemeColors()
        {
            BackColor = AppColors.PageBg;
            _leftPanel.BackColor = AppColors.White;
            _rightPanel.BackColor = AppColors.PageBg;
            _messagesPanel.BackColor = AppColors.PageBg;
            _userList.BackColor = AppColors.White;
            _userList.ForeColor = AppColors.TextPrimary;
            _peerTitleLabel.ForeColor = AppColors.TextPrimary;
            _btnSend.BackColor = AppColors.Accent;
        }

        private void UserList_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            e.DrawBackground();
            var item = _userList.Items[e.Index] as UserListItem;
            if (item == null) return;

            bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            var bg = selected ? AppColors.AccentBgSoft : (e.Index % 2 == 0 ? AppColors.White : AppColors.BgLighter);
            using (var brush = new SolidBrush(bg))
                e.Graphics.FillRectangle(brush, e.Bounds);

            int cy = e.Bounds.Y + e.Bounds.Height / 2;
            var avatarRect = new Rectangle(e.Bounds.X + 10, cy - 14, 28, 28);
            using (var brush = new SolidBrush(item.Info.IsOnline ? AppColors.Accent : AppColors.TextMuted))
                e.Graphics.FillEllipse(brush, avatarRect);
            var initial = string.IsNullOrEmpty(item.Info.UserName) ? "?" : item.Info.UserName.Substring(0, 1).ToUpperInvariant();
            TextRenderer.DrawText(e.Graphics, initial, AppFonts.SmallBold, avatarRect, Color.White,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            // Chấm trạng thái online/offline ở góc avatar
            var dotColor = item.Info.IsOnline ? AppColors.Success : AppColors.TextMuted;
            var dotRect = new Rectangle(avatarRect.Right - 8, avatarRect.Bottom - 8, 9, 9);
            using (var dotBrush = new SolidBrush(dotColor))
                e.Graphics.FillEllipse(dotBrush, dotRect);
            using (var dotPen = new Pen(AppColors.White, 1.5f))
                e.Graphics.DrawEllipse(dotPen, dotRect);

            // 3 dòng (tên / vai trò / trạng thái) được xếp chồng dựa trên chiều cao đo thực tế
            // của font (_rowNameH / _rowSmallH) — không dùng số px cố định — nên khung chữ
            // luôn đủ cao cho chính font đó, kể cả dấu tiếng Việt, ở mọi mức DPI/scale.
            int textLeft = e.Bounds.X + 48;
            int textWidth = e.Bounds.Width - 60;
            int y = e.Bounds.Y + 6;

            var nameRect = new Rectangle(textLeft, y, textWidth, _rowNameH);
            TextRenderer.DrawText(e.Graphics, item.Info.UserName, AppFonts.Body, nameRect, AppColors.TextPrimary,
                TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter);
            y += _rowNameH + 4;

            var roleRect = new Rectangle(textLeft, y, textWidth, _rowSmallH);
            TextRenderer.DrawText(e.Graphics, item.Info.RoleCode ?? "", AppFonts.Small, roleRect, AppColors.TextMuted,
                TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter);
            y += _rowSmallH + 2;

            var statusText = item.Info.IsOnline ? Lang.Get("chat.peer.online") : Lang.Get("chat.peer.offline");
            var statusRect = new Rectangle(textLeft, y, textWidth, _rowSmallH);
            TextRenderer.DrawText(e.Graphics, statusText, AppFonts.Small, statusRect,
                item.Info.IsOnline ? AppColors.Success : AppColors.TextMuted,
                TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter);
        }

        private void UserList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressSelectionEvent) return;
            if (_userList.SelectedItem is UserListItem item)
            {
                ConversationSelected?.Invoke(item.Info.UserId);
                _presenter.NotifyPeerDisplayName(item.Info.UserId, item.Info.UserName);
            }
        }

        private void DoSend()
        {
            var text = _inputBox.Text?.Trim();
            if (string.IsNullOrEmpty(text)) return;
            SendRequested?.Invoke(_selectedPeerId, text);
        }

        private Control CreateBubble(ChatMessageDto msg, bool isMine)
        {
            int width = Math.Max(120, _messagesPanel.ClientSize.Width - 36);
            var wrap = new Panel
            {
                Width = width,
                Height = 56,
                Margin = new Padding(0, 0, 0, 8),
                BackColor = Color.Transparent
            };

            var bubble = new Panel
            {
                Height = 48,
                BackColor = isMine ? AppColors.Accent : AppColors.White,
                Padding = new Padding(12, 8, 12, 8)
            };

            var nameTime = new Label
            {
                AutoSize = false,
                Height = 16,
                Dock = DockStyle.Top,
                Font = AppFonts.Small,
                ForeColor = isMine ? Color.FromArgb(220, 255, 255, 255) : AppColors.TextMuted,
                Text = (isMine ? Lang.Get("chat.you") : (msg.UserName ?? "")) + " · " + FormatTime(msg.Timestamp)
            };

            var body = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                Font = AppFonts.Body,
                ForeColor = isMine ? Color.White : AppColors.TextPrimary,
                Text = msg.Text ?? "",
                MaximumSize = new Size(width * 2 / 3, 0)
            };

            var textSize = TextRenderer.MeasureText(msg.Text ?? "", AppFonts.Body, new Size(width * 2 / 3, int.MaxValue),
                TextFormatFlags.WordBreak);
            int bubbleW = Math.Min(width * 2 / 3, Math.Max(100, textSize.Width + 28));
            int bubbleH = Math.Max(48, textSize.Height + 36);
            bubble.Size = new Size(bubbleW, bubbleH);
            bubble.Location = new Point(isMine ? width - bubbleW - 8 : 8, 0);
            wrap.Height = bubbleH + 4;

            bubble.Controls.Add(body);
            bubble.Controls.Add(nameTime);

            bubble.Resize += (s, e) =>
            {
                try
                {
                    bubble.Region?.Dispose();
                    bubble.Region = new Region(AppRadius.GetRoundedPath(new Rectangle(0, 0, bubble.Width, bubble.Height), AppRadius.Medium));
                }
                catch { /* ignore */ }
            };
            try
            {
                bubble.Region = new Region(AppRadius.GetRoundedPath(new Rectangle(0, 0, bubble.Width, bubble.Height), AppRadius.Medium));
            }
            catch { /* ignore */ }

            wrap.Controls.Add(bubble);
            return wrap;
        }

        private static string FormatTime(long unixMs)
        {
            if (unixMs <= 0) return DateTime.Now.ToString("HH:mm");
            try
            {
                return DateTimeOffset.FromUnixTimeMilliseconds(unixMs).LocalDateTime.ToString("HH:mm");
            }
            catch
            {
                return DateTime.Now.ToString("HH:mm");
            }
        }

        private sealed class UserListItem
        {
            public OnlineUserInfo Info { get; }
            public UserListItem(OnlineUserInfo info) { Info = info; }
            public override string ToString() => Info?.UserName ?? "";
        }
    }
}
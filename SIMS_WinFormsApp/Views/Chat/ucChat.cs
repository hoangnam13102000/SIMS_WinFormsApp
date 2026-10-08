using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.Models.Chat;
using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.Views.Interfaces;
using SIMS_WinFormsApp.Services.Chat;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Chat
{

    public sealed partial class ucChat : UserControl, IChatView
    {
        public event Action ViewLoaded;
        public event Action ViewUnloaded;
        public event Action<int, string> SendRequested;
        public event Action<int> ConversationSelected;

        private readonly ChatPresenter _presenter;

        private readonly Dictionary<int, OnlineUserInfo> _onlineMap = new Dictionary<int, OnlineUserInfo>();
        private int _selectedPeerId;
        private bool _suppressSelectionEvent;

        // Chiều cao 1 dòng chữ đo thực tế theo font đang dùng (không đoán số px cứng),
        // để không bị cắt dấu tiếng Việt dù DPI/scale màn hình khác nhau.
        private int _rowNameH;
        private int _rowSmallH;

        public ucChat()
        {
            InitializeComponent();
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            Dock = DockStyle.Fill;
            BackColor = AppColors.PageBg;
            DoubleBuffered = true;

            const string heightProbe = "Ầệgy";
            _rowNameH = TextRenderer.MeasureText(heightProbe, AppFonts.Body).Height;
            _rowSmallH = TextRenderer.MeasureText(heightProbe, AppFonts.Small).Height;
            _userList.Font = AppFonts.Body;
            _userList.ItemHeight = Math.Max(56, 6 + _rowNameH + 4 + _rowSmallH + 2 + _rowSmallH + 6);
            _userList.DrawItem += UserList_DrawItem;
            _userList.SelectedIndexChanged += UserList_SelectedIndexChanged;
            _messagesPanel.Padding = new Padding(16, 12, 16, 12);
            _messagesPanel.Resize += (_, __) =>
            {
                foreach (Control control in _messagesPanel.Controls)
                    control.Width = Math.Max(120, _messagesPanel.ClientSize.Width - 36);
            };
            _inputBox.KeyDown += (sender, args) =>
            {
                if (args.KeyCode == Keys.Enter)
                {
                    args.SuppressKeyPress = true;
                    DoSend();
                }
            };
            _btnSend.Click += (_, __) => DoSend();
            _btnSend.Text = Lang.Get("chat.btn.send");
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

            // Dùng chiều cao đo thực tế (_rowSmallH, đo sẵn theo font AppFonts.Small kèm ký tự
            // có dấu cao/thấp nhất "Ầệgy") thay vì số px cố định 16, để dòng "Tên · giờ" không
            // bị cắt ngang phần dấu tiếng Việt phía trên (ví dụ "Bạn" bị cắt còn "Ban.").
            var nameTime = new Label
            {
                AutoSize = false,
                Height = _rowSmallH,
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
            // Chiều cao bubble = padding trên/dưới + chiều cao thật của dòng tên/giờ (_rowSmallH)
            // + chiều cao nội dung tin nhắn, thay vì cộng thêm hằng số 36 áng chừng (nguồn gốc
            // gây cắt chữ vì trước đó nameTime chỉ cao 16px trong khi cần _rowSmallH).
            int bubbleH = Math.Max(48, bubble.Padding.Top + bubble.Padding.Bottom + _rowSmallH + textSize.Height);
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
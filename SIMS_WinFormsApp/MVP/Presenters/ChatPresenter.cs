using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Models;
using SIMS_WinFormsApp.Models.Chat;
using SIMS_WinFormsApp.Views.Interfaces;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.Services.Chat;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.UI.I18n;

namespace SIMS_WinFormsApp.MVP.Presenters
{

    public sealed class ChatPresenter : IDisposable
    {
        private readonly IChatView _view;
        private readonly IChatClientService _client;
        private readonly Control _syncControl;
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;

        private int _selectedPeerId;
        private string _selectedPeerName = string.Empty;
        private bool _bound;

        // Danh bạ nhân viên lấy từ DB (kể cả offline) + tập ID đang online qua WebSocket.
        // UI hiển thị hợp nhất 2 nguồn này thay vì chỉ hiển thị người đang kết nối,
        // nếu không danh sách sẽ trống khi chỉ có 1 client đang chạy.
        private readonly Dictionary<int, OnlineUserInfo> _directory = new Dictionary<int, OnlineUserInfo>();
        private readonly HashSet<int> _onlineIds = new HashSet<int>();

        public ChatPresenter(IChatView view, Control syncControl, IChatClientService client = null,
            IChatRepository chatRepository = null, IUserRepository userRepository = null)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _syncControl = syncControl ?? throw new ArgumentNullException(nameof(syncControl));
            _client = client ?? ChatClientService.Instance;
            _chatRepository = chatRepository ?? AppComposition.CreateChatRepository();
            _userRepository = userRepository ?? AppComposition.CreateUserRepository();

            _view.ViewLoaded += OnViewLoaded;
            _view.ViewUnloaded += OnViewUnloaded;
            _view.SendRequested += OnSendRequested;
            _view.ConversationSelected += OnConversationSelected;
        }

        private void OnViewLoaded()
        {
            EnsureConnected();
            BindClientEvents();
            _view.SetConnectionStatus(_client.IsConnected,
                _client.IsConnected ? Lang.Get("chat.status.online") : Lang.Get("chat.status.connecting"));
            _view.SetInputEnabled(_selectedPeerId > 0 && _client.IsConnected);
            LoadDirectoryAsync();
        }

        private void OnViewUnloaded()
        {
            UnbindClientEvents();
        }

        // Nạp toàn bộ nhân viên (trừ CUSTOMER, trừ chính mình) từ DB để luôn hiển thị đủ
        // danh bạ, không phụ thuộc việc họ có đang mở app / kết nối WebSocket hay không.
        private void LoadDirectoryAsync()
        {
            var me = UserSession.Instance.CurrentUser?.UserId ?? 0;
            if (me <= 0) return;

            Task.Run(() =>
            {
                IReadOnlyList<User> staff;
                try
                {
                    staff = _userRepository.GetActiveStaffExcept(me);
                }
                catch
                {
                    return; // DB tạm thời không khả dụng: vẫn hiển thị người đang online qua WS
                }

                RunOnUi(() =>
                {
                    _directory.Clear();
                    foreach (var u in staff)
                    {
                        _directory[u.UserId] = new OnlineUserInfo
                        {
                            UserId = u.UserId,
                            UserName = u.FullName ?? u.Username,
                            RoleCode = u.RoleCode ?? string.Empty,
                            IsOnline = _onlineIds.Contains(u.UserId)
                        };
                    }
                    PushMergedList();
                });
            });
        }

        private void EnsureConnected()
        {
            var user = UserSession.Instance.CurrentUser;
            if (user == null) return;

            ChatServerHost.Instance.Start();

            if (!_client.IsConnected)
            {
                _client.ConnectStaff(user.UserId, user.FullName ?? user.Username, user.RoleCode ?? string.Empty);
            }
        }

        private void BindClientEvents()
        {
            if (_bound) return;
            _client.MessageReceived += OnMessageReceived;
            _client.ConnectionChanged += OnConnectionChanged;
            _client.PresenceUpdated += OnPresenceUpdated;
            _bound = true;
        }

        private void UnbindClientEvents()
        {
            if (!_bound) return;
            _client.MessageReceived -= OnMessageReceived;
            _client.ConnectionChanged -= OnConnectionChanged;
            _client.PresenceUpdated -= OnPresenceUpdated;
            _bound = false;
        }

        private void OnConnectionChanged(bool connected)
        {
            RunOnUi(() =>
            {
                _view.SetConnectionStatus(connected,
                    connected ? Lang.Get("chat.status.online") : Lang.Get("chat.status.offline"));
                _view.SetInputEnabled(_selectedPeerId > 0 && connected);
            });
        }

        private void OnPresenceUpdated(IReadOnlyList<OnlineUserInfo> users)
        {
            RunOnUi(() =>
            {
                _onlineIds.Clear();
                if (users != null)
                {
                    foreach (var u in users)
                    {
                        if (u == null) continue;
                        _onlineIds.Add(u.UserId);

                        // Người đang online nhưng chưa có trong danh bạ (vd. tài khoản mới) vẫn được thêm.
                        if (!_directory.ContainsKey(u.UserId))
                            _directory[u.UserId] = new OnlineUserInfo
                            {
                                UserId = u.UserId,
                                UserName = u.UserName,
                                RoleCode = u.RoleCode,
                                IsOnline = true
                            };
                    }
                }

                foreach (var entry in _directory.Values)
                    entry.IsOnline = _onlineIds.Contains(entry.UserId);

                PushMergedList();
            });
        }

        // Đẩy danh sách hợp nhất (danh bạ DB + trạng thái online) lên UI: online trước, offline sau.
        private void PushMergedList()
        {
            var me = UserSession.Instance.CurrentUser?.UserId ?? 0;
            var merged = _directory.Values
                .OrderByDescending(u => u.IsOnline)
                .ThenBy(u => u.UserName, StringComparer.CurrentCultureIgnoreCase)
                .ToList();
            _view.SetOnlineUsers(merged, me);
        }

        private void OnMessageReceived(ChatMessageDto msg)
        {
            if (msg == null) return;
            if (!msg.IsStaffChat) return;

            var me = UserSession.Instance.CurrentUser?.UserId ?? 0;
            bool isMine = msg.UserId == me;
            int peerId = isMine ? msg.ToUserId : msg.UserId;

            RunOnUi(() =>
            {
                if (_selectedPeerId == 0 || peerId != _selectedPeerId) return;
                _view.AppendMessage(msg, isMine);
            });
        }

        private void OnConversationSelected(int peerUserId)
        {
            _selectedPeerId = peerUserId;
            _selectedPeerName = peerUserId > 0 ? ("#" + peerUserId) : string.Empty;
            _view.ClearMessages();
            _view.SetSelectedPeer(peerUserId, _selectedPeerName);
            _view.SetInputEnabled(peerUserId > 0 && _client.IsConnected);

            if (peerUserId > 0)
                LoadHistoryAsync(peerUserId);
        }

        // Nạp lịch sử chat đã lưu trong DB (ChatConversations/ChatMessages) cho cặp
        // tài khoản hiện tại <-> peer, để tin nhắn cũ không biến mất khi mở lại hội thoại.
        private void LoadHistoryAsync(int peerUserId)
        {
            var me = UserSession.Instance.CurrentUser?.UserId ?? 0;
            if (me <= 0) return;

            Task.Run(() =>
            {
                IReadOnlyList<ChatMessageDto> history;
                try
                {
                    history = _chatRepository.GetStaffConversationHistory(me, peerUserId);
                }
                catch
                {
                    // DB tạm thời không khả dụng: vẫn cho chat real-time hoạt động bình thường
                    return;
                }

                RunOnUi(() =>
                {
                    if (_selectedPeerId != peerUserId) return; // người dùng đã chuyển hội thoại khác
                    foreach (var m in history)
                    {
                        bool isMine = m.UserId == me;
                        _view.AppendMessage(m, isMine);
                    }
                });
            });
        }

        private void OnSendRequested(int toUserId, string text)
        {
            if (toUserId <= 0 || string.IsNullOrWhiteSpace(text))
            {
                _view.ShowInfo(Lang.Get("chat.error.selectPeer"));
                return;
            }
            if (!_client.IsConnected)
            {
                _view.ShowError(Lang.Get("chat.error.notConnected"));
                return;
            }

            bool queuedOrSent = _client.SendStaffMessage(toUserId, text);
            var me = UserSession.Instance.CurrentUser;
            var local = ChatMessageDto.StaffChat(me?.UserId ?? 0, me?.FullName ?? me?.Username, toUserId, text.Trim());
            _view.AppendMessage(local, true);
            _view.ClearInput();

            if (!queuedOrSent)
                _view.ShowInfo(Lang.Get("chat.info.queued"));
        }

        public void NotifyPeerDisplayName(int userId, string name)
        {
            if (userId == _selectedPeerId)
            {
                _selectedPeerName = name ?? string.Empty;
                _view.SetSelectedPeer(userId, _selectedPeerName);
            }
        }

        private void RunOnUi(Action action)
        {
            if (_syncControl == null || _syncControl.IsDisposed)
            {
                action();
                return;
            }
            if (_syncControl.InvokeRequired)
                _syncControl.BeginInvoke(action);
            else
                action();
        }

        public void Dispose()
        {
            UnbindClientEvents();
            _view.ViewLoaded -= OnViewLoaded;
            _view.ViewUnloaded -= OnViewUnloaded;
            _view.SendRequested -= OnSendRequested;
            _view.ConversationSelected -= OnConversationSelected;
        }
    }
}
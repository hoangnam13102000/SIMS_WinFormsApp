using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Fleck;
using Newtonsoft.Json;
using SIMS_WinFormsApp.Models.Chat;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.Repositories.Implementations;

namespace SIMS_WinFormsApp.Services.Chat
{
    public sealed class ChatServerHost : IDisposable
    {
        private static readonly Lazy<ChatServerHost> Lazy =
            new Lazy<ChatServerHost>(() => new ChatServerHost());
        public static ChatServerHost Instance => Lazy.Value;

        private readonly ConcurrentDictionary<int, IWebSocketConnection> _staffByUserId =
            new ConcurrentDictionary<int, IWebSocketConnection>();
        private readonly ConcurrentDictionary<IWebSocketConnection, StaffSession> _sessionByConn =
            new ConcurrentDictionary<IWebSocketConnection, StaffSession>();

        // Liên kết chat real-time với DB (ChatConversations/ChatMessages, FK -> Users) ----
        private readonly IChatRepository _chatRepository;

        private WebSocketServer _server;
        private bool _started;
        private readonly object _startLock = new object();

        private ChatServerHost() : this(AppComposition.CreateChatRepository()) { }

        internal ChatServerHost(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository ?? throw new ArgumentNullException(nameof(chatRepository));
        }

        public bool IsRunning
        {
            get { lock (_startLock) return _started && _server != null; }
        }

        public void Start()
        {
            lock (_startLock)
            {
                if (_started) return;
                try
                {
                    FleckLog.Level = LogLevel.Warn;
                    _server = new WebSocketServer(ChatWsConfig.ServerListenUrl);
                    _server.RestartAfterListenError = true;
                    _server.Start(socket =>
                    {
                        socket.OnOpen = () => { /* chờ STAFF_JOIN */ };
                        socket.OnClose = () => OnClose(socket);
                        socket.OnError = ex => OnClose(socket);
                        socket.OnMessage = message => OnMessage(socket, message);
                    });
                    _started = true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[ChatServerHost] Start failed: {ex.Message}");
                    _server = null;
                    _started = false;
                }
            }
        }

        public void Stop()
        {
            lock (_startLock)
            {
                if (!_started) return;
                try { _server?.Dispose(); } catch { /* ignore */ }
                _server = null;
                _started = false;
                _staffByUserId.Clear();
                _sessionByConn.Clear();
            }
        }

        public void Dispose() => Stop();

        // ===================== Handlers =====================

        private void OnMessage(IWebSocketConnection socket, string raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return;
            ChatMessageDto msg;
            try
            {
                msg = JsonConvert.DeserializeObject<ChatMessageDto>(raw);
            }
            catch
            {
                return;
            }
            if (msg == null || string.IsNullOrEmpty(msg.Type)) return;

            switch (msg.Type.ToUpperInvariant())
            {
                case "STAFF_JOIN":
                case "JOIN":
                    HandleStaffJoin(socket, msg);
                    break;
                case "STAFF_CHAT":
                    HandleStaffChat(socket, msg);
                    break;
                case "LEAVE":
                    OnClose(socket);
                    break;
            }
        }

        private void HandleStaffJoin(IWebSocketConnection socket, ChatMessageDto msg)
        {
            if (msg.UserId <= 0) return;

            if (_staffByUserId.TryGetValue(msg.UserId, out var old) && old != socket)
            {
                try { old.Close(); } catch { /* ignore */ }
                _sessionByConn.TryRemove(old, out _);
            }

            var session = new StaffSession
            {
                UserId = msg.UserId,
                UserName = msg.UserName ?? ("User" + msg.UserId),
                RoleCode = msg.RoleCode ?? string.Empty
            };
            _sessionByConn[socket] = session;
            _staffByUserId[msg.UserId] = socket;

            BroadcastPresence();
        }

        private void HandleStaffChat(IWebSocketConnection socket, ChatMessageDto msg)
        {
            if (!_sessionByConn.TryGetValue(socket, out var sender))
                return;

            msg.UserId = sender.UserId;
            msg.UserName = sender.UserName;
            msg.RoleCode = sender.RoleCode;
            msg.Staff = true;
            msg.Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            if (string.IsNullOrWhiteSpace(msg.Text)) return;

            // Lưu vào DB (liên kết với tài khoản qua SenderUserID/StaffUserIdA/StaffUserIdB)
            // để tin nhắn không mất khi server restart. Không chặn luồng relay real-time
            // nếu DB tạm thời không kết nối được.
            if (msg.ToUserId > 0)
            {
                try
                {
                    msg.MessageId = _chatRepository.SaveStaffMessage(
                        msg.UserId, msg.ToUserId, msg.UserId, msg.UserName, msg.Text,
                        DateTimeOffset.FromUnixTimeMilliseconds(msg.Timestamp).UtcDateTime);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[ChatServerHost] Lưu tin nhắn vào DB thất bại: {ex.Message}");
                }
            }

            var json = JsonConvert.SerializeObject(msg);

            if (msg.ToUserId > 0 && _staffByUserId.TryGetValue(msg.ToUserId, out var target) && target.IsAvailable)
            {
                try { target.Send(json); } catch { /* ignore */ }
            }

            if (socket.IsAvailable)
            {
                try { socket.Send(json); } catch { /* ignore */ }
            }
        }

        private void OnClose(IWebSocketConnection socket)
        {
            if (_sessionByConn.TryRemove(socket, out var session))
            {
                _staffByUserId.TryRemove(session.UserId, out _);
                BroadcastPresence();
            }
        }

        private void BroadcastPresence()
        {
            var online = _sessionByConn.Values
                .Select(s => $"{s.UserId}|{s.UserName}|{s.RoleCode}")
                .Distinct()
                .ToArray();

            var presence = ChatMessageDto.Presence(online);
            var json = JsonConvert.SerializeObject(presence);

            foreach (var kv in _staffByUserId)
            {
                var conn = kv.Value;
                if (conn != null && conn.IsAvailable)
                {
                    try { conn.Send(json); } catch { /* ignore */ }
                }
            }
        }

        private sealed class StaffSession
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string RoleCode { get; set; }
        }
    }
}
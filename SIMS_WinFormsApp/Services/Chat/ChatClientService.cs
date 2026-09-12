using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SIMS_WinFormsApp.Models.Chat;

namespace SIMS_WinFormsApp.Services.Chat
{
    public sealed class ChatClientService : IChatClientService
    {
        private static readonly Lazy<ChatClientService> Lazy =
            new Lazy<ChatClientService>(() => new ChatClientService());
        public static ChatClientService Instance => Lazy.Value;

        private const int MaxPending = 50;
        private static readonly TimeSpan ReconnectInterval = TimeSpan.FromSeconds(5);

        private readonly object _sync = new object();
        private readonly ConcurrentQueue<string> _pending = new ConcurrentQueue<string>();
        private ClientWebSocket _socket;
        private CancellationTokenSource _cts;
        private Timer _reconnectTimer;
        private bool _wantConnected;
        private bool _disposed;

        private int _userId;
        private string _userName;
        private string _roleCode;

        public bool IsConnected
        {
            get
            {
                var s = _socket;
                return s != null && s.State == WebSocketState.Open;
            }
        }

        public int CurrentUserId => _userId;
        public string CurrentUserName => _userName;

        public event Action<ChatMessageDto> MessageReceived;
        public event Action<bool> ConnectionChanged;
        public event Action<IReadOnlyList<OnlineUserInfo>> PresenceUpdated;

        private ChatClientService() { }

        public void ConnectStaff(int userId, string userName, string roleCode)
        {
            if (_disposed) throw new ObjectDisposedException(nameof(ChatClientService));
            lock (_sync)
            {
                _userId = userId;
                _userName = userName ?? string.Empty;
                _roleCode = roleCode ?? string.Empty;
                _wantConnected = true;
            }
            EnsureReconnectTimer();
            _ = Task.Run(AttemptConnectAsync);
        }

        public void Disconnect()
        {
            lock (_sync)
            {
                _wantConnected = false;
            }
            CloseSocketQuietly();
            RaiseConnection(false);
        }

        public bool SendStaffMessage(int toUserId, string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return false;
            var msg = ChatMessageDto.StaffChat(_userId, _userName, toUserId, text.Trim());
            return SendOrQueue(msg);
        }

        public void FlushPending()
        {
            while (_pending.TryDequeue(out var json))
            {
                if (!TrySendRaw(json))
                {
                    var rest = new List<string> { json };
                    while (_pending.TryDequeue(out var x)) rest.Add(x);
                    foreach (var r in rest) _pending.Enqueue(r);
                    break;
                }
            }
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            Disconnect();
            _reconnectTimer?.Dispose();
            _reconnectTimer = null;
            _cts?.Cancel();
            _cts?.Dispose();
        }

        // ===================== Internal =====================

        private void EnsureReconnectTimer()
        {
            if (_reconnectTimer != null) return;
            _reconnectTimer = new Timer(_ =>
            {
                if (!_wantConnected || IsConnected) return;
                _ = Task.Run(AttemptConnectAsync);
            }, null, ReconnectInterval, ReconnectInterval);
        }

        private async Task AttemptConnectAsync()
        {
            if (!_wantConnected || IsConnected) return;

            CloseSocketQuietly();

            var socket = new ClientWebSocket();
            var cts = new CancellationTokenSource();
            lock (_sync)
            {
                _socket = socket;
                _cts = cts;
            }

            try
            {
                var uri = new Uri(ChatWsConfig.ClientEndpoint);
                await socket.ConnectAsync(uri, cts.Token).ConfigureAwait(false);

                var join = ChatMessageDto.StaffJoin(_userId, _userName, _roleCode);
                var joinJson = JsonConvert.SerializeObject(join);
                var bytes = Encoding.UTF8.GetBytes(joinJson);
                await socket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, cts.Token)
                    .ConfigureAwait(false);

                RaiseConnection(true);
                FlushPending();

                var buffer = new byte[64 * 1024];
                var sb = new StringBuilder();
                while (socket.State == WebSocketState.Open && !cts.IsCancellationRequested)
                {
                    sb.Clear();
                    WebSocketReceiveResult result;
                    do
                    {
                        result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), cts.Token)
                            .ConfigureAwait(false);
                        if (result.MessageType == WebSocketMessageType.Close)
                        {
                            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "bye", CancellationToken.None)
                                .ConfigureAwait(false);
                            RaiseConnection(false);
                            return;
                        }
                        sb.Append(Encoding.UTF8.GetString(buffer, 0, result.Count));
                    } while (!result.EndOfMessage);

                    HandleIncoming(sb.ToString());
                }
            }
            catch (OperationCanceledException) { /* expected on dispose */ }
            catch (Exception)
            {
                RaiseConnection(false);
            }
            finally
            {
                if (!IsConnected)
                    RaiseConnection(false);
            }
        }

        private void HandleIncoming(string json)
        {
            if (string.IsNullOrWhiteSpace(json)) return;
            try
            {
                var msg = JsonConvert.DeserializeObject<ChatMessageDto>(json);
                if (msg == null) return;

                if (msg.IsPresence && msg.OnlineUsers != null)
                {
                    var list = new List<OnlineUserInfo>();
                    foreach (var entry in msg.OnlineUsers)
                    {
                        if (string.IsNullOrEmpty(entry)) continue;
                        var parts = entry.Split('|');
                        if (parts.Length < 2) continue;
                        int.TryParse(parts[0], out int id);
                        list.Add(new OnlineUserInfo
                        {
                            UserId = id,
                            UserName = parts[1],
                            RoleCode = parts.Length > 2 ? parts[2] : string.Empty,
                            IsOnline = true
                        });
                    }
                    PresenceUpdated?.Invoke(list);
                }

                MessageReceived?.Invoke(msg);
            }
            catch
            {
                // ignore malformed
            }
        }

        private bool SendOrQueue(ChatMessageDto msg)
        {
            var json = JsonConvert.SerializeObject(msg);
            if (TrySendRaw(json)) return true;

            if (_pending.Count >= MaxPending)
                _pending.TryDequeue(out _);
            _pending.Enqueue(json);
            return false;
        }

        private bool TrySendRaw(string json)
        {
            var socket = _socket;
            if (socket == null || socket.State != WebSocketState.Open) return false;
            try
            {
                var bytes = Encoding.UTF8.GetBytes(json);
                socket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None)
                    .GetAwaiter().GetResult();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void CloseSocketQuietly()
        {
            try
            {
                _cts?.Cancel();
                var s = _socket;
                if (s != null)
                {
                    if (s.State == WebSocketState.Open)
                    {
                        try
                        {
                            s.CloseAsync(WebSocketCloseStatus.NormalClosure, "disconnect", CancellationToken.None)
                                .GetAwaiter().GetResult();
                        }
                        catch { /* ignore */ }
                    }
                    s.Dispose();
                }
            }
            catch { /* ignore */ }
            finally
            {
                lock (_sync)
                {
                    _socket = null;
                }
            }
        }

        private void RaiseConnection(bool connected)
        {
            try { ConnectionChanged?.Invoke(connected); }
            catch { /* listener error */ }
        }
    }
}
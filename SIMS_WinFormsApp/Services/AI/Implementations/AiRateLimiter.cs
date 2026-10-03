using System;
using System.Collections.Generic;
using SIMS_WinFormsApp.Services.AI.Interfaces;
using SIMS_WinFormsApp.UI.I18n;

namespace SIMS_WinFormsApp.Services.AI.Implementations
{
    public sealed class AiRateLimiter : IAiRateLimiter
    {
        private readonly int _maximumCalls;
        private readonly TimeSpan _window;
        private readonly Dictionary<int, Queue<DateTime>> _callsByUser =
            new Dictionary<int, Queue<DateTime>>();
        private readonly object _sync = new object();

        public AiRateLimiter(int maximumCalls, TimeSpan window)
        {
            if (maximumCalls <= 0) throw new ArgumentOutOfRangeException(nameof(maximumCalls));
            if (window <= TimeSpan.Zero) throw new ArgumentOutOfRangeException(nameof(window));
            _maximumCalls = maximumCalls;
            _window = window;
        }

        public bool TryAcquire(int userId, out string message)
        {
            message = string.Empty;
            if (userId <= 0)
            {
                message = Lang.Get("ai.error.session");
                return false;
            }

            lock (_sync)
            {
                DateTime now = DateTime.UtcNow;
                var expiredUsers = new List<int>();
                foreach (var entry in _callsByUser)
                {
                    while (entry.Value.Count > 0 && now - entry.Value.Peek() >= _window)
                        entry.Value.Dequeue();

                    if (entry.Value.Count == 0)
                        expiredUsers.Add(entry.Key);
                }
                foreach (int expiredUserId in expiredUsers)
                    _callsByUser.Remove(expiredUserId);

                Queue<DateTime> calls;
                if (!_callsByUser.TryGetValue(userId, out calls))
                {
                    calls = new Queue<DateTime>();
                    _callsByUser[userId] = calls;
                }

                if (calls.Count >= _maximumCalls)
                {
                    message = Lang.Get("ai.error.rateLimit");
                    return false;
                }

                calls.Enqueue(now);
                return true;
            }
        }
    }
}

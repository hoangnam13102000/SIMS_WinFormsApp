using System;
using System.Configuration;

namespace SIMS_WinFormsApp.Services.Chat
{
    public static class ChatWsConfig
    {
        public const int DefaultPort = 8890;
        public const string DefaultHost = "127.0.0.1";

        public static int Port
        {
            get
            {
                try
                {
                    var s = ConfigurationManager.AppSettings["WS_CHAT_PORT"];
                    if (!string.IsNullOrWhiteSpace(s) && int.TryParse(s.Trim(), out int p) && p > 0 && p < 65536)
                        return p;
                }
                catch { /* ignore */ }
                return DefaultPort;
            }
        }

        public static string Host
        {
            get
            {
                try
                {
                    var s = ConfigurationManager.AppSettings["WS_CHAT_HOST"];
                    if (!string.IsNullOrWhiteSpace(s)) return s.Trim();
                }
                catch { /* ignore */ }
                return DefaultHost;
            }
        }

        public static string ServerListenUrl => $"ws://0.0.0.0:{Port}";

        public static string ClientEndpoint
        {
            get
            {
                var configuredUrl = ConfigurationManager.AppSettings["WS_CHAT_URL"];
                if (string.IsNullOrWhiteSpace(configuredUrl))
                    return $"ws://{Host}:{Port}";

                if (!Uri.TryCreate(configuredUrl.Trim(), UriKind.Absolute, out var uri))
                    throw new ConfigurationErrorsException("WS_CHAT_URL phải là một URL WebSocket hợp lệ.");

                string scheme;
                switch (uri.Scheme.ToLowerInvariant())
                {
                    case "http":
                    case "ws":
                        scheme = "ws";
                        break;
                    case "https":
                    case "wss":
                        scheme = "wss";
                        break;
                    default:
                        throw new ConfigurationErrorsException(
                            "WS_CHAT_URL chỉ hỗ trợ các giao thức http, https, ws hoặc wss.");
                }

                var builder = new UriBuilder(uri) { Scheme = scheme };
                if (uri.IsDefaultPort)
                    builder.Port = -1;

                return builder.Uri.ToString();
            }
        }
    }
}

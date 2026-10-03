using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SIMS_WinFormsApp.Services.AI.Interfaces;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.UI.I18n;

namespace SIMS_WinFormsApp.Services.AI.Implementations
{
    public sealed class AiToolDispatcher : IAiToolDispatcher
    {
        private const int MaximumResultLines = 8;
        private const int MaximumResultCharacters = 2000;

        private readonly IAiToolRegistry _registry;
        private readonly IAiRateLimiter _rateLimiter;

        public AiToolDispatcher(IAiToolRegistry registry, IAiRateLimiter rateLimiter)
        {
            _registry = registry ?? throw new ArgumentNullException(nameof(registry));
            _rateLimiter = rateLimiter ?? throw new ArgumentNullException(nameof(rateLimiter));
        }

        public Task<string> ExecuteAsync(string toolName, string argumentsJson, IUserSession session)
        {
            return Task.Run(() => ExecuteCore(toolName, argumentsJson, session));
        }

        private string ExecuteCore(string toolName, string argumentsJson, IUserSession session)
        {
            var tool = _registry.Find(toolName);
            int userId = session == null || session.CurrentUser == null ? 0 : session.CurrentUser.UserId;
            if (tool == null)
            {
                TraceTool(toolName, userId, "UNKNOWN_TOOL");
                return Lang.Get("ai.error.unknownTool");
            }

            if (!tool.IsAllowed(session))
            {
                TraceTool(toolName, userId, "DENIED");
                return Lang.Get("ai.error.permission");
            }

            string rateLimitMessage;
            if (!_rateLimiter.TryAcquire(userId, out rateLimitMessage))
            {
                TraceTool(toolName, userId, "RATE_LIMITED");
                return rateLimitMessage;
            }

            JObject arguments;
            string validationError;
            if (!AiToolArgumentValidator.TryParseAndValidate(
                argumentsJson,
                tool.ParametersSchemaJson,
                out arguments,
                out validationError))
            {
                TraceTool(toolName, userId, "INVALID_ARGUMENTS");
                return validationError;
            }

            try
            {
                string result = tool.Execute(arguments);
                TraceTool(toolName, userId, "OK");
                return LimitResult(result);
            }
            catch (Exception ex)
            {
                TraceTool(toolName, userId, "ERROR:" + ex.GetType().Name);
                return Lang.Get("ai.error.toolExecution");
            }
        }

        private static string LimitResult(string result)
        {
            string normalized = (result ?? string.Empty).Replace("\r\n", "\n").Replace('\r', '\n');
            string[] lines = normalized.Split('\n');
            var limited = new StringBuilder();

            for (int index = 0; index < lines.Length && index < MaximumResultLines; index++)
            {
                string line = lines[index];
                int separatorLength = limited.Length == 0 ? 0 : Environment.NewLine.Length;
                int remaining = MaximumResultCharacters - limited.Length - separatorLength;
                if (remaining <= 0) break;

                if (limited.Length > 0) limited.AppendLine();
                if (line.Length > remaining)
                {
                    limited.Append(line.Substring(0, remaining));
                    break;
                }
                limited.Append(line);
            }

            if (limited.Length == 0) return Lang.Get("ai.error.emptyToolResult");
            if (normalized.Length > limited.Length || lines.Length > MaximumResultLines)
            {
                if (limited.Length > MaximumResultCharacters - 3)
                    limited.Length = MaximumResultCharacters - 3;
                limited.Append("...");
            }
            return limited.ToString();
        }

        private static void TraceTool(string toolName, int userId, string result)
        {
            Debug.WriteLine("[AI] tool=" + toolName + " user=" + userId + " result=" + result);
        }
    }
}

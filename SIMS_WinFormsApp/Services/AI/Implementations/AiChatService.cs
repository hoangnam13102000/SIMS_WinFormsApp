using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SIMS_WinFormsApp.Models.AI;
using SIMS_WinFormsApp.Services.AI.Interfaces;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.UI.I18n;

namespace SIMS_WinFormsApp.Services.AI.Implementations
{
    public sealed class AiChatService : IAiChatService
    {
        private const int MaximumToolRounds = 4;

        private readonly ILlmClient _llmClient;
        private readonly IAiToolRegistry _toolRegistry;
        private readonly IAiToolDispatcher _toolDispatcher;
        private readonly IAiPromptBuilder _promptBuilder;
        private readonly IUserSession _session;
        private readonly List<JObject> _history = new List<JObject>();
        private readonly object _historyLock = new object();

        public AiChatService(
            ILlmClient llmClient,
            IAiToolRegistry toolRegistry,
            IAiToolDispatcher toolDispatcher,
            IAiPromptBuilder promptBuilder,
            IUserSession session)
        {
            _llmClient = llmClient ?? throw new ArgumentNullException(nameof(llmClient));
            _toolRegistry = toolRegistry ?? throw new ArgumentNullException(nameof(toolRegistry));
            _toolDispatcher = toolDispatcher ?? throw new ArgumentNullException(nameof(toolDispatcher));
            _promptBuilder = promptBuilder ?? throw new ArgumentNullException(nameof(promptBuilder));
            _session = session ?? throw new ArgumentNullException(nameof(session));
        }

        public async Task<string> SendAsync(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new AiChatException(Lang.Get("ai.error.emptyMessage"));
            if (message.Length > 4000)
                throw new AiChatException(Lang.Get("ai.error.messageTooLong"));
            if (!_session.IsLoggedIn)
                throw new AiChatException(Lang.Get("ai.error.session"));

            var contents = CopyHistory();
            contents.Add(new JObject
            {
                ["role"] = "user",
                ["parts"] = new JArray(new JObject { ["text"] = message.Trim() })
            });

            string prompt = _promptBuilder.BuildSystemPrompt(_session);
            var availableTools = await Task.Run(() => _toolRegistry.GetAvailableTools(_session));

            for (int round = 0; round < MaximumToolRounds; round++)
            {
                var response = await _llmClient.GenerateAsync(prompt, contents, availableTools);
                if (response == null)
                    throw new AiChatException(Lang.Get("ai.error.emptyResponse"));

                if (response.ModelContent != null)
                    contents.Add((JObject)response.ModelContent.DeepClone());

                if (response.FunctionCalls.Count == 0)
                {
                    if (string.IsNullOrWhiteSpace(response.Text))
                        throw new AiChatException(Lang.Get("ai.error.emptyResponse"));
                    SaveHistory(contents);
                    return response.Text.Trim();
                }

                var functionResponses = new JArray();
                foreach (var functionCall in response.FunctionCalls)
                {
                    string toolResult = await _toolDispatcher.ExecuteAsync(
                        functionCall.Name,
                        functionCall.ArgumentsJson,
                        _session);
                    functionResponses.Add(new JObject
                    {
                        ["functionResponse"] = new JObject
                        {
                            ["name"] = functionCall.Name,
                            ["response"] = new JObject { ["result"] = toolResult }
                        }
                    });
                }
                contents.Add(new JObject
                {
                    ["role"] = "user",
                    ["parts"] = functionResponses
                });
            }

            var finalResponse = await _llmClient.GenerateAsync(
                prompt,
                contents,
                new List<AiToolDefinition>().AsReadOnly());
            if (finalResponse == null || string.IsNullOrWhiteSpace(finalResponse.Text))
                throw new AiChatException(Lang.Get("ai.error.emptyResponse"));

            if (finalResponse.ModelContent != null)
                contents.Add((JObject)finalResponse.ModelContent.DeepClone());
            SaveHistory(contents);
            return finalResponse.Text.Trim();
        }

        private List<JObject> CopyHistory()
        {
            lock (_historyLock)
                return _history.Select(item => (JObject)item.DeepClone()).ToList();
        }

        private void SaveHistory(IList<JObject> contents)
        {
            lock (_historyLock)
            {
                _history.Clear();
                _history.AddRange(contents.Select(item => (JObject)item.DeepClone()));
            }
        }
    }
}

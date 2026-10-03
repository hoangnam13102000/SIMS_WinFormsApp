using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SIMS_WinFormsApp.Infrastructure.Security;
using SIMS_WinFormsApp.Models.AI;
using SIMS_WinFormsApp.Services.AI.Interfaces;
using SIMS_WinFormsApp.UI.I18n;

namespace SIMS_WinFormsApp.Services.AI.Implementations
{
    public sealed class GeminiClient : ILlmClient
    {
        private static readonly HttpClient SharedHttpClient = CreateHttpClient();

        public async Task<AiLlmResponse> GenerateAsync(
            string systemInstruction,
            IList<JObject> contents,
            IReadOnlyList<AiToolDefinition> tools)
        {
            string apiKey = AppConfig.Get("GEMINI_API_KEY");
            string model = AppConfig.Get("GEMINI_MODEL");
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new AiChatException(Lang.Get("ai.error.missingApiKey"));
            if (string.IsNullOrWhiteSpace(model))
                throw new AiChatException(Lang.Get("ai.error.missingModel"));

            JObject requestBody = BuildRequest(systemInstruction, contents, tools);
            string url = "https://generativelanguage.googleapis.com/v1beta/models/"
                + Uri.EscapeDataString(model)
                + ":generateContent?key="
                + Uri.EscapeDataString(apiKey);

            try
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, url))
                {
                    request.Content = new StringContent(
                        requestBody.ToString(Formatting.None),
                        Encoding.UTF8,
                        "application/json");

                    using (var response = await SharedHttpClient.SendAsync(request))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            string errorBody = await response.Content.ReadAsStringAsync();
                            throw new AiChatException(Lang.Get(
                                "ai.error.http",
                                (int)response.StatusCode,
                                GetSafeErrorMessage(errorBody)));
                        }

                        string responseBody = await response.Content.ReadAsStringAsync();
                        return ParseResponse(responseBody);
                    }
                }
            }
            catch (AiChatException)
            {
                throw;
            }
            catch (TaskCanceledException ex)
            {
                throw new AiChatException(Lang.Get("ai.error.timeout"), ex);
            }
            catch (HttpRequestException ex)
            {
                throw new AiChatException(Lang.Get("ai.error.network"), ex);
            }
            catch (JsonException ex)
            {
                throw new AiChatException(Lang.Get("ai.error.responseFormat"), ex);
            }
        }

        private static HttpClient CreateHttpClient()
        {
            return new HttpClient { Timeout = TimeSpan.FromSeconds(60) };
        }

        private static JObject BuildRequest(
            string systemInstruction,
            IList<JObject> contents,
            IReadOnlyList<AiToolDefinition> tools)
        {
            var contentArray = new JArray();
            if (contents != null)
            {
                foreach (var content in contents)
                {
                    if (content != null) contentArray.Add(content.DeepClone());
                }
            }

            var request = new JObject
            {
                ["systemInstruction"] = new JObject
                {
                    ["parts"] = new JArray(new JObject
                    {
                        ["text"] = systemInstruction ?? string.Empty
                    })
                },
                ["contents"] = contentArray,
                ["generationConfig"] = new JObject { ["temperature"] = 0.2 }
            };

            if (tools != null && tools.Count > 0)
            {
                var declarations = new JArray();
                foreach (var tool in tools)
                {
                    declarations.Add(new JObject
                    {
                        ["name"] = tool.Name,
                        ["description"] = tool.Description,
                        ["parameters"] = JObject.Parse(tool.ParametersSchemaJson)
                    });
                }
                request["tools"] = new JArray(new JObject { ["functionDeclarations"] = declarations });
            }

            return request;
        }

        private static AiLlmResponse ParseResponse(string responseBody)
        {
            JObject response = JObject.Parse(responseBody);
            var candidates = response["candidates"] as JArray;
            var candidate = candidates == null || candidates.Count == 0
                ? null
                : candidates[0] as JObject;
            var content = candidate == null ? null : candidate["content"] as JObject;
            var parts = content == null ? null : content["parts"] as JArray;
            if (content == null || parts == null)
                throw new AiChatException(Lang.Get("ai.error.emptyResponse"));

            string text = string.Empty;
            var functionCalls = new List<AiFunctionCall>();
            foreach (var partToken in parts)
            {
                var part = partToken as JObject;
                if (part == null) continue;

                string partText = (string)part["text"];
                if (!string.IsNullOrEmpty(partText))
                    text += (text.Length == 0 ? string.Empty : Environment.NewLine) + partText;

                var functionCall = part["functionCall"] as JObject;
                if (functionCall == null) continue;

                string name = (string)functionCall["name"];
                var arguments = functionCall["args"] as JObject ?? new JObject();
                if (!string.IsNullOrWhiteSpace(name))
                    functionCalls.Add(new AiFunctionCall(name, arguments.ToString(Formatting.None)));
            }

            return new AiLlmResponse(text, (JObject)content.DeepClone(), functionCalls.AsReadOnly());
        }

        private static string GetSafeErrorMessage(string responseBody)
        {
            try
            {
                JObject errorDocument = JObject.Parse(responseBody);
                string message = (string)errorDocument["error"]?["message"];
                if (string.IsNullOrWhiteSpace(message))
                    return Lang.Get("ai.error.httpNoDetails");

                message = message.Replace("\r", " ").Replace("\n", " ").Trim();
                const int maximumLength = 400;
                if (message.Length > maximumLength)
                    message = message.Substring(0, maximumLength) + "...";
                return message;
            }
            catch (JsonException)
            {
                return Lang.Get("ai.error.httpNoDetails");
            }
        }
    }
}

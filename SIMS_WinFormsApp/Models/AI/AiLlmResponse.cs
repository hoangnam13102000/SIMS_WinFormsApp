using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace SIMS_WinFormsApp.Models.AI
{
    public sealed class AiLlmResponse
    {
        public AiLlmResponse(string text, JObject modelContent, IReadOnlyList<AiFunctionCall> functionCalls)
        {
            Text = text ?? string.Empty;
            ModelContent = modelContent;
            FunctionCalls = functionCalls ?? new List<AiFunctionCall>();
        }

        public string Text { get; private set; }
        public JObject ModelContent { get; private set; }
        public IReadOnlyList<AiFunctionCall> FunctionCalls { get; private set; }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SIMS_WinFormsApp.Models.AI;

namespace SIMS_WinFormsApp.Services.AI.Interfaces
{
    public interface ILlmClient
    {
        Task<AiLlmResponse> GenerateAsync(
            string systemInstruction,
            IList<JObject> contents,
            IReadOnlyList<AiToolDefinition> tools);
    }
}

using System.Collections.Generic;
using SIMS_WinFormsApp.Models.AI;
using SIMS_WinFormsApp.Services.Session;

namespace SIMS_WinFormsApp.Services.AI.Interfaces
{
    public interface IAiToolRegistry
    {
        IReadOnlyList<AiToolDefinition> GetAvailableTools(IUserSession session);
        IAiTool Find(string name);
    }
}

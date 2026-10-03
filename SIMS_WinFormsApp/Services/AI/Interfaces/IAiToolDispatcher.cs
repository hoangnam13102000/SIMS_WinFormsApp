using System.Threading.Tasks;
using SIMS_WinFormsApp.Services.Session;

namespace SIMS_WinFormsApp.Services.AI.Interfaces
{
    public interface IAiToolDispatcher
    {
        Task<string> ExecuteAsync(string toolName, string argumentsJson, IUserSession session);
    }
}

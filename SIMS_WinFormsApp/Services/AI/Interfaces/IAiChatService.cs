using System.Threading.Tasks;

namespace SIMS_WinFormsApp.Services.AI.Interfaces
{
    public interface IAiChatService
    {
        Task<string> SendAsync(string message);
    }
}

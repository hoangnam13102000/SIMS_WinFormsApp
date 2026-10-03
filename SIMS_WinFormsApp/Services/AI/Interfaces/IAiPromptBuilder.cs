using SIMS_WinFormsApp.Services.Session;

namespace SIMS_WinFormsApp.Services.AI.Interfaces
{
    public interface IAiPromptBuilder
    {
        string BuildSystemPrompt(IUserSession session);
    }
}

using Newtonsoft.Json.Linq;
using SIMS_WinFormsApp.Services.Session;

namespace SIMS_WinFormsApp.Services.AI.Interfaces
{
    public interface IAiTool
    {
        string Name { get; }
        string Description { get; }
        string ParametersSchemaJson { get; }
        bool IsAllowed(IUserSession session);
        string Execute(JObject args);
    }
}

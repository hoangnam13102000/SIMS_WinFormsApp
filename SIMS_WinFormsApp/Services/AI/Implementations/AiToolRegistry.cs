using System;
using System.Collections.Generic;
using System.Linq;
using SIMS_WinFormsApp.Models.AI;
using SIMS_WinFormsApp.Services.AI.Interfaces;
using SIMS_WinFormsApp.Services.Session;

namespace SIMS_WinFormsApp.Services.AI.Implementations
{
    public sealed class AiToolRegistry : IAiToolRegistry
    {
        private readonly IReadOnlyList<IAiTool> _tools;
        private readonly Dictionary<string, IAiTool> _toolsByName;

        public AiToolRegistry(IEnumerable<IAiTool> tools)
        {
            if (tools == null) throw new ArgumentNullException(nameof(tools));
            _tools = tools.ToList().AsReadOnly();
            _toolsByName = new Dictionary<string, IAiTool>(StringComparer.Ordinal);

            foreach (var tool in _tools)
            {
                if (tool == null) throw new ArgumentException("Danh sách tool không được chứa null.", nameof(tools));
                if (string.IsNullOrWhiteSpace(tool.Name))
                    throw new ArgumentException("Tên tool không được để trống.", nameof(tools));
                if (_toolsByName.ContainsKey(tool.Name))
                    throw new ArgumentException("Tên tool bị trùng: " + tool.Name, nameof(tools));
                _toolsByName.Add(tool.Name, tool);
            }
        }

        public IReadOnlyList<AiToolDefinition> GetAvailableTools(IUserSession session)
        {
            return _tools
                .Where(tool => tool.IsAllowed(session))
                .Select(tool => new AiToolDefinition(
                    tool.Name,
                    tool.Description,
                    tool.ParametersSchemaJson))
                .ToList()
                .AsReadOnly();
        }

        public IAiTool Find(string name)
        {
            IAiTool tool;
            return name != null && _toolsByName.TryGetValue(name, out tool) ? tool : null;
        }
    }
}

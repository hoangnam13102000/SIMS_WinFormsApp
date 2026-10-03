using System;
using Newtonsoft.Json.Linq;
using SIMS_WinFormsApp.Models.Permission;
using SIMS_WinFormsApp.Services.AI.Interfaces;
using SIMS_WinFormsApp.Services.Session;

namespace SIMS_WinFormsApp.Services.AI.Implementations
{
    public abstract class AiToolBase : IAiTool
    {
        private readonly IAiToolPermissionPolicy _permissionPolicy;
        private readonly AppPermission _requiredPermission;

        protected AiToolBase(
            IAiToolPermissionPolicy permissionPolicy,
            AppPermission requiredPermission,
            string name,
            string description,
            string parametersSchemaJson)
        {
            _permissionPolicy = permissionPolicy
                ?? throw new ArgumentNullException(nameof(permissionPolicy));
            _requiredPermission = requiredPermission;
            Name = name;
            Description = description;
            ParametersSchemaJson = parametersSchemaJson;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ParametersSchemaJson { get; private set; }

        public bool IsAllowed(IUserSession session)
        {
            return _permissionPolicy.IsAllowed(session, _requiredPermission);
        }

        public abstract string Execute(JObject args);
    }
}

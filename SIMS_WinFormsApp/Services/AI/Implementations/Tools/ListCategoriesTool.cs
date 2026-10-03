using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SIMS_WinFormsApp.Models.Permission;
using SIMS_WinFormsApp.Services.AI.Implementations;
using SIMS_WinFormsApp.Services.AI.Interfaces;
using SIMS_WinFormsApp.Services.Interfaces.Pos;

namespace SIMS_WinFormsApp.Services.AI.Implementations.Tools
{
    public sealed class ListCategoriesTool : AiToolBase
    {
        private const string Schema =
            "{\"type\":\"OBJECT\",\"properties\":{},\"required\":[]}";

        private readonly IProductCatalogService _catalogService;

        public ListCategoriesTool(
            IProductCatalogService catalogService,
            IAiToolPermissionPolicy permissionPolicy)
            : base(
                permissionPolicy,
                AppPermission.CATEGORY_VIEW,
                "list_categories",
                "Liệt kê danh mục sản phẩm đang hoạt động.",
                Schema)
        {
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
        }

        public override string Execute(JObject args)
        {
            var categories = _catalogService.GetCategories()
                .Take(30)
                .Select(category => new JObject
                {
                    ["category_id"] = category.CategoryId,
                    ["name"] = category.Name
                });

            return new JObject { ["categories"] = new JArray(categories) }.ToString(Formatting.None);
        }
    }
}

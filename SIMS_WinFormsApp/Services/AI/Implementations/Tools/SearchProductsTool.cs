using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SIMS_WinFormsApp.Models.DTOs.Pos;
using SIMS_WinFormsApp.Models.Permission;
using SIMS_WinFormsApp.Services.AI.Implementations;
using SIMS_WinFormsApp.Services.AI.Interfaces;
using SIMS_WinFormsApp.Services.Interfaces.Pos;

namespace SIMS_WinFormsApp.Services.AI.Implementations.Tools
{
    public sealed class SearchProductsTool : AiToolBase
    {
        private const string Schema =
            "{\"type\":\"OBJECT\",\"properties\":{\"keyword\":{\"type\":\"STRING\",\"description\":\"Từ khóa tên hoặc mã sản phẩm\",\"minLength\":1,\"maxLength\":80}},\"required\":[\"keyword\"]}";

        private readonly IProductCatalogService _catalogService;

        public SearchProductsTool(
            IProductCatalogService catalogService,
            IAiToolPermissionPolicy permissionPolicy)
            : base(
                permissionPolicy,
                AppPermission.PRODUCT_VIEW,
                "search_products",
                "Tìm sản phẩm đang hoạt động theo tên hoặc mã sản phẩm.",
                Schema)
        {
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
        }

        public override string Execute(JObject args)
        {
            string keyword = (string)args["keyword"];
            var products = _catalogService.Search(keyword, null)
                .Take(8)
                .Select(ToProductJson)
                .ToArray();
            return new JObject { ["products"] = new JArray(products) }.ToString(Formatting.None);
        }

        private static JObject ToProductJson(ProductCatalogItemDto product)
        {
            return new JObject
            {
                ["product_id"] = product.ProductId,
                ["name"] = product.Name,
                ["product_code"] = product.Barcode,
                ["category"] = product.CategoryName,
                ["price"] = product.Price
            };
        }
    }
}

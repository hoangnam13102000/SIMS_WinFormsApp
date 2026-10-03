using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SIMS_WinFormsApp.Models.DTOs.Pos;
using SIMS_WinFormsApp.Models.Permission;
using SIMS_WinFormsApp.Services.AI.Implementations;
using SIMS_WinFormsApp.Services.AI.Interfaces;
using SIMS_WinFormsApp.Services.Interfaces.Pos;

namespace SIMS_WinFormsApp.Services.AI.Implementations.Tools
{
    public sealed class GetStockStatusTool : AiToolBase
    {
        private const string Schema =
            "{\"type\":\"OBJECT\",\"properties\":{\"product_id\":{\"type\":\"INTEGER\",\"description\":\"ID sản phẩm trong SIMS\",\"minimum\":1}},\"required\":[\"product_id\"]}";

        private readonly IProductCatalogService _catalogService;

        public GetStockStatusTool(
            IProductCatalogService catalogService,
            IAiToolPermissionPolicy permissionPolicy)
            : base(
                permissionPolicy,
                AppPermission.STOCK_VIEW,
                "get_stock_status",
                "Lấy số lượng tồn hiện tại của một sản phẩm theo ID.",
                Schema)
        {
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
        }

        public override string Execute(JObject args)
        {
            int productId = (int)args["product_id"];
            ProductCatalogItemDto product = _catalogService.GetById(productId);
            if (product == null) return "Không tìm thấy sản phẩm đang hoạt động với ID này.";

            return new JObject
            {
                ["product_id"] = product.ProductId,
                ["name"] = product.Name,
                ["stock_quantity"] = product.StockQuantity,
                ["status"] = product.StockQuantity <= 0 ? "Hết hàng" : "Còn hàng"
            }.ToString(Formatting.None);
        }
    }
}

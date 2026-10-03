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
    public sealed class GetProductDetailTool : AiToolBase
    {
        private const string Schema =
            "{\"type\":\"OBJECT\",\"properties\":{\"product_id\":{\"type\":\"INTEGER\",\"description\":\"ID sản phẩm trong SIMS\",\"minimum\":1}},\"required\":[\"product_id\"]}";

        private readonly IProductCatalogService _catalogService;

        public GetProductDetailTool(
            IProductCatalogService catalogService,
            IAiToolPermissionPolicy permissionPolicy)
            : base(
                permissionPolicy,
                AppPermission.PRODUCT_VIEW,
                "get_product_detail",
                "Lấy thông tin sản phẩm theo ID, không bao gồm số lượng tồn kho.",
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
                ["product_code"] = product.Barcode,
                ["category"] = product.CategoryName,
                ["price"] = product.Price
            }.ToString(Formatting.None);
        }
    }
}

using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs.Pos;

namespace SIMS_WinFormsApp.Services.Interfaces.Pos
{
    public interface IProductCatalogService
    {
        IReadOnlyList<CategoryOptionDto> GetCategories();

        IReadOnlyList<ProductCatalogItemDto> Search(string keyword, int? categoryId);

        /// <summary>Tra 1 sản phẩm theo Id (dùng khi bấm "Thêm vào giỏ" trên 1 thẻ sản phẩm đang hiển thị).</summary>
        ProductCatalogItemDto GetById(int productId);

        /// <summary>Tra sản phẩm theo mã vạch (dùng khi quét bằng webcam) - null nếu không có.</summary>
        ProductCatalogItemDto FindByBarcode(string barcode);
    }
}
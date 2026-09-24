using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs.Catalog;

namespace SIMS_WinFormsApp.Services.Interfaces
{
    public interface ICatalogAdminService
    {
        CatalogPage<ProductRowDto> GetProducts(int pageIndex, int pageSize, string keyword, string status);
        ProductEditDto GetProduct(int productId);
        CatalogSaveResult SaveProduct(ProductEditDto dto);
        CatalogSaveResult SetProductActive(int productId, bool active);
        IReadOnlyList<LookupItem> GetCategoryOptions();
        IReadOnlyList<LookupItem> GetSupplierOptions();

        CatalogPage<CategoryRowDto> GetCategories(int pageIndex, int pageSize, string keyword, string status);
        CategoryEditDto GetCategory(int categoryId);
        CatalogSaveResult SaveCategory(CategoryEditDto dto);
        CatalogSaveResult SetCategoryActive(int categoryId, bool active);

        CatalogPage<SupplierRowDto> GetSuppliers(int pageIndex, int pageSize, string keyword, bool debtOnly);
        SupplierEditDto GetSupplier(int supplierId);
        CatalogSaveResult SaveSupplier(SupplierEditDto dto);
        CatalogSaveResult DeleteSupplier(int supplierId);
    }
}

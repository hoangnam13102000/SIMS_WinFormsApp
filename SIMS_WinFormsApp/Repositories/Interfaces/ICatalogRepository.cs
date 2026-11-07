using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs.Catalog;

namespace SIMS_WinFormsApp.Repositories.Interfaces
{
    public interface ICatalogRepository
    {
        CatalogPage<ProductRowDto> GetProducts(int pageIndex, int pageSize, string keyword, string status, int? categoryId);
        ProductEditDto GetProduct(int productId);
        int InsertProduct(ProductEditDto dto);
        void UpdateProduct(ProductEditDto dto);
        void SetProductStatus(int productId, bool active);
        int? FindProductIdByName(string name);

        CatalogPage<CategoryRowDto> GetCategories(int pageIndex, int pageSize, string keyword, string status);
        IReadOnlyList<LookupItem> GetActiveCategories();
        IReadOnlyList<LookupItem> GetAllCategories();
        CategoryEditDto GetCategory(int categoryId);
        int InsertCategory(CategoryEditDto dto);
        void UpdateCategory(CategoryEditDto dto);
        void SetCategoryStatus(int categoryId, bool active);
        int? FindCategoryIdByName(string name);

        CatalogPage<SupplierRowDto> GetSuppliers(int pageIndex, int pageSize, string keyword);
        SupplierEditDto GetSupplier(int supplierId);
        int InsertSupplier(SupplierEditDto dto);
        void UpdateSupplier(SupplierEditDto dto);
        void SoftDeleteSupplier(int supplierId);
        int? FindSupplierIdByName(string name);
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.UI.Controls;

namespace SIMS_WinFormsApp.MVP.Presenters.Catalog
{
    public sealed class ProductManagementPresenter
    {
        private readonly ICatalogAdminService _service;
        private IReadOnlyList<ProductRowDto> _currentRows = Array.Empty<ProductRowDto>();

        public ProductManagementPresenter(ICatalogAdminService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public IReadOnlyList<ProductRowDto> CurrentRows => _currentRows;

        public TablePageResult Load(int pageIndex, int pageSize, string keyword, string status)
        {
            var page = _service.GetProducts(pageIndex, pageSize, keyword, NormalizeStatus(status));
            _currentRows = page.Rows;
            return new TablePageResult
            {
                TotalCount = page.TotalCount,
                Rows = page.Rows.Select(row => new object[]
                {
                    row.ImagePath,
                    row.Code,
                    row.Name,
                    row.CategoryName,
                    CatalogFormat.Money(row.SellPrice),
                    row.Stock.ToString(CultureInfo.InvariantCulture),
                    CatalogFormat.Status(row.IsActive)
                }).ToList()
            };
        }

        public ProductEditDto Get(int productId) => _service.GetProduct(productId);
        public ProductEditDto NewDraft() => new ProductEditDto { MinStock = 5, IsActive = true };
        public CatalogSaveResult Save(ProductEditDto dto) => _service.SaveProduct(dto);
        public CatalogSaveResult SetActive(int productId, bool active) => _service.SetProductActive(productId, active);
        public IReadOnlyList<LookupItem> Categories() => _service.GetCategoryOptions();
        public IReadOnlyList<LookupItem> Suppliers() => _service.GetSupplierOptions();

        public IReadOnlyList<object[]> ExportRows()
        {
            var page = _service.GetProducts(0, 100000, null, null);
            return page.Rows.Select(row => new object[]
            {
                row.Code, row.Name, row.CategoryName, CatalogFormat.Money(row.SellPrice),
                row.Stock, CatalogFormat.Status(row.IsActive)
            }).Cast<object[]>().ToList();
        }

        private static string NormalizeStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status) || status == "ALL") return null;
            return status;
        }
    }

    public sealed class CategoryManagementPresenter
    {
        private readonly ICatalogAdminService _service;
        private IReadOnlyList<CategoryRowDto> _currentRows = Array.Empty<CategoryRowDto>();

        public CategoryManagementPresenter(ICatalogAdminService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public IReadOnlyList<CategoryRowDto> CurrentRows => _currentRows;

        public TablePageResult Load(int pageIndex, int pageSize, string keyword, string status)
        {
            var page = _service.GetCategories(pageIndex, pageSize, keyword, string.IsNullOrWhiteSpace(status) ? null : status);
            _currentRows = page.Rows;
            return new TablePageResult
            {
                TotalCount = page.TotalCount,
                Rows = page.Rows.Select(row => new object[]
                {
                    row.Name,
                    row.ProductCount.ToString(CultureInfo.InvariantCulture),
                    CatalogFormat.Status(row.IsActive)
                }).ToList()
            };
        }

        public CategoryEditDto Get(int categoryId) => _service.GetCategory(categoryId);
        public CategoryEditDto NewDraft() => new CategoryEditDto { IsActive = true };
        public CatalogSaveResult Save(CategoryEditDto dto) => _service.SaveCategory(dto);
        public CatalogSaveResult SetActive(int categoryId, bool active) => _service.SetCategoryActive(categoryId, active);

        public IReadOnlyList<object[]> ExportRows()
        {
            var page = _service.GetCategories(0, 100000, null, null);
            return page.Rows.Select(row => new object[]
            {
                row.Name, row.ProductCount, CatalogFormat.Status(row.IsActive)
            }).Cast<object[]>().ToList();
        }
    }

    public sealed class SupplierManagementPresenter
    {
        private readonly ICatalogAdminService _service;
        private IReadOnlyList<SupplierRowDto> _currentRows = Array.Empty<SupplierRowDto>();

        public SupplierManagementPresenter(ICatalogAdminService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public IReadOnlyList<SupplierRowDto> CurrentRows => _currentRows;

        public TablePageResult Load(int pageIndex, int pageSize, string keyword, string debtFilter)
        {
            bool debtOnly = string.Equals(debtFilter, "DEBT", StringComparison.OrdinalIgnoreCase);
            var page = _service.GetSuppliers(pageIndex, pageSize, keyword, debtOnly);
            _currentRows = page.Rows;
            return new TablePageResult
            {
                TotalCount = page.TotalCount,
                Rows = page.Rows.Select(row => new object[]
                {
                    row.Name,
                    row.Phone,
                    row.Email,
                    row.Address,
                    CatalogFormat.Money(row.DebtBalance)
                }).ToList()
            };
        }

        public SupplierEditDto Get(int supplierId) => _service.GetSupplier(supplierId);
        public SupplierEditDto NewDraft() => new SupplierEditDto();
        public CatalogSaveResult Save(SupplierEditDto dto) => _service.SaveSupplier(dto);
        public CatalogSaveResult Delete(int supplierId) => _service.DeleteSupplier(supplierId);

        public IReadOnlyList<object[]> ExportRows()
        {
            var page = _service.GetSuppliers(0, 100000, null, false);
            return page.Rows.Select(row => new object[]
            {
                row.Name, row.Phone, row.Email, row.Address, row.SuppliedItems, CatalogFormat.Money(row.DebtBalance)
            }).Cast<object[]>().ToList();
        }
    }

    internal static class CatalogFormat
    {
        public static string Status(bool active) => active ? "Đang hoạt động" : "Vô hiệu hóa";

        public static string Money(decimal value) =>
            value.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
    }
}

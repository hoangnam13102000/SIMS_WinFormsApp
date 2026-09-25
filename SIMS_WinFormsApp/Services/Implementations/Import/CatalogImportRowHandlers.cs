using System;
using System.Globalization;
using System.IO;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.Services.Interfaces;

namespace SIMS_WinFormsApp.Services.Implementations.Import
{
    public sealed class ProductImportRowHandler
    {
        public static readonly string[] ExpectedColumns =
        {
            "Tên sản phẩm", "Danh mục", "Nhà cung cấp", "Thương hiệu", "Đơn vị",
            "Khối lượng", "Giá nhập", "Giá bán", "Tồn kho", "Tồn tối thiểu", "Mô tả", "Ảnh"
        };

        private readonly ICatalogAdminService _service;

        public ProductImportRowHandler(ICatalogAdminService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public ImportRowResult Handle(string[] cells, int rowNumber)
        {
            string name = Cell(cells, 0);
            string categoryName = Cell(cells, 1);
            string supplierName = Cell(cells, 2);
            if (string.IsNullOrWhiteSpace(name))
                return ImportRowResult.Failure("Dòng " + rowNumber + ": thiếu tên sản phẩm.");
            if (string.IsNullOrWhiteSpace(categoryName))
                return ImportRowResult.Failure("Dòng " + rowNumber + ": thiếu danh mục.");
            if (!CatalogImportParsing.TryMoney(Cell(cells, 6), out decimal importPrice))
                return ImportRowResult.Failure("Dòng " + rowNumber + ": giá nhập không hợp lệ.");
            if (!CatalogImportParsing.TryMoney(Cell(cells, 7), out decimal sellPrice))
                return ImportRowResult.Failure("Dòng " + rowNumber + ": giá bán không hợp lệ.");

            int categoryId = EnsureCategory(_service, categoryName);
            int? supplierId = string.IsNullOrWhiteSpace(supplierName) ? (int?)null : EnsureSupplier(_service, supplierName);
            string image = Cell(cells, 11);
            if (!string.IsNullOrWhiteSpace(image) && !File.Exists(image))
                return ImportRowResult.Failure("Dòng " + rowNumber + ": không tìm thấy file ảnh.");

            var dto = new ProductEditDto
            {
                Name = name,
                CategoryId = categoryId,
                SupplierId = supplierId,
                Brand = Cell(cells, 3),
                Unit = Cell(cells, 4),
                WeightVolume = Cell(cells, 5),
                ImportPrice = importPrice,
                SellPrice = sellPrice,
                Stock = CatalogImportParsing.ParseInt(Cell(cells, 8), 0),
                MinStock = CatalogImportParsing.ParseInt(Cell(cells, 9), 5),
                Description = Cell(cells, 10),
                ImagePath = image,
                IsActive = true
            };

            var result = _service.SaveProduct(dto);
            return result.Success
                ? ImportRowResult.Success()
                : ImportRowResult.Failure("Dòng " + rowNumber + ": " + result.Message);
        }

        internal static int EnsureCategory(ICatalogAdminService service, string name)
        {
            foreach (var item in service.GetCategoryOptions())
            {
                if (string.Equals(item.Name, name, StringComparison.OrdinalIgnoreCase))
                    return item.Id;
            }
            var created = service.SaveCategory(new CategoryEditDto { Name = name, IsActive = true });
            if (!created.Success) throw new InvalidOperationException(created.Message);
            return created.Id;
        }

        internal static int EnsureSupplier(ICatalogAdminService service, string name)
        {
            foreach (var item in service.GetSupplierOptions())
            {
                if (string.Equals(item.Name, name, StringComparison.OrdinalIgnoreCase))
                    return item.Id;
            }
            var created = service.SaveSupplier(new SupplierEditDto { Name = name });
            if (!created.Success) throw new InvalidOperationException(created.Message);
            return created.Id;
        }

        private static string Cell(string[] cells, int index) =>
            index >= 0 && index < cells.Length && cells[index] != null ? cells[index].Trim() : string.Empty;
    }

    public sealed class CategoryImportRowHandler
    {
        public static readonly string[] ExpectedColumns = { "Tên danh mục", "Trạng thái" };
        private readonly ICatalogAdminService _service;

        public CategoryImportRowHandler(ICatalogAdminService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public ImportRowResult Handle(string[] cells, int rowNumber)
        {
            string name = cells.Length > 0 ? (cells[0] ?? string.Empty).Trim() : string.Empty;
            if (string.IsNullOrWhiteSpace(name))
                return ImportRowResult.Failure("Dòng " + rowNumber + ": thiếu tên danh mục.");

            var result = _service.SaveCategory(new CategoryEditDto
            {
                Name = name,
                IsActive = CatalogImportParsing.IsActive(cells.Length > 1 ? cells[1] : null)
            });
            return result.Success
                ? ImportRowResult.Success()
                : ImportRowResult.Failure("Dòng " + rowNumber + ": " + result.Message);
        }
    }

    public sealed class SupplierImportRowHandler
    {
        public static readonly string[] ExpectedColumns =
        {
            "Tên nhà cung cấp", "Số điện thoại", "Email", "Địa chỉ", "Mặt hàng cung cấp"
        };

        private readonly ICatalogAdminService _service;

        public SupplierImportRowHandler(ICatalogAdminService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public ImportRowResult Handle(string[] cells, int rowNumber)
        {
            string name = Cell(cells, 0);
            if (string.IsNullOrWhiteSpace(name))
                return ImportRowResult.Failure("Dòng " + rowNumber + ": thiếu tên nhà cung cấp.");

            var result = _service.SaveSupplier(new SupplierEditDto
            {
                Name = name,
                Phone = Cell(cells, 1),
                Email = Cell(cells, 2),
                Address = Cell(cells, 3),
                SuppliedItems = Cell(cells, 4)
            });
            return result.Success
                ? ImportRowResult.Success()
                : ImportRowResult.Failure("Dòng " + rowNumber + ": " + result.Message);
        }

        private static string Cell(string[] cells, int index) =>
            index >= 0 && index < cells.Length && cells[index] != null ? cells[index].Trim() : string.Empty;
    }

    internal static class CatalogImportParsing
    {
        public static bool TryMoney(string text, out decimal value)
        {
            value = 0;
            if (string.IsNullOrWhiteSpace(text)) return false;
            string cleaned = text.Trim()
                .Replace(" ", string.Empty)
                .Replace(".", string.Empty)
                .Replace(",", string.Empty)
                .Replace("đ", string.Empty)
                .Replace("VND", string.Empty);
            return decimal.TryParse(cleaned, NumberStyles.Number, CultureInfo.InvariantCulture, out value);
        }

        public static int ParseInt(string text, int fallback)
        {
            if (string.IsNullOrWhiteSpace(text)) return fallback;
            string cleaned = text.Trim().Replace(".", string.Empty).Replace(",", string.Empty);
            int value;
            return int.TryParse(cleaned, NumberStyles.Integer, CultureInfo.InvariantCulture, out value) ? value : fallback;
        }

        public static bool IsActive(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return true;
            string normalized = text.Trim().ToLowerInvariant();
            return normalized != "disabled" && normalized != "vô hiệu hóa" && normalized != "vo hieu hoa" && normalized != "inactive";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.Services.Interfaces;

namespace SIMS_WinFormsApp.Services.Implementations.Catalog
{
    public sealed class CatalogAdminService : ICatalogAdminService
    {
        private readonly ICatalogRepository _repository;
        private readonly LocalProductImageStore _images;

        public CatalogAdminService(ICatalogRepository repository, LocalProductImageStore images)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _images = images ?? throw new ArgumentNullException(nameof(images));
        }

        public CatalogPage<ProductRowDto> GetProducts(int pageIndex, int pageSize, string keyword, string status)
        {
            var page = _repository.GetProducts(pageIndex, pageSize, keyword, status);
            foreach (var row in page.Rows)
                row.ImagePath = _images.Resolve(row.ImagePath);
            return page;
        }

        public ProductEditDto GetProduct(int productId)
        {
            var dto = _repository.GetProduct(productId);
            if (dto != null) dto.ImagePath = _images.Resolve(dto.ImagePath) ?? dto.ImagePath;
            return dto;
        }

        public CatalogSaveResult SaveProduct(ProductEditDto dto)
        {
            string error = ValidateProduct(dto);
            if (error != null) return CatalogSaveResult.Fail(error);
            if (_repository.FindProductIdByName(dto.Name) is int existing && existing != dto.ProductId)
                return CatalogSaveResult.Fail("Đã có sản phẩm tên \"" + dto.Name.Trim() + "\".");

            try
            {
                dto.ImagePath = StoreImageIfNeeded(dto.ImagePath);
                if (dto.ProductId <= 0)
                {
                    int id = _repository.InsertProduct(dto);
                    _repository.SetPreferredSupplier(id, dto.SupplierId);
                    return CatalogSaveResult.Ok(id, "Đã thêm sản phẩm.");
                }

                _repository.UpdateProduct(dto);
                _repository.SetPreferredSupplier(dto.ProductId, dto.SupplierId);
                return CatalogSaveResult.Ok(dto.ProductId, "Đã cập nhật sản phẩm.");
            }
            catch (Exception ex)
            {
                return CatalogSaveResult.Fail(Describe(ex));
            }
        }

        public CatalogSaveResult SetProductActive(int productId, bool active)
        {
            try
            {
                _repository.SetProductStatus(productId, active);
                return CatalogSaveResult.Ok(productId, active ? "Đã mở lại sản phẩm." : "Đã vô hiệu hóa sản phẩm.");
            }
            catch (Exception ex)
            {
                return CatalogSaveResult.Fail(Describe(ex));
            }
        }

        public IReadOnlyList<LookupItem> GetCategoryOptions() => _repository.GetActiveCategories();
        public IReadOnlyList<LookupItem> GetSupplierOptions() => _repository.GetActiveSuppliers();

        public CatalogPage<CategoryRowDto> GetCategories(int pageIndex, int pageSize, string keyword, string status) =>
            _repository.GetCategories(pageIndex, pageSize, keyword, status);

        public CategoryEditDto GetCategory(int categoryId) => _repository.GetCategory(categoryId);

        public CatalogSaveResult SaveCategory(CategoryEditDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Name))
                return CatalogSaveResult.Fail("Nhập tên danh mục.");
            if (dto.Name.Trim().Length > 100)
                return CatalogSaveResult.Fail("Tên danh mục tối đa 100 ký tự.");
            if (_repository.FindCategoryIdByName(dto.Name) is int existing && existing != dto.CategoryId)
                return CatalogSaveResult.Fail("Danh mục \"" + dto.Name.Trim() + "\" đã tồn tại.");

            try
            {
                if (dto.CategoryId <= 0)
                    return CatalogSaveResult.Ok(_repository.InsertCategory(dto), "Đã thêm danh mục.");
                _repository.UpdateCategory(dto);
                return CatalogSaveResult.Ok(dto.CategoryId, "Đã cập nhật danh mục.");
            }
            catch (Exception ex)
            {
                return CatalogSaveResult.Fail(Describe(ex));
            }
        }

        public CatalogSaveResult SetCategoryActive(int categoryId, bool active)
        {
            try
            {
                _repository.SetCategoryStatus(categoryId, active);
                return CatalogSaveResult.Ok(categoryId, active ? "Đã mở lại danh mục." : "Đã vô hiệu hóa danh mục.");
            }
            catch (Exception ex)
            {
                return CatalogSaveResult.Fail(Describe(ex));
            }
        }

        public CatalogPage<SupplierRowDto> GetSuppliers(int pageIndex, int pageSize, string keyword, bool debtOnly) =>
            _repository.GetSuppliers(pageIndex, pageSize, keyword, debtOnly);

        public SupplierEditDto GetSupplier(int supplierId) => _repository.GetSupplier(supplierId);

        public CatalogSaveResult SaveSupplier(SupplierEditDto dto)
        {
            string error = ValidateSupplier(dto);
            if (error != null) return CatalogSaveResult.Fail(error);
            if (_repository.FindSupplierIdByName(dto.Name) is int existing && existing != dto.SupplierId)
                return CatalogSaveResult.Fail("Nhà cung cấp \"" + dto.Name.Trim() + "\" đã tồn tại.");

            try
            {
                if (dto.SupplierId <= 0)
                    return CatalogSaveResult.Ok(_repository.InsertSupplier(dto), "Đã thêm nhà cung cấp.");
                _repository.UpdateSupplier(dto);
                return CatalogSaveResult.Ok(dto.SupplierId, "Đã cập nhật nhà cung cấp.");
            }
            catch (Exception ex)
            {
                return CatalogSaveResult.Fail(Describe(ex));
            }
        }

        public CatalogSaveResult DeleteSupplier(int supplierId)
        {
            try
            {
                _repository.SoftDeleteSupplier(supplierId);
                return CatalogSaveResult.Ok(supplierId, "Đã xóa nhà cung cấp.");
            }
            catch (Exception ex)
            {
                return CatalogSaveResult.Fail(Describe(ex));
            }
        }

        private string StoreImageIfNeeded(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path)) return path;
            string imagesRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProductImages");
            if (path.StartsWith(imagesRoot, StringComparison.OrdinalIgnoreCase)) return path;
            return _images.SaveCopy(path);
        }

        private static string ValidateProduct(ProductEditDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Name)) return "Nhập tên sản phẩm.";
            if (dto.Name.Trim().Length > 150) return "Tên sản phẩm tối đa 150 ký tự.";
            if (dto.CategoryId <= 0) return "Chọn danh mục.";
            if (dto.ImportPrice < 0 || dto.SellPrice < 0) return "Giá không được âm.";
            if (dto.SellPrice < dto.ImportPrice) return "Giá bán phải lớn hơn hoặc bằng giá nhập.";
            if (dto.Stock < 0 || dto.MinStock < 0) return "Tồn kho và tồn tối thiểu không được âm.";
            return null;
        }

        private static string ValidateSupplier(SupplierEditDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Name)) return "Nhập tên nhà cung cấp.";
            if (dto.Name.Trim().Length > 150) return "Tên nhà cung cấp tối đa 150 ký tự.";
            if (!string.IsNullOrWhiteSpace(dto.Phone) && dto.Phone.Trim().Length > 20)
                return "Số điện thoại tối đa 20 ký tự.";
            if (!string.IsNullOrWhiteSpace(dto.Email) && dto.Email.Trim().Length > 100)
                return "Email tối đa 100 ký tự.";
            return null;
        }

        private static string Describe(Exception ex)
        {
            var sql = ex as SqlException;
            if (sql != null && (sql.Number == 2627 || sql.Number == 2601))
                return "Dữ liệu bị trùng với bản ghi đã có.";
            return ex.Message;
        }
    }
}

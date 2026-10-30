using System;
using System.Collections.Generic;
using System.Linq;
using SIMS_WinFormsApp.DAL.Linq;
using SIMS_WinFormsApp.DAL.Linq.Entities.Business;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.Repositories.Interfaces;

namespace SIMS_WinFormsApp.Repositories.Implementations
{
    public sealed class CatalogRepository : ICatalogRepository
    {
        private readonly IDbConnectionFactory _connections;

        public CatalogRepository(IDbConnectionFactory connections)
        {
            _connections = connections ?? throw new ArgumentNullException(nameof(connections));
        }

        public CatalogPage<ProductRowDto> GetProducts(int pageIndex, int pageSize, string keyword, string status)
        {
            ValidatePage(pageIndex, pageSize);
            string term = (keyword ?? string.Empty).Trim();
            using (var db = _connections.CreateDataContext())
            {
                var query = from product in db.Products
                            join category in db.Categories on product.CategoryID equals category.CategoryID
                            where (term == string.Empty || product.ProductName.Contains(term)
                                || product.ProductCode.Contains(term) || category.CategoryName.Contains(term))
                                && (status == null || product.Status == status)
                            select new
                            {
                                product.ProductID, product.ProductCode, product.ProductName,
                                category.CategoryName, product.SellPrice, product.Stock, product.Status, product.ImageUrl
                            };
                int count = query.Count();
                var rows = query.OrderBy(row => row.ProductName).Skip(checked(pageIndex * pageSize)).Take(pageSize)
                    .ToList().Select(row => new ProductRowDto
                    {
                        ProductId = row.ProductID,
                        Code = row.ProductCode,
                        Name = row.ProductName,
                        CategoryName = row.CategoryName,
                        SellPrice = row.SellPrice,
                        Stock = row.Stock,
                        IsActive = row.Status == "ACTIVE",
                        ImagePath = row.ImageUrl
                    }).ToList();
                return new CatalogPage<ProductRowDto> { TotalCount = count, Rows = rows };
            }
        }

        public ProductEditDto GetProduct(int productId)
        {
            using (var db = _connections.CreateDataContext())
            {
                ProductEntity product = db.Products.SingleOrDefault(p => p.ProductID == productId);
                if (product == null) return null;
                return new ProductEditDto
                {
                    ProductId = product.ProductID,
                    Code = product.ProductCode,
                    Name = product.ProductName,
                    CategoryId = product.CategoryID,
                    SupplierId = db.SupplierProducts.Where(sp => sp.ProductID == productId && sp.IsPreferred)
                        .Select(sp => (int?)sp.SupplierID).FirstOrDefault(),
                    Brand = product.Brand,
                    Unit = product.Unit,
                    WeightVolume = product.WeightVolume,
                    Description = product.Description,
                    ImportPrice = product.ImportPrice,
                    SellPrice = product.SellPrice,
                    Stock = product.Stock,
                    MinStock = product.MinStock,
                    ImagePath = product.ImageUrl,
                    IsActive = product.Status == "ACTIVE"
                };
            }
        }

        public int InsertProduct(ProductEditDto dto)
        {
            using (var db = _connections.CreateDataContext())
            {
                var product = new ProductEntity();
                ApplyProduct(product, dto);
                product.Stock = dto.Stock;
                db.Products.InsertOnSubmit(product);
                db.SubmitChanges();
                return product.ProductID;
            }
        }

        public void UpdateProduct(ProductEditDto dto)
        {
            using (var db = _connections.CreateDataContext())
            {
                ProductEntity product = db.Products.SingleOrDefault(p => p.ProductID == dto.ProductId);
                if (product == null) return;
                ApplyProduct(product, dto);
                db.SubmitChanges();
            }
        }

        public void SetProductStatus(int productId, bool active)
        {
            using (var db = _connections.CreateDataContext())
            {
                ProductEntity product = db.Products.SingleOrDefault(p => p.ProductID == productId);
                if (product == null) return;
                product.Status = active ? "ACTIVE" : "DISABLED";
                product.UpdatedAt = DateTime.Now;
                db.SubmitChanges();
            }
        }

        public void SetPreferredSupplier(int productId, int? supplierId)
        {
            using (var db = _connections.CreateDataContext())
            {
                db.Connection.Open();
                using (var transaction = db.Connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    db.Transaction = transaction;
                    var links = db.SupplierProducts.Where(sp => sp.ProductID == productId).ToList();
                    foreach (SupplierProductEntity link in links)
                        link.IsPreferred = false;
                    if (supplierId.HasValue)
                    {
                        SupplierProductEntity selected = links.FirstOrDefault(link => link.SupplierID == supplierId.Value);
                        if (selected == null)
                            db.SupplierProducts.InsertOnSubmit(new SupplierProductEntity
                            {
                                SupplierID = supplierId.Value,
                                ProductID = productId,
                                IsPreferred = true
                            });
                        else
                            selected.IsPreferred = true;
                    }
                    db.SubmitChanges();
                    transaction.Commit();
                }
            }
        }

        public int? FindProductIdByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;
            using (var db = _connections.CreateDataContext())
                return db.Products.Where(p => p.ProductName == name.Trim())
                    .OrderBy(p => p.ProductID).Select(p => (int?)p.ProductID).FirstOrDefault();
        }

        public CatalogPage<CategoryRowDto> GetCategories(int pageIndex, int pageSize, string keyword, string status)
        {
            ValidatePage(pageIndex, pageSize);
            string term = (keyword ?? string.Empty).Trim();
            using (var db = _connections.CreateDataContext())
            {
                var query = db.Categories.Where(c => (term == string.Empty || c.CategoryName.Contains(term))
                    && (status == null || c.Status == status));
                int count = query.Count();
                var rows = query.OrderBy(c => c.CategoryName).Skip(checked(pageIndex * pageSize)).Take(pageSize)
                    .Select(c => new
                    {
                        c.CategoryID, c.CategoryName, c.Status,
                        ProductCount = db.Products.Count(p => p.CategoryID == c.CategoryID)
                    }).ToList().Select(c => new CategoryRowDto
                    {
                        CategoryId = c.CategoryID,
                        Name = c.CategoryName,
                        ProductCount = c.ProductCount,
                        IsActive = c.Status == "ACTIVE"
                    }).ToList();
                return new CatalogPage<CategoryRowDto> { TotalCount = count, Rows = rows };
            }
        }

        public IReadOnlyList<LookupItem> GetActiveCategories()
        {
            using (var db = _connections.CreateDataContext())
                return db.Categories.Where(c => c.Status == "ACTIVE").OrderBy(c => c.CategoryName)
                    .Select(c => new LookupItem { Id = c.CategoryID, Name = c.CategoryName }).ToList();
        }

        public CategoryEditDto GetCategory(int categoryId)
        {
            using (var db = _connections.CreateDataContext())
            {
                CategoryEntity category = db.Categories.SingleOrDefault(c => c.CategoryID == categoryId);
                return category == null ? null : new CategoryEditDto
                {
                    CategoryId = category.CategoryID,
                    Name = category.CategoryName,
                    IsActive = category.Status == "ACTIVE"
                };
            }
        }

        public int InsertCategory(CategoryEditDto dto)
        {
            using (var db = _connections.CreateDataContext())
            {
                var category = new CategoryEntity
                {
                    CategoryName = dto.Name.Trim(),
                    Status = dto.IsActive ? "ACTIVE" : "DISABLED"
                };
                db.Categories.InsertOnSubmit(category);
                db.SubmitChanges();
                return category.CategoryID;
            }
        }

        public void UpdateCategory(CategoryEditDto dto)
        {
            using (var db = _connections.CreateDataContext())
            {
                CategoryEntity category = db.Categories.SingleOrDefault(c => c.CategoryID == dto.CategoryId);
                if (category == null) return;
                category.CategoryName = dto.Name.Trim();
                category.Status = dto.IsActive ? "ACTIVE" : "DISABLED";
                db.SubmitChanges();
            }
        }

        public void SetCategoryStatus(int categoryId, bool active)
        {
            using (var db = _connections.CreateDataContext())
            {
                CategoryEntity category = db.Categories.SingleOrDefault(c => c.CategoryID == categoryId);
                if (category == null) return;
                category.Status = active ? "ACTIVE" : "DISABLED";
                db.SubmitChanges();
            }
        }

        public int? FindCategoryIdByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;
            using (var db = _connections.CreateDataContext())
                return db.Categories.Where(c => c.CategoryName == name.Trim()).OrderBy(c => c.CategoryID)
                    .Select(c => (int?)c.CategoryID).FirstOrDefault();
        }

        public CatalogPage<SupplierRowDto> GetSuppliers(int pageIndex, int pageSize, string keyword)
        {
            ValidatePage(pageIndex, pageSize);
            string term = (keyword ?? string.Empty).Trim();
            using (var db = _connections.CreateDataContext())
            {
                var query = db.Suppliers.Where(s => !s.IsDeleted
                    && (term == string.Empty || s.SupplierName.Contains(term)
                        || s.Phone.Contains(term) || s.Email.Contains(term)));
                int count = query.Count();
                var rows = query.OrderBy(s => s.SupplierName).Skip(checked(pageIndex * pageSize)).Take(pageSize)
                    .Select(s => new SupplierRowDto
                    {
                        SupplierId = s.SupplierID,
                        Name = s.SupplierName,
                        Phone = s.Phone,
                        Email = s.Email,
                        Address = s.Address,
                        SuppliedItems = s.SuppliedItems
                    }).ToList();
                return new CatalogPage<SupplierRowDto> { TotalCount = count, Rows = rows };
            }
        }

        public IReadOnlyList<LookupItem> GetActiveSuppliers()
        {
            using (var db = _connections.CreateDataContext())
                return db.Suppliers.Where(s => !s.IsDeleted).OrderBy(s => s.SupplierName)
                    .Select(s => new LookupItem { Id = s.SupplierID, Name = s.SupplierName }).ToList();
        }

        public SupplierEditDto GetSupplier(int supplierId)
        {
            using (var db = _connections.CreateDataContext())
            {
                SupplierEntity supplier = db.Suppliers.SingleOrDefault(s => s.SupplierID == supplierId && !s.IsDeleted);
                return supplier == null ? null : MapSupplier(supplier);
            }
        }

        public int InsertSupplier(SupplierEditDto dto)
        {
            using (var db = _connections.CreateDataContext())
            {
                var supplier = new SupplierEntity();
                ApplySupplier(supplier, dto);
                supplier.IsDeleted = false;
                db.Suppliers.InsertOnSubmit(supplier);
                db.SubmitChanges();
                return supplier.SupplierID;
            }
        }

        public void UpdateSupplier(SupplierEditDto dto)
        {
            using (var db = _connections.CreateDataContext())
            {
                SupplierEntity supplier = db.Suppliers.SingleOrDefault(s => s.SupplierID == dto.SupplierId && !s.IsDeleted);
                if (supplier == null) return;
                ApplySupplier(supplier, dto);
                db.SubmitChanges();
            }
        }

        public void SoftDeleteSupplier(int supplierId)
        {
            using (var db = _connections.CreateDataContext())
            {
                SupplierEntity supplier = db.Suppliers.SingleOrDefault(s => s.SupplierID == supplierId);
                if (supplier == null) return;
                supplier.IsDeleted = true;
                supplier.DeletedAt = DateTime.Now;
                db.SubmitChanges();
            }
        }

        public int? FindSupplierIdByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;
            using (var db = _connections.CreateDataContext())
                return db.Suppliers.Where(s => !s.IsDeleted && s.SupplierName == name.Trim())
                    .OrderBy(s => s.SupplierID).Select(s => (int?)s.SupplierID).FirstOrDefault();
        }

        private static void ApplyProduct(ProductEntity product, ProductEditDto dto)
        {
            product.ProductName = dto.Name.Trim();
            product.CategoryID = dto.CategoryId;
            product.Brand = Normalize(dto.Brand);
            product.Unit = Normalize(dto.Unit);
            product.WeightVolume = Normalize(dto.WeightVolume);
            product.Description = Normalize(dto.Description);
            product.ImportPrice = dto.ImportPrice;
            product.SellPrice = dto.SellPrice;
            product.Margin = dto.SellPrice - dto.ImportPrice;
            product.AutoPrice = false;
            product.ImageUrl = Normalize(dto.ImagePath);
            product.MinStock = dto.MinStock;
            product.Status = dto.IsActive ? "ACTIVE" : "DISABLED";
            product.UpdatedAt = DateTime.Now;
        }

        private static void ApplySupplier(SupplierEntity supplier, SupplierEditDto dto)
        {
            supplier.SupplierName = dto.Name.Trim();
            supplier.Address = Normalize(dto.Address);
            supplier.Phone = Normalize(dto.Phone);
            supplier.Email = Normalize(dto.Email);
            supplier.SuppliedItems = Normalize(dto.SuppliedItems);
        }

        private static SupplierEditDto MapSupplier(SupplierEntity supplier)
        {
            return new SupplierEditDto
            {
                SupplierId = supplier.SupplierID,
                Name = supplier.SupplierName,
                Address = supplier.Address,
                Phone = supplier.Phone,
                Email = supplier.Email,
                SuppliedItems = supplier.SuppliedItems
            };
        }

        private static string Normalize(string value) =>
            string.IsNullOrWhiteSpace(value) ? null : value.Trim();

        private static void ValidatePage(int pageIndex, int pageSize)
        {
            if (pageIndex < 0) throw new ArgumentOutOfRangeException(nameof(pageIndex));
            if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize));
        }
    }
}

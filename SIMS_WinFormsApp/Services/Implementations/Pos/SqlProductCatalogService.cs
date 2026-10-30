using System;
using System.Collections.Generic;
using System.Linq;
using SIMS_WinFormsApp.DAL.Linq.Entities.Business;
using System.Drawing;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.Models.DTOs.Pos;
using SIMS_WinFormsApp.Services.Implementations.Catalog;
using SIMS_WinFormsApp.Services.Interfaces.Pos;

namespace SIMS_WinFormsApp.Services.Implementations.Pos
{
    public sealed class SqlProductCatalogService : IProductCatalogService
    {
        private static readonly Color[] TilePalette =
        {
            Color.FromArgb(220, 245, 226),
            Color.FromArgb(253, 226, 233),
            Color.FromArgb(255, 236, 210),
            Color.FromArgb(224, 236, 255),
            Color.FromArgb(240, 226, 255)
        };

        private readonly IDbConnectionFactory _connectionFactory;
        private readonly LocalProductImageStore _images;

        public SqlProductCatalogService(IDbConnectionFactory connectionFactory, LocalProductImageStore images = null)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _images = images ?? new LocalProductImageStore();
        }

        public IReadOnlyList<CategoryOptionDto> GetCategories()
        {
            using (var db = _connectionFactory.CreateDataContext())
                return db.Categories.Where(category => category.Status == "ACTIVE")
                    .OrderBy(category => category.CategoryName)
                    .Select(category => new CategoryOptionDto
                    {
                        CategoryId = category.CategoryID,
                        Name = category.CategoryName
                    }).ToList();
        }

        public IReadOnlyList<ProductCatalogItemDto> Search(string keyword, int? categoryId)
        {
            string normalizedKeyword = (keyword ?? string.Empty).Trim();
            using (var db = _connectionFactory.CreateDataContext())
            {
                var query = from product in db.Products
                            join category in db.Categories on product.CategoryID equals category.CategoryID
                            where product.Status == "ACTIVE" && category.Status == "ACTIVE"
                                && (normalizedKeyword == string.Empty
                                    || product.ProductName.Contains(normalizedKeyword)
                                    || product.ProductCode.Contains(normalizedKeyword))
                            orderby product.ProductName
                            select new
                            {
                                product.ProductID, product.ProductCode, product.ProductName, product.CategoryID,
                                category.CategoryName, product.SellPrice, product.Stock, product.ImageUrl
                            };
                if (categoryId.HasValue)
                    query = query.Where(row => row.CategoryID == categoryId.Value);

                return query.ToList().Select(row => MapProduct(
                    row.ProductID, row.ProductCode, row.ProductName, row.CategoryID, row.CategoryName,
                    row.SellPrice, row.Stock, row.ImageUrl)).ToList();
            }
        }

        public ProductCatalogItemDto GetById(int productId)
        {
            using (var db = _connectionFactory.CreateDataContext())
            {
                var row = (from product in db.Products
                           join category in db.Categories on product.CategoryID equals category.CategoryID
                           where product.ProductID == productId && product.Status == "ACTIVE"
                               && category.Status == "ACTIVE"
                           select new
                           {
                               product.ProductID, product.ProductCode, product.ProductName, product.CategoryID,
                               category.CategoryName, product.SellPrice, product.Stock, product.ImageUrl
                           }).FirstOrDefault();
                return row == null ? null : MapProduct(row.ProductID, row.ProductCode, row.ProductName,
                    row.CategoryID, row.CategoryName, row.SellPrice, row.Stock, row.ImageUrl);
            }
        }

        public ProductCatalogItemDto FindByBarcode(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode)) return null;

            using (var db = _connectionFactory.CreateDataContext())
            {
                var row = (from product in db.Products
                           join category in db.Categories on product.CategoryID equals category.CategoryID
                           where product.ProductCode == barcode.Trim() && product.Status == "ACTIVE"
                               && category.Status == "ACTIVE"
                           select new
                           {
                               product.ProductID, product.ProductCode, product.ProductName, product.CategoryID,
                               category.CategoryName, product.SellPrice, product.Stock, product.ImageUrl
                           }).FirstOrDefault();
                return row == null ? null : MapProduct(row.ProductID, row.ProductCode, row.ProductName,
                    row.CategoryID, row.CategoryName, row.SellPrice, row.Stock, row.ImageUrl);
            }
        }

        private ProductCatalogItemDto MapProduct(
            int productId, string code, string name, int categoryId, string categoryName,
            decimal price, int stock, string imageUrl)
        {
            return new ProductCatalogItemDto
            {
                ProductId = productId,
                Barcode = code,
                Name = name,
                CategoryId = categoryId,
                CategoryName = categoryName,
                Price = price,
                StockQuantity = stock,
                ImagePath = _images.Resolve(imageUrl),
                TileColor = TilePalette[Math.Abs(productId) % TilePalette.Length]
            };
        }
    }
}
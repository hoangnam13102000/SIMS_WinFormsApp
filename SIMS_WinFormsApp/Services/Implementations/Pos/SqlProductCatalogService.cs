using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.Models.DTOs.Pos;
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

        public SqlProductCatalogService(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public IReadOnlyList<CategoryOptionDto> GetCategories()
        {
            const string sql = @"
                SELECT CategoryID, CategoryName
                FROM Categories
                WHERE Status = 'ACTIVE'
                ORDER BY CategoryName";

            var categories = new List<CategoryOptionDto>();
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new CategoryOptionDto
                        {
                            CategoryId = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        });
                    }
                }
            }
            return categories;
        }

        public IReadOnlyList<ProductCatalogItemDto> Search(string keyword, int? categoryId)
        {
            const string sql = @"
                SELECT p.ProductID, p.ProductCode, p.ProductName, p.CategoryID,
                       c.CategoryName, p.SellPrice, p.Stock
                FROM Products p
                INNER JOIN Categories c ON c.CategoryID = p.CategoryID
                WHERE p.Status = 'ACTIVE'
                  AND c.Status = 'ACTIVE'
                  AND (@Keyword = N''
                       OR p.ProductName LIKE @Pattern
                       OR p.ProductCode LIKE @Pattern)
                  AND (@CategoryID IS NULL OR p.CategoryID = @CategoryID)
                ORDER BY p.ProductName";

            var products = new List<ProductCatalogItemDto>();
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                AddSearchParameters(command, keyword, categoryId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read()) products.Add(MapProduct(reader));
                }
            }
            return products;
        }

        public ProductCatalogItemDto GetById(int productId)
        {
            const string sql = @"
                SELECT p.ProductID, p.ProductCode, p.ProductName, p.CategoryID,
                       c.CategoryName, p.SellPrice, p.Stock
                FROM Products p
                INNER JOIN Categories c ON c.CategoryID = p.CategoryID
                WHERE p.ProductID = @ProductID
                  AND p.Status = 'ACTIVE'
                  AND c.Status = 'ACTIVE'";

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productId;
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    return reader.Read() ? MapProduct(reader) : null;
                }
            }
        }

        public ProductCatalogItemDto FindByBarcode(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode)) return null;

            const string sql = @"
                SELECT p.ProductID, p.ProductCode, p.ProductName, p.CategoryID,
                       c.CategoryName, p.SellPrice, p.Stock
                FROM Products p
                INNER JOIN Categories c ON c.CategoryID = p.CategoryID
                WHERE p.ProductCode = @ProductCode
                  AND p.Status = 'ACTIVE'
                  AND c.Status = 'ACTIVE'";

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@ProductCode", SqlDbType.VarChar, 20).Value = barcode.Trim();
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    return reader.Read() ? MapProduct(reader) : null;
                }
            }
        }

        private static void AddSearchParameters(SqlCommand command, string keyword, int? categoryId)
        {
            string normalizedKeyword = (keyword ?? string.Empty).Trim();
            command.Parameters.Add("@Keyword", SqlDbType.NVarChar, 150).Value = normalizedKeyword;
            command.Parameters.Add("@Pattern", SqlDbType.NVarChar, 152).Value = "%" + normalizedKeyword + "%";
            command.Parameters.Add("@CategoryID", SqlDbType.Int).Value =
                categoryId.HasValue ? (object)categoryId.Value : DBNull.Value;
        }

        private static ProductCatalogItemDto MapProduct(IDataRecord record)
        {
            int productId = record.GetInt32(0);
            return new ProductCatalogItemDto
            {
                ProductId = productId,
                Barcode = record.GetString(1),
                Name = record.GetString(2),
                CategoryId = record.GetInt32(3),
                CategoryName = record.GetString(4),
                Price = record.GetDecimal(5),
                StockQuantity = record.GetInt32(6),
                TileColor = TilePalette[Math.Abs(productId) % TilePalette.Length]
            };
        }
    }
}
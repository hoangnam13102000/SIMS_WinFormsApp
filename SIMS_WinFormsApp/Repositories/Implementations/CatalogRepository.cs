using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            const string sql = @"
                SELECT ProductID, ProductCode, ProductName, CategoryName, SellPrice, Stock, Status, ImageUrl
                FROM (
                    SELECT p.ProductID, p.ProductCode, p.ProductName, c.CategoryName, p.SellPrice, p.Stock, p.Status, p.ImageUrl,
                           ROW_NUMBER() OVER (ORDER BY p.ProductName) AS rn
                    FROM Products p
                    INNER JOIN Categories c ON c.CategoryID = p.CategoryID
                    WHERE (@Keyword = N'' OR p.ProductName LIKE @Pattern OR p.ProductCode LIKE @Pattern OR c.CategoryName LIKE @Pattern)
                      AND (@Status IS NULL OR p.Status = @Status)
                ) x
                WHERE rn BETWEEN @Start AND @End;

                SELECT COUNT(*)
                FROM Products p
                INNER JOIN Categories c ON c.CategoryID = p.CategoryID
                WHERE (@Keyword = N'' OR p.ProductName LIKE @Pattern OR p.ProductCode LIKE @Pattern OR c.CategoryName LIKE @Pattern)
                  AND (@Status IS NULL OR p.Status = @Status);";

            var page = new CatalogPage<ProductRowDto> { Rows = new List<ProductRowDto>() };
            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                AddFilter(command, keyword, status, pageIndex, pageSize);
                using (var reader = command.ExecuteReader())
                {
                    var rows = (List<ProductRowDto>)page.Rows;
                    while (reader.Read())
                    {
                        rows.Add(new ProductRowDto
                        {
                            ProductId = reader.GetInt32(0),
                            Code = ReadString(reader, 1),
                            Name = ReadString(reader, 2),
                            CategoryName = ReadString(reader, 3),
                            SellPrice = ReadDecimal(reader, 4),
                            Stock = ReadInt(reader, 5),
                            IsActive = string.Equals(ReadString(reader, 6), "ACTIVE", StringComparison.OrdinalIgnoreCase),
                            ImagePath = ReadString(reader, 7)
                        });
                    }
                    if (reader.NextResult() && reader.Read())
                        page.TotalCount = reader.GetInt32(0);
                }
            }
            return page;
        }

        public ProductEditDto GetProduct(int productId)
        {
            const string sql = @"
                SELECT p.ProductID, p.ProductCode, p.ProductName, p.CategoryID, p.Brand, p.Unit, p.WeightVolume,
                       p.Description, p.ImportPrice, p.SellPrice, p.Stock, p.MinStock, p.ImageUrl, p.Status,
                       (SELECT TOP 1 sp.SupplierID FROM SupplierProducts sp
                        WHERE sp.ProductID = p.ProductID AND sp.IsPreferred = 1) AS SupplierID
                FROM Products p
                WHERE p.ProductID = @ProductID";

            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productId;
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read()) return null;
                    return new ProductEditDto
                    {
                        ProductId = reader.GetInt32(0),
                        Code = ReadString(reader, 1),
                        Name = ReadString(reader, 2),
                        CategoryId = ReadInt(reader, 3),
                        Brand = ReadString(reader, 4),
                        Unit = ReadString(reader, 5),
                        WeightVolume = ReadString(reader, 6),
                        Description = ReadString(reader, 7),
                        ImportPrice = ReadDecimal(reader, 8),
                        SellPrice = ReadDecimal(reader, 9),
                        Stock = ReadInt(reader, 10),
                        MinStock = ReadInt(reader, 11),
                        ImagePath = ReadString(reader, 12),
                        IsActive = string.Equals(ReadString(reader, 13), "ACTIVE", StringComparison.OrdinalIgnoreCase),
                        SupplierId = reader.IsDBNull(14) ? (int?)null : reader.GetInt32(14)
                    };
                }
            }
        }

        public int InsertProduct(ProductEditDto dto)
        {
            const string sql = @"
                INSERT INTO Products
                    (ProductName, CategoryID, Brand, Unit, WeightVolume, Description,
                     ImportPrice, SellPrice, Margin, AutoPrice, ImageUrl, Stock, MinStock, Status, UpdatedAt)
                VALUES
                    (@Name, @CategoryID, @Brand, @Unit, @WeightVolume, @Description,
                     @ImportPrice, @SellPrice, @Margin, 0, @ImageUrl, @Stock, @MinStock, @Status, GETDATE());
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                BindProduct(command, dto, includeStock: true);
                return (int)command.ExecuteScalar();
            }
        }

        public void UpdateProduct(ProductEditDto dto)
        {
            const string sql = @"
                UPDATE Products SET
                    ProductName = @Name,
                    CategoryID = @CategoryID,
                    Brand = @Brand,
                    Unit = @Unit,
                    WeightVolume = @WeightVolume,
                    Description = @Description,
                    ImportPrice = @ImportPrice,
                    SellPrice = @SellPrice,
                    Margin = @Margin,
                    AutoPrice = 0,
                    ImageUrl = @ImageUrl,
                    MinStock = @MinStock,
                    Status = @Status,
                    UpdatedAt = GETDATE()
                WHERE ProductID = @ProductID";

            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                BindProduct(command, dto, includeStock: false);
                command.Parameters.Add("@ProductID", SqlDbType.Int).Value = dto.ProductId;
                command.ExecuteNonQuery();
            }
        }

        public void SetProductStatus(int productId, bool active)
        {
            const string sql = "UPDATE Products SET Status = @Status, UpdatedAt = GETDATE() WHERE ProductID = @ProductID";
            ExecuteStatus(sql, "@ProductID", productId, active);
        }

        public void SetPreferredSupplier(int productId, int? supplierId)
        {
            const string sql = @"
                UPDATE SupplierProducts SET IsPreferred = 0 WHERE ProductID = @ProductID;
                IF @SupplierID IS NOT NULL
                BEGIN
                    IF EXISTS (SELECT 1 FROM SupplierProducts WHERE ProductID = @ProductID AND SupplierID = @SupplierID)
                        UPDATE SupplierProducts SET IsPreferred = 1 WHERE ProductID = @ProductID AND SupplierID = @SupplierID;
                    ELSE
                        INSERT INTO SupplierProducts (SupplierID, ProductID, SupplyPrice, IsPreferred)
                        VALUES (@SupplierID, @ProductID, NULL, 1);
                END";

            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productId;
                command.Parameters.Add("@SupplierID", SqlDbType.Int).Value = (object)supplierId ?? DBNull.Value;
                command.ExecuteNonQuery();
            }
        }

        public int? FindProductIdByName(string name)
        {
            return FindId("SELECT TOP 1 ProductID FROM Products WHERE ProductName = @Name", name);
        }

        public CatalogPage<CategoryRowDto> GetCategories(int pageIndex, int pageSize, string keyword, string status)
        {
            const string sql = @"
                SELECT CategoryID, CategoryName, ProductCount, Status
                FROM (
                    SELECT c.CategoryID, c.CategoryName, c.Status,
                           (SELECT COUNT(*) FROM Products p WHERE p.CategoryID = c.CategoryID) AS ProductCount,
                           ROW_NUMBER() OVER (ORDER BY c.CategoryName) AS rn
                    FROM Categories c
                    WHERE (@Keyword = N'' OR c.CategoryName LIKE @Pattern)
                      AND (@Status IS NULL OR c.Status = @Status)
                ) x
                WHERE rn BETWEEN @Start AND @End;

                SELECT COUNT(*) FROM Categories c
                WHERE (@Keyword = N'' OR c.CategoryName LIKE @Pattern)
                  AND (@Status IS NULL OR c.Status = @Status);";

            var page = new CatalogPage<CategoryRowDto> { Rows = new List<CategoryRowDto>() };
            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                AddFilter(command, keyword, status, pageIndex, pageSize);
                using (var reader = command.ExecuteReader())
                {
                    var rows = (List<CategoryRowDto>)page.Rows;
                    while (reader.Read())
                    {
                        rows.Add(new CategoryRowDto
                        {
                            CategoryId = reader.GetInt32(0),
                            Name = ReadString(reader, 1),
                            ProductCount = ReadInt(reader, 2),
                            IsActive = string.Equals(ReadString(reader, 3), "ACTIVE", StringComparison.OrdinalIgnoreCase)
                        });
                    }
                    if (reader.NextResult() && reader.Read())
                        page.TotalCount = reader.GetInt32(0);
                }
            }
            return page;
        }

        public IReadOnlyList<LookupItem> GetActiveCategories()
        {
            return ReadLookup("SELECT CategoryID, CategoryName FROM Categories WHERE Status = 'ACTIVE' ORDER BY CategoryName");
        }

        public CategoryEditDto GetCategory(int categoryId)
        {
            const string sql = "SELECT CategoryID, CategoryName, Status FROM Categories WHERE CategoryID = @Id";
            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = categoryId;
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read()) return null;
                    return new CategoryEditDto
                    {
                        CategoryId = reader.GetInt32(0),
                        Name = ReadString(reader, 1),
                        IsActive = string.Equals(ReadString(reader, 2), "ACTIVE", StringComparison.OrdinalIgnoreCase)
                    };
                }
            }
        }

        public int InsertCategory(CategoryEditDto dto)
        {
            const string sql = @"
                INSERT INTO Categories (CategoryName, Status) VALUES (@Name, @Status);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";
            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = dto.Name.Trim();
                command.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = dto.IsActive ? "ACTIVE" : "DISABLED";
                return (int)command.ExecuteScalar();
            }
        }

        public void UpdateCategory(CategoryEditDto dto)
        {
            const string sql = "UPDATE Categories SET CategoryName = @Name, Status = @Status WHERE CategoryID = @Id";
            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = dto.Name.Trim();
                command.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = dto.IsActive ? "ACTIVE" : "DISABLED";
                command.Parameters.Add("@Id", SqlDbType.Int).Value = dto.CategoryId;
                command.ExecuteNonQuery();
            }
        }

        public void SetCategoryStatus(int categoryId, bool active)
        {
            ExecuteStatus("UPDATE Categories SET Status = @Status WHERE CategoryID = @Id", "@Id", categoryId, active);
        }

        public int? FindCategoryIdByName(string name)
        {
            return FindId("SELECT TOP 1 CategoryID FROM Categories WHERE CategoryName = @Name", name);
        }

        public CatalogPage<SupplierRowDto> GetSuppliers(int pageIndex, int pageSize, string keyword, bool debtOnly)
        {
            const string sql = @"
                SELECT SupplierID, SupplierName, Phone, Email, Address, SuppliedItems, DebtBalance
                FROM (
                    SELECT SupplierID, SupplierName, Phone, Email, Address, SuppliedItems, DebtBalance,
                           ROW_NUMBER() OVER (ORDER BY SupplierName) AS rn
                    FROM Suppliers
                    WHERE IsDeleted = 0
                      AND (@Keyword = N'' OR SupplierName LIKE @Pattern OR Phone LIKE @Pattern OR Email LIKE @Pattern)
                      AND (@DebtOnly = 0 OR DebtBalance > 0)
                ) x
                WHERE rn BETWEEN @Start AND @End;

                SELECT COUNT(*) FROM Suppliers
                WHERE IsDeleted = 0
                  AND (@Keyword = N'' OR SupplierName LIKE @Pattern OR Phone LIKE @Pattern OR Email LIKE @Pattern)
                  AND (@DebtOnly = 0 OR DebtBalance > 0);";

            var page = new CatalogPage<SupplierRowDto> { Rows = new List<SupplierRowDto>() };
            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                AddKeyword(command, keyword);
                command.Parameters.Add("@DebtOnly", SqlDbType.Bit).Value = debtOnly;
                command.Parameters.Add("@Start", SqlDbType.Int).Value = pageIndex * pageSize + 1;
                command.Parameters.Add("@End", SqlDbType.Int).Value = pageIndex * pageSize + pageSize;
                using (var reader = command.ExecuteReader())
                {
                    var rows = (List<SupplierRowDto>)page.Rows;
                    while (reader.Read())
                    {
                        rows.Add(new SupplierRowDto
                        {
                            SupplierId = reader.GetInt32(0),
                            Name = ReadString(reader, 1),
                            Phone = ReadString(reader, 2),
                            Email = ReadString(reader, 3),
                            Address = ReadString(reader, 4),
                            SuppliedItems = ReadString(reader, 5),
                            DebtBalance = ReadDecimal(reader, 6)
                        });
                    }
                    if (reader.NextResult() && reader.Read())
                        page.TotalCount = reader.GetInt32(0);
                }
            }
            return page;
        }

        public IReadOnlyList<LookupItem> GetActiveSuppliers()
        {
            return ReadLookup("SELECT SupplierID, SupplierName FROM Suppliers WHERE IsDeleted = 0 ORDER BY SupplierName");
        }

        public SupplierEditDto GetSupplier(int supplierId)
        {
            const string sql = @"
                SELECT SupplierID, SupplierName, Phone, Email, Address, SuppliedItems, DebtBalance
                FROM Suppliers WHERE SupplierID = @Id AND IsDeleted = 0";
            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = supplierId;
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read()) return null;
                    return new SupplierEditDto
                    {
                        SupplierId = reader.GetInt32(0),
                        Name = ReadString(reader, 1),
                        Phone = ReadString(reader, 2),
                        Email = ReadString(reader, 3),
                        Address = ReadString(reader, 4),
                        SuppliedItems = ReadString(reader, 5),
                        DebtBalance = ReadDecimal(reader, 6)
                    };
                }
            }
        }

        public int InsertSupplier(SupplierEditDto dto)
        {
            const string sql = @"
                INSERT INTO Suppliers (SupplierName, Address, Phone, Email, SuppliedItems)
                VALUES (@Name, @Address, @Phone, @Email, @Items);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";
            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                BindSupplier(command, dto);
                return (int)command.ExecuteScalar();
            }
        }

        public void UpdateSupplier(SupplierEditDto dto)
        {
            const string sql = @"
                UPDATE Suppliers SET
                    SupplierName = @Name, Address = @Address, Phone = @Phone, Email = @Email, SuppliedItems = @Items
                WHERE SupplierID = @Id AND IsDeleted = 0";
            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                BindSupplier(command, dto);
                command.Parameters.Add("@Id", SqlDbType.Int).Value = dto.SupplierId;
                command.ExecuteNonQuery();
            }
        }

        public void SoftDeleteSupplier(int supplierId)
        {
            const string sql = "UPDATE Suppliers SET IsDeleted = 1, DeletedAt = GETDATE() WHERE SupplierID = @Id";
            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = supplierId;
                command.ExecuteNonQuery();
            }
        }

        public int? FindSupplierIdByName(string name)
        {
            return FindId("SELECT TOP 1 SupplierID FROM Suppliers WHERE SupplierName = @Name AND IsDeleted = 0", name);
        }

        private SqlConnection Open()
        {
            var connection = _connections.CreateConnection();
            connection.Open();
            return connection;
        }

        private IReadOnlyList<LookupItem> ReadLookup(string sql)
        {
            var items = new List<LookupItem>();
            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                    items.Add(new LookupItem { Id = reader.GetInt32(0), Name = ReadString(reader, 1) });
            }
            return items;
        }

        private int? FindId(string sql, string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;
            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Name", SqlDbType.NVarChar, 150).Value = name.Trim();
                object value = command.ExecuteScalar();
                return value == null || value == DBNull.Value ? (int?)null : Convert.ToInt32(value);
            }
        }

        private void ExecuteStatus(string sql, string idParameter, int id, bool active)
        {
            using (var connection = Open())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = active ? "ACTIVE" : "DISABLED";
                command.Parameters.Add(idParameter, SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        private static void AddFilter(SqlCommand command, string keyword, string status, int pageIndex, int pageSize)
        {
            AddKeyword(command, keyword);
            command.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = (object)status ?? DBNull.Value;
            command.Parameters.Add("@Start", SqlDbType.Int).Value = pageIndex * pageSize + 1;
            command.Parameters.Add("@End", SqlDbType.Int).Value = pageIndex * pageSize + pageSize;
        }

        private static void AddKeyword(SqlCommand command, string keyword)
        {
            string normalized = (keyword ?? string.Empty).Trim();
            command.Parameters.Add("@Keyword", SqlDbType.NVarChar, 150).Value = normalized;
            command.Parameters.Add("@Pattern", SqlDbType.NVarChar, 152).Value = "%" + normalized + "%";
        }

        private static void BindProduct(SqlCommand command, ProductEditDto dto, bool includeStock)
        {
            command.Parameters.Add("@Name", SqlDbType.NVarChar, 150).Value = dto.Name.Trim();
            command.Parameters.Add("@CategoryID", SqlDbType.Int).Value = dto.CategoryId;
            command.Parameters.Add("@Brand", SqlDbType.NVarChar, 100).Value = Db(dto.Brand);
            command.Parameters.Add("@Unit", SqlDbType.NVarChar, 30).Value = Db(dto.Unit);
            command.Parameters.Add("@WeightVolume", SqlDbType.NVarChar, 50).Value = Db(dto.WeightVolume);
            command.Parameters.Add("@Description", SqlDbType.NVarChar, 1000).Value = Db(dto.Description);
            command.Parameters.Add("@ImportPrice", SqlDbType.Decimal).Value = dto.ImportPrice;
            command.Parameters.Add("@SellPrice", SqlDbType.Decimal).Value = dto.SellPrice;
            command.Parameters.Add("@Margin", SqlDbType.Decimal).Value = dto.SellPrice - dto.ImportPrice;
            command.Parameters.Add("@ImageUrl", SqlDbType.NVarChar, 500).Value = Db(dto.ImagePath);
            command.Parameters.Add("@MinStock", SqlDbType.Int).Value = dto.MinStock;
            command.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = dto.IsActive ? "ACTIVE" : "DISABLED";
            if (includeStock)
                command.Parameters.Add("@Stock", SqlDbType.Int).Value = dto.Stock;
        }

        private static void BindSupplier(SqlCommand command, SupplierEditDto dto)
        {
            command.Parameters.Add("@Name", SqlDbType.NVarChar, 150).Value = dto.Name.Trim();
            command.Parameters.Add("@Address", SqlDbType.NVarChar, 255).Value = Db(dto.Address);
            command.Parameters.Add("@Phone", SqlDbType.VarChar, 20).Value = Db(dto.Phone);
            command.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = Db(dto.Email);
            command.Parameters.Add("@Items", SqlDbType.NVarChar, 255).Value = Db(dto.SuppliedItems);
        }

        private static object Db(string value) =>
            string.IsNullOrWhiteSpace(value) ? (object)DBNull.Value : value.Trim();

        private static string ReadString(IDataRecord reader, int index) =>
            reader.IsDBNull(index) ? null : reader.GetString(index);

        private static int ReadInt(IDataRecord reader, int index) =>
            reader.IsDBNull(index) ? 0 : Convert.ToInt32(reader.GetValue(index));

        private static decimal ReadDecimal(IDataRecord reader, int index) =>
            reader.IsDBNull(index) ? 0m : Convert.ToDecimal(reader.GetValue(index));
    }
}

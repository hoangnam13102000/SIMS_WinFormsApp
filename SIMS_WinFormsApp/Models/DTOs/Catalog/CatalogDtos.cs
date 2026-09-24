using System;
using System.Collections.Generic;

namespace SIMS_WinFormsApp.Models.DTOs.Catalog
{
    public sealed class LookupItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString() => Name ?? string.Empty;
    }

    public sealed class CatalogPage<T>
    {
        public int TotalCount { get; set; }
        public IReadOnlyList<T> Rows { get; set; } = Array.Empty<T>();
    }

    public sealed class CatalogSaveResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int Id { get; set; }

        public static CatalogSaveResult Ok(int id, string message) =>
            new CatalogSaveResult { Success = true, Id = id, Message = message };

        public static CatalogSaveResult Fail(string message) =>
            new CatalogSaveResult { Success = false, Message = message };
    }

    public sealed class ProductRowDto
    {
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public decimal SellPrice { get; set; }
        public int Stock { get; set; }
        public bool IsActive { get; set; }
        public string ImagePath { get; set; }
    }

    public sealed class ProductEditDto
    {
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int? SupplierId { get; set; }
        public string Brand { get; set; }
        public string Unit { get; set; }
        public string WeightVolume { get; set; }
        public string Description { get; set; }
        public decimal ImportPrice { get; set; }
        public decimal SellPrice { get; set; }
        public int Stock { get; set; }
        public int MinStock { get; set; } = 5;
        public string ImagePath { get; set; }
        public bool IsActive { get; set; } = true;

        public ProductEditDto Copy()
        {
            return new ProductEditDto
            {
                ProductId = ProductId,
                Code = Code,
                Name = Name,
                CategoryId = CategoryId,
                SupplierId = SupplierId,
                Brand = Brand,
                Unit = Unit,
                WeightVolume = WeightVolume,
                Description = Description,
                ImportPrice = ImportPrice,
                SellPrice = SellPrice,
                Stock = Stock,
                MinStock = MinStock,
                ImagePath = ImagePath,
                IsActive = IsActive
            };
        }
    }

    public sealed class CategoryRowDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int ProductCount { get; set; }
        public bool IsActive { get; set; }
    }

    public sealed class CategoryEditDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;

        public CategoryEditDto Copy() => new CategoryEditDto
        {
            CategoryId = CategoryId,
            Name = Name,
            IsActive = IsActive
        };
    }

    public sealed class SupplierRowDto
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string SuppliedItems { get; set; }
        public decimal DebtBalance { get; set; }
    }

    public sealed class SupplierEditDto
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string SuppliedItems { get; set; }
        public decimal DebtBalance { get; set; }

        public SupplierEditDto Copy() => new SupplierEditDto
        {
            SupplierId = SupplierId,
            Name = Name,
            Phone = Phone,
            Email = Email,
            Address = Address,
            SuppliedItems = SuppliedItems,
            DebtBalance = DebtBalance
        };
    }
}

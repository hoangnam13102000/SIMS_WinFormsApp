using System;
using System.Data.Linq.Mapping;

namespace SIMS_WinFormsApp.DAL.Linq.Entities.Business
{
    [Table(Name = "Categories")]
    public class CategoryEntity
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)] public int CategoryID { get; set; }
        [Column] public string CategoryName { get; set; }
        [Column] public string Status { get; set; }
    }

    [Table(Name = "Products")]
    public class ProductEntity
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)] public int ProductID { get; set; }
        [Column(IsDbGenerated = true, CanBeNull = false)] public string ProductCode { get; set; }
        [Column] public string ProductName { get; set; }
        [Column] public int CategoryID { get; set; }
        [Column] public string Brand { get; set; }
        [Column] public string Unit { get; set; }
        [Column] public string WeightVolume { get; set; }
        [Column] public string Description { get; set; }
        [Column] public decimal ImportPrice { get; set; }
        [Column] public decimal SellPrice { get; set; }
        [Column] public decimal? Margin { get; set; }
        [Column] public bool AutoPrice { get; set; }
        [Column] public string ImageUrl { get; set; }
        [Column] public int Stock { get; set; }
        [Column] public int MinStock { get; set; }
        [Column] public string Status { get; set; }
        [Column(IsDbGenerated = true)] public DateTime CreatedAt { get; set; }
        [Column] public DateTime? UpdatedAt { get; set; }
    }

    [Table(Name = "Customers")]
    public class CustomerEntity
    {
        [Column(IsPrimaryKey = true)] public int CustomerID { get; set; }
        [Column] public string CustomerCode { get; set; }
        [Column] public int MemberPoint { get; set; }
        [Column] public DateTime CreatedAt { get; set; }
    }

    [Table(Name = "InventoryBatch")]
    public class InventoryBatchEntity
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)] public int BatchID { get; set; }
        [Column] public int ProductID { get; set; }
        [Column] public DateTime? ExpiryDate { get; set; }
        [Column] public int RemainingQty { get; set; }
        [Column] public string Status { get; set; }
    }

    [Table(Name = "Suppliers")]
    public class SupplierEntity
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)] public int SupplierID { get; set; }
        [Column] public string SupplierName { get; set; }
        [Column] public string Address { get; set; }
        [Column] public string Phone { get; set; }
        [Column] public string Email { get; set; }
        [Column] public string SuppliedItems { get; set; }
        [Column] public bool IsDeleted { get; set; }
        [Column] public DateTime? DeletedAt { get; set; }
    }

    [Table(Name = "SupplierProducts")]
    public class SupplierProductEntity
    {
        [Column(IsPrimaryKey = true)] public int SupplierID { get; set; }
        [Column(IsPrimaryKey = true)] public int ProductID { get; set; }
        [Column] public decimal? SupplyPrice { get; set; }
        [Column] public bool IsPreferred { get; set; }
        [Column(IsDbGenerated = true)] public DateTime CreatedAt { get; set; }
    }
}

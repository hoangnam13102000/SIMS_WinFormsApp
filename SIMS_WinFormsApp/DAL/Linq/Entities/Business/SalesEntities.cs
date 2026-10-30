using System;
using System.Data.Linq.Mapping;

namespace SIMS_WinFormsApp.DAL.Linq.Entities.Business
{
    [Table(Name = "Invoices")]
    public class InvoiceEntity
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)] public int InvoiceID { get; set; }
        [Column] public string InvoiceCode { get; set; }
        [Column] public int CreatedBy { get; set; }
        [Column] public int? CustomerID { get; set; }
        [Column(IsDbGenerated = true)] public DateTime CreatedAt { get; set; }
        [Column] public decimal SubTotal { get; set; }
        [Column] public decimal DiscountAmount { get; set; }
        [Column] public int? PromotionID { get; set; }
        [Column] public string PromotionCode { get; set; }
        [Column] public decimal VATRate { get; set; }
        [Column(IsDbGenerated = true)] public decimal VATAmount { get; set; }
        [Column] public decimal TotalAmount { get; set; }
        [Column] public string PaymentMethod { get; set; }
        [Column] public string Status { get; set; }
        [Column] public string CancelReason { get; set; }
        [Column] public DateTime? CancelledAt { get; set; }
        [Column] public int PointsUsed { get; set; }
        [Column] public decimal PointsDiscountAmount { get; set; }
    }

    [Table(Name = "InvoiceDetails")]
    public class InvoiceDetailEntity
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)] public int InvoiceDetailID { get; set; }
        [Column] public int InvoiceID { get; set; }
        [Column] public int ProductID { get; set; }
        [Column] public int Quantity { get; set; }
        [Column] public decimal UnitPrice { get; set; }
        [Column(IsDbGenerated = true)] public decimal LineTotal { get; set; }
    }

    [Table(Name = "ReturnExchanges")]
    public class ReturnExchangeEntity
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)] public int ReturnID { get; set; }
        [Column] public int InvoiceID { get; set; }
        [Column] public string Type { get; set; }
        [Column] public string Reason { get; set; }
        [Column] public string RejectionReason { get; set; }
        [Column] public decimal TotalValue { get; set; }
        [Column] public decimal DiscountShare { get; set; }
        [Column] public decimal PointsShare { get; set; }
        [Column] public bool RequiresApproval { get; set; }
        [Column] public string Status { get; set; }
        [Column] public int? ApprovedBy { get; set; }
        [Column] public DateTime? ApprovedAt { get; set; }
        [Column] public int CreatedBy { get; set; }
        [Column(IsDbGenerated = true)] public DateTime CreatedAt { get; set; }
    }

    [Table(Name = "ReturnExchangeDetails")]
    public class ReturnExchangeDetailEntity
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)] public int ReturnDetailID { get; set; }
        [Column] public int ReturnID { get; set; }
        [Column] public int ProductID { get; set; }
        [Column] public int Quantity { get; set; }
        [Column] public string Direction { get; set; }
        [Column] public decimal UnitPrice { get; set; }
    }

    [Table(Name = "Promotions")]
    public class PromotionEntity
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)] public int PromotionID { get; set; }
        [Column] public string Code { get; set; }
        [Column] public string Name { get; set; }
        [Column] public string DiscountType { get; set; }
        [Column] public decimal DiscountValue { get; set; }
        [Column] public decimal? MaxDiscountAmount { get; set; }
        [Column] public decimal MinOrderAmount { get; set; }
        [Column] public DateTime StartDate { get; set; }
        [Column] public DateTime EndDate { get; set; }
        [Column] public int? UsageLimit { get; set; }
        [Column] public int UsedCount { get; set; }
        [Column] public bool IsActive { get; set; }
        [Column] public bool IsDeleted { get; set; }
        [Column] public DateTime? DeletedAt { get; set; }
        [Column] public int? CreatedBy { get; set; }
        [Column(IsDbGenerated = true)] public DateTime CreatedAt { get; set; }
        [Column] public bool ShowOnBanner { get; set; }
        [Column] public int? BannerSortOrder { get; set; }
    }
}

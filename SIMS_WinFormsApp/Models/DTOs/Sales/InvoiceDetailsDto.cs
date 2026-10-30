using System;
using System.Collections.Generic;

namespace SIMS_WinFormsApp.Models.DTOs.Sales
{
    internal sealed class InvoiceDetails
    {
        public int InvoiceId { get; set; }
        public string InvoiceCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CashierName { get; set; }
        public string CustomerName { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public string PromotionCode { get; set; }
        public decimal VatRate { get; set; }
        public decimal VatAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsCancelled { get; set; }
        public string CancelReason { get; set; }
        public DateTime? CancelledAt { get; set; }
        public int PointsUsed { get; set; }
        public decimal PointsDiscountAmount { get; set; }
        public List<InvoiceLine> Lines { get; } = new List<InvoiceLine>();
    }

    internal sealed class InvoiceLine
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }

    internal sealed class InvoiceManagementRow
    {
        public int InvoiceId { get; set; }
        public string InvoiceCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CustomerName { get; set; }
        public string CashierName { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
    }

    internal sealed class NotificationDto
    {
        public int NotificationId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
    }

}

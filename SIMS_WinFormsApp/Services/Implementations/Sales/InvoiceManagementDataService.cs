using System;
using System.Collections.Generic;
using System.Linq;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.Models.DTOs.Sales;

namespace SIMS_WinFormsApp.Services.Implementations.Sales
{
    internal sealed class InvoiceManagementDataService
    {
        private readonly IDbConnectionFactory _connections;

        public InvoiceManagementDataService(IDbConnectionFactory connections)
        {
            _connections = connections ?? throw new ArgumentNullException(nameof(connections));
        }

        public IReadOnlyList<InvoiceManagementRow> GetInvoices(
            DateTime fromDate, DateTime toDate, bool canViewAll, int currentUserId,
            string status, string keyword)
        {
            DateTime startDate = fromDate.Date;
            DateTime dayAfterTo = toDate.Date.AddDays(1);
            string search = (keyword ?? string.Empty).Trim();
            using (var db = _connections.CreateDataContext())
            {
                return (from invoice in db.Invoices
                        join cashier in db.Users on invoice.CreatedBy equals cashier.UserID
                        join customerUser in db.Users on invoice.CustomerID equals (int?)customerUser.UserID into customers
                        from customer in customers.DefaultIfEmpty()
                        where invoice.CreatedAt >= startDate && invoice.CreatedAt < dayAfterTo
                            && (canViewAll || invoice.CreatedBy == currentUserId)
                            && (status == "ALL" || invoice.Status == status)
                            && (search == string.Empty || invoice.InvoiceCode.Contains(search)
                                || (customer != null && customer.FullName.Contains(search))
                                || cashier.FullName.Contains(search))
                        orderby invoice.CreatedAt descending, invoice.InvoiceID descending
                        select new InvoiceManagementRow
                        {
                            InvoiceId = invoice.InvoiceID,
                            InvoiceCode = invoice.InvoiceCode,
                            CreatedAt = invoice.CreatedAt,
                            CustomerName = customer == null ? "Khách lẻ" : customer.FullName,
                            CashierName = cashier.FullName,
                            TotalAmount = invoice.TotalAmount,
                            Status = invoice.Status
                        }).ToList();
            }
        }

        public InvoiceDetails GetInvoiceDetails(int invoiceId, bool canViewAll, int currentUserId)
        {
            using (var db = _connections.CreateDataContext())
            {
                var header = (from invoice in db.Invoices
                              join cashier in db.Users on invoice.CreatedBy equals cashier.UserID
                              join customerUser in db.Users on invoice.CustomerID equals (int?)customerUser.UserID into customers
                              from customer in customers.DefaultIfEmpty()
                              where invoice.InvoiceID == invoiceId
                                  && (canViewAll || invoice.CreatedBy == currentUserId)
                              select new
                              {
                                  invoice.InvoiceID, invoice.InvoiceCode, invoice.CreatedAt,
                                  CashierName = cashier.FullName,
                                  CustomerName = customer == null ? "Khách lẻ" : customer.FullName,
                                  invoice.PaymentMethod, invoice.SubTotal, invoice.DiscountAmount,
                                  invoice.PromotionCode, invoice.VATRate, invoice.VATAmount,
                                  invoice.TotalAmount, invoice.Status, invoice.CancelReason, invoice.CancelledAt,
                                  invoice.PointsUsed, invoice.PointsDiscountAmount
                              }).FirstOrDefault();
                if (header == null) return null;
                var result = new InvoiceDetails
                {
                    InvoiceId = header.InvoiceID,
                    InvoiceCode = header.InvoiceCode,
                    CreatedAt = header.CreatedAt,
                    CashierName = header.CashierName,
                    CustomerName = header.CustomerName,
                    PaymentMethod = header.PaymentMethod,
                    Subtotal = header.SubTotal,
                    DiscountAmount = header.DiscountAmount,
                    PromotionCode = header.PromotionCode ?? string.Empty,
                    VatRate = header.VATRate,
                    VatAmount = header.VATAmount,
                    TotalAmount = header.TotalAmount,
                    IsCancelled = header.Status == "CANCELLED",
                    CancelReason = header.CancelReason ?? string.Empty,
                    CancelledAt = header.CancelledAt,
                    PointsUsed = header.PointsUsed,
                    PointsDiscountAmount = header.PointsDiscountAmount
                };
                var lines = (from detail in db.InvoiceDetails
                             join product in db.Products on detail.ProductID equals product.ProductID
                             where detail.InvoiceID == invoiceId
                             orderby detail.InvoiceDetailID
                             select new
                             {
                                 detail.ProductID, product.ProductName, detail.Quantity,
                                 detail.UnitPrice, detail.LineTotal
                             }).ToList();
                foreach (var line in lines)
                    result.Lines.Add(new InvoiceLine
                    {
                        ProductId = line.ProductID,
                        ProductName = line.ProductName,
                        Quantity = line.Quantity,
                        UnitPrice = line.UnitPrice,
                        LineTotal = line.LineTotal
                    });
                return result;
            }
        }
    }
}

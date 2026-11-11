using System;
using System.Linq;
using Newtonsoft.Json;
using SIMS_WinFormsApp.DAL.Linq.Entities;
using SIMS_WinFormsApp.Services.Session;

namespace SIMS_WinFormsApp.DAL.Linq
{
    /// <summary>
    /// Insert vào các bảng có trigger INSTEAD OF INSERT (InvoiceDetails, StockReconciliation).
    /// Dùng ExecuteCommand / ExecuteQuery của LINQ to SQL (SimsDataContext) để trigger chạy đúng,
    /// không dùng ADO.NET.
    /// </summary>
    public static class TriggerInsertExecutor
    {
        public static void InsertInvoiceDetail(
            SimsDataContext db,
            int invoiceId,
            int productId,
            int quantity,
            decimal unitPrice)
        {
            EnsureTransaction(db);

            int invoiceDetailId = db.ExecuteQuery<int>(
                "SET NOCOUNT ON; " +
                "INSERT INTO InvoiceDetails (InvoiceID, ProductID, Quantity, UnitPrice) " +
                "VALUES ({0}, {1}, {2}, {3}); " +
                "SELECT TOP (1) InvoiceDetailID FROM InvoiceDetails " +
                "WHERE InvoiceID = {0} AND ProductID = {1} " +
                "AND Quantity = {2} AND UnitPrice = {3} " +
                "ORDER BY InvoiceDetailID DESC;",
                invoiceId, productId, quantity, unitPrice).FirstOrDefault();

            if (invoiceDetailId <= 0)
                throw new InvalidOperationException(
                    "Không thể xác định mã chi tiết hóa đơn vừa được tạo.");

            if (UserSession.Instance.CurrentUser == null)
                return;

            decimal lineTotal = quantity * unitPrice;
            db.AuditLogs.InsertOnSubmit(new AuditLogEntity
            {
                UserID = UserSession.Instance.CurrentUser.UserId,
                Action = "DATA_CREATE",
                TableName = "InvoiceDetails",
                RecordID = invoiceDetailId,
                NewValue = JsonConvert.SerializeObject(new
                {
                    InvoiceDetailID = invoiceDetailId,
                    InvoiceID = invoiceId,
                    ProductID = productId,
                    Quantity = quantity,
                    UnitPrice = unitPrice,
                    LineTotal = lineTotal
                }),
                Detail = "Đã thêm dữ liệu trong InvoiceDetails " +
                    "(InvoiceDetailID=" + invoiceDetailId + ")",
                CreatedAt = DateTime.Now
            });
        }

        public static void InsertStockReconciliation(
            SimsDataContext db,
            int productId,
            int systemStock,
            int actualStock,
            string note,
            int createdBy,
            DateTime createdAt)
        {
            EnsureTransaction(db);

            if (note == null)
            {
                db.ExecuteCommand(
                    "INSERT INTO StockReconciliation " +
                    "(ProductID, SystemStock, ActualStock, Note, CreatedBy, CreatedAt) " +
                    "VALUES ({0}, {1}, {2}, NULL, {3}, {4});",
                    productId, systemStock, actualStock, createdBy, createdAt);
            }
            else
            {
                db.ExecuteCommand(
                    "INSERT INTO StockReconciliation " +
                    "(ProductID, SystemStock, ActualStock, Note, CreatedBy, CreatedAt) " +
                    "VALUES ({0}, {1}, {2}, {3}, {4}, {5});",
                    productId, systemStock, actualStock, note, createdBy, createdAt);
            }
        }

        private static void EnsureTransaction(SimsDataContext db)
        {
            if (db == null)
                throw new ArgumentNullException(nameof(db));
            if (db.Transaction == null)
                throw new InvalidOperationException(
                    "Thao tác này cần chạy trong transaction đang mở của SimsDataContext.");
        }
    }
}
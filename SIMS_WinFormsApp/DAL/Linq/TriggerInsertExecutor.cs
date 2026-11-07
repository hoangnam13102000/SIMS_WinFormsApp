using System;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using SIMS_WinFormsApp.DAL.Linq.Entities;
using SIMS_WinFormsApp.Services.Session;

namespace SIMS_WinFormsApp.DAL.Linq
{
    public static class TriggerInsertExecutor
    {
        public static void InsertInvoiceDetail(
            SimsDataContext db,
            int invoiceId,
            int productId,
            int quantity,
            decimal unitPrice)
        {
            if (db == null)
                throw new ArgumentNullException(nameof(db));

            int invoiceDetailId;
            using (SqlCommand command = CreateCommand(db))
            {
                command.CommandText =
                    "SET NOCOUNT ON; " +
                    "INSERT INTO InvoiceDetails (InvoiceID, ProductID, Quantity, UnitPrice) " +
                    "VALUES (@InvoiceID, @ProductID, @Quantity, @UnitPrice); " +
                    "SELECT TOP (1) InvoiceDetailID FROM InvoiceDetails " +
                    "WHERE InvoiceID = @InvoiceID AND ProductID = @ProductID " +
                    "AND Quantity = @Quantity AND UnitPrice = @UnitPrice " +
                    "ORDER BY InvoiceDetailID DESC;";
                command.Parameters.Add("@InvoiceID", SqlDbType.Int).Value = invoiceId;
                command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productId;
                command.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;
                SqlParameter unitPriceParameter = command.Parameters.Add("@UnitPrice", SqlDbType.Decimal);
                unitPriceParameter.Precision = 18;
                unitPriceParameter.Scale = 0;
                unitPriceParameter.Value = unitPrice;

                object insertedId = command.ExecuteScalar();
                if (insertedId == null || insertedId == DBNull.Value)
                    throw new InvalidOperationException(
                        "Không thể xác định mã chi tiết hóa đơn vừa được tạo.");
                invoiceDetailId = Convert.ToInt32(insertedId);
            }

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
            if (db == null)
                throw new ArgumentNullException(nameof(db));

            using (SqlCommand command = CreateCommand(db))
            {
                command.CommandText =
                    "INSERT INTO StockReconciliation " +
                    "(ProductID, SystemStock, ActualStock, Note, CreatedBy, CreatedAt) " +
                    "VALUES (@ProductID, @SystemStock, @ActualStock, @Note, @CreatedBy, @CreatedAt);";
                command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productId;
                command.Parameters.Add("@SystemStock", SqlDbType.Int).Value = systemStock;
                command.Parameters.Add("@ActualStock", SqlDbType.Int).Value = actualStock;
                command.Parameters.Add("@Note", SqlDbType.NVarChar, 255).Value =
                    (object)note ?? DBNull.Value;
                command.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = createdBy;
                command.Parameters.Add("@CreatedAt", SqlDbType.DateTime).Value = createdAt;
                command.ExecuteNonQuery();
            }
        }

        private static SqlCommand CreateCommand(SimsDataContext db)
        {
            SqlConnection connection = db.Connection as SqlConnection;
            SqlTransaction transaction = db.Transaction as SqlTransaction;
            if (connection == null)
                throw new InvalidOperationException(
                    "Trigger inserts require a SQL Server connection.");
            if (connection.State != ConnectionState.Open)
                throw new InvalidOperationException(
                    "Trigger inserts require an open database connection.");
            if (transaction == null)
                throw new InvalidOperationException(
                    "Trigger inserts require the active database transaction.");

            return new SqlCommand
            {
                Connection = connection,
                Transaction = transaction
            };
        }
    }
}

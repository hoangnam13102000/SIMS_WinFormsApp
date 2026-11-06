using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using SIMS_WinFormsApp.DAL.Linq;
using SIMS_WinFormsApp.DAL.Linq.Entities;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.Models.DTOs.Inventory;

namespace SIMS_WinFormsApp.Services.Implementations.Inventory
{
    public sealed class ExpiryBatchManagementService
    {
        private readonly IDbConnectionFactory _connections;

        public ExpiryBatchManagementService(IDbConnectionFactory connections)
        {
            _connections = connections ?? throw new ArgumentNullException(nameof(connections));
        }

        public IReadOnlyList<ExpiryBatchDto> GetExpiryBatches(
            string keyword, ExpiryBatchFilter filter, int? productId = null)
        {
            if (!Enum.IsDefined(typeof(ExpiryBatchFilter), filter))
                throw new ArgumentOutOfRangeException(nameof(filter));

            string normalizedKeyword = (keyword ?? string.Empty).Trim();
            string pattern = "%" + normalizedKeyword + "%";
            DateTime today = DateTime.Today;
            bool hasProduct = productId.HasValue;
            int productIdValue = productId ?? 0;
            using (SimsDataContext db = _connections.CreateDataContext())
            {
                var batches = (from batch in db.InventoryBatches
                               join product in db.Products on batch.ProductID equals product.ProductID
                               join supplier in db.Suppliers on batch.SupplierID equals supplier.SupplierID
                               where batch.Status == "ACTIVE"
                                   && batch.ExpiryDate.HasValue
                                   && batch.RemainingQty > 0
                                 && (!hasProduct || batch.ProductID == productIdValue)
                                   && (filter == ExpiryBatchFilter.All
                                       || (filter == ExpiryBatchFilter.NearExpiry
                                           && batch.ExpiryDate >= today
                                           && batch.ExpiryDate < today.AddDays(31))
                                       || (filter == ExpiryBatchFilter.Expired
                                           && batch.ExpiryDate < today))
                                   && (normalizedKeyword.Length == 0
                                       || SqlMethods.Like(product.ProductCode, pattern)
                                       || SqlMethods.Like(product.ProductName, pattern)
                                       || SqlMethods.Like(batch.BatchCode, pattern)
                                       || SqlMethods.Like(batch.LotNumber, pattern)
                                       || SqlMethods.Like(supplier.SupplierName, pattern))
                               orderby batch.ExpiryDate, product.ProductName, batch.BatchID
                               select new ExpiryBatchDto
                               {
                                   BatchId = batch.BatchID,
                                   BatchCode = batch.BatchCode,
                                   ProductCode = product.ProductCode,
                                   ProductName = product.ProductName,
                                   LotNumber = batch.LotNumber ?? "-",
                                   SupplierName = supplier.SupplierName,
                                   ExpiryDate = batch.ExpiryDate.Value,
                                   RemainingQuantity = batch.RemainingQty,
                                   UnitCost = batch.ImportPrice
                               }).ToList();
                return batches;
            }
        }

        public ExpiryBatchDto GetExpiryBatch(int batchId)
        {
            if (batchId <= 0)
                throw new ArgumentOutOfRangeException(nameof(batchId));

            using (SimsDataContext db = _connections.CreateDataContext())
            {
                return (from batch in db.InventoryBatches
                        join product in db.Products on batch.ProductID equals product.ProductID
                        join supplier in db.Suppliers on batch.SupplierID equals supplier.SupplierID
                        where batch.BatchID == batchId
                            && batch.Status == "ACTIVE"
                            && batch.ExpiryDate.HasValue
                            && batch.RemainingQty > 0
                        select new ExpiryBatchDto
                        {
                            BatchId = batch.BatchID,
                            ProductId = batch.ProductID,
                            BatchCode = batch.BatchCode,
                            ProductCode = product.ProductCode,
                            ProductName = product.ProductName,
                            LotNumber = batch.LotNumber ?? "-",
                            SupplierName = supplier.SupplierName,
                            ExpiryDate = batch.ExpiryDate.Value,
                            RemainingQuantity = batch.RemainingQty,
                            UnitCost = batch.ImportPrice
                        }).SingleOrDefault();
            }
        }

        public string DisposeExpiredBatch(int batchId, int userId)
        {
            if (batchId <= 0)
                throw new ArgumentOutOfRangeException(nameof(batchId));
            if (userId <= 0)
                throw new ArgumentOutOfRangeException(nameof(userId));

            using (SimsDataContext db = _connections.CreateDataContext())
            {
                db.Connection.Open();
                using (var transaction = db.Connection.BeginTransaction(IsolationLevel.Serializable))
                {
                    db.Transaction = transaction;
                    try
                    {
                        InventoryBatchEntity batch = db.InventoryBatches
                            .SingleOrDefault(item => item.BatchID == batchId);
                        if (batch == null)
                            throw new InvalidOperationException("Không tìm thấy lô hàng cần xử lý.");
                        ProductEntity product = db.Products
                            .SingleOrDefault(item => item.ProductID == batch.ProductID);
                        if (product == null)
                            throw new InvalidOperationException("Không tìm thấy sản phẩm của lô hàng cần xử lý.");

                        int quantity = batch.RemainingQty;
                        decimal unitCost = batch.ImportPrice;
                        string productName = product.ProductName;
                        DateTime expiryDate = batch.ExpiryDate ?? DateTime.MaxValue;

                        if (expiryDate.Date >= DateTime.Today)
                            throw new InvalidOperationException("Chỉ được tiêu hủy lô hàng đã hết hạn.");
                        if (quantity <= 0)
                            throw new InvalidOperationException("Lô hàng này không còn số lượng để xử lý.");

                        decimal lossAmount = quantity * unitCost;
                        string disposalNote = "Tiêu hủy toàn bộ số lượng còn lại của lô " + batchId
                            + " đã hết hạn ngày " + expiryDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        var disposal = new StockDisposalEntity
                        {
                            Reason = "EXPIRED",
                            Status = "COMPLETED",
                            TotalLossAmount = lossAmount,
                            Note = disposalNote,
                            CreatedBy = userId
                        };
                        db.StockDisposals.InsertOnSubmit(disposal);
                        db.SubmitChangesWithoutAutomaticAudit(ConflictMode.FailOnFirstConflict);

                        db.StockDisposalDetails.InsertOnSubmit(new StockDisposalDetailEntity
                        {
                            DisposalID = disposal.DisposalID,
                            ProductID = product.ProductID,
                            BatchID = batchId,
                            Quantity = quantity,
                            UnitCost = unitCost
                        });

                        int stockBefore = product.Stock;
                        if (stockBefore < quantity || batch.RemainingQty != quantity)
                            throw new InvalidOperationException(
                                "Tồn kho sản phẩm không khớp với số lượng lô; không thể tiêu hủy.");

                        product.Stock -= quantity;
                        batch.RemainingQty = 0;
                        batch.Status = "DEPLETED";
                        db.InventoryTransactions.InsertOnSubmit(new InventoryTransactionEntity
                        {
                            ProductID = product.ProductID,
                            TransactionType = "DISPOSAL",
                            Direction = "OUT",
                            Quantity = quantity,
                            StockBefore = stockBefore,
                            StockAfter = product.Stock,
                            RefTable = "StockDisposals",
                            RefID = disposal.DisposalID,
                            CreatedBy = userId,
                            Note = "Tiêu hủy lô hết hạn của " + productName
                        });
                        db.AuditLogs.InsertOnSubmit(new AuditLogEntity
                        {
                            UserID = userId,
                            Action = "DATA_CREATE",
                            TableName = "StockDisposals",
                            RecordID = disposal.DisposalID,
                            OldValue = JsonConvert.SerializeObject(
                                new { BatchId = batchId, RemainingQuantity = quantity }),
                            NewValue = JsonConvert.SerializeObject(
                                new { BatchId = batchId, RemainingQuantity = 0 }),
                            Detail = "Đã tiêu hủy lô " + batchId + " của sản phẩm " + productName
                                + ", số lượng " + quantity.ToString(CultureInfo.InvariantCulture)
                                + ", thiệt hại " + lossAmount.ToString(CultureInfo.InvariantCulture) + ".",
                            CreatedAt = DateTime.Now
                        });
                        db.SubmitChangesWithoutAutomaticAudit(ConflictMode.FailOnFirstConflict);

                        transaction.Commit();
                        return "TH_" + disposal.DisposalID.ToString("D6", CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                    finally
                    {
                        db.Transaction = null;
                    }
                }
            }
        }
    }

    public enum ExpiryBatchFilter
    {
        All,
        NearExpiry,
        Expired
    }
}

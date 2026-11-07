using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using System.Globalization;
using System.Linq;
using SIMS_WinFormsApp.DAL.Linq;
using SIMS_WinFormsApp.DAL.Linq.Entities;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.Repositories.Interfaces;

namespace SIMS_WinFormsApp.Repositories.Implementations
{
    public sealed class StockDisposalRepository : IStockDisposalRepository
    {
        private readonly IDbConnectionFactory _connections;

        public StockDisposalRepository(IDbConnectionFactory connections)
        {
            _connections = connections ?? throw new ArgumentNullException(nameof(connections));
        }

        public IReadOnlyList<StockDisposalListItemDto> SearchDisposals(
            string keyword, string reasonCodeList, DateTime? fromDate, DateTime? toDate)
        {
            string normalizedKeyword = (keyword ?? string.Empty).Trim();
            string pattern = "%" + normalizedKeyword + "%";
            string reasonCodes = "," + (reasonCodeList ?? string.Empty) + ",";
            DateTime? fromDateTime = fromDate.HasValue ? fromDate.Value.Date : (DateTime?)null;
            DateTime? toExclusive = toDate.HasValue ? toDate.Value.Date.AddDays(1) : (DateTime?)null;
            using (SimsDataContext db = _connections.CreateDataContext())
            {
                IQueryable<StockDisposalEntity> disposals = db.StockDisposals;
                if (fromDateTime.HasValue)
                {
                    DateTime start = fromDateTime.Value;
                    disposals = disposals.Where(disposal => disposal.CreatedAt >= start);
                }
                if (toExclusive.HasValue)
                {
                    DateTime end = toExclusive.Value;
                    disposals = disposals.Where(disposal => disposal.CreatedAt < end);
                }

                return (from disposal in disposals
                    join user in db.Users on disposal.CreatedBy equals user.UserID
                    where (normalizedKeyword.Length == 0
                            || SqlMethods.Like(disposal.DisposalCode, pattern)
                            || SqlMethods.Like(user.FullName, pattern)
                            || SqlMethods.Like(disposal.Note, pattern)
                            || reasonCodes.Contains("," + disposal.Reason + ",")
                            || db.StockDisposalDetails.Any(detail =>
                                detail.DisposalID == disposal.DisposalID
                                && (db.InventoryBatches.Any(batch =>
                                        batch.BatchID == detail.BatchID
                                        && (SqlMethods.Like(batch.BatchCode, pattern)
                                            || SqlMethods.Like(batch.LotNumber, pattern)))
                                    || db.Products.Any(product =>
                                        product.ProductID == detail.ProductID
                                        && (SqlMethods.Like(product.ProductCode, pattern)
                                            || SqlMethods.Like(product.ProductName, pattern))))))
                    orderby disposal.CreatedAt descending, disposal.DisposalID descending
                    select new StockDisposalListItemDto
                    {
                        DisposalId = disposal.DisposalID,
                        DisposalCode = disposal.DisposalCode,
                        Reason = disposal.Reason,
                        Status = disposal.Status,
                        TotalLossAmount = disposal.TotalLossAmount,
                        Note = disposal.Note,
                        CreatedByName = user.FullName,
                        CreatedAt = disposal.CreatedAt,
                        ItemCount = db.StockDisposalDetails.Count(
                            detail => detail.DisposalID == disposal.DisposalID)
                    }).ToList();
            }
        }

        public IReadOnlyList<StockDisposalDetailLineDto> GetDisposalDetails(int disposalId)
        {
            using (SimsDataContext db = _connections.CreateDataContext())
            {
                return (from detail in db.StockDisposalDetails
                    join product in db.Products on detail.ProductID equals product.ProductID
                    join batch in db.InventoryBatches on detail.BatchID equals batch.BatchID
                    from receiptDetail in db.PurchaseReceiptDetails
                        .Where(item => batch.ReceiptDetailID == item.ReceiptDetailID)
                        .DefaultIfEmpty()
                    from receipt in db.PurchaseReceipts
                        .Where(item => receiptDetail != null && item.ReceiptID == receiptDetail.ReceiptID)
                        .DefaultIfEmpty()
                    where detail.DisposalID == disposalId
                    orderby detail.DisposalDetailID
                    select new StockDisposalDetailLineDto
                    {
                        ProductCode = product.ProductCode,
                        ProductName = product.ProductName,
                        ReceiptCode = receipt == null ? null : receipt.ReceiptCode,
                        BatchCode = batch.BatchCode,
                        ExpiryDate = batch.ExpiryDate,
                        Quantity = detail.Quantity,
                        UnitCost = detail.UnitCost
                    }).ToList();
            }
        }

        public IReadOnlyList<DisposableBatchDto> GetDisposableBatches()
        {
            // Giống StockDisposalDAO.listDisposableBatches: lô còn hàng, HSD gần trước, không có HSD để cuối.
            using (SimsDataContext db = _connections.CreateDataContext())
            {
                return (from batch in db.InventoryBatches
                        join product in db.Products on batch.ProductID equals product.ProductID
                        join supplier in db.Suppliers on batch.SupplierID equals supplier.SupplierID
                        where batch.RemainingQty > 0
                        orderby (batch.ExpiryDate.HasValue ? 0 : 1), batch.ExpiryDate, batch.BatchID
                        select new DisposableBatchDto
                        {
                            BatchId = batch.BatchID,
                            BatchCode = batch.BatchCode,
                            LotNumber = batch.LotNumber,
                            ProductId = batch.ProductID,
                            ProductCode = product.ProductCode,
                            ProductName = product.ProductName,
                            SupplierName = supplier.SupplierName,
                            ExpiryDate = batch.ExpiryDate,
                            RemainingQty = batch.RemainingQty,
                            ImportPrice = batch.ImportPrice
                        }).ToList();
            }
        }

        /// <summary>Giống StockDisposalDAO.createDisposal (Java): 1 transaction thêm phiếu + dòng, trừ lô, trừ tồn, ghi DISPOSAL.</summary>
        public int CreateDisposal(StockDisposalDto disposal, out string disposalCode)
        {
            if (disposal == null) throw new ArgumentNullException(nameof(disposal));
            if (disposal.Lines == null || disposal.Lines.Count == 0)
                throw new ArgumentException("Phiếu tiêu hủy phải có ít nhất 1 dòng.", nameof(disposal));

            disposalCode = null;
            using (SimsDataContext db = _connections.CreateDataContext())
            {
                db.Connection.Open();
                using (var transaction = db.Connection.BeginTransaction(IsolationLevel.Serializable))
                {
                    db.Transaction = transaction;
                    try
                    {
                        var prepared = new List<PreparedLine>();
                        decimal totalLoss = 0m;
                        foreach (StockDisposalLineDto line in disposal.Lines)
                        {
                            InventoryBatchEntity batch = db.InventoryBatches.SingleOrDefault(
                                candidate => candidate.BatchID == line.BatchId);
                            if (batch == null)
                                throw new InvalidOperationException("Không tìm thấy lô hàng đã chọn.");
                            ProductEntity product = db.Products.SingleOrDefault(
                                candidate => candidate.ProductID == batch.ProductID);
                            if (product == null)
                                throw new InvalidOperationException("Không tìm thấy sản phẩm của lô hàng đã chọn.");
                            if (line.Quantity > batch.RemainingQty)
                                throw new InvalidOperationException("Lô của \"" + product.ProductName + "\" chỉ còn "
                                    + batch.RemainingQty + ", không đủ để hủy " + line.Quantity + ".");

                            var item = new PreparedLine
                            {
                                Batch = batch,
                                Product = product,
                                BatchId = batch.BatchID,
                                ProductId = product.ProductID,
                                Quantity = line.Quantity,
                                UnitCost = batch.ImportPrice
                            };
                            prepared.Add(item);
                            totalLoss += item.UnitCost * item.Quantity;
                        }

                        var disposalEntity = new StockDisposalEntity
                        {
                            Reason = disposal.Reason,
                            Status = "COMPLETED",
                            TotalLossAmount = totalLoss,
                            Note = string.IsNullOrWhiteSpace(disposal.Note) ? null : disposal.Note.Trim(),
                            CreatedBy = disposal.CreatedBy,
                            CreatedAt = DateTime.Now
                        };
                        db.StockDisposals.InsertOnSubmit(disposalEntity);
                        db.SubmitChangesWithoutAutomaticAudit(ConflictMode.FailOnFirstConflict);
                        disposalCode = disposalEntity.DisposalCode
                            ?? "TH_" + disposalEntity.DisposalID.ToString("D6", CultureInfo.InvariantCulture);

                        string transactionNote = "Tiêu hủy " + DisposalReasons.ToLabel(disposal.Reason)
                            + " " + disposalCode;
                        foreach (PreparedLine line in prepared)
                        {
                            if (line.Quantity > line.Batch.RemainingQty)
                                throw new InvalidOperationException(
                                    "Không trừ được tồn của lô. Vui lòng tải lại và thử lại.");

                            int stockBefore = line.Product.Stock;
                            if (stockBefore < line.Quantity)
                                throw new InvalidOperationException("Tồn kho của sản phẩm không đủ để tiêu hủy.");

                            db.StockDisposalDetails.InsertOnSubmit(new StockDisposalDetailEntity
                            {
                                DisposalID = disposalEntity.DisposalID,
                                ProductID = line.ProductId,
                                BatchID = line.BatchId,
                                Quantity = line.Quantity,
                                UnitCost = line.UnitCost
                            });

                            line.Batch.RemainingQty -= line.Quantity;
                            if (line.Batch.RemainingQty == 0)
                                line.Batch.Status = "DEPLETED";
                            line.Product.Stock -= line.Quantity;

                            db.InventoryTransactions.InsertOnSubmit(new InventoryTransactionEntity
                            {
                                ProductID = line.ProductId,
                                TransactionType = "DISPOSAL",
                                Direction = "OUT",
                                Quantity = line.Quantity,
                                StockBefore = stockBefore,
                                StockAfter = line.Product.Stock,
                                RefTable = "StockDisposals",
                                RefID = disposalEntity.DisposalID,
                                CreatedBy = disposal.CreatedBy,
                                CreatedAt = DateTime.Now,
                                Note = transactionNote
                            });
                        }

                        db.SubmitChangesWithoutAutomaticAudit(ConflictMode.FailOnFirstConflict);
                        transaction.Commit();
                        return disposalEntity.DisposalID;
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

        private sealed class PreparedLine
        {
            public InventoryBatchEntity Batch { get; set; }
            public ProductEntity Product { get; set; }
            public int BatchId { get; set; }
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal UnitCost { get; set; }
        }

    }
}

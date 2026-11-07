using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using System.Linq;
using SIMS_WinFormsApp.DAL.Linq;
using SIMS_WinFormsApp.DAL.Linq.Entities;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.Repositories.Interfaces;

namespace SIMS_WinFormsApp.Repositories.Implementations
{
    public sealed class StockReconciliationRepository : IStockReconciliationRepository
    {
        private readonly IDbConnectionFactory _connections;

        public StockReconciliationRepository(IDbConnectionFactory connections)
        {
            _connections = connections ?? throw new ArgumentNullException(nameof(connections));
        }

        // Tạo phiên hôm nay: mỗi sản phẩm đang bán, còn tồn, chưa có dòng hôm nay thì thêm 1 dòng.
        // Tồn thực tế = tồn kho nên trigger trg_StockReconciliation_Apply không đổi tồn.
        public int CreateTodaySession(int createdBy)
        {
            using (SimsDataContext db = _connections.CreateDataContext())
            {
                db.Connection.Open();
                using (var transaction = db.Connection.BeginTransaction(IsolationLevel.Serializable))
                {
                    db.Transaction = transaction;
                    try
                    {
                        DateTime today = DateTime.Today;
                        DateTime tomorrow = today.AddDays(1);
                        List<ProductEntity> eligibleProducts = (from product in db.Products
                                                                join category in db.Categories
                                                                    on product.CategoryID equals category.CategoryID
                                                                where product.Status == "ACTIVE"
                                                                    && category.Status == "ACTIVE"
                                                                    && product.Stock > 0
                                                                select product).ToList();
                        var existingProductIds = new HashSet<int>(db.StockReconciliation
                            .Where(row => row.CreatedAt >= today && row.CreatedAt < tomorrow)
                            .Select(row => row.ProductID)
                            .ToList());
                        DateTime createdAt = DateTime.Now;
                        var newRows = eligibleProducts
                            .Where(product => !existingProductIds.Contains(product.ProductID))
                            .Select(product => new StockReconciliationEntity
                            {
                                ProductID = product.ProductID,
                                SystemStock = product.Stock,
                                ActualStock = product.Stock,
                                CreatedBy = createdBy,
                                CreatedAt = createdAt
                            })
                            .ToList();

                        foreach (StockReconciliationEntity row in newRows)
                            TriggerInsertExecutor.InsertStockReconciliation(
                                db,
                                row.ProductID,
                                row.SystemStock,
                                row.ActualStock,
                                row.Note,
                                row.CreatedBy,
                                row.CreatedAt);
                        db.SubmitChangesWithoutAutomaticAudit(ConflictMode.FailOnFirstConflict);
                        transaction.Commit();
                        return newRows.Count;
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

        // Dòng hôm nay còn "Khớp" mà tồn sản phẩm đã đổi (bán, nhập, tiêu hủy...) thì
        // lấy lại tồn mới. Dòng đang lệch thì giữ nguyên số bạn đã đếm.
        public int SyncTodayWithStock()
        {
            using (SimsDataContext db = _connections.CreateDataContext())
            {
                DateTime today = DateTime.Today;
                DateTime tomorrow = today.AddDays(1);
                var matchingRows = (from row in db.StockReconciliation
                                    join product in db.Products on row.ProductID equals product.ProductID
                                    where row.CreatedAt >= today && row.CreatedAt < tomorrow
                                        && row.ActualStock == row.SystemStock
                                        && row.SystemStock != product.Stock
                                    select new { Row = row, Product = product }).ToList();

                foreach (var item in matchingRows)
                {
                    item.Row.SystemStock = item.Product.Stock;
                    item.Row.ActualStock = item.Product.Stock;
                }

                db.SubmitChangesWithoutAutomaticAudit(ConflictMode.FailOnFirstConflict);
                return matchingRows.Count;
            }
        }

        public IReadOnlyList<StockReconciliationRowDto> Search(
            string keyword, DateTime? from, DateTime? to)
        {
            string normalizedKeyword = (keyword ?? string.Empty).Trim();
            string pattern = "%" + normalizedKeyword + "%";
            DateTime? fromDate = from.HasValue ? from.Value.Date : (DateTime?)null;
            DateTime? toExclusive = to.HasValue ? to.Value.Date.AddDays(1) : (DateTime?)null;

            using (SimsDataContext db = _connections.CreateDataContext())
            {
                IQueryable<StockReconciliationEntity> rows = db.StockReconciliation
                    .Where(row => row.SystemStock != 0);
                if (fromDate.HasValue)
                {
                    DateTime start = fromDate.Value;
                    rows = rows.Where(row => row.CreatedAt >= start);
                }
                if (toExclusive.HasValue)
                {
                    DateTime end = toExclusive.Value;
                    rows = rows.Where(row => row.CreatedAt < end);
                }

                return (from row in rows
                        join product in db.Products on row.ProductID equals product.ProductID
                        join category in db.Categories on product.CategoryID equals category.CategoryID
                        join user in db.Users on row.CreatedBy equals user.UserID
                        let updatedAt = db.InventoryTransactions
                            .Where(item => item.RefTable == "StockReconciliation"
                                && item.RefID == row.ReconciliationID)
                            .Select(item => (DateTime?)item.CreatedAt)
                            .Max()
                        where (normalizedKeyword.Length == 0
                                || SqlMethods.Like(product.ProductName, pattern)
                                || SqlMethods.Like(product.ProductCode, pattern)
                                || SqlMethods.Like(category.CategoryName, pattern)
                                || SqlMethods.Like(row.Note, pattern))
                        orderby row.CreatedAt.Date descending,
                            (row.ActualStock != row.SystemStock ? 0 : 1),
                            product.ProductName,
                            row.ReconciliationID descending
                        select new StockReconciliationRowDto
                        {
                            ReconciliationId = row.ReconciliationID,
                            ProductId = row.ProductID,
                            ProductCode = product.ProductCode,
                            ProductName = product.ProductName,
                            CategoryName = category.CategoryName,
                            SystemStock = row.SystemStock,
                            ActualStock = row.ActualStock,
                            CurrentStock = product.Stock,
                            Note = row.Note,
                            CreatedAt = row.CreatedAt,
                            UpdatedAt = updatedAt,
                            CreatedByName = user.FullName
                        }).ToList();
            }
        }

        // Sửa tồn thực tế / lý do của dòng hôm nay. Phần chênh so với lần đếm trước được
        // cộng/trừ vào Products.Stock ngay và ghi InventoryTransactions (RECONCILE_ADJUST).
        public StockCountUpdateStatus UpdateCount(
            int reconciliationId, int actualStock, string note, int userId)
        {
            using (SimsDataContext db = _connections.CreateDataContext())
            {
                db.Connection.Open();
                using (var transaction = db.Connection.BeginTransaction(IsolationLevel.Serializable))
                {
                    db.Transaction = transaction;
                    try
                    {
                        StockReconciliationEntity row = db.StockReconciliation.SingleOrDefault(
                            item => item.ReconciliationID == reconciliationId);
                        if (row == null)
                        {
                            transaction.Rollback();
                            return StockCountUpdateStatus.NotFound;
                        }

                        if (row.CreatedAt.Date != DateTime.Today)
                        {
                            transaction.Rollback();
                            return StockCountUpdateStatus.Locked;
                        }

                        if (actualStock < 0 || actualStock > row.SystemStock)
                        {
                            transaction.Rollback();
                            return StockCountUpdateStatus.OutOfRange;
                        }

                        int delta = actualStock - row.ActualStock;
                        if (delta != 0)
                        {
                            ProductEntity product = db.Products.SingleOrDefault(
                                item => item.ProductID == row.ProductID);
                            if (product == null)
                                throw new InvalidOperationException(
                                    "Không tìm thấy sản phẩm của dòng đối chiếu tồn kho.");

                            int stockBefore = product.Stock;
                            int stockAfter = stockBefore + delta;
                            if (stockAfter < 0)
                            {
                                transaction.Rollback();
                                return StockCountUpdateStatus.NotEnoughStock;
                            }

                            product.Stock = stockAfter;
                            db.InventoryTransactions.InsertOnSubmit(new InventoryTransactionEntity
                            {
                                ProductID = product.ProductID,
                                TransactionType = "RECONCILE_ADJUST",
                                Direction = delta > 0 ? "IN" : "OUT",
                                Quantity = Math.Abs(delta),
                                StockBefore = stockBefore,
                                StockAfter = stockAfter,
                                RefTable = "StockReconciliation",
                                RefID = row.ReconciliationID,
                                CreatedBy = userId,
                                CreatedAt = DateTime.Now,
                                Note = string.IsNullOrWhiteSpace(note) ? null : note.Trim()
                            });
                        }

                        row.ActualStock = actualStock;
                        row.Note = string.IsNullOrWhiteSpace(note) ? null : note.Trim();
                        db.SubmitChangesWithoutAutomaticAudit(ConflictMode.FailOnFirstConflict);
                        transaction.Commit();
                        return StockCountUpdateStatus.Ok;
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
}

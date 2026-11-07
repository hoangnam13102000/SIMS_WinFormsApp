using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using SIMS_WinFormsApp.DAL.Linq;
using SIMS_WinFormsApp.DAL.Linq.Entities;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.Services.Session;

namespace SIMS_WinFormsApp.Repositories.Implementations
{
    public sealed class InventoryRepository : IInventoryRepository
    {
        private readonly IDbConnectionFactory _connections;

        public InventoryRepository(IDbConnectionFactory connections)
        {
            _connections = connections ?? throw new ArgumentNullException(nameof(connections));
        }

        public IReadOnlyList<LookupItem> GetActiveSuppliers()
        {
            using (SimsDataContext db = _connections.CreateDataContext())
                return db.Suppliers
                    .Where(supplier => !supplier.IsDeleted)
                    .OrderBy(supplier => supplier.SupplierName)
                    .Select(supplier => new LookupItem
                    {
                        Id = supplier.SupplierID,
                        Name = supplier.SupplierName
                    })
                    .ToList();
        }

        public IReadOnlyList<LookupItem> GetActiveProducts()
        {
            using (SimsDataContext db = _connections.CreateDataContext())
                return db.Products
                    .Where(product => product.Status == "ACTIVE")
                    .OrderBy(product => product.ProductName)
                    .Select(product => new LookupItem
                    {
                        Id = product.ProductID,
                        Name = product.ProductCode + " - " + product.ProductName
                    })
                    .ToList();
        }

        public IReadOnlyList<LookupItem> GetCategories()
        {
            using (SimsDataContext db = _connections.CreateDataContext())
                return db.Categories
                    .OrderBy(category => category.CategoryName)
                    .Select(category => new LookupItem
                    {
                        Id = category.CategoryID,
                        Name = category.CategoryName
                    })
                    .ToList();
        }

        public int CreatePurchaseReceipt(PurchaseReceiptDto receipt, out string receiptCode)
        {
            if (receipt == null) throw new ArgumentNullException(nameof(receipt));
            if (receipt.Lines == null) throw new ArgumentException("Danh sách dòng nhập không được null.", nameof(receipt));

            receiptCode = null;
            using (SimsDataContext db = _connections.CreateDataContext())
            {
                db.Connection.Open();
                using (var transaction = db.Connection.BeginTransaction(IsolationLevel.Serializable))
                {
                    db.Transaction = transaction;
                    try
                    {
                        string codePrefix = "PN-" + DateTime.Today.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + "-";
                        int sequence = db.PurchaseReceipts
                            .Where(item => item.ReceiptCode.StartsWith(codePrefix))
                            .Select(item => item.ReceiptCode)
                            .ToList()
                            .Select(code =>
                            {
                                string suffix = code.Substring(codePrefix.Length);
                                int parsed;
                                return suffix.Length == 3 && int.TryParse(
                                    suffix, NumberStyles.None, CultureInfo.InvariantCulture, out parsed)
                                    ? parsed
                                    : 0;
                            })
                            .DefaultIfEmpty(0)
                            .Max() + 1;

                        if (sequence > 999)
                            throw new InvalidOperationException("Mã phiếu nhập trong ngày đã vượt quá 999.");

                        receiptCode = codePrefix + sequence.ToString("D3", CultureInfo.InvariantCulture);
                        var receiptEntity = new PurchaseReceiptEntity
                        {
                            ReceiptCode = receiptCode,
                            SupplierID = receipt.SupplierId,
                            CreatedBy = receipt.CreatedBy,
                            CreatedAt = DateTime.Now,
                            TotalAmount = 0m,
                            Status = "COMPLETED"
                        };
                        db.PurchaseReceipts.InsertOnSubmit(receiptEntity);
                        db.SubmitChangesWithoutAutomaticAudit(ConflictMode.FailOnFirstConflict);

                        decimal totalAmount = 0m;
                        foreach (PurchaseLineDto line in receipt.Lines)
                        {
                            if (line == null)
                                throw new ArgumentException("Dòng nhập không được null.", nameof(receipt));

                            totalAmount += line.Quantity * line.ImportPrice;
                            db.PurchaseReceiptDetails.InsertOnSubmit(new PurchaseReceiptDetailEntity
                            {
                                ReceiptID = receiptEntity.ReceiptID,
                                ProductID = line.ProductId,
                                Quantity = line.Quantity,
                                ImportPrice = line.ImportPrice,
                                LotNumber = line.LotNumber,
                                ManufactureDate = line.ManufactureDate,
                                ExpiryDate = line.ExpiryDate
                            });
                        }
                        receiptEntity.TotalAmount = totalAmount;
                        db.AuditLogs.InsertOnSubmit(new AuditLogEntity
                        {
                            UserID = receipt.CreatedBy,
                            Action = "DATA_CREATE",
                            TableName = "PurchaseReceipts",
                            RecordID = receiptEntity.ReceiptID,
                            NewValue = JsonConvert.SerializeObject(new
                            {
                                ReceiptCode = receiptCode,
                                receipt.SupplierId,
                                TotalAmount = totalAmount,
                                Lines = receipt.Lines.Select(line => new
                                {
                                    line.ProductId,
                                    line.Quantity,
                                    line.ImportPrice,
                                    line.LotNumber,
                                    line.ManufactureDate,
                                    line.ExpiryDate
                                }).ToList()
                            }),
                            Detail = "Đã tạo phiếu nhập " + receiptCode + ".",
                            CreatedAt = DateTime.Now
                        });
                        db.SubmitChangesWithoutAutomaticAudit(ConflictMode.FailOnFirstConflict);

                        transaction.Commit();
                        return receiptEntity.ReceiptID;
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

        public IReadOnlyList<PurchaseReceiptListItemDto> SearchPurchaseReceipts(
            string keyword, DateTime? fromDate, DateTime? toDate)
        {
            string normalizedKeyword = (keyword ?? string.Empty).Trim();
            string pattern = "%" + normalizedKeyword + "%";
            DateTime? fromDateTime = fromDate.HasValue ? fromDate.Value.Date : (DateTime?)null;
            DateTime? toExclusive = toDate.HasValue
                ? toDate.Value.Date.AddDays(1)
                : (DateTime?)null;
            using (SimsDataContext db = _connections.CreateDataContext())
            {
                IQueryable<PurchaseReceiptEntity> receipts = db.PurchaseReceipts;
                if (fromDateTime.HasValue)
                {
                    DateTime start = fromDateTime.Value;
                    receipts = receipts.Where(receipt => receipt.CreatedAt >= start);
                }
                if (toExclusive.HasValue)
                {
                    DateTime end = toExclusive.Value;
                    receipts = receipts.Where(receipt => receipt.CreatedAt < end);
                }

                return (from receipt in receipts
                        join supplier in db.Suppliers on receipt.SupplierID equals supplier.SupplierID
                        join user in db.Users on receipt.CreatedBy equals user.UserID
                        where (normalizedKeyword.Length == 0
                                || SqlMethods.Like(receipt.ReceiptCode, pattern)
                                || SqlMethods.Like(supplier.SupplierName, pattern)
                                || SqlMethods.Like(user.FullName, pattern)
                                || db.PurchaseReceiptDetails.Any(detail =>
                                    detail.ReceiptID == receipt.ReceiptID
                                    && db.Products.Any(product =>
                                        product.ProductID == detail.ProductID
                                        && (SqlMethods.Like(product.ProductCode, pattern)
                                            || SqlMethods.Like(product.ProductName, pattern)
                                            || SqlMethods.Like(detail.LotNumber, pattern)
                                            || db.InventoryBatches.Any(batch =>
                                                batch.ReceiptDetailID == detail.ReceiptDetailID
                                                && SqlMethods.Like(batch.BatchCode, pattern))))))
                        orderby receipt.CreatedAt descending, receipt.ReceiptID descending
                        select new PurchaseReceiptListItemDto
                        {
                            ReceiptId = receipt.ReceiptID,
                            ReceiptCode = receipt.ReceiptCode,
                            SupplierName = supplier.SupplierName,
                            CreatedByName = user.FullName,
                            CreatedAt = receipt.CreatedAt,
                            TotalAmount = receipt.TotalAmount,
                            Status = receipt.Status,
                            ItemCount = db.PurchaseReceiptDetails.Count(
                                detail => detail.ReceiptID == receipt.ReceiptID)
                        }).ToList();
            }
        }

        public IReadOnlyList<PurchaseReceiptDetailLineDto> GetPurchaseReceiptDetails(int receiptId)
        {
            using (SimsDataContext db = _connections.CreateDataContext())
            {
                return (from detail in db.PurchaseReceiptDetails
                        join product in db.Products on detail.ProductID equals product.ProductID
                        from batch in db.InventoryBatches
                            .Where(item => item.ReceiptDetailID == detail.ReceiptDetailID)
                            .DefaultIfEmpty()
                        where detail.ReceiptID == receiptId
                        orderby detail.ReceiptDetailID
                        select new PurchaseReceiptDetailLineDto
                        {
                            ProductCode = product.ProductCode,
                            ProductName = product.ProductName,
                            BatchCode = batch == null ? null : batch.BatchCode,
                            LotNumber = detail.LotNumber,
                            ManufactureDate = detail.ManufactureDate,
                            ExpiryDate = detail.ExpiryDate,
                            Quantity = detail.Quantity,
                            ImportPrice = detail.ImportPrice
                        }).ToList();
            }
        }

        public IReadOnlyList<string> GetPurchaseReceiptSuggestions()
        {
            using (SimsDataContext db = _connections.CreateDataContext())
            {
                var suggestions = db.PurchaseReceipts
                    .Select(receipt => receipt.ReceiptCode)
                    .ToList();
                suggestions.AddRange((from receipt in db.PurchaseReceipts
                                      join supplier in db.Suppliers
                                          on receipt.SupplierID equals supplier.SupplierID
                                      select supplier.SupplierName).ToList());
                suggestions.AddRange((from receipt in db.PurchaseReceipts
                                      join user in db.Users on receipt.CreatedBy equals user.UserID
                                      select user.FullName).ToList());
                suggestions.AddRange((from detail in db.PurchaseReceiptDetails
                                      join product in db.Products
                                          on detail.ProductID equals product.ProductID
                                      select product.ProductCode).ToList());
                suggestions.AddRange((from detail in db.PurchaseReceiptDetails
                                      join product in db.Products
                                          on detail.ProductID equals product.ProductID
                                      select product.ProductName).ToList());
                suggestions.AddRange(db.PurchaseReceiptDetails
                    .Select(detail => detail.LotNumber)
                    .ToList());
                suggestions.AddRange((from batch in db.InventoryBatches
                                      where batch.ReceiptDetailID.HasValue
                                          && db.PurchaseReceiptDetails.Any(detail =>
                                              detail.ReceiptDetailID == batch.ReceiptDetailID.Value)
                                      select batch.BatchCode).ToList());

                return suggestions
                    .Where(suggestion => !string.IsNullOrWhiteSpace(suggestion))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .OrderBy(suggestion => suggestion, StringComparer.OrdinalIgnoreCase)
                    .ToList();
            }
        }

        public IReadOnlyList<StockRowDto> GetStock(string keyword, int? categoryId)
        {
            string normalizedKeyword = (keyword ?? string.Empty).Trim();
            string pattern = "%" + normalizedKeyword + "%";
            using (SimsDataContext db = _connections.CreateDataContext())
            {
                IQueryable<ProductEntity> products = db.Products
                    .Where(product => product.Status == "ACTIVE");
                if (categoryId.HasValue)
                {
                    int selectedCategoryId = categoryId.Value;
                    products = products.Where(product => product.CategoryID == selectedCategoryId);
                }

                return (from product in products
                        join category in db.Categories
                            on product.CategoryID equals category.CategoryID
                        where (normalizedKeyword.Length == 0
                                || SqlMethods.Like(product.ProductName, pattern)
                                || SqlMethods.Like(product.ProductCode, pattern))
                        orderby (product.Stock <= product.MinStock ? 0 : 1), product.ProductName
                        select new StockRowDto
                        {
                            ProductId = product.ProductID,
                            Code = product.ProductCode,
                            Name = product.ProductName,
                            CategoryName = category.CategoryName,
                            Stock = product.Stock,
                            MinStock = product.MinStock
                        }).ToList();
            }
        }

        public IReadOnlyList<StockAlertListItemDto> SearchStockAlerts(
            string keyword, int? categoryId, DateTime? from, DateTime? to)
        {
            string normalizedKeyword = (keyword ?? string.Empty).Trim();
            string pattern = "%" + normalizedKeyword + "%";
            DateTime? fromDate = from.HasValue ? from.Value.Date : (DateTime?)null;
            DateTime? toExclusive = to.HasValue ? to.Value.Date.AddDays(1) : (DateTime?)null;
            using (SimsDataContext db = _connections.CreateDataContext())
            {
                IQueryable<StockAlertEntity> alerts = db.StockAlerts;
                if (fromDate.HasValue)
                {
                    DateTime start = fromDate.Value;
                    alerts = alerts.Where(alert => alert.CreatedAt >= start);
                }
                if (toExclusive.HasValue)
                {
                    DateTime end = toExclusive.Value;
                    alerts = alerts.Where(alert => alert.CreatedAt < end);
                }

                IQueryable<ProductEntity> products = db.Products;
                if (categoryId.HasValue)
                {
                    int selectedCategoryId = categoryId.Value;
                    products = products.Where(product => product.CategoryID == selectedCategoryId);
                }

                return (from alert in alerts
                        join product in products on alert.ProductID equals product.ProductID
                        join reporter in db.Users on alert.ReportedBy equals reporter.UserID into reporters
                        from reportedBy in reporters.DefaultIfEmpty()
                        join resolver in db.Users on alert.ResolvedBy equals resolver.UserID into resolvers
                        from resolvedBy in resolvers.DefaultIfEmpty()
                        where (normalizedKeyword.Length == 0
                                || SqlMethods.Like(product.ProductName, pattern)
                                || SqlMethods.Like(product.ProductCode, pattern))
                        orderby alert.CreatedAt descending, alert.AlertID descending
                        select new StockAlertListItemDto
                        {
                            AlertId = alert.AlertID,
                            ProductId = alert.ProductID,
                            ProductCode = product.ProductCode,
                            ProductName = product.ProductName,
                            CategoryId = product.CategoryID,
                            AlertType = alert.AlertType,
                            StockAtReport = alert.StockAtReport,
                            ReportedByName = reportedBy == null ? "Hệ thống" : reportedBy.FullName,
                            CreatedAt = alert.CreatedAt,
                            Status = alert.Status,
                            ResolvedByName = resolvedBy == null ? null : resolvedBy.FullName,
                            ResolvedAt = alert.ResolvedAt,
                            Note = alert.Note
                        }).ToList();
            }
        }

        public int ReportOutOfStock(int productId, int stockAtReport, int reportedBy)
        {
            using (SimsDataContext db = _connections.CreateDataContext())
            {
                db.Connection.Open();
                using (var transaction = db.Connection.BeginTransaction())
                {
                    db.Transaction = transaction;
                    try
                    {
                        var alert = new StockAlertEntity
                        {
                            ProductID = productId,
                            AlertType = "OUT_OF_STOCK",
                            StockAtReport = stockAtReport,
                            Note = "Báo hết hàng từ POS",
                            ReportedBy = reportedBy,
                            CreatedAt = DateTime.Now,
                            Status = "NEW",
                            SeenByInventoryManager = false
                        };
                        db.StockAlerts.InsertOnSubmit(alert);
                        db.SubmitChangesWithoutAutomaticAudit(ConflictMode.FailOnFirstConflict);

                        WriteAuditLog(
                            db,
                            reportedBy,
                            "DATA_CREATE",
                            "StockAlerts",
                            alert.AlertID,
                            null,
                            JsonConvert.SerializeObject(new
                            {
                                ProductID = productId,
                                AlertType = "OUT_OF_STOCK",
                                StockAtReport = stockAtReport,
                                ReportedBy = reportedBy,
                                Status = "NEW"
                            }),
                            "Nhân viên báo hết hàng từ POS.");
                        db.SubmitChangesWithoutAutomaticAudit(ConflictMode.FailOnFirstConflict);

                        transaction.Commit();
                        return alert.AlertID;
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

        public int MarkAlertPlanned(int alertId)
        {
            using (SimsDataContext db = _connections.CreateDataContext())
            {
                db.Connection.Open();
                using (var transaction = db.Connection.BeginTransaction(IsolationLevel.Serializable))
                {
                    db.Transaction = transaction;
                    try
                    {
                        StockAlertEntity alert = db.StockAlerts.SingleOrDefault(item =>
                            item.AlertID == alertId && item.Status == "NEW");
                        if (alert == null)
                        {
                            transaction.Commit();
                            return 0;
                        }

                        string oldStatus = alert.Status;
                        alert.Status = "PLANNED";
                        WriteAuditLog(
                            db,
                            UserSession.Instance.CurrentUser?.UserId,
                            "DATA_UPDATE",
                            "StockAlerts",
                            alertId,
                            JsonConvert.SerializeObject(new { Status = oldStatus }),
                            JsonConvert.SerializeObject(new { Status = alert.Status }),
                            "Đã chuyển cảnh báo tồn kho sang trạng thái dự kiến.");
                        db.SubmitChangesWithoutAutomaticAudit(ConflictMode.FailOnFirstConflict);
                        transaction.Commit();
                        return 1;
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

        public int ResolveAlert(int alertId, int userId)
        {
            using (SimsDataContext db = _connections.CreateDataContext())
            {
                db.Connection.Open();
                using (var transaction = db.Connection.BeginTransaction(IsolationLevel.Serializable))
                {
                    db.Transaction = transaction;
                    try
                    {
                        StockAlertEntity alert = db.StockAlerts.SingleOrDefault(item =>
                            item.AlertID == alertId && item.Status != "RESOLVED");
                        if (alert == null)
                        {
                            transaction.Commit();
                            return 0;
                        }

                        string oldStatus = alert.Status;
                        alert.Status = "RESOLVED";
                        alert.ResolvedBy = userId;
                        alert.ResolvedAt = DateTime.Now;
                        WriteAuditLog(
                            db,
                            userId,
                            "DATA_UPDATE",
                            "StockAlerts",
                            alertId,
                            JsonConvert.SerializeObject(new { Status = oldStatus }),
                            JsonConvert.SerializeObject(new { Status = alert.Status }),
                            "Đã xử lý cảnh báo tồn kho.");
                        db.SubmitChangesWithoutAutomaticAudit(ConflictMode.FailOnFirstConflict);
                        transaction.Commit();
                        return 1;
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

        public int MarkAllAlertsSeen()
        {
            using (SimsDataContext db = _connections.CreateDataContext())
            {
                List<StockAlertEntity> alerts = db.StockAlerts
                    .Where(alert => !alert.SeenByInventoryManager)
                    .ToList();
                foreach (StockAlertEntity alert in alerts)
                    alert.SeenByInventoryManager = true;
                if (alerts.Count > 0)
                    db.SubmitChangesWithoutAutomaticAudit(ConflictMode.FailOnFirstConflict);
                return alerts.Count;
            }
        }

        public void SaveStockCounts(IEnumerable<StockCountDto> counts, int createdBy)
        {
            if (counts == null) throw new ArgumentNullException(nameof(counts));

            using (SimsDataContext db = _connections.CreateDataContext())
            {
                db.Connection.Open();
                using (var transaction = db.Connection.BeginTransaction())
                {
                    db.Transaction = transaction;
                    try
                    {
                        foreach (StockCountDto count in counts)
                        {
                            if (count == null)
                                throw new ArgumentException("Dòng kiểm kê không được null.", nameof(counts));

                            TriggerInsertExecutor.InsertStockReconciliation(
                                db,
                                count.ProductId,
                                count.SystemStock,
                                count.ActualStock,
                                count.Note,
                                createdBy,
                                DateTime.Now);
                            WriteAuditLog(
                                db,
                                createdBy,
                                "DATA_CREATE",
                                "StockReconciliation",
                                null,
                                null,
                                JsonConvert.SerializeObject(new
                                {
                                    count.ProductId,
                                    count.SystemStock,
                                    count.ActualStock,
                                    count.Note
                                }),
                                "Đã tạo bản ghi kiểm kê tồn kho.");
                        }

                        db.SubmitChangesWithoutAutomaticAudit(ConflictMode.FailOnFirstConflict);
                        transaction.Commit();
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

        private static void WriteAuditLog(
            SimsDataContext db,
            int? userId,
            string action,
            string tableName,
            int? recordId,
            string oldValue,
            string newValue,
            string detail)
        {
            db.AuditLogs.InsertOnSubmit(new AuditLogEntity
            {
                UserID = userId,
                Action = action,
                TableName = tableName,
                RecordID = recordId,
                OldValue = oldValue,
                NewValue = newValue,
                Detail = detail,
                CreatedAt = DateTime.Now
            });
        }
    }
}
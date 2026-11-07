using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using SIMS_WinFormsApp.DAL.Linq;
using SIMS_WinFormsApp.DAL.Linq.Entities;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.MVP.Presenters.Pos;
using SIMS_WinFormsApp.Models.DTOs.Sales;

namespace SIMS_WinFormsApp.Services.Implementations.Sales
{
    internal sealed class ReturnableLineData
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int SoldQuantity { get; set; }
        public int ReturnedQuantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    internal sealed class ReturnSelectionData
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    internal sealed class ReturnCreateResult
    {
        public int ReturnId { get; set; }
        public bool RequiresApproval { get; set; }
        public bool ApprovedImmediately { get; set; }
    }

    internal sealed class ReturnRequestRowData
    {
        public int ReturnId { get; set; }
        public string InvoiceCode { get; set; }
        public string CustomerName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Reason { get; set; }
        public decimal TotalValue { get; set; }
        public string Status { get; set; }
    }

    internal interface IReturnManagementDataService
    {
        IReadOnlyList<ReturnableLineData> GetReturnableLines(int invoiceId);
        void GetReturnPolicy(out decimal approvalThreshold, out int returnPolicyDays);
        ReturnCreateResult CreateReturn(
            int invoiceId, int currentUserId, int returnPolicyDays, bool canApprove, bool approveImmediately,
            string reason, IReadOnlyList<ReturnSelectionData> selected);
        IReadOnlyList<ReturnRequestRowData> GetRequests(
            DateTime fromDate, DateTime toDate, bool canApprove, int currentUserId,
            string keyword, string reasonFilter);
        bool UpdateRequest(int returnId, bool approve, string rejectionReason, int userId);
        List<NotificationDto> GetCompletedReturns(int userId);
        List<NotificationDto> GetPendingReturnNotifications(int currentUserId);
        List<NotificationDto> GetStockAlertNotifications();
        List<NotificationDto> GetLowStockNotifications();
    }

    internal sealed class ReturnManagementDataService : IReturnManagementDataService
    {
        private readonly IDbConnectionFactory _connections;

        public ReturnManagementDataService(IDbConnectionFactory connections)
        {
            _connections = connections ?? throw new ArgumentNullException(nameof(connections));
        }

        public IReadOnlyList<ReturnableLineData> GetReturnableLines(int invoiceId)
        {
            using (var db = _connections.CreateDataContext())
            {
                var sold = (from detail in db.InvoiceDetails
                            join product in db.Products on detail.ProductID equals product.ProductID
                            where detail.InvoiceID == invoiceId
                            group detail by new { detail.ProductID, product.ProductName, detail.UnitPrice } into lines
                            select new
                            {
                                lines.Key.ProductID,
                                lines.Key.ProductName,
                                lines.Key.UnitPrice,
                                Quantity = lines.Sum(line => line.Quantity)
                            }).OrderBy(line => line.ProductName).ThenBy(line => line.UnitPrice).ToList();
                var returned = (from detail in db.ReturnExchangeDetails
                                join request in db.ReturnExchanges on detail.ReturnID equals request.ReturnID
                                where request.InvoiceID == invoiceId
                                    && (request.Status == "PENDING" || request.Status == "APPROVED")
                                    && detail.Direction == "IN"
                                group detail by new { detail.ProductID, detail.UnitPrice } into lines
                                select new
                                {
                                    lines.Key.ProductID,
                                    lines.Key.UnitPrice,
                                    Quantity = lines.Sum(line => line.Quantity)
                                }).ToList();
                return sold.Select(line => new ReturnableLineData
                {
                    ProductId = line.ProductID,
                    ProductName = line.ProductName,
                    SoldQuantity = line.Quantity,
                    ReturnedQuantity = returned.Where(item =>
                        item.ProductID == line.ProductID && item.UnitPrice == line.UnitPrice)
                        .Select(item => item.Quantity).FirstOrDefault(),
                    UnitPrice = line.UnitPrice
                }).ToList();
            }
        }

        public void GetReturnPolicy(out decimal approvalThreshold, out int returnPolicyDays)
        {
            approvalThreshold = 0m;
            returnPolicyDays = 7;
            using (var db = _connections.CreateDataContext())
            {
                var settings = db.StoreConfigs.Where(c => c.ConfigKey == "RETURN_APPROVAL_THRESHOLD"
                    || c.ConfigKey == "RETURN_POLICY_DAYS").ToList();
                foreach (var setting in settings)
                {
                    decimal decimalValue;
                    int intValue;
                    if (setting.ConfigKey == "RETURN_APPROVAL_THRESHOLD"
                        && decimal.TryParse(setting.ConfigValue, NumberStyles.Any, CultureInfo.InvariantCulture, out decimalValue))
                        approvalThreshold = decimalValue;
                    else if (setting.ConfigKey == "RETURN_POLICY_DAYS"
                        && int.TryParse(setting.ConfigValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out intValue))
                        returnPolicyDays = intValue;
                }
            }
        }

        public ReturnCreateResult CreateReturn(
            int invoiceId, int currentUserId, int returnPolicyDays, bool canApprove, bool approveImmediately,
            string reason,
            IReadOnlyList<ReturnSelectionData> selected)
        {
            using (var db = _connections.CreateDataContext())
            {
                db.Connection.Open();
                using (var transaction = db.Connection.BeginTransaction(IsolationLevel.Serializable))
                {
                    db.Transaction = transaction;
                    try
                    {
                        InvoiceEntity invoice = db.Invoices.SingleOrDefault(item => item.InvoiceID == invoiceId);
                        if (invoice == null)
                            throw new InvalidOperationException("Hóa đơn không còn tồn tại.");
                        if (invoice.Status != "ACTIVE")
                            throw new InvalidOperationException("Chỉ có thể trả hàng từ hóa đơn đang hoạt động.");
                        if (DateTime.Today > invoice.CreatedAt.Date.AddDays(returnPolicyDays))
                            throw new InvalidOperationException("Hóa đơn đã quá thời hạn đổi/trả " +
                                returnPolicyDays + " ngày.");
                        if (db.ReturnExchanges.Any(item => item.InvoiceID == invoiceId && item.Status == "PENDING"))
                            throw new InvalidOperationException(
                                "Hóa đơn đang có yêu cầu trả hàng chờ duyệt. Hãy đợi yêu cầu đó được xử lý trước.");

                        foreach (var request in selected.GroupBy(item => item.ProductId))
                        {
                            int requestedQuantity = request.Sum(item => item.Quantity);
                            int sold = db.InvoiceDetails.Where(detail =>
                                    detail.InvoiceID == invoiceId && detail.ProductID == request.Key)
                                .Select(detail => (int?)detail.Quantity).Sum() ?? 0;
                            int returned = (from detail in db.ReturnExchangeDetails
                                            join existing in db.ReturnExchanges on detail.ReturnID equals existing.ReturnID
                                            where existing.InvoiceID == invoiceId
                                                && (existing.Status == "PENDING" || existing.Status == "APPROVED")
                                                && detail.ProductID == request.Key && detail.Direction == "IN"
                                            select (int?)detail.Quantity).Sum() ?? 0;
                            if (requestedQuantity > sold - returned)
                                throw new InvalidOperationException("Số lượng còn được trả của sản phẩm vừa thay đổi. Hãy tải lại hóa đơn.");
                        }

                        decimal gross = selected.Sum(item => item.Quantity * item.UnitPrice);
                        decimal discountShare = invoice.SubTotal <= 0m ? 0m : Math.Min(invoice.DiscountAmount,
                            decimal.Round(gross * invoice.DiscountAmount / invoice.SubTotal, 0, MidpointRounding.AwayFromZero));
                        decimal pointsShare = invoice.SubTotal <= 0m ? 0m : Math.Min(invoice.PointsDiscountAmount,
                            decimal.Round(gross * invoice.PointsDiscountAmount / invoice.SubTotal, 0, MidpointRounding.AwayFromZero));
                        decimal taxable = Math.Max(0m, gross - discountShare);
                        decimal refundValue = decimal.Round(
                            Math.Max(0m, taxable + taxable * invoice.VATRate / 100m - pointsShare),
                            0, MidpointRounding.AwayFromZero);

                        decimal threshold = 0m;
                        string thresholdValue = db.StoreConfigs
                            .Where(c => c.ConfigKey == "RETURN_APPROVAL_THRESHOLD")
                            .Select(c => c.ConfigValue).FirstOrDefault();
                        decimal.TryParse(thresholdValue, NumberStyles.Any, CultureInfo.InvariantCulture, out threshold);
                        bool requiresApproval = refundValue > threshold;
                        bool approved = !requiresApproval || (canApprove && approveImmediately);
                        var requestEntity = new ReturnExchangeEntity
                        {
                            InvoiceID = invoiceId,
                            Type = "RETURN",
                            Reason = reason,
                            TotalValue = refundValue,
                            DiscountShare = discountShare,
                            PointsShare = pointsShare,
                            RequiresApproval = requiresApproval,
                            Status = "PENDING",
                            CreatedBy = currentUserId,
                            CreatedAt = DateTime.Now
                        };
                        db.ReturnExchanges.InsertOnSubmit(requestEntity);
                        db.SubmitChanges();
                        foreach (ReturnSelectionData item in selected)
                            db.ReturnExchangeDetails.InsertOnSubmit(new ReturnExchangeDetailEntity
                            {
                                ReturnID = requestEntity.ReturnID,
                                ProductID = item.ProductId,
                                Quantity = item.Quantity,
                                Direction = "IN",
                                UnitPrice = item.UnitPrice
                            });
                        db.SubmitChanges();
                        if (approved)
                        {
                            requestEntity.Status = "APPROVED";
                            requestEntity.ApprovedBy = currentUserId;
                            requestEntity.ApprovedAt = DateTime.Now;
                            RestoreLoyaltyPoints(db, invoiceId, requestEntity.ReturnID,
                                invoice.CustomerID, pointsShare);
                        }
                        db.SubmitChanges();
                        transaction.Commit();
                        return new ReturnCreateResult
                        {
                            ReturnId = requestEntity.ReturnID,
                            RequiresApproval = requiresApproval,
                            ApprovedImmediately = approved
                        };
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public IReadOnlyList<ReturnRequestRowData> GetRequests(
            DateTime fromDate, DateTime toDate, bool canApprove, int currentUserId,
            string keyword, string reasonFilter)
        {
            DateTime startDate = fromDate.Date;
            DateTime end = toDate.Date.AddDays(1);
            string search = (keyword ?? string.Empty).Trim();
            string reason = reasonFilter ?? string.Empty;
            using (var db = _connections.CreateDataContext())
                return (from request in db.ReturnExchanges
                        join invoice in db.Invoices on request.InvoiceID equals invoice.InvoiceID
                        join customerUser in db.Users on invoice.CustomerID equals (int?)customerUser.UserID into customers
                        from customer in customers.DefaultIfEmpty()
                        where request.CreatedAt >= startDate && request.CreatedAt < end
                            && (canApprove || request.CreatedBy == currentUserId)
                            && (search == string.Empty || invoice.InvoiceCode.Contains(search)
                                || (customer != null && customer.FullName.Contains(search))
                                || request.Reason.Contains(search))
                            && (reason == string.Empty || request.Reason.Contains(reason))
                        orderby (request.Status == "PENDING") descending, request.CreatedAt descending
                        select new ReturnRequestRowData
                        {
                            ReturnId = request.ReturnID,
                            InvoiceCode = invoice.InvoiceCode,
                            CustomerName = customer == null ? "Khách lẻ" : customer.FullName,
                            CreatedAt = request.CreatedAt,
                            Reason = request.Reason,
                            TotalValue = request.TotalValue,
                            Status = request.Status
                        }).ToList();
        }

        public bool UpdateRequest(int returnId, bool approve, string rejectionReason, int userId)
        {
            using (var db = _connections.CreateDataContext())
            {
                db.Connection.Open();
                using (var transaction = db.Connection.BeginTransaction(IsolationLevel.Serializable))
                {
                    db.Transaction = transaction;
                    try
                    {
                        ReturnExchangeEntity request = db.ReturnExchanges.SingleOrDefault(item =>
                            item.ReturnID == returnId && item.Status == "PENDING"
                                && (!approve || item.RequiresApproval));
                        if (request == null)
                        {
                            transaction.Rollback();
                            return false;
                        }
                        InvoiceEntity invoice = db.Invoices.SingleOrDefault(item =>
                            item.InvoiceID == request.InvoiceID);
                        if (invoice == null)
                            throw new InvalidOperationException("Không tìm thấy hóa đơn của yêu cầu trả.");
                        if (approve)
                        {
                            if (invoice.Status != "ACTIVE")
                                throw new InvalidOperationException("Hóa đơn đã hủy; yêu cầu trả không thể được duyệt.");
                            request.Status = "APPROVED";
                            request.ApprovedBy = userId;
                            request.ApprovedAt = DateTime.Now;
                            RestoreLoyaltyPoints(db, invoice.InvoiceID, request.ReturnID,
                                invoice.CustomerID, request.PointsShare);
                        }
                        else
                        {
                            request.Status = "REJECTED";
                            request.RejectionReason = rejectionReason;
                        }
                        db.SubmitChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<NotificationDto> GetCompletedReturns(int userId)
        {
            using (var db = _connections.CreateDataContext())
            {
                var completedReturns = (from request in db.ReturnExchanges
                    join invoice in db.Invoices on request.InvoiceID equals invoice.InvoiceID
                    where request.CreatedBy == userId
                        && request.Type == "RETURN"
                        && (request.Status == "APPROVED" || request.Status == "REJECTED")
                    orderby request.ReturnID descending
                    select new
                    {
                        ReturnId = request.ReturnID,
                        invoice.InvoiceCode,
                        request.Status,
                        request.RejectionReason,
                        request.CreatedAt
                    })
                    .Take(20)
                    .ToList();

                return completedReturns.Select(item => new NotificationDto
                {
                    NotificationId = "return-result:" + item.ReturnId.ToString(CultureInfo.InvariantCulture),
                    Kind = "RETURN_RESULT",
                    Title = item.Status == "APPROVED"
                        ? "Yêu cầu trả hàng đã được duyệt"
                        : "Yêu cầu trả hàng bị từ chối",
                    Message = "Yêu cầu trả hàng TR-" +
                        item.ReturnId.ToString("D6", CultureInfo.InvariantCulture) +
                        " cho hóa đơn " + item.InvoiceCode +
                        (item.Status == "APPROVED"
                            ? " đã được duyệt."
                            : " đã bị từ chối: " + item.RejectionReason),
                    CreatedAt = item.CreatedAt,
                    IsRead = false
                }).ToList();
            }
        }

        public List<NotificationDto> GetPendingReturnNotifications(int currentUserId)
        {
            using (var db = _connections.CreateDataContext())
            {
                var pendingRequests = (from request in db.ReturnExchanges
                    join invoice in db.Invoices on request.InvoiceID equals invoice.InvoiceID
                    join sender in db.Users on request.CreatedBy equals sender.UserID
                    join role in db.Roles on sender.RoleID equals role.RoleID
                    where request.CreatedBy != currentUserId
                        && request.Type == "RETURN"
                        && request.Status == "PENDING"
                        && request.RequiresApproval
                        && role.RoleCode == "SALES_STAFF"
                    orderby request.CreatedAt descending
                    select new
                    {
                        request.ReturnID,
                        invoice.InvoiceCode,
                        sender.FullName,
                        sender.Username,
                        request.CreatedAt
                    })
                    .Take(20)
                    .ToList();

                return pendingRequests.Select(item => new NotificationDto
                {
                    NotificationId = "return-pending:" + item.ReturnID.ToString(CultureInfo.InvariantCulture),
                    Kind = "RETURN_PENDING",
                    Title = "Yêu cầu hoàn trả cần duyệt",
                    Message = (string.IsNullOrWhiteSpace(item.FullName) ? item.Username : item.FullName) +
                        " gửi yêu cầu trả hàng TR-" +
                        item.ReturnID.ToString("D6", CultureInfo.InvariantCulture) +
                        " cho hóa đơn " + item.InvoiceCode + ".",
                    CreatedAt = item.CreatedAt,
                    TargetPageKey = "returns"
                }).ToList();
            }
        }

        public List<NotificationDto> GetStockAlertNotifications()
        {
            using (SimsDataContext db = _connections.CreateDataContext())
            {
                var alerts = (from alert in db.StockAlerts
                              join product in db.Products on alert.ProductID equals product.ProductID
                              join user in db.Users on alert.ReportedBy equals user.UserID into reporters
                              from reporter in reporters.DefaultIfEmpty()
                              where alert.Status != "RESOLVED"
                              orderby alert.CreatedAt descending, alert.AlertID descending
                              select new
                              {
                                  alert.AlertID,
                                  alert.AlertType,
                                  alert.StockAtReport,
                                  alert.CreatedAt,
                                  alert.SeenByInventoryManager,
                                  product.ProductName,
                                  ReporterFullName = reporter == null ? null : reporter.FullName,
                                  ReporterUsername = reporter == null ? null : reporter.Username
                              }).Take(20).ToList();

                var notifications = new List<NotificationDto>(alerts.Count);
                foreach (var alert in alerts)
                {
                    string productName = alert.ProductName ?? "sản phẩm";
                    string reporterName = !string.IsNullOrEmpty(alert.ReporterFullName)
                        ? alert.ReporterFullName
                        : alert.ReporterUsername ?? "Hệ thống";
                    notifications.Add(new NotificationDto
                    {
                        NotificationId = "stock-alert:" +
                            alert.AlertID.ToString(CultureInfo.InvariantCulture),
                        Kind = "STOCK_ALERT",
                        Title = alert.AlertType == "OUT_OF_STOCK"
                            ? "Báo cáo hết hàng"
                            : "Báo cáo sắp hết hàng",
                        Message = reporterName + " báo \"" + productName +
                            "\" hết hàng. Tồn tại thời điểm báo: " +
                            alert.StockAtReport.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + ".",
                        CreatedAt = alert.CreatedAt,
                        IsRead = alert.SeenByInventoryManager,
                        TargetPageKey = "stock-alert"
                    });
                }
                return notifications;
            }
        }

        public List<NotificationDto> GetLowStockNotifications()
        {
            const int minimumStock = 10;
            using (SimsDataContext db = _connections.CreateDataContext())
            {
                var products = db.Products
                    .Where(product => product.Status == "ACTIVE" && product.Stock < minimumStock)
                    .OrderBy(product => product.Stock)
                    .ThenBy(product => product.ProductName)
                    .Take(20)
                    .ToList();
                var notifications = new List<NotificationDto>(products.Count);
                foreach (var product in products)
                {
                    string productCode = product.ProductCode ?? string.Empty;
                    string productName = product.ProductName ?? "sản phẩm";
                    notifications.Add(new NotificationDto
                    {
                        NotificationId = "low-stock:" +
                            product.ProductID.ToString(CultureInfo.InvariantCulture),
                        Kind = "LOW_STOCK",
                        Title = "Tồn kho dưới mức tối thiểu",
                        Message = (string.IsNullOrWhiteSpace(productCode)
                                ? productName
                                : productCode + " - " + productName) +
                            " hiện còn " +
                            product.Stock.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) +
                            " (mức tối thiểu: " +
                            minimumStock.ToString(CultureInfo.InvariantCulture) + ").",
                        CreatedAt = DateTime.Now,
                        TargetPageKey = "inventory"
                    });
                }
                return notifications;
            }
        }

        private static void RestoreLoyaltyPoints(
            SimsDataContext db, int invoiceId, int returnId, int? customerId, decimal pointsShare)
        {
            if (!customerId.HasValue || pointsShare <= 0m) return;
            decimal previousShares = db.ReturnExchanges.Where(item => item.InvoiceID == invoiceId
                    && item.Status == "APPROVED" && item.ReturnID != returnId)
                .Select(item => (decimal?)item.PointsShare).Sum() ?? 0m;
            decimal rate = CartCalculator.PointRedeemRateVnd;
            int points = decimal.ToInt32(
                decimal.Round((previousShares + pointsShare) / rate, 0, MidpointRounding.AwayFromZero) -
                decimal.Round(previousShares / rate, 0, MidpointRounding.AwayFromZero));
            if (points <= 0) return;
            CustomerEntity customer = db.Customers.SingleOrDefault(item => item.CustomerID == customerId.Value);
            if (customer == null)
                throw new InvalidOperationException("Không thể hoàn lại điểm thưởng của khách hàng.");
            customer.MemberPoint += points;
        }
    }
}

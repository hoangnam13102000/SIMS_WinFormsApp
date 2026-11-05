using System;
using System.Collections.Generic;
using System.Globalization;

namespace SIMS_WinFormsApp.Services.Implementations.Reports
{
    public static class ReportParameterBuilder
    {
        private static readonly CultureInfo Invariant = CultureInfo.InvariantCulture;

        public static IDictionary<string, string> CreateInventoryParameters(
            InventoryReportData report,
            DateTime fromDate,
            DateTime toDate,
            string categoryName,
            string preparedBy,
            DateTime? reportDate = null)
        {
            return CreateParameters(
                report == null ? null : report.BusinessName,
                fromDate,
                toDate,
                categoryName,
                preparedBy,
                report == null ? null : report.DirectorName,
                report == null ? null : report.InventoryManagerName,
                "Theo khoảng ngày",
                reportDate);
        }

        public static IDictionary<string, string> CreateFinancialParameters(
            FinancialReportSignatories signatories,
            DateTime fromDate,
            DateTime toDate,
            string preparedBy,
            string period,
            DateTime? reportDate = null)
        {
            return CreateParameters(
                signatories == null ? null : signatories.BusinessName,
                fromDate,
                toDate,
                "Tất cả danh mục",
                preparedBy,
                signatories == null ? null : signatories.DirectorName,
                signatories == null ? null : signatories.InventoryManagerName,
                period,
                reportDate);
        }

        private static IDictionary<string, string> CreateParameters(
            string businessName,
            DateTime fromDate,
            DateTime toDate,
            string categoryName,
            string preparedBy,
            string directorName,
            string inventoryManagerName,
            string period,
            DateTime? reportDate)
        {
            return new Dictionary<string, string>(StringComparer.Ordinal)
            {
                ["TenCuaHang"] = OrDefault(businessName, "Chưa cấu hình tên cửa hàng"),
                ["TuNgay"] = fromDate.Date.ToString("yyyy-MM-dd", Invariant),
                ["DenNgay"] = toDate.Date.ToString("yyyy-MM-dd", Invariant),
                ["TenDanhMuc"] = OrDefault(categoryName, "Tất cả danh mục"),
                ["NguoiLap"] = OrDefault(preparedBy, "Hệ thống"),
                ["GiamDoc"] = OrDefault(directorName, "Chưa phân công"),
                ["QuanLyKho"] = OrDefault(inventoryManagerName, "Chưa phân công"),
                ["NgayLap"] = (reportDate ?? DateTime.Today).Date.ToString("yyyy-MM-dd", Invariant),
                ["KyBaoCao"] = OrDefault(period, "Theo khoảng ngày")
            };
        }

        private static string OrDefault(string value, string fallback)
        {
            return string.IsNullOrWhiteSpace(value) ? fallback : value.Trim();
        }
    }
}

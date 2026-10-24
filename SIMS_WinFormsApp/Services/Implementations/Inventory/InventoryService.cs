using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.Services.Interfaces;

namespace SIMS_WinFormsApp.Services.Implementations.Inventory
{
    public sealed class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repository;

        public InventoryService(IInventoryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public IReadOnlyList<LookupItem> GetActiveSuppliers() => _repository.GetActiveSuppliers();

        public IReadOnlyList<LookupItem> GetActiveProducts() => _repository.GetActiveProducts();

        public IReadOnlyList<LookupItem> GetCategories() => _repository.GetCategories();

        public IReadOnlyList<StockRowDto> GetStock(string keyword, int? categoryId) =>
            _repository.GetStock(keyword, categoryId);

        public CatalogSaveResult CreatePurchaseReceipt(PurchaseReceiptDto receipt)
        {
            if (receipt == null)
                return CatalogSaveResult.Fail("Thông tin phiếu nhập không hợp lệ.");
            if (receipt.SupplierId <= 0)
                return CatalogSaveResult.Fail("Vui lòng chọn nhà cung cấp.");
            if (receipt.Lines == null || receipt.Lines.Count == 0)
                return CatalogSaveResult.Fail("Phiếu nhập phải có ít nhất 1 sản phẩm.");

            foreach (PurchaseLineDto line in receipt.Lines)
            {
                if (line == null || line.ProductId <= 0)
                    return CatalogSaveResult.Fail("Vui lòng chọn sản phẩm cho tất cả các dòng nhập.");
                if (line.Quantity <= 0)
                    return CatalogSaveResult.Fail("Số lượng nhập phải lớn hơn 0.");
                if (line.ImportPrice < 0)
                    return CatalogSaveResult.Fail("Giá nhập không được âm.");
                if (line.ExpiryDate.HasValue && line.ExpiryDate.Value.Date <= DateTime.Today)
                    return CatalogSaveResult.Fail("Hạn sử dụng phải sau ngày hôm nay.");
                if (line.ExpiryDate.HasValue && line.ManufactureDate.HasValue &&
                    line.ExpiryDate.Value.Date <= line.ManufactureDate.Value.Date)
                    return CatalogSaveResult.Fail("Hạn sử dụng phải sau ngày sản xuất.");
            }

            try
            {
                string receiptCode;
                int id = _repository.CreatePurchaseReceipt(receipt, out receiptCode);
                return CatalogSaveResult.Ok(id, "Đã lưu phiếu nhập " + receiptCode);
            }
            catch (SqlException ex)
            {
                return CatalogSaveResult.Fail(DescribeSqlException(ex,
                    "Không thể lưu phiếu nhập do lỗi cơ sở dữ liệu."));
            }
            catch (Exception)
            {
                return CatalogSaveResult.Fail("Không thể lưu phiếu nhập. Vui lòng thử lại.");
            }
        }

        public CatalogSaveResult SaveStockCounts(IReadOnlyList<StockCountDto> counts, int createdBy)
        {
            if (counts == null || counts.Count == 0)
                return CatalogSaveResult.Fail("Chưa nhập số lượng thực tế cho sản phẩm nào.");

            foreach (StockCountDto count in counts)
            {
                if (count == null)
                    return CatalogSaveResult.Fail("Dữ liệu kiểm kê không hợp lệ.");
                if (count.ActualStock < 0)
                    return CatalogSaveResult.Fail("Số lượng thực tế không được âm.");
                if (count.ActualStock != count.SystemStock && string.IsNullOrWhiteSpace(count.Note))
                    return CatalogSaveResult.Fail(
                        "Vui lòng ghi lý do chênh lệch cho sản phẩm có mã ID " + count.ProductId + ".");
            }

            try
            {
                _repository.SaveStockCounts(counts, createdBy);
                return CatalogSaveResult.Ok(0,
                    "Đã ghi nhận kiểm kê " + counts.Count + " sản phẩm.");
            }
            catch (SqlException ex)
            {
                return CatalogSaveResult.Fail(DescribeSqlException(ex,
                    "Không thể ghi nhận kiểm kê do lỗi cơ sở dữ liệu."));
            }
            catch (Exception)
            {
                return CatalogSaveResult.Fail("Không thể ghi nhận kiểm kê. Vui lòng thử lại.");
            }
        }

        private static string DescribeSqlException(SqlException exception, string fallbackMessage)
        {
            if (exception.Number == 2627 || exception.Number == 2601)
                return "Dữ liệu bị trùng với bản ghi đã có.";
            if (exception.Number == 547)
                return "Dữ liệu không hợp lệ hoặc vi phạm ràng buộc cơ sở dữ liệu.";
            if (exception.Number == 1205)
                return "Giao dịch đang bị xung đột. Vui lòng thử lại.";
            return fallbackMessage;
        }
    }
}
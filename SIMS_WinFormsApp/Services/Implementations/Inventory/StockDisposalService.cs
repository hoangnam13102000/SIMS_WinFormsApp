using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.Services.Interfaces;

namespace SIMS_WinFormsApp.Services.Implementations.Inventory
{
    public sealed class StockDisposalService : IStockDisposalService
    {
        private const int MaxNoteLength = 500;
        private readonly IStockDisposalRepository _repository;

        public StockDisposalService(IStockDisposalRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public IReadOnlyList<StockDisposalListItemDto> SearchDisposals(string keyword, DateTime? fromDate, DateTime? toDate)
        {
            string trimmed = (keyword ?? string.Empty).Trim();
            var matchedReasons = new List<string>();
            if (trimmed.Length > 0)
            {
                foreach (string code in DisposalReasons.Codes)
                {
                    if (DisposalReasons.ToLabel(code).IndexOf(trimmed, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                        code.IndexOf(trimmed, StringComparison.OrdinalIgnoreCase) >= 0)
                        matchedReasons.Add(code);
                }
            }
            return _repository.SearchDisposals(trimmed, string.Join(",", matchedReasons), fromDate, toDate);
        }

        public IReadOnlyList<StockDisposalDetailLineDto> GetDisposalDetails(int disposalId) =>
            _repository.GetDisposalDetails(disposalId);

        public IReadOnlyList<DisposableBatchDto> GetDisposableBatches() =>
            _repository.GetDisposableBatches();

        public CatalogSaveResult CreateDisposal(StockDisposalDto disposal)
        {
            if (disposal == null)
                return CatalogSaveResult.Fail("Thông tin phiếu tiêu hủy không hợp lệ.");

            string reason = (disposal.Reason ?? string.Empty).Trim().ToUpperInvariant();
            if (Array.IndexOf(DisposalReasons.Codes, reason) < 0)
                return CatalogSaveResult.Fail("Vui lòng chọn lý do tiêu hủy.");
            if (disposal.CreatedBy <= 0)
                return CatalogSaveResult.Fail("Phiên đăng nhập không hợp lệ. Vui lòng đăng nhập lại.");
            if (disposal.Lines == null || disposal.Lines.Count == 0)
                return CatalogSaveResult.Fail("Thêm ít nhất 1 lô cần tiêu hủy trước khi lưu phiếu.");
            if (disposal.Note != null && disposal.Note.Trim().Length > MaxNoteLength)
                return CatalogSaveResult.Fail("Ghi chú tối đa " + MaxNoteLength + " ký tự.");

            var seenBatches = new HashSet<int>();
            foreach (StockDisposalLineDto line in disposal.Lines)
            {
                if (line == null || line.BatchId <= 0)
                    return CatalogSaveResult.Fail("Vui lòng chọn lô hàng cho tất cả các dòng.");
                if (line.Quantity <= 0)
                    return CatalogSaveResult.Fail("Số lượng hủy phải là số nguyên lớn hơn 0.");
                if (!seenBatches.Add(line.BatchId))
                    return CatalogSaveResult.Fail("Lô này đã có trên phiếu. Xóa dòng cũ nếu muốn sửa SL.");
            }

            disposal.Reason = reason;
            try
            {
                string disposalCode;
                int id = _repository.CreateDisposal(disposal, out disposalCode);
                return CatalogSaveResult.Ok(id, "Đã lập phiếu " + disposalCode);
            }
            catch (InvalidOperationException ex)
            {
                return CatalogSaveResult.Fail(ex.Message);
            }
            catch (SqlException ex)
            {
                return CatalogSaveResult.Fail(DescribeSqlException(ex,
                    "Không thể lập phiếu tiêu hủy do lỗi cơ sở dữ liệu."));
            }
            catch (Exception)
            {
                return CatalogSaveResult.Fail("Không thể lập phiếu tiêu hủy. Vui lòng thử lại.");
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

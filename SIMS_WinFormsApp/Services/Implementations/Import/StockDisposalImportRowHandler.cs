using System;
using System.Collections.Generic;
using System.Globalization;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.Services.Interfaces;

namespace SIMS_WinFormsApp.Services.Implementations.Import
{
    /// <summary>Mỗi dòng trong file tạo 1 phiếu tiêu hủy gồm 1 lô, lưu qua CreateDisposal như form Lập phiếu tiêu hủy.</summary>
    public sealed class StockDisposalImportRowHandler
    {
        public static readonly string[] ExpectedColumns = { "Mã lô", "Số lượng", "Lý do", "Ghi chú" };

        private readonly IStockDisposalService _service;
        private readonly int _userId;
        private readonly Dictionary<string, DisposableBatchDto> _batches =
            new Dictionary<string, DisposableBatchDto>(StringComparer.OrdinalIgnoreCase);

        public StockDisposalImportRowHandler(IStockDisposalService service, int userId)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _userId = userId;
            foreach (DisposableBatchDto batch in _service.GetDisposableBatches() ?? new List<DisposableBatchDto>())
            {
                if (!string.IsNullOrWhiteSpace(batch.BatchCode))
                    _batches[batch.BatchCode.Trim()] = batch;
            }
        }

        public ImportRowResult Handle(string[] cells, int rowNumber)
        {
            string batchCode = Cell(cells, 0);
            string quantityText = Cell(cells, 1).Replace(".", string.Empty).Replace(",", string.Empty);
            string reason = ToReasonCode(Cell(cells, 2));
            string note = Cell(cells, 3);

            if (batchCode.Length == 0)
                return ImportRowResult.Failure("Dòng " + rowNumber + ": thiếu mã lô.");
            DisposableBatchDto batch;
            if (!_batches.TryGetValue(batchCode, out batch))
                return ImportRowResult.Failure("Dòng " + rowNumber + ": lô " + batchCode + " không có hoặc đã hết hàng.");
            int quantity;
            if (!int.TryParse(quantityText, NumberStyles.None, CultureInfo.InvariantCulture, out quantity) || quantity <= 0)
                return ImportRowResult.Failure("Dòng " + rowNumber + ": số lượng phải là số nguyên lớn hơn 0.");
            if (quantity > batch.RemainingQty)
                return ImportRowResult.Failure("Dòng " + rowNumber + ": lô " + batchCode + " chỉ còn " + batch.RemainingQty + ".");
            if (reason == null)
                return ImportRowResult.Failure("Dòng " + rowNumber + ": lý do phải là Hết hạn, Hỏng / hư hỏng, Chất lượng hoặc Khác.");

            var disposal = new StockDisposalDto
            {
                Reason = reason,
                Note = note.Length == 0 ? null : note,
                CreatedBy = _userId
            };
            disposal.Lines.Add(new StockDisposalLineDto { BatchId = batch.BatchId, Quantity = quantity });

            CatalogSaveResult result = _service.CreateDisposal(disposal);
            if (result == null || !result.Success)
                return ImportRowResult.Failure("Dòng " + rowNumber + ": "
                    + (result == null ? "không thể lưu phiếu." : result.Message));

            batch.RemainingQty -= quantity;
            return ImportRowResult.Success();
        }

        // Nhận cả chữ trên form ("Hết hạn") lẫn mã ("EXPIRED"); để trống thì là Khác.
        private static string ToReasonCode(string text)
        {
            string value = (text ?? string.Empty).Trim();
            if (value.Length == 0)
                return DisposalReasons.Other;
            foreach (string code in DisposalReasons.Codes)
            {
                if (string.Equals(code, value, StringComparison.OrdinalIgnoreCase)
                    || string.Equals(DisposalReasons.ToLabel(code), value, StringComparison.OrdinalIgnoreCase))
                    return code;
            }
            if (value.StartsWith("Hỏng", StringComparison.OrdinalIgnoreCase)
                || value.StartsWith("Hư", StringComparison.OrdinalIgnoreCase))
                return DisposalReasons.Damaged;
            return null;
        }

        private static string Cell(string[] cells, int index)
        {
            return cells != null && index < cells.Length && cells[index] != null ? cells[index].Trim() : string.Empty;
        }
    }
}
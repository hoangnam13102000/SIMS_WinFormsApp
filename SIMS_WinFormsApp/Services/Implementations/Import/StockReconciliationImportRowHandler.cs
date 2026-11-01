using System;
using System.Collections.Generic;
using System.Globalization;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.Services.Interfaces;

namespace SIMS_WinFormsApp.Services.Implementations.Import
{
    /// <summary>Mỗi dòng trong file cập nhật Tồn thực tế / Lý do của 1 sản phẩm trong phiên đối chiếu hôm nay.</summary>
    public sealed class StockReconciliationImportRowHandler
    {
        public static readonly string[] ExpectedColumns = { "Mã SP", "Tồn thực tế", "Lý do" };

        private readonly IStockReconciliationService _service;
        private readonly int _userId;

        public StockReconciliationImportRowHandler(IStockReconciliationService service, int userId)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _userId = userId;
        }

        public ImportRowResult Handle(string[] cells, int rowNumber)
        {
            string productCode = Cell(cells, 0);
            string actualText = Cell(cells, 1).Replace(".", string.Empty).Replace(",", string.Empty);
            string note = Cell(cells, 2);

            if (productCode.Length == 0)
                return ImportRowResult.Failure("Dòng " + rowNumber + ": thiếu mã SP.");
            int actual;
            if (!int.TryParse(actualText, NumberStyles.None, CultureInfo.InvariantCulture, out actual))
                return ImportRowResult.Failure("Dòng " + rowNumber + ": tồn thực tế phải là số nguyên ≥ 0.");

            StockReconciliationRowDto row = FindTodayRow(productCode);
            if (row == null)
                return ImportRowResult.Failure("Dòng " + rowNumber + ": " + productCode
                    + " không có trong phiên đối chiếu hôm nay.");

            CatalogSaveResult result = _service.UpdateCount(
                row.ReconciliationId, actual, note.Length == 0 ? row.Note : note, _userId);
            return result != null && result.Success
                ? ImportRowResult.Success()
                : ImportRowResult.Failure("Dòng " + rowNumber + ": "
                    + (result == null ? "không thể cập nhật." : result.Message));
        }

        private StockReconciliationRowDto FindTodayRow(string productCode)
        {
            IReadOnlyList<StockReconciliationRowDto> rows = _service.Search(productCode, DateTime.Today, DateTime.Today);
            foreach (StockReconciliationRowDto row in rows ?? new List<StockReconciliationRowDto>())
            {
                if (string.Equals(row.ProductCode, productCode, StringComparison.OrdinalIgnoreCase))
                    return row;
            }
            return null;
        }

        private static string Cell(string[] cells, int index)
        {
            return cells != null && index < cells.Length && cells[index] != null ? cells[index].Trim() : string.Empty;
        }
    }
}
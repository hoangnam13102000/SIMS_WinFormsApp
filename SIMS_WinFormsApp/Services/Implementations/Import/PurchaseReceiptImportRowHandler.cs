using System;
using System.Collections.Generic;
using System.Globalization;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Session;

namespace SIMS_WinFormsApp.Services.Implementations.Import
{
    /// <summary>Mỗi dòng trong file tạo 1 phiếu nhập gồm 1 sản phẩm, lưu qua CreatePurchaseReceipt như form Lập phiếu nhập.</summary>
    public sealed class PurchaseReceiptImportRowHandler
    {
        public static readonly string[] ExpectedColumns =
        {
            "Nhà cung cấp", "Sản phẩm", "Số lượng", "Giá nhập", "Số lô", "Ngày sản xuất", "Hạn sử dụng"
        };

        private static readonly string[] DateFormats =
        {
            "dd/MM/yyyy", "d/M/yyyy", "dd-MM-yyyy", "d-M-yyyy", "yyyy-MM-dd",
            "dd/MM/yyyy HH:mm:ss", "d/M/yyyy H:mm:ss"
        };

        private readonly IInventoryService _service;
        private readonly IUserSession _session;
        private readonly IReadOnlyList<LookupItem> _suppliers;
        private readonly IReadOnlyList<LookupItem> _products;

        public PurchaseReceiptImportRowHandler(IInventoryService service, IUserSession session)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _session = session ?? throw new ArgumentNullException(nameof(session));
            _suppliers = _service.GetActiveSuppliers() ?? Array.Empty<LookupItem>();
            _products = _service.GetActiveProducts() ?? Array.Empty<LookupItem>();
        }

        public ImportRowResult Handle(string[] cells, int rowNumber)
        {
            if (_session.CurrentUser == null)
                return Fail(rowNumber, "phiên đăng nhập không hợp lệ.");

            string supplierText = Cell(cells, 0);
            string productText = Cell(cells, 1);
            if (supplierText.Length == 0) return Fail(rowNumber, "thiếu nhà cung cấp.");
            if (productText.Length == 0) return Fail(rowNumber, "thiếu sản phẩm.");

            int supplierId = FindSupplier(supplierText);
            if (supplierId <= 0)
                return Fail(rowNumber, "không tìm thấy nhà cung cấp \"" + supplierText + "\".");
            int productId = FindProduct(productText);
            if (productId <= 0)
                return Fail(rowNumber, "không tìm thấy sản phẩm \"" + productText + "\" hoặc sản phẩm đã ngừng bán.");

            int quantity = CatalogImportParsing.ParseInt(Cell(cells, 2), 0);
            if (quantity <= 0)
                return Fail(rowNumber, "số lượng phải là số nguyên lớn hơn 0.");
            decimal importPrice;
            if (!CatalogImportParsing.TryMoney(Cell(cells, 3), out importPrice))
                return Fail(rowNumber, "giá nhập không hợp lệ.");

            DateTime? manufactureDate;
            if (!TryDate(Cell(cells, 5), out manufactureDate))
                return Fail(rowNumber, "ngày sản xuất không đúng dạng dd/MM/yyyy.");
            DateTime? expiryDate;
            if (!TryDate(Cell(cells, 6), out expiryDate))
                return Fail(rowNumber, "hạn sử dụng không đúng dạng dd/MM/yyyy.");

            string lotNumber = Cell(cells, 4);
            var receipt = new PurchaseReceiptDto
            {
                SupplierId = supplierId,
                CreatedBy = _session.CurrentUser.UserId,
                Lines = new List<PurchaseLineDto>
                {
                    new PurchaseLineDto
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        ImportPrice = importPrice,
                        LotNumber = lotNumber.Length == 0 ? null : lotNumber,
                        ManufactureDate = manufactureDate,
                        ExpiryDate = expiryDate
                    }
                }
            };

            CatalogSaveResult result = _service.CreatePurchaseReceipt(receipt);
            if (result == null) return Fail(rowNumber, "không lưu được phiếu nhập.");
            return result.Success ? ImportRowResult.Success() : Fail(rowNumber, result.Message);
        }

        private int FindSupplier(string text)
        {
            foreach (LookupItem item in _suppliers)
            {
                if (string.Equals((item.Name ?? string.Empty).Trim(), text, StringComparison.OrdinalIgnoreCase))
                    return item.Id;
            }
            return 0;
        }

        // GetActiveProducts trả về Name dạng "MãSP - Tên sản phẩm": cho phép ghi mã hoặc tên.
        private int FindProduct(string text)
        {
            foreach (LookupItem item in _products)
            {
                string display = (item.Name ?? string.Empty).Trim();
                string code = display;
                string name = display;
                int separator = display.IndexOf(" - ", StringComparison.Ordinal);
                if (separator > 0)
                {
                    code = display.Substring(0, separator).Trim();
                    name = display.Substring(separator + 3).Trim();
                }
                if (string.Equals(code, text, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(name, text, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(display, text, StringComparison.OrdinalIgnoreCase))
                    return item.Id;
            }
            return 0;
        }

        private static bool TryDate(string text, out DateTime? value)
        {
            value = null;
            if (string.IsNullOrWhiteSpace(text)) return true;
            DateTime parsed;
            if (!DateTime.TryParseExact(text.Trim(), DateFormats, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out parsed))
                return false;
            value = parsed.Date;
            return true;
        }

        private static ImportRowResult Fail(int rowNumber, string message) =>
            ImportRowResult.Failure("Dòng " + rowNumber + ": " + message);

        private static string Cell(string[] cells, int index) =>
            cells != null && index >= 0 && index < cells.Length && cells[index] != null ? cells[index].Trim() : string.Empty;
    }
}

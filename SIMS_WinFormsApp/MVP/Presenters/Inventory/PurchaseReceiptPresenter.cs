using System;
using System.Collections.Generic;
using System.Linq;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.MVP.Presenters.Inventory
{
    public sealed class PurchaseReceiptPresenter
    {
        private const string ReceiptMessagePrefix = "Đã lưu phiếu nhập ";
        private readonly IPurchaseReceiptView _view;
        private readonly IInventoryService _service;
        private readonly IUserSession _session;

        public PurchaseReceiptPresenter(
            IPurchaseReceiptView view,
            IInventoryService service,
            IUserSession session)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _session = session ?? throw new ArgumentNullException(nameof(session));
            _view.SaveRequested += OnSaveRequested;
            _view.LinesChanged += OnLinesChanged;
        }

        public void Initialize()
        {
            _view.BindSuppliers(_service.GetActiveSuppliers());
            _view.BindProducts(_service.GetActiveProducts());
            _view.ShowTotal(0m);
        }

        private void OnLinesChanged(object sender, EventArgs e)
        {
            IReadOnlyList<PurchaseLineDto> lines = _view.ReadLines();
            decimal total = lines == null ? 0m : lines.Sum(line => line.Amount);
            _view.ShowTotal(total);
        }

        private void OnSaveRequested(object sender, EventArgs e)
        {
            if (_session.CurrentUser == null)
            {
                _view.ShowMessage("Phiên đăng nhập không hợp lệ. Vui lòng đăng nhập lại.", true);
                return;
            }

            var receipt = new PurchaseReceiptDto
            {
                SupplierId = _view.SelectedSupplierId,
                CreatedBy = _session.CurrentUser.UserId,
                Lines = new List<PurchaseLineDto>(_view.ReadLines() ?? Array.Empty<PurchaseLineDto>())
            };

            CatalogSaveResult result = _service.CreatePurchaseReceipt(receipt);
            if (result == null || !result.Success)
            {
                _view.ShowMessage(result == null ? "Không lưu được phiếu nhập." : result.Message, true);
                return;
            }

            _view.ShowMessage(result.Message, false);
            _view.ShowReceiptCode(ExtractReceiptCode(result.Message));
            _view.ResetLines();
            _view.ShowTotal(0m);
        }

        private static string ExtractReceiptCode(string message)
        {
            if (string.IsNullOrWhiteSpace(message) ||
                !message.StartsWith(ReceiptMessagePrefix, StringComparison.Ordinal))
                return message;

            return message.Substring(ReceiptMessagePrefix.Length);
        }
    }
}
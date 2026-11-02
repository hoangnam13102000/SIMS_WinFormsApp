using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.MVP.Presenters.Inventory
{
    public sealed class StockDisposalEditorPresenter
    {
        private static readonly CultureInfo VietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");
        private readonly IStockDisposalEditorView _view;
        private readonly IStockDisposalService _service;
        private readonly IUserSession _session;
        private List<DisposableBatchDto> _batches = new List<DisposableBatchDto>();

        public StockDisposalEditorPresenter(
            IStockDisposalEditorView view, IStockDisposalService service, IUserSession session)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _session = session ?? throw new ArgumentNullException(nameof(session));

            _view.LinesChanged += OnLinesChanged;
            _view.SaveRequested += OnSaveRequested;
        }

        public void Initialize()
        {
            try
            {
                IReadOnlyList<DisposableBatchDto> batches = _service.GetDisposableBatches();
                _batches = batches == null ? new List<DisposableBatchDto>() : batches.ToList();
            }
            catch (Exception exception)
            {
                _batches = new List<DisposableBatchDto>();
                _view.ShowMessage("Không tải được danh sách lô hàng: " + exception.Message, true);
            }

            _view.BindBatches(_batches);
            _view.ShowTotal(0m);
        }

        private void OnLinesChanged(object sender, EventArgs e)
        {
            _view.ShowTotal(CalculateTotal(_view.ReadLines()));
        }

        private void OnSaveRequested(object sender, EventArgs e)
        {
            if (_session.CurrentUser == null)
            {
                _view.ShowMessage("Phiên đăng nhập không hợp lệ. Vui lòng đăng nhập lại.", true);
                return;
            }

            List<StockDisposalLineDto> lines = (_view.ReadLines() ?? Array.Empty<StockDisposalLineDto>()).ToList();
            if (lines.Count == 0)
            {
                _view.ShowMessage("Thêm ít nhất 1 lô cần tiêu hủy trước khi lưu phiếu.", true);
                return;
            }

            decimal total = CalculateTotal(lines);
            if (!_view.ConfirmSave(lines.Count, total))
                return;

            CatalogSaveResult result = _service.CreateDisposal(new StockDisposalDto
            {
                Reason = _view.SelectedReason,
                Note = _view.Note,
                CreatedBy = _session.CurrentUser.UserId,
                Lines = lines
            });
            if (result == null || !result.Success)
            {
                _view.ShowMessage(result == null
                    ? "Không thể lập phiếu. Kiểm tra tồn lô / kết nối DB."
                    : result.Message, true);
                return;
            }

            _view.CloseAfterSave(result.Message + " (" + lines.Count + " dòng). Tổn thất: "
                + total.ToString("N0", VietnameseCulture) + " đ");
        }

        private decimal CalculateTotal(IEnumerable<StockDisposalLineDto> lines)
        {
            if (lines == null) return 0m;
            decimal total = 0m;
            foreach (StockDisposalLineDto line in lines)
            {
                DisposableBatchDto batch = _batches.FirstOrDefault(item => item.BatchId == line.BatchId);
                if (batch != null) total += batch.ImportPrice * line.Quantity;
            }
            return total;
        }
    }
}

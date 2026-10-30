using System;
using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.MVP.Presenters.Inventory
{
    public sealed class StockReconciliationPresenter
    {
        private readonly IStockReconciliationView _view;
        private readonly IInventoryService _service;
        private readonly IUserSession _session;

        public StockReconciliationPresenter(
            IStockReconciliationView view,
            IInventoryService service,
            IUserSession session)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _session = session ?? throw new ArgumentNullException(nameof(session));
            _view.LoadRequested += OnLoadRequested;
            _view.SaveRequested += OnSaveRequested;
        }

        public void Initialize()
        {
            _view.BindCategories(_service.GetCategories());
            OnLoadRequested(this, EventArgs.Empty);
        }

        private void OnLoadRequested(object sender, EventArgs e)
        {
            _view.BindStock(_service.GetStock(_view.Keyword, _view.SelectedCategoryId));
        }

        private void OnSaveRequested(object sender, EventArgs e)
        {
            IReadOnlyList<StockCountDto> counts = _view.ReadCounts();
            if (counts == null) counts = Array.Empty<StockCountDto>();

            if (counts.Count > 0 && !_view.Confirm(
                "Ghi nhận kiểm kê " + counts.Count + " sản phẩm? Tồn kho sẽ được cập nhật theo số thực tế."))
                return;

            var currentUser = _session.CurrentUser;
            if (counts.Count > 0 && currentUser == null)
            {
                _view.ShowMessage("Phiên đăng nhập không hợp lệ. Vui lòng đăng nhập lại.", true);
                return;
            }

            CatalogSaveResult result = _service.SaveStockCounts(
                counts, currentUser == null ? 0 : currentUser.UserId);
            if (result == null)
            {
                _view.ShowMessage("Không thể lưu kết quả kiểm kê.", true);
                return;
            }

            _view.ShowMessage(result.Message, !result.Success);
            if (result.Success)
                OnLoadRequested(this, EventArgs.Empty);
        }
    }
}
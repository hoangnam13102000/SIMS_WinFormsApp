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
    }
}
using System.Windows.Forms;
using System;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.MVP.Presenters.Pos;

namespace SIMS_WinFormsApp.UI.Controls.Pos
{
    public static class PosPage
    {
        public static Control Create(
            string cashierDisplayName,
            Action invoiceCreated = null,
            Action<string> invoiceViewed = null)
        {
            var view = new PosPageControl(cashierDisplayName);

            var presenter = new PosPresenter(
                view,
                AppComposition.CreateProductCatalogService(),
                AppComposition.CreateCustomerLookupService(),
                AppComposition.CreateInvoiceService(),
                AppComposition.CreatePromotionService(),
                AppComposition.CreateBarcodeScannerLauncher(),
                new HeldCartStore(),
                () => view.FindForm() ?? (IWin32Window)view,
                SIMS_WinFormsApp.Services.Session.UserSession.Instance,
                invoiceCreated,
                invoiceViewed);

            view.Disposed += (s, e) => presenter.Dispose();

            return view;
        }
    }
}
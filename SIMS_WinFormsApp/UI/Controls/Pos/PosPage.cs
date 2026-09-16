using System.Windows.Forms;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.MVP.Presenters.Pos;

namespace SIMS_WinFormsApp.UI.Controls.Pos
{
    public static class PosPage
    {
        public static Control Create(string cashierDisplayName)
        {
            var view = new PosPageControl(cashierDisplayName);

            var presenter = new PosPresenter(
                view,
                AppComposition.CreateProductCatalogService(),
                AppComposition.CreateCustomerLookupService(),
                AppComposition.CreatePromotionService(),
                AppComposition.CreateBarcodeScannerLauncher(),
                new HeldCartStore(),
                () => view.FindForm() ?? (IWin32Window)view);

            view.Disposed += (s, e) => presenter.Dispose();

            return view;
        }
    }
}
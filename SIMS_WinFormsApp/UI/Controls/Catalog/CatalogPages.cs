using System.Windows.Forms;
using SIMS_WinFormsApp.Views.Catalog;
using SIMS_WinFormsApp.Infrastructure.Composition;

namespace SIMS_WinFormsApp.UI.Controls.Catalog
{
    public static class CatalogPages
    {
        public static Control Products() =>
            new ucProductManagement(AppComposition.CreateCatalogAdminService());

        public static Control Categories() =>
            new ucCategoryManagement(AppComposition.CreateCatalogAdminService());

        public static Control Suppliers() =>
            new ucSupplierManagement(AppComposition.CreateCatalogAdminService());
    }
}

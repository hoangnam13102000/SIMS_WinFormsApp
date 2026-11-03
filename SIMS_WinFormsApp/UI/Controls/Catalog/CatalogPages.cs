using System.Windows.Forms;
using System;
using SIMS_WinFormsApp.Views.Catalog;
using SIMS_WinFormsApp.Infrastructure.Composition;

namespace SIMS_WinFormsApp.UI.Controls.Catalog
{
    public static class CatalogPages
    {
        public static Control Products(
            bool canEdit,
            bool canManage,
            Action<int> showExpiredBatches = null) =>
            new ucProductManagement(
                AppComposition.CreateCatalogAdminService(), canEdit, canManage, showExpiredBatches);

        public static Control Categories() =>
            new ucCategoryManagement(AppComposition.CreateCatalogAdminService());

        public static Control Categories(bool canEdit, bool canManage) =>
            new ucCategoryManagement(AppComposition.CreateCatalogAdminService(), canEdit, canManage);

        public static Control Suppliers() =>
            new ucSupplierManagement(AppComposition.CreateCatalogAdminService());
    }
}

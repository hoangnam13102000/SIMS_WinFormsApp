using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Forms.Catalog;
using SIMS_WinFormsApp.Forms.SystemMgmt;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.MVP.Presenters.Catalog;
using SIMS_WinFormsApp.Services.Implementations.Export;
using SIMS_WinFormsApp.Services.Implementations.Import;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Controls.Filter;
using SIMS_WinFormsApp.UI.Controls.Loading;
using SIMS_WinFormsApp.UI.Controls.Toast;

namespace SIMS_WinFormsApp.UI.Controls.Catalog
{
    /// <summary>
    /// View factory for product, category and supplier management. Each page reuses
    /// <see cref="BaseTable"/> and leaves loading, validation and persistence to its presenter.
    /// </summary>
    public static class CatalogPages
    {
        public static Control Products() => CreateProducts(AppComposition.CreateCatalogAdminService());

        public static Control Categories() => CreateCategories(AppComposition.CreateCatalogAdminService());

        public static Control Suppliers() => CreateSuppliers(AppComposition.CreateCatalogAdminService());

        private static Control CreateProducts(ICatalogAdminService service)
        {
            var presenter = new ProductManagementPresenter(service);
            LoadingOverlayHost host = null;
            BaseTable table = null;
            var importer = new ProductImportRowHandler(service);
            string[] exportHeaders = { "Mã", "Tên sản phẩm", "Danh mục", "Giá bán", "Tồn kho", "Trạng thái" };

            table = new BaseTable(
                "Quản lý sản phẩm",
                "Danh sách sản phẩm, giá bán, tồn kho và hình ảnh",
                IconChar.Box,
                new[] { "Ảnh", "Mã", "Tên sản phẩm", "Danh mục", "Giá bán", "Tồn kho", "Trạng thái", "Thao tác" },
                (pageIndex, pageSize, search, status) => Load(host, () => presenter.Load(pageIndex, pageSize, search, status)),
                StatusOptions(),
                addButtonText: "+ Thêm sản phẩm",
                overflowActions: ProductOverflow(presenter, importer, exportHeaders, () => table),
                lockFollowsInactiveStatus: true,
                searchPlaceholder: "Tìm theo tên, mã, danh mục...");

            table.AddButtonClicked += (sender, e) => EditProduct(table, presenter, presenter.NewDraft(), false);
            table.ActionButtonClicked += (sender, e) => OnProductAction(table, presenter, e);
            host = new LoadingOverlayHost(table);
            return host;
        }

        private static Control CreateCategories(ICatalogAdminService service)
        {
            var presenter = new CategoryManagementPresenter(service);
            LoadingOverlayHost host = null;
            BaseTable table = null;
            var importer = new CategoryImportRowHandler(service);
            string[] exportHeaders = { "Tên danh mục", "Số sản phẩm", "Trạng thái" };

            table = new BaseTable(
                "Quản lý danh mục",
                "Nhóm sản phẩm dùng khi bán hàng và nhập kho",
                IconChar.Tags,
                new[] { "Tên danh mục", "Số sản phẩm", "Trạng thái", "Thao tác" },
                (pageIndex, pageSize, search, status) => Load(host, () => presenter.Load(pageIndex, pageSize, search, status)),
                StatusOptions(),
                addButtonText: "+ Thêm danh mục",
                overflowActions: CategoryOverflow(presenter, importer, exportHeaders, () => table),
                lockFollowsInactiveStatus: true,
                searchPlaceholder: "Tìm theo tên danh mục...");

            table.AddButtonClicked += (sender, e) => EditCategory(table, presenter, presenter.NewDraft(), false);
            table.ActionButtonClicked += (sender, e) => OnCategoryAction(table, presenter, e);
            host = new LoadingOverlayHost(table);
            return host;
        }

        private static Control CreateSuppliers(ICatalogAdminService service)
        {
            var presenter = new SupplierManagementPresenter(service);
            LoadingOverlayHost host = null;
            BaseTable table = null;
            var importer = new SupplierImportRowHandler(service);
            string[] exportHeaders = { "Tên nhà cung cấp", "Số điện thoại", "Email", "Địa chỉ", "Mặt hàng cung cấp", "Công nợ" };

            table = new BaseTable(
                "Quản lý nhà cung cấp",
                "Đối tác nhập hàng, liên hệ và công nợ",
                IconChar.Building,
                new[] { "Tên nhà cung cấp", "Số điện thoại", "Email", "Địa chỉ", "Công nợ", "Thao tác" },
                (pageIndex, pageSize, search, debt) => Load(host, () => presenter.Load(pageIndex, pageSize, search, debt)),
                new[]
                {
                    new FilterOption("Tất cả nhà cung cấp", null),
                    new FilterOption("Còn công nợ", "DEBT")
                },
                addButtonText: "+ Thêm nhà cung cấp",
                overflowActions: SupplierOverflow(presenter, importer, exportHeaders, () => table),
                trailingActionIsDelete: true,
                searchPlaceholder: "Tìm theo tên, điện thoại, email...");

            table.AddButtonClicked += (sender, e) => EditSupplier(table, presenter, presenter.NewDraft(), false);
            table.ActionButtonClicked += (sender, e) => OnSupplierAction(table, presenter, e);
            host = new LoadingOverlayHost(table);
            return host;
        }

        private static IList<OverflowMenuAction> ProductOverflow(
            ProductManagementPresenter presenter,
            ProductImportRowHandler importer,
            string[] exportHeaders,
            Func<BaseTable> getTable)
        {
            return new List<OverflowMenuAction>
            {
                new OverflowMenuAction("Xuất CSV", IconChar.FileCsv, () =>
                    TableExportRunner.Run(getTable().FindForm(), new CsvTableExporter(), "Sản phẩm",
                        () => exportHeaders, presenter.ExportRows)),
                new OverflowMenuAction("Xuất Excel", IconChar.FileExcel, () =>
                    TableExportRunner.Run(getTable().FindForm(), new ExcelTableExporter(), "Sản phẩm",
                        () => exportHeaders, presenter.ExportRows)),
                new OverflowMenuAction("Nhập dữ liệu", IconChar.Upload, () =>
                {
                    var table = getTable();
                    var result = frmImportData.Show(
                        table.FindForm(),
                        "Sản phẩm",
                        ProductImportRowHandler.ExpectedColumns,
                        "Giá nhập và giá bán là số. Ảnh là đường dẫn file trên máy, có thể để trống. Danh mục và nhà cung cấp chưa có sẽ được tạo theo tên.",
                        DefaultSpreadsheetImporters.All,
                        importer.Handle);
                    if (result == DialogResult.OK) table.Reload();
                })
            };
        }

        private static IList<OverflowMenuAction> CategoryOverflow(
            CategoryManagementPresenter presenter,
            CategoryImportRowHandler importer,
            string[] exportHeaders,
            Func<BaseTable> getTable)
        {
            return new List<OverflowMenuAction>
            {
                new OverflowMenuAction("Xuất CSV", IconChar.FileCsv, () =>
                    TableExportRunner.Run(getTable().FindForm(), new CsvTableExporter(), "Danh mục",
                        () => exportHeaders, presenter.ExportRows)),
                new OverflowMenuAction("Xuất Excel", IconChar.FileExcel, () =>
                    TableExportRunner.Run(getTable().FindForm(), new ExcelTableExporter(), "Danh mục",
                        () => exportHeaders, presenter.ExportRows)),
                new OverflowMenuAction("Nhập dữ liệu", IconChar.Upload, () =>
                {
                    var table = getTable();
                    var result = frmImportData.Show(
                        table.FindForm(),
                        "Danh mục",
                        CategoryImportRowHandler.ExpectedColumns,
                        "Trạng thái: Đang hoạt động hoặc Vô hiệu hóa. Để trống thì danh mục được mở.",
                        DefaultSpreadsheetImporters.All,
                        importer.Handle);
                    if (result == DialogResult.OK) table.Reload();
                })
            };
        }

        private static IList<OverflowMenuAction> SupplierOverflow(
            SupplierManagementPresenter presenter,
            SupplierImportRowHandler importer,
            string[] exportHeaders,
            Func<BaseTable> getTable)
        {
            return new List<OverflowMenuAction>
            {
                new OverflowMenuAction("Xuất CSV", IconChar.FileCsv, () =>
                    TableExportRunner.Run(getTable().FindForm(), new CsvTableExporter(), "Nhà cung cấp",
                        () => exportHeaders, presenter.ExportRows)),
                new OverflowMenuAction("Xuất Excel", IconChar.FileExcel, () =>
                    TableExportRunner.Run(getTable().FindForm(), new ExcelTableExporter(), "Nhà cung cấp",
                        () => exportHeaders, presenter.ExportRows)),
                new OverflowMenuAction("Nhập dữ liệu", IconChar.Upload, () =>
                {
                    var table = getTable();
                    var result = frmImportData.Show(
                        table.FindForm(),
                        "Nhà cung cấp",
                        SupplierImportRowHandler.ExpectedColumns,
                        "Mỗi dòng là một nhà cung cấp. Tên là bắt buộc; số điện thoại, email, địa chỉ và mặt hàng có thể để trống.",
                        DefaultSpreadsheetImporters.All,
                        importer.Handle);
                    if (result == DialogResult.OK) table.Reload();
                })
            };
        }

        private static void OnProductAction(BaseTable table, ProductManagementPresenter presenter, TableActionEventArgs e)
        {
            var rows = presenter.CurrentRows;
            if (e.RowIndex < 0 || e.RowIndex >= rows.Count) return;
            var row = rows[e.RowIndex];

            if (e.Action == TableActionType.Lock)
            {
                bool enable = !row.IsActive;
                string verb = enable ? "mở lại" : "vô hiệu hóa";
                if (!DialogHelper.Confirm(table.FindForm(), "Xác nhận thao tác",
                    "Bạn có chắc muốn " + verb + " sản phẩm '" + row.Name + "' không?"))
                    return;

                var result = presenter.SetActive(row.ProductId, enable);
                if (result.Success) table.Reload();
                Notify(table, result);
                return;
            }

            if (e.Action != TableActionType.View && e.Action != TableActionType.Edit) return;
            EditProduct(table, presenter, presenter.Get(row.ProductId), e.Action == TableActionType.View, row.CategoryName);
        }

        private static void OnCategoryAction(BaseTable table, CategoryManagementPresenter presenter, TableActionEventArgs e)
        {
            var rows = presenter.CurrentRows;
            if (e.RowIndex < 0 || e.RowIndex >= rows.Count) return;
            var row = rows[e.RowIndex];

            if (e.Action == TableActionType.Lock)
            {
                bool enable = !row.IsActive;
                string verb = enable ? "mở lại" : "vô hiệu hóa";
                if (!DialogHelper.Confirm(table.FindForm(), "Xác nhận thao tác",
                    "Bạn có chắc muốn " + verb + " danh mục '" + row.Name + "' không?"))
                    return;

                var result = presenter.SetActive(row.CategoryId, enable);
                if (result.Success) table.Reload();
                Notify(table, result);
                return;
            }

            if (e.Action != TableActionType.View && e.Action != TableActionType.Edit) return;
            EditCategory(table, presenter, presenter.Get(row.CategoryId), e.Action == TableActionType.View);
        }

        private static void OnSupplierAction(BaseTable table, SupplierManagementPresenter presenter, TableActionEventArgs e)
        {
            var rows = presenter.CurrentRows;
            if (e.RowIndex < 0 || e.RowIndex >= rows.Count) return;
            var row = rows[e.RowIndex];

            if (e.Action == TableActionType.Delete)
            {
                if (!DialogHelper.ConfirmDelete(table.FindForm(), "nhà cung cấp", row.Name)) return;
                var result = presenter.Delete(row.SupplierId);
                if (result.Success) table.Reload();
                Notify(table, result);
                return;
            }

            if (e.Action != TableActionType.View && e.Action != TableActionType.Edit) return;
            EditSupplier(table, presenter, presenter.Get(row.SupplierId), e.Action == TableActionType.View);
        }

        private static void EditProduct(BaseTable table, ProductManagementPresenter presenter, ProductEditDto draft, bool readOnly, string categoryName = null)
        {
            if (draft == null)
            {
                AppToast.Warning(table, "Không tìm thấy sản phẩm.");
                return;
            }

            var owner = table.FindForm();
            var categories = EnsureLookup(presenter.Categories(), draft.CategoryId, categoryName);
            var suppliers = EnsureLookup(presenter.Suppliers(), draft.SupplierId ?? 0, null);
            if (readOnly)
            {
                ProductEditDto ignored;
                frmProductEditor.TryEdit(owner, draft, categories, suppliers, true, out ignored);
                return;
            }

            while (frmProductEditor.TryEdit(owner, draft, categories, suppliers, false, out var edited))
            {
                var result = presenter.Save(edited);
                if (result.Success)
                {
                    table.Reload();
                    Notify(table, result);
                    return;
                }

                DialogHelper.ShowWarning(owner ?? table, result.Message);
                draft = edited;
            }
        }

        private static void EditCategory(BaseTable table, CategoryManagementPresenter presenter, CategoryEditDto draft, bool readOnly)
        {
            if (draft == null)
            {
                AppToast.Warning(table, "Không tìm thấy danh mục.");
                return;
            }

            var owner = table.FindForm();
            if (readOnly)
            {
                CategoryEditDto ignored;
                frmCategoryEditor.TryEdit(owner, draft, true, out ignored);
                return;
            }

            while (frmCategoryEditor.TryEdit(owner, draft, false, out var edited))
            {
                var result = presenter.Save(edited);
                if (result.Success)
                {
                    table.Reload();
                    Notify(table, result);
                    return;
                }

                DialogHelper.ShowWarning(owner ?? table, result.Message);
                draft = edited;
            }
        }

        private static void EditSupplier(BaseTable table, SupplierManagementPresenter presenter, SupplierEditDto draft, bool readOnly)
        {
            if (draft == null)
            {
                AppToast.Warning(table, "Không tìm thấy nhà cung cấp.");
                return;
            }

            var owner = table.FindForm();
            if (readOnly)
            {
                SupplierEditDto ignored;
                frmSupplierEditor.TryEdit(owner, draft, true, out ignored);
                return;
            }

            while (frmSupplierEditor.TryEdit(owner, draft, false, out var edited))
            {
                var result = presenter.Save(edited);
                if (result.Success)
                {
                    table.Reload();
                    Notify(table, result);
                    return;
                }

                DialogHelper.ShowWarning(owner ?? table, result.Message);
                draft = edited;
            }
        }

        private static IReadOnlyList<LookupItem> EnsureLookup(IReadOnlyList<LookupItem> items, int id, string name)
        {
            var list = new List<LookupItem>(items ?? Array.Empty<LookupItem>());
            if (id <= 0 || list.Exists(item => item.Id == id)) return list;
            list.Insert(0, new LookupItem
            {
                Id = id,
                Name = string.IsNullOrWhiteSpace(name) ? "Mục hiện tại" : name
            });
            return list;
        }

        private static IList<FilterOption> StatusOptions() => new[]
        {
            new FilterOption("Tất cả trạng thái", null),
            new FilterOption("Đang hoạt động", "ACTIVE"),
            new FilterOption("Vô hiệu hóa", "DISABLED")
        };

        private static TablePageResult Load(LoadingOverlayHost host, Func<TablePageResult> load)
        {
            host?.ShowLoading("Đang tải dữ liệu...");
            Application.DoEvents();
            try
            {
                return WithActions(load());
            }
            finally
            {
                host?.HideLoading();
            }
        }

        private static TablePageResult WithActions(TablePageResult page)
        {
            var rows = new List<object[]>();
            foreach (var row in page.Rows ?? new List<object[]>())
            {
                var cells = new object[(row == null ? 0 : row.Length) + 1];
                if (row != null) Array.Copy(row, cells, row.Length);
                cells[cells.Length - 1] = string.Empty;
                rows.Add(cells);
            }

            return new TablePageResult { TotalCount = page.TotalCount, Rows = rows };
        }

        private static void Notify(Control anchor, CatalogSaveResult result)
        {
            if (result != null && result.Success) AppToast.Success(anchor, result.Message);
            else AppToast.Warning(anchor, result == null ? "Không lưu được dữ liệu." : result.Message);
        }
    }
}

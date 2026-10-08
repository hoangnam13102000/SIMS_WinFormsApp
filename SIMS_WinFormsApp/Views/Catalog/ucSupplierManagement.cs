using System;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Views.UserManager;
using SIMS_WinFormsApp.Views.SystemManagement;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.MVP.Presenters.Catalog;
using SIMS_WinFormsApp.Services.Implementations.Export;
using SIMS_WinFormsApp.Services.Implementations.Import;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Controls.Filter;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.UI.Controls.Toast;

namespace SIMS_WinFormsApp.Views.Catalog
{
    public sealed partial class ucSupplierManagement : UserControl
    {
        private readonly SupplierManagementPresenter _presenter;
        private readonly ICatalogAdminService _service;
        private readonly Timer _searchTimer;
        private int _pageIndex;
        private int _pageSize = 10;
        private int _totalCount;

        public ucSupplierManagement() : this(null)
        {
        }

        public ucSupplierManagement(ICatalogAdminService service)
        {
            InitializeComponent();
            if (service == null) return;

            _service = service;
            _presenter = new SupplierManagementPresenter(service);
            _searchTimer = new Timer { Interval = 350 };
            _searchTimer.Tick += (sender, args) =>
            {
                _searchTimer.Stop();
                _pageIndex = 0;
                LoadPage();
            };
            _searchTextBox.TextChanged += (sender, args) =>
            {
                _searchTimer.Stop();
                _searchTimer.Start();
            };
            _debtComboBox.SelectedIndexChanged += (sender, args) =>
            {
                _pageIndex = 0;
                LoadPage();
            };
            _pageSizeComboBox.SelectedIndexChanged += (sender, args) =>
            {
                _pageSize = _pageSizeComboBox.SelectedIndex == 1 ? 20 :
                    (_pageSizeComboBox.SelectedIndex == 2 ? 50 : 10);
                _pageIndex = 0;
                LoadPage();
            };
            _previousButton.Click += (sender, args) =>
            {
                if (_pageIndex <= 0) return;
                _pageIndex--;
                LoadPage();
            };
            _nextButton.Click += (sender, args) =>
            {
                if (_pageIndex + 1 >= PageCount) return;
                _pageIndex++;
                LoadPage();
            };
            _addButton.Click += (sender, args) => EditSupplier(_presenter.NewDraft());
            _importButton.Click += (sender, args) => ImportSuppliers();
            _exportCsvButton.Click += (sender, args) => ExportSuppliers(new CsvTableExporter());
            _exportExcelButton.Click += (sender, args) => ExportSuppliers(new ExcelTableExporter());
            _suppliersGrid.CellMouseClick += OnGridCellMouseClick;
            Load += (sender, args) => LoadPage();
            Disposed += (sender, args) => _searchTimer.Dispose();
        }

        private int PageCount => Math.Max(1, (int)Math.Ceiling(_totalCount / (double)_pageSize));

        private void LoadPage()
        {
            if (_presenter == null || IsDisposed) return;
            _suppliersGrid.Rows.Clear();
            try
            {
                string debtFilter = _debtComboBox.SelectedIndex == 1 ? "DEBT" : null;
                TablePageResult result = _presenter.Load(_pageIndex, _pageSize, _searchTextBox.Text, debtFilter);
                _totalCount = result.TotalCount;
                if (_pageIndex >= PageCount)
                {
                    _pageIndex = PageCount - 1;
                    LoadPage();
                    return;
                }

                foreach (SupplierRowDto row in _presenter.CurrentRows)
                {
                    _suppliersGrid.Rows.Add(
                        row.Name,
                        row.Phone,
                        row.Email,
                        row.Address,
                        row.SuppliedItems,
                        CatalogFormat.Money(row.DebtBalance),
                        IconBitmapCache.GetActionStrip(IconChar.Trash, AppColors.Error));
                }

                UpdatePaging();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Không thể tải nhà cung cấp: " + ex.Message,
                    "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdatePaging()
        {
            int firstItem = _totalCount == 0 ? 0 : _pageIndex * _pageSize + 1;
            int lastItem = Math.Min((_pageIndex + 1) * _pageSize, _totalCount);
            _resultsLabel.Text = _totalCount == 0
                ? "Hiển thị 0 nhà cung cấp"
                : string.Format("Hiển thị {0}-{1} / {2} nhà cung cấp", firstItem, lastItem, _totalCount);
            _pageLabel.Text = "Trang " + (_pageIndex + 1) + " / " + PageCount;
            _previousButton.Enabled = _pageIndex > 0;
            _nextButton.Enabled = _pageIndex + 1 < PageCount;
            _previousButton.BackColor = _previousButton.Enabled ? Color.White : Color.FromArgb(248, 250, 252);
            _nextButton.BackColor = _nextButton.Enabled ? Color.White : Color.FromArgb(248, 250, 252);
        }

        private void OnGridCellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || _presenter == null || e.RowIndex < 0 ||
                e.RowIndex >= _presenter.CurrentRows.Count)
                return;

            if (e.ColumnIndex != _actionsColumn.Index) return;
            SupplierRowDto row = _presenter.CurrentRows[e.RowIndex];
            const int iconSize = 24;
            const int gap = 8;
            int stripWidth = iconSize * 3 + gap * 2;
            int offset = e.X - (_actionsColumn.Width - stripWidth) / 2;
            int actionIndex = offset / (iconSize + gap);
            if (offset < 0 || actionIndex > 2 || offset % (iconSize + gap) >= iconSize) return;
            if (actionIndex == 0)
                EditSupplier(_presenter.Get(row.SupplierId), true);
            else if (actionIndex == 1)
                EditSupplier(_presenter.Get(row.SupplierId));
            else
            {
                if (!DialogHelper.ConfirmDelete(this, "nhà cung cấp", row.Name)) return;
                CatalogSaveResult result = _presenter.Delete(row.SupplierId);
                Notify(result);
                if (result != null && result.Success) LoadPage();
            }
        }

        private void EditSupplier(SupplierEditDto draft, bool readOnly = false)
        {
            if (draft == null)
            {
                AppToast.Warning(this, "Không tìm thấy nhà cung cấp.");
                return;
            }

            IWin32Window owner = FindForm();
            if (readOnly)
            {
                SupplierEditDto ignored;
                frmSupplierEditor.TryEdit(owner, draft, true, out ignored);
                return;
            }

            SupplierEditDto edited;
            while (frmSupplierEditor.TryEdit(owner, draft, false, out edited))
            {
                CatalogSaveResult result = _presenter.Save(edited);
                if (result != null && result.Success)
                {
                    Notify(result);
                    _pageIndex = 0;
                    LoadPage();
                    return;
                }

                DialogHelper.ShowWarning(owner ?? this,
                    result == null ? "Không lưu được nhà cung cấp." : result.Message);
                draft = edited;
            }
        }

        private void ImportSuppliers()
        {
            var importer = new SupplierImportRowHandler(_service);
            DialogResult result = frmImportData.Show(
                FindForm(),
                "Nhà cung cấp",
                SupplierImportRowHandler.ExpectedColumns,
                "Các cột gồm tên, số điện thoại, email, địa chỉ và mặt hàng cung cấp.",
                DefaultSpreadsheetImporters.All,
                importer.Handle);
            if (result == DialogResult.OK) LoadPage();
        }

        private void ExportSuppliers(ITableDataExporter exporter)
        {
            string[] headers = { "Tên nhà cung cấp", "Số điện thoại", "Email", "Địa chỉ", "Mặt hàng cung cấp", "Công nợ" };
            TableExportRunner.Run(
                FindForm(), exporter, "Nhà cung cấp", () => headers, _presenter.ExportRows);
        }

        private void Notify(CatalogSaveResult result)
        {
            if (result != null && result.Success)
                AppToast.Success(this, result.Message);
            else
                AppToast.Warning(this, result == null ? "Không lưu được dữ liệu." : result.Message);
        }
    }
}

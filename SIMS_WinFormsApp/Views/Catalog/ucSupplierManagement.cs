using System;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Views.UserManager;
using SIMS_WinFormsApp.Views.SystemManagement;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.MVP.Presenters.Catalog;
using SIMS_WinFormsApp.Services.Implementations.Export;
using SIMS_WinFormsApp.Services.Implementations.Import;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Toast;
using SIMS_WinFormsApp.Views.Dialogs;

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
            _header.SizeChanged += (sender, args) => LayoutHeader();
            LayoutHeader();
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
            _pageSizeComboBox.SelectedIndexChanged += (sender, args) =>
            {
                _pageSize = _pageSizeComboBox.SelectedIndex == 1 ? 20 :
                    (_pageSizeComboBox.SelectedIndex == 2 ? 50 : 10);
                _pageIndex = 0;
                LoadPage();
            };
            _suppliersGrid.CellMouseClick += OnGridCellMouseClick;
            Load += (sender, args) => LoadPage();
            Disposed += (sender, args) => _searchTimer.Dispose();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (_presenter == null) return;
            EditSupplier(_presenter.NewDraft());
        }

        private void OptionsButton_Click(object sender, EventArgs e)
        {
            _optionsMenu.Show(_optionsButton, 0, _optionsButton.Height);
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void PreviousButton_Click(object sender, EventArgs e)
        {
            if (_presenter == null) return;
            if (_pageIndex <= 0) return;
            _pageIndex--;
            LoadPage();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (_presenter == null) return;
            if (_pageIndex + 1 >= PageCount) return;
            _pageIndex++;
            LoadPage();
        }

        private void ExportCsvMenuItem_Click(object sender, EventArgs e)
        {
            ExportSuppliers(new CsvTableExporter());
        }

        private void ExportExcelMenuItem_Click(object sender, EventArgs e)
        {
            ExportSuppliers(new ExcelTableExporter());
        }

        private void ImportMenuItem_Click(object sender, EventArgs e)
        {
            ImportSuppliers();
        }

        private void LayoutHeader()
        {
            int right = _header.ClientSize.Width - 16;
            _refreshButton.Location = new Point(right - _refreshButton.Width, 29);
            right = _refreshButton.Left - 8;
            _optionsButton.Location = new Point(right - _optionsButton.Width, 29);
            right = _optionsButton.Left - 8;
            _addButton.Location = new Point(right - _addButton.Width, 29);
            _titleLabel.Width = Math.Max(180, _addButton.Left - _titleLabel.Left - 16);
            _addButton.BringToFront();
            _optionsButton.BringToFront();
            _refreshButton.BringToFront();
        }

        private int PageCount => Math.Max(1, (int)Math.Ceiling(_totalCount / (double)_pageSize));

        private void LoadPage()
        {
            if (_presenter == null || IsDisposed) return;
            _suppliersGrid.Rows.Clear();
            try
            {
                TablePageResult result = _presenter.Load(_pageIndex, _pageSize, _searchTextBox.Text);
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
                        IconBitmapCache.GetActionStrip(IconChar.Trash, AppColors.Error));
                }

                UpdatePaging();
                _suppliersGrid.ClearSelection();
                _suppliersGrid.CurrentCell = null;
            }
            catch (Exception ex)
            {
                DialogHelper.ShowError(this, "Lỗi tải dữ liệu", "Không thể tải nhà cung cấp: " + ex.Message);
            }
        }

        private SupplierRowDto SelectedSupplier()
        {
            if (_presenter == null || _suppliersGrid.SelectedRows.Count == 0)
                return null;

            int rowIndex = _suppliersGrid.SelectedRows[0].Index;
            return rowIndex >= 0 && rowIndex < _presenter.CurrentRows.Count
                ? _presenter.CurrentRows[rowIndex]
                : null;
        }

        private void ViewSelectedSupplier()
        {
            SupplierRowDto row = SelectedSupplier();
            if (row == null) return;

            EditSupplier(_presenter.Get(row.SupplierId), true);
        }

        // Edit: mở form sửa -> Presenter.Save -> Service.SaveSupplier -> Repository.UpdateSupplier.
        private void EditSelectedSupplier()
        {
            SupplierRowDto row = SelectedSupplier();
            if (row == null) return;

            EditSupplier(_presenter.Get(row.SupplierId));
        }

        // Delete: xác nhận -> Presenter.Delete -> Service.DeleteSupplier -> Repository.SoftDeleteSupplier.
        private void DeleteSelectedSupplier()
        {
            SupplierRowDto row = SelectedSupplier();
            if (row == null || !DialogHelper.ConfirmDelete(this, "nhà cung cấp", row.Name))
                return;

            CatalogSaveResult result = _presenter.Delete(row.SupplierId);
            Notify(result);
            if (result != null && result.Success) LoadPage();
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
            _suppliersGrid.CurrentCell = _suppliersGrid.Rows[e.RowIndex].Cells[0];
            _suppliersGrid.Rows[e.RowIndex].Selected = true;
            const int iconSize = 24;
            const int gap = 8;
            int stripWidth = iconSize * 3 + gap * 2;
            int offset = e.X - (_actionsColumn.Width - stripWidth) / 2;
            int actionIndex = offset / (iconSize + gap);
            if (offset < 0 || actionIndex > 2 || offset % (iconSize + gap) >= iconSize) return;
            if (actionIndex == 0)
                ViewSelectedSupplier();
            else if (actionIndex == 1)
                EditSelectedSupplier();
            else if (actionIndex == 2)
                DeleteSelectedSupplier();
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
                CatalogSaveResult ignoredResult;
                frmSupplierEditor.TryEdit(owner, draft, true, _presenter.Save, out ignored, out ignoredResult);
                return;
            }

            SupplierEditDto edited;
            CatalogSaveResult saveResult;
            if (!frmSupplierEditor.TryEdit(
                    owner, draft, false, _presenter.Save, out edited, out saveResult))
                return;

            Notify(saveResult);
            _pageIndex = 0;
            LoadPage();
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
            string[] headers = { "Tên nhà cung cấp", "Số điện thoại", "Email", "Địa chỉ", "Mặt hàng cung cấp" };
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

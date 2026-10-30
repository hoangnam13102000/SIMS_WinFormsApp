using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.MVP.Presenters.Inventory;
using SIMS_WinFormsApp.Services.Implementations.Export;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.Views.Interfaces;
using SIMS_WinFormsApp.Views.Toast;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    public partial class frmStockReconciliation : Form, IStockReconciliationView
    {
        private const string SearchPlaceholder = "Tìm tên hoặc mã sản phẩm...";
        private StockReconciliationPresenter _presenter;
        private bool _searchPlaceholderActive;

        public event EventHandler LoadRequested;
        public event EventHandler SaveRequested;

        public frmStockReconciliation() : this(null, null)
        {
        }

        public frmStockReconciliation(IInventoryService service, IUserSession session)
        {
            InitializeComponent();
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            _presenter = new StockReconciliationPresenter(this, service, session);
            _loadButton.Click += (sender, args) => RaiseLoadRequested();
            _saveButton.Click += (sender, args) => SaveRequested?.Invoke(this, EventArgs.Empty);
            _exportButton.Click += (sender, args) => ExportStock();
            _searchBox.Enter += (sender, args) => HideSearchPlaceholder();
            _searchBox.Leave += (sender, args) => ShowSearchPlaceholder();
            _searchBox.KeyDown += OnSearchBoxKeyDown;
            _stockGrid.CellEndEdit += OnStockGridCellEndEdit;
            ShowSearchPlaceholder();
            _presenter.Initialize();
        }

        public string Keyword => _searchPlaceholderActive ? string.Empty : _searchBox.Text.Trim();

        public int? SelectedCategoryId
        {
            get
            {
                int categoryId;
                if (!int.TryParse(Convert.ToString(_categoryFilter.SelectedValue, CultureInfo.InvariantCulture),
                    NumberStyles.Integer, CultureInfo.InvariantCulture, out categoryId) || categoryId <= 0)
                    return null;
                return categoryId;
            }
        }

        public void BindCategories(IReadOnlyList<LookupItem> items)
        {
            var options = new List<LookupItem>
            {
                new LookupItem { Id = 0, Name = "Tất cả danh mục" }
            };
            if (items != null) options.AddRange(items);

            _categoryFilter.Items.Clear();
            _categoryFilter.DisplayMember = "Name";
            _categoryFilter.ValueMember = "Id";
            _categoryFilter.DataSource = options;
            _categoryFilter.SelectedIndex = 0;
        }

        public void BindStock(IReadOnlyList<StockRowDto> rows)
        {
            _stockGrid.Rows.Clear();
            if (rows == null) rows = Array.Empty<StockRowDto>();

            foreach (StockRowDto stock in rows)
            {
                int rowIndex = _stockGrid.Rows.Add(
                    stock.ProductId,
                    stock.Code,
                    stock.Name,
                    stock.Stock,
                    stock.StatusText,
                    null,
                    null,
                    null);

                DataGridViewRow row = _stockGrid.Rows[rowIndex];
                if (stock.Stock <= 0)
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 232, 232);
                else if (stock.Stock <= stock.MinStock)
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 242, 220);
            }

            _countLabel.Text = "Số mặt hàng: " + rows.Count.ToString(CultureInfo.InvariantCulture);
        }

        public IReadOnlyList<StockCountDto> ReadCounts()
        {
            _stockGrid.EndEdit();
            var counts = new List<StockCountDto>();

            foreach (DataGridViewRow row in _stockGrid.Rows)
            {
                if (row.IsNewRow) continue;

                string actualText = Convert.ToString(row.Cells["CountQuantity"].Value, CultureInfo.CurrentCulture);
                int actualStock;
                if (string.IsNullOrWhiteSpace(actualText) ||
                    !int.TryParse(actualText, NumberStyles.Integer, CultureInfo.CurrentCulture, out actualStock))
                    continue;

                int productId;
                int systemStock;
                if (!int.TryParse(Convert.ToString(row.Cells["_productIdColumn"].Value, CultureInfo.InvariantCulture),
                        NumberStyles.Integer, CultureInfo.InvariantCulture, out productId) ||
                    !int.TryParse(Convert.ToString(row.Cells["SystemQuantity"].Value, CultureInfo.InvariantCulture),
                        NumberStyles.Integer, CultureInfo.InvariantCulture, out systemStock))
                    continue;

                counts.Add(new StockCountDto
                {
                    ProductId = productId,
                    SystemStock = systemStock,
                    ActualStock = actualStock,
                    Note = Convert.ToString(row.Cells["_noteColumn"].Value, CultureInfo.CurrentCulture)
                });
            }

            return counts;
        }

        public bool Confirm(string message)
        {
            return AppComposition.CreateDialogService().Confirm(this, "Xác nhận", message);
        }

        public void ShowMessage(string message, bool isError)
        {
            IToastNotifier notifier = AppComposition.CreateToastNotifier();
            if (isError)
                notifier.Error(this, message);
            else
                notifier.Success(this, message);
        }

        private void OnSearchBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            RaiseLoadRequested();
        }

        private void RaiseLoadRequested()
        {
            LoadRequested?.Invoke(this, EventArgs.Empty);
        }

        private void HideSearchPlaceholder()
        {
            if (!_searchPlaceholderActive) return;
            _searchBox.Clear();
            _searchBox.ForeColor = SystemColors.WindowText;
            _searchPlaceholderActive = false;
        }

        private void ShowSearchPlaceholder()
        {
            if (!string.IsNullOrWhiteSpace(_searchBox.Text)) return;
            _searchBox.Text = SearchPlaceholder;
            _searchBox.ForeColor = Color.Gray;
            _searchPlaceholderActive = true;
        }

        private void OnStockGridCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != _stockGrid.Columns["CountQuantity"].Index)
                return;

            DataGridViewRow row = _stockGrid.Rows[e.RowIndex];
            int actualStock;
            string actualText = Convert.ToString(row.Cells["CountQuantity"].Value, CultureInfo.CurrentCulture);
            if (!int.TryParse(actualText, NumberStyles.Integer, CultureInfo.CurrentCulture, out actualStock))
            {
                row.Cells["CountQuantity"].Value = null;
                row.Cells["Difference"].Value = null;
                row.Cells["Difference"].Style.ForeColor = Color.Empty;
                return;
            }

            int systemStock;
            if (!int.TryParse(Convert.ToString(row.Cells["SystemQuantity"].Value, CultureInfo.InvariantCulture),
                NumberStyles.Integer, CultureInfo.InvariantCulture, out systemStock))
                return;

            int difference = actualStock - systemStock;
            string differenceText = difference > 0
                ? "+" + difference.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"))
                : difference.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
            row.Cells["Difference"].Value = differenceText;
            row.Cells["Difference"].Style.ForeColor = difference < 0 ? Color.Red : Color.Empty;
        }

        private void ExportStock()
        {
            string[] headers =
            {
                "Mã sản phẩm", "Tên sản phẩm", "Tồn hệ thống", "Trạng thái",
                "Số lượng thực tế", "Chênh lệch", "Ghi chú"
            };

            TableExportRunner.Run(
                FindForm() ?? this,
                new ExcelTableExporter(),
                "Kiểm kê tồn kho",
                () => headers,
                GetExportRows);
        }

        private IReadOnlyList<object[]> GetExportRows()
        {
            var rows = new List<object[]>();
            foreach (DataGridViewRow row in _stockGrid.Rows)
            {
                if (row.IsNewRow) continue;
                rows.Add(new object[]
                {
                    row.Cells["Sku"].Value,
                    row.Cells["Product"].Value,
                    row.Cells["SystemQuantity"].Value,
                    row.Cells["_statusColumn"].Value,
                    row.Cells["CountQuantity"].Value,
                    row.Cells["Difference"].Value,
                    row.Cells["_noteColumn"].Value
                });
            }
            return rows;
        }
    }
}

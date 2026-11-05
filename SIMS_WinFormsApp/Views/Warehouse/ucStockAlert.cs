using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.MVP.Presenters.Inventory;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;
using SIMS_WinFormsApp.Views.Toast;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    public partial class ucStockAlert : UserControl, IStockAlertView
    {
        private const uint EmSetCueBanner = 0x1501;
        private static readonly CultureInfo VietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");
        private static readonly Font BadgeFont = new Font("Segoe UI", 9F, FontStyle.Bold);
        private readonly IInventoryService _service;
        private readonly IUserSession _session;
        private readonly StockAlertPresenter _presenter;
        private readonly Timer _searchTimer;
        private bool _initialized;
        private bool _suppressEvents;
        private bool _fromDateActive;
        private bool _toDateActive;

        public ucStockAlert()
            : this(null, null, null)
        {
        }

        public ucStockAlert(IInventoryService service, IUserSession session, Action alertsSeen)
        {
            InitializeComponent();
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            _service = service ?? AppComposition.CreateInventoryService();
            _session = session ?? UserSession.Instance;
            _presenter = new StockAlertPresenter(
                this, _service, _session, AppComposition.CreateDialogService(), alertsSeen);
            _searchTimer = new Timer(components);
            _searchTimer.Interval = 300;
            _searchTimer.Tick += new EventHandler(this.SearchTimer_Tick);

            _searchBox.HandleCreated += new EventHandler(this.SearchBox_HandleCreated);
            if (_searchBox.IsHandleCreated)
                ApplySearchPlaceholder();
            _searchBox.TextChanged += new EventHandler(this.SearchBox_TextChanged);
            _categoryCombo.SelectedIndexChanged += new EventHandler(this.FilterControl_Changed);
            _fromDatePicker.ValueChanged += new EventHandler(this.FilterControl_Changed);
            _toDatePicker.ValueChanged += new EventHandler(this.FilterControl_Changed);
            _clearFilterLink.LinkClicked += new LinkLabelLinkClickedEventHandler(this.ClearFilterLink_LinkClicked);
            _pageSizeCombo.SelectedIndexChanged += new EventHandler(this.PageSizeCombo_SelectedIndexChanged);
            _prevButton.Click += new EventHandler(this.PreviousButton_Click);
            _nextButton.Click += new EventHandler(this.NextButton_Click);
            _optionsButton.Click += new EventHandler(this.OptionsButton_Click);
            _exportCsvItem.Click += new EventHandler(this.ExportCsvItem_Click);
            _exportExcelItem.Click += new EventHandler(this.ExportExcelItem_Click);
            _grid.CellClick += new DataGridViewCellEventHandler(this.Grid_CellClick);
            _grid.CellMouseClick += new DataGridViewCellMouseEventHandler(this.Grid_CellMouseClick);
            _grid.CellMouseEnter += new DataGridViewCellEventHandler(this.Grid_CellMouseEnter);
            _grid.CellMouseLeave += new DataGridViewCellEventHandler(this.Grid_CellMouseLeave);
            _grid.CellFormatting += new DataGridViewCellFormattingEventHandler(this.Grid_CellFormatting);
            _grid.CellToolTipTextNeeded += new DataGridViewCellToolTipTextNeededEventHandler(this.Grid_CellToolTipTextNeeded);
            DataGridViewCodeCopy.Attach(_grid, false, "colProductCode");
            VisibleChanged += new EventHandler(this.OnVisibleChanged);
        }

        public string Keyword => _searchBox.Text.Trim();

        public int? CategoryId
        {
            get
            {
                CategoryOption category = _categoryCombo.SelectedItem as CategoryOption;
                return category == null ? (int?)null : category.Id;
            }
        }

        public DateTime? FromDate => _fromDateActive
            ? _fromDatePicker.Value.Date
            : (DateTime?)null;

        public DateTime? ToDate => _toDateActive
            ? _toDatePicker.Value.Date
            : (DateTime?)null;

        public int PageSize => _pageSizeCombo.SelectedItem == null
            ? 10
            : Convert.ToInt32(_pageSizeCombo.SelectedItem, CultureInfo.InvariantCulture);

        public IWin32Window Owner => FindForm();

        public event EventHandler SearchRequested;
        public event EventHandler FiltersChanged;
        public event EventHandler ClearFiltersRequested;
        public event EventHandler PageSizeChanged;
        public event EventHandler PreviousPageRequested;
        public event EventHandler NextPageRequested;
        public event EventHandler CsvExportRequested;
        public event EventHandler ExcelExportRequested;
        public event Action<int> PlanAlertRequested;
        public event Action<int> ResolveAlertRequested;

        public void BindCategories(IReadOnlyList<LookupItem> categories)
        {
            _suppressEvents = true;
            try
            {
                var options = new List<CategoryOption>
                {
                    new CategoryOption(null, "Tất cả loại SP")
                };
                if (categories != null)
                {
                    foreach (LookupItem category in categories)
                    {
                        if (category != null)
                            options.Add(new CategoryOption(category.Id, category.Name));
                    }
                }

                _categoryCombo.DataSource = null;
                _categoryCombo.DisplayMember = "Name";
                _categoryCombo.ValueMember = "Id";
                _categoryCombo.DataSource = options;
                _categoryCombo.SelectedIndex = 0;
            }
            finally
            {
                _suppressEvents = false;
            }
        }

        public void DisplayPage(
            IReadOnlyList<StockAlertListItemDto> rows,
            int totalCount,
            int pageNumber,
            int pageCount)
        {
            _grid.Rows.Clear();
            foreach (StockAlertListItemDto alert in rows ?? Array.Empty<StockAlertListItemDto>())
            {
                bool canPlan = string.Equals(alert.Status, "NEW", StringComparison.OrdinalIgnoreCase);
                bool canResolve = !string.Equals(alert.Status, "RESOLVED", StringComparison.OrdinalIgnoreCase);
                int rowIndex = _grid.Rows.Add(
                    alert.AlertId,
                    alert.ProductCode,
                    alert.ProductName,
                    GetAlertTypeText(alert.AlertType),
                    string.IsNullOrWhiteSpace(alert.ReportedByName) ? "Hệ thống" : alert.ReportedByName,
                    alert.CreatedAt.ToString("dd/MM/yyyy HH:mm", VietnameseCulture),
                    GetStatusText(alert.Status),
                    null,
                    null);
                DataGridViewRow row = _grid.Rows[rowIndex];
                row.Tag = alert;
                row.Cells[colPlan.Index].Value = IconBitmapCache.Get(
                    IconChar.CalendarPlus,
                    canPlan ? Color.FromArgb(37, 99, 235) : Color.FromArgb(203, 213, 225),
                    new Size(18, 18));
                row.Cells[colResolve.Index].Value = IconBitmapCache.Get(
                    IconChar.CircleCheck,
                    canResolve ? Color.FromArgb(22, 163, 74) : Color.FromArgb(203, 213, 225),
                    new Size(18, 18));
                row.Cells[colPlan.Index].ToolTipText = canPlan
                    ? "Đã lên kế hoạch nhập bổ sung"
                    : string.Empty;
                row.Cells[colResolve.Index].ToolTipText = canResolve
                    ? "Đánh dấu đã xử lý xong"
                    : string.Empty;
            }

            _grid.ClearSelection();
            _grid.CurrentCell = null;
            _totalLabel.Text = "Tổng cộng: " + totalCount.ToString("N0", VietnameseCulture) + " cảnh báo";
            _pageLabel.Text = pageNumber.ToString(CultureInfo.InvariantCulture);
            _pageInfoLabel.Text = "Trang " + pageNumber.ToString(CultureInfo.InvariantCulture) +
                " / " + pageCount.ToString(CultureInfo.InvariantCulture);
            _prevButton.Enabled = pageNumber > 1;
            _nextButton.Enabled = pageNumber < pageCount;

            if (_grid.Rows.Count == 0)
            {
                _emptyLabel.Text = HasActiveFilters()
                    ? "Không tìm thấy cảnh báo phù hợp"
                    : "Chưa có cảnh báo nào";
                _emptyLabel.Visible = true;
                _emptyLabel.BringToFront();
            }
            else
            {
                _emptyLabel.Visible = false;
            }
        }

        public void SetClearFilterVisible(bool visible)
        {
            _clearFilterLink.Visible = visible;
        }

        public void ClearFilters()
        {
            _searchTimer.Stop();
            _suppressEvents = true;
            try
            {
                _searchBox.Clear();
                _categoryCombo.SelectedIndex = _categoryCombo.Items.Count > 0 ? 0 : -1;
                _fromDatePicker.Checked = false;
                _toDatePicker.Checked = false;
                _fromDateActive = false;
                _toDateActive = false;
                _fromDatePicker.CustomFormat = " ";
                _toDatePicker.CustomFormat = " ";
            }
            finally
            {
                _suppressEvents = false;
            }
            SetClearFilterVisible(false);
        }

        public void ClearToDate()
        {
            _suppressEvents = true;
            try
            {
                _toDatePicker.Checked = false;
                _toDateActive = false;
                _toDatePicker.CustomFormat = " ";
            }
            finally
            {
                _suppressEvents = false;
            }
        }

        public void ShowInvalidDateRange()
        {
            AppToast.Warning(this, "Khoảng ngày không hợp lệ",
                "Ngày \"Đến\" phải lớn hơn hoặc bằng ngày \"Từ\".");
        }

        public void ShowSuccess(string title, string message)
        {
            AppToast.Success(this, title, message);
        }

        public void ShowError(string title, string message)
        {
            AppToast.Error(this, title, message);
        }

        private void OnVisibleChanged(object sender, EventArgs e)
        {
            if (!Visible || _initialized || _presenter == null)
                return;

            _initialized = true;
            _presenter.Initialize();
        }

        private void SearchBox_HandleCreated(object sender, EventArgs e)
        {
            ApplySearchPlaceholder();
        }

        private void ApplySearchPlaceholder()
        {
            SendMessage(_searchBox.Handle, EmSetCueBanner, new IntPtr(1),
                "Tìm theo tên sản phẩm, mã SP...");
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents)
                return;

            UpdateClearFilterVisibility();
            _searchTimer.Stop();
            _searchTimer.Start();
        }

        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            _searchTimer.Stop();
            SearchRequested?.Invoke(this, EventArgs.Empty);
        }

        private void FilterControl_Changed(object sender, EventArgs e)
        {
            if (_suppressEvents)
                return;

            if (sender == _fromDatePicker)
            {
                _fromDatePicker.Checked = true;
                _fromDateActive = true;
                _fromDatePicker.CustomFormat = "dd/MM/yyyy";
            }
            else if (sender == _toDatePicker)
            {
                _toDatePicker.Checked = true;
                _toDateActive = true;
                _toDatePicker.CustomFormat = "dd/MM/yyyy";
            }

            UpdateClearFilterVisibility();
            FiltersChanged?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateClearFilterVisibility()
        {
            _clearFilterLink.Visible = HasActiveFilters();
        }

        private bool HasActiveFilters()
        {
            CategoryOption category = _categoryCombo.SelectedItem as CategoryOption;
            return _searchBox.TextLength > 0 || (category != null && category.Id.HasValue) ||
                _fromDateActive || _toDateActive;
        }

        private void ClearFilterLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ClearFiltersRequested?.Invoke(this, EventArgs.Empty);
        }

        private void PageSizeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageSizeChanged?.Invoke(this, EventArgs.Empty);
        }

        private void PreviousButton_Click(object sender, EventArgs e)
        {
            PreviousPageRequested?.Invoke(this, EventArgs.Empty);
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            NextPageRequested?.Invoke(this, EventArgs.Empty);
        }

        private void OptionsButton_Click(object sender, EventArgs e)
        {
            _optionsMenu.Show(_optionsButton, 0, _optionsButton.Height);
        }

        private void ExportCsvItem_Click(object sender, EventArgs e)
        {
            CsvExportRequested?.Invoke(this, EventArgs.Empty);
        }

        private void ExportExcelItem_Click(object sender, EventArgs e)
        {
            ExcelExportRequested?.Invoke(this, EventArgs.Empty);
        }

        private void Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _grid.Rows.Count)
                return;

            StockAlertListItemDto alert = _grid.Rows[e.RowIndex].Tag as StockAlertListItemDto;
            if (alert == null)
                return;

            if (e.ColumnIndex == colPlan.Index &&
                string.Equals(alert.Status, "NEW", StringComparison.OrdinalIgnoreCase))
                PlanAlertRequested?.Invoke(alert.AlertId);
            else if (e.ColumnIndex == colResolve.Index &&
                !string.Equals(alert.Status, "RESOLVED", StringComparison.OrdinalIgnoreCase))
                ResolveAlertRequested?.Invoke(alert.AlertId);
        }

        private void Grid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || e.RowIndex < 0 || e.RowIndex >= _grid.Rows.Count ||
                e.ColumnIndex != colProductCode.Index)
                return;

            StockAlertListItemDto alert = _grid.Rows[e.RowIndex].Tag as StockAlertListItemDto;
            if (alert != null)
                CopyProductCode(alert.ProductCode);
        }

        private void Grid_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _grid.Rows.Count)
            {
                _grid.Cursor = Cursors.Default;
                return;
            }

            StockAlertListItemDto alert = _grid.Rows[e.RowIndex].Tag as StockAlertListItemDto;
            bool enabled = alert != null &&
                ((e.ColumnIndex == colPlan.Index &&
                    string.Equals(alert.Status, "NEW", StringComparison.OrdinalIgnoreCase)) ||
                 (e.ColumnIndex == colResolve.Index &&
                    !string.Equals(alert.Status, "RESOLVED", StringComparison.OrdinalIgnoreCase)));
            _grid.Cursor = enabled ? Cursors.Hand : Cursors.Default;
        }

        private void Grid_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            _grid.Cursor = Cursors.Default;
        }

        private void Grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string text = Convert.ToString(e.Value);
            Color textColor;
            Color fillColor;
            if (e.ColumnIndex == colAlertType.Index)
            {
                if (string.Equals(text, "Hết hàng", StringComparison.Ordinal))
                {
                    textColor = Color.FromArgb(220, 38, 38);
                    fillColor = Color.FromArgb(254, 226, 226);
                }
                else if (string.Equals(text, "Sắp hết hàng", StringComparison.Ordinal))
                {
                    textColor = Color.FromArgb(217, 119, 6);
                    fillColor = Color.FromArgb(254, 243, 199);
                }
                else
                {
                    return;
                }
            }
            else if (e.ColumnIndex == colStatus.Index)
            {
                if (string.Equals(text, "Mới", StringComparison.Ordinal))
                {
                    textColor = Color.FromArgb(217, 119, 6);
                    fillColor = Color.FromArgb(254, 243, 199);
                }
                else if (string.Equals(text, "Đã lên kế hoạch", StringComparison.Ordinal))
                {
                    textColor = Color.FromArgb(37, 99, 235);
                    fillColor = Color.FromArgb(219, 234, 254);
                }
                else if (string.Equals(text, "Đã xử lý", StringComparison.Ordinal))
                {
                    textColor = Color.FromArgb(22, 163, 74);
                    fillColor = Color.FromArgb(220, 252, 231);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }

            e.CellStyle.ForeColor = textColor;
            e.CellStyle.BackColor = fillColor;
            e.CellStyle.SelectionForeColor = textColor;
            e.CellStyle.SelectionBackColor = fillColor;
            e.CellStyle.Font = BadgeFont;
        }

        private void Grid_CellToolTipTextNeeded(
            object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != colProductCode.Index)
                return;

            string productCode = Convert.ToString(_grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            e.ToolTipText = "Click để copy mã sản phẩm: " + productCode;
        }

        private void CopyProductCode(string productCode)
        {
            if (string.IsNullOrEmpty(productCode))
                return;

            try
            {
                Clipboard.SetText(productCode);
                AppToast.Success(this, "Copy thành công", "Đã copy mã sản phẩm: " + productCode);
            }
            catch (Exception exception)
            {
                AppToast.Error(this, "Không thể copy mã sản phẩm", exception.Message);
            }
        }

        private static string GetAlertTypeText(string alertType)
        {
            if (string.Equals(alertType, "OUT_OF_STOCK", StringComparison.OrdinalIgnoreCase))
                return "Hết hàng";
            if (string.Equals(alertType, "LOW_STOCK", StringComparison.OrdinalIgnoreCase))
                return "Sắp hết hàng";
            return alertType ?? string.Empty;
        }

        private static string GetStatusText(string status)
        {
            if (string.Equals(status, "NEW", StringComparison.OrdinalIgnoreCase))
                return "Mới";
            if (string.Equals(status, "PLANNED", StringComparison.OrdinalIgnoreCase))
                return "Đã lên kế hoạch";
            if (string.Equals(status, "RESOLVED", StringComparison.OrdinalIgnoreCase))
                return "Đã xử lý";
            return status ?? string.Empty;
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint message, IntPtr wParam, string lParam);

        private sealed class CategoryOption
        {
            public int? Id { get; private set; }
            public string Name { get; private set; }

            public CategoryOption(int? id, string name)
            {
                Id = id;
                Name = name;
            }
        }
    }
}

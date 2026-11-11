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
using SIMS_WinFormsApp.Services;
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

            _searchBox.HandleCreated += new EventHandler(this.SearchBox_HandleCreated);
            if (_searchBox.IsHandleCreated)
                ApplySearchPlaceholder();
            _searchBox.TextChanged += new EventHandler(this.SearchBox_TextChanged);
            _categoryCombo.SelectedIndexChanged += new EventHandler(this.FilterControl_Changed);
            _fromDatePicker.ValueChanged += new EventHandler(this.FilterControl_Changed);
            _toDatePicker.ValueChanged += new EventHandler(this.FilterControl_Changed);
            _pageSizeCombo.SelectedIndexChanged += new EventHandler(this.PageSizeCombo_SelectedIndexChanged);
            _grid.CellMouseClick += new DataGridViewCellMouseEventHandler(this.Grid_CellMouseClick);
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
        public event EventHandler PageSizeChanged;
        public event EventHandler PreviousPageRequested;
        public event EventHandler NextPageRequested;
        public event EventHandler CsvExportRequested;
        public event EventHandler ExcelExportRequested;

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
                int rowIndex = _grid.Rows.Add(
                    alert.AlertId,
                    alert.ProductCode,
                    alert.ProductName,
                    GetAlertTypeText(alert.AlertType),
                    string.IsNullOrWhiteSpace(alert.ReportedByName) ? "Hệ thống" : alert.ReportedByName,
                    alert.CreatedAt.ToString("dd/MM/yyyy HH:mm", VietnameseCulture),
                    GetStatusText(alert.Status));
                _grid.Rows[rowIndex].Tag = alert;
            }

            _grid.ClearSelection();
            _grid.CurrentCell = null;
            UpdateActionButtons();
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
            _clearFilterButton.Visible = visible;
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
            _clearFilterButton.Visible = HasActiveFilters();
        }

        private bool HasActiveFilters()
        {
            CategoryOption category = _categoryCombo.SelectedItem as CategoryOption;
            return _searchBox.TextLength > 0 || (category != null && category.Id.HasValue) ||
                _fromDateActive || _toDateActive;
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

        // ---------- Nút Lên kế hoạch / Đã xử lý ----------

        private StockAlertListItemDto GetSelectedAlert()
        {
            if (_grid.SelectedRows.Count == 0)
                return null;
            return _grid.SelectedRows[0].Tag as StockAlertListItemDto;
        }

        private void UpdateActionButtons()
        {
            StockAlertListItemDto alert = GetSelectedAlert();
            _planButton.Enabled = alert != null &&
                string.Equals(alert.Status, "NEW", StringComparison.OrdinalIgnoreCase);
            _resolveButton.Enabled = alert != null &&
                !string.Equals(alert.Status, "RESOLVED", StringComparison.OrdinalIgnoreCase);
        }

        private void _grid_SelectionChanged(object sender, EventArgs e)
        {
            UpdateActionButtons();
        }

        private void _planButton_Click(object sender, EventArgs e)
        {
            // 1. Lấy cảnh báo đang chọn
            StockAlertListItemDto alert = GetSelectedAlert();
            if (alert == null)
            {
                ShowError("Chưa chọn cảnh báo", "Vui lòng chọn 1 dòng trong bảng.");
                return;
            }

            // 2. Chỉ cảnh báo "Mới" mới lên kế hoạch được
            if (!string.Equals(alert.Status, "NEW", StringComparison.OrdinalIgnoreCase))
            {
                ShowError("Không thể lên kế hoạch", "Chỉ cảnh báo ở trạng thái \"Mới\" mới lên kế hoạch được.");
                return;
            }

            // 3. Hỏi xác nhận
            IDialogService dialogs = AppComposition.CreateDialogService();
            string message = "Đánh dấu \"" + alert.ProductName +
                "\" đã được lên kế hoạch nhập hàng bổ sung?";
            if (!dialogs.Confirm(FindForm(), "Lên kế hoạch nhập bổ sung", message))
                return;

            // 4. Cập nhật CSDL (Status: NEW -> PLANNED)
            try
            {
                int updated = _service.MarkAlertPlanned(alert.AlertId);

                // 5. Báo kết quả
                if (updated == 0)
                    ShowError("Không thể cập nhật", "Cảnh báo đã được người khác cập nhật. Đang tải lại.");
                else
                    ShowSuccess("Thành công", "Đã đánh dấu \"" + alert.ProductName +
                        "\" đang được lên kế hoạch nhập bổ sung.");
            }
            catch (Exception ex)
            {
                ShowError("Không thể cập nhật", ex.Message);
            }

            // 6. Tải lại bảng
            _presenter.ReloadAlerts();
        }

        private void _resolveButton_Click(object sender, EventArgs e)
        {
            // 1. Lấy cảnh báo đang chọn
            StockAlertListItemDto alert = GetSelectedAlert();
            if (alert == null)
            {
                ShowError("Chưa chọn cảnh báo", "Vui lòng chọn 1 dòng trong bảng.");
                return;
            }

            // 2. Cảnh báo đã xử lý thì không xử lý lại
            if (string.Equals(alert.Status, "RESOLVED", StringComparison.OrdinalIgnoreCase))
            {
                ShowError("Không thể cập nhật", "Cảnh báo này đã được xử lý.");
                return;
            }

            // 3. Kiểm tra người đăng nhập
            if (_session.CurrentUser == null)
            {
                ShowError("Không thể cập nhật", "Không tìm thấy người dùng đang đăng nhập.");
                return;
            }

            // 4. Hỏi xác nhận
            IDialogService dialogs = AppComposition.CreateDialogService();
            string message = "Xác nhận \"" + alert.ProductName +
                "\" đã được nhập hàng bổ sung / không còn cần xử lý?";
            if (!dialogs.Confirm(FindForm(), "Đánh dấu đã xử lý", message))
                return;

            // 5. Cập nhật CSDL (Status -> RESOLVED, ghi ResolvedBy, ResolvedAt)
            try
            {
                int updated = _service.ResolveAlert(alert.AlertId, _session.CurrentUser.UserId);

                // 6. Báo kết quả
                if (updated == 0)
                    ShowError("Không thể cập nhật", "Cảnh báo đã được người khác cập nhật. Đang tải lại.");
                else
                    ShowSuccess("Thành công", "Đã xử lý xong \"" + alert.ProductName + "\".");
            }
            catch (Exception ex)
            {
                ShowError("Không thể cập nhật", ex.Message);
            }

            // 7. Tải lại bảng
            _presenter.ReloadAlerts();
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

        private void _clearFilterButton_Click(object sender, EventArgs e)
        {
            // 1. Dừng tìm kiếm đang chờ
            _searchTimer.Stop();

            // 2. Xóa bộ lọc (tạm tắt sự kiện để không tải lại nhiều lần)
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

            // 3. Ẩn nút Xóa lọc
            _clearFilterButton.Visible = false;

            // 4. Về trang 1 và tải lại bảng
            _presenter.GoToFirstPage();
            _presenter.ReloadAlerts();
        }
    }
}

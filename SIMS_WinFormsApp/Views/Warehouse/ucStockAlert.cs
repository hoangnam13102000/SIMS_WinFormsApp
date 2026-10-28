using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        private readonly IInventoryService _service;
        private readonly IUserSession _session;
        private readonly StockAlertPresenter _presenter;
        private readonly Timer _searchTimer;
        private readonly ToolTip _actionToolTip;
        private bool _initialized;
        private bool _suppressFilterEvents;
        private string _currentActionToolTip;

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
            _searchTimer = new Timer(components) { Interval = 300 };
            _actionToolTip = new ToolTip(components) { ShowAlways = true };

            _searchBox.Clear();
            _searchBox.ForeColor = Color.FromArgb(15, 23, 42);
            _searchBox.HandleCreated += SearchBox_HandleCreated;
            if (_searchBox.IsHandleCreated)
                ApplySearchPlaceholder();
            _searchBox.TextChanged += SearchBox_TextChanged;
            _searchTimer.Tick += SearchTimer_Tick;
            _categoryCombo.SelectedIndexChanged += FilterControl_Changed;
            _fromDate.ValueChanged += FilterControl_Changed;
            _toDate.ValueChanged += FilterControl_Changed;
            _clearFilterLink.LinkClicked += ClearFilterLink_LinkClicked;
            _pageSizeCombo.SelectedIndexChanged += PageSizeCombo_SelectedIndexChanged;
            _prevButton.Click += PreviousButton_Click;
            _nextButton.Click += NextButton_Click;
            _optionsButton.Click += OptionsButton_Click;
            _exportCsvItem.Click += ExportCsvItem_Click;
            _exportExcelItem.Click += ExportExcelItem_Click;
            _grid.CellPainting += Grid_CellPainting;
            _grid.CellMouseClick += Grid_CellMouseClick;
            _grid.CellMouseMove += Grid_CellMouseMove;
            _grid.CellToolTipTextNeeded += Grid_CellToolTipTextNeeded;
            _grid.MouseLeave += Grid_MouseLeave;
            _pageLabel.Paint += PageLabel_Paint;
            VisibleChanged += OnVisibleChanged;
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

        public DateTime? FromDate => _fromDate.Value;
        public DateTime? ToDate => _toDate.Value;
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
            _suppressFilterEvents = true;
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
                _suppressFilterEvents = false;
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
                    alert.AlertType,
                    alert.ReportedByName,
                    alert.CreatedAt,
                    alert.Status,
                    null);
                _grid.Rows[rowIndex].Tag = alert;
            }

            _grid.ClearSelection();
            _grid.CurrentCell = null;
            _totalLabel.Text = "Tổng cộng: " + totalCount.ToString("N0", VietnameseCulture) + " cảnh báo";
            _pageLabel.Text = pageNumber.ToString(CultureInfo.InvariantCulture);
            _pageLabel.Invalidate();
            _pageInfoLabel.Text = "Trang " + pageNumber.ToString(CultureInfo.InvariantCulture) +
                " / " + pageCount.ToString(CultureInfo.InvariantCulture);
            _prevButton.Enabled = pageNumber > 1;
            _nextButton.Enabled = pageNumber < pageCount;
        }

        public void SetClearFilterVisible(bool visible)
        {
            _clearFilterLink.Visible = visible;
        }

        public void ClearFilters()
        {
            _searchTimer.Stop();
            _suppressFilterEvents = true;
            try
            {
                _searchBox.Clear();
                _categoryCombo.SelectedIndex = _categoryCombo.Items.Count > 0 ? 0 : -1;
                _fromDate.Value = null;
                _toDate.Value = null;
            }
            finally
            {
                _suppressFilterEvents = false;
            }
            SetClearFilterVisible(false);
        }

        public void ClearToDate()
        {
            _suppressFilterEvents = true;
            try
            {
                _toDate.Value = null;
            }
            finally
            {
                _suppressFilterEvents = false;
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
            if (_suppressFilterEvents)
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
            if (_suppressFilterEvents)
                return;

            UpdateClearFilterVisibility();
            FiltersChanged?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateClearFilterVisibility()
        {
            CategoryOption category = _categoryCombo.SelectedItem as CategoryOption;
            bool hasCategory = category != null && category.Id.HasValue;
            _clearFilterLink.Visible = _searchBox.TextLength > 0 || hasCategory ||
                _fromDate.Value.HasValue || _toDate.Value.HasValue;
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

        private void Grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            StockAlertListItemDto alert = _grid.Rows[e.RowIndex].Tag as StockAlertListItemDto;
            if (alert == null)
                return;

            if (e.ColumnIndex == colAlertType.Index)
            {
                bool outOfStock = string.Equals(alert.AlertType, "OUT_OF_STOCK", StringComparison.OrdinalIgnoreCase);
                PaintBadge(e, outOfStock ? "Hết hàng" : "Sắp hết hàng",
                    outOfStock ? Color.FromArgb(254, 226, 226) : Color.FromArgb(254, 243, 199),
                    outOfStock ? Color.FromArgb(220, 38, 38) : Color.FromArgb(217, 119, 6));
                return;
            }

            if (e.ColumnIndex == colStatus.Index)
            {
                string label;
                Color fill;
                Color text;
                GetStatusStyle(alert.Status, out label, out fill, out text);
                PaintBadge(e, label, fill, text);
                return;
            }

            if (e.ColumnIndex == colActions.Index)
                PaintActionIcons(e, alert);
        }

        private void PaintBadge(
            DataGridViewCellPaintingEventArgs e, string label, Color fill, Color text)
        {
            e.PaintBackground(e.CellBounds, true);
            e.Paint(e.CellBounds, DataGridViewPaintParts.Border);
            Size textSize = TextRenderer.MeasureText(label, e.CellStyle.Font, Size.Empty,
                TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
            int width = Math.Min(e.CellBounds.Width - 8, textSize.Width + 20);
            var bounds = new Rectangle(
                e.CellBounds.Left + (e.CellBounds.Width - width) / 2,
                e.CellBounds.Top + (e.CellBounds.Height - 26) / 2,
                width,
                26);
            using (GraphicsPath path = CreateRoundedPath(bounds, 13))
            using (var brush = new SolidBrush(fill))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillPath(brush, path);
            }
            TextRenderer.DrawText(e.Graphics, label, e.CellStyle.Font, bounds, text,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
                TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
            e.Handled = true;
        }

        private void PaintActionIcons(DataGridViewCellPaintingEventArgs e, StockAlertListItemDto alert)
        {
            e.PaintBackground(e.CellBounds, true);
            e.Paint(e.CellBounds, DataGridViewPaintParts.Border);
            const int iconSize = 20;
            const int gap = 16;
            const int groupWidth = iconSize * 2 + gap;
            int left = e.CellBounds.Left + (e.CellBounds.Width - groupWidth) / 2;
            int top = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;
            bool canPlan = string.Equals(alert.Status, "NEW", StringComparison.OrdinalIgnoreCase);
            bool canResolve = !string.Equals(alert.Status, "RESOLVED", StringComparison.OrdinalIgnoreCase);
            Color planColor = canPlan ? Color.FromArgb(37, 99, 235) : Color.FromArgb(203, 213, 225);
            Color resolveColor = canResolve ? Color.FromArgb(22, 163, 74) : Color.FromArgb(203, 213, 225);
            Bitmap planIcon = IconBitmapCache.Get(IconChar.CalendarPlus, planColor, new Size(iconSize, iconSize));
            Bitmap resolveIcon = IconBitmapCache.Get(IconChar.CircleCheck, resolveColor, new Size(iconSize, iconSize));
            e.Graphics.DrawImageUnscaled(planIcon, left, top);
            e.Graphics.DrawImageUnscaled(resolveIcon, left + iconSize + gap, top);
            e.Handled = true;
        }

        private void Grid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || e.RowIndex < 0 || e.RowIndex >= _grid.Rows.Count)
                return;

            StockAlertListItemDto alert = _grid.Rows[e.RowIndex].Tag as StockAlertListItemDto;
            if (alert == null)
                return;

            if (e.ColumnIndex == colProductCode.Index)
            {
                CopyProductCode(alert.ProductCode);
                return;
            }

            if (e.ColumnIndex != colActions.Index)
                return;

            ActionIcon action = HitTestAction(e.RowIndex, e.X);
            if (action == ActionIcon.Plan &&
                string.Equals(alert.Status, "NEW", StringComparison.OrdinalIgnoreCase))
                PlanAlertRequested?.Invoke(alert.AlertId);
            else if (action == ActionIcon.Resolve &&
                !string.Equals(alert.Status, "RESOLVED", StringComparison.OrdinalIgnoreCase))
                ResolveAlertRequested?.Invoke(alert.AlertId);
        }

        private void Grid_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != colActions.Index || e.RowIndex >= _grid.Rows.Count)
            {
                ResetActionHover();
                return;
            }

            StockAlertListItemDto alert = _grid.Rows[e.RowIndex].Tag as StockAlertListItemDto;
            if (alert == null)
            {
                ResetActionHover();
                return;
            }

            ActionIcon action = HitTestAction(e.RowIndex, e.X);
            if (action == ActionIcon.None)
            {
                ResetActionHover();
                return;
            }

            bool enabled = action == ActionIcon.Plan
                ? string.Equals(alert.Status, "NEW", StringComparison.OrdinalIgnoreCase)
                : !string.Equals(alert.Status, "RESOLVED", StringComparison.OrdinalIgnoreCase);
            _grid.Cursor = enabled ? Cursors.Hand : Cursors.Default;
            string tooltip = action == ActionIcon.Plan
                ? "Đã lên kế hoạch nhập bổ sung"
                : "Đánh dấu đã xử lý xong";
            if (_currentActionToolTip != tooltip)
            {
                _actionToolTip.Hide(_grid);
                Rectangle cell = _grid.GetCellDisplayRectangle(colActions.Index, e.RowIndex, false);
                _actionToolTip.Show(tooltip, _grid, cell.Left + e.X + 12, cell.Top + e.Y + 20, 2500);
                _currentActionToolTip = tooltip;
            }
        }

        private void Grid_CellToolTipTextNeeded(
            object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == colProductCode.Index)
                e.ToolTipText = "Click để copy mã sản phẩm";
        }

        private void Grid_MouseLeave(object sender, EventArgs e)
        {
            ResetActionHover();
        }

        private ActionIcon HitTestAction(int rowIndex, int cellX)
        {
            Rectangle bounds = _grid.GetCellDisplayRectangle(colActions.Index, rowIndex, false);
            const int iconSize = 20;
            const int gap = 16;
            int left = (bounds.Width - iconSize * 2 - gap) / 2;
            if (cellX >= left && cellX < left + iconSize)
                return ActionIcon.Plan;
            if (cellX >= left + iconSize + gap && cellX < left + iconSize * 2 + gap)
                return ActionIcon.Resolve;
            return ActionIcon.None;
        }

        private void ResetActionHover()
        {
            _grid.Cursor = Cursors.Default;
            _actionToolTip.Hide(_grid);
            _currentActionToolTip = null;
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

        private void PageLabel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (var brush = new SolidBrush(Color.FromArgb(37, 99, 235)))
                e.Graphics.FillEllipse(brush, 1, 1, _pageLabel.Width - 2, _pageLabel.Height - 2);
            TextRenderer.DrawText(e.Graphics, _pageLabel.Text, _pageLabel.Font,
                _pageLabel.ClientRectangle, Color.White,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
                TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
        }

        private static void GetStatusStyle(string status, out string label, out Color fill, out Color text)
        {
            if (string.Equals(status, "PLANNED", StringComparison.OrdinalIgnoreCase))
            {
                label = "Đã lên kế hoạch";
                fill = Color.FromArgb(219, 234, 254);
                text = Color.FromArgb(37, 99, 235);
            }
            else if (string.Equals(status, "RESOLVED", StringComparison.OrdinalIgnoreCase))
            {
                label = "Đã xử lý";
                fill = Color.FromArgb(220, 252, 231);
                text = Color.FromArgb(22, 163, 74);
            }
            else
            {
                label = "Mới";
                fill = Color.FromArgb(254, 243, 199);
                text = Color.FromArgb(217, 119, 6);
            }
        }

        private static GraphicsPath CreateRoundedPath(Rectangle bounds, int radius)
        {
            int diameter = Math.Min(radius * 2, Math.Min(bounds.Width, bounds.Height));
            var path = new GraphicsPath();
            path.AddArc(bounds.Left, bounds.Top, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Top, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.Left, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint message, IntPtr wParam, string lParam);

        private enum ActionIcon
        {
            None,
            Plan,
            Resolve
        }

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

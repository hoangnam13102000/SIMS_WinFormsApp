using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.MVP.Presenters.Inventory;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;
using SIMS_WinFormsApp.Views.Toast;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    public partial class ucPurchaseReceiptManagement : UserControl, IPurchaseReceiptListView
    {
        private const uint EmSetCueBanner = 0x1501;
        private static readonly CultureInfo VietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");
        private readonly IInventoryService _service;
        private readonly PurchaseReceiptListPresenter _presenter;
        private readonly Timer _searchTimer;
        private readonly ContextMenuStrip _optionsMenu;
        private bool _initialized;
        private bool _suppressDateEvents;

        public ucPurchaseReceiptManagement() : this(null)
        {
        }

        public ucPurchaseReceiptManagement(IInventoryService service)
        {
            InitializeComponent();
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            _service = service ?? AppComposition.CreateInventoryService();
            _presenter = new PurchaseReceiptListPresenter(this, _service);
            _searchTimer = new Timer(components) { Interval = 300 };
            _searchTimer.Tick += (sender, args) =>
            {
                _searchTimer.Stop();
                SearchRequested?.Invoke(this, EventArgs.Empty);
            };

            _searchBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            _searchBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            _searchBox.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            _searchBox.HandleCreated += SearchBox_HandleCreated;
            if (_searchBox.IsHandleCreated)
                ApplySearchPlaceholder();
            _searchBox.TextChanged += OnSearchTextChanged;
            _clearSearchButton.Click += (sender, args) =>
            {
                _searchBox.Clear();
                _searchBox.Focus();
            };

            _fromDate.ValueChanged += OnDateValueChanged;
            _toDate.ValueChanged += OnDateValueChanged;
            _clearDateLink.Click += (sender, args) => ClearDatesRequested?.Invoke(this, EventArgs.Empty);
            _pageSizeCombo.SelectedIndexChanged += (sender, args) =>
                PageSizeChanged?.Invoke(this, EventArgs.Empty);
            _prevButton.Click += (sender, args) => PreviousPageRequested?.Invoke(this, EventArgs.Empty);
            _nextButton.Click += (sender, args) => NextPageRequested?.Invoke(this, EventArgs.Empty);
            _createButton.Click += (sender, args) => CreateRequested?.Invoke(this, EventArgs.Empty);

            _optionsMenu = new ContextMenuStrip(components);
            _optionsMenu.Items.Add(new ToolStripMenuItem("Xuất CSV",
                IconBitmapCache.Get(IconChar.FileCsv, AppColors.TableViewAction, new Size(18, 18)),
                (sender, args) => CsvExportRequested?.Invoke(this, EventArgs.Empty)));
            _optionsMenu.Items.Add(new ToolStripMenuItem("Xuất Excel",
                IconBitmapCache.Get(IconChar.FileExcel, AppColors.TableViewAction, new Size(18, 18)),
                (sender, args) => ExcelExportRequested?.Invoke(this, EventArgs.Empty)));
            _optionsButton.Click += (sender, args) =>
                _optionsMenu.Show(_optionsButton, 0, _optionsButton.Height);

            _grid.CellPainting += Grid_CellPainting;
            _grid.CellClick += Grid_CellClick;
            _grid.CellDoubleClick += Grid_CellDoubleClick;
            _grid.CellToolTipTextNeeded += Grid_CellToolTipTextNeeded;
            _grid.CellMouseMove += Grid_CellMouseMove;
            _grid.MouseLeave += (sender, args) => _grid.Cursor = Cursors.Default;
            _pageNumberBadge.Paint += PageNumberBadge_Paint;
            _headerIconCircle.Paint += HeaderIconCircle_Paint;
            VisibleChanged += OnVisibleChanged;
        }

        public event EventHandler SearchRequested;
        public event EventHandler DateFilterChanged;
        public event EventHandler ClearDatesRequested;
        public event EventHandler PageSizeChanged;
        public event EventHandler PreviousPageRequested;
        public event EventHandler NextPageRequested;
        public event EventHandler CreateRequested;
        public event EventHandler CsvExportRequested;
        public event EventHandler ExcelExportRequested;
        public event Action<string> ReceiptCodeCopyRequested;
        public event Action<int> DetailRequested;

        public string Keyword => _searchBox.Text;
        public DateTime? FromDate => _fromDate.Value;
        public DateTime? ToDate => _toDate.Value;
        public int PageSize => _pageSizeCombo.SelectedItem == null
            ? 10
            : Convert.ToInt32(_pageSizeCombo.SelectedItem, CultureInfo.InvariantCulture);
        public IWin32Window Owner => FindForm();

        public void DisplayPage(
            IReadOnlyList<PurchaseReceiptListItemDto> rows,
            int totalCount,
            int pageNumber,
            int pageCount)
        {
            _grid.Rows.Clear();
            foreach (PurchaseReceiptListItemDto receipt in rows ?? Array.Empty<PurchaseReceiptListItemDto>())
            {
                int rowIndex = _grid.Rows.Add(
                    receipt.ReceiptId,
                    receipt.ReceiptCode,
                    receipt.SupplierName,
                    receipt.CreatedByName,
                    receipt.CreatedAt,
                    receipt.ItemCount,
                    receipt.TotalAmount,
                    receipt.Status,
                    string.Empty);
                _grid.Rows[rowIndex].Tag = receipt.ReceiptId;
            }

            _grid.ClearSelection();
            _grid.CurrentCell = null;
            _totalLabel.Text = "Tổng cộng: " + totalCount.ToString("N0", VietnameseCulture) + " phiếu nhập kho";
            _pageNumberLabel.Text = pageNumber.ToString(CultureInfo.InvariantCulture);
            _pageInfoLabel.Text = "Trang " + pageNumber.ToString(CultureInfo.InvariantCulture) +
                " / " + pageCount.ToString(CultureInfo.InvariantCulture);
            _prevButton.Enabled = pageNumber > 1;
            _nextButton.Enabled = pageNumber < pageCount;
        }

        public void BindSuggestions(IReadOnlyList<string> suggestions)
        {
            var source = new AutoCompleteStringCollection();
            if (suggestions != null)
                source.AddRange(suggestions.Where(value => !string.IsNullOrWhiteSpace(value)).Distinct().ToArray());
            _searchBox.AutoCompleteCustomSource = source;
        }

        public void ClearToDate()
        {
            _suppressDateEvents = true;
            try
            {
                _toDate.Value = null;
            }
            finally
            {
                _suppressDateEvents = false;
            }
            UpdateClearDateVisibility();
        }

        public void ClearDates()
        {
            _suppressDateEvents = true;
            try
            {
                _fromDate.Value = null;
                _toDate.Value = null;
            }
            finally
            {
                _suppressDateEvents = false;
            }
            UpdateClearDateVisibility();
        }

        public void CopyReceiptCode(string receiptCode)
        {
            if (string.IsNullOrEmpty(receiptCode))
                return;

            try
            {
                Clipboard.SetText(receiptCode);
                AppToast.Success(this, "Copy thành công", "Đã copy mã phiếu nhập: " + receiptCode);
            }
            catch (Exception exception)
            {
                AppToast.Error(this, "Không thể copy mã phiếu", exception.Message);
            }
        }

        public void ShowInvalidDateRange()
        {
            AppToast.Warning(this, "Khoảng ngày không hợp lệ",
                "Ngày \"đến\" phải lớn hơn hoặc bằng ngày \"từ\".");
        }

        public void ShowError(string title, string message)
        {
            AppToast.Error(this, title, message);
        }

        public void OpenCreateForm()
        {
            using (var form = new frmPurchaseReceiptEditor(_service, UserSession.Instance))
                if (form.ShowDialog(FindForm()) == DialogResult.OK)
                    _presenter.RefreshAfterCreate();
        }

        public void OpenDetail(PurchaseReceiptListItemDto receipt)
        {
            if (receipt == null)
                return;

            try
            {
                IReadOnlyList<PurchaseReceiptDetailLineDto> lines =
                    _service.GetPurchaseReceiptDetails(receipt.ReceiptId);
                using (var dialog = new frmPurchaseReceiptDetail(receipt, lines))
                    dialog.ShowDialog(FindForm());
            }
            catch (Exception exception)
            {
                AppToast.Error(this, "Lỗi tải chi tiết phiếu nhập", exception.Message);
            }
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
                "Tìm mã phiếu, mã sản phẩm, tên sản phẩm, nhà cung cấp...");
        }

        private void OnSearchTextChanged(object sender, EventArgs e)
        {
            _clearSearchButton.Visible = _searchBox.TextLength > 0;
            _searchTimer.Stop();
            _searchTimer.Start();
        }

        private void OnDateValueChanged(object sender, EventArgs e)
        {
            UpdateClearDateVisibility();
            if (!_suppressDateEvents)
                DateFilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateClearDateVisibility()
        {
            _clearDateLink.Visible = _fromDate.Value.HasValue || _toDate.Value.HasValue;
        }

        private void Grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == _codeColumn.Index)
            {
                e.PaintBackground(e.CellBounds, true);
                e.Paint(e.CellBounds, DataGridViewPaintParts.Border);

                var textBounds = new Rectangle(
                    e.CellBounds.Left + 10,
                    e.CellBounds.Top,
                    Math.Max(1, e.CellBounds.Width - 40),
                    e.CellBounds.Height);
                TextRenderer.DrawText(e.Graphics, Convert.ToString(e.FormattedValue, CultureInfo.CurrentCulture),
                    e.CellStyle.Font, textBounds, e.CellStyle.ForeColor,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.NoPadding);

                Bitmap copyIcon = IconBitmapCache.Get(
                    IconChar.Copy, AppColors.Accent, new Size(14, 14));
                var iconBounds = new Rectangle(
                    e.CellBounds.Right - 24,
                    e.CellBounds.Top + (e.CellBounds.Height - 14) / 2,
                    14,
                    14);
                e.Graphics.DrawImage(copyIcon, iconBounds);
                e.Handled = true;
                return;
            }

            if (e.ColumnIndex == _actionColumn.Index)
            {
                e.PaintBackground(e.CellBounds, true);
                Bitmap eyeIcon = IconBitmapCache.Get(
                    IconChar.Eye, AppColors.TableViewAction, new Size(20, 20));
                int x = e.CellBounds.Left + (e.CellBounds.Width - eyeIcon.Width) / 2;
                int y = e.CellBounds.Top + (e.CellBounds.Height - eyeIcon.Height) / 2;
                e.Graphics.DrawImageUnscaled(eyeIcon, x, y);
                e.Handled = true;
                return;
            }

            if (e.ColumnIndex == _statusColumn.Index)
            {
                e.PaintBackground(e.CellBounds, true);
                e.Paint(e.CellBounds, DataGridViewPaintParts.Border);

                string status = Convert.ToString(e.FormattedValue, CultureInfo.InvariantCulture);
                bool cancelled = string.Equals(status, "CANCELLED", StringComparison.OrdinalIgnoreCase);
                string label = cancelled ? "Đã hủy" : "Hoàn tất";
                Color fill = cancelled ? Color.FromArgb(254, 226, 226) : Color.FromArgb(220, 252, 231);
                Color text = cancelled ? Color.FromArgb(185, 28, 28) : Color.FromArgb(22, 101, 52);
                Size labelSize = TextRenderer.MeasureText(label, e.CellStyle.Font,
                    Size.Empty, TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
                int badgeWidth = labelSize.Width + 20;
                var badgeBounds = new Rectangle(
                    e.CellBounds.Left + (e.CellBounds.Width - badgeWidth) / 2,
                    e.CellBounds.Top + (e.CellBounds.Height - 28) / 2,
                    badgeWidth,
                    28);
                using (GraphicsPath path = CreateRoundedPath(badgeBounds, 14))
                using (var brush = new SolidBrush(fill))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.FillPath(brush, path);
                }
                TextRenderer.DrawText(e.Graphics, label, e.CellStyle.Font, badgeBounds, text,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
                    TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
                e.Handled = true;
            }
        }

        private void Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _grid.Rows.Count)
                return;

            int receiptId = Convert.ToInt32(_grid.Rows[e.RowIndex].Tag, CultureInfo.InvariantCulture);
            if (e.ColumnIndex == _codeColumn.Index)
                ReceiptCodeCopyRequested?.Invoke(Convert.ToString(
                    _grid.Rows[e.RowIndex].Cells[_codeColumn.Index].Value, CultureInfo.CurrentCulture));
            else if (e.ColumnIndex == _actionColumn.Index)
                DetailRequested?.Invoke(receiptId);
        }

        private void Grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _grid.Rows.Count)
                return;

            int receiptId = Convert.ToInt32(_grid.Rows[e.RowIndex].Tag, CultureInfo.InvariantCulture);
            DetailRequested?.Invoke(receiptId);
        }

        private void Grid_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == _actionColumn.Index)
                e.ToolTipText = "Xem chi tiết";
            else if (e.RowIndex >= 0 && e.ColumnIndex == _codeColumn.Index)
                e.ToolTipText = "Sao chép mã phiếu";
        }

            private void Grid_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
            {
                _grid.Cursor = e.RowIndex >= 0 && e.ColumnIndex == _actionColumn.Index
                ? Cursors.Hand
                : Cursors.Default;
            }

        private void PageNumberBadge_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (var brush = new SolidBrush(Color.FromArgb(37, 99, 235)))
                e.Graphics.FillEllipse(brush, 1, 1, _pageNumberBadge.Width - 2, _pageNumberBadge.Height - 2);
        }

        private void HeaderIconCircle_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (var brush = new SolidBrush(Color.FromArgb(239, 246, 255)))
                e.Graphics.FillEllipse(brush, 1, 1, _headerIconCircle.Width - 2, _headerIconCircle.Height - 2);
        }

        private static GraphicsPath CreateRoundedPath(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
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
    }
}
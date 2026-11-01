using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.Services.Implementations.Inventory;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    public partial class ucExpiryBatchManagement : UserControl
    {
        private static readonly CultureInfo VietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");
        private readonly ExpiryBatchManagementService _service;
        private readonly bool _canDispose;
        private readonly Timer _searchTimer;
        private bool _isLoading;
        private bool _reloadRequested;
        private int? _filterProductId;

        public event Action<int> DisposeBatchRequested;

        public ucExpiryBatchManagement() : this(false)
        {
        }

        public ucExpiryBatchManagement(bool canDispose)
        {
            InitializeComponent();
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            _canDispose = canDispose;
            _service = AppComposition.CreateExpiryBatchManagementService();
            _searchTimer = new Timer(components) { Interval = 300 };
            _searchTimer.Tick += async (sender, args) =>
            {
                _searchTimer.Stop();
                await LoadBatchesAsync();
            };
            _filterCombo.SelectedIndexChanged += async (sender, args) => await LoadBatchesAsync();
            _searchBox.TextChanged += (sender, args) =>
            {
                _searchTimer.Stop();
                _searchTimer.Start();
            };
            _refreshButton.Click += async (sender, args) => await LoadBatchesAsync();
            _disposeButton.Enabled = false;
            _disposeButton.Visible = _canDispose;
            _disposeButton.Click += (sender, args) =>
            {
                ExpiryBatchDto selected = GetSelectedBatch();
                if (selected != null && selected.IsExpired)
                    DisposeBatchRequested?.Invoke(selected.BatchId);
            };
            _clearProductFilterButton.Visible = false;
            _clearProductFilterButton.Click += async (sender, args) =>
            {
                _filterProductId = null;
                _clearProductFilterButton.Visible = false;
                _filterCombo.SelectedIndex = 0;
                _searchBox.Clear();
                _searchTimer.Stop();
                await LoadBatchesAsync();
            };
            _grid.SelectionChanged += (sender, args) => UpdateDisposeButton();
            Load += async (sender, args) => await LoadBatchesAsync();
            VisibleChanged += async (sender, args) =>
            {
                if (Visible && IsHandleCreated)
                    await LoadBatchesAsync();
            };
        }

        private async Task LoadBatchesAsync()
        {
            if (_isLoading)
            {
                _reloadRequested = true;
                return;
            }

            _isLoading = true;
            _refreshButton.Enabled = false;
            try
            {
                ExpiryBatchFilter filter = _filterCombo.SelectedIndex == 1
                    ? ExpiryBatchFilter.NearExpiry
                    : _filterCombo.SelectedIndex == 2 ? ExpiryBatchFilter.Expired : ExpiryBatchFilter.All;
                var rows = await Task.Run(() =>
                    _service.GetExpiryBatches(_searchBox.Text, filter, _filterProductId));
                DisplayBatches(rows);
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, "Không thể tải danh sách lô hàng.\r\n\r\n" + exception.Message,
                    "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoading = false;
                _refreshButton.Enabled = true;
                if (_reloadRequested)
                {
                    _reloadRequested = false;
                    await LoadBatchesAsync();
                }
            }
        }

        private void DisplayBatches(System.Collections.Generic.IReadOnlyList<ExpiryBatchDto> rows)
        {
            _grid.Rows.Clear();
            long expiredQuantity = 0;
            int nearExpiryCount = 0;
            foreach (ExpiryBatchDto batch in rows)
            {
                string status;
                Color rowColor = Color.White;
                if (batch.IsExpired)
                {
                    status = "Đã hết hạn";
                    rowColor = Color.FromArgb(254, 242, 242);
                    expiredQuantity += batch.RemainingQuantity;
                }
                else if (batch.ExpiryDate.Date <= DateTime.Today.AddDays(30))
                {
                    status = "Sắp hết hạn";
                    rowColor = Color.FromArgb(255, 251, 235);
                    nearExpiryCount++;
                }
                else
                {
                    status = "Còn hạn";
                }

                int rowIndex = _grid.Rows.Add(
                    batch.BatchId,
                    batch.BatchCode,
                    batch.ProductCode + " · " + batch.ProductName,
                    batch.LotNumber,
                    batch.SupplierName,
                    batch.ExpiryDate.ToString("dd/MM/yyyy", VietnameseCulture),
                    batch.RemainingQuantity.ToString("N0", VietnameseCulture),
                    batch.UnitCost.ToString("N0", VietnameseCulture) + " ₫",
                    batch.LossAmount.ToString("N0", VietnameseCulture) + " ₫",
                    status);
                _grid.Rows[rowIndex].Tag = batch;
                _grid.Rows[rowIndex].DefaultCellStyle.BackColor = rowColor;
                if (batch.IsExpired)
                    _grid.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.FromArgb(153, 27, 27);
            }

            _expiredValue.Text = expiredQuantity.ToString("N0", VietnameseCulture);
            _nearExpiryValue.Text = nearExpiryCount.ToString("N0", VietnameseCulture);
            _totalValue.Text = rows.Count.ToString("N0", VietnameseCulture);
            _footerLabel.Text = rows.Count == 0
                ? "Không có lô hàng phù hợp với bộ lọc."
                : rows.Count.ToString("N0", VietnameseCulture)
                    + " lô còn hàng · Hàng hết hạn không được bán; tiêu hủy sẽ ghi phiếu và trừ tồn kho.";
            UpdateDisposeButton();
        }

        private void UpdateDisposeButton()
        {
            ExpiryBatchDto selected = GetSelectedBatch();
            _disposeButton.Enabled = _canDispose && selected != null && selected.IsExpired;
        }

        public void FilterByProductId(int productId)
        {
            if (productId <= 0)
                throw new ArgumentOutOfRangeException(nameof(productId));

            _filterProductId = productId;
            _clearProductFilterButton.Visible = true;
            _searchBox.Clear();
            _searchTimer.Stop();
            _filterCombo.SelectedIndex = 2;
            _ = LoadBatchesAsync();
        }

        private ExpiryBatchDto GetSelectedBatch()
        {
            return _grid.CurrentRow == null
                ? null
                : _grid.CurrentRow.Tag as ExpiryBatchDto;
        }
    }
}

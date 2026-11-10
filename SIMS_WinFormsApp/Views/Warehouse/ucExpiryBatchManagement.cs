using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.Services.Implementations.Inventory;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    public partial class ucExpiryBatchManagement : UserControl
    {
        private static readonly CultureInfo VietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");
        private readonly ExpiryBatchManagementService _service;
        private readonly bool _canDispose;
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

            DataGridViewCodeCopy.Attach(_grid, "BatchCode");
            _canDispose = canDispose;
            _service = AppComposition.CreateExpiryBatchManagementService();
            _disposeButton.Enabled = false;
            _disposeButton.Visible = _canDispose;
            _clearProductFilterButton.Visible = false;
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

        private void _searchBox_TextChanged(object sender, EventArgs e)
        {
            _searchTimer.Stop();
            _searchTimer.Start();
        }

        private async void _searchTimer_Tick(object sender, EventArgs e)
        {
            _searchTimer.Stop();
            await LoadBatchesAsync();
        }

        private async void _refreshButton_Click(object sender, EventArgs e)
        {
            await LoadBatchesAsync();
        }

        private void _disposeButton_Click(object sender, EventArgs e)
        {
            ExpiryBatchDto selected = GetSelectedBatch();
            if (selected != null && selected.IsExpired)
                DisposeBatchRequested?.Invoke(selected.BatchId);
        }

        private void _clearProductFilterButton_Click(object sender, EventArgs e)
        {
            _filterProductId = null;
            _clearProductFilterButton.Visible = false;
            _filterCombo.SelectedIndex = 0;
            _searchBox.Clear();
            _searchTimer.Stop();
            _ = LoadBatchesAsync();
        }

        private void _grid_SelectionChanged(object sender, EventArgs e)
        {
            UpdateDisposeButton();
        }
    }
}

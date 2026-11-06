using System;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.Services.Implementations.Inventory;
using SIMS_WinFormsApp.Services.Session;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    public sealed partial class ucExpiredBatchDisposal : UserControl
    {
        private static readonly CultureInfo VietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");
        private readonly ExpiryBatchManagementService _service;
        private ExpiryBatchDto _batch;

        public event EventHandler ReturnRequested;
        public event EventHandler DisposalCompleted;

        public ucExpiredBatchDisposal(ExpiryBatchManagementService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            InitializeComponent();
        }

        private async void _confirmButton_Click(object sender, EventArgs e)
        {
            await CreateDisposalAsync();
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            ReturnRequested?.Invoke(this, EventArgs.Empty);
        }

        public async Task LoadBatchAsync(int batchId)
        {
            _batch = null;
            _confirmButton.Enabled = false;
            _statusLabel.ForeColor = Color.FromArgb(100, 116, 139);
            _statusLabel.Text = "Đang tải thông tin lô...";
            try
            {
                _batch = await Task.Run(() => _service.GetExpiryBatch(batchId));
                if (_batch == null)
                {
                    _statusLabel.ForeColor = Color.FromArgb(153, 27, 27);
                    _statusLabel.Text = "Lô không còn tồn tại hoặc không còn số lượng để xử lý.";
                    ClearDetails();
                    return;
                }

                _productValue.Text = _batch.ProductCode + " · " + _batch.ProductName;
                _batchValue.Text = _batch.BatchCode + " (ID " +
                    _batch.BatchId.ToString(CultureInfo.InvariantCulture) + ")";
                _expiryValue.Text = _batch.ExpiryDate.ToString("dd/MM/yyyy", VietnameseCulture);
                _quantityValue.Text = _batch.RemainingQuantity.ToString("N0", VietnameseCulture);
                _lossValue.Text = _batch.LossAmount.ToString("N0", VietnameseCulture) + " ₫";
                bool expired = _batch.IsExpired;
                _statusLabel.ForeColor = expired
                    ? Color.FromArgb(153, 27, 27)
                    : Color.FromArgb(180, 83, 9);
                _statusLabel.Text = expired
                    ? "Lô đã hết hạn. Xác nhận sẽ tạo phiếu tiêu hủy và trừ số lượng này khỏi tồn kho."
                    : "Lô này chưa hết hạn nên không thể lập phiếu tiêu hủy.";
                _confirmButton.Enabled = expired;
            }
            catch (Exception exception)
            {
                _statusLabel.ForeColor = Color.FromArgb(153, 27, 27);
                _statusLabel.Text = "Không thể tải thông tin lô: " + exception.Message;
                ClearDetails();
            }
        }

        private async Task CreateDisposalAsync()
        {
            if (_batch == null || !_batch.IsExpired || _confirmButton.Enabled == false)
                return;

            DialogResult result = MessageBox.Show(
                this,
                "Lập phiếu tiêu hủy toàn bộ " +
                    _batch.RemainingQuantity.ToString("N0", VietnameseCulture) +
                    " sản phẩm của lô " + _batch.BatchCode + "?\r\n" +
                    "Giá trị thiệt hại: " + _batch.LossAmount.ToString("N0", VietnameseCulture) +
                    " ₫.\r\nThao tác sẽ trừ tồn kho và không thể hoàn tác.",
                "Xác nhận tiêu hủy",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
                return;

            _confirmButton.Enabled = false;
            try
            {
                int userId = UserSession.Instance.CurrentUser?.UserId ?? 0;
                string disposalCode = await Task.Run(
                    () => _service.DisposeExpiredBatch(_batch.BatchId, userId));
                MessageBox.Show(this, "Đã lập phiếu tiêu hủy " + disposalCode + ".",
                    "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisposalCompleted?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception exception)
            {
                _statusLabel.ForeColor = Color.FromArgb(153, 27, 27);
                _statusLabel.Text = "Không thể lập phiếu tiêu hủy: " + exception.Message;
                _confirmButton.Enabled = true;
            }
        }

        private void ClearDetails()
        {
            _productValue.Text = "—";
            _batchValue.Text = "—";
            _expiryValue.Text = "—";
            _quantityValue.Text = "—";
            _lossValue.Text = "—";
        }
    }
}

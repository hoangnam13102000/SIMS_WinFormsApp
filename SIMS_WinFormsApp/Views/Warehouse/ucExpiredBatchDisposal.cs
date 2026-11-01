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
    public sealed class ucExpiredBatchDisposal : UserControl
    {
        private static readonly CultureInfo VietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");
        private readonly ExpiryBatchManagementService _service;
        private readonly Label _statusLabel;
        private readonly Label _productValue;
        private readonly Label _batchValue;
        private readonly Label _expiryValue;
        private readonly Label _quantityValue;
        private readonly Label _lossValue;
        private readonly Button _confirmButton;
        private ExpiryBatchDto _batch;

        public event EventHandler ReturnRequested;
        public event EventHandler DisposalCompleted;

        public ucExpiredBatchDisposal(ExpiryBatchManagementService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            BackColor = Color.FromArgb(244, 247, 250);
            Padding = new Padding(28);

            var root = new TableLayoutPanel
            {
                ColumnCount = 1,
                RowCount = 4,
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 290F));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Controls.Add(root);

            var title = new Label
            {
                Text = "Lập phiếu tiêu hủy hàng hết hạn",
                AutoSize = true,
                Font = new Font("Segoe UI", 21F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(0, 0)
            };
            var subtitle = new Label
            {
                Text = "Kiểm tra đúng lô và số lượng trước khi xác nhận trừ tồn kho.",
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(2, 50)
            };
            var header = new Panel { Dock = DockStyle.Fill };
            header.Controls.Add(title);
            header.Controls.Add(subtitle);
            root.Controls.Add(header, 0, 0);

            _statusLabel = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(153, 27, 27),
                Text = "Đang tải thông tin lô..."
            };
            root.Controls.Add(_statusLabel, 0, 1);

            var details = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 5,
                Dock = DockStyle.Top,
                Height = 260,
                BackColor = Color.White,
                Padding = new Padding(18),
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
            };
            details.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            details.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68F));
            for (int i = 0; i < 5; i++)
                details.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));

            _productValue = AddDetailRow(details, 0, "Sản phẩm");
            _batchValue = AddDetailRow(details, 1, "Mã lô");
            _expiryValue = AddDetailRow(details, 2, "Hạn sử dụng");
            _quantityValue = AddDetailRow(details, 3, "Số lượng tiêu hủy");
            _lossValue = AddDetailRow(details, 4, "Giá trị thiệt hại");
            root.Controls.Add(details, 0, 2);

            var buttons = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                Padding = new Padding(0, 16, 0, 0)
            };
            _confirmButton = new Button
            {
                Text = "Xác nhận lập phiếu",
                Width = 180,
                Height = 40,
                Enabled = false,
                BackColor = Color.FromArgb(220, 38, 38),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Margin = new Padding(8, 0, 0, 0)
            };
            _confirmButton.FlatAppearance.BorderSize = 0;
            _confirmButton.Click += async (sender, args) => await CreateDisposalAsync();
            var cancelButton = new Button
            {
                Text = "Quay lại danh sách lô",
                Width = 180,
                Height = 40,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F),
                Margin = new Padding(8, 0, 0, 0)
            };
            cancelButton.Click += (sender, args) => ReturnRequested?.Invoke(this, EventArgs.Empty);
            buttons.Controls.Add(_confirmButton);
            buttons.Controls.Add(cancelButton);
            root.Controls.Add(buttons, 0, 3);
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

        private static Label AddDetailRow(TableLayoutPanel table, int row, string caption)
        {
            table.Controls.Add(new Label
            {
                Text = caption,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(71, 85, 105),
                Padding = new Padding(4, 0, 0, 0)
            }, 0, row);
            var value = new Label
            {
                Text = "—",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(8, 0, 0, 0),
                AutoEllipsis = true
            };
            table.Controls.Add(value, 1, row);
            return value;
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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    public sealed partial class frmStockDisposalDetail : Form
    {
        private static readonly CultureInfo VietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");
        private static readonly Font ExpiryEmphasisFont = new Font("Segoe UI", 9.75F, FontStyle.Bold);
        private readonly StockDisposalListItemDto _disposal;
        private readonly IReadOnlyList<StockDisposalDetailLineDto> _lines;

        public frmStockDisposalDetail()
            : this(null, null)
        {
        }

        public frmStockDisposalDetail(
            StockDisposalListItemDto disposal,
            IReadOnlyList<StockDisposalDetailLineDto> lines)
        {
            InitializeComponent();
            _disposal = disposal ?? new StockDisposalListItemDto();
            _lines = lines ?? new List<StockDisposalDetailLineDto>();
            DataGridViewCodeCopy.Attach(_grid, "colProductCode", "colBatchCode");
            ShowDisposal();
            _closeButton.Click += new EventHandler(_closeButton_Click);
        }

        private void ShowDisposal()
        {
            bool cancelled = _disposal.IsCancelled;
            Color statusColor = cancelled
                ? Color.FromArgb(220, 38, 38)
                : Color.FromArgb(22, 163, 74);
            Color statusBackColor = cancelled
                ? Color.FromArgb(254, 226, 226)
                : Color.FromArgb(220, 252, 231);

            _disposalCodeLabel.Text = string.IsNullOrWhiteSpace(_disposal.DisposalCode)
                ? "—"
                : _disposal.DisposalCode;
            _subtitleLabel.Text = _disposal.ReasonLabel + "  ·  " + _disposal.StatusText;
            _statusLabel.Text = _disposal.StatusText;
            _statusLabel.ForeColor = statusColor;
            _statusLabel.BackColor = statusBackColor;
            _statusValueLabel.Text = _disposal.StatusText;
            _statusValueLabel.ForeColor = statusColor;
            _headerIconPanel.BackColor = statusBackColor;
            _headerIcon.BackColor = statusBackColor;
            _headerIcon.ForeColor = statusColor;
            _headerIcon.IconColor = statusColor;

            _reasonValueLabel.Text = string.IsNullOrWhiteSpace(_disposal.ReasonLabel)
                ? "—"
                : _disposal.ReasonLabel;
            _createdByValueLabel.Text = string.IsNullOrWhiteSpace(_disposal.CreatedByName)
                ? "—"
                : _disposal.CreatedByName;
            _createdAtValueLabel.Text = _disposal.CreatedAt.ToString("dd/MM/yyyy HH:mm", VietnameseCulture);
            _itemCountValueLabel.Text = _lines.Count.ToString("N0", VietnameseCulture);
            _noteValueLabel.Text = string.IsNullOrWhiteSpace(_disposal.Note) ? "—" : _disposal.Note;

            decimal totalLoss = _disposal.TotalLossAmount;
            if (totalLoss == 0m && _lines.Count > 0)
            {
                foreach (StockDisposalDetailLineDto line in _lines)
                    totalLoss += line.LossAmount;
            }
            string lossText = totalLoss.ToString("N0", VietnameseCulture) + " đ";
            _lossValueLabel.Text = lossText;
            _totalValueLabel.Text = lossText;
            _productCountLabel.Text = "Danh sách sản phẩm (" +
                _lines.Count.ToString(CultureInfo.InvariantCulture) + ")";

            PopulateLines();
        }

        private void PopulateLines()
        {
            _grid.Rows.Clear();
            DateTime expiryThreshold = DateTime.Today.AddDays(30);
            bool hasExpiringLine = false;

            foreach (StockDisposalDetailLineDto line in _lines)
            {
                string batchCode = string.IsNullOrWhiteSpace(line.BatchCode) ? "—" : line.BatchCode;
                string expiryText = line.ExpiryDate.HasValue
                    ? line.ExpiryDate.Value.ToString("dd/MM/yyyy", VietnameseCulture)
                    : "—";
                int rowIndex = _grid.Rows.Add(
                    line.ProductCode ?? string.Empty,
                    line.ProductName ?? string.Empty,
                    batchCode,
                    expiryText,
                    line.Quantity.ToString("N0", VietnameseCulture),
                    line.UnitCost.ToString("N0", VietnameseCulture),
                    line.LossAmount.ToString("N0", VietnameseCulture));

                if (line.ExpiryDate.HasValue && line.ExpiryDate.Value.Date <= expiryThreshold)
                {
                    hasExpiringLine = true;
                    DataGridViewCell expiryCell = _grid.Rows[rowIndex].Cells[colExpiryDate.Index];
                    expiryCell.Style.ForeColor = Color.FromArgb(220, 38, 38);
                    expiryCell.Style.Font = ExpiryEmphasisFont;
                }
            }

            _expiryAlertPanel.Visible = hasExpiringLine;
            _grid.ClearSelection();
            _grid.CurrentCell = null;
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
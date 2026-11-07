using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using SIMS_WinFormsApp.Models.DTOs.Inventory;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    public partial class frmStockReconciliationDetail : Form
    {
        private static readonly CultureInfo VietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");
        private readonly StockReconciliationRowDto _row;

        public frmStockReconciliationDetail()
        {
            InitializeComponent();
        }

        public frmStockReconciliationDetail(StockReconciliationRowDto row) : this()
        {
            _row = row ?? new StockReconciliationRowDto();
            ShowRow();
        }

        private void ShowRow()
        {
            _titleLabel.Text = Value(_row.ProductName);
            _subtitleLabel.Text = Value(_row.ProductCode) + "  ·  Phiên ngày "
                + _row.CreatedAt.ToString("dd/MM/yyyy", VietnameseCulture);

            _statusLabel.Text = _row.SessionStatusText;
            if (_row.IsEditable)
            {
                _statusLabel.BackColor = Color.FromArgb(219, 234, 254);
                _statusLabel.ForeColor = Color.FromArgb(37, 99, 235);
            }
            else
            {
                _statusLabel.BackColor = Color.FromArgb(241, 245, 249);
                _statusLabel.ForeColor = Color.FromArgb(71, 85, 105);
            }

            _productCodeValue.Text = Value(_row.ProductCode);
            _productNameValue.Text = Value(_row.ProductName);
            _categoryValue.Text = Value(_row.CategoryName);
            _sessionDateValue.Text = _row.CreatedAt.ToString("dd/MM/yyyy", VietnameseCulture);
            _systemValue.Text = _row.SystemStock.ToString("N0", VietnameseCulture);
            _actualValue.Text = _row.ActualStock.ToString("N0", VietnameseCulture);
            _diffValue.Text = _row.DiscrepancyText;
            _currentStockValue.Text = _row.CurrentStock.ToString("N0", VietnameseCulture);
            _reasonValue.Text = Value(_row.Note);
            _createdByValue.Text = Value(_row.CreatedByName);
            _updatedAtValue.Text = _row.UpdatedAt.HasValue
                ? _row.UpdatedAt.Value.ToString("dd/MM/yyyy HH:mm", VietnameseCulture)
                : "Chưa chỉnh";

            Color diffColor = _row.Discrepancy == 0
                ? Color.FromArgb(21, 128, 61)
                : (_row.Discrepancy > 0 ? Color.FromArgb(194, 65, 12) : Color.FromArgb(185, 28, 28));
            _diffValue.ForeColor = diffColor;
            _footerDiffLabel.ForeColor = diffColor;
            _footerDiffLabel.Text = _row.DiscrepancyText;
        }

        private static string Value(string text)
        {
            return string.IsNullOrWhiteSpace(text) ? "—" : text;
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using SIMS_WinFormsApp.Models.DTOs.Sales;
using SIMS_WinFormsApp.Services.Implementations.Sales;
using SIMS_WinFormsApp.MVP.Presenters.Pos;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Dialogs;

namespace SIMS_WinFormsApp.Views.Sales.InvoiceManagement
{
    public sealed partial class InvoiceReturnDialog : Form
    {
        private readonly InvoiceDetails _invoice;
        private readonly int _currentUserId;
        private readonly bool _canApprove;
        private readonly IReturnManagementDataService _dataService;
        private readonly List<ReturnableLine> _lines = new List<ReturnableLine>();
        private decimal _approvalThreshold;
        private int _returnPolicyDays = 7;
        private bool _approvedImmediately;
        private bool _managerApproved;
        private readonly bool _designPreview;

        public InvoiceReturnDialog()
            : this(null, CreateDesignPreviewInvoice(), 0, false, true)
        {
        }

        internal InvoiceReturnDialog(
            IReturnManagementDataService dataService,
            InvoiceDetails invoice,
            int currentUserId,
            bool canApprove)
            : this(dataService, invoice, currentUserId, canApprove, false)
        {
        }

        private InvoiceReturnDialog(
            IReturnManagementDataService dataService,
            InvoiceDetails invoice,
            int currentUserId,
            bool canApprove,
            bool designPreview)
        {
            if (dataService == null && !designPreview)
                throw new ArgumentNullException(nameof(dataService));
            InitializeComponent();
            _designPreview = designPreview;
            _invoice = invoice ?? throw new ArgumentNullException(nameof(invoice));
            _currentUserId = currentUserId;
            _canApprove = canApprove;
            _dataService = designPreview ? null : dataService;

            Text = "Tạo yêu cầu trả hàng";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;
            ClientSize = new Size(820, 600);
            BackColor = AppColors.PageBg;

            LoadReturnableLines();
        }

        private void SelectAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in _linesGrid.Rows)
            {
                if (row.Cells["Selected"].ReadOnly)
                    continue;
                row.Cells["Selected"].Value = _selectAllCheckBox.Checked;
            }
            UpdateSummary();
        }

        private void LinesGrid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (_linesGrid.IsCurrentCellDirty)
                _linesGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void LinesGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            UpdateSummary();
        }

        private void LinesGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            UpdateSummary();
        }

        private void LinesGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            DialogHelper.ShowWarning(this, "Số lượng không hợp lệ",
                "Số lượng trả phải là số nguyên hợp lệ.");
        }

        private void OnLinesGridEditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (_linesGrid.CurrentCell?.OwningColumn?.Name == "Quantity" && e.Control is TextBox textBox)
            {
                NumberInputFormatter.Attach(textBox);
                NumberInputFormatter.SetMaximumDigits(textBox, 10);
            }
        }

        private void LoadReturnableLines()
        {
            if (_designPreview)
            {
                _lines.Add(new ReturnableLine(1, "Sản phẩm mẫu", 3, 0, 28000m));
                _approvalThreshold = 500000m;
                _returnPolicyDays = 7;
            }
            else
            {
                foreach (ReturnableLineData line in _dataService.GetReturnableLines(_invoice.InvoiceId))
                {
                    _lines.Add(new ReturnableLine(line.ProductId, line.ProductName,
                        line.SoldQuantity, line.ReturnedQuantity, line.UnitPrice));
                }
                _dataService.GetReturnPolicy(out _approvalThreshold, out _returnPolicyDays);
            }

            _policyLabel.Text = _returnPolicyDays == 0
                ? "Hóa đơn ngày " + _invoice.CreatedAt.ToString("dd/MM/yyyy") + " • Chỉ trả trong ngày mua"
                : "Hóa đơn ngày " + _invoice.CreatedAt.ToString("dd/MM/yyyy") +
                  " • Thời hạn trả " + _returnPolicyDays + " ngày";

            foreach (ReturnableLine line in _lines)
            {
                int rowIndex = _linesGrid.Rows.Add(
                    false,
                    line.ProductName,
                    line.SoldQuantity,
                    line.ReturnedQuantity,
                    Math.Max(0, line.SoldQuantity - line.ReturnedQuantity),
                    FormatMoney(line.UnitPrice));
                DataGridViewRow row = _linesGrid.Rows[rowIndex];
                row.Tag = line;
                if (line.SoldQuantity <= line.ReturnedQuantity)
                {
                    row.Cells["Selected"].ReadOnly = true;
                    row.Cells["Quantity"].ReadOnly = true;
                }
            }
            UpdateSummary();
        }

        private void UpdateSummary()
        {
            decimal total = 0m;
            foreach (DataGridViewRow row in _linesGrid.Rows)
            {
                if (row.IsNewRow || !Convert.ToBoolean(row.Cells["Selected"].Value ?? false))
                    continue;

                int quantity;
                if (int.TryParse(NumberInputFormatter.DigitsOnly(
                    Convert.ToString(row.Cells["Quantity"].Value)), out quantity) && quantity > 0)
                {
                    ReturnableLine line = row.Tag as ReturnableLine;
                    total += quantity * line.UnitPrice;
                }
            }

            _summaryLabel.Text = "Giá trị hàng chọn: " + FormatMoney(total);
            bool hasReturnableLine = _lines.Any(line => line.SoldQuantity > line.ReturnedQuantity);
            _submitButton.Enabled = hasReturnableLine && !_invoice.IsCancelled;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (_designPreview)
                return;

            if (_linesGrid.IsCurrentCellDirty)
                _linesGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            _linesGrid.EndEdit();

            string reason = Convert.ToString(_reasonCombo.SelectedItem);
            if (!string.IsNullOrWhiteSpace(_reasonDetails.Text))
                reason += ": " + _reasonDetails.Text.Trim();
            if (reason.Length > 255)
                reason = reason.Substring(0, 255);

            var selected = new List<SelectedReturnLine>();
            foreach (DataGridViewRow row in _linesGrid.Rows)
            {
                if (row.IsNewRow || !Convert.ToBoolean(row.Cells["Selected"].Value ?? false))
                    continue;

                int quantity;
                if (!int.TryParse(NumberInputFormatter.DigitsOnly(
                    Convert.ToString(row.Cells["Quantity"].Value)), out quantity) || quantity <= 0)
                {
                    DialogHelper.ShowWarning(this, "Thiếu số lượng",
                        "Nhập số lượng trả hợp lệ cho từng sản phẩm đã chọn.");
                    return;
                }

                ReturnableLine line = row.Tag as ReturnableLine;
                if (quantity > line.SoldQuantity - line.ReturnedQuantity)
                {
                    DialogHelper.ShowWarning(this, "Số lượng vượt giới hạn",
                        "Số lượng trả vượt quá số lượng còn được trả của sản phẩm \"" +
                        line.ProductName + "\".");
                    return;
                }
                selected.Add(new SelectedReturnLine(line, quantity));
            }

            if (selected.Count == 0)
            {
                DialogHelper.ShowWarning(this, "Chưa chọn sản phẩm",
                    "Hãy chọn ít nhất một sản phẩm và nhập số lượng cần trả.");
                return;
            }

            if (!DialogHelper.ConfirmCustom(this,
                    "Xác nhận trả hàng",
                    "Tạo yêu cầu trả " + selected.Sum(item => item.Quantity) +
                    " sản phẩm từ hóa đơn này?\r\nTiền hoàn dự kiến: " + FormatMoney(CalculateRefund(selected)),
                    "Gửi yêu cầu", "Kiểm tra lại"))
                return;

            _managerApproved = false;
            if (CalculateRefund(selected) > _approvalThreshold && _canApprove)
            {
                DialogResult approval = DialogHelper.Show(this,
                    "Giá trị hoàn vượt ngưỡng " + FormatMoney(_approvalThreshold) +
                    ". Bạn có quyền duyệt yêu cầu này. Duyệt ngay?",
                    "Cần duyệt", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (approval == DialogResult.Cancel)
                    return;
                _managerApproved = approval == DialogResult.Yes;
            }

            _submitButton.Enabled = false;
            try
            {
                ReturnCreateResult created = _dataService.CreateReturn(
                    _invoice.InvoiceId, _currentUserId, _returnPolicyDays, _canApprove, _managerApproved,
                    reason, selected.Select(item => new ReturnSelectionData
                    {
                        ProductId = item.Line.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.Line.UnitPrice
                    }).ToList());
                _approvedImmediately = created.ApprovedImmediately;
                int returnId = created.ReturnId;
                DialogHelper.ShowInfo(this, "Đã tạo yêu cầu", _approvedImmediately
                            ? "Đã duyệt yêu cầu trả hàng #" + returnId + ". Tồn kho đã được cập nhật."
                            : "Đã gửi yêu cầu trả hàng #" + returnId + " để quản lý duyệt.");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (OperationCanceledException)
            {
            _submitButton.Enabled = true;
            }
            catch (Exception ex)
            {
                DialogHelper.ShowError(this, "Lỗi trả hàng",
                    "Không thể tạo yêu cầu trả hàng.\r\n" + ex.Message);
                _submitButton.Enabled = true;
            }
        }

        private decimal CalculateRefund(IEnumerable<SelectedReturnLine> selected)
        {
            decimal gross = selected.Sum(item => item.Quantity * item.Line.UnitPrice);
            decimal discountShare = _invoice.Subtotal <= 0 ? 0m :
                Math.Min(_invoice.DiscountAmount,
                    decimal.Round(gross * _invoice.DiscountAmount / _invoice.Subtotal, 0, MidpointRounding.AwayFromZero));
            decimal pointsShare = _invoice.Subtotal <= 0 ? 0m :
                Math.Min(_invoice.PointsDiscountAmount,
                    decimal.Round(gross * _invoice.PointsDiscountAmount / _invoice.Subtotal, 0, MidpointRounding.AwayFromZero));
            decimal taxable = Math.Max(0m, gross - discountShare);
            return decimal.Round(
                Math.Max(0m, taxable + taxable * _invoice.VatRate / 100m - pointsShare),
                0,
                MidpointRounding.AwayFromZero);
        }

        private static string FormatMoney(decimal amount)
        {
            return amount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + " đ";
        }

        private static InvoiceDetails CreateDesignPreviewInvoice()
        {
            return new InvoiceDetails
            {
                InvoiceId = 1,
                InvoiceCode = "HD-MẪU-000001",
                CreatedAt = DateTime.Today,
                Subtotal = 84000m,
                TotalAmount = 84000m
            };
        }

        private sealed class ReturnableLine
        {
            public ReturnableLine(int productId, string productName, int soldQuantity, int returnedQuantity, decimal unitPrice)
            {
                ProductId = productId;
                ProductName = productName;
                SoldQuantity = soldQuantity;
                ReturnedQuantity = returnedQuantity;
                UnitPrice = unitPrice;
            }

            public int ProductId { get; }
            public string ProductName { get; }
            public int SoldQuantity { get; }
            public int ReturnedQuantity { get; }
            public decimal UnitPrice { get; }
        }

        private sealed class SelectedReturnLine
        {
            public SelectedReturnLine(ReturnableLine line, int quantity)
            {
                Line = line;
                Quantity = quantity;
            }

            public ReturnableLine Line { get; }
            public int Quantity { get; }
        }
    }
}

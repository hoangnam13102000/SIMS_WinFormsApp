using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using SIMS_WinFormsApp.Models.DTOs.Sales;
using SIMS_WinFormsApp.Services.Implementations.Sales;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.MVP.Presenters.Pos;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Dialogs;

namespace SIMS_WinFormsApp.Views.Sales.InvoiceManagement
{
    public sealed partial class InvoiceReturnDialog : Form
    {
        private readonly InvoiceDetails _invoice;
        private readonly int _currentUserId;
        private readonly bool _canApprove;
        private readonly ReturnManagementDataService _dataService;
        private readonly DataGridView _linesGrid;
        private readonly ComboBox _reasonCombo;
        private readonly TextBox _reasonDetails;
        private readonly Label _summaryLabel;
        private readonly Label _policyLabel;
        private readonly Button _submitButton;
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
            IDbConnectionFactory connectionFactory,
            InvoiceDetails invoice,
            int currentUserId,
            bool canApprove)
            : this(connectionFactory, invoice, currentUserId, canApprove, false)
        {
        }

        private InvoiceReturnDialog(
            IDbConnectionFactory connectionFactory,
            InvoiceDetails invoice,
            int currentUserId,
            bool canApprove,
            bool designPreview)
        {
            if (connectionFactory == null && !designPreview)
                throw new ArgumentNullException(nameof(connectionFactory));
            InitializeComponent();
            _invoice = invoice ?? throw new ArgumentNullException(nameof(invoice));
            _currentUserId = currentUserId;
            _canApprove = canApprove;
            _designPreview = designPreview;
            _dataService = designPreview ? null : new ReturnManagementDataService(connectionFactory);

            Text = "Tạo yêu cầu trả hàng";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;
            ClientSize = new Size(820, 600);
            BackColor = AppColors.PageBg;

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(20)
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 130F));
            Controls.Add(root);

            var header = new Panel { Dock = DockStyle.Fill };
            header.Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = AppColors.TextTitle,
                Location = new Point(0, 0),
                Text = "Trả hàng từ hóa đơn " + _invoice.InvoiceCode
            });
            _policyLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                ForeColor = AppColors.TextMuted,
                Location = new Point(2, 38)
            };
            header.Controls.Add(_policyLabel);
            var selectAllCheckBox = new CheckBox
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Location = new Point(590, 43),
                Text = "Chọn toàn bộ hóa đơn"
            };
            selectAllCheckBox.CheckedChanged += (sender, args) =>
            {
                foreach (DataGridViewRow row in _linesGrid.Rows)
                {
                    if (row.Cells["Selected"].ReadOnly)
                        continue;
                    row.Cells["Selected"].Value = selectAllCheckBox.Checked;
                }
                UpdateSummary();
            };
            header.Controls.Add(selectAllCheckBox);
            root.Controls.Add(header, 0, 0);

            _linesGrid = new DataGridView
            {
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = AppColors.White,
                BorderStyle = BorderStyle.None,
                ColumnHeadersHeight = 38,
                Dock = DockStyle.Fill,
                EnableHeadersVisualStyles = false,
                GridColor = AppColors.TableGrid,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.CellSelect,
                MultiSelect = false
            };
            _linesGrid.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "Selected",
                HeaderText = "Trả",
                FillWeight = 8,
                MinimumWidth = 48
            });
            _linesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Product",
                HeaderText = "Sản phẩm",
                FillWeight = 39,
                ReadOnly = true
            });
            _linesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Sold",
                HeaderText = "Đã mua",
                FillWeight = 10,
                ReadOnly = true
            });
            _linesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Returned",
                HeaderText = "Đã yêu cầu/đã trả",
                FillWeight = 17,
                ReadOnly = true
            });
            _linesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Quantity",
                HeaderText = "SL trả",
                FillWeight = 9
            });
            _linesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Price",
                HeaderText = "Đơn giá",
                FillWeight = 17,
                ReadOnly = true
            });
            _linesGrid.Columns["Sold"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            _linesGrid.Columns["Returned"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            _linesGrid.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            _linesGrid.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _linesGrid.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            _linesGrid.ColumnHeadersDefaultCellStyle.BackColor = AppColors.TableHeaderBg;
            _linesGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            _linesGrid.CurrentCellDirtyStateChanged += (sender, args) =>
            {
                if (_linesGrid.IsCurrentCellDirty)
                    _linesGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
            _linesGrid.CellValueChanged += (sender, args) => UpdateSummary();
            _linesGrid.CellEndEdit += (sender, args) => UpdateSummary();
            _linesGrid.DataError += (sender, args) =>
            {
                args.ThrowException = false;
                DialogHelper.ShowWarning(this, "Số lượng không hợp lệ",
                    "Số lượng trả phải là số nguyên hợp lệ.");
            };
            root.Controls.Add(_linesGrid, 0, 1);

            var footer = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 3,
                Padding = new Padding(0, 10, 0, 0)
            };
            footer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            footer.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 230F));
            footer.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            footer.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            footer.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            root.Controls.Add(footer, 0, 2);

            var reasonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                WrapContents = false,
                Margin = Padding.Empty
            };
            reasonPanel.Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Text = "Lý do:",
                Margin = new Padding(0, 6, 8, 0)
            });
            _reasonCombo = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 160,
                Margin = new Padding(0, 2, 8, 0)
            };
            _reasonCombo.Items.AddRange(new object[]
            {
                "Lỗi sản phẩm",
                "Giao sai sản phẩm",
                "Không còn nhu cầu",
                "Khác"
            });
            _reasonCombo.SelectedIndex = 0;
            reasonPanel.Controls.Add(_reasonCombo);
            _reasonDetails = new TextBox
            {
                Dock = DockStyle.Fill,
                MaxLength = 180,
            };
            reasonPanel.Controls.Add(_reasonDetails);
            footer.Controls.Add(reasonPanel, 0, 0);
            footer.SetColumnSpan(reasonPanel, 2);

            _summaryLabel = new Label
            {
                AutoSize = true,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = AppColors.Accent,
                TextAlign = ContentAlignment.MiddleLeft
            };
            footer.Controls.Add(_summaryLabel, 0, 1);

            var buttons = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false
            };
            var cancelButton = new Button
            {
                Text = "Hủy",
                FlatStyle = FlatStyle.Flat,
                Size = new Size(78, 34),
                Margin = new Padding(8, 2, 0, 0),
                DialogResult = DialogResult.Cancel
            };
            _submitButton = new Button
            {
                Text = "Gửi yêu cầu",
                FlatStyle = FlatStyle.Flat,
                BackColor = AppColors.Accent,
                ForeColor = Color.White,
                Size = new Size(124, 34),
                Margin = new Padding(8, 2, 0, 0)
            };
            _submitButton.Click += SubmitButton_Click;
            buttons.Controls.Add(cancelButton);
            buttons.Controls.Add(_submitButton);
            footer.Controls.Add(buttons, 1, 1);

            var note = new Label
            {
                AutoSize = true,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 8F),
                ForeColor = AppColors.TextMuted,
                Text = "Tiền hoàn được ghi nhận theo giá trị dự kiến; thanh toán thực tế xử lý bên ngoài."
            };
            footer.Controls.Add(note, 0, 2);
            footer.SetColumnSpan(note, 2);
            CancelButton = cancelButton;

            LoadReturnableLines();
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
                if (int.TryParse(Convert.ToString(row.Cells["Quantity"].Value), out quantity) && quantity > 0)
                {
                    ReturnableLine line = row.Tag as ReturnableLine;
                    total += quantity * line.UnitPrice;
                }
            }

            _summaryLabel.Text = "Giá trị hàng chọn: " + FormatMoney(total);
            bool hasReturnableLine = _lines.Any(line => line.SoldQuantity > line.ReturnedQuantity);
            _submitButton.Enabled = hasReturnableLine && !_invoice.IsCancelled;
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
                if (!int.TryParse(Convert.ToString(row.Cells["Quantity"].Value), out quantity) || quantity <= 0)
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

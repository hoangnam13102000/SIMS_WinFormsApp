using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.MVP.Presenters.Inventory;
using SIMS_WinFormsApp.Services;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;
using SIMS_WinFormsApp.Views.Toast;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    public sealed partial class frmStockDisposalEditor : Form, IStockDisposalEditorView
    {
        private static readonly CultureInfo VietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");
        private readonly IStockDisposalService _service;
        private readonly IUserSession _session;
        private readonly StockDisposalEditorPresenter _presenter;
        private readonly IDialogService _dialogService;

        public frmStockDisposalEditor()
            : this(null, null)
        {
        }

        public frmStockDisposalEditor(IStockDisposalService service, IUserSession session)
        {
            InitializeComponent();
            Load += new EventHandler(this.Editor_Load);
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            _service = service ?? AppComposition.CreateStockDisposalService();
            _session = session ?? UserSession.Instance;
            _dialogService = AppComposition.CreateDialogService();
            _presenter = new StockDisposalEditorPresenter(this, _service, _session);

            _batchComboBox.SelectedIndexChanged += new EventHandler(this.BatchComboBox_SelectedIndexChanged);
            _addLineButton.Click += new EventHandler(this.AddLineButton_Click);
            _linesGrid.CellClick += new DataGridViewCellEventHandler(this.LinesGrid_CellClick);
            _linesGrid.CellMouseMove += new DataGridViewCellMouseEventHandler(this.LinesGrid_CellMouseMove);
            _linesGrid.MouseLeave += new EventHandler(this.LinesGrid_MouseLeave);
            DataGridViewCodeCopy.Attach(_linesGrid, "colBatchCode");
            _saveButton.Click += new EventHandler(this.SaveButton_Click);
            _cancelButton.Click += new EventHandler(this.CancelButton_Click);
        }

        public event EventHandler LinesChanged;
        public event EventHandler SaveRequested;

        public string SelectedReason
        {
            get
            {
                int selectedIndex = _reasonComboBox.SelectedIndex;
                return selectedIndex < 0 || selectedIndex >= DisposalReasons.Codes.Length
                    ? DisposalReasons.Expired
                    : DisposalReasons.Codes[selectedIndex];
            }
        }

        public string Note => _noteTextBox.Text.Trim();

        public string SavedMessage { get; private set; }

        public IReadOnlyList<StockDisposalLineDto> ReadLines()
        {
            var lines = new List<StockDisposalLineDto>();
            foreach (DataGridViewRow row in _linesGrid.Rows)
            {
                DisposableBatchDto batch = row.Tag as DisposableBatchDto;
                if (batch == null)
                    continue;

                int quantity;
                if (!int.TryParse(Convert.ToString(row.Cells[colQuantity.Index].Value, CultureInfo.InvariantCulture),
                        NumberStyles.Integer, CultureInfo.InvariantCulture, out quantity))
                    continue;

                lines.Add(new StockDisposalLineDto
                {
                    BatchId = batch.BatchId,
                    Quantity = quantity
                });
            }
            return lines;
        }

        public void BindBatches(IReadOnlyList<DisposableBatchDto> batches)
        {
            _batchComboBox.Items.Clear();
            if (batches != null)
            {
                foreach (DisposableBatchDto batch in batches)
                {
                    if (batch != null)
                        _batchComboBox.Items.Add(batch);
                }
            }

            if (_batchComboBox.Items.Count > 0)
            {
                _batchComboBox.SelectedIndex = 0;
                _addLineButton.Enabled = true;
            }
            else
            {
                _remainHintLabel.Text = "Không có lô nào còn hàng để tiêu hủy";
                _addLineButton.Enabled = false;
            }
        }

        public void ShowTotal(decimal total)
        {
            _totalValueLabel.Text = total.ToString("N0", VietnameseCulture) + " đ";
        }

        public bool ConfirmSave(int lineCount, decimal total)
        {
            string message = "Tiêu hủy " + lineCount.ToString(CultureInfo.InvariantCulture) +
                " lô?\nTổng tổn thất ước tính: " + total.ToString("N0", VietnameseCulture) +
                " đ\nTồn kho sẽ bị trừ ngay sau khi lưu.";
            return _dialogService.ConfirmCustom(this, "Xác nhận tiêu hủy", message,
                "Lưu phiếu", "Hủy", Color.FromArgb(220, 38, 38));
        }

        public void ShowMessage(string message, bool isError)
        {
            if (isError)
                AppToast.Error(this, "Thất bại", message);
            else
                AppToast.Success(this, message);
        }

        public void CloseAfterSave(string message)
        {
            SavedMessage = message;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            _reasonComboBox.SelectedIndex = 0;
            UpdateLineCountAndEmptyState();
            if (_presenter != null)
                _presenter.Initialize();
        }

        private void BatchComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisposableBatchDto batch = _batchComboBox.SelectedItem as DisposableBatchDto;
            _remainHintLabel.Text = batch == null
                ? string.Empty
                : "Còn " + batch.RemainingQty.ToString("N0", VietnameseCulture) +
                  "  ·  Giá vốn " + batch.ImportPrice.ToString("N0", VietnameseCulture) + " đ";
        }

        private void AddLineButton_Click(object sender, EventArgs e)
        {
            DisposableBatchDto batch = _batchComboBox.SelectedItem as DisposableBatchDto;
            if (batch == null)
            {
                AppToast.Error(this, "Thiếu thông tin", "Vui lòng chọn lô hàng.");
                return;
            }

            foreach (DataGridViewRow existingRow in _linesGrid.Rows)
            {
                DisposableBatchDto existingBatch = existingRow.Tag as DisposableBatchDto;
                if (existingBatch != null && existingBatch.BatchId == batch.BatchId)
                {
                    AppToast.Error(this, "Trùng lô", "Lô này đã có trên phiếu. Xóa dòng cũ nếu muốn sửa SL.");
                    return;
                }
            }

            int quantity;
            if (!int.TryParse(_quantityTextBox.Text.Trim(), NumberStyles.Integer,
                    CultureInfo.InvariantCulture, out quantity) || quantity <= 0)
            {
                AppToast.Error(this, "Số lượng không hợp lệ", "Số lượng phải là số nguyên lớn hơn 0.");
                return;
            }

            if (quantity > batch.RemainingQty)
            {
                AppToast.Error(this, "Vượt tồn lô", "Lô chỉ còn " +
                    batch.RemainingQty.ToString(CultureInfo.InvariantCulture) + ".");
                return;
            }

            string expiryText = batch.ExpiryDate.HasValue
                ? batch.ExpiryDate.Value.ToString("dd/MM/yyyy", VietnameseCulture)
                : "—";
            int rowIndex = _linesGrid.Rows.Add(
                batch.BatchCode,
                batch.ProductName,
                expiryText,
                quantity,
                batch.ImportPrice.ToString("N0", VietnameseCulture),
                (batch.ImportPrice * quantity).ToString("N0", VietnameseCulture),
                IconBitmapCache.Get(IconChar.TrashCan, Color.FromArgb(220, 38, 38), new Size(18, 18)));
            DataGridViewRow row = _linesGrid.Rows[rowIndex];
            row.Tag = batch;
            row.Cells[colRemove.Index].ToolTipText = "Xóa dòng này";
            _quantityTextBox.Clear();
            UpdateLineCountAndEmptyState();
            LinesChanged?.Invoke(this, EventArgs.Empty);
        }

        private void LinesGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _linesGrid.Rows.Count || e.ColumnIndex != colRemove.Index)
                return;

            _linesGrid.Rows.RemoveAt(e.RowIndex);
            UpdateLineCountAndEmptyState();
            LinesChanged?.Invoke(this, EventArgs.Empty);
        }

        private void LinesGrid_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            _linesGrid.Cursor = e.RowIndex >= 0 && e.ColumnIndex == colRemove.Index
                ? Cursors.Hand
                : Cursors.Default;
        }

        private void LinesGrid_MouseLeave(object sender, EventArgs e)
        {
            _linesGrid.Cursor = Cursors.Default;
        }

        private void UpdateLineCountAndEmptyState()
        {
            int count = _linesGrid.Rows.Count;
            _lineCountLabel.Text = count.ToString(CultureInfo.InvariantCulture) + " dòng";
            _emptyLabel.Visible = count == 0;
            if (_emptyLabel.Visible)
                _emptyLabel.BringToFront();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveRequested?.Invoke(this, EventArgs.Empty);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

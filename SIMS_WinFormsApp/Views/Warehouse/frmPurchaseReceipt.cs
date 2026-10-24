using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.MVP.Presenters.Inventory;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.Views.Interfaces;
using SIMS_WinFormsApp.Views.Toast;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    public partial class frmPurchaseReceipt : Form, IPurchaseReceiptView
    {
        private readonly IToastNotifier _toastNotifier;
        private PurchaseReceiptPresenter _presenter;

        public event EventHandler SaveRequested;
        public event EventHandler LinesChanged;

        public frmPurchaseReceipt() : this(null, null)
        {
        }

        public frmPurchaseReceipt(IInventoryService service, IUserSession session)
        {
            InitializeComponent();
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            _toastNotifier = AppComposition.CreateToastNotifier();
            _presenter = new PurchaseReceiptPresenter(this, service, session);
            _addItemButton.Click += (sender, args) => _itemsGrid.Rows.Add();
            _saveButton.Click += (sender, args) => SaveRequested?.Invoke(this, EventArgs.Empty);
            _cancelButton.Click += (sender, args) => ResetLines();
            _itemsGrid.CellEndEdit += OnItemsGridCellEndEdit;
            _presenter.Initialize();
        }

        public int SelectedSupplierId
        {
            get
            {
                int supplierId;
                return int.TryParse(Convert.ToString(_supplier.SelectedValue, CultureInfo.InvariantCulture),
                    NumberStyles.Integer, CultureInfo.InvariantCulture, out supplierId)
                    ? supplierId
                    : 0;
            }
        }

        public IReadOnlyList<PurchaseLineDto> ReadLines()
        {
            var lines = new List<PurchaseLineDto>();
            CultureInfo culture = CultureInfo.GetCultureInfo("vi-VN");

            foreach (DataGridViewRow row in _itemsGrid.Rows)
            {
                if (row.IsNewRow) continue;

                bool invalid = false;
                int productId;
                if (!int.TryParse(Convert.ToString(row.Cells["Product"].Value, CultureInfo.InvariantCulture),
                    NumberStyles.Integer, CultureInfo.InvariantCulture, out productId))
                {
                    productId = 0;
                    invalid = true;
                }

                int quantity;
                if (!int.TryParse(Convert.ToString(row.Cells["Quantity"].Value, culture),
                    NumberStyles.Integer, culture, out quantity))
                {
                    quantity = 0;
                    invalid = true;
                }

                decimal importPrice;
                if (!decimal.TryParse(Convert.ToString(row.Cells["UnitPrice"].Value, culture),
                    NumberStyles.Number, culture, out importPrice))
                {
                    importPrice = 0m;
                    invalid = true;
                }

                DateTime? expiryDate = null;
                string expiryText = Convert.ToString(row.Cells["ExpiryDate"].Value, culture);
                if (!string.IsNullOrWhiteSpace(expiryText))
                {
                    DateTime parsedExpiryDate;
                    if (DateTime.TryParseExact(expiryText.Trim(), "dd/MM/yyyy", culture,
                        DateTimeStyles.None, out parsedExpiryDate))
                    {
                        expiryDate = parsedExpiryDate;
                    }
                    else
                    {
                        invalid = true;
                    }
                }

                var line = new PurchaseLineDto
                {
                    ProductId = productId,
                    Quantity = invalid ? 0 : quantity,
                    ImportPrice = importPrice,
                    LotNumber = Convert.ToString(row.Cells["LotNumber"].Value, culture),
                    ExpiryDate = expiryDate
                };
                lines.Add(line);
            }

            return lines;
        }

        public void BindSuppliers(IReadOnlyList<LookupItem> items)
        {
            _supplier.DisplayMember = "Name";
            _supplier.ValueMember = "Id";
            _supplier.DataSource = items ?? Array.Empty<LookupItem>();
            _supplier.SelectedIndex = -1;
        }

        public void BindProducts(IReadOnlyList<LookupItem> items)
        {
            var productColumn = (DataGridViewComboBoxColumn)_itemsGrid.Columns["Product"];
            productColumn.DisplayMember = "Name";
            productColumn.ValueMember = "Id";
            productColumn.DataSource = items ?? Array.Empty<LookupItem>();
        }

        public void ShowTotal(decimal total)
        {
            _totalLabel.Text = "Tổng tiền: " + total.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + " đ";
        }

        public void ShowReceiptCode(string code)
        {
            _receiptCode.Text = code ?? "Tự động";
        }

        public void ShowMessage(string message, bool isError)
        {
            if (isError)
                _toastNotifier.Error(this, message);
            else
                _toastNotifier.Success(this, message);
        }

        public void ResetLines()
        {
            _itemsGrid.Rows.Clear();
            ShowTotal(0m);
        }

        private void OnItemsGridCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = _itemsGrid.Rows[e.RowIndex];
            CultureInfo culture = CultureInfo.GetCultureInfo("vi-VN");
            int quantity;
            decimal importPrice;
            bool validQuantity = int.TryParse(Convert.ToString(row.Cells["Quantity"].Value, culture),
                NumberStyles.Integer, culture, out quantity);
            bool validPrice = decimal.TryParse(Convert.ToString(row.Cells["UnitPrice"].Value, culture),
                NumberStyles.Number, culture, out importPrice);

            row.Cells["Amount"].Value = validQuantity && validPrice
                ? (quantity * importPrice).ToString("N0", culture)
                : "0";
            LinesChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

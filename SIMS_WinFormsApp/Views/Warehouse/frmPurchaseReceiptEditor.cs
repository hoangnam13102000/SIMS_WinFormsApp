using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.Models.DTOs.Inventory;
using SIMS_WinFormsApp.MVP.Presenters.Inventory;
using SIMS_WinFormsApp.Services;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;
using SIMS_WinFormsApp.Views.Toast;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    public sealed partial class frmPurchaseReceiptEditor : Form, IPurchaseReceiptView
    {
        private static readonly CultureInfo VietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");
        private static readonly Random Randomizer = new Random();
        private readonly IInventoryService _service;
        private readonly IUserSession _session;
        private readonly PurchaseReceiptPresenter _presenter;
        private readonly IDialogService _dialogService;
        private readonly List<SupplierOption> _supplierOptions = new List<SupplierOption>();
        private readonly List<ProductOption> _productOptions = new List<ProductOption>();

        public event EventHandler SaveRequested;
        public event EventHandler LinesChanged;

        public frmPurchaseReceiptEditor() : this(null, null)
        {
        }

        public frmPurchaseReceiptEditor(IInventoryService service, IUserSession session)
        {
            InitializeComponent();
            _service = service;
            _session = session;
            Load += Editor_Load;

            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            if (_service == null)
                _service = AppComposition.CreateInventoryService();
            if (_session == null)
                _session = UserSession.Instance;

            _dialogService = AppComposition.CreateDialogService();
            _presenter = new PurchaseReceiptPresenter(this, _service, _session);
            NumberInputFormatter.Attach(_priceTextBox);
            NumberInputFormatter.SetMaximumDigits(_priceTextBox, 18);
            ConfigureEntryControls();

            _demoButton.Click += (sender, args) => FillDemoData();
            _addLineButton.Click += (sender, args) => AddLineFromInputs();
            _linesGrid.CellClick += LinesGrid_CellClick;
            _linesGrid.CellPainting += LinesGrid_CellPainting;
            _linesGrid.CellToolTipTextNeeded += LinesGrid_CellToolTipTextNeeded;
            _saveButton.Click += (sender, args) => SaveReceipt();
            _cancelButton.Click += (sender, args) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };
            _presenter.Initialize();
        }

        public int SelectedSupplierId
        {
            get
            {
                SupplierOption supplier = _supplierComboBox.SelectedItem as SupplierOption;
                return supplier == null ? 0 : supplier.Id;
            }
        }

        public IReadOnlyList<PurchaseLineDto> ReadLines()
        {
            var lines = new List<PurchaseLineDto>();
            foreach (DataGridViewRow row in _linesGrid.Rows)
            {
                ProductOption product = row.Tag as ProductOption;
                int quantity;
                decimal importPrice;
                if (product == null || !int.TryParse(Convert.ToString(row.Cells["Quantity"].Value,
                    CultureInfo.InvariantCulture), NumberStyles.Integer, CultureInfo.InvariantCulture, out quantity) ||
                    !decimal.TryParse(Convert.ToString(row.Cells["ImportPrice"].Value, CultureInfo.InvariantCulture),
                        NumberStyles.Number, CultureInfo.InvariantCulture, out importPrice))
                    continue;

                lines.Add(new PurchaseLineDto
                {
                    ProductId = product.Id,
                    Quantity = quantity,
                    ImportPrice = importPrice,
                    LotNumber = Convert.ToString(row.Cells["LotNumber"].Value, CultureInfo.CurrentCulture),
                    ManufactureDate = ReadDate(row.Cells["ManufactureDate"].Value),
                    ExpiryDate = ReadDate(row.Cells["ExpiryDate"].Value)
                });
            }
            return lines;
        }

        public void BindSuppliers(IReadOnlyList<LookupItem> items)
        {
            _supplierOptions.Clear();
            _supplierOptions.Add(new SupplierOption(0, "— Chọn nhà cung cấp —"));
            if (items != null)
                _supplierOptions.AddRange(items.Select(item => new SupplierOption(item.Id, item.Name)));

            _supplierComboBox.DataSource = null;
            _supplierComboBox.DisplayMember = "Name";
            _supplierComboBox.ValueMember = "Id";
            _supplierComboBox.DataSource = _supplierOptions.ToList();
            _supplierComboBox.SelectedIndex = 0;
        }

        public void BindProducts(IReadOnlyList<LookupItem> items)
        {
            _productOptions.Clear();
            if (items != null)
                _productOptions.AddRange(items.Select(item => new ProductOption(item.Id, item.Name)));

            _productComboBox.DataSource = null;
            _productComboBox.DisplayMember = "Name";
            _productComboBox.ValueMember = "Id";
            _productComboBox.DataSource = _productOptions.ToList();
            _productComboBox.SelectedIndex = -1;
            _productComboBox.Text = string.Empty;
        }

        public void ShowTotal(decimal total)
        {
            _totalValueLabel.Text = total.ToString("N0", VietnameseCulture) + " đ";
        }

        public void ShowReceiptCode(string code)
        {
            BeginInvoke(new Action(() =>
            {
                DialogResult = DialogResult.OK;
                Close();
            }));
        }

        public void ShowMessage(string message, bool isError)
        {
            if (isError)
            {
                AppToast.Error(this, message);
                return;
            }

            const string prefix = "Đã lưu phiếu nhập ";
            string receiptCode = message != null && message.StartsWith(prefix, StringComparison.Ordinal)
                ? message.Substring(prefix.Length)
                : message;
            AppToast.Success(this, "Đã lập phiếu " + receiptCode);
        }

        public void ResetLines()
        {
            _linesGrid.Rows.Clear();
            UpdateLineCountAndEmptyState();
        }

        private void ConfigureEntryControls()
        {
            _quantityTextBox.MaxLength = 9;
            _quantityTextBox.KeyPress += IntegerTextBox_KeyPress;
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            Height = Math.Min(Height, Screen.FromControl(this).WorkingArea.Height - 40);
        }

        private void AddLineFromInputs()
        {
            ProductOption product = _productComboBox.SelectedItem as ProductOption;
            if (product == null)
            {
                ShowValidationError("Vui lòng chọn sản phẩm.", _productComboBox);
                return;
            }

            int quantity;
            if (!int.TryParse(_quantityTextBox.Text.Trim(), NumberStyles.Integer,
                CultureInfo.InvariantCulture, out quantity) || quantity <= 0)
            {
                ShowValidationError("Số lượng phải là số nguyên lớn hơn 0.", _quantityTextBox);
                return;
            }

            string priceDigits = NumberInputFormatter.DigitsOnly(_priceTextBox.Text);
            decimal importPrice;
            if (string.IsNullOrWhiteSpace(priceDigits) ||
                !decimal.TryParse(priceDigits, NumberStyles.None, CultureInfo.InvariantCulture, out importPrice) ||
                importPrice < 0m)
            {
                ShowValidationError("Nhập giá ≥ 0.", _priceTextBox);
                return;
            }

            string lotNumber = _lotNumberTextBox.Text.Trim();
            if (lotNumber.Length == 0)
            {
                ShowValidationError(
                    "Vui lòng ghi lại số lô in trên bao bì/phiếu giao của nhà cung cấp trước khi thêm dòng.",
                    _lotNumberTextBox);
                return;
            }

            DateTime? manufactureDate = _manufactureDateBox.Value;
            DateTime? expiryDate = _expiryDateBox.Value;
            if (manufactureDate.HasValue && expiryDate.HasValue && expiryDate.Value.Date <= manufactureDate.Value.Date)
            {
                ShowValidationError("Hạn sử dụng phải sau ngày sản xuất.", _expiryDateBox);
                return;
            }

            AddLine(product, quantity, importPrice, lotNumber, manufactureDate, expiryDate);
            ClearEntryFields();
            LinesChanged?.Invoke(this, EventArgs.Empty);
            _productComboBox.Focus();
        }

        private void AddLine(
            ProductOption product,
            int quantity,
            decimal importPrice,
            string lotNumber,
            DateTime? manufactureDate,
            DateTime? expiryDate)
        {
            int index = _linesGrid.Rows.Add(
                product.Name,
                quantity,
                importPrice,
                lotNumber,
                manufactureDate.HasValue ? manufactureDate.Value.ToString("dd/MM/yyyy", VietnameseCulture) : "-",
                expiryDate.HasValue ? expiryDate.Value.ToString("dd/MM/yyyy", VietnameseCulture) : "-",
                quantity * importPrice,
                "✕");
            _linesGrid.Rows[index].Tag = product;
            UpdateLineCountAndEmptyState();
        }

        private void ClearEntryFields()
        {
            _productComboBox.SelectedIndex = -1;
            _productComboBox.Text = string.Empty;
            _quantityTextBox.Clear();
            _priceTextBox.Clear();
            _lotNumberTextBox.Clear();
            _manufactureDateBox.Value = null;
            _expiryDateBox.Value = null;
        }

        private void SaveReceipt()
        {
            if (SelectedSupplierId <= 0)
            {
                ShowValidationError("Vui lòng chọn nhà cung cấp.", _supplierComboBox);
                return;
            }

            IReadOnlyList<PurchaseLineDto> lines = ReadLines();
            if (lines.Count == 0)
            {
                ShowValidationError("Thêm ít nhất 1 dòng sản phẩm trước khi lưu phiếu.", _productComboBox);
                return;
            }

            SupplierOption supplier = _supplierComboBox.SelectedItem as SupplierOption;
            string message = "Lập phiếu nhập " + lines.Count.ToString(CultureInfo.InvariantCulture) +
                " dòng từ \"" + (supplier == null ? string.Empty : supplier.Name) + "\"?";
            if (_dialogService.Confirm(this, "Xác nhận lập phiếu nhập", message))
                SaveRequested?.Invoke(this, EventArgs.Empty);
        }

        private void FillDemoData()
        {
            if (_supplierComboBox.SelectedIndex <= 0 && _supplierOptions.Count > 1)
                _supplierComboBox.SelectedIndex = Randomizer.Next(1, _supplierOptions.Count);

            List<ProductOption> candidates = _productOptions
                .OrderBy(product => Randomizer.Next())
                .Take(4)
                .ToList();
            if (candidates.Count == 0)
            {
                AppToast.Warning(this, "Không có sản phẩm đang hoạt động để tạo dữ liệu demo.");
                return;
            }

            int lineCount = candidates.Count < 2 ? candidates.Count : Randomizer.Next(2, candidates.Count + 1);
            candidates = candidates.Take(lineCount).ToList();
            var catalogService = AppComposition.CreateCatalogAdminService();
            foreach (ProductOption product in candidates)
            {
                _productComboBox.SelectedItem = _productOptions.FirstOrDefault(option => option.Id == product.Id);
                _quantityTextBox.Text = Randomizer.Next(10, 201).ToString(CultureInfo.InvariantCulture);

                decimal currentImportPrice = 0m;
                try
                {
                    ProductEditDto productDetails = catalogService.GetProduct(product.Id);
                    if (productDetails != null)
                        currentImportPrice = productDetails.ImportPrice;
                }
                catch
                {
                    currentImportPrice = 0m;
                }

                decimal demoPrice;
                if (currentImportPrice > 0m)
                {
                    decimal variation = (decimal)(Randomizer.NextDouble() * 0.30 - 0.15);
                    demoPrice = Math.Max(0m,
                        Math.Round(currentImportPrice * (1m + variation) / 500m,
                            MidpointRounding.AwayFromZero) * 500m);
                }
                else
                {
                    demoPrice = Randomizer.Next(10, 201) * 500m;
                }
                _priceTextBox.Text = NumberInputFormatter.FormatInteger(
                    demoPrice.ToString("0", CultureInfo.InvariantCulture));

                string[] lotPrefixes = { "LNCC", "PO", "BATCH" };
                _lotNumberTextBox.Text = lotPrefixes[Randomizer.Next(lotPrefixes.Length)] + "-" +
                    Randomizer.Next(100000, 1000000).ToString(CultureInfo.InvariantCulture);
                DateTime manufactureDate = DateTime.Today.AddDays(-Randomizer.Next(1, 61));
                _manufactureDateBox.Value = manufactureDate;
                _expiryDateBox.Value = manufactureDate.AddDays(Randomizer.Next(90, 366));
                AddLineFromInputs();
            }
            _productComboBox.Focus();
        }

        private void ShowValidationError(string message, Control control)
        {
            AppToast.Warning(this, message);
            control.Focus();
            if (control is TextBox textBox)
                textBox.SelectAll();
            else if (control == _productComboBox)
                _productComboBox.DroppedDown = true;
        }

        private void LinesGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != _linesGrid.Columns["RemoveLine"].Index)
                return;

            _linesGrid.Rows.RemoveAt(e.RowIndex);
            UpdateLineCountAndEmptyState();
            LinesChanged?.Invoke(this, EventArgs.Empty);
        }

        private void LinesGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != _removeColumn.Index)
                return;

            e.PaintBackground(e.CellBounds, true);
            Bitmap deleteIcon = IconBitmapCache.Get(
                IconChar.Xmark, AppColors.Error, new Size(16, 16));
            int x = e.CellBounds.Left + (e.CellBounds.Width - deleteIcon.Width) / 2;
            int y = e.CellBounds.Top + (e.CellBounds.Height - deleteIcon.Height) / 2;
            e.Graphics.DrawImageUnscaled(deleteIcon, x, y);
            e.Handled = true;
        }

        private void LinesGrid_CellToolTipTextNeeded(
            object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == _removeColumn.Index)
                e.ToolTipText = "Xóa dòng này";
        }

        private void UpdateLineCountAndEmptyState()
        {
            int count = _linesGrid.Rows.Count;
            _lineCountLabel.Text = count.ToString(CultureInfo.InvariantCulture) + " dòng";
            _emptyLabel.Visible = count == 0;
            _emptyLabel.BringToFront();
        }

        private static void IntegerTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private static DateTime? ReadDate(object value)
        {
            if (value == null || value == DBNull.Value)
                return null;
            DateTime date;
            return DateTime.TryParseExact(Convert.ToString(value, CultureInfo.CurrentCulture),
                "dd/MM/yyyy", VietnameseCulture, DateTimeStyles.None, out date)
                ? date.Date
                : (DateTime?)null;
        }

        private sealed class SupplierOption
        {
            public int Id { get; private set; }
            public string Name { get; private set; }

            public SupplierOption(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public override string ToString() => Name ?? string.Empty;
        }

        private sealed class ProductOption
        {
            public int Id { get; private set; }
            public string Name { get; private set; }

            public ProductOption(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public override string ToString() => Name ?? string.Empty;
        }
    }
}
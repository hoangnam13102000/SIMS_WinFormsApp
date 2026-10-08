using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SIMS_WinFormsApp.Models.DTOs.Catalog;

namespace SIMS_WinFormsApp.Views.Catalog
{
    public sealed partial class frmProductEditor : Form
    {
        private ProductEditDto _model;
        private bool _readOnly;

        public frmProductEditor()
        {
            InitializeComponent();
            Text = "Sản phẩm";
            PopulateLookups(Array.Empty<LookupItem>(), Array.Empty<LookupItem>());
            PopulatePreviewValues();
        }

        private frmProductEditor(
            IWin32Window owner,
            ProductEditDto model,
            IReadOnlyList<LookupItem> categories,
            IReadOnlyList<LookupItem> suppliers,
            bool readOnly)
        {
            InitializeComponent();
            _model = model ?? new ProductEditDto();
            _readOnly = readOnly;
            Text = readOnly
                ? "Chi tiết sản phẩm"
                : (_model.ProductId > 0 ? "Sửa sản phẩm" : "Thêm sản phẩm");

            PopulateLookups(categories, suppliers);
            PopulateModel();
            SetReadOnly(readOnly);

            _uploadImageButton.Click += UploadImage_Click;
            _clearImageButton.Click += ClearImage_Click;
            _saveButton.Click += (sender, args) => SaveAndClose();
            _closeButton.Text = readOnly ? "Đóng" : "Hủy";
            _closeButton.Click += (sender, args) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };
        }

        public static bool TryEdit(
            IWin32Window owner,
            ProductEditDto model,
            IReadOnlyList<LookupItem> categories,
            IReadOnlyList<LookupItem> suppliers,
            bool readOnly,
            out ProductEditDto result)
        {
            result = null;
            using (var dialog = new frmProductEditor(owner, model.Copy(), categories, suppliers, readOnly))
            {
                if (dialog.ShowDialog(owner) != DialogResult.OK) return false;
                result = dialog._model;
                return true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_preview != null)
                {
                    Image preview = _preview.Image;
                    _preview.Image = null;
                    if (preview != null) preview.Dispose();
                }
                if (components != null) components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void PopulateLookups(
            IReadOnlyList<LookupItem> categories,
            IReadOnlyList<LookupItem> suppliers)
        {
            _categoryComboBox.DisplayMember = nameof(LookupItem.Name);
            _categoryComboBox.ValueMember = nameof(LookupItem.Id);
            _categoryComboBox.DataSource = (categories ?? Array.Empty<LookupItem>()).ToList();

            var supplierItems = new List<LookupItem>
            {
                new LookupItem { Id = 0, Name = "(Không chọn)" }
            };
            supplierItems.AddRange(suppliers ?? Array.Empty<LookupItem>());
            _supplierComboBox.DisplayMember = nameof(LookupItem.Name);
            _supplierComboBox.ValueMember = nameof(LookupItem.Id);
            _supplierComboBox.DataSource = supplierItems;

            _statusComboBox.Items.Clear();
            _statusComboBox.Items.AddRange(new object[] { "Đang bán", "Ngừng bán" });
            _statusComboBox.SelectedIndex = 0;
        }

        private void PopulateModel()
        {
            _codeLabel.Text = string.IsNullOrWhiteSpace(_model.Code) ? "Mã sẽ được tạo khi lưu" : _model.Code;
            _nameTextBox.Text = _model.Name ?? string.Empty;
            _brandTextBox.Text = _model.Brand ?? string.Empty;
            _unitTextBox.Text = _model.Unit ?? string.Empty;
            _weightTextBox.Text = _model.WeightVolume ?? string.Empty;
            _descriptionTextBox.Text = _model.Description ?? string.Empty;
            _importPriceTextBox.Text = _model.ImportPrice.ToString("0", CultureInfo.InvariantCulture);
            _sellPriceTextBox.Text = _model.SellPrice.ToString("0", CultureInfo.InvariantCulture);
            _stockTextBox.Text = _model.Stock.ToString(CultureInfo.InvariantCulture);
            _minStockTextBox.Text = _model.MinStock.ToString(CultureInfo.InvariantCulture);
            SelectLookup(_categoryComboBox, _model.CategoryId);
            SelectLookup(_supplierComboBox, _model.SupplierId ?? 0);
            _statusComboBox.SelectedIndex = _model.IsActive ? 0 : 1;
            ShowPreview(_model.ImagePath);
            UpdateDetailSummary();
        }

        private void PopulatePreviewValues()
        {
            _codeLabel.Text = "Mã sản phẩm";
            _statusComboBox.SelectedIndex = 0;
            _categoryComboBox.SelectedIndex = -1;
            _supplierComboBox.SelectedIndex = 0;
            _detailSummaryLabel.Text = "Thông tin tồn kho và trạng thái sẽ hiển thị ở đây.";
        }

        private void SetReadOnly(bool readOnly)
        {
            _nameTextBox.ReadOnly = readOnly;
            _brandTextBox.ReadOnly = readOnly;
            _unitTextBox.ReadOnly = readOnly;
            _weightTextBox.ReadOnly = readOnly;
            _descriptionTextBox.ReadOnly = readOnly;
            _importPriceTextBox.ReadOnly = readOnly;
            _sellPriceTextBox.ReadOnly = readOnly;
            _stockTextBox.ReadOnly = readOnly || _model.ProductId > 0;
            _minStockTextBox.ReadOnly = readOnly;
            _categoryComboBox.Enabled = !readOnly;
            _supplierComboBox.Enabled = !readOnly;
            _statusComboBox.Enabled = !readOnly;
            _uploadImageButton.Enabled = !readOnly;
            _clearImageButton.Enabled = !readOnly;
            _saveButton.Visible = !readOnly;
            _closeButton.Text = readOnly ? "Đóng" : "Hủy";
            _detailSummaryLabel.Visible = readOnly;
            if (readOnly)
                _detailsGroup.Text = "Thông tin chi tiết";
        }

        private void UpdateDetailSummary()
        {
            if (_model == null) return;
            string stockStatus = _model.Stock <= 0
                ? "Hết hàng"
                : (_model.Stock <= _model.MinStock ? "Sắp hết hàng" : "Còn hàng");
            decimal profit = _model.SellPrice - _model.ImportPrice;
            string categoryName = (_categoryComboBox.SelectedItem as LookupItem)?.Name ?? "Chưa phân loại";
            _detailSummaryLabel.Text =
                "Mã: " + (string.IsNullOrWhiteSpace(_model.Code) ? "Chưa có" : _model.Code) +
                "    Danh mục: " + categoryName +
                "    Trạng thái: " + (_model.IsActive ? "Đang bán" : "Ngừng bán") +
                Environment.NewLine +
                "Tồn kho: " + _model.Stock + " sản phẩm  |  Tối thiểu: " + _model.MinStock +
                "  |  " + stockStatus +
                Environment.NewLine +
                "Giá nhập: " + FormatMoney(_model.ImportPrice) +
                " VNĐ  |  Giá bán: " + FormatMoney(_model.SellPrice) +
                " VNĐ  |  Chênh lệch: " + FormatMoney(profit) + " VNĐ";
        }

        private static string FormatMoney(decimal amount) =>
            amount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));

        private void SelectLookup(ComboBox combo, int id)
        {
            for (int index = 0; index < combo.Items.Count; index++)
            {
                if (combo.Items[index] is LookupItem item && item.Id == id)
                {
                    combo.SelectedIndex = index;
                    return;
                }
            }
            combo.SelectedIndex = combo.Items.Count == 0 ? -1 : 0;
        }

        private void SaveAndClose()
        {
            if (_model == null || _readOnly) return;
            if (!TryParseMoney(_importPriceTextBox.Text, out decimal importPrice) ||
                !TryParseMoney(_sellPriceTextBox.Text, out decimal sellPrice) ||
                importPrice < 0 || sellPrice < 0)
            {
                MessageBox.Show(this, "Giá nhập và giá bán phải là số không âm.",
                    "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(_stockTextBox.Text, out int stock) || stock < 0 ||
                !int.TryParse(_minStockTextBox.Text, out int minStock) || minStock < 0)
            {
                MessageBox.Show(this, "Tồn kho và tồn kho tối thiểu phải là số nguyên không âm.",
                    "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var category = _categoryComboBox.SelectedItem as LookupItem;
            var supplier = _supplierComboBox.SelectedItem as LookupItem;
            if (category == null)
            {
                MessageBox.Show(this, "Vui lòng chọn danh mục sản phẩm.",
                    "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _model.Name = _nameTextBox.Text.Trim();
            _model.CategoryId = category.Id;
            _model.SupplierId = supplier == null || supplier.Id <= 0 ? (int?)null : supplier.Id;
            _model.Brand = _brandTextBox.Text;
            _model.Unit = _unitTextBox.Text;
            _model.WeightVolume = _weightTextBox.Text;
            _model.Description = _descriptionTextBox.Text;
            _model.ImportPrice = importPrice;
            _model.SellPrice = sellPrice;
            _model.Stock = stock;
            _model.MinStock = minStock;
            _model.IsActive = _statusComboBox.SelectedIndex == 0;
            DialogResult = DialogResult.OK;
            Close();
        }

        private static bool TryParseMoney(string text, out decimal value)
        {
            string normalized = (text ?? string.Empty)
                .Replace(".", string.Empty)
                .Replace(",", string.Empty)
                .Trim();
            return decimal.TryParse(normalized, NumberStyles.Number, CultureInfo.InvariantCulture, out value);
        }

        private void UploadImage_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog
            {
                Filter = "Ảnh|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
                Title = "Chọn ảnh sản phẩm"
            })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                _model.ImagePath = dialog.FileName;
                ShowPreview(dialog.FileName);
            }
        }

        private void ClearImage_Click(object sender, EventArgs e)
        {
            _model.ImagePath = null;
            ShowPreview(null);
        }

        private void ShowPreview(string path)
        {
            Image previous = _preview.Image;
            _preview.Image = null;
            if (previous != null) previous.Dispose();
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path)) return;

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var source = Image.FromStream(stream))
                _preview.Image = new Bitmap(source);
        }
    }
}

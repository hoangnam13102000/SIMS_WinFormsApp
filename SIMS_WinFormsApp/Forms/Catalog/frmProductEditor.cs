using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Forms.Catalog
{
    public sealed class frmProductEditor : BaseFormDialogForm
    {
        private readonly ProductEditDto _model;
        private readonly bool _readOnly;
        private readonly PictureBox _preview;
        private readonly LabeledIconField _name;
        private readonly LabeledComboField _category;
        private readonly LabeledComboField _supplier;
        private readonly LabeledIconField _brand;
        private readonly LabeledIconField _unit;
        private readonly LabeledIconField _weight;
        private readonly LabeledIconField _importPrice;
        private readonly LabeledIconField _sellPrice;
        private readonly LabeledIconField _stock;
        private readonly LabeledIconField _minStock;
        private readonly LabeledIconField _description;
        private readonly List<LookupItem> _categories;
        private readonly List<LookupItem> _suppliers;

        private frmProductEditor(IWin32Window owner, ProductEditDto model, IReadOnlyList<LookupItem> categories, IReadOnlyList<LookupItem> suppliers, bool readOnly)
            : base(owner)
        {
            _model = model ?? new ProductEditDto();
            _readOnly = readOnly;
            _categories = (categories ?? Array.Empty<LookupItem>()).ToList();
            _suppliers = new List<LookupItem> { new LookupItem { Id = 0, Name = "(Không chọn)" } };
            _suppliers.AddRange(suppliers ?? Array.Empty<LookupItem>());

            HeaderTitle = readOnly ? "Chi tiết sản phẩm" : (_model.ProductId > 0 ? "Sửa sản phẩm" : "Thêm sản phẩm");
            SetHeaderIcon(IconChar.Box, AppColors.Accent);
            Size = new Size(680, 760);

            _preview = new PictureBox
            {
                Size = new Size(88, 88),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = AppColors.BgLighter
            };
            _name = Field("Tên sản phẩm", IconChar.Barcode, true);
            _category = Combo("Danh mục", true);
            _supplier = Combo("Nhà cung cấp", false);
            _brand = Field("Thương hiệu", IconChar.Copyright, false);
            _unit = Field("Đơn vị tính", IconChar.ScaleBalanced, false);
            _weight = Field("Khối lượng / dung tích", IconChar.WeightScale, false);
            _importPrice = Field("Giá nhập", IconChar.MoneyBill, true);
            _sellPrice = Field("Giá bán", IconChar.Tags, true);
            _stock = Field("Tồn kho ban đầu", IconChar.BoxesStacked, false);
            _minStock = Field("Tồn tối thiểu", IconChar.TriangleExclamation, false);
            _description = Field("Mô tả", IconChar.AlignLeft, false);

            _category.SetItems(_categories);
            _supplier.SetItems(_suppliers);
            LoadModel();
            if (_model.ProductId > 0 || readOnly) _stock.Enabled = false;
            if (readOnly)
            {
                _name.Enabled = false;
                _category.Enabled = false;
                _supplier.Enabled = false;
                _brand.Enabled = false;
                _unit.Enabled = false;
                _weight.Enabled = false;
                _importPrice.Enabled = false;
                _sellPrice.Enabled = false;
                _minStock.Enabled = false;
                _description.Enabled = false;
            }

            var imageRow = BuildImageRow();
            ContentHost.Controls.Add(_description);
            ContentHost.Controls.Add(_minStock);
            ContentHost.Controls.Add(_stock);
            ContentHost.Controls.Add(_sellPrice);
            ContentHost.Controls.Add(_importPrice);
            ContentHost.Controls.Add(_weight);
            ContentHost.Controls.Add(_unit);
            ContentHost.Controls.Add(_brand);
            ContentHost.Controls.Add(_supplier);
            ContentHost.Controls.Add(_category);
            ContentHost.Controls.Add(_name);
            ContentHost.Controls.Add(imageRow);

            AddFooterButton("Đóng", false, (s, e) => { DialogResult = DialogResult.Cancel; Close(); });
            if (!readOnly)
                AddFooterButton("Lưu", true, (s, e) => SaveAndClose());
        }

        public static bool TryEdit(IWin32Window owner, ProductEditDto model, IReadOnlyList<LookupItem> categories, IReadOnlyList<LookupItem> suppliers, bool readOnly, out ProductEditDto result)
        {
            result = null;
            using (var dialog = new frmProductEditor(owner, model.Copy(), categories, suppliers, readOnly))
            {
                if (dialog.ShowDialog(owner) != DialogResult.OK) return false;
                result = dialog._model;
                return true;
            }
        }

        private void LoadModel()
        {
            _name.Value = _model.Name;
            _brand.Value = _model.Brand;
            _unit.Value = _model.Unit;
            _weight.Value = _model.WeightVolume;
            _importPrice.Value = _model.ImportPrice.ToString("0");
            _sellPrice.Value = _model.SellPrice.ToString("0");
            _stock.Value = _model.Stock.ToString();
            _minStock.Value = _model.MinStock.ToString();
            _description.Value = _model.Description;
            Select(_category, _categories, _model.CategoryId);
            Select(_supplier, _suppliers, _model.SupplierId ?? 0);
            ShowPreview(_model.ImagePath);
        }

        private void SaveAndClose()
        {
            if (!decimal.TryParse((_importPrice.Value ?? string.Empty).Replace(".", string.Empty).Replace(",", string.Empty), out decimal importPrice) ||
                !decimal.TryParse((_sellPrice.Value ?? string.Empty).Replace(".", string.Empty).Replace(",", string.Empty), out decimal sellPrice))
            {
                DialogHelper.ShowWarning(this, "Giá nhập và giá bán phải là số.");
                return;
            }

            int stock = _model.Stock;
            int minStock = _model.MinStock;
            int.TryParse(_stock.Value, out stock);
            int.TryParse(_minStock.Value, out minStock);
            var category = _category.SelectedItem as LookupItem;
            var supplier = _supplier.SelectedItem as LookupItem;

            _model.Name = _name.Value;
            _model.CategoryId = category == null ? 0 : category.Id;
            _model.SupplierId = supplier == null || supplier.Id <= 0 ? (int?)null : supplier.Id;
            _model.Brand = _brand.Value;
            _model.Unit = _unit.Value;
            _model.WeightVolume = _weight.Value;
            _model.Description = _description.Value;
            _model.ImportPrice = importPrice;
            _model.SellPrice = sellPrice;
            _model.Stock = stock;
            _model.MinStock = minStock;
            DialogResult = DialogResult.OK;
            Close();
        }

        private Control BuildImageRow()
        {
            var host = new Panel { Dock = DockStyle.Top, Height = 108, BackColor = Color.Transparent };
            _preview.Location = new Point(0, 8);
            var upload = new PrimaryButton
            {
                Text = "Tải ảnh lên",
                IsPrimary = false,
                Size = new Size(140, 40),
                Location = new Point(104, 16)
            };
            var clear = new PrimaryButton
            {
                Text = "Xóa ảnh",
                IsPrimary = false,
                Size = new Size(110, 40),
                Location = new Point(252, 16)
            };
            upload.Click += (s, e) => ChooseImage();
            clear.Click += (s, e) => { _model.ImagePath = null; ShowPreview(null); };
            if (_readOnly)
            {
                upload.Enabled = false;
                clear.Enabled = false;
            }
            host.Controls.Add(_preview);
            host.Controls.Add(upload);
            host.Controls.Add(clear);
            return host;
        }

        private void ChooseImage()
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

        private void ShowPreview(string path)
        {
            Image previous = _preview.Image;
            _preview.Image = null;
            if (previous != null) previous.Dispose();
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path)) return;
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                _preview.Image = Image.FromStream(stream);
        }

        private static LabeledIconField Field(string label, IconChar icon, bool required)
        {
            return new LabeledIconField { LabelText = label, Icon = icon, IsRequired = required, Dock = DockStyle.Top };
        }

        private static LabeledComboField Combo(string label, bool required)
        {
            return new LabeledComboField { LabelText = label, IsRequired = required, Dock = DockStyle.Top };
        }

        private static void Select(LabeledComboField combo, IList<LookupItem> items, int id)
        {
            combo.SelectedItem = items.FirstOrDefault(item => item.Id == id) ?? (items.Count > 0 ? items[0] : null);
        }
    }
}

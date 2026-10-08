using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Controls;

namespace SIMS_WinFormsApp.Forms.Catalog
{
    partial class frmProductEditor
    {
        private void InitializeComponent()
        {
            this._designEditLayout = new System.Windows.Forms.TableLayoutPanel();
            this._name = new SIMS_WinFormsApp.UI.Controls.LabeledIconField();
            this._category = new SIMS_WinFormsApp.UI.Controls.LabeledComboField();
            this._supplier = new SIMS_WinFormsApp.UI.Controls.LabeledComboField();
            this._brand = new SIMS_WinFormsApp.UI.Controls.LabeledIconField();
            this._unit = new SIMS_WinFormsApp.UI.Controls.LabeledIconField();
            this._weight = new SIMS_WinFormsApp.UI.Controls.LabeledIconField();
            this._description = new SIMS_WinFormsApp.UI.Controls.LabeledIconField();
            this._importPrice = new SIMS_WinFormsApp.UI.Controls.LabeledIconField();
            this._sellPrice = new SIMS_WinFormsApp.UI.Controls.LabeledIconField();
            this._stock = new SIMS_WinFormsApp.UI.Controls.LabeledIconField();
            this._minStock = new SIMS_WinFormsApp.UI.Controls.LabeledIconField();
            this._status = new SIMS_WinFormsApp.UI.Controls.LabeledComboField();
            this._imageRow = new System.Windows.Forms.Panel();
            this._preview = new System.Windows.Forms.PictureBox();
            this._uploadImageButton = new SIMS_WinFormsApp.UI.Controls.PrimaryButton();
            this._clearImageButton = new SIMS_WinFormsApp.UI.Controls.PrimaryButton();
            this._designEditLayout.SuspendLayout();
            this._imageRow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._preview)).BeginInit();
            this.SuspendLayout();
            // 
            // _designEditLayout
            // 
            this._designEditLayout.AutoSize = true;
            this._designEditLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._designEditLayout.ColumnCount = 3;
            this._designEditLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
            this._designEditLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.334F));
            this._designEditLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
            this._designEditLayout.Controls.Add(this._name, 0, 0);
            this._designEditLayout.Controls.Add(this._category, 0, 1);
            this._designEditLayout.Controls.Add(this._supplier, 1, 1);
            this._designEditLayout.Controls.Add(this._brand, 2, 1);
            this._designEditLayout.Controls.Add(this._unit, 0, 2);
            this._designEditLayout.Controls.Add(this._weight, 1, 2);
            this._designEditLayout.Controls.Add(this._description, 2, 2);
            this._designEditLayout.Controls.Add(this._importPrice, 0, 3);
            this._designEditLayout.Controls.Add(this._sellPrice, 1, 3);
            this._designEditLayout.Controls.Add(this._stock, 2, 3);
            this._designEditLayout.Controls.Add(this._minStock, 0, 4);
            this._designEditLayout.Controls.Add(this._status, 1, 4);
            this._designEditLayout.Controls.Add(this._imageRow, 0, 5);
            this._designEditLayout.Dock = System.Windows.Forms.DockStyle.Top;
            this._designEditLayout.Location = new System.Drawing.Point(28, 20);
            this._designEditLayout.Name = "_designEditLayout";
            this._designEditLayout.Padding = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this._designEditLayout.RowCount = 6;
            this._designEditLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._designEditLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._designEditLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._designEditLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._designEditLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._designEditLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._designEditLayout.Size = new System.Drawing.Size(914, 653);
            this._designEditLayout.TabIndex = 0;
            // 
            // _name
            // 
            this._name.AutoSize = true;
            this._name.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._name.BackColor = System.Drawing.Color.Transparent;
            this._designEditLayout.SetColumnSpan(this._name, 3);
            this._name.Dock = System.Windows.Forms.DockStyle.Fill;
            this._name.HintText = "";
            this._name.Icon = FontAwesome.Sharp.IconChar.Barcode;
            this._name.IsRequired = true;
            this._name.LabelText = "Tên sản phẩm";
            this._name.Location = new System.Drawing.Point(8, 6);
            this._name.Margin = new System.Windows.Forms.Padding(8, 6, 8, 8);
            this._name.MaxLength = 32767;
            this._name.Name = "_name";
            this._name.PlaceholderText = "";
            this._name.Size = new System.Drawing.Size(898, 108);
            this._name.TabIndex = 0;
            this._name.UseThousandsSeparator = false;
            this._name.Value = "";
            // 
            // _category
            // 
            this._category.AutoSize = true;
            this._category.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._category.BackColor = System.Drawing.Color.Transparent;
            this._category.Dock = System.Windows.Forms.DockStyle.Fill;
            this._category.IsRequired = true;
            this._category.LabelText = "Danh mục";
            this._category.Location = new System.Drawing.Point(8, 107);
            this._category.Margin = new System.Windows.Forms.Padding(8, 6, 8, 8);
            this._category.Name = "_category";
            this._category.SelectedItem = null;
            this._category.Size = new System.Drawing.Size(288, 100);
            this._category.TabIndex = 1;
            // 
            // _supplier
            // 
            this._supplier.AutoSize = true;
            this._supplier.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._supplier.BackColor = System.Drawing.Color.Transparent;
            this._supplier.Dock = System.Windows.Forms.DockStyle.Fill;
            this._supplier.IsRequired = false;
            this._supplier.LabelText = "Nhà cung cấp";
            this._supplier.Location = new System.Drawing.Point(312, 107);
            this._supplier.Margin = new System.Windows.Forms.Padding(8, 6, 8, 8);
            this._supplier.Name = "_supplier";
            this._supplier.SelectedItem = null;
            this._supplier.Size = new System.Drawing.Size(288, 100);
            this._supplier.TabIndex = 2;
            // 
            // _brand
            // 
            this._brand.AutoSize = true;
            this._brand.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._brand.BackColor = System.Drawing.Color.Transparent;
            this._brand.Dock = System.Windows.Forms.DockStyle.Fill;
            this._brand.HintText = "";
            this._brand.Icon = FontAwesome.Sharp.IconChar.Copyright;
            this._brand.IsRequired = false;
            this._brand.LabelText = "Thương hiệu";
            this._brand.Location = new System.Drawing.Point(616, 107);
            this._brand.Margin = new System.Windows.Forms.Padding(8, 6, 8, 8);
            this._brand.MaxLength = 32767;
            this._brand.Name = "_brand";
            this._brand.PlaceholderText = "";
            this._brand.Size = new System.Drawing.Size(290, 108);
            this._brand.TabIndex = 3;
            this._brand.UseThousandsSeparator = false;
            this._brand.Value = "";
            // 
            // _unit
            // 
            this._unit.AutoSize = true;
            this._unit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._unit.BackColor = System.Drawing.Color.Transparent;
            this._unit.Dock = System.Windows.Forms.DockStyle.Fill;
            this._unit.HintText = "";
            this._unit.Icon = FontAwesome.Sharp.IconChar.ScaleBalanced;
            this._unit.IsRequired = false;
            this._unit.LabelText = "Đơn vị tính";
            this._unit.Location = new System.Drawing.Point(8, 208);
            this._unit.Margin = new System.Windows.Forms.Padding(8, 6, 8, 8);
            this._unit.MaxLength = 32767;
            this._unit.Name = "_unit";
            this._unit.PlaceholderText = "";
            this._unit.Size = new System.Drawing.Size(288, 108);
            this._unit.TabIndex = 4;
            this._unit.UseThousandsSeparator = false;
            this._unit.Value = "";
            // 
            // _weight
            // 
            this._weight.AutoSize = true;
            this._weight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._weight.BackColor = System.Drawing.Color.Transparent;
            this._weight.Dock = System.Windows.Forms.DockStyle.Fill;
            this._weight.HintText = "";
            this._weight.Icon = FontAwesome.Sharp.IconChar.Weight;
            this._weight.IsRequired = false;
            this._weight.LabelText = "Khối lượng / dung tích";
            this._weight.Location = new System.Drawing.Point(312, 208);
            this._weight.Margin = new System.Windows.Forms.Padding(8, 6, 8, 8);
            this._weight.MaxLength = 32767;
            this._weight.Name = "_weight";
            this._weight.PlaceholderText = "";
            this._weight.Size = new System.Drawing.Size(288, 108);
            this._weight.TabIndex = 5;
            this._weight.UseThousandsSeparator = false;
            this._weight.Value = "";
            // 
            // _description
            // 
            this._description.AutoSize = true;
            this._description.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._description.BackColor = System.Drawing.Color.Transparent;
            this._description.Dock = System.Windows.Forms.DockStyle.Fill;
            this._description.HintText = "";
            this._description.Icon = FontAwesome.Sharp.IconChar.AlignLeft;
            this._description.IsRequired = false;
            this._description.LabelText = "Mô tả sản phẩm";
            this._description.Location = new System.Drawing.Point(616, 208);
            this._description.Margin = new System.Windows.Forms.Padding(8, 6, 8, 8);
            this._description.MaxLength = 32767;
            this._description.Name = "_description";
            this._description.PlaceholderText = "";
            this._description.Size = new System.Drawing.Size(290, 108);
            this._description.TabIndex = 6;
            this._description.UseThousandsSeparator = false;
            this._description.Value = "";
            // 
            // _importPrice
            // 
            this._importPrice.AutoSize = true;
            this._importPrice.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._importPrice.BackColor = System.Drawing.Color.Transparent;
            this._importPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this._importPrice.HintText = "";
            this._importPrice.Icon = FontAwesome.Sharp.IconChar.MoneyBill;
            this._importPrice.IsRequired = true;
            this._importPrice.LabelText = "Giá nhập";
            this._importPrice.Location = new System.Drawing.Point(8, 309);
            this._importPrice.Margin = new System.Windows.Forms.Padding(8, 6, 8, 8);
            this._importPrice.MaxLength = 32767;
            this._importPrice.Name = "_importPrice";
            this._importPrice.PlaceholderText = "";
            this._importPrice.Size = new System.Drawing.Size(288, 108);
            this._importPrice.TabIndex = 7;
            this._importPrice.UseThousandsSeparator = false;
            this._importPrice.Value = "";
            // 
            // _sellPrice
            // 
            this._sellPrice.AutoSize = true;
            this._sellPrice.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._sellPrice.BackColor = System.Drawing.Color.Transparent;
            this._sellPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this._sellPrice.HintText = "";
            this._sellPrice.Icon = FontAwesome.Sharp.IconChar.Tags;
            this._sellPrice.IsRequired = true;
            this._sellPrice.LabelText = "Giá bán";
            this._sellPrice.Location = new System.Drawing.Point(312, 309);
            this._sellPrice.Margin = new System.Windows.Forms.Padding(8, 6, 8, 8);
            this._sellPrice.MaxLength = 32767;
            this._sellPrice.Name = "_sellPrice";
            this._sellPrice.PlaceholderText = "";
            this._sellPrice.Size = new System.Drawing.Size(288, 108);
            this._sellPrice.TabIndex = 8;
            this._sellPrice.UseThousandsSeparator = false;
            this._sellPrice.Value = "";
            // 
            // _stock
            // 
            this._stock.AutoSize = true;
            this._stock.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._stock.BackColor = System.Drawing.Color.Transparent;
            this._stock.Dock = System.Windows.Forms.DockStyle.Fill;
            this._stock.HintText = "";
            this._stock.Icon = FontAwesome.Sharp.IconChar.BoxesStacked;
            this._stock.IsRequired = false;
            this._stock.LabelText = "Tồn kho ban đầu";
            this._stock.Location = new System.Drawing.Point(616, 309);
            this._stock.Margin = new System.Windows.Forms.Padding(8, 6, 8, 8);
            this._stock.MaxLength = 32767;
            this._stock.Name = "_stock";
            this._stock.PlaceholderText = "";
            this._stock.Size = new System.Drawing.Size(290, 108);
            this._stock.TabIndex = 9;
            this._stock.UseThousandsSeparator = false;
            this._stock.Value = "";
            // 
            // _minStock
            // 
            this._minStock.AutoSize = true;
            this._minStock.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._minStock.BackColor = System.Drawing.Color.Transparent;
            this._minStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this._minStock.HintText = "";
            this._minStock.Icon = FontAwesome.Sharp.IconChar.ExclamationTriangle;
            this._minStock.IsRequired = false;
            this._minStock.LabelText = "Tồn kho tối thiểu";
            this._minStock.Location = new System.Drawing.Point(8, 410);
            this._minStock.Margin = new System.Windows.Forms.Padding(8, 6, 8, 8);
            this._minStock.MaxLength = 32767;
            this._minStock.Name = "_minStock";
            this._minStock.PlaceholderText = "";
            this._minStock.Size = new System.Drawing.Size(288, 108);
            this._minStock.TabIndex = 10;
            this._minStock.UseThousandsSeparator = false;
            this._minStock.Value = "";
            // 
            // _status
            // 
            this._status.AutoSize = true;
            this._status.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._status.BackColor = System.Drawing.Color.Transparent;
            this._designEditLayout.SetColumnSpan(this._status, 2);
            this._status.Dock = System.Windows.Forms.DockStyle.Fill;
            this._status.IsRequired = true;
            this._status.LabelText = "Trạng thái";
            this._status.Location = new System.Drawing.Point(312, 410);
            this._status.Margin = new System.Windows.Forms.Padding(8, 6, 8, 8);
            this._status.Name = "_status";
            this._status.SelectedItem = null;
            this._status.Size = new System.Drawing.Size(594, 100);
            this._status.TabIndex = 11;
            // 
            // _imageRow
            // 
            this._imageRow.BackColor = System.Drawing.Color.Transparent;
            this._designEditLayout.SetColumnSpan(this._imageRow, 3);
            this._imageRow.Controls.Add(this._preview);
            this._imageRow.Controls.Add(this._uploadImageButton);
            this._imageRow.Controls.Add(this._clearImageButton);
            this._imageRow.Dock = System.Windows.Forms.DockStyle.Top;
            this._imageRow.Location = new System.Drawing.Point(8, 513);
            this._imageRow.Margin = new System.Windows.Forms.Padding(8);
            this._imageRow.Name = "_imageRow";
            this._imageRow.Size = new System.Drawing.Size(898, 120);
            this._imageRow.TabIndex = 12;
            // 
            // _preview
            // 
            this._preview.BackColor = System.Drawing.SystemColors.ControlLight;
            this._preview.Location = new System.Drawing.Point(0, 28);
            this._preview.Name = "_preview";
            this._preview.Size = new System.Drawing.Size(72, 72);
            this._preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._preview.TabIndex = 0;
            this._preview.TabStop = false;
            // 
            // _uploadImageButton
            // 
            this._uploadImageButton.BackColor = System.Drawing.Color.Transparent;
            this._uploadImageButton.CornerRadius = 10;
            this._uploadImageButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this._uploadImageButton.CustomAccentColor = null;
            this._uploadImageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._uploadImageButton.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this._uploadImageButton.ForeColor = System.Drawing.Color.White;
            this._uploadImageButton.Icon = null;
            this._uploadImageButton.IconSize = 16;
            this._uploadImageButton.IsPrimary = false;
            this._uploadImageButton.Location = new System.Drawing.Point(88, 30);
            this._uploadImageButton.MinimumSize = new System.Drawing.Size(120, 42);
            this._uploadImageButton.Name = "_uploadImageButton";
            this._uploadImageButton.Size = new System.Drawing.Size(126, 42);
            this._uploadImageButton.TabIndex = 1;
            this._uploadImageButton.Text = "Tải ảnh lên";
            this._uploadImageButton.UseVisualStyleBackColor = false;
            this._uploadImageButton.Click += new System.EventHandler(this.UploadImage_Click);
            // 
            // _clearImageButton
            // 
            this._clearImageButton.BackColor = System.Drawing.Color.Transparent;
            this._clearImageButton.CornerRadius = 10;
            this._clearImageButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this._clearImageButton.CustomAccentColor = null;
            this._clearImageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._clearImageButton.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this._clearImageButton.ForeColor = System.Drawing.Color.White;
            this._clearImageButton.Icon = null;
            this._clearImageButton.IconSize = 16;
            this._clearImageButton.IsPrimary = false;
            this._clearImageButton.Location = new System.Drawing.Point(226, 30);
            this._clearImageButton.MinimumSize = new System.Drawing.Size(120, 42);
            this._clearImageButton.Name = "_clearImageButton";
            this._clearImageButton.Size = new System.Drawing.Size(120, 42);
            this._clearImageButton.TabIndex = 2;
            this._clearImageButton.Text = "Xóa ảnh";
            this._clearImageButton.UseVisualStyleBackColor = false;
            this._clearImageButton.Click += new System.EventHandler(this.ClearImage_Click);
            // 
            // frmProductEditor
            // 
            this.ClientSize = new System.Drawing.Size(1000, 760);
            this.Name = "frmProductEditor";
            this.Text = "Sản phẩm";
            this._designEditLayout.ResumeLayout(false);
            this._designEditLayout.PerformLayout();
            this._imageRow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._preview)).EndInit();
            this.ResumeLayout(false);

        }
    }
}

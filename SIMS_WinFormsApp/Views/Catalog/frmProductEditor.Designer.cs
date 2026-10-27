using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Catalog
{
    partial class frmProductEditor
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel _root;
        private Label _titleLabel;
        private Panel _codePanel;
        private Label _codeLabel;
        private TableLayoutPanel _body;
        private TableLayoutPanel _fields;
        private GroupBox _detailsGroup;
        private Label _nameLabel;
        private TextBox _nameTextBox;
        private Label _categoryLabel;
        private ComboBox _categoryComboBox;
        private Label _supplierLabel;
        private ComboBox _supplierComboBox;
        private Label _brandLabel;
        private TextBox _brandTextBox;
        private Label _unitLabel;
        private TextBox _unitTextBox;
        private Label _weightLabel;
        private TextBox _weightTextBox;
        private Label _descriptionLabel;
        private TextBox _descriptionTextBox;
        private Label _importPriceLabel;
        private TextBox _importPriceTextBox;
        private Label _sellPriceLabel;
        private TextBox _sellPriceTextBox;
        private Label _stockLabel;
        private TextBox _stockTextBox;
        private Label _minStockLabel;
        private TextBox _minStockTextBox;
        private Label _statusLabel;
        private ComboBox _statusComboBox;
        private GroupBox _imageGroup;
        private PictureBox _preview;
        private Button _uploadImageButton;
        private Button _clearImageButton;
        private Label _detailSummaryLabel;
        private FlowLayoutPanel _footer;
        private Button _saveButton;
        private Button _closeButton;

        private void InitializeComponent()
        {
            this._root = new System.Windows.Forms.TableLayoutPanel();
            this._titleLabel = new System.Windows.Forms.Label();
            this._codePanel = new System.Windows.Forms.Panel();
            this._codeLabel = new System.Windows.Forms.Label();
            this._body = new System.Windows.Forms.TableLayoutPanel();
            this._detailsGroup = new System.Windows.Forms.GroupBox();
            this._fields = new System.Windows.Forms.TableLayoutPanel();
            this._nameLabel = new System.Windows.Forms.Label();
            this._nameTextBox = new System.Windows.Forms.TextBox();
            this._categoryLabel = new System.Windows.Forms.Label();
            this._categoryComboBox = new System.Windows.Forms.ComboBox();
            this._supplierLabel = new System.Windows.Forms.Label();
            this._supplierComboBox = new System.Windows.Forms.ComboBox();
            this._brandLabel = new System.Windows.Forms.Label();
            this._brandTextBox = new System.Windows.Forms.TextBox();
            this._unitLabel = new System.Windows.Forms.Label();
            this._unitTextBox = new System.Windows.Forms.TextBox();
            this._weightLabel = new System.Windows.Forms.Label();
            this._weightTextBox = new System.Windows.Forms.TextBox();
            this._statusLabel = new System.Windows.Forms.Label();
            this._statusComboBox = new System.Windows.Forms.ComboBox();
            this._descriptionLabel = new System.Windows.Forms.Label();
            this._descriptionTextBox = new System.Windows.Forms.TextBox();
            this._importPriceLabel = new System.Windows.Forms.Label();
            this._importPriceTextBox = new System.Windows.Forms.TextBox();
            this._sellPriceLabel = new System.Windows.Forms.Label();
            this._sellPriceTextBox = new System.Windows.Forms.TextBox();
            this._stockLabel = new System.Windows.Forms.Label();
            this._stockTextBox = new System.Windows.Forms.TextBox();
            this._minStockLabel = new System.Windows.Forms.Label();
            this._minStockTextBox = new System.Windows.Forms.TextBox();
            this._imageGroup = new System.Windows.Forms.GroupBox();
            this._clearImageButton = new System.Windows.Forms.Button();
            this._uploadImageButton = new System.Windows.Forms.Button();
            this._preview = new System.Windows.Forms.PictureBox();
            this._detailSummaryLabel = new System.Windows.Forms.Label();
            this._footer = new System.Windows.Forms.FlowLayoutPanel();
            this._closeButton = new System.Windows.Forms.Button();
            this._saveButton = new System.Windows.Forms.Button();
            this._root.SuspendLayout();
            this._codePanel.SuspendLayout();
            this._body.SuspendLayout();
            this._detailsGroup.SuspendLayout();
            this._fields.SuspendLayout();
            this._imageGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._preview)).BeginInit();
            this._footer.SuspendLayout();
            this.SuspendLayout();
            // 
            // _root
            // 
            this._root.ColumnCount = 1;
            this._root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Controls.Add(this._titleLabel, 0, 0);
            this._root.Controls.Add(this._codePanel, 0, 1);
            this._root.Controls.Add(this._body, 0, 2);
            this._root.Controls.Add(this._detailSummaryLabel, 0, 3);
            this._root.Controls.Add(this._footer, 0, 4);
            this._root.Dock = System.Windows.Forms.DockStyle.Fill;
            this._root.Location = new System.Drawing.Point(0, 0);
            this._root.Name = "_root";
            this._root.Padding = new System.Windows.Forms.Padding(24, 18, 24, 18);
            this._root.RowCount = 5;
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this._root.Size = new System.Drawing.Size(1020, 760);
            this._root.TabIndex = 0;
            // 
            // _titleLabel
            // 
            this._titleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._titleLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this._titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._titleLabel.Location = new System.Drawing.Point(27, 18);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(966, 48);
            this._titleLabel.TabIndex = 0;
            this._titleLabel.Text = "Thông tin sản phẩm";
            this._titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _codePanel
            // 
            this._codePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this._codePanel.Controls.Add(this._codeLabel);
            this._codePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._codePanel.Location = new System.Drawing.Point(27, 69);
            this._codePanel.Name = "_codePanel";
            this._codePanel.Size = new System.Drawing.Size(966, 26);
            this._codePanel.TabIndex = 1;
            // 
            // _codeLabel
            // 
            this._codeLabel.AutoSize = true;
            this._codeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._codeLabel.Location = new System.Drawing.Point(10, 8);
            this._codeLabel.Name = "_codeLabel";
            this._codeLabel.Size = new System.Drawing.Size(105, 20);
            this._codeLabel.TabIndex = 0;
            this._codeLabel.Text = "Mã sản phẩm";
            // 
            // _body
            // 
            this._body.ColumnCount = 2;
            this._body.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72F));
            this._body.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this._body.Controls.Add(this._detailsGroup, 0, 0);
            this._body.Controls.Add(this._imageGroup, 1, 0);
            this._body.Dock = System.Windows.Forms.DockStyle.Fill;
            this._body.Location = new System.Drawing.Point(27, 101);
            this._body.Name = "_body";
            this._body.Size = new System.Drawing.Size(966, 516);
            this._body.TabIndex = 2;
            // 
            // _detailsGroup
            // 
            this._detailsGroup.Controls.Add(this._fields);
            this._detailsGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this._detailsGroup.Location = new System.Drawing.Point(0, 6);
            this._detailsGroup.Margin = new System.Windows.Forms.Padding(0, 6, 10, 6);
            this._detailsGroup.Name = "_detailsGroup";
            this._detailsGroup.Padding = new System.Windows.Forms.Padding(12);
            this._detailsGroup.Size = new System.Drawing.Size(685, 504);
            this._detailsGroup.TabIndex = 0;
            this._detailsGroup.TabStop = false;
            this._detailsGroup.Text = "Thông tin sản phẩm";
            // 
            // _fields
            // 
            this._fields.ColumnCount = 4;
            this._fields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._fields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._fields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._fields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._fields.Controls.Add(this._nameLabel, 0, 0);
            this._fields.Controls.Add(this._nameTextBox, 0, 1);
            this._fields.Controls.Add(this._categoryLabel, 0, 2);
            this._fields.Controls.Add(this._categoryComboBox, 0, 3);
            this._fields.Controls.Add(this._supplierLabel, 2, 2);
            this._fields.Controls.Add(this._supplierComboBox, 2, 3);
            this._fields.Controls.Add(this._brandLabel, 0, 4);
            this._fields.Controls.Add(this._brandTextBox, 0, 5);
            this._fields.Controls.Add(this._unitLabel, 2, 4);
            this._fields.Controls.Add(this._unitTextBox, 2, 5);
            this._fields.Controls.Add(this._weightLabel, 0, 6);
            this._fields.Controls.Add(this._weightTextBox, 0, 7);
            this._fields.Controls.Add(this._statusLabel, 2, 6);
            this._fields.Controls.Add(this._statusComboBox, 2, 7);
            this._fields.Controls.Add(this._descriptionLabel, 0, 8);
            this._fields.Controls.Add(this._descriptionTextBox, 0, 9);
            this._fields.Controls.Add(this._importPriceLabel, 0, 10);
            this._fields.Controls.Add(this._importPriceTextBox, 0, 11);
            this._fields.Controls.Add(this._sellPriceLabel, 2, 10);
            this._fields.Controls.Add(this._sellPriceTextBox, 2, 11);
            this._fields.Controls.Add(this._stockLabel, 0, 12);
            this._fields.Controls.Add(this._stockTextBox, 0, 13);
            this._fields.Controls.Add(this._minStockLabel, 2, 12);
            this._fields.Controls.Add(this._minStockTextBox, 2, 13);
            this._fields.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fields.Location = new System.Drawing.Point(12, 31);
            this._fields.Name = "_fields";
            this._fields.RowCount = 14;
            this._fields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this._fields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._fields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this._fields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._fields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this._fields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._fields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this._fields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._fields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this._fields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._fields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this._fields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._fields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this._fields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._fields.Size = new System.Drawing.Size(661, 461);
            this._fields.TabIndex = 0;
            // 
            // _nameLabel
            // 
            this._nameLabel.AutoSize = true;
            this._fields.SetColumnSpan(this._nameLabel, 4);
            this._nameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._nameLabel.Location = new System.Drawing.Point(3, 0);
            this._nameLabel.Name = "_nameLabel";
            this._nameLabel.Size = new System.Drawing.Size(655, 24);
            this._nameLabel.TabIndex = 0;
            this._nameLabel.Text = "Tên sản phẩm";
            this._nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _nameTextBox
            // 
            this._fields.SetColumnSpan(this._nameTextBox, 4);
            this._nameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._nameTextBox.Location = new System.Drawing.Point(0, 24);
            this._nameTextBox.Margin = new System.Windows.Forms.Padding(0, 0, 10, 8);
            this._nameTextBox.Name = "_nameTextBox";
            this._nameTextBox.Size = new System.Drawing.Size(651, 26);
            this._nameTextBox.TabIndex = 1;
            // 
            // _categoryLabel
            // 
            this._categoryLabel.AutoSize = true;
            this._fields.SetColumnSpan(this._categoryLabel, 2);
            this._categoryLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._categoryLabel.Location = new System.Drawing.Point(3, 54);
            this._categoryLabel.Name = "_categoryLabel";
            this._categoryLabel.Size = new System.Drawing.Size(324, 24);
            this._categoryLabel.TabIndex = 2;
            this._categoryLabel.Text = "Danh mục";
            this._categoryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _categoryComboBox
            // 
            this._fields.SetColumnSpan(this._categoryComboBox, 2);
            this._categoryComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._categoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._categoryComboBox.Location = new System.Drawing.Point(0, 78);
            this._categoryComboBox.Margin = new System.Windows.Forms.Padding(0, 0, 10, 8);
            this._categoryComboBox.Name = "_categoryComboBox";
            this._categoryComboBox.Size = new System.Drawing.Size(320, 28);
            this._categoryComboBox.TabIndex = 3;
            // 
            // _supplierLabel
            // 
            this._supplierLabel.AutoSize = true;
            this._fields.SetColumnSpan(this._supplierLabel, 2);
            this._supplierLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._supplierLabel.Location = new System.Drawing.Point(333, 54);
            this._supplierLabel.Name = "_supplierLabel";
            this._supplierLabel.Size = new System.Drawing.Size(325, 24);
            this._supplierLabel.TabIndex = 4;
            this._supplierLabel.Text = "Nhà cung cấp";
            this._supplierLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _supplierComboBox
            // 
            this._fields.SetColumnSpan(this._supplierComboBox, 2);
            this._supplierComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._supplierComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._supplierComboBox.Location = new System.Drawing.Point(330, 78);
            this._supplierComboBox.Margin = new System.Windows.Forms.Padding(0, 0, 10, 8);
            this._supplierComboBox.Name = "_supplierComboBox";
            this._supplierComboBox.Size = new System.Drawing.Size(321, 28);
            this._supplierComboBox.TabIndex = 5;
            // 
            // _brandLabel
            // 
            this._brandLabel.AutoSize = true;
            this._fields.SetColumnSpan(this._brandLabel, 2);
            this._brandLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._brandLabel.Location = new System.Drawing.Point(3, 108);
            this._brandLabel.Name = "_brandLabel";
            this._brandLabel.Size = new System.Drawing.Size(324, 24);
            this._brandLabel.TabIndex = 6;
            this._brandLabel.Text = "Thương hiệu";
            this._brandLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _brandTextBox
            // 
            this._fields.SetColumnSpan(this._brandTextBox, 2);
            this._brandTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._brandTextBox.Location = new System.Drawing.Point(0, 132);
            this._brandTextBox.Margin = new System.Windows.Forms.Padding(0, 0, 10, 8);
            this._brandTextBox.Name = "_brandTextBox";
            this._brandTextBox.Size = new System.Drawing.Size(320, 26);
            this._brandTextBox.TabIndex = 7;
            // 
            // _unitLabel
            // 
            this._unitLabel.AutoSize = true;
            this._fields.SetColumnSpan(this._unitLabel, 2);
            this._unitLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._unitLabel.Location = new System.Drawing.Point(333, 108);
            this._unitLabel.Name = "_unitLabel";
            this._unitLabel.Size = new System.Drawing.Size(325, 24);
            this._unitLabel.TabIndex = 8;
            this._unitLabel.Text = "Đơn vị tính";
            this._unitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _unitTextBox
            // 
            this._fields.SetColumnSpan(this._unitTextBox, 2);
            this._unitTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._unitTextBox.Location = new System.Drawing.Point(330, 132);
            this._unitTextBox.Margin = new System.Windows.Forms.Padding(0, 0, 10, 8);
            this._unitTextBox.Name = "_unitTextBox";
            this._unitTextBox.Size = new System.Drawing.Size(321, 26);
            this._unitTextBox.TabIndex = 9;
            // 
            // _weightLabel
            // 
            this._weightLabel.AutoSize = true;
            this._fields.SetColumnSpan(this._weightLabel, 2);
            this._weightLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._weightLabel.Location = new System.Drawing.Point(3, 162);
            this._weightLabel.Name = "_weightLabel";
            this._weightLabel.Size = new System.Drawing.Size(324, 24);
            this._weightLabel.TabIndex = 10;
            this._weightLabel.Text = "Khối lượng / dung tích";
            this._weightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _weightTextBox
            // 
            this._fields.SetColumnSpan(this._weightTextBox, 2);
            this._weightTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._weightTextBox.Location = new System.Drawing.Point(0, 186);
            this._weightTextBox.Margin = new System.Windows.Forms.Padding(0, 0, 10, 8);
            this._weightTextBox.Name = "_weightTextBox";
            this._weightTextBox.Size = new System.Drawing.Size(320, 26);
            this._weightTextBox.TabIndex = 11;
            // 
            // _statusLabel
            // 
            this._statusLabel.AutoSize = true;
            this._fields.SetColumnSpan(this._statusLabel, 2);
            this._statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._statusLabel.Location = new System.Drawing.Point(333, 162);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(325, 24);
            this._statusLabel.TabIndex = 12;
            this._statusLabel.Text = "Trạng thái";
            this._statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _statusComboBox
            // 
            this._fields.SetColumnSpan(this._statusComboBox, 2);
            this._statusComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._statusComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._statusComboBox.Items.AddRange(new object[] {
            "Đang bán",
            "Ngừng bán"});
            this._statusComboBox.Location = new System.Drawing.Point(330, 186);
            this._statusComboBox.Margin = new System.Windows.Forms.Padding(0, 0, 10, 8);
            this._statusComboBox.Name = "_statusComboBox";
            this._statusComboBox.Size = new System.Drawing.Size(321, 28);
            this._statusComboBox.TabIndex = 13;
            // 
            // _descriptionLabel
            // 
            this._descriptionLabel.AutoSize = true;
            this._fields.SetColumnSpan(this._descriptionLabel, 4);
            this._descriptionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._descriptionLabel.Location = new System.Drawing.Point(3, 216);
            this._descriptionLabel.Name = "_descriptionLabel";
            this._descriptionLabel.Size = new System.Drawing.Size(655, 24);
            this._descriptionLabel.TabIndex = 14;
            this._descriptionLabel.Text = "Mô tả sản phẩm";
            this._descriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _descriptionTextBox
            // 
            this._fields.SetColumnSpan(this._descriptionTextBox, 4);
            this._descriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._descriptionTextBox.Location = new System.Drawing.Point(0, 240);
            this._descriptionTextBox.Margin = new System.Windows.Forms.Padding(0, 0, 10, 8);
            this._descriptionTextBox.Multiline = true;
            this._descriptionTextBox.Name = "_descriptionTextBox";
            this._descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._descriptionTextBox.Size = new System.Drawing.Size(651, 105);
            this._descriptionTextBox.TabIndex = 15;
            // 
            // _importPriceLabel
            // 
            this._importPriceLabel.AutoSize = true;
            this._fields.SetColumnSpan(this._importPriceLabel, 2);
            this._importPriceLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._importPriceLabel.Location = new System.Drawing.Point(3, 353);
            this._importPriceLabel.Name = "_importPriceLabel";
            this._importPriceLabel.Size = new System.Drawing.Size(324, 24);
            this._importPriceLabel.TabIndex = 16;
            this._importPriceLabel.Text = "Giá nhập (VNĐ)";
            this._importPriceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _importPriceTextBox
            // 
            this._fields.SetColumnSpan(this._importPriceTextBox, 2);
            this._importPriceTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._importPriceTextBox.Location = new System.Drawing.Point(0, 377);
            this._importPriceTextBox.Margin = new System.Windows.Forms.Padding(0, 0, 10, 8);
            this._importPriceTextBox.Name = "_importPriceTextBox";
            this._importPriceTextBox.Size = new System.Drawing.Size(320, 26);
            this._importPriceTextBox.TabIndex = 17;
            // 
            // _sellPriceLabel
            // 
            this._sellPriceLabel.AutoSize = true;
            this._fields.SetColumnSpan(this._sellPriceLabel, 2);
            this._sellPriceLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._sellPriceLabel.Location = new System.Drawing.Point(333, 353);
            this._sellPriceLabel.Name = "_sellPriceLabel";
            this._sellPriceLabel.Size = new System.Drawing.Size(325, 24);
            this._sellPriceLabel.TabIndex = 18;
            this._sellPriceLabel.Text = "Giá bán (VNĐ)";
            this._sellPriceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _sellPriceTextBox
            // 
            this._fields.SetColumnSpan(this._sellPriceTextBox, 2);
            this._sellPriceTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._sellPriceTextBox.Location = new System.Drawing.Point(330, 377);
            this._sellPriceTextBox.Margin = new System.Windows.Forms.Padding(0, 0, 10, 8);
            this._sellPriceTextBox.Name = "_sellPriceTextBox";
            this._sellPriceTextBox.Size = new System.Drawing.Size(321, 26);
            this._sellPriceTextBox.TabIndex = 19;
            // 
            // _stockLabel
            // 
            this._stockLabel.AutoSize = true;
            this._fields.SetColumnSpan(this._stockLabel, 2);
            this._stockLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._stockLabel.Location = new System.Drawing.Point(3, 407);
            this._stockLabel.Name = "_stockLabel";
            this._stockLabel.Size = new System.Drawing.Size(324, 24);
            this._stockLabel.TabIndex = 20;
            this._stockLabel.Text = "Tồn kho";
            this._stockLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _stockTextBox
            // 
            this._fields.SetColumnSpan(this._stockTextBox, 2);
            this._stockTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._stockTextBox.Location = new System.Drawing.Point(0, 431);
            this._stockTextBox.Margin = new System.Windows.Forms.Padding(0, 0, 10, 8);
            this._stockTextBox.Name = "_stockTextBox";
            this._stockTextBox.Size = new System.Drawing.Size(320, 26);
            this._stockTextBox.TabIndex = 21;
            // 
            // _minStockLabel
            // 
            this._minStockLabel.AutoSize = true;
            this._fields.SetColumnSpan(this._minStockLabel, 2);
            this._minStockLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._minStockLabel.Location = new System.Drawing.Point(333, 407);
            this._minStockLabel.Name = "_minStockLabel";
            this._minStockLabel.Size = new System.Drawing.Size(325, 24);
            this._minStockLabel.TabIndex = 22;
            this._minStockLabel.Text = "Tồn kho tối thiểu";
            this._minStockLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _minStockTextBox
            // 
            this._fields.SetColumnSpan(this._minStockTextBox, 2);
            this._minStockTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._minStockTextBox.Location = new System.Drawing.Point(330, 431);
            this._minStockTextBox.Margin = new System.Windows.Forms.Padding(0, 0, 10, 8);
            this._minStockTextBox.Name = "_minStockTextBox";
            this._minStockTextBox.Size = new System.Drawing.Size(321, 26);
            this._minStockTextBox.TabIndex = 23;
            // 
            // _imageGroup
            // 
            this._imageGroup.Controls.Add(this._clearImageButton);
            this._imageGroup.Controls.Add(this._uploadImageButton);
            this._imageGroup.Controls.Add(this._preview);
            this._imageGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this._imageGroup.Location = new System.Drawing.Point(705, 6);
            this._imageGroup.Margin = new System.Windows.Forms.Padding(10, 6, 0, 6);
            this._imageGroup.Name = "_imageGroup";
            this._imageGroup.Padding = new System.Windows.Forms.Padding(12);
            this._imageGroup.Size = new System.Drawing.Size(261, 504);
            this._imageGroup.TabIndex = 1;
            this._imageGroup.TabStop = false;
            this._imageGroup.Text = "Ảnh sản phẩm";
            // 
            // _clearImageButton
            // 
            this._clearImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._clearImageButton.Location = new System.Drawing.Point(16, 798);
            this._clearImageButton.Name = "_clearImageButton";
            this._clearImageButton.Size = new System.Drawing.Size(283, 34);
            this._clearImageButton.TabIndex = 0;
            this._clearImageButton.Text = "Xóa ảnh";
            // 
            // _uploadImageButton
            // 
            this._uploadImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._uploadImageButton.Location = new System.Drawing.Point(16, 754);
            this._uploadImageButton.Name = "_uploadImageButton";
            this._uploadImageButton.Size = new System.Drawing.Size(283, 36);
            this._uploadImageButton.TabIndex = 1;
            this._uploadImageButton.Text = "Chọn ảnh...";
            // 
            // _preview
            // 
            this._preview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._preview.Location = new System.Drawing.Point(16, 40);
            this._preview.Name = "_preview";
            this._preview.Size = new System.Drawing.Size(283, 704);
            this._preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._preview.TabIndex = 2;
            this._preview.TabStop = false;
            // 
            // _detailSummaryLabel
            // 
            this._detailSummaryLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this._detailSummaryLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._detailSummaryLabel.Location = new System.Drawing.Point(27, 620);
            this._detailSummaryLabel.Name = "_detailSummaryLabel";
            this._detailSummaryLabel.Padding = new System.Windows.Forms.Padding(10);
            this._detailSummaryLabel.Size = new System.Drawing.Size(966, 74);
            this._detailSummaryLabel.TabIndex = 3;
            this._detailSummaryLabel.Text = "Thông tin tồn kho và trạng thái";
            this._detailSummaryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _footer
            // 
            this._footer.Controls.Add(this._closeButton);
            this._footer.Controls.Add(this._saveButton);
            this._footer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._footer.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._footer.Location = new System.Drawing.Point(27, 697);
            this._footer.Name = "_footer";
            this._footer.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this._footer.Size = new System.Drawing.Size(966, 42);
            this._footer.TabIndex = 4;
            // 
            // _closeButton
            // 
            this._closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._closeButton.Location = new System.Drawing.Point(866, 8);
            this._closeButton.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(100, 34);
            this._closeButton.TabIndex = 0;
            this._closeButton.Text = "Hủy";
            // 
            // _saveButton
            // 
            this._saveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._saveButton.FlatAppearance.BorderSize = 0;
            this._saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._saveButton.ForeColor = System.Drawing.Color.White;
            this._saveButton.Location = new System.Drawing.Point(718, 8);
            this._saveButton.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(140, 34);
            this._saveButton.TabIndex = 1;
            this._saveButton.Text = "Lưu thay đổi";
            this._saveButton.UseVisualStyleBackColor = false;
            // 
            // frmProductEditor
            // 
            this.AcceptButton = this._saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._closeButton;
            this.ClientSize = new System.Drawing.Size(1020, 760);
            this.Controls.Add(this._root);
            this.MinimumSize = new System.Drawing.Size(880, 660);
            this.Name = "frmProductEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sản phẩm";
            this._root.ResumeLayout(false);
            this._codePanel.ResumeLayout(false);
            this._codePanel.PerformLayout();
            this._body.ResumeLayout(false);
            this._detailsGroup.ResumeLayout(false);
            this._fields.ResumeLayout(false);
            this._fields.PerformLayout();
            this._imageGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._preview)).EndInit();
            this._footer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

    }
}

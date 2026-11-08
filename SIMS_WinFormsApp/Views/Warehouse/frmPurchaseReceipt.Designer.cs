using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Controls;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    public sealed partial class frmPurchaseReceipt
    {
        private IContainer components;
        private Panel _headerPanel;
        private Panel _headerIconPanel;
        private IconPictureBox _headerIcon;
        private Label _titleLabel;
        private Label _subtitleLabel;
        private Button _demoButton;
        private Panel _headerDivider;
        private Panel _bodyPanel;
        private CardPanel _supplierCard;
        private Label _supplierTitle;
        private Label _supplierRequired;
        private ComboBox _supplierComboBox;
        private CardPanel _entryCard;
        private IconPictureBox _entryIcon;
        private Label _entryTitle;
        private Label _productLabel;
        private Label _quantityLabel;
        private Label _priceLabel;
        private Label _lotNumberLabel;
        private Label _manufactureDateLabel;
        private Label _expiryDateLabel;
        private ComboBox _productComboBox;
        private TextBox _quantityTextBox;
        private TextBox _priceTextBox;
        private TextBox _lotNumberTextBox;
        private DateFilterBox _manufactureDateBox;
        private DateFilterBox _expiryDateBox;
        private Button _addLineButton;
        private CardPanel _linesCard;
        private IconPictureBox _linesIcon;
        private Label _linesTitle;
        private Label _lineCountLabel;
        private DataGridView _linesGrid;
        private Label _emptyLabel;
        private DataGridViewTextBoxColumn _productColumn;
        private DataGridViewTextBoxColumn _quantityColumn;
        private DataGridViewTextBoxColumn _importPriceColumn;
        private DataGridViewTextBoxColumn _lotNumberColumn;
        private DataGridViewTextBoxColumn _manufactureColumn;
        private DataGridViewTextBoxColumn _expiryColumn;
        private DataGridViewTextBoxColumn _amountColumn;
        private DataGridViewTextBoxColumn _removeColumn;
        private Panel _footerPanel;
        private Label _totalCaptionLabel;
        private Label _totalValueLabel;
        private IconButton _cancelButton;
        private IconButton _saveButton;
        private Panel _footerDivider;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            this._headerPanel = new System.Windows.Forms.Panel();
            this._headerIconPanel = new System.Windows.Forms.Panel();
            this._headerIcon = new FontAwesome.Sharp.IconPictureBox();
            this._titleLabel = new System.Windows.Forms.Label();
            this._subtitleLabel = new System.Windows.Forms.Label();
            this._demoButton = new System.Windows.Forms.Button();
            this._headerDivider = new System.Windows.Forms.Panel();
            this._bodyPanel = new System.Windows.Forms.Panel();
            this._footerPanel = new System.Windows.Forms.Panel();
            this._totalCaptionLabel = new System.Windows.Forms.Label();
            this._totalValueLabel = new System.Windows.Forms.Label();
            this._saveButton = new FontAwesome.Sharp.IconButton();
            this._cancelButton = new FontAwesome.Sharp.IconButton();
            this._footerDivider = new System.Windows.Forms.Panel();
            this._supplierCard = new SIMS_WinFormsApp.UI.Controls.CardPanel();
            this._supplierTitle = new System.Windows.Forms.Label();
            this._supplierRequired = new System.Windows.Forms.Label();
            this._supplierComboBox = new System.Windows.Forms.ComboBox();
            this._entryCard = new SIMS_WinFormsApp.UI.Controls.CardPanel();
            this._entryIcon = new FontAwesome.Sharp.IconPictureBox();
            this._entryTitle = new System.Windows.Forms.Label();
            this._productLabel = new System.Windows.Forms.Label();
            this._quantityLabel = new System.Windows.Forms.Label();
            this._priceLabel = new System.Windows.Forms.Label();
            this._productComboBox = new System.Windows.Forms.ComboBox();
            this._quantityTextBox = new System.Windows.Forms.TextBox();
            this._priceTextBox = new System.Windows.Forms.TextBox();
            this._lotNumberLabel = new System.Windows.Forms.Label();
            this._manufactureDateLabel = new System.Windows.Forms.Label();
            this._expiryDateLabel = new System.Windows.Forms.Label();
            this._lotNumberTextBox = new System.Windows.Forms.TextBox();
            this._manufactureDateBox = new SIMS_WinFormsApp.UI.Controls.DateFilterBox();
            this._expiryDateBox = new SIMS_WinFormsApp.UI.Controls.DateFilterBox();
            this._addLineButton = new System.Windows.Forms.Button();
            this._linesCard = new SIMS_WinFormsApp.UI.Controls.CardPanel();
            this._linesIcon = new FontAwesome.Sharp.IconPictureBox();
            this._linesTitle = new System.Windows.Forms.Label();
            this._lineCountLabel = new System.Windows.Forms.Label();
            this._linesGrid = new System.Windows.Forms.DataGridView();
            this.Product = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImportPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LotNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ManufactureDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpiryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RemoveLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._emptyLabel = new System.Windows.Forms.Label();
            this._headerPanel.SuspendLayout();
            this._headerIconPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).BeginInit();
            this._bodyPanel.SuspendLayout();
            this._footerPanel.SuspendLayout();
            this._supplierCard.SuspendLayout();
            this._entryCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._entryIcon)).BeginInit();
            this._linesCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._linesIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._linesGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // _headerPanel
            // 
            this._headerPanel.BackColor = System.Drawing.Color.White;
            this._headerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._headerPanel.Controls.Add(this._headerIconPanel);
            this._headerPanel.Controls.Add(this._titleLabel);
            this._headerPanel.Controls.Add(this._subtitleLabel);
            this._headerPanel.Controls.Add(this._demoButton);
            this._headerPanel.Controls.Add(this._headerDivider);
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._headerPanel.Location = new System.Drawing.Point(0, 0);
            this._headerPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._headerPanel.Name = "_headerPanel";
            this._headerPanel.Size = new System.Drawing.Size(1467, 143);
            this._headerPanel.TabIndex = 0;
            // 
            // _headerIconPanel
            // 
            this._headerIconPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(240)))), ((int)(((byte)(254)))));
            this._headerIconPanel.Controls.Add(this._headerIcon);
            this._headerIconPanel.Location = new System.Drawing.Point(36, 30);
            this._headerIconPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._headerIconPanel.Name = "_headerIconPanel";
            this._headerIconPanel.Size = new System.Drawing.Size(84, 84);
            this._headerIconPanel.TabIndex = 0;
            // 
            // _headerIcon
            // 
            this._headerIcon.BackColor = System.Drawing.Color.Transparent;
            this._headerIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._headerIcon.IconChar = FontAwesome.Sharp.IconChar.FileText;
            this._headerIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._headerIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._headerIcon.IconSize = 42;
            this._headerIcon.Location = new System.Drawing.Point(21, 21);
            this._headerIcon.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._headerIcon.Name = "_headerIcon";
            this._headerIcon.Size = new System.Drawing.Size(42, 42);
            this._headerIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._headerIcon.TabIndex = 0;
            this._headerIcon.TabStop = false;
            // 
            // _titleLabel
            // 
            this._titleLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this._titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._titleLabel.Location = new System.Drawing.Point(144, 27);
            this._titleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(900, 51);
            this._titleLabel.TabIndex = 1;
            this._titleLabel.Text = "Lập phiếu nhập kho";
            this._titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._titleLabel.UseMnemonic = false;
            // 
            // _subtitleLabel
            // 
            this._subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this._subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._subtitleLabel.Location = new System.Drawing.Point(144, 84);
            this._subtitleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._subtitleLabel.Name = "_subtitleLabel";
            this._subtitleLabel.Size = new System.Drawing.Size(900, 33);
            this._subtitleLabel.TabIndex = 2;
            this._subtitleLabel.Text = "Một phiếu · nhiều sản phẩm · mỗi dòng tự sinh 1 lô hàng (FEFO)";
            this._subtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._subtitleLabel.UseMnemonic = false;
            // 
            // _demoButton
            // 
            this._demoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._demoButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._demoButton.FlatAppearance.BorderSize = 0;
            this._demoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._demoButton.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this._demoButton.ForeColor = System.Drawing.Color.White;
            this._demoButton.Location = new System.Drawing.Point(1129, 24);
            this._demoButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._demoButton.Name = "_demoButton";
            this._demoButton.Size = new System.Drawing.Size(270, 57);
            this._demoButton.TabIndex = 3;
            this._demoButton.Text = "⚡ Điền dữ liệu Demo";
            this._demoButton.UseVisualStyleBackColor = false;
            this._demoButton.Click += new System.EventHandler(this.DemoButton_Click);
            // 
            // _headerDivider
            // 
            this._headerDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._headerDivider.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._headerDivider.Location = new System.Drawing.Point(0, 139);
            this._headerDivider.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._headerDivider.Name = "_headerDivider";
            this._headerDivider.Size = new System.Drawing.Size(1465, 2);
            this._headerDivider.TabIndex = 4;
            // 
            // _bodyPanel
            // 
            this._bodyPanel.AutoScroll = true;
            this._bodyPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this._bodyPanel.Controls.Add(this._supplierCard);
            this._bodyPanel.Controls.Add(this._entryCard);
            this._bodyPanel.Controls.Add(this._linesCard);
            this._bodyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._bodyPanel.Location = new System.Drawing.Point(0, 143);
            this._bodyPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._bodyPanel.Name = "_bodyPanel";
            this._bodyPanel.Size = new System.Drawing.Size(1467, 619);
            this._bodyPanel.TabIndex = 0;
            // 
            // _footerPanel
            // 
            this._footerPanel.BackColor = System.Drawing.Color.White;
            this._footerPanel.Controls.Add(this._totalCaptionLabel);
            this._footerPanel.Controls.Add(this._totalValueLabel);
            this._footerPanel.Controls.Add(this._saveButton);
            this._footerPanel.Controls.Add(this._cancelButton);
            this._footerPanel.Controls.Add(this._footerDivider);
            this._footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._footerPanel.Location = new System.Drawing.Point(0, 762);
            this._footerPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._footerPanel.Name = "_footerPanel";
            this._footerPanel.Size = new System.Drawing.Size(1467, 114);
            this._footerPanel.TabIndex = 1;
            // 
            // _totalCaptionLabel
            // 
            this._totalCaptionLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._totalCaptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._totalCaptionLabel.Location = new System.Drawing.Point(36, 18);
            this._totalCaptionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._totalCaptionLabel.Name = "_totalCaptionLabel";
            this._totalCaptionLabel.Size = new System.Drawing.Size(300, 27);
            this._totalCaptionLabel.TabIndex = 0;
            this._totalCaptionLabel.Text = "TỔNG TIỀN";
            this._totalCaptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._totalCaptionLabel.UseMnemonic = false;
            // 
            // _totalValueLabel
            // 
            this._totalValueLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this._totalValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._totalValueLabel.Location = new System.Drawing.Point(36, 48);
            this._totalValueLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._totalValueLabel.Name = "_totalValueLabel";
            this._totalValueLabel.Size = new System.Drawing.Size(480, 51);
            this._totalValueLabel.TabIndex = 1;
            this._totalValueLabel.Text = "0 đ";
            this._totalValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._totalValueLabel.UseMnemonic = false;
            // 
            // _saveButton
            // 
            this._saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._saveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._saveButton.FlatAppearance.BorderSize = 0;
            this._saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._saveButton.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this._saveButton.ForeColor = System.Drawing.Color.White;
            this._saveButton.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this._saveButton.IconColor = System.Drawing.Color.White;
            this._saveButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._saveButton.IconSize = 18;
            this._saveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._saveButton.Location = new System.Drawing.Point(1116, 22);
            this._saveButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(315, 69);
            this._saveButton.TabIndex = 2;
            this._saveButton.Text = "Lưu phiếu nhập";
            this._saveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._saveButton.UseVisualStyleBackColor = false;
            this._saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // _cancelButton
            // 
            this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cancelButton.BackColor = System.Drawing.Color.White;
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this._cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._cancelButton.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this._cancelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this._cancelButton.IconChar = FontAwesome.Sharp.IconChar.Remove;
            this._cancelButton.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this._cancelButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._cancelButton.IconSize = 18;
            this._cancelButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cancelButton.Location = new System.Drawing.Point(948, 22);
            this._cancelButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(150, 69);
            this._cancelButton.TabIndex = 3;
            this._cancelButton.Text = "Hủy";
            this._cancelButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._cancelButton.UseVisualStyleBackColor = false;
            this._cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // _footerDivider
            // 
            this._footerDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._footerDivider.Dock = System.Windows.Forms.DockStyle.Top;
            this._footerDivider.Location = new System.Drawing.Point(0, 0);
            this._footerDivider.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._footerDivider.Name = "_footerDivider";
            this._footerDivider.Size = new System.Drawing.Size(1467, 2);
            this._footerDivider.TabIndex = 4;
            // 
            // _supplierCard
            // 
            this._supplierCard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._supplierCard.BackColor = System.Drawing.Color.White;
            this._supplierCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._supplierCard.Controls.Add(this._supplierTitle);
            this._supplierCard.Controls.Add(this._supplierRequired);
            this._supplierCard.Controls.Add(this._supplierComboBox);
            this._supplierCard.FillColor = System.Drawing.Color.White;
            this._supplierCard.Location = new System.Drawing.Point(36, 99);
            this._supplierCard.Margin = new System.Windows.Forms.Padding(4);
            this._supplierCard.Name = "_supplierCard";
            this._supplierCard.Size = new System.Drawing.Size(2621, 113);
            this._supplierCard.TabIndex = 0;
            // 
            // _supplierTitle
            // 
            this._supplierTitle.AutoSize = true;
            this._supplierTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this._supplierTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._supplierTitle.Location = new System.Drawing.Point(30, 40);
            this._supplierTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._supplierTitle.Name = "_supplierTitle";
            this._supplierTitle.Size = new System.Drawing.Size(149, 30);
            this._supplierTitle.TabIndex = 0;
            this._supplierTitle.Text = "Nhà cung cấp";
            this._supplierTitle.UseMnemonic = false;
            // 
            // _supplierRequired
            // 
            this._supplierRequired.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._supplierRequired.AutoSize = true;
            this._supplierRequired.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this._supplierRequired.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._supplierRequired.Location = new System.Drawing.Point(2472, 44);
            this._supplierRequired.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._supplierRequired.Name = "_supplierRequired";
            this._supplierRequired.Size = new System.Drawing.Size(90, 23);
            this._supplierRequired.TabIndex = 1;
            this._supplierRequired.Text = "* bắt buộc";
            this._supplierRequired.UseMnemonic = false;
            // 
            // _supplierComboBox
            // 
            this._supplierComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._supplierComboBox.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this._supplierComboBox.FormattingEnabled = true;
            this._supplierComboBox.Location = new System.Drawing.Point(225, 30);
            this._supplierComboBox.Margin = new System.Windows.Forms.Padding(4);
            this._supplierComboBox.Name = "_supplierComboBox";
            this._supplierComboBox.Size = new System.Drawing.Size(778, 38);
            this._supplierComboBox.TabIndex = 2;
            // 
            // _entryCard
            // 
            this._entryCard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._entryCard.BackColor = System.Drawing.Color.White;
            this._entryCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._entryCard.Controls.Add(this._entryIcon);
            this._entryCard.Controls.Add(this._entryTitle);
            this._entryCard.Controls.Add(this._productLabel);
            this._entryCard.Controls.Add(this._quantityLabel);
            this._entryCard.Controls.Add(this._priceLabel);
            this._entryCard.Controls.Add(this._productComboBox);
            this._entryCard.Controls.Add(this._quantityTextBox);
            this._entryCard.Controls.Add(this._priceTextBox);
            this._entryCard.Controls.Add(this._lotNumberLabel);
            this._entryCard.Controls.Add(this._manufactureDateLabel);
            this._entryCard.Controls.Add(this._expiryDateLabel);
            this._entryCard.Controls.Add(this._lotNumberTextBox);
            this._entryCard.Controls.Add(this._manufactureDateBox);
            this._entryCard.Controls.Add(this._expiryDateBox);
            this._entryCard.Controls.Add(this._addLineButton);
            this._entryCard.FillColor = System.Drawing.Color.White;
            this._entryCard.Location = new System.Drawing.Point(36, 237);
            this._entryCard.Margin = new System.Windows.Forms.Padding(4);
            this._entryCard.Name = "_entryCard";
            this._entryCard.Size = new System.Drawing.Size(2621, 329);
            this._entryCard.TabIndex = 1;
            // 
            // _entryIcon
            // 
            this._entryIcon.BackColor = System.Drawing.Color.Transparent;
            this._entryIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._entryIcon.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            this._entryIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._entryIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._entryIcon.IconSize = 27;
            this._entryIcon.Location = new System.Drawing.Point(30, 27);
            this._entryIcon.Margin = new System.Windows.Forms.Padding(4);
            this._entryIcon.Name = "_entryIcon";
            this._entryIcon.Size = new System.Drawing.Size(27, 27);
            this._entryIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._entryIcon.TabIndex = 0;
            this._entryIcon.TabStop = false;
            // 
            // _entryTitle
            // 
            this._entryTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._entryTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._entryTitle.Location = new System.Drawing.Point(69, 24);
            this._entryTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._entryTitle.Name = "_entryTitle";
            this._entryTitle.Size = new System.Drawing.Size(600, 36);
            this._entryTitle.TabIndex = 1;
            this._entryTitle.Text = "Thêm sản phẩm vào phiếu";
            this._entryTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._entryTitle.UseMnemonic = false;
            // 
            // _productLabel
            // 
            this._productLabel.AutoSize = true;
            this._productLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._productLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._productLabel.Location = new System.Drawing.Point(30, 84);
            this._productLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._productLabel.Name = "_productLabel";
            this._productLabel.Size = new System.Drawing.Size(105, 25);
            this._productLabel.TabIndex = 2;
            this._productLabel.Text = "Sản phẩm *";
            this._productLabel.UseMnemonic = false;
            // 
            // _quantityLabel
            // 
            this._quantityLabel.AutoSize = true;
            this._quantityLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._quantityLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._quantityLabel.Location = new System.Drawing.Point(699, 84);
            this._quantityLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._quantityLabel.Name = "_quantityLabel";
            this._quantityLabel.Size = new System.Drawing.Size(98, 25);
            this._quantityLabel.TabIndex = 3;
            this._quantityLabel.Text = "Số lượng *";
            this._quantityLabel.UseMnemonic = false;
            // 
            // _priceLabel
            // 
            this._priceLabel.AutoSize = true;
            this._priceLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._priceLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._priceLabel.Location = new System.Drawing.Point(1068, 84);
            this._priceLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._priceLabel.Name = "_priceLabel";
            this._priceLabel.Size = new System.Drawing.Size(95, 25);
            this._priceLabel.TabIndex = 4;
            this._priceLabel.Text = "Giá nhập *";
            this._priceLabel.UseMnemonic = false;
            // 
            // _productComboBox
            // 
            this._productComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this._productComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this._productComboBox.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this._productComboBox.FormattingEnabled = true;
            this._productComboBox.Location = new System.Drawing.Point(30, 117);
            this._productComboBox.Margin = new System.Windows.Forms.Padding(4);
            this._productComboBox.Name = "_productComboBox";
            this._productComboBox.Size = new System.Drawing.Size(643, 38);
            this._productComboBox.TabIndex = 5;
            // 
            // _quantityTextBox
            // 
            this._quantityTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._quantityTextBox.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this._quantityTextBox.Location = new System.Drawing.Point(699, 117);
            this._quantityTextBox.Margin = new System.Windows.Forms.Padding(4);
            this._quantityTextBox.Name = "_quantityTextBox";
            this._quantityTextBox.Size = new System.Drawing.Size(344, 35);
            this._quantityTextBox.TabIndex = 6;
            // 
            // _priceTextBox
            // 
            this._priceTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._priceTextBox.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this._priceTextBox.Location = new System.Drawing.Point(1068, 117);
            this._priceTextBox.Margin = new System.Windows.Forms.Padding(4);
            this._priceTextBox.Name = "_priceTextBox";
            this._priceTextBox.Size = new System.Drawing.Size(356, 35);
            this._priceTextBox.TabIndex = 7;
            // 
            // _lotNumberLabel
            // 
            this._lotNumberLabel.AutoSize = true;
            this._lotNumberLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._lotNumberLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._lotNumberLabel.Location = new System.Drawing.Point(30, 201);
            this._lotNumberLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lotNumberLabel.Name = "_lotNumberLabel";
            this._lotNumberLabel.Size = new System.Drawing.Size(106, 25);
            this._lotNumberLabel.TabIndex = 8;
            this._lotNumberLabel.Text = "Số lô NCC *";
            this._lotNumberLabel.UseMnemonic = false;
            // 
            // _manufactureDateLabel
            // 
            this._manufactureDateLabel.AutoSize = true;
            this._manufactureDateLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._manufactureDateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._manufactureDateLabel.Location = new System.Drawing.Point(354, 201);
            this._manufactureDateLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._manufactureDateLabel.Name = "_manufactureDateLabel";
            this._manufactureDateLabel.Size = new System.Drawing.Size(46, 25);
            this._manufactureDateLabel.TabIndex = 9;
            this._manufactureDateLabel.Text = "NSX";
            this._manufactureDateLabel.UseMnemonic = false;
            // 
            // _expiryDateLabel
            // 
            this._expiryDateLabel.AutoSize = true;
            this._expiryDateLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._expiryDateLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._expiryDateLabel.Location = new System.Drawing.Point(699, 201);
            this._expiryDateLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._expiryDateLabel.Name = "_expiryDateLabel";
            this._expiryDateLabel.Size = new System.Drawing.Size(48, 25);
            this._expiryDateLabel.TabIndex = 10;
            this._expiryDateLabel.Text = "HSD";
            this._expiryDateLabel.UseMnemonic = false;
            // 
            // _lotNumberTextBox
            // 
            this._lotNumberTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._lotNumberTextBox.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this._lotNumberTextBox.Location = new System.Drawing.Point(30, 234);
            this._lotNumberTextBox.Margin = new System.Windows.Forms.Padding(4);
            this._lotNumberTextBox.Name = "_lotNumberTextBox";
            this._lotNumberTextBox.Size = new System.Drawing.Size(299, 35);
            this._lotNumberTextBox.TabIndex = 11;
            // 
            // _manufactureDateBox
            // 
            this._manufactureDateBox.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this._manufactureDateBox.Location = new System.Drawing.Point(354, 234);
            this._manufactureDateBox.Margin = new System.Windows.Forms.Padding(4);
            this._manufactureDateBox.MinimumSize = new System.Drawing.Size(150, 54);
            this._manufactureDateBox.Name = "_manufactureDateBox";
            this._manufactureDateBox.Size = new System.Drawing.Size(321, 54);
            this._manufactureDateBox.TabIndex = 12;
            this._manufactureDateBox.Value = null;
            // 
            // _expiryDateBox
            // 
            this._expiryDateBox.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this._expiryDateBox.Location = new System.Drawing.Point(699, 234);
            this._expiryDateBox.Margin = new System.Windows.Forms.Padding(4);
            this._expiryDateBox.MinimumSize = new System.Drawing.Size(150, 54);
            this._expiryDateBox.Name = "_expiryDateBox";
            this._expiryDateBox.Size = new System.Drawing.Size(345, 54);
            this._expiryDateBox.TabIndex = 13;
            this._expiryDateBox.Value = null;
            // 
            // _addLineButton
            // 
            this._addLineButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._addLineButton.FlatAppearance.BorderSize = 0;
            this._addLineButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._addLineButton.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this._addLineButton.ForeColor = System.Drawing.Color.White;
            this._addLineButton.Location = new System.Drawing.Point(1068, 231);
            this._addLineButton.Margin = new System.Windows.Forms.Padding(4);
            this._addLineButton.Name = "_addLineButton";
            this._addLineButton.Size = new System.Drawing.Size(357, 60);
            this._addLineButton.TabIndex = 14;
            this._addLineButton.Text = "+ Thêm dòng";
            this._addLineButton.UseVisualStyleBackColor = false;
            this._addLineButton.Click += new System.EventHandler(this.AddLineButton_Click);
            // 
            // _linesCard
            // 
            this._linesCard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._linesCard.BackColor = System.Drawing.Color.White;
            this._linesCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._linesCard.Controls.Add(this._linesIcon);
            this._linesCard.Controls.Add(this._linesTitle);
            this._linesCard.Controls.Add(this._lineCountLabel);
            this._linesCard.Controls.Add(this._linesGrid);
            this._linesCard.Controls.Add(this._emptyLabel);
            this._linesCard.FillColor = System.Drawing.Color.White;
            this._linesCard.Location = new System.Drawing.Point(36, 591);
            this._linesCard.Margin = new System.Windows.Forms.Padding(4);
            this._linesCard.Name = "_linesCard";
            this._linesCard.Size = new System.Drawing.Size(2621, 479);
            this._linesCard.TabIndex = 2;
            // 
            // _linesIcon
            // 
            this._linesIcon.BackColor = System.Drawing.Color.Transparent;
            this._linesIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._linesIcon.IconChar = FontAwesome.Sharp.IconChar.ListUl;
            this._linesIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._linesIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._linesIcon.IconSize = 27;
            this._linesIcon.Location = new System.Drawing.Point(30, 27);
            this._linesIcon.Margin = new System.Windows.Forms.Padding(4);
            this._linesIcon.Name = "_linesIcon";
            this._linesIcon.Size = new System.Drawing.Size(27, 27);
            this._linesIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._linesIcon.TabIndex = 0;
            this._linesIcon.TabStop = false;
            // 
            // _linesTitle
            // 
            this._linesTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._linesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._linesTitle.Location = new System.Drawing.Point(69, 24);
            this._linesTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._linesTitle.Name = "_linesTitle";
            this._linesTitle.Size = new System.Drawing.Size(900, 36);
            this._linesTitle.TabIndex = 1;
            this._linesTitle.Text = "Danh sách sản phẩm trên phiếu";
            this._linesTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._linesTitle.UseMnemonic = false;
            // 
            // _lineCountLabel
            // 
            this._lineCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._lineCountLabel.AutoSize = true;
            this._lineCountLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._lineCountLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._lineCountLabel.Location = new System.Drawing.Point(2487, 27);
            this._lineCountLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lineCountLabel.Name = "_lineCountLabel";
            this._lineCountLabel.Size = new System.Drawing.Size(70, 25);
            this._lineCountLabel.TabIndex = 2;
            this._lineCountLabel.Text = "0 dòng";
            this._lineCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._lineCountLabel.UseMnemonic = false;
            // 
            // _linesGrid
            // 
            this._linesGrid.AllowUserToAddRows = false;
            this._linesGrid.AllowUserToDeleteRows = false;
            this._linesGrid.AllowUserToResizeRows = false;
            this._linesGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._linesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._linesGrid.BackgroundColor = System.Drawing.Color.White;
            this._linesGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._linesGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this._linesGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._linesGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this._linesGrid.ColumnHeadersHeight = 40;
            this._linesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this._linesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Product,
            this.Quantity,
            this.ImportPrice,
            this.LotNumber,
            this.ManufactureDate,
            this.ExpiryDate,
            this.Amount,
            this.RemoveLine});
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle20.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._linesGrid.DefaultCellStyle = dataGridViewCellStyle20;
            this._linesGrid.EnableHeadersVisualStyles = false;
            this._linesGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._linesGrid.Location = new System.Drawing.Point(30, 78);
            this._linesGrid.Margin = new System.Windows.Forms.Padding(4);
            this._linesGrid.MultiSelect = false;
            this._linesGrid.Name = "_linesGrid";
            this._linesGrid.ReadOnly = true;
            this._linesGrid.RowHeadersVisible = false;
            this._linesGrid.RowHeadersWidth = 62;
            this._linesGrid.RowTemplate.Height = 40;
            this._linesGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._linesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._linesGrid.Size = new System.Drawing.Size(2562, 375);
            this._linesGrid.TabIndex = 3;
            // 
            // Product
            // 
            this.Product.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Product.DefaultCellStyle = dataGridViewCellStyle12;
            this.Product.FillWeight = 28F;
            this.Product.HeaderText = "Sản phẩm";
            this.Product.MinimumWidth = 160;
            this.Product.Name = "Product";
            this.Product.ReadOnly = true;
            this.Product.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Quantity
            // 
            this.Quantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.Quantity.DefaultCellStyle = dataGridViewCellStyle13;
            this.Quantity.FillWeight = 8F;
            this.Quantity.HeaderText = "SL";
            this.Quantity.MinimumWidth = 48;
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            this.Quantity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ImportPrice
            // 
            this.ImportPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle14.Format = "N0";
            dataGridViewCellStyle14.FormatProvider = new System.Globalization.CultureInfo("vi-VN");
            dataGridViewCellStyle14.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.ImportPrice.DefaultCellStyle = dataGridViewCellStyle14;
            this.ImportPrice.FillWeight = 12F;
            this.ImportPrice.HeaderText = "Giá nhập";
            this.ImportPrice.MinimumWidth = 80;
            this.ImportPrice.Name = "ImportPrice";
            this.ImportPrice.ReadOnly = true;
            this.ImportPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // LotNumber
            // 
            this.LotNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.LotNumber.DefaultCellStyle = dataGridViewCellStyle15;
            this.LotNumber.FillWeight = 14F;
            this.LotNumber.HeaderText = "Số lô NCC";
            this.LotNumber.MinimumWidth = 90;
            this.LotNumber.Name = "LotNumber";
            this.LotNumber.ReadOnly = true;
            this.LotNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ManufactureDate
            // 
            this.ManufactureDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle16.Format = "dd/MM/yyyy";
            this.ManufactureDate.DefaultCellStyle = dataGridViewCellStyle16;
            this.ManufactureDate.FillWeight = 11F;
            this.ManufactureDate.HeaderText = "NSX";
            this.ManufactureDate.MinimumWidth = 75;
            this.ManufactureDate.Name = "ManufactureDate";
            this.ManufactureDate.ReadOnly = true;
            this.ManufactureDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ExpiryDate
            // 
            this.ExpiryDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle17.Format = "dd/MM/yyyy";
            this.ExpiryDate.DefaultCellStyle = dataGridViewCellStyle17;
            this.ExpiryDate.FillWeight = 11F;
            this.ExpiryDate.HeaderText = "HSD";
            this.ExpiryDate.MinimumWidth = 75;
            this.ExpiryDate.Name = "ExpiryDate";
            this.ExpiryDate.ReadOnly = true;
            this.ExpiryDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Amount
            // 
            this.Amount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle18.Format = "N0";
            dataGridViewCellStyle18.FormatProvider = new System.Globalization.CultureInfo("vi-VN");
            dataGridViewCellStyle18.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.Amount.DefaultCellStyle = dataGridViewCellStyle18;
            this.Amount.FillWeight = 13F;
            this.Amount.HeaderText = "Thành tiền";
            this.Amount.MinimumWidth = 100;
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // RemoveLine
            // 
            this.RemoveLine.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.RemoveLine.DefaultCellStyle = dataGridViewCellStyle19;
            this.RemoveLine.HeaderText = "";
            this.RemoveLine.MinimumWidth = 8;
            this.RemoveLine.Name = "RemoveLine";
            this.RemoveLine.ReadOnly = true;
            this.RemoveLine.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RemoveLine.Width = 48;
            // 
            // _emptyLabel
            // 
            this._emptyLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._emptyLabel.BackColor = System.Drawing.Color.White;
            this._emptyLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this._emptyLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._emptyLabel.Location = new System.Drawing.Point(30, 138);
            this._emptyLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._emptyLabel.Name = "_emptyLabel";
            this._emptyLabel.Size = new System.Drawing.Size(2562, 315);
            this._emptyLabel.TabIndex = 4;
            this._emptyLabel.Text = "Chưa có sản phẩm — dùng form phía trên để thêm dòng";
            this._emptyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._emptyLabel.UseMnemonic = false;
            // 
            // frmPurchaseReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(1467, 876);
            this.Controls.Add(this._bodyPanel);
            this.Controls.Add(this._footerPanel);
            this.Controls.Add(this._headerPanel);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1489, 932);
            this.Name = "frmPurchaseReceipt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lập phiếu nhập kho";
            this._headerPanel.ResumeLayout(false);
            this._headerIconPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).EndInit();
            this._bodyPanel.ResumeLayout(false);
            this._footerPanel.ResumeLayout(false);
            this._supplierCard.ResumeLayout(false);
            this._supplierCard.PerformLayout();
            this._entryCard.ResumeLayout(false);
            this._entryCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._entryIcon)).EndInit();
            this._linesCard.ResumeLayout(false);
            this._linesCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._linesIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._linesGrid)).EndInit();
            this.ResumeLayout(false);

        }

        private DataGridViewTextBoxColumn Product;
        private DataGridViewTextBoxColumn Quantity;
        private DataGridViewTextBoxColumn ImportPrice;
        private DataGridViewTextBoxColumn LotNumber;
        private DataGridViewTextBoxColumn ManufactureDate;
        private DataGridViewTextBoxColumn ExpiryDate;
        private DataGridViewTextBoxColumn Amount;
        private DataGridViewTextBoxColumn RemoveLine;
    }
}
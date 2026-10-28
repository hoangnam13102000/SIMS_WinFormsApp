using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    partial class frmPurchaseReceiptDetail
    {
        private IContainer components = null;
        private Panel _rootPanel;
        private Panel _headerPanel;
        private Panel _headerDivider;
        private Panel _bodyPanel;
        private Panel _footerPanel;
        private IconButton _closeButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this._rootPanel = new System.Windows.Forms.Panel();
            this._bodyPanel = new System.Windows.Forms.Panel();
            this._contentLayout = new System.Windows.Forms.TableLayoutPanel();
            this._informationPanel = new SIMS_WinFormsApp.Views.Warehouse.frmPurchaseReceiptDetail.RoundedInformationPanel();
            this._informationLayout = new System.Windows.Forms.TableLayoutPanel();
            this._supplierField = new System.Windows.Forms.FlowLayoutPanel();
            this._supplierCaptionLabel = new System.Windows.Forms.Label();
            this._supplierValueLabel = new System.Windows.Forms.Label();
            this._createdByField = new System.Windows.Forms.FlowLayoutPanel();
            this._createdByCaptionLabel = new System.Windows.Forms.Label();
            this._createdByValueLabel = new System.Windows.Forms.Label();
            this._createdAtField = new System.Windows.Forms.FlowLayoutPanel();
            this._createdAtCaptionLabel = new System.Windows.Forms.Label();
            this._createdAtValueLabel = new System.Windows.Forms.Label();
            this._itemCountField = new System.Windows.Forms.FlowLayoutPanel();
            this._itemCountCaptionLabel = new System.Windows.Forms.Label();
            this._itemCountValueLabel = new System.Windows.Forms.Label();
            this._receiptStatusField = new System.Windows.Forms.FlowLayoutPanel();
            this._receiptStatusCaptionLabel = new System.Windows.Forms.Label();
            this._receiptStatusValueLabel = new System.Windows.Forms.Label();
            this._receiptTotalField = new System.Windows.Forms.FlowLayoutPanel();
            this._receiptTotalCaptionLabel = new System.Windows.Forms.Label();
            this._receiptTotalValueLabel = new System.Windows.Forms.Label();
            this._productCountLabel = new System.Windows.Forms.Label();
            this._grid = new System.Windows.Forms.DataGridView();
            this.ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LotNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ManufactureDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpiryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImportPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._expiryAlertPanel = new System.Windows.Forms.Panel();
            this._expiryAlertLabel = new System.Windows.Forms.Label();
            this._expiryAlertIcon = new FontAwesome.Sharp.IconPictureBox();
            this._totalPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._totalValueLabel = new System.Windows.Forms.Label();
            this._totalCaptionLabel = new System.Windows.Forms.Label();
            this._footerPanel = new System.Windows.Forms.Panel();
            this._closeButton = new FontAwesome.Sharp.IconButton();
            this._headerPanel = new System.Windows.Forms.Panel();
            this._headerIconCircle = new System.Windows.Forms.Panel();
            this._headerReceiptIcon = new FontAwesome.Sharp.IconPictureBox();
            this._receiptCodeLabel = new System.Windows.Forms.Label();
            this._receiptSubtitleLabel = new System.Windows.Forms.Label();
            this._statusLabel = new SIMS_WinFormsApp.Views.Warehouse.frmPurchaseReceiptDetail.RoundedBadgeLabel();
            this._headerDivider = new System.Windows.Forms.Panel();
            this._rootPanel.SuspendLayout();
            this._bodyPanel.SuspendLayout();
            this._contentLayout.SuspendLayout();
            this._informationPanel.SuspendLayout();
            this._informationLayout.SuspendLayout();
            this._supplierField.SuspendLayout();
            this._createdByField.SuspendLayout();
            this._createdAtField.SuspendLayout();
            this._itemCountField.SuspendLayout();
            this._receiptStatusField.SuspendLayout();
            this._receiptTotalField.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this._expiryAlertPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._expiryAlertIcon)).BeginInit();
            this._totalPanel.SuspendLayout();
            this._footerPanel.SuspendLayout();
            this._headerPanel.SuspendLayout();
            this._headerIconCircle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._headerReceiptIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // _rootPanel
            // 
            this._rootPanel.Controls.Add(this._bodyPanel);
            this._rootPanel.Controls.Add(this._footerPanel);
            this._rootPanel.Controls.Add(this._headerPanel);
            this._rootPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rootPanel.Location = new System.Drawing.Point(0, 0);
            this._rootPanel.Name = "_rootPanel";
            this._rootPanel.Size = new System.Drawing.Size(960, 620);
            this._rootPanel.TabIndex = 0;
            // 
            // _bodyPanel
            // 
            this._bodyPanel.BackColor = System.Drawing.Color.White;
            this._bodyPanel.Controls.Add(this._contentLayout);
            this._bodyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._bodyPanel.Location = new System.Drawing.Point(0, 84);
            this._bodyPanel.Name = "_bodyPanel";
            this._bodyPanel.Padding = new System.Windows.Forms.Padding(24, 12, 24, 8);
            this._bodyPanel.Size = new System.Drawing.Size(960, 476);
            this._bodyPanel.TabIndex = 0;
            // 
            // _contentLayout
            // 
            this._contentLayout.ColumnCount = 1;
            this._contentLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._contentLayout.Controls.Add(this._informationPanel, 0, 0);
            this._contentLayout.Controls.Add(this._productCountLabel, 0, 1);
            this._contentLayout.Controls.Add(this._grid, 0, 2);
            this._contentLayout.Controls.Add(this._expiryAlertPanel, 0, 3);
            this._contentLayout.Controls.Add(this._totalPanel, 0, 4);
            this._contentLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._contentLayout.Location = new System.Drawing.Point(24, 12);
            this._contentLayout.Margin = new System.Windows.Forms.Padding(0);
            this._contentLayout.Name = "_contentLayout";
            this._contentLayout.RowCount = 5;
            this._contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this._contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this._contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this._contentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this._contentLayout.Size = new System.Drawing.Size(952, 588);
            this._contentLayout.TabIndex = 0;
            // 
            // _informationPanel
            // 
            this._informationPanel.BackColor = System.Drawing.Color.Transparent;
            this._informationPanel.Controls.Add(this._informationLayout);
            this._informationPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._informationPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this._informationPanel.Location = new System.Drawing.Point(0, 0);
            this._informationPanel.Margin = new System.Windows.Forms.Padding(0);
            this._informationPanel.Name = "_informationPanel";
            this._informationPanel.Padding = new System.Windows.Forms.Padding(20);
            this._informationPanel.Size = new System.Drawing.Size(952, 220);
            this._informationPanel.TabIndex = 0;
            // 
            // _informationLayout
            // 
            this._informationLayout.BackColor = System.Drawing.Color.Transparent;
            this._informationLayout.ColumnCount = 2;
            this._informationLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._informationLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._informationLayout.Controls.Add(this._supplierField, 0, 0);
            this._informationLayout.Controls.Add(this._createdByField, 1, 0);
            this._informationLayout.Controls.Add(this._createdAtField, 0, 1);
            this._informationLayout.Controls.Add(this._itemCountField, 1, 1);
            this._informationLayout.Controls.Add(this._receiptStatusField, 0, 2);
            this._informationLayout.Controls.Add(this._receiptTotalField, 1, 2);
            this._informationLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._informationLayout.Location = new System.Drawing.Point(20, 20);
            this._informationLayout.Margin = new System.Windows.Forms.Padding(0);
            this._informationLayout.Name = "_informationLayout";
            this._informationLayout.RowCount = 3;
            this._informationLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this._informationLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this._informationLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this._informationLayout.Size = new System.Drawing.Size(912, 180);
            this._informationLayout.TabIndex = 0;
            // 
            // _supplierField
            // 
            this._supplierField.Controls.Add(this._supplierCaptionLabel);
            this._supplierField.Controls.Add(this._supplierValueLabel);
            this._supplierField.Dock = System.Windows.Forms.DockStyle.Fill;
            this._supplierField.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this._supplierField.Location = new System.Drawing.Point(0, 0);
            this._supplierField.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this._supplierField.Name = "_supplierField";
            this._supplierField.Size = new System.Drawing.Size(440, 60);
            this._supplierField.TabIndex = 0;
            this._supplierField.WrapContents = false;
            // 
            // _supplierCaptionLabel
            // 
            this._supplierCaptionLabel.AutoSize = true;
            this._supplierCaptionLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._supplierCaptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._supplierCaptionLabel.Location = new System.Drawing.Point(0, 0);
            this._supplierCaptionLabel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this._supplierCaptionLabel.Name = "_supplierCaptionLabel";
            this._supplierCaptionLabel.Size = new System.Drawing.Size(100, 20);
            this._supplierCaptionLabel.TabIndex = 0;
            this._supplierCaptionLabel.Text = "Nhà cung cấp";
            this._supplierCaptionLabel.UseMnemonic = false;
            // 
            // _supplierValueLabel
            // 
            this._supplierValueLabel.AutoEllipsis = true;
            this._supplierValueLabel.AutoSize = true;
            this._supplierValueLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._supplierValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._supplierValueLabel.Location = new System.Drawing.Point(0, 24);
            this._supplierValueLabel.Margin = new System.Windows.Forms.Padding(0);
            this._supplierValueLabel.Name = "_supplierValueLabel";
            this._supplierValueLabel.Size = new System.Drawing.Size(0, 28);
            this._supplierValueLabel.TabIndex = 1;
            this._supplierValueLabel.UseMnemonic = false;
            // 
            // _createdByField
            // 
            this._createdByField.Controls.Add(this._createdByCaptionLabel);
            this._createdByField.Controls.Add(this._createdByValueLabel);
            this._createdByField.Dock = System.Windows.Forms.DockStyle.Fill;
            this._createdByField.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this._createdByField.Location = new System.Drawing.Point(456, 0);
            this._createdByField.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this._createdByField.Name = "_createdByField";
            this._createdByField.Size = new System.Drawing.Size(440, 60);
            this._createdByField.TabIndex = 1;
            this._createdByField.WrapContents = false;
            // 
            // _createdByCaptionLabel
            // 
            this._createdByCaptionLabel.AutoSize = true;
            this._createdByCaptionLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._createdByCaptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._createdByCaptionLabel.Location = new System.Drawing.Point(0, 0);
            this._createdByCaptionLabel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this._createdByCaptionLabel.Name = "_createdByCaptionLabel";
            this._createdByCaptionLabel.Size = new System.Drawing.Size(118, 20);
            this._createdByCaptionLabel.TabIndex = 0;
            this._createdByCaptionLabel.Text = "Người tạo phiếu";
            this._createdByCaptionLabel.UseMnemonic = false;
            // 
            // _createdByValueLabel
            // 
            this._createdByValueLabel.AutoEllipsis = true;
            this._createdByValueLabel.AutoSize = true;
            this._createdByValueLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._createdByValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._createdByValueLabel.Location = new System.Drawing.Point(0, 24);
            this._createdByValueLabel.Margin = new System.Windows.Forms.Padding(0);
            this._createdByValueLabel.Name = "_createdByValueLabel";
            this._createdByValueLabel.Size = new System.Drawing.Size(0, 28);
            this._createdByValueLabel.TabIndex = 1;
            this._createdByValueLabel.UseMnemonic = false;
            // 
            // _createdAtField
            // 
            this._createdAtField.Controls.Add(this._createdAtCaptionLabel);
            this._createdAtField.Controls.Add(this._createdAtValueLabel);
            this._createdAtField.Dock = System.Windows.Forms.DockStyle.Fill;
            this._createdAtField.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this._createdAtField.Location = new System.Drawing.Point(0, 60);
            this._createdAtField.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this._createdAtField.Name = "_createdAtField";
            this._createdAtField.Size = new System.Drawing.Size(440, 60);
            this._createdAtField.TabIndex = 2;
            this._createdAtField.WrapContents = false;
            // 
            // _createdAtCaptionLabel
            // 
            this._createdAtCaptionLabel.AutoSize = true;
            this._createdAtCaptionLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._createdAtCaptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._createdAtCaptionLabel.Location = new System.Drawing.Point(0, 0);
            this._createdAtCaptionLabel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this._createdAtCaptionLabel.Name = "_createdAtCaptionLabel";
            this._createdAtCaptionLabel.Size = new System.Drawing.Size(70, 20);
            this._createdAtCaptionLabel.TabIndex = 0;
            this._createdAtCaptionLabel.Text = "Ngày tạo";
            this._createdAtCaptionLabel.UseMnemonic = false;
            // 
            // _createdAtValueLabel
            // 
            this._createdAtValueLabel.AutoEllipsis = true;
            this._createdAtValueLabel.AutoSize = true;
            this._createdAtValueLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._createdAtValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._createdAtValueLabel.Location = new System.Drawing.Point(0, 24);
            this._createdAtValueLabel.Margin = new System.Windows.Forms.Padding(0);
            this._createdAtValueLabel.Name = "_createdAtValueLabel";
            this._createdAtValueLabel.Size = new System.Drawing.Size(0, 28);
            this._createdAtValueLabel.TabIndex = 1;
            this._createdAtValueLabel.UseMnemonic = false;
            // 
            // _itemCountField
            // 
            this._itemCountField.Controls.Add(this._itemCountCaptionLabel);
            this._itemCountField.Controls.Add(this._itemCountValueLabel);
            this._itemCountField.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemCountField.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this._itemCountField.Location = new System.Drawing.Point(456, 60);
            this._itemCountField.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this._itemCountField.Name = "_itemCountField";
            this._itemCountField.Size = new System.Drawing.Size(440, 60);
            this._itemCountField.TabIndex = 3;
            this._itemCountField.WrapContents = false;
            // 
            // _itemCountCaptionLabel
            // 
            this._itemCountCaptionLabel.AutoSize = true;
            this._itemCountCaptionLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._itemCountCaptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._itemCountCaptionLabel.Location = new System.Drawing.Point(0, 0);
            this._itemCountCaptionLabel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this._itemCountCaptionLabel.Name = "_itemCountCaptionLabel";
            this._itemCountCaptionLabel.Size = new System.Drawing.Size(133, 20);
            this._itemCountCaptionLabel.TabIndex = 0;
            this._itemCountCaptionLabel.Text = "Số dòng sản phẩm";
            this._itemCountCaptionLabel.UseMnemonic = false;
            // 
            // _itemCountValueLabel
            // 
            this._itemCountValueLabel.AutoEllipsis = true;
            this._itemCountValueLabel.AutoSize = true;
            this._itemCountValueLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._itemCountValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._itemCountValueLabel.Location = new System.Drawing.Point(0, 24);
            this._itemCountValueLabel.Margin = new System.Windows.Forms.Padding(0);
            this._itemCountValueLabel.Name = "_itemCountValueLabel";
            this._itemCountValueLabel.Size = new System.Drawing.Size(0, 28);
            this._itemCountValueLabel.TabIndex = 1;
            this._itemCountValueLabel.UseMnemonic = false;
            // 
            // _receiptStatusField
            // 
            this._receiptStatusField.Controls.Add(this._receiptStatusCaptionLabel);
            this._receiptStatusField.Controls.Add(this._receiptStatusValueLabel);
            this._receiptStatusField.Dock = System.Windows.Forms.DockStyle.Fill;
            this._receiptStatusField.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this._receiptStatusField.Location = new System.Drawing.Point(0, 120);
            this._receiptStatusField.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this._receiptStatusField.Name = "_receiptStatusField";
            this._receiptStatusField.Size = new System.Drawing.Size(440, 60);
            this._receiptStatusField.TabIndex = 4;
            this._receiptStatusField.WrapContents = false;
            // 
            // _receiptStatusCaptionLabel
            // 
            this._receiptStatusCaptionLabel.AutoSize = true;
            this._receiptStatusCaptionLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._receiptStatusCaptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._receiptStatusCaptionLabel.Location = new System.Drawing.Point(0, 0);
            this._receiptStatusCaptionLabel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this._receiptStatusCaptionLabel.Name = "_receiptStatusCaptionLabel";
            this._receiptStatusCaptionLabel.Size = new System.Drawing.Size(75, 20);
            this._receiptStatusCaptionLabel.TabIndex = 0;
            this._receiptStatusCaptionLabel.Text = "Trạng thái";
            this._receiptStatusCaptionLabel.UseMnemonic = false;
            // 
            // _receiptStatusValueLabel
            // 
            this._receiptStatusValueLabel.AutoEllipsis = true;
            this._receiptStatusValueLabel.AutoSize = true;
            this._receiptStatusValueLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._receiptStatusValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._receiptStatusValueLabel.Location = new System.Drawing.Point(0, 24);
            this._receiptStatusValueLabel.Margin = new System.Windows.Forms.Padding(0);
            this._receiptStatusValueLabel.Name = "_receiptStatusValueLabel";
            this._receiptStatusValueLabel.Size = new System.Drawing.Size(0, 28);
            this._receiptStatusValueLabel.TabIndex = 1;
            this._receiptStatusValueLabel.UseMnemonic = false;
            // 
            // _receiptTotalField
            // 
            this._receiptTotalField.Controls.Add(this._receiptTotalCaptionLabel);
            this._receiptTotalField.Controls.Add(this._receiptTotalValueLabel);
            this._receiptTotalField.Dock = System.Windows.Forms.DockStyle.Fill;
            this._receiptTotalField.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this._receiptTotalField.Location = new System.Drawing.Point(456, 120);
            this._receiptTotalField.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this._receiptTotalField.Name = "_receiptTotalField";
            this._receiptTotalField.Size = new System.Drawing.Size(440, 60);
            this._receiptTotalField.TabIndex = 5;
            this._receiptTotalField.WrapContents = false;
            // 
            // _receiptTotalCaptionLabel
            // 
            this._receiptTotalCaptionLabel.AutoSize = true;
            this._receiptTotalCaptionLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._receiptTotalCaptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._receiptTotalCaptionLabel.Location = new System.Drawing.Point(0, 0);
            this._receiptTotalCaptionLabel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this._receiptTotalCaptionLabel.Name = "_receiptTotalCaptionLabel";
            this._receiptTotalCaptionLabel.Size = new System.Drawing.Size(113, 20);
            this._receiptTotalCaptionLabel.TabIndex = 0;
            this._receiptTotalCaptionLabel.Text = "Tổng tiền phiếu";
            this._receiptTotalCaptionLabel.UseMnemonic = false;
            // 
            // _receiptTotalValueLabel
            // 
            this._receiptTotalValueLabel.AutoEllipsis = true;
            this._receiptTotalValueLabel.AutoSize = true;
            this._receiptTotalValueLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._receiptTotalValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this._receiptTotalValueLabel.Location = new System.Drawing.Point(0, 24);
            this._receiptTotalValueLabel.Margin = new System.Windows.Forms.Padding(0);
            this._receiptTotalValueLabel.Name = "_receiptTotalValueLabel";
            this._receiptTotalValueLabel.Size = new System.Drawing.Size(0, 28);
            this._receiptTotalValueLabel.TabIndex = 1;
            this._receiptTotalValueLabel.UseMnemonic = false;
            // 
            // _productCountLabel
            // 
            this._productCountLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._productCountLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._productCountLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._productCountLabel.Location = new System.Drawing.Point(0, 232);
            this._productCountLabel.Margin = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this._productCountLabel.Name = "_productCountLabel";
            this._productCountLabel.Size = new System.Drawing.Size(952, 32);
            this._productCountLabel.TabIndex = 1;
            this._productCountLabel.Text = "Danh sách sản phẩm";
            this._productCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._productCountLabel.UseMnemonic = false;
            // 
            // _grid
            // 
            this._grid.AllowUserToAddRows = false;
            this._grid.AllowUserToDeleteRows = false;
            this._grid.AllowUserToResizeRows = false;
            this._grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._grid.BackgroundColor = System.Drawing.Color.White;
            this._grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._grid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this._grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this._grid.ColumnHeadersHeight = 40;
            this._grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this._grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductCode,
            this.ProductName,
            this.BatchCode,
            this.LotNumber,
            this.ManufactureDate,
            this.ExpiryDate,
            this.Quantity,
            this.ImportPrice,
            this.Amount});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            dataGridViewCellStyle11.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._grid.DefaultCellStyle = dataGridViewCellStyle11;
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.EnableHeadersVisualStyles = false;
            this._grid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this._grid.Location = new System.Drawing.Point(0, 264);
            this._grid.Margin = new System.Windows.Forms.Padding(0);
            this._grid.MultiSelect = false;
            this._grid.Name = "_grid";
            this._grid.ReadOnly = true;
            this._grid.RowHeadersVisible = false;
            this._grid.RowHeadersWidth = 51;
            this._grid.RowTemplate.Height = 40;
            this._grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._grid.Size = new System.Drawing.Size(952, 232);
            this._grid.TabIndex = 0;
            // 
            // ProductCode
            // 
            this.ProductCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ProductCode.DefaultCellStyle = dataGridViewCellStyle2;
            this.ProductCode.FillWeight = 9F;
            this.ProductCode.HeaderText = "Mã SP";
            this.ProductCode.MinimumWidth = 75;
            this.ProductCode.Name = "ProductCode";
            this.ProductCode.ReadOnly = true;
            // 
            // ProductName
            // 
            this.ProductName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ProductName.DefaultCellStyle = dataGridViewCellStyle3;
            this.ProductName.FillWeight = 20F;
            this.ProductName.HeaderText = "Sản phẩm";
            this.ProductName.MinimumWidth = 130;
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            // 
            // BatchCode
            // 
            this.BatchCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.BatchCode.DefaultCellStyle = dataGridViewCellStyle4;
            this.BatchCode.FillWeight = 12F;
            this.BatchCode.HeaderText = "Mã lô";
            this.BatchCode.MinimumWidth = 105;
            this.BatchCode.Name = "BatchCode";
            this.BatchCode.ReadOnly = true;
            // 
            // LotNumber
            // 
            this.LotNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.LotNumber.DefaultCellStyle = dataGridViewCellStyle5;
            this.LotNumber.FillWeight = 11F;
            this.LotNumber.HeaderText = "Số lô NCC";
            this.LotNumber.MinimumWidth = 90;
            this.LotNumber.Name = "LotNumber";
            this.LotNumber.ReadOnly = true;
            // 
            // ManufactureDate
            // 
            this.ManufactureDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ManufactureDate.DefaultCellStyle = dataGridViewCellStyle6;
            this.ManufactureDate.FillWeight = 11F;
            this.ManufactureDate.HeaderText = "NSX";
            this.ManufactureDate.MinimumWidth = 95;
            this.ManufactureDate.Name = "ManufactureDate";
            this.ManufactureDate.ReadOnly = true;
            // 
            // ExpiryDate
            // 
            this.ExpiryDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ExpiryDate.DefaultCellStyle = dataGridViewCellStyle7;
            this.ExpiryDate.FillWeight = 11F;
            this.ExpiryDate.HeaderText = "HSD";
            this.ExpiryDate.MinimumWidth = 95;
            this.ExpiryDate.Name = "ExpiryDate";
            this.ExpiryDate.ReadOnly = true;
            // 
            // Quantity
            // 
            this.Quantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Quantity.DefaultCellStyle = dataGridViewCellStyle8;
            this.Quantity.FillWeight = 6F;
            this.Quantity.HeaderText = "SL";
            this.Quantity.MinimumWidth = 45;
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            // 
            // ImportPrice
            // 
            this.ImportPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            dataGridViewCellStyle9.Format = "N0";
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ImportPrice.DefaultCellStyle = dataGridViewCellStyle9;
            this.ImportPrice.FillWeight = 10F;
            this.ImportPrice.HeaderText = "Giá nhập";
            this.ImportPrice.MinimumWidth = 95;
            this.ImportPrice.Name = "ImportPrice";
            this.ImportPrice.ReadOnly = true;
            // 
            // Amount
            // 
            this.Amount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle10.Format = "N0";
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle10;
            this.Amount.FillWeight = 11F;
            this.Amount.HeaderText = "Thành tiền";
            this.Amount.MinimumWidth = 105;
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            // 
            // _expiryAlertPanel
            // 
            this._expiryAlertPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(251)))), ((int)(((byte)(235)))));
            this._expiryAlertPanel.Controls.Add(this._expiryAlertLabel);
            this._expiryAlertPanel.Controls.Add(this._expiryAlertIcon);
            this._expiryAlertPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._expiryAlertPanel.Location = new System.Drawing.Point(0, 500);
            this._expiryAlertPanel.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this._expiryAlertPanel.Name = "_expiryAlertPanel";
            this._expiryAlertPanel.Size = new System.Drawing.Size(952, 32);
            this._expiryAlertPanel.TabIndex = 0;
            // 
            // _expiryAlertLabel
            // 
            this._expiryAlertLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._expiryAlertLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._expiryAlertLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(83)))), ((int)(((byte)(9)))));
            this._expiryAlertLabel.Location = new System.Drawing.Point(0, 0);
            this._expiryAlertLabel.Name = "_expiryAlertLabel";
            this._expiryAlertLabel.Padding = new System.Windows.Forms.Padding(40, 0, 8, 0);
            this._expiryAlertLabel.Size = new System.Drawing.Size(952, 32);
            this._expiryAlertLabel.TabIndex = 0;
            this._expiryAlertLabel.Text = "Phiếu có lô hàng sắp hết hạn hoặc đã hết hạn sử dụng.";
            this._expiryAlertLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._expiryAlertLabel.UseMnemonic = false;
            // 
            // _expiryAlertIcon
            // 
            this._expiryAlertIcon.BackColor = System.Drawing.Color.Transparent;
            this._expiryAlertIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this._expiryAlertIcon.IconChar = FontAwesome.Sharp.IconChar.ExclamationTriangle;
            this._expiryAlertIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this._expiryAlertIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._expiryAlertIcon.IconSize = 20;
            this._expiryAlertIcon.Location = new System.Drawing.Point(12, 10);
            this._expiryAlertIcon.Name = "_expiryAlertIcon";
            this._expiryAlertIcon.Size = new System.Drawing.Size(20, 20);
            this._expiryAlertIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._expiryAlertIcon.TabIndex = 1;
            this._expiryAlertIcon.TabStop = false;
            // 
            // _totalPanel
            // 
            this._totalPanel.AutoSize = true;
            this._totalPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._totalPanel.Controls.Add(this._totalValueLabel);
            this._totalPanel.Controls.Add(this._totalCaptionLabel);
            this._totalPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._totalPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._totalPanel.Location = new System.Drawing.Point(0, 538);
            this._totalPanel.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this._totalPanel.Name = "_totalPanel";
            this._totalPanel.Padding = new System.Windows.Forms.Padding(0, 8, 4, 0);
            this._totalPanel.Size = new System.Drawing.Size(952, 50);
            this._totalPanel.TabIndex = 2;
            this._totalPanel.WrapContents = false;
            // 
            // _totalValueLabel
            // 
            this._totalValueLabel.AutoSize = true;
            this._totalValueLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this._totalValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this._totalValueLabel.Location = new System.Drawing.Point(948, 8);
            this._totalValueLabel.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this._totalValueLabel.Name = "_totalValueLabel";
            this._totalValueLabel.Size = new System.Drawing.Size(0, 37);
            this._totalValueLabel.TabIndex = 0;
            // 
            // _totalCaptionLabel
            // 
            this._totalCaptionLabel.AutoSize = true;
            this._totalCaptionLabel.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this._totalCaptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._totalCaptionLabel.Location = new System.Drawing.Point(811, 13);
            this._totalCaptionLabel.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this._totalCaptionLabel.Name = "_totalCaptionLabel";
            this._totalCaptionLabel.Size = new System.Drawing.Size(129, 30);
            this._totalCaptionLabel.TabIndex = 1;
            this._totalCaptionLabel.Text = "Tổng cộng:";
            this._totalCaptionLabel.UseMnemonic = false;
            // 
            // _footerPanel
            // 
            this._footerPanel.BackColor = System.Drawing.Color.White;
            this._footerPanel.Controls.Add(this._closeButton);
            this._footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._footerPanel.Location = new System.Drawing.Point(0, 560);
            this._footerPanel.Name = "_footerPanel";
            this._footerPanel.Padding = new System.Windows.Forms.Padding(24, 10, 24, 10);
            this._footerPanel.Size = new System.Drawing.Size(960, 60);
            this._footerPanel.TabIndex = 1;
            // 
            // _closeButton
            // 
            this._closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._closeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this._closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._closeButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this._closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._closeButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._closeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._closeButton.IconChar = FontAwesome.Sharp.IconChar.RightFromBracket;
            this._closeButton.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._closeButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._closeButton.IconSize = 16;
            this._closeButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._closeButton.Location = new System.Drawing.Point(816, 10);
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(120, 40);
            this._closeButton.TabIndex = 0;
            this._closeButton.Text = "Thoát";
            this._closeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._closeButton.UseVisualStyleBackColor = false;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _headerPanel
            // 
            this._headerPanel.BackColor = System.Drawing.Color.White;
            this._headerPanel.Controls.Add(this._headerIconCircle);
            this._headerPanel.Controls.Add(this._receiptCodeLabel);
            this._headerPanel.Controls.Add(this._receiptSubtitleLabel);
            this._headerPanel.Controls.Add(this._statusLabel);
            this._headerPanel.Controls.Add(this._headerDivider);
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._headerPanel.Location = new System.Drawing.Point(0, 0);
            this._headerPanel.Name = "_headerPanel";
            this._headerPanel.Padding = new System.Windows.Forms.Padding(0);
            this._headerPanel.Size = new System.Drawing.Size(960, 84);
            this._headerPanel.TabIndex = 2;
            // 
            // _headerIconCircle
            // 
            this._headerIconCircle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this._headerIconCircle.AutoSize = false;
            this._headerIconCircle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this._headerIconCircle.Controls.Add(this._headerReceiptIcon);
            this._headerIconCircle.Location = new System.Drawing.Point(20, 18);
            this._headerIconCircle.Margin = new System.Windows.Forms.Padding(0);
            this._headerIconCircle.Name = "_headerIconCircle";
            this._headerIconCircle.Size = new System.Drawing.Size(44, 44);
            this._headerIconCircle.TabIndex = 0;
            // 
            // _headerReceiptIcon
            // 
            this._headerReceiptIcon.BackColor = System.Drawing.Color.Transparent;
            this._headerReceiptIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this._headerReceiptIcon.IconChar = FontAwesome.Sharp.IconChar.FileText;
            this._headerReceiptIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this._headerReceiptIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._headerReceiptIcon.IconSize = 22;
            this._headerReceiptIcon.Location = new System.Drawing.Point(11, 11);
            this._headerReceiptIcon.Name = "_headerReceiptIcon";
            this._headerReceiptIcon.Size = new System.Drawing.Size(22, 22);
            this._headerReceiptIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._headerReceiptIcon.TabIndex = 0;
            this._headerReceiptIcon.TabStop = false;
            // 
            // _receiptCodeLabel
            // 
            this._receiptCodeLabel.AutoEllipsis = true;
            this._receiptCodeLabel.AutoSize = false;
            this._receiptCodeLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this._receiptCodeLabel.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this._receiptCodeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._receiptCodeLabel.Location = new System.Drawing.Point(76, 12);
            this._receiptCodeLabel.Margin = new System.Windows.Forms.Padding(0);
            this._receiptCodeLabel.Name = "_receiptCodeLabel";
            this._receiptCodeLabel.Size = new System.Drawing.Size(560, 34);
            this._receiptCodeLabel.TabIndex = 0;
            this._receiptCodeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._receiptCodeLabel.UseMnemonic = false;
            // 
            // _receiptSubtitleLabel
            // 
            this._receiptSubtitleLabel.AutoEllipsis = true;
            this._receiptSubtitleLabel.AutoSize = false;
            this._receiptSubtitleLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this._receiptSubtitleLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this._receiptSubtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._receiptSubtitleLabel.Location = new System.Drawing.Point(76, 48);
            this._receiptSubtitleLabel.Margin = new System.Windows.Forms.Padding(0);
            this._receiptSubtitleLabel.Name = "_receiptSubtitleLabel";
            this._receiptSubtitleLabel.Size = new System.Drawing.Size(560, 22);
            this._receiptSubtitleLabel.TabIndex = 1;
            this._receiptSubtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._receiptSubtitleLabel.UseMnemonic = false;
            // 
            // _statusLabel
            // 
            this._statusLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this._statusLabel.AutoSize = false;
            this._statusLabel.BackColor = System.Drawing.Color.Transparent;
            this._statusLabel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this._statusLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this._statusLabel.Location = new System.Drawing.Point(844, 27);
            this._statusLabel.Margin = new System.Windows.Forms.Padding(0);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(96, 30);
            this._statusLabel.TabIndex = 2;
            this._statusLabel.Text = "Hoàn tất";
            this._statusLabel.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this._statusLabel.UseMnemonic = false;
            // 
            // _headerDivider
            // 
            this._headerDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._headerDivider.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._headerDivider.Location = new System.Drawing.Point(0, 83);
            this._headerDivider.Name = "_headerDivider";
            this._headerDivider.Size = new System.Drawing.Size(960, 1);
            this._headerDivider.TabIndex = 0;
            // 
            // frmPurchaseReceiptDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = false;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this._closeButton;
            this.ClientSize = new System.Drawing.Size(960, 620);
            this.Controls.Add(this._rootPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(900, 560);
            this.Name = "frmPurchaseReceiptDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chi tiết phiếu nhập kho";
            this._rootPanel.ResumeLayout(false);
            this._bodyPanel.ResumeLayout(false);
            this._contentLayout.ResumeLayout(false);
            this._contentLayout.PerformLayout();
            this._informationPanel.ResumeLayout(false);
            this._informationLayout.ResumeLayout(false);
            this._supplierField.ResumeLayout(false);
            this._supplierField.PerformLayout();
            this._createdByField.ResumeLayout(false);
            this._createdByField.PerformLayout();
            this._createdAtField.ResumeLayout(false);
            this._createdAtField.PerformLayout();
            this._itemCountField.ResumeLayout(false);
            this._itemCountField.PerformLayout();
            this._receiptStatusField.ResumeLayout(false);
            this._receiptStatusField.PerformLayout();
            this._receiptTotalField.ResumeLayout(false);
            this._receiptTotalField.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this._expiryAlertPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._expiryAlertIcon)).EndInit();
            this._totalPanel.ResumeLayout(false);
            this._totalPanel.PerformLayout();
            this._footerPanel.ResumeLayout(false);
            this._headerPanel.ResumeLayout(false);
            this._headerIconCircle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._headerReceiptIcon)).EndInit();
            this.ResumeLayout(false);

        }

        private TableLayoutPanel _contentLayout;
        private Panel _headerIconCircle;
        private IconPictureBox _headerReceiptIcon;
        private Label _receiptCodeLabel;
        private Label _receiptSubtitleLabel;
        private RoundedBadgeLabel _statusLabel;
        private RoundedInformationPanel _informationPanel;
        private TableLayoutPanel _informationLayout;
        private FlowLayoutPanel _supplierField;
        private Label _supplierCaptionLabel;
        private Label _supplierValueLabel;
        private FlowLayoutPanel _createdByField;
        private Label _createdByCaptionLabel;
        private Label _createdByValueLabel;
        private FlowLayoutPanel _createdAtField;
        private Label _createdAtCaptionLabel;
        private Label _createdAtValueLabel;
        private FlowLayoutPanel _itemCountField;
        private Label _itemCountCaptionLabel;
        private Label _itemCountValueLabel;
        private FlowLayoutPanel _receiptStatusField;
        private Label _receiptStatusCaptionLabel;
        private Label _receiptStatusValueLabel;
        private FlowLayoutPanel _receiptTotalField;
        private Label _receiptTotalCaptionLabel;
        private Label _receiptTotalValueLabel;
        private Label _productCountLabel;
        private DataGridView _grid;
        private Panel _expiryAlertPanel;
        private IconPictureBox _expiryAlertIcon;
        private Label _expiryAlertLabel;
        private FlowLayoutPanel _totalPanel;
        private Label _totalCaptionLabel;
        private Label _totalValueLabel;
        private DataGridViewTextBoxColumn _productCodeColumn;
        private DataGridViewTextBoxColumn _productNameColumn;
        private DataGridViewTextBoxColumn _batchCodeColumn;
        private DataGridViewTextBoxColumn _lotNumberColumn;
        private DataGridViewTextBoxColumn _manufactureDateColumn;
        private DataGridViewTextBoxColumn _expiryDateColumn;
        private DataGridViewTextBoxColumn _quantityColumn;
        private DataGridViewTextBoxColumn _importPriceColumn;
        private DataGridViewTextBoxColumn _amountColumn;
        private DataGridViewTextBoxColumn ProductCode;
        private DataGridViewTextBoxColumn ProductName;
        private DataGridViewTextBoxColumn BatchCode;
        private DataGridViewTextBoxColumn LotNumber;
        private DataGridViewTextBoxColumn ManufactureDate;
        private DataGridViewTextBoxColumn ExpiryDate;
        private DataGridViewTextBoxColumn Quantity;
        private DataGridViewTextBoxColumn ImportPrice;
        private DataGridViewTextBoxColumn Amount;
    }
}
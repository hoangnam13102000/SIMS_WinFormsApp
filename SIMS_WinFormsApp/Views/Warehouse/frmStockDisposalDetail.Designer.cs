using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    partial class frmStockDisposalDetail
    {
        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this._headerPanel = new System.Windows.Forms.Panel();
            this._headerIconPanel = new System.Windows.Forms.Panel();
            this._headerIcon = new FontAwesome.Sharp.IconPictureBox();
            this._disposalCodeLabel = new System.Windows.Forms.Label();
            this._subtitleLabel = new System.Windows.Forms.Label();
            this._statusLabel = new System.Windows.Forms.Label();
            this._headerLine = new System.Windows.Forms.Panel();
            this._footerPanel = new System.Windows.Forms.Panel();
            this._totalCaptionLabel = new System.Windows.Forms.Label();
            this._totalValueLabel = new System.Windows.Forms.Label();
            this._closeButton = new FontAwesome.Sharp.IconButton();
            this._footerLine = new System.Windows.Forms.Panel();
            this._bodyPanel = new System.Windows.Forms.Panel();
            this._gridCard = new System.Windows.Forms.Panel();
            this._grid = new System.Windows.Forms.DataGridView();
            this.colProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBatchCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExpiryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnitCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoss = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._productCountLabel = new System.Windows.Forms.Label();
            this._gap2 = new System.Windows.Forms.Panel();
            this._expiryAlertPanel = new System.Windows.Forms.Panel();
            this._expiryAlertIcon = new FontAwesome.Sharp.IconPictureBox();
            this._expiryAlertLabel = new System.Windows.Forms.Label();
            this._gap1 = new System.Windows.Forms.Panel();
            this._infoCard = new System.Windows.Forms.Panel();
            this._reasonCaptionLabel = new System.Windows.Forms.Label();
            this._reasonValueLabel = new System.Windows.Forms.Label();
            this._createdByCaptionLabel = new System.Windows.Forms.Label();
            this._createdByValueLabel = new System.Windows.Forms.Label();
            this._createdAtCaptionLabel = new System.Windows.Forms.Label();
            this._createdAtValueLabel = new System.Windows.Forms.Label();
            this._itemCountCaptionLabel = new System.Windows.Forms.Label();
            this._itemCountValueLabel = new System.Windows.Forms.Label();
            this._statusCaptionLabel = new System.Windows.Forms.Label();
            this._statusValueLabel = new System.Windows.Forms.Label();
            this._noteCaptionLabel = new System.Windows.Forms.Label();
            this._noteValueLabel = new System.Windows.Forms.Label();
            this._lossCaptionLabel = new System.Windows.Forms.Label();
            this._lossValueLabel = new System.Windows.Forms.Label();
            this._headerPanel.SuspendLayout();
            this._headerIconPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).BeginInit();
            this._footerPanel.SuspendLayout();
            this._bodyPanel.SuspendLayout();
            this._gridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this._expiryAlertPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._expiryAlertIcon)).BeginInit();
            this._infoCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // _headerPanel
            // 
            this._headerPanel.BackColor = System.Drawing.Color.White;
            this._headerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._headerPanel.Controls.Add(this._headerIconPanel);
            this._headerPanel.Controls.Add(this._disposalCodeLabel);
            this._headerPanel.Controls.Add(this._subtitleLabel);
            this._headerPanel.Controls.Add(this._statusLabel);
            this._headerPanel.Controls.Add(this._headerLine);
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._headerPanel.Location = new System.Drawing.Point(0, 0);
            this._headerPanel.Name = "_headerPanel";
            this._headerPanel.Size = new System.Drawing.Size(864, 80);
            this._headerPanel.TabIndex = 0;
            // 
            // _headerIconPanel
            // 
            this._headerIconPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this._headerIconPanel.Controls.Add(this._headerIcon);
            this._headerIconPanel.Location = new System.Drawing.Point(24, 18);
            this._headerIconPanel.Name = "_headerIconPanel";
            this._headerIconPanel.Size = new System.Drawing.Size(44, 44);
            this._headerIconPanel.TabIndex = 0;
            // 
            // _headerIcon
            // 
            this._headerIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this._headerIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this._headerIcon.IconChar = FontAwesome.Sharp.IconChar.TrashCan;
            this._headerIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this._headerIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._headerIcon.IconSize = 20;
            this._headerIcon.Location = new System.Drawing.Point(12, 12);
            this._headerIcon.Name = "_headerIcon";
            this._headerIcon.Size = new System.Drawing.Size(20, 20);
            this._headerIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._headerIcon.TabIndex = 0;
            this._headerIcon.TabStop = false;
            // 
            // _disposalCodeLabel
            // 
            this._disposalCodeLabel.AutoSize = false;
            this._disposalCodeLabel.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this._disposalCodeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._disposalCodeLabel.Location = new System.Drawing.Point(80, 14);
            this._disposalCodeLabel.Name = "_disposalCodeLabel";
            this._disposalCodeLabel.Size = new System.Drawing.Size(600, 30);
            this._disposalCodeLabel.TabIndex = 1;
            this._disposalCodeLabel.Text = "TH_000000";
            this._disposalCodeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._disposalCodeLabel.UseMnemonic = false;
            // 
            // _subtitleLabel
            // 
            this._subtitleLabel.AutoSize = false;
            this._subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this._subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._subtitleLabel.Location = new System.Drawing.Point(80, 44);
            this._subtitleLabel.Name = "_subtitleLabel";
            this._subtitleLabel.Size = new System.Drawing.Size(600, 20);
            this._subtitleLabel.TabIndex = 2;
            this._subtitleLabel.Text = "Hết hạn  ·  Hoàn tất";
            this._subtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._subtitleLabel.UseMnemonic = false;
            // 
            // _statusLabel
            // 
            this._statusLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this._statusLabel.AutoSize = false;
            this._statusLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this._statusLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this._statusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this._statusLabel.Location = new System.Drawing.Point(750, 25);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(90, 30);
            this._statusLabel.TabIndex = 3;
            this._statusLabel.Text = "Hoàn tất";
            this._statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _headerLine
            // 
            this._headerLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._headerLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._headerLine.Location = new System.Drawing.Point(0, 79);
            this._headerLine.Name = "_headerLine";
            this._headerLine.Size = new System.Drawing.Size(864, 1);
            this._headerLine.TabIndex = 4;
            // 
            // _footerPanel
            // 
            this._footerPanel.BackColor = System.Drawing.Color.White;
            this._footerPanel.Controls.Add(this._totalCaptionLabel);
            this._footerPanel.Controls.Add(this._totalValueLabel);
            this._footerPanel.Controls.Add(this._closeButton);
            this._footerPanel.Controls.Add(this._footerLine);
            this._footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._footerPanel.Location = new System.Drawing.Point(0, 555);
            this._footerPanel.Name = "_footerPanel";
            this._footerPanel.Size = new System.Drawing.Size(864, 66);
            this._footerPanel.TabIndex = 1;
            // 
            // _totalCaptionLabel
            // 
            this._totalCaptionLabel.AutoSize = true;
            this._totalCaptionLabel.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this._totalCaptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._totalCaptionLabel.Location = new System.Drawing.Point(24, 22);
            this._totalCaptionLabel.Name = "_totalCaptionLabel";
            this._totalCaptionLabel.Size = new System.Drawing.Size(114, 19);
            this._totalCaptionLabel.TabIndex = 0;
            this._totalCaptionLabel.Text = "Tổng thiệt hại:";
            // 
            // _totalValueLabel
            // 
            this._totalValueLabel.AutoSize = false;
            this._totalValueLabel.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this._totalValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this._totalValueLabel.Location = new System.Drawing.Point(140, 17);
            this._totalValueLabel.Name = "_totalValueLabel";
            this._totalValueLabel.Size = new System.Drawing.Size(320, 30);
            this._totalValueLabel.TabIndex = 1;
            this._totalValueLabel.Text = "0 đ";
            this._totalValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _closeButton
            // 
            this._closeButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this._closeButton.BackColor = System.Drawing.Color.White;
            this._closeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this._closeButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this._closeButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._closeButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._closeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._closeButton.IconChar = FontAwesome.Sharp.IconChar.Xmark;
            this._closeButton.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._closeButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._closeButton.IconSize = 16;
            this._closeButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._closeButton.Location = new System.Drawing.Point(744, 14);
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(96, 38);
            this._closeButton.TabIndex = 2;
            this._closeButton.Text = "Đóng";
            this._closeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._closeButton.UseVisualStyleBackColor = false;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _footerLine
            // 
            this._footerLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._footerLine.Dock = System.Windows.Forms.DockStyle.Top;
            this._footerLine.Location = new System.Drawing.Point(0, 0);
            this._footerLine.Name = "_footerLine";
            this._footerLine.Size = new System.Drawing.Size(864, 1);
            this._footerLine.TabIndex = 3;
            // 
            // _bodyPanel
            // 
            this._bodyPanel.BackColor = System.Drawing.Color.White;
            this._bodyPanel.Controls.Add(this._gridCard);
            this._bodyPanel.Controls.Add(this._productCountLabel);
            this._bodyPanel.Controls.Add(this._gap2);
            this._bodyPanel.Controls.Add(this._expiryAlertPanel);
            this._bodyPanel.Controls.Add(this._gap1);
            this._bodyPanel.Controls.Add(this._infoCard);
            this._bodyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._bodyPanel.Location = new System.Drawing.Point(0, 80);
            this._bodyPanel.Name = "_bodyPanel";
            this._bodyPanel.Padding = new System.Windows.Forms.Padding(24, 18, 24, 12);
            this._bodyPanel.Size = new System.Drawing.Size(864, 475);
            this._bodyPanel.TabIndex = 2;
            // 
            // _gridCard
            // 
            this._gridCard.BackColor = System.Drawing.Color.White;
            this._gridCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._gridCard.Controls.Add(this._grid);
            this._gridCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridCard.Location = new System.Drawing.Point(24, 303);
            this._gridCard.Name = "_gridCard";
            this._gridCard.Size = new System.Drawing.Size(816, 147);
            this._gridCard.TabIndex = 0;
            // 
            // _grid
            // 
            this._grid.AllowUserToAddRows = false;
            this._grid.AllowUserToDeleteRows = false;
            this._grid.AllowUserToResizeRows = false;
            this._grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._grid.BackgroundColor = System.Drawing.Color.White;
            this._grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._grid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Single;
            this._grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this._grid.ColumnHeadersHeight = 38;
            this._grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this._grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProductCode,
            this.colProductName,
            this.colBatchCode,
            this.colExpiryDate,
            this.colQuantity,
            this.colUnitCost,
            this.colLoss});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._grid.DefaultCellStyle = dataGridViewCellStyle2;
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.EnableHeadersVisualStyles = false;
            this._grid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._grid.Location = new System.Drawing.Point(0, 0);
            this._grid.MultiSelect = false;
            this._grid.Name = "_grid";
            this._grid.ReadOnly = true;
            this._grid.RowHeadersVisible = false;
            this._grid.RowTemplate.Height = 36;
            this._grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._grid.Size = new System.Drawing.Size(814, 145);
            this._grid.TabIndex = 0;
            // 
            // colProductCode
            // 
            this.colProductCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colProductCode.FillWeight = 12F;
            this.colProductCode.HeaderText = "Mã sản phẩm";
            this.colProductCode.Name = "colProductCode";
            this.colProductCode.ReadOnly = true;
            this.colProductCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colProductName
            // 
            this.colProductName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colProductName.FillWeight = 22F;
            this.colProductName.HeaderText = "Sản phẩm";
            this.colProductName.Name = "colProductName";
            this.colProductName.ReadOnly = true;
            this.colProductName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colBatchCode
            // 
            this.colBatchCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colBatchCode.FillWeight = 15F;
            this.colBatchCode.HeaderText = "Mã lô (hệ thống)";
            this.colBatchCode.Name = "colBatchCode";
            this.colBatchCode.ReadOnly = true;
            this.colBatchCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colExpiryDate
            // 
            this.colExpiryDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colExpiryDate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colExpiryDate.FillWeight = 11F;
            this.colExpiryDate.HeaderCell.Style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colExpiryDate.HeaderText = "HSD";
            this.colExpiryDate.Name = "colExpiryDate";
            this.colExpiryDate.ReadOnly = true;
            this.colExpiryDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colQuantity
            // 
            this.colQuantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colQuantity.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colQuantity.FillWeight = 8F;
            this.colQuantity.HeaderCell.Style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colQuantity.HeaderText = "SL hủy";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.ReadOnly = true;
            this.colQuantity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colUnitCost
            // 
            this.colUnitCost.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colUnitCost.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colUnitCost.FillWeight = 13F;
            this.colUnitCost.HeaderCell.Style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colUnitCost.HeaderText = "Đơn giá vốn";
            this.colUnitCost.Name = "colUnitCost";
            this.colUnitCost.ReadOnly = true;
            this.colUnitCost.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colLoss
            // 
            this.colLoss.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colLoss.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colLoss.FillWeight = 14F;
            this.colLoss.HeaderCell.Style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colLoss.HeaderText = "Thiệt hại";
            this.colLoss.Name = "colLoss";
            this.colLoss.ReadOnly = true;
            this.colLoss.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // _productCountLabel
            // 
            this._productCountLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._productCountLabel.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this._productCountLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._productCountLabel.Location = new System.Drawing.Point(24, 269);
            this._productCountLabel.Name = "_productCountLabel";
            this._productCountLabel.Size = new System.Drawing.Size(816, 34);
            this._productCountLabel.TabIndex = 1;
            this._productCountLabel.Text = "Danh sách sản phẩm (0)";
            this._productCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _gap2
            // 
            this._gap2.BackColor = System.Drawing.Color.White;
            this._gap2.Dock = System.Windows.Forms.DockStyle.Top;
            this._gap2.Location = new System.Drawing.Point(24, 255);
            this._gap2.Name = "_gap2";
            this._gap2.Size = new System.Drawing.Size(816, 14);
            this._gap2.TabIndex = 2;
            // 
            // _expiryAlertPanel
            // 
            this._expiryAlertPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this._expiryAlertPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._expiryAlertPanel.Controls.Add(this._expiryAlertIcon);
            this._expiryAlertPanel.Controls.Add(this._expiryAlertLabel);
            this._expiryAlertPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._expiryAlertPanel.Location = new System.Drawing.Point(24, 215);
            this._expiryAlertPanel.Name = "_expiryAlertPanel";
            this._expiryAlertPanel.Size = new System.Drawing.Size(816, 40);
            this._expiryAlertPanel.TabIndex = 3;
            this._expiryAlertPanel.Visible = false;
            // 
            // _expiryAlertIcon
            // 
            this._expiryAlertIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this._expiryAlertIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this._expiryAlertIcon.IconChar = FontAwesome.Sharp.IconChar.TriangleExclamation;
            this._expiryAlertIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this._expiryAlertIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._expiryAlertIcon.IconSize = 18;
            this._expiryAlertIcon.Location = new System.Drawing.Point(12, 10);
            this._expiryAlertIcon.Name = "_expiryAlertIcon";
            this._expiryAlertIcon.Size = new System.Drawing.Size(18, 18);
            this._expiryAlertIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._expiryAlertIcon.TabIndex = 0;
            this._expiryAlertIcon.TabStop = false;
            // 
            // _expiryAlertLabel
            // 
            this._expiryAlertLabel.AutoSize = true;
            this._expiryAlertLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this._expiryAlertLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this._expiryAlertLabel.Location = new System.Drawing.Point(38, 11);
            this._expiryAlertLabel.Name = "_expiryAlertLabel";
            this._expiryAlertLabel.Size = new System.Drawing.Size(375, 17);
            this._expiryAlertLabel.TabIndex = 1;
            this._expiryAlertLabel.Text = "Phiếu có lô hàng sắp hết hạn hoặc đã hết hạn sử dụng.";
            // 
            // _gap1
            // 
            this._gap1.BackColor = System.Drawing.Color.White;
            this._gap1.Dock = System.Windows.Forms.DockStyle.Top;
            this._gap1.Location = new System.Drawing.Point(24, 201);
            this._gap1.Name = "_gap1";
            this._gap1.Size = new System.Drawing.Size(816, 14);
            this._gap1.TabIndex = 4;
            // 
            // _infoCard
            // 
            this._infoCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._infoCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._infoCard.Controls.Add(this._reasonCaptionLabel);
            this._infoCard.Controls.Add(this._reasonValueLabel);
            this._infoCard.Controls.Add(this._createdByCaptionLabel);
            this._infoCard.Controls.Add(this._createdByValueLabel);
            this._infoCard.Controls.Add(this._createdAtCaptionLabel);
            this._infoCard.Controls.Add(this._createdAtValueLabel);
            this._infoCard.Controls.Add(this._itemCountCaptionLabel);
            this._infoCard.Controls.Add(this._itemCountValueLabel);
            this._infoCard.Controls.Add(this._statusCaptionLabel);
            this._infoCard.Controls.Add(this._statusValueLabel);
            this._infoCard.Controls.Add(this._noteCaptionLabel);
            this._infoCard.Controls.Add(this._noteValueLabel);
            this._infoCard.Controls.Add(this._lossCaptionLabel);
            this._infoCard.Controls.Add(this._lossValueLabel);
            this._infoCard.Dock = System.Windows.Forms.DockStyle.Top;
            this._infoCard.Location = new System.Drawing.Point(24, 18);
            this._infoCard.Name = "_infoCard";
            this._infoCard.Size = new System.Drawing.Size(816, 172);
            this._infoCard.TabIndex = 5;
            // 
            // _reasonCaptionLabel
            // 
            this._reasonCaptionLabel.AutoSize = true;
            this._reasonCaptionLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._reasonCaptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._reasonCaptionLabel.Location = new System.Drawing.Point(18, 12);
            this._reasonCaptionLabel.Name = "_reasonCaptionLabel";
            this._reasonCaptionLabel.Size = new System.Drawing.Size(92, 15);
            this._reasonCaptionLabel.TabIndex = 0;
            this._reasonCaptionLabel.Text = "Lý do tiêu hủy";
            // 
            // _reasonValueLabel
            // 
            this._reasonValueLabel.AutoEllipsis = true;
            this._reasonValueLabel.AutoSize = false;
            this._reasonValueLabel.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this._reasonValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._reasonValueLabel.Location = new System.Drawing.Point(18, 30);
            this._reasonValueLabel.Name = "_reasonValueLabel";
            this._reasonValueLabel.Size = new System.Drawing.Size(380, 22);
            this._reasonValueLabel.TabIndex = 1;
            this._reasonValueLabel.UseMnemonic = false;
            // 
            // _createdByCaptionLabel
            // 
            this._createdByCaptionLabel.AutoSize = true;
            this._createdByCaptionLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._createdByCaptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._createdByCaptionLabel.Location = new System.Drawing.Point(420, 12);
            this._createdByCaptionLabel.Name = "_createdByCaptionLabel";
            this._createdByCaptionLabel.Size = new System.Drawing.Size(87, 15);
            this._createdByCaptionLabel.TabIndex = 2;
            this._createdByCaptionLabel.Text = "Người tạo phiếu";
            // 
            // _createdByValueLabel
            // 
            this._createdByValueLabel.AutoEllipsis = true;
            this._createdByValueLabel.AutoSize = false;
            this._createdByValueLabel.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this._createdByValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._createdByValueLabel.Location = new System.Drawing.Point(420, 30);
            this._createdByValueLabel.Name = "_createdByValueLabel";
            this._createdByValueLabel.Size = new System.Drawing.Size(380, 22);
            this._createdByValueLabel.TabIndex = 3;
            this._createdByValueLabel.UseMnemonic = false;
            // 
            // _createdAtCaptionLabel
            // 
            this._createdAtCaptionLabel.AutoSize = true;
            this._createdAtCaptionLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._createdAtCaptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._createdAtCaptionLabel.Location = new System.Drawing.Point(18, 52);
            this._createdAtCaptionLabel.Name = "_createdAtCaptionLabel";
            this._createdAtCaptionLabel.Size = new System.Drawing.Size(53, 15);
            this._createdAtCaptionLabel.TabIndex = 4;
            this._createdAtCaptionLabel.Text = "Ngày tạo";
            // 
            // _createdAtValueLabel
            // 
            this._createdAtValueLabel.AutoEllipsis = true;
            this._createdAtValueLabel.AutoSize = false;
            this._createdAtValueLabel.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this._createdAtValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._createdAtValueLabel.Location = new System.Drawing.Point(18, 70);
            this._createdAtValueLabel.Name = "_createdAtValueLabel";
            this._createdAtValueLabel.Size = new System.Drawing.Size(380, 22);
            this._createdAtValueLabel.TabIndex = 5;
            this._createdAtValueLabel.UseMnemonic = false;
            // 
            // _itemCountCaptionLabel
            // 
            this._itemCountCaptionLabel.AutoSize = true;
            this._itemCountCaptionLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._itemCountCaptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._itemCountCaptionLabel.Location = new System.Drawing.Point(420, 52);
            this._itemCountCaptionLabel.Name = "_itemCountCaptionLabel";
            this._itemCountCaptionLabel.Size = new System.Drawing.Size(100, 15);
            this._itemCountCaptionLabel.TabIndex = 6;
            this._itemCountCaptionLabel.Text = "Số dòng sản phẩm";
            // 
            // _itemCountValueLabel
            // 
            this._itemCountValueLabel.AutoEllipsis = true;
            this._itemCountValueLabel.AutoSize = false;
            this._itemCountValueLabel.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this._itemCountValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._itemCountValueLabel.Location = new System.Drawing.Point(420, 70);
            this._itemCountValueLabel.Name = "_itemCountValueLabel";
            this._itemCountValueLabel.Size = new System.Drawing.Size(380, 22);
            this._itemCountValueLabel.TabIndex = 7;
            this._itemCountValueLabel.UseMnemonic = false;
            // 
            // _statusCaptionLabel
            // 
            this._statusCaptionLabel.AutoSize = true;
            this._statusCaptionLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._statusCaptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._statusCaptionLabel.Location = new System.Drawing.Point(18, 92);
            this._statusCaptionLabel.Name = "_statusCaptionLabel";
            this._statusCaptionLabel.Size = new System.Drawing.Size(58, 15);
            this._statusCaptionLabel.TabIndex = 8;
            this._statusCaptionLabel.Text = "Trạng thái";
            // 
            // _statusValueLabel
            // 
            this._statusValueLabel.AutoEllipsis = true;
            this._statusValueLabel.AutoSize = false;
            this._statusValueLabel.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this._statusValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this._statusValueLabel.Location = new System.Drawing.Point(18, 110);
            this._statusValueLabel.Name = "_statusValueLabel";
            this._statusValueLabel.Size = new System.Drawing.Size(380, 22);
            this._statusValueLabel.TabIndex = 9;
            this._statusValueLabel.UseMnemonic = false;
            // 
            // _noteCaptionLabel
            // 
            this._noteCaptionLabel.AutoSize = true;
            this._noteCaptionLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._noteCaptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._noteCaptionLabel.Location = new System.Drawing.Point(420, 92);
            this._noteCaptionLabel.Name = "_noteCaptionLabel";
            this._noteCaptionLabel.Size = new System.Drawing.Size(48, 15);
            this._noteCaptionLabel.TabIndex = 10;
            this._noteCaptionLabel.Text = "Ghi chú";
            // 
            // _noteValueLabel
            // 
            this._noteValueLabel.AutoEllipsis = true;
            this._noteValueLabel.AutoSize = false;
            this._noteValueLabel.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this._noteValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._noteValueLabel.Location = new System.Drawing.Point(420, 110);
            this._noteValueLabel.Name = "_noteValueLabel";
            this._noteValueLabel.Size = new System.Drawing.Size(380, 22);
            this._noteValueLabel.TabIndex = 11;
            this._noteValueLabel.UseMnemonic = false;
            // 
            // _lossCaptionLabel
            // 
            this._lossCaptionLabel.AutoSize = true;
            this._lossCaptionLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._lossCaptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._lossCaptionLabel.Location = new System.Drawing.Point(18, 132);
            this._lossCaptionLabel.Name = "_lossCaptionLabel";
            this._lossCaptionLabel.Size = new System.Drawing.Size(83, 15);
            this._lossCaptionLabel.TabIndex = 12;
            this._lossCaptionLabel.Text = "Tổng thiệt hại";
            // 
            // _lossValueLabel
            // 
            this._lossValueLabel.AutoEllipsis = true;
            this._lossValueLabel.AutoSize = false;
            this._lossValueLabel.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this._lossValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this._lossValueLabel.Location = new System.Drawing.Point(18, 150);
            this._lossValueLabel.Name = "_lossValueLabel";
            this._lossValueLabel.Size = new System.Drawing.Size(380, 20);
            this._lossValueLabel.TabIndex = 13;
            this._lossValueLabel.UseMnemonic = false;
            // 
            // frmStockDisposalDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this._closeButton;
            this.ClientSize = new System.Drawing.Size(864, 621);
            this.Controls.Add(this._bodyPanel);
            this.Controls.Add(this._footerPanel);
            this.Controls.Add(this._headerPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 500);
            this.Name = "frmStockDisposalDetail";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chi tiết phiếu tiêu hủy";
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this._headerPanel.ResumeLayout(false);
            this._headerIconPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).EndInit();
            this._footerPanel.ResumeLayout(false);
            this._footerPanel.PerformLayout();
            this._bodyPanel.ResumeLayout(false);
            this._gridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this._expiryAlertPanel.ResumeLayout(false);
            this._expiryAlertPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._expiryAlertIcon)).EndInit();
            this._infoCard.ResumeLayout(false);
            this._infoCard.PerformLayout();
            this.ResumeLayout(false);
        }

        private Panel _headerPanel;
        private Panel _headerIconPanel;
        private IconPictureBox _headerIcon;
        private Label _disposalCodeLabel;
        private Label _subtitleLabel;
        private Label _statusLabel;
        private Panel _headerLine;
        private Panel _footerPanel;
        private Label _totalCaptionLabel;
        private Label _totalValueLabel;
        private IconButton _closeButton;
        private Panel _footerLine;
        private Panel _bodyPanel;
        private Panel _gridCard;
        private DataGridView _grid;
        private DataGridViewTextBoxColumn colProductCode;
        private DataGridViewTextBoxColumn colProductName;
        private DataGridViewTextBoxColumn colBatchCode;
        private DataGridViewTextBoxColumn colExpiryDate;
        private DataGridViewTextBoxColumn colQuantity;
        private DataGridViewTextBoxColumn colUnitCost;
        private DataGridViewTextBoxColumn colLoss;
        private Label _productCountLabel;
        private Panel _gap2;
        private Panel _expiryAlertPanel;
        private IconPictureBox _expiryAlertIcon;
        private Label _expiryAlertLabel;
        private Panel _gap1;
        private Panel _infoCard;
        private Label _reasonCaptionLabel;
        private Label _reasonValueLabel;
        private Label _createdByCaptionLabel;
        private Label _createdByValueLabel;
        private Label _createdAtCaptionLabel;
        private Label _createdAtValueLabel;
        private Label _itemCountCaptionLabel;
        private Label _itemCountValueLabel;
        private Label _statusCaptionLabel;
        private Label _statusValueLabel;
        private Label _noteCaptionLabel;
        private Label _noteValueLabel;
        private Label _lossCaptionLabel;
        private Label _lossValueLabel;
    }
}
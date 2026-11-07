using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    partial class frmStockDisposalEditor
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this._headerPanel = new System.Windows.Forms.Panel();
            this._headerIconPanel = new System.Windows.Forms.Panel();
            this._headerIcon = new FontAwesome.Sharp.IconPictureBox();
            this._titleLabel = new System.Windows.Forms.Label();
            this._subtitleLabel = new System.Windows.Forms.Label();
            this._headerLine = new System.Windows.Forms.Panel();
            this._footerPanel = new System.Windows.Forms.Panel();
            this._totalCaptionLabel = new System.Windows.Forms.Label();
            this._totalValueLabel = new System.Windows.Forms.Label();
            this._cancelButton = new FontAwesome.Sharp.IconButton();
            this._saveButton = new FontAwesome.Sharp.IconButton();
            this._footerLine = new System.Windows.Forms.Panel();
            this._bodyPanel = new System.Windows.Forms.Panel();
            this._linesCard = new System.Windows.Forms.Panel();
            this._linesIcon = new FontAwesome.Sharp.IconPictureBox();
            this._linesTitle = new System.Windows.Forms.Label();
            this._lineCountLabel = new System.Windows.Forms.Label();
            this._linesGrid = new System.Windows.Forms.DataGridView();
            this.colBatchCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExpiry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnitCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoss = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRemove = new System.Windows.Forms.DataGridViewImageColumn();
            this._emptyLabel = new System.Windows.Forms.Label();
            this._gap2 = new System.Windows.Forms.Panel();
            this._entryCard = new System.Windows.Forms.Panel();
            this._entryIcon = new FontAwesome.Sharp.IconPictureBox();
            this._entryTitle = new System.Windows.Forms.Label();
            this._batchLabel = new System.Windows.Forms.Label();
            this._batchComboBox = new System.Windows.Forms.ComboBox();
            this._quantityLabel = new System.Windows.Forms.Label();
            this._quantityTextBox = new System.Windows.Forms.TextBox();
            this._addLineButton = new FontAwesome.Sharp.IconButton();
            this._remainHintLabel = new System.Windows.Forms.Label();
            this._gap1 = new System.Windows.Forms.Panel();
            this._reasonCard = new System.Windows.Forms.Panel();
            this._reasonLabel = new System.Windows.Forms.Label();
            this._reasonComboBox = new System.Windows.Forms.ComboBox();
            this._noteLabel = new System.Windows.Forms.Label();
            this._noteTextBox = new System.Windows.Forms.TextBox();
            this._headerPanel.SuspendLayout();
            this._headerIconPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).BeginInit();
            this._footerPanel.SuspendLayout();
            this._bodyPanel.SuspendLayout();
            this._linesCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._linesIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._linesGrid)).BeginInit();
            this._entryCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._entryIcon)).BeginInit();
            this._reasonCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // _headerPanel
            // 
            this._headerPanel.BackColor = System.Drawing.Color.White;
            this._headerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._headerPanel.Controls.Add(this._headerIconPanel);
            this._headerPanel.Controls.Add(this._titleLabel);
            this._headerPanel.Controls.Add(this._subtitleLabel);
            this._headerPanel.Controls.Add(this._headerLine);
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._headerPanel.Location = new System.Drawing.Point(0, 0);
            this._headerPanel.Name = "_headerPanel";
            this._headerPanel.Size = new System.Drawing.Size(1004, 92);
            this._headerPanel.TabIndex = 0;
            // 
            // _headerIconPanel
            // 
            this._headerIconPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this._headerIconPanel.Controls.Add(this._headerIcon);
            this._headerIconPanel.Location = new System.Drawing.Point(28, 20);
            this._headerIconPanel.Name = "_headerIconPanel";
            this._headerIconPanel.Size = new System.Drawing.Size(52, 52);
            this._headerIconPanel.TabIndex = 0;
            // 
            // _headerIcon
            // 
            this._headerIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this._headerIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this._headerIcon.IconChar = FontAwesome.Sharp.IconChar.TrashAlt;
            this._headerIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this._headerIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._headerIcon.IconSize = 22;
            this._headerIcon.Location = new System.Drawing.Point(15, 15);
            this._headerIcon.Name = "_headerIcon";
            this._headerIcon.Size = new System.Drawing.Size(22, 22);
            this._headerIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._headerIcon.TabIndex = 0;
            this._headerIcon.TabStop = false;
            // 
            // _titleLabel
            // 
            this._titleLabel.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this._titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._titleLabel.Location = new System.Drawing.Point(92, 8);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(600, 42);
            this._titleLabel.TabIndex = 1;
            this._titleLabel.Text = "Lập phiếu tiêu hủy hàng";
            this._titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._titleLabel.UseMnemonic = false;
            // 
            // _subtitleLabel
            // 
            this._subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this._subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._subtitleLabel.Location = new System.Drawing.Point(92, 52);
            this._subtitleLabel.Name = "_subtitleLabel";
            this._subtitleLabel.Size = new System.Drawing.Size(700, 22);
            this._subtitleLabel.TabIndex = 2;
            this._subtitleLabel.Text = "Một phiếu · nhiều lô · trừ tồn theo FEFO / HSD · tổn thất = SL × giá nhập lô";
            this._subtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._subtitleLabel.UseMnemonic = false;
            // 
            // _headerLine
            // 
            this._headerLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._headerLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._headerLine.Location = new System.Drawing.Point(0, 89);
            this._headerLine.Name = "_headerLine";
            this._headerLine.Size = new System.Drawing.Size(1002, 1);
            this._headerLine.TabIndex = 3;
            // 
            // _footerPanel
            // 
            this._footerPanel.BackColor = System.Drawing.Color.White;
            this._footerPanel.Controls.Add(this._totalCaptionLabel);
            this._footerPanel.Controls.Add(this._totalValueLabel);
            this._footerPanel.Controls.Add(this._cancelButton);
            this._footerPanel.Controls.Add(this._saveButton);
            this._footerPanel.Controls.Add(this._footerLine);
            this._footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._footerPanel.Location = new System.Drawing.Point(0, 585);
            this._footerPanel.Name = "_footerPanel";
            this._footerPanel.Size = new System.Drawing.Size(1004, 76);
            this._footerPanel.TabIndex = 1;
            // 
            // _totalCaptionLabel
            // 
            this._totalCaptionLabel.AutoSize = true;
            this._totalCaptionLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this._totalCaptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._totalCaptionLabel.Location = new System.Drawing.Point(28, 14);
            this._totalCaptionLabel.Name = "_totalCaptionLabel";
            this._totalCaptionLabel.Size = new System.Drawing.Size(149, 23);
            this._totalCaptionLabel.TabIndex = 0;
            this._totalCaptionLabel.Text = "TỔNG TỔN THẤT";
            // 
            // _totalValueLabel
            // 
            this._totalValueLabel.Font = new System.Drawing.Font("Segoe UI", 16.5F, System.Drawing.FontStyle.Bold);
            this._totalValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this._totalValueLabel.Location = new System.Drawing.Point(26, 32);
            this._totalValueLabel.Name = "_totalValueLabel";
            this._totalValueLabel.Size = new System.Drawing.Size(360, 32);
            this._totalValueLabel.TabIndex = 1;
            this._totalValueLabel.Text = "0 đ";
            this._totalValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _cancelButton
            // 
            this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cancelButton.BackColor = System.Drawing.Color.White;
            this._cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._cancelButton.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this._cancelButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._cancelButton.IconChar = FontAwesome.Sharp.IconChar.Remove;
            this._cancelButton.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._cancelButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._cancelButton.IconSize = 14;
            this._cancelButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._cancelButton.Location = new System.Drawing.Point(692, 18);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(90, 40);
            this._cancelButton.TabIndex = 2;
            this._cancelButton.Text = "Hủy";
            this._cancelButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            this._cancelButton.UseVisualStyleBackColor = false;
            // 
            // _saveButton
            // 
            this._saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._saveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._saveButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this._saveButton.FlatAppearance.BorderSize = 0;
            this._saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._saveButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this._saveButton.ForeColor = System.Drawing.Color.White;
            this._saveButton.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this._saveButton.IconColor = System.Drawing.Color.White;
            this._saveButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._saveButton.IconSize = 16;
            this._saveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._saveButton.Location = new System.Drawing.Point(794, 18);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(182, 40);
            this._saveButton.TabIndex = 3;
            this._saveButton.Text = "Lưu phiếu tiêu hủy";
            this._saveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._saveButton.UseVisualStyleBackColor = false;
            this._saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // _footerLine
            // 
            this._footerLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._footerLine.Dock = System.Windows.Forms.DockStyle.Top;
            this._footerLine.Location = new System.Drawing.Point(0, 0);
            this._footerLine.Name = "_footerLine";
            this._footerLine.Size = new System.Drawing.Size(1004, 1);
            this._footerLine.TabIndex = 4;
            // 
            // _bodyPanel
            // 
            this._bodyPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._bodyPanel.Controls.Add(this._linesCard);
            this._bodyPanel.Controls.Add(this._gap2);
            this._bodyPanel.Controls.Add(this._entryCard);
            this._bodyPanel.Controls.Add(this._gap1);
            this._bodyPanel.Controls.Add(this._reasonCard);
            this._bodyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._bodyPanel.Location = new System.Drawing.Point(0, 92);
            this._bodyPanel.Name = "_bodyPanel";
            this._bodyPanel.Padding = new System.Windows.Forms.Padding(24, 16, 24, 12);
            this._bodyPanel.Size = new System.Drawing.Size(1004, 493);
            this._bodyPanel.TabIndex = 2;
            // 
            // _linesCard
            // 
            this._linesCard.BackColor = System.Drawing.Color.White;
            this._linesCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._linesCard.Controls.Add(this._linesIcon);
            this._linesCard.Controls.Add(this._linesTitle);
            this._linesCard.Controls.Add(this._lineCountLabel);
            this._linesCard.Controls.Add(this._linesGrid);
            this._linesCard.Controls.Add(this._emptyLabel);
            this._linesCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._linesCard.Location = new System.Drawing.Point(24, 286);
            this._linesCard.Name = "_linesCard";
            this._linesCard.Size = new System.Drawing.Size(956, 195);
            this._linesCard.TabIndex = 0;
            // 
            // _linesIcon
            // 
            this._linesIcon.BackColor = System.Drawing.Color.White;
            this._linesIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._linesIcon.IconChar = FontAwesome.Sharp.IconChar.ListUl;
            this._linesIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._linesIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._linesIcon.IconSize = 18;
            this._linesIcon.Location = new System.Drawing.Point(18, 15);
            this._linesIcon.Name = "_linesIcon";
            this._linesIcon.Size = new System.Drawing.Size(18, 18);
            this._linesIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._linesIcon.TabIndex = 0;
            this._linesIcon.TabStop = false;
            // 
            // _linesTitle
            // 
            this._linesTitle.AutoSize = true;
            this._linesTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this._linesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._linesTitle.Location = new System.Drawing.Point(42, 13);
            this._linesTitle.Name = "_linesTitle";
            this._linesTitle.Size = new System.Drawing.Size(247, 30);
            this._linesTitle.TabIndex = 1;
            this._linesTitle.Text = "Danh sách lô trên phiếu";
            // 
            // _lineCountLabel
            // 
            this._lineCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._lineCountLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._lineCountLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._lineCountLabel.Location = new System.Drawing.Point(836, 15);
            this._lineCountLabel.Name = "_lineCountLabel";
            this._lineCountLabel.Size = new System.Drawing.Size(100, 20);
            this._lineCountLabel.TabIndex = 2;
            this._lineCountLabel.Text = "0 dòng";
            this._lineCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _linesGrid
            // 
            this._linesGrid.AllowUserToAddRows = false;
            this._linesGrid.AllowUserToDeleteRows = false;
            this._linesGrid.AllowUserToResizeRows = false;
            this._linesGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._linesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._linesGrid.BackgroundColor = System.Drawing.Color.White;
            this._linesGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._linesGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._linesGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this._linesGrid.ColumnHeadersHeight = 36;
            this._linesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this._linesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colBatchCode,
            this.colProduct,
            this.colExpiry,
            this.colQuantity,
            this.colUnitCost,
            this.colLoss,
            this.colRemove});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle9.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._linesGrid.DefaultCellStyle = dataGridViewCellStyle9;
            this._linesGrid.EnableHeadersVisualStyles = false;
            this._linesGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._linesGrid.Location = new System.Drawing.Point(18, 46);
            this._linesGrid.MultiSelect = false;
            this._linesGrid.Name = "_linesGrid";
            this._linesGrid.ReadOnly = true;
            this._linesGrid.RowHeadersVisible = false;
            this._linesGrid.RowHeadersWidth = 62;
            this._linesGrid.RowTemplate.Height = 34;
            this._linesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._linesGrid.Size = new System.Drawing.Size(918, 135);
            this._linesGrid.TabIndex = 3;
            // 
            // colBatchCode
            // 
            this.colBatchCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colBatchCode.DefaultCellStyle = dataGridViewCellStyle2;
            this.colBatchCode.FillWeight = 14F;
            this.colBatchCode.HeaderText = "Mã lô";
            this.colBatchCode.MinimumWidth = 8;
            this.colBatchCode.Name = "colBatchCode";
            this.colBatchCode.ReadOnly = true;
            this.colBatchCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colProduct
            // 
            this.colProduct.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.colProduct.DefaultCellStyle = dataGridViewCellStyle3;
            this.colProduct.FillWeight = 28F;
            this.colProduct.HeaderText = "Sản phẩm";
            this.colProduct.MinimumWidth = 8;
            this.colProduct.Name = "colProduct";
            this.colProduct.ReadOnly = true;
            this.colProduct.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colExpiry
            // 
            this.colExpiry.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colExpiry.DefaultCellStyle = dataGridViewCellStyle4;
            this.colExpiry.FillWeight = 11F;
            this.colExpiry.HeaderText = "HSD";
            this.colExpiry.MinimumWidth = 8;
            this.colExpiry.Name = "colExpiry";
            this.colExpiry.ReadOnly = true;
            this.colExpiry.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colQuantity
            // 
            this.colQuantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colQuantity.DefaultCellStyle = dataGridViewCellStyle5;
            this.colQuantity.FillWeight = 9F;
            this.colQuantity.HeaderText = "SL hủy";
            this.colQuantity.MinimumWidth = 8;
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.ReadOnly = true;
            this.colQuantity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colUnitCost
            // 
            this.colUnitCost.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colUnitCost.DefaultCellStyle = dataGridViewCellStyle6;
            this.colUnitCost.FillWeight = 13F;
            this.colUnitCost.HeaderText = "Giá vốn";
            this.colUnitCost.MinimumWidth = 8;
            this.colUnitCost.Name = "colUnitCost";
            this.colUnitCost.ReadOnly = true;
            this.colUnitCost.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colLoss
            // 
            this.colLoss.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colLoss.DefaultCellStyle = dataGridViewCellStyle7;
            this.colLoss.FillWeight = 14F;
            this.colLoss.HeaderText = "Tổn thất";
            this.colLoss.MinimumWidth = 8;
            this.colLoss.Name = "colLoss";
            this.colLoss.ReadOnly = true;
            this.colLoss.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colRemove
            // 
            this.colRemove.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.NullValue = null;
            this.colRemove.DefaultCellStyle = dataGridViewCellStyle8;
            this.colRemove.HeaderText = "";
            this.colRemove.MinimumWidth = 8;
            this.colRemove.Name = "colRemove";
            this.colRemove.ReadOnly = true;
            this.colRemove.Width = 46;
            // 
            // _emptyLabel
            // 
            this._emptyLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._emptyLabel.BackColor = System.Drawing.Color.White;
            this._emptyLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this._emptyLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._emptyLabel.Location = new System.Drawing.Point(18, 82);
            this._emptyLabel.Name = "_emptyLabel";
            this._emptyLabel.Size = new System.Drawing.Size(918, 99);
            this._emptyLabel.TabIndex = 4;
            this._emptyLabel.Text = "Chưa có lô — dùng form phía trên để thêm dòng";
            this._emptyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _gap2
            // 
            this._gap2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._gap2.Dock = System.Windows.Forms.DockStyle.Top;
            this._gap2.Location = new System.Drawing.Point(24, 274);
            this._gap2.Name = "_gap2";
            this._gap2.Size = new System.Drawing.Size(956, 12);
            this._gap2.TabIndex = 1;
            // 
            // _entryCard
            // 
            this._entryCard.BackColor = System.Drawing.Color.White;
            this._entryCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._entryCard.Controls.Add(this._entryIcon);
            this._entryCard.Controls.Add(this._entryTitle);
            this._entryCard.Controls.Add(this._batchLabel);
            this._entryCard.Controls.Add(this._batchComboBox);
            this._entryCard.Controls.Add(this._quantityLabel);
            this._entryCard.Controls.Add(this._quantityTextBox);
            this._entryCard.Controls.Add(this._addLineButton);
            this._entryCard.Controls.Add(this._remainHintLabel);
            this._entryCard.Dock = System.Windows.Forms.DockStyle.Top;
            this._entryCard.Location = new System.Drawing.Point(24, 132);
            this._entryCard.Name = "_entryCard";
            this._entryCard.Size = new System.Drawing.Size(956, 142);
            this._entryCard.TabIndex = 2;
            // 
            // _entryIcon
            // 
            this._entryIcon.BackColor = System.Drawing.Color.White;
            this._entryIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._entryIcon.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            this._entryIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._entryIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._entryIcon.IconSize = 18;
            this._entryIcon.Location = new System.Drawing.Point(18, 15);
            this._entryIcon.Name = "_entryIcon";
            this._entryIcon.Size = new System.Drawing.Size(18, 18);
            this._entryIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._entryIcon.TabIndex = 0;
            this._entryIcon.TabStop = false;
            // 
            // _entryTitle
            // 
            this._entryTitle.AutoSize = true;
            this._entryTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this._entryTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._entryTitle.Location = new System.Drawing.Point(42, 13);
            this._entryTitle.Name = "_entryTitle";
            this._entryTitle.Size = new System.Drawing.Size(220, 30);
            this._entryTitle.TabIndex = 1;
            this._entryTitle.Text = "Thêm lô cần tiêu hủy";
            // 
            // _batchLabel
            // 
            this._batchLabel.AutoSize = true;
            this._batchLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._batchLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._batchLabel.Location = new System.Drawing.Point(18, 48);
            this._batchLabel.Name = "_batchLabel";
            this._batchLabel.Size = new System.Drawing.Size(89, 25);
            this._batchLabel.TabIndex = 2;
            this._batchLabel.Text = "Lô hàng *";
            // 
            // _batchComboBox
            // 
            this._batchComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._batchComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._batchComboBox.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this._batchComboBox.FormattingEnabled = true;
            this._batchComboBox.Location = new System.Drawing.Point(18, 70);
            this._batchComboBox.Name = "_batchComboBox";
            this._batchComboBox.Size = new System.Drawing.Size(588, 36);
            this._batchComboBox.TabIndex = 3;
            // 
            // _quantityLabel
            // 
            this._quantityLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._quantityLabel.AutoSize = true;
            this._quantityLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._quantityLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._quantityLabel.Location = new System.Drawing.Point(620, 48);
            this._quantityLabel.Name = "_quantityLabel";
            this._quantityLabel.Size = new System.Drawing.Size(98, 25);
            this._quantityLabel.TabIndex = 4;
            this._quantityLabel.Text = "Số lượng *";
            // 
            // _quantityTextBox
            // 
            this._quantityTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._quantityTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._quantityTextBox.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this._quantityTextBox.Location = new System.Drawing.Point(620, 70);
            this._quantityTextBox.Name = "_quantityTextBox";
            this._quantityTextBox.Size = new System.Drawing.Size(124, 33);
            this._quantityTextBox.TabIndex = 5;
            this._quantityTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _addLineButton
            // 
            this._addLineButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._addLineButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._addLineButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this._addLineButton.FlatAppearance.BorderSize = 0;
            this._addLineButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._addLineButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this._addLineButton.ForeColor = System.Drawing.Color.White;
            this._addLineButton.IconChar = FontAwesome.Sharp.IconChar.Plus;
            this._addLineButton.IconColor = System.Drawing.Color.White;
            this._addLineButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._addLineButton.IconSize = 14;
            this._addLineButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._addLineButton.Location = new System.Drawing.Point(756, 66);
            this._addLineButton.Name = "_addLineButton";
            this._addLineButton.Size = new System.Drawing.Size(166, 36);
            this._addLineButton.TabIndex = 6;
            this._addLineButton.Text = "Thêm dòng";
            this._addLineButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._addLineButton.Click += new System.EventHandler(this.AddLineButton_Click);
            this._addLineButton.UseVisualStyleBackColor = false;
            // 
            // _remainHintLabel
            // 
            this._remainHintLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._remainHintLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._remainHintLabel.Location = new System.Drawing.Point(18, 104);
            this._remainHintLabel.Name = "_remainHintLabel";
            this._remainHintLabel.Size = new System.Drawing.Size(588, 20);
            this._remainHintLabel.TabIndex = 7;
            this._remainHintLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _gap1
            // 
            this._gap1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._gap1.Dock = System.Windows.Forms.DockStyle.Top;
            this._gap1.Location = new System.Drawing.Point(24, 120);
            this._gap1.Name = "_gap1";
            this._gap1.Size = new System.Drawing.Size(956, 12);
            this._gap1.TabIndex = 3;
            // 
            // _reasonCard
            // 
            this._reasonCard.BackColor = System.Drawing.Color.White;
            this._reasonCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._reasonCard.Controls.Add(this._reasonLabel);
            this._reasonCard.Controls.Add(this._reasonComboBox);
            this._reasonCard.Controls.Add(this._noteLabel);
            this._reasonCard.Controls.Add(this._noteTextBox);
            this._reasonCard.Dock = System.Windows.Forms.DockStyle.Top;
            this._reasonCard.Location = new System.Drawing.Point(24, 16);
            this._reasonCard.Name = "_reasonCard";
            this._reasonCard.Size = new System.Drawing.Size(956, 104);
            this._reasonCard.TabIndex = 4;
            // 
            // _reasonLabel
            // 
            this._reasonLabel.AutoSize = true;
            this._reasonLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._reasonLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._reasonLabel.Location = new System.Drawing.Point(18, 12);
            this._reasonLabel.Name = "_reasonLabel";
            this._reasonLabel.Size = new System.Drawing.Size(68, 25);
            this._reasonLabel.TabIndex = 0;
            this._reasonLabel.Text = "Lý do *";
            // 
            // _reasonComboBox
            // 
            this._reasonComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._reasonComboBox.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this._reasonComboBox.FormattingEnabled = true;
            this._reasonComboBox.Items.AddRange(new object[] {
            "Hết hạn",
            "Hỏng / hư hỏng",
            "Chất lượng",
            "Khác"});
            this._reasonComboBox.Location = new System.Drawing.Point(18, 34);
            this._reasonComboBox.Name = "_reasonComboBox";
            this._reasonComboBox.Size = new System.Drawing.Size(306, 36);
            this._reasonComboBox.TabIndex = 1;
            // 
            // _noteLabel
            // 
            this._noteLabel.AutoSize = true;
            this._noteLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._noteLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._noteLabel.Location = new System.Drawing.Point(336, 12);
            this._noteLabel.Name = "_noteLabel";
            this._noteLabel.Size = new System.Drawing.Size(71, 25);
            this._noteLabel.TabIndex = 2;
            this._noteLabel.Text = "Ghi chú";
            // 
            // _noteTextBox
            // 
            this._noteTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._noteTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._noteTextBox.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this._noteTextBox.Location = new System.Drawing.Point(336, 34);
            this._noteTextBox.MaxLength = 500;
            this._noteTextBox.Multiline = true;
            this._noteTextBox.Name = "_noteTextBox";
            this._noteTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._noteTextBox.Size = new System.Drawing.Size(600, 54);
            this._noteTextBox.TabIndex = 3;
            // 
            // frmStockDisposalEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(1004, 661);
            this.Controls.Add(this._bodyPanel);
            this.Controls.Add(this._footerPanel);
            this.Controls.Add(this._headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(900, 620);
            this.Name = "frmStockDisposalEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lập phiếu tiêu hủy";
            this._headerPanel.ResumeLayout(false);
            this._headerIconPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).EndInit();
            this._footerPanel.ResumeLayout(false);
            this._footerPanel.PerformLayout();
            this._bodyPanel.ResumeLayout(false);
            this._linesCard.ResumeLayout(false);
            this._linesCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._linesIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._linesGrid)).EndInit();
            this._entryCard.ResumeLayout(false);
            this._entryCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._entryIcon)).EndInit();
            this._reasonCard.ResumeLayout(false);
            this._reasonCard.PerformLayout();
            this.ResumeLayout(false);

        }

        private Panel _headerPanel;
        private Panel _headerIconPanel;
        private IconPictureBox _headerIcon;
        private Label _titleLabel;
        private Label _subtitleLabel;
        private Panel _headerLine;
        private Panel _footerPanel;
        private Label _totalCaptionLabel;
        private Label _totalValueLabel;
        private IconButton _cancelButton;
        private IconButton _saveButton;
        private Panel _footerLine;
        private Panel _bodyPanel;
        private Panel _linesCard;
        private IconPictureBox _linesIcon;
        private Label _linesTitle;
        private Label _lineCountLabel;
        private DataGridView _linesGrid;
        private DataGridViewTextBoxColumn colBatchCode;
        private DataGridViewTextBoxColumn colProduct;
        private DataGridViewTextBoxColumn colExpiry;
        private DataGridViewTextBoxColumn colQuantity;
        private DataGridViewTextBoxColumn colUnitCost;
        private DataGridViewTextBoxColumn colLoss;
        private DataGridViewImageColumn colRemove;
        private Label _emptyLabel;
        private Panel _gap2;
        private Panel _entryCard;
        private IconPictureBox _entryIcon;
        private Label _entryTitle;
        private Label _batchLabel;
        private ComboBox _batchComboBox;
        private Label _quantityLabel;
        private TextBox _quantityTextBox;
        private IconButton _addLineButton;
        private Label _remainHintLabel;
        private Panel _gap1;
        private Panel _reasonCard;
        private Label _reasonLabel;
        private ComboBox _reasonComboBox;
        private Label _noteLabel;
        private TextBox _noteTextBox;
    }
}

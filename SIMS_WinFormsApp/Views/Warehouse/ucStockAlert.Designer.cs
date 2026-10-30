using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Controls;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    partial class ucStockAlert
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this._headerCard = new System.Windows.Forms.Panel();
            this._headerIcon = new FontAwesome.Sharp.IconPictureBox();
            this._titleLabel = new System.Windows.Forms.Label();
            this._subtitleLabel = new System.Windows.Forms.Label();
            this._optionsButton = new System.Windows.Forms.Button();
            this._optionsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._exportCsvItem = new System.Windows.Forms.ToolStripMenuItem();
            this._exportExcelItem = new System.Windows.Forms.ToolStripMenuItem();
            this._filterCard = new System.Windows.Forms.Panel();
            this._searchBox = new System.Windows.Forms.TextBox();
            this._categoryCombo = new System.Windows.Forms.ComboBox();
            this._fromLabel = new System.Windows.Forms.Label();
            this._fromDate = new SIMS_WinFormsApp.UI.Controls.DateFilterBox();
            this._toLabel = new System.Windows.Forms.Label();
            this._toDate = new SIMS_WinFormsApp.UI.Controls.DateFilterBox();
            this._clearFilterLink = new System.Windows.Forms.LinkLabel();
            this._totalLabel = new System.Windows.Forms.Label();
            this._pagerPanel = new System.Windows.Forms.Panel();
            this._pagerFlow = new System.Windows.Forms.FlowLayoutPanel();
            this._prevButton = new System.Windows.Forms.Button();
            this._pageLabel = new System.Windows.Forms.Label();
            this._nextButton = new System.Windows.Forms.Button();
            this._pageInfoLabel = new System.Windows.Forms.Label();
            this._displayLabel = new System.Windows.Forms.Label();
            this._pageSizeCombo = new System.Windows.Forms.ComboBox();
            this._rowsLabel = new System.Windows.Forms.Label();
            this._pagerGap = new System.Windows.Forms.Panel();
            this._headerGap = new System.Windows.Forms.Panel();
            this._grid = new System.Windows.Forms.DataGridView();
            this.colAlertId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAlertType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReporter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreatedAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActions = new System.Windows.Forms.DataGridViewImageColumn();
            this._headerCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).BeginInit();
            this._optionsMenu.SuspendLayout();
            this._filterCard.SuspendLayout();
            this._pagerPanel.SuspendLayout();
            this._pagerFlow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this.SuspendLayout();
            // 
            // _headerCard
            // 
            this._headerCard.BackColor = System.Drawing.Color.White;
            this._headerCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._headerCard.Controls.Add(this._headerIcon);
            this._headerCard.Controls.Add(this._titleLabel);
            this._headerCard.Controls.Add(this._subtitleLabel);
            this._headerCard.Controls.Add(this._optionsButton);
            this._headerCard.Dock = System.Windows.Forms.DockStyle.Top;
            this._headerCard.Location = new System.Drawing.Point(30, 30);
            this._headerCard.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._headerCard.Name = "_headerCard";
            this._headerCard.Size = new System.Drawing.Size(1440, 120);
            this._headerCard.TabIndex = 0;
            // 
            // _headerIcon
            // 
            this._headerIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(240)))), ((int)(((byte)(254)))));
            this._headerIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._headerIcon.IconChar = FontAwesome.Sharp.IconChar.ExclamationTriangle;
            this._headerIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._headerIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._headerIcon.IconSize = 70;
            this._headerIcon.Location = new System.Drawing.Point(30, 25);
            this._headerIcon.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._headerIcon.Name = "_headerIcon";
            this._headerIcon.Size = new System.Drawing.Size(70, 70);
            this._headerIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._headerIcon.TabIndex = 0;
            this._headerIcon.TabStop = false;
            // 
            // _titleLabel
            // 
            this._titleLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this._titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._titleLabel.Location = new System.Drawing.Point(120, 22);
            this._titleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(625, 42);
            this._titleLabel.TabIndex = 1;
            this._titleLabel.Text = "Cảnh báo tồn kho";
            this._titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._titleLabel.UseMnemonic = false;
            // 
            // _subtitleLabel
            // 
            this._subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this._subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._subtitleLabel.Location = new System.Drawing.Point(120, 68);
            this._subtitleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._subtitleLabel.Name = "_subtitleLabel";
            this._subtitleLabel.Size = new System.Drawing.Size(950, 28);
            this._subtitleLabel.TabIndex = 2;
            this._subtitleLabel.Text = "Các báo cáo hết/sắp hết hàng từ nhân viên bán hàng · lên kế hoạch nhập hàng bổ su" +
    "ng";
            this._subtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._subtitleLabel.UseMnemonic = false;
            // 
            // _optionsButton
            // 
            this._optionsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._optionsButton.BackColor = System.Drawing.Color.White;
            this._optionsButton.ContextMenuStrip = this._optionsMenu;
            this._optionsButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._optionsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this._optionsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._optionsButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._optionsButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._optionsButton.Location = new System.Drawing.Point(1222, 35);
            this._optionsButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._optionsButton.Name = "_optionsButton";
            this._optionsButton.Size = new System.Drawing.Size(188, 50);
            this._optionsButton.TabIndex = 3;
            this._optionsButton.Text = "Tùy chọn ▾";
            this._optionsButton.UseVisualStyleBackColor = false;
            // 
            // _optionsMenu
            // 
            this._optionsMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._optionsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._exportCsvItem,
            this._exportExcelItem});
            this._optionsMenu.Name = "_optionsMenu";
            this._optionsMenu.Size = new System.Drawing.Size(147, 52);
            // 
            // _exportCsvItem
            // 
            this._exportCsvItem.Name = "_exportCsvItem";
            this._exportCsvItem.Size = new System.Drawing.Size(146, 24);
            this._exportCsvItem.Text = "Xuất CSV";
            // 
            // _exportExcelItem
            // 
            this._exportExcelItem.Name = "_exportExcelItem";
            this._exportExcelItem.Size = new System.Drawing.Size(146, 24);
            this._exportExcelItem.Text = "Xuất Excel";
            // 
            // _filterCard
            // 
            this._filterCard.BackColor = System.Drawing.Color.White;
            this._filterCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._filterCard.Controls.Add(this._searchBox);
            this._filterCard.Controls.Add(this._categoryCombo);
            this._filterCard.Controls.Add(this._fromLabel);
            this._filterCard.Controls.Add(this._fromDate);
            this._filterCard.Controls.Add(this._toLabel);
            this._filterCard.Controls.Add(this._toDate);
            this._filterCard.Controls.Add(this._clearFilterLink);
            this._filterCard.Controls.Add(this._totalLabel);
            this._filterCard.Dock = System.Windows.Forms.DockStyle.Top;
            this._filterCard.Location = new System.Drawing.Point(30, 165);
            this._filterCard.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._filterCard.Name = "_filterCard";
            this._filterCard.Size = new System.Drawing.Size(1440, 90);
            this._filterCard.TabIndex = 1;
            // 
            // _searchBox
            // 
            this._searchBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._searchBox.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this._searchBox.ForeColor = System.Drawing.Color.Gray;
            this._searchBox.Location = new System.Drawing.Point(25, 22);
            this._searchBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._searchBox.Name = "_searchBox";
            this._searchBox.Size = new System.Drawing.Size(400, 31);
            this._searchBox.TabIndex = 0;
            this._searchBox.Text = "Tìm theo tên sản phẩm, mã SP...";
            // 
            // _categoryCombo
            // 
            this._categoryCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._categoryCombo.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this._categoryCombo.FormattingEnabled = true;
            this._categoryCombo.Items.AddRange(new object[] {
            "Tất cả loại SP"});
            this._categoryCombo.Location = new System.Drawing.Point(445, 22);
            this._categoryCombo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._categoryCombo.Name = "_categoryCombo";
            this._categoryCombo.Size = new System.Drawing.Size(249, 31);
            this._categoryCombo.TabIndex = 1;
            // 
            // _fromLabel
            // 
            this._fromLabel.AutoSize = true;
            this._fromLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._fromLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._fromLabel.Location = new System.Drawing.Point(715, 34);
            this._fromLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._fromLabel.Name = "_fromLabel";
            this._fromLabel.Size = new System.Drawing.Size(29, 23);
            this._fromLabel.TabIndex = 2;
            this._fromLabel.Text = "Từ";
            this._fromLabel.UseMnemonic = false;
            // 
            // _fromDate
            // 
            this._fromDate.Location = new System.Drawing.Point(750, 22);
            this._fromDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._fromDate.MinimumSize = new System.Drawing.Size(125, 45);
            this._fromDate.Name = "_fromDate";
            this._fromDate.Size = new System.Drawing.Size(200, 45);
            this._fromDate.TabIndex = 3;
            this._fromDate.Value = null;
            // 
            // _toLabel
            // 
            this._toLabel.AutoSize = true;
            this._toLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._toLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._toLabel.Location = new System.Drawing.Point(965, 34);
            this._toLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._toLabel.Name = "_toLabel";
            this._toLabel.Size = new System.Drawing.Size(41, 23);
            this._toLabel.TabIndex = 4;
            this._toLabel.Text = "Đến";
            this._toLabel.UseMnemonic = false;
            // 
            // _toDate
            // 
            this._toDate.Location = new System.Drawing.Point(1008, 22);
            this._toDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._toDate.MinimumSize = new System.Drawing.Size(125, 45);
            this._toDate.Name = "_toDate";
            this._toDate.Size = new System.Drawing.Size(200, 45);
            this._toDate.TabIndex = 5;
            this._toDate.Value = null;
            // 
            // _clearFilterLink
            // 
            this._clearFilterLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this._clearFilterLink.AutoSize = true;
            this._clearFilterLink.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._clearFilterLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this._clearFilterLink.Location = new System.Drawing.Point(1225, 34);
            this._clearFilterLink.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._clearFilterLink.Name = "_clearFilterLink";
            this._clearFilterLink.Size = new System.Drawing.Size(59, 20);
            this._clearFilterLink.TabIndex = 6;
            this._clearFilterLink.TabStop = true;
            this._clearFilterLink.Text = "Xóa lọc";
            this._clearFilterLink.UseMnemonic = false;
            this._clearFilterLink.Visible = false;
            // 
            // _totalLabel
            // 
            this._totalLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._totalLabel.AutoSize = true;
            this._totalLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._totalLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._totalLabel.Location = new System.Drawing.Point(1255, 34);
            this._totalLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._totalLabel.Name = "_totalLabel";
            this._totalLabel.Size = new System.Drawing.Size(186, 23);
            this._totalLabel.TabIndex = 7;
            this._totalLabel.Text = "Tổng cộng: 0 cảnh báo";
            this._totalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._totalLabel.UseMnemonic = false;
            // 
            // _pagerPanel
            // 
            this._pagerPanel.BackColor = System.Drawing.Color.White;
            this._pagerPanel.Controls.Add(this._pagerFlow);
            this._pagerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._pagerPanel.Location = new System.Drawing.Point(30, 845);
            this._pagerPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._pagerPanel.Name = "_pagerPanel";
            this._pagerPanel.Size = new System.Drawing.Size(1440, 75);
            this._pagerPanel.TabIndex = 2;
            // 
            // _pagerFlow
            // 
            this._pagerFlow.Anchor = System.Windows.Forms.AnchorStyles.None;
            this._pagerFlow.AutoSize = true;
            this._pagerFlow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._pagerFlow.BackColor = System.Drawing.Color.White;
            this._pagerFlow.Controls.Add(this._prevButton);
            this._pagerFlow.Controls.Add(this._pageLabel);
            this._pagerFlow.Controls.Add(this._nextButton);
            this._pagerFlow.Controls.Add(this._pageInfoLabel);
            this._pagerFlow.Controls.Add(this._displayLabel);
            this._pagerFlow.Controls.Add(this._pageSizeCombo);
            this._pagerFlow.Controls.Add(this._rowsLabel);
            this._pagerFlow.Location = new System.Drawing.Point(488, 15);
            this._pagerFlow.Margin = new System.Windows.Forms.Padding(0);
            this._pagerFlow.Name = "_pagerFlow";
            this._pagerFlow.Size = new System.Drawing.Size(496, 45);
            this._pagerFlow.TabIndex = 0;
            this._pagerFlow.WrapContents = false;
            // 
            // _prevButton
            // 
            this._prevButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this._prevButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._prevButton.Font = new System.Drawing.Font("Segoe UI", 16F);
            this._prevButton.Location = new System.Drawing.Point(0, 0);
            this._prevButton.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this._prevButton.Name = "_prevButton";
            this._prevButton.Size = new System.Drawing.Size(45, 45);
            this._prevButton.TabIndex = 0;
            this._prevButton.Text = "‹";
            this._prevButton.UseVisualStyleBackColor = true;
            // 
            // _pageLabel
            // 
            this._pageLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._pageLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._pageLabel.ForeColor = System.Drawing.Color.White;
            this._pageLabel.Location = new System.Drawing.Point(53, 0);
            this._pageLabel.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this._pageLabel.Name = "_pageLabel";
            this._pageLabel.Size = new System.Drawing.Size(45, 45);
            this._pageLabel.TabIndex = 1;
            this._pageLabel.Text = "1";
            this._pageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._pageLabel.UseMnemonic = false;
            // 
            // _nextButton
            // 
            this._nextButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this._nextButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._nextButton.Font = new System.Drawing.Font("Segoe UI", 16F);
            this._nextButton.Location = new System.Drawing.Point(106, 0);
            this._nextButton.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this._nextButton.Name = "_nextButton";
            this._nextButton.Size = new System.Drawing.Size(45, 45);
            this._nextButton.TabIndex = 2;
            this._nextButton.Text = "›";
            this._nextButton.UseVisualStyleBackColor = true;
            // 
            // _pageInfoLabel
            // 
            this._pageInfoLabel.AutoSize = true;
            this._pageInfoLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._pageInfoLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._pageInfoLabel.Location = new System.Drawing.Point(186, 11);
            this._pageInfoLabel.Margin = new System.Windows.Forms.Padding(20, 11, 0, 0);
            this._pageInfoLabel.Name = "_pageInfoLabel";
            this._pageInfoLabel.Size = new System.Drawing.Size(80, 20);
            this._pageInfoLabel.TabIndex = 3;
            this._pageInfoLabel.Text = "Trang 1 / 1";
            this._pageInfoLabel.UseMnemonic = false;
            // 
            // _displayLabel
            // 
            this._displayLabel.AutoSize = true;
            this._displayLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._displayLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._displayLabel.Location = new System.Drawing.Point(286, 11);
            this._displayLabel.Margin = new System.Windows.Forms.Padding(20, 11, 8, 0);
            this._displayLabel.Name = "_displayLabel";
            this._displayLabel.Size = new System.Drawing.Size(64, 20);
            this._displayLabel.TabIndex = 4;
            this._displayLabel.Text = "Hiển thị:";
            this._displayLabel.UseMnemonic = false;
            // 
            // _pageSizeCombo
            // 
            this._pageSizeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._pageSizeCombo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._pageSizeCombo.Items.AddRange(new object[] {
            10,
            20,
            50});
            this._pageSizeCombo.Location = new System.Drawing.Point(358, 2);
            this._pageSizeCombo.Margin = new System.Windows.Forms.Padding(0, 2, 8, 0);
            this._pageSizeCombo.Name = "_pageSizeCombo";
            this._pageSizeCombo.Size = new System.Drawing.Size(86, 28);
            this._pageSizeCombo.TabIndex = 5;
            // 
            // _rowsLabel
            // 
            this._rowsLabel.AutoSize = true;
            this._rowsLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._rowsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._rowsLabel.Location = new System.Drawing.Point(452, 11);
            this._rowsLabel.Margin = new System.Windows.Forms.Padding(0, 11, 0, 0);
            this._rowsLabel.Name = "_rowsLabel";
            this._rowsLabel.Size = new System.Drawing.Size(44, 20);
            this._rowsLabel.TabIndex = 6;
            this._rowsLabel.Text = "dòng";
            this._rowsLabel.UseMnemonic = false;
            // 
            // _pagerGap
            // 
            this._pagerGap.BackColor = System.Drawing.Color.Transparent;
            this._pagerGap.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._pagerGap.Location = new System.Drawing.Point(30, 830);
            this._pagerGap.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._pagerGap.Name = "_pagerGap";
            this._pagerGap.Size = new System.Drawing.Size(1440, 15);
            this._pagerGap.TabIndex = 1;
            // 
            // _headerGap
            // 
            this._headerGap.BackColor = System.Drawing.Color.Transparent;
            this._headerGap.Dock = System.Windows.Forms.DockStyle.Top;
            this._headerGap.Location = new System.Drawing.Point(30, 150);
            this._headerGap.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._headerGap.Name = "_headerGap";
            this._headerGap.Size = new System.Drawing.Size(1440, 15);
            this._headerGap.TabIndex = 3;
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
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this._grid.ColumnHeadersHeight = 44;
            this._grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this._grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAlertId,
            this.colProductCode,
            this.colProductName,
            this.colAlertType,
            this.colReporter,
            this.colCreatedAt,
            this.colStatus,
            this.colActions});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._grid.DefaultCellStyle = dataGridViewCellStyle8;
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.EnableHeadersVisualStyles = false;
            this._grid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._grid.Location = new System.Drawing.Point(30, 255);
            this._grid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._grid.MultiSelect = false;
            this._grid.Name = "_grid";
            this._grid.ReadOnly = true;
            this._grid.RowHeadersVisible = false;
            this._grid.RowHeadersWidth = 51;
            this._grid.RowTemplate.Height = 44;
            this._grid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._grid.Size = new System.Drawing.Size(1440, 575);
            this._grid.TabIndex = 0;
            // 
            // colAlertId
            // 
            this.colAlertId.HeaderText = "AlertID";
            this.colAlertId.MinimumWidth = 6;
            this.colAlertId.Name = "colAlertId";
            this.colAlertId.ReadOnly = true;
            this.colAlertId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colAlertId.Visible = false;
            // 
            // colProductCode
            // 
            this.colProductCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.colProductCode.DefaultCellStyle = dataGridViewCellStyle2;
            this.colProductCode.FillWeight = 10F;
            this.colProductCode.HeaderText = "Mã SP";
            this.colProductCode.MinimumWidth = 6;
            this.colProductCode.Name = "colProductCode";
            this.colProductCode.ReadOnly = true;
            this.colProductCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colProductName
            // 
            this.colProductName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.colProductName.DefaultCellStyle = dataGridViewCellStyle3;
            this.colProductName.FillWeight = 24F;
            this.colProductName.HeaderText = "Tên sản phẩm";
            this.colProductName.MinimumWidth = 6;
            this.colProductName.Name = "colProductName";
            this.colProductName.ReadOnly = true;
            this.colProductName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colAlertType
            // 
            this.colAlertType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colAlertType.DefaultCellStyle = dataGridViewCellStyle4;
            this.colAlertType.FillWeight = 14F;
            this.colAlertType.HeaderText = "Loại cảnh báo";
            this.colAlertType.MinimumWidth = 6;
            this.colAlertType.Name = "colAlertType";
            this.colAlertType.ReadOnly = true;
            this.colAlertType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colReporter
            // 
            this.colReporter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.colReporter.DefaultCellStyle = dataGridViewCellStyle5;
            this.colReporter.FillWeight = 16F;
            this.colReporter.HeaderText = "Người báo cáo";
            this.colReporter.MinimumWidth = 6;
            this.colReporter.Name = "colReporter";
            this.colReporter.ReadOnly = true;
            this.colReporter.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colCreatedAt
            // 
            this.colCreatedAt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.Format = "dd/MM/yy HH:mm";
            dataGridViewCellStyle6.FormatProvider = new System.Globalization.CultureInfo("vi-VN");
            this.colCreatedAt.DefaultCellStyle = dataGridViewCellStyle6;
            this.colCreatedAt.FillWeight = 14F;
            this.colCreatedAt.HeaderText = "Thời gian";
            this.colCreatedAt.MinimumWidth = 6;
            this.colCreatedAt.Name = "colCreatedAt";
            this.colCreatedAt.ReadOnly = true;
            this.colCreatedAt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colStatus
            // 
            this.colStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colStatus.DefaultCellStyle = dataGridViewCellStyle7;
            this.colStatus.FillWeight = 14F;
            this.colStatus.HeaderText = "Trạng thái";
            this.colStatus.MinimumWidth = 6;
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colActions
            // 
            this.colActions.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colActions.HeaderText = "Thao tác";
            this.colActions.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.colActions.MinimumWidth = 6;
            this.colActions.Name = "colActions";
            this.colActions.ReadOnly = true;
            this.colActions.Width = 110;
            // 
            // ucStockAlert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.Controls.Add(this._grid);
            this.Controls.Add(this._pagerGap);
            this.Controls.Add(this._pagerPanel);
            this.Controls.Add(this._filterCard);
            this.Controls.Add(this._headerGap);
            this.Controls.Add(this._headerCard);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ucStockAlert";
            this.Padding = new System.Windows.Forms.Padding(30, 30, 30, 30);
            this.Size = new System.Drawing.Size(1500, 950);
            this._headerCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).EndInit();
            this._optionsMenu.ResumeLayout(false);
            this._filterCard.ResumeLayout(false);
            this._filterCard.PerformLayout();
            this._pagerPanel.ResumeLayout(false);
            this._pagerPanel.PerformLayout();
            this._pagerFlow.ResumeLayout(false);
            this._pagerFlow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this.ResumeLayout(false);

        }

        private IContainer components = null;
        private Panel _headerCard;
        private IconPictureBox _headerIcon;
        private Label _titleLabel;
        private Label _subtitleLabel;
        private Button _optionsButton;
        private Panel _headerGap;
        private Panel _filterCard;
        private TextBox _searchBox;
        private ComboBox _categoryCombo;
        private Label _fromLabel;
        private DateFilterBox _fromDate;
        private Label _toLabel;
        private DateFilterBox _toDate;
        private LinkLabel _clearFilterLink;
        private Label _totalLabel;
        private DataGridView _grid;
        private DataGridViewTextBoxColumn colAlertId;
        private DataGridViewTextBoxColumn colProductCode;
        private DataGridViewTextBoxColumn colProductName;
        private DataGridViewTextBoxColumn colAlertType;
        private DataGridViewTextBoxColumn colReporter;
        private DataGridViewTextBoxColumn colCreatedAt;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewImageColumn colActions;
        private Panel _pagerGap;
        private Panel _pagerPanel;
        private FlowLayoutPanel _pagerFlow;
        private Button _prevButton;
        private Label _pageLabel;
        private Button _nextButton;
        private Label _pageInfoLabel;
        private Label _displayLabel;
        private ComboBox _pageSizeCombo;
        private Label _rowsLabel;
        private ContextMenuStrip _optionsMenu;
        private ToolStripMenuItem _exportCsvItem;
        private ToolStripMenuItem _exportExcelItem;
    }
}
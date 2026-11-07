namespace SIMS_WinFormsApp.Views.Warehouse
{
    partial class frmStockReconciliationDetail
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._headerPanel = new System.Windows.Forms.Panel();
            this._headerLine = new System.Windows.Forms.Panel();
            this._headerIconPanel = new System.Windows.Forms.Panel();
            this._headerIcon = new FontAwesome.Sharp.IconPictureBox();
            this._titleLabel = new System.Windows.Forms.Label();
            this._subtitleLabel = new System.Windows.Forms.Label();
            this._statusLabel = new System.Windows.Forms.Label();
            this._bodyPanel = new System.Windows.Forms.Panel();
            this._infoCard = new System.Windows.Forms.Panel();
            this._productCodeCaption = new System.Windows.Forms.Label();
            this._productNameCaption = new System.Windows.Forms.Label();
            this._categoryCaption = new System.Windows.Forms.Label();
            this._sessionDateCaption = new System.Windows.Forms.Label();
            this._systemCaption = new System.Windows.Forms.Label();
            this._actualCaption = new System.Windows.Forms.Label();
            this._diffCaption = new System.Windows.Forms.Label();
            this._currentStockCaption = new System.Windows.Forms.Label();
            this._reasonCaption = new System.Windows.Forms.Label();
            this._createdByCaption = new System.Windows.Forms.Label();
            this._updatedAtCaption = new System.Windows.Forms.Label();
            this._productCodeValue = new System.Windows.Forms.Label();
            this._productNameValue = new System.Windows.Forms.Label();
            this._categoryValue = new System.Windows.Forms.Label();
            this._sessionDateValue = new System.Windows.Forms.Label();
            this._systemValue = new System.Windows.Forms.Label();
            this._actualValue = new System.Windows.Forms.Label();
            this._diffValue = new System.Windows.Forms.Label();
            this._currentStockValue = new System.Windows.Forms.Label();
            this._reasonValue = new System.Windows.Forms.Label();
            this._createdByValue = new System.Windows.Forms.Label();
            this._updatedAtValue = new System.Windows.Forms.Label();
            this._footerPanel = new System.Windows.Forms.Panel();
            this._footerLine = new System.Windows.Forms.Panel();
            this._footerCaptionLabel = new System.Windows.Forms.Label();
            this._footerDiffLabel = new System.Windows.Forms.Label();
            this._closeButton = new System.Windows.Forms.Button();
            this._headerPanel.SuspendLayout();
            this._headerIconPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._headerIcon)).BeginInit();
            this._bodyPanel.SuspendLayout();
            this._infoCard.SuspendLayout();
            this._footerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _headerPanel
            // 
            this._headerPanel.BackColor = System.Drawing.Color.White;
            this._headerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._headerPanel.Controls.Add(this._statusLabel);
            this._headerPanel.Controls.Add(this._subtitleLabel);
            this._headerPanel.Controls.Add(this._titleLabel);
            this._headerPanel.Controls.Add(this._headerIconPanel);
            this._headerPanel.Controls.Add(this._headerLine);
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._headerPanel.Location = new System.Drawing.Point(0, 0);
            this._headerPanel.Name = "_headerPanel";
            this._headerPanel.Size = new System.Drawing.Size(720, 80);
            this._headerPanel.TabIndex = 0;
            // 
            // _headerLine
            // 
            this._headerLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._headerLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._headerLine.Location = new System.Drawing.Point(0, 79);
            this._headerLine.Name = "_headerLine";
            this._headerLine.Size = new System.Drawing.Size(720, 1);
            this._headerLine.TabIndex = 0;
            // 
            // _headerIconPanel
            // 
            this._headerIconPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this._headerIconPanel.Controls.Add(this._headerIcon);
            this._headerIconPanel.Location = new System.Drawing.Point(24, 18);
            this._headerIconPanel.Name = "_headerIconPanel";
            this._headerIconPanel.Size = new System.Drawing.Size(44, 44);
            this._headerIconPanel.TabIndex = 0;
            // 
            // _headerIcon
            // 
            this._headerIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this._headerIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._headerIcon.IconChar = FontAwesome.Sharp.IconChar.ScaleBalanced;
            this._headerIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._headerIcon.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this._headerIcon.IconSize = 20;
            this._headerIcon.Location = new System.Drawing.Point(12, 12);
            this._headerIcon.Name = "_headerIcon";
            this._headerIcon.Size = new System.Drawing.Size(20, 20);
            this._headerIcon.TabIndex = 0;
            this._headerIcon.TabStop = false;
            // 
            // _titleLabel
            // 
            this._titleLabel.AutoSize = false;
            this._titleLabel.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this._titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._titleLabel.Location = new System.Drawing.Point(80, 14);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(480, 30);
            this._titleLabel.TabIndex = 1;
            this._titleLabel.Text = "Tên sản phẩm";
            this._titleLabel.UseMnemonic = false;
            // 
            // _subtitleLabel
            // 
            this._subtitleLabel.AutoSize = false;
            this._subtitleLabel.AutoEllipsis = true;
            this._subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this._subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._subtitleLabel.Location = new System.Drawing.Point(80, 44);
            this._subtitleLabel.Name = "_subtitleLabel";
            this._subtitleLabel.Size = new System.Drawing.Size(480, 20);
            this._subtitleLabel.TabIndex = 2;
            this._subtitleLabel.Text = "Sản phẩm  ·  Ngày phiên";
            this._subtitleLabel.UseMnemonic = false;
            // 
            // _statusLabel
            // 
            this._statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._statusLabel.AutoSize = false;
            this._statusLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this._statusLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this._statusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._statusLabel.Location = new System.Drawing.Point(590, 25);
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(106, 30);
            this._statusLabel.TabIndex = 3;
            this._statusLabel.Text = "Đang mở";
            this._statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _bodyPanel
            // 
            this._bodyPanel.BackColor = System.Drawing.Color.White;
            this._bodyPanel.Controls.Add(this._infoCard);
            this._bodyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._bodyPanel.Location = new System.Drawing.Point(0, 80);
            this._bodyPanel.Name = "_bodyPanel";
            this._bodyPanel.Padding = new System.Windows.Forms.Padding(24, 18, 24, 12);
            this._bodyPanel.Size = new System.Drawing.Size(720, 328);
            this._bodyPanel.TabIndex = 1;
            // 
            // _infoCard
            // 
            this._infoCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._infoCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._infoCard.Controls.Add(this._updatedAtValue);
            this._infoCard.Controls.Add(this._updatedAtCaption);
            this._infoCard.Controls.Add(this._createdByValue);
            this._infoCard.Controls.Add(this._createdByCaption);
            this._infoCard.Controls.Add(this._reasonValue);
            this._infoCard.Controls.Add(this._reasonCaption);
            this._infoCard.Controls.Add(this._currentStockValue);
            this._infoCard.Controls.Add(this._currentStockCaption);
            this._infoCard.Controls.Add(this._diffValue);
            this._infoCard.Controls.Add(this._diffCaption);
            this._infoCard.Controls.Add(this._actualValue);
            this._infoCard.Controls.Add(this._actualCaption);
            this._infoCard.Controls.Add(this._systemValue);
            this._infoCard.Controls.Add(this._systemCaption);
            this._infoCard.Controls.Add(this._sessionDateValue);
            this._infoCard.Controls.Add(this._sessionDateCaption);
            this._infoCard.Controls.Add(this._categoryValue);
            this._infoCard.Controls.Add(this._categoryCaption);
            this._infoCard.Controls.Add(this._productNameValue);
            this._infoCard.Controls.Add(this._productNameCaption);
            this._infoCard.Controls.Add(this._productCodeValue);
            this._infoCard.Controls.Add(this._productCodeCaption);
            this._infoCard.Location = new System.Drawing.Point(24, 18);
            this._infoCard.Name = "_infoCard";
            this._infoCard.Size = new System.Drawing.Size(672, 290);
            this._infoCard.TabIndex = 0;
            // 
            // _productCodeCaption
            // 
            this._productCodeCaption.AutoSize = true;
            this._productCodeCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._productCodeCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._productCodeCaption.Location = new System.Drawing.Point(18, 12);
            this._productCodeCaption.Name = "_productCodeCaption";
            this._productCodeCaption.Size = new System.Drawing.Size(77, 15);
            this._productCodeCaption.TabIndex = 0;
            this._productCodeCaption.Text = "Mã sản phẩm";
            // 
            // _productNameCaption
            // 
            this._productNameCaption.AutoSize = true;
            this._productNameCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._productNameCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._productNameCaption.Location = new System.Drawing.Point(350, 12);
            this._productNameCaption.Name = "_productNameCaption";
            this._productNameCaption.Size = new System.Drawing.Size(67, 15);
            this._productNameCaption.TabIndex = 1;
            this._productNameCaption.Text = "Sản phẩm";
            // 
            // _productCodeValue
            // 
            this._productCodeValue.AutoEllipsis = true;
            this._productCodeValue.AutoSize = false;
            this._productCodeValue.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this._productCodeValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._productCodeValue.Location = new System.Drawing.Point(18, 30);
            this._productCodeValue.Name = "_productCodeValue";
            this._productCodeValue.Size = new System.Drawing.Size(310, 22);
            this._productCodeValue.TabIndex = 22;
            this._productCodeValue.Text = "—";
            this._productCodeValue.UseMnemonic = false;
            // 
            // _productNameValue
            // 
            this._productNameValue.AutoEllipsis = true;
            this._productNameValue.AutoSize = false;
            this._productNameValue.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this._productNameValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._productNameValue.Location = new System.Drawing.Point(350, 30);
            this._productNameValue.Name = "_productNameValue";
            this._productNameValue.Size = new System.Drawing.Size(310, 22);
            this._productNameValue.TabIndex = 23;
            this._productNameValue.Text = "—";
            this._productNameValue.UseMnemonic = false;
            // 
            // _categoryCaption
            // 
            this._categoryCaption.AutoSize = true;
            this._categoryCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._categoryCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._categoryCaption.Location = new System.Drawing.Point(18, 58);
            this._categoryCaption.Name = "_categoryCaption";
            this._categoryCaption.Size = new System.Drawing.Size(68, 15);
            this._categoryCaption.TabIndex = 2;
            this._categoryCaption.Text = "Danh mục";
            // 
            // _sessionDateCaption
            // 
            this._sessionDateCaption.AutoSize = true;
            this._sessionDateCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._sessionDateCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._sessionDateCaption.Location = new System.Drawing.Point(350, 58);
            this._sessionDateCaption.Name = "_sessionDateCaption";
            this._sessionDateCaption.Size = new System.Drawing.Size(72, 15);
            this._sessionDateCaption.TabIndex = 3;
            this._sessionDateCaption.Text = "Ngày phiên";
            // 
            // _categoryValue
            // 
            this._categoryValue.AutoEllipsis = true;
            this._categoryValue.AutoSize = false;
            this._categoryValue.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this._categoryValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._categoryValue.Location = new System.Drawing.Point(18, 76);
            this._categoryValue.Name = "_categoryValue";
            this._categoryValue.Size = new System.Drawing.Size(310, 22);
            this._categoryValue.TabIndex = 24;
            this._categoryValue.Text = "—";
            this._categoryValue.UseMnemonic = false;
            // 
            // _sessionDateValue
            // 
            this._sessionDateValue.AutoEllipsis = true;
            this._sessionDateValue.AutoSize = false;
            this._sessionDateValue.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this._sessionDateValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._sessionDateValue.Location = new System.Drawing.Point(350, 76);
            this._sessionDateValue.Name = "_sessionDateValue";
            this._sessionDateValue.Size = new System.Drawing.Size(310, 22);
            this._sessionDateValue.TabIndex = 25;
            this._sessionDateValue.Text = "—";
            this._sessionDateValue.UseMnemonic = false;
            // 
            // _systemCaption
            // 
            this._systemCaption.AutoSize = true;
            this._systemCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._systemCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._systemCaption.Location = new System.Drawing.Point(18, 104);
            this._systemCaption.Name = "_systemCaption";
            this._systemCaption.Size = new System.Drawing.Size(154, 15);
            this._systemCaption.TabIndex = 4;
            this._systemCaption.Text = "Tồn kho (lúc lập phiên)";
            // 
            // _actualCaption
            // 
            this._actualCaption.AutoSize = true;
            this._actualCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._actualCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._actualCaption.Location = new System.Drawing.Point(350, 104);
            this._actualCaption.Name = "_actualCaption";
            this._actualCaption.Size = new System.Drawing.Size(76, 15);
            this._actualCaption.TabIndex = 5;
            this._actualCaption.Text = "Tồn thực tế";
            // 
            // _systemValue
            // 
            this._systemValue.AutoEllipsis = true;
            this._systemValue.AutoSize = false;
            this._systemValue.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this._systemValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._systemValue.Location = new System.Drawing.Point(18, 122);
            this._systemValue.Name = "_systemValue";
            this._systemValue.Size = new System.Drawing.Size(310, 22);
            this._systemValue.TabIndex = 26;
            this._systemValue.Text = "—";
            this._systemValue.UseMnemonic = false;
            // 
            // _actualValue
            // 
            this._actualValue.AutoEllipsis = true;
            this._actualValue.AutoSize = false;
            this._actualValue.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this._actualValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._actualValue.Location = new System.Drawing.Point(350, 122);
            this._actualValue.Name = "_actualValue";
            this._actualValue.Size = new System.Drawing.Size(310, 22);
            this._actualValue.TabIndex = 27;
            this._actualValue.Text = "—";
            this._actualValue.UseMnemonic = false;
            // 
            // _diffCaption
            // 
            this._diffCaption.AutoSize = true;
            this._diffCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._diffCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._diffCaption.Location = new System.Drawing.Point(18, 150);
            this._diffCaption.Name = "_diffCaption";
            this._diffCaption.Size = new System.Drawing.Size(73, 15);
            this._diffCaption.TabIndex = 6;
            this._diffCaption.Text = "Chênh lệch";
            // 
            // _currentStockCaption
            // 
            this._currentStockCaption.AutoSize = true;
            this._currentStockCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._currentStockCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._currentStockCaption.Location = new System.Drawing.Point(350, 150);
            this._currentStockCaption.Name = "_currentStockCaption";
            this._currentStockCaption.Size = new System.Drawing.Size(93, 15);
            this._currentStockCaption.TabIndex = 7;
            this._currentStockCaption.Text = "Tồn hiện tại";
            // 
            // _diffValue
            // 
            this._diffValue.AutoEllipsis = true;
            this._diffValue.AutoSize = false;
            this._diffValue.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this._diffValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._diffValue.Location = new System.Drawing.Point(18, 168);
            this._diffValue.Name = "_diffValue";
            this._diffValue.Size = new System.Drawing.Size(310, 22);
            this._diffValue.TabIndex = 28;
            this._diffValue.Text = "—";
            this._diffValue.UseMnemonic = false;
            // 
            // _currentStockValue
            // 
            this._currentStockValue.AutoEllipsis = true;
            this._currentStockValue.AutoSize = false;
            this._currentStockValue.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this._currentStockValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._currentStockValue.Location = new System.Drawing.Point(350, 168);
            this._currentStockValue.Name = "_currentStockValue";
            this._currentStockValue.Size = new System.Drawing.Size(310, 22);
            this._currentStockValue.TabIndex = 29;
            this._currentStockValue.Text = "—";
            this._currentStockValue.UseMnemonic = false;
            // 
            // _reasonCaption
            // 
            this._reasonCaption.AutoSize = true;
            this._reasonCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._reasonCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._reasonCaption.Location = new System.Drawing.Point(18, 196);
            this._reasonCaption.Name = "_reasonCaption";
            this._reasonCaption.Size = new System.Drawing.Size(58, 15);
            this._reasonCaption.TabIndex = 8;
            this._reasonCaption.Text = "Lý do";
            // 
            // _reasonValue
            // 
            this._reasonValue.AutoEllipsis = true;
            this._reasonValue.AutoSize = false;
            this._reasonValue.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this._reasonValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._reasonValue.Location = new System.Drawing.Point(18, 214);
            this._reasonValue.Name = "_reasonValue";
            this._reasonValue.Size = new System.Drawing.Size(640, 22);
            this._reasonValue.TabIndex = 30;
            this._reasonValue.Text = "—";
            this._reasonValue.UseMnemonic = false;
            // 
            // _createdByCaption
            // 
            this._createdByCaption.AutoSize = true;
            this._createdByCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._createdByCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._createdByCaption.Location = new System.Drawing.Point(18, 242);
            this._createdByCaption.Name = "_createdByCaption";
            this._createdByCaption.Size = new System.Drawing.Size(111, 15);
            this._createdByCaption.TabIndex = 9;
            this._createdByCaption.Text = "Người lập phiên";
            // 
            // _updatedAtCaption
            // 
            this._updatedAtCaption.AutoSize = true;
            this._updatedAtCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._updatedAtCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._updatedAtCaption.Location = new System.Drawing.Point(350, 242);
            this._updatedAtCaption.Name = "_updatedAtCaption";
            this._updatedAtCaption.Size = new System.Drawing.Size(119, 15);
            this._updatedAtCaption.TabIndex = 10;
            this._updatedAtCaption.Text = "Lần chỉnh gần nhất";
            // 
            // _createdByValue
            // 
            this._createdByValue.AutoEllipsis = true;
            this._createdByValue.AutoSize = false;
            this._createdByValue.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this._createdByValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._createdByValue.Location = new System.Drawing.Point(18, 260);
            this._createdByValue.Name = "_createdByValue";
            this._createdByValue.Size = new System.Drawing.Size(310, 22);
            this._createdByValue.TabIndex = 31;
            this._createdByValue.Text = "—";
            this._createdByValue.UseMnemonic = false;
            // 
            // _updatedAtValue
            // 
            this._updatedAtValue.AutoEllipsis = true;
            this._updatedAtValue.AutoSize = false;
            this._updatedAtValue.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this._updatedAtValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._updatedAtValue.Location = new System.Drawing.Point(350, 260);
            this._updatedAtValue.Name = "_updatedAtValue";
            this._updatedAtValue.Size = new System.Drawing.Size(310, 22);
            this._updatedAtValue.TabIndex = 32;
            this._updatedAtValue.Text = "—";
            this._updatedAtValue.UseMnemonic = false;
            // 
            // _footerPanel
            // 
            this._footerPanel.BackColor = System.Drawing.Color.White;
            this._footerPanel.Controls.Add(this._closeButton);
            this._footerPanel.Controls.Add(this._footerDiffLabel);
            this._footerPanel.Controls.Add(this._footerCaptionLabel);
            this._footerPanel.Controls.Add(this._footerLine);
            this._footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._footerPanel.Location = new System.Drawing.Point(0, 408);
            this._footerPanel.Name = "_footerPanel";
            this._footerPanel.Size = new System.Drawing.Size(720, 66);
            this._footerPanel.TabIndex = 2;
            // 
            // _footerLine
            // 
            this._footerLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._footerLine.Dock = System.Windows.Forms.DockStyle.Top;
            this._footerLine.Location = new System.Drawing.Point(0, 0);
            this._footerLine.Name = "_footerLine";
            this._footerLine.Size = new System.Drawing.Size(720, 1);
            this._footerLine.TabIndex = 0;
            // 
            // _footerCaptionLabel
            // 
            this._footerCaptionLabel.AutoSize = true;
            this._footerCaptionLabel.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this._footerCaptionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._footerCaptionLabel.Location = new System.Drawing.Point(24, 23);
            this._footerCaptionLabel.Name = "_footerCaptionLabel";
            this._footerCaptionLabel.Size = new System.Drawing.Size(90, 19);
            this._footerCaptionLabel.TabIndex = 1;
            this._footerCaptionLabel.Text = "Chênh lệch:";
            // 
            // _footerDiffLabel
            // 
            this._footerDiffLabel.AutoSize = false;
            this._footerDiffLabel.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this._footerDiffLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this._footerDiffLabel.Location = new System.Drawing.Point(122, 17);
            this._footerDiffLabel.Name = "_footerDiffLabel";
            this._footerDiffLabel.Size = new System.Drawing.Size(300, 30);
            this._footerDiffLabel.TabIndex = 2;
            this._footerDiffLabel.Text = "Khớp";
            this._footerDiffLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _closeButton
            // 
            this._closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._closeButton.BackColor = System.Drawing.Color.White;
            this._closeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this._closeButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this._closeButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._closeButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._closeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._closeButton.Location = new System.Drawing.Point(600, 14);
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(96, 38);
            this._closeButton.TabIndex = 3;
            this._closeButton.Text = "Đóng";
            this._closeButton.UseVisualStyleBackColor = false;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // frmStockReconciliationDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this._closeButton;
            this.ClientSize = new System.Drawing.Size(720, 474);
            this.Controls.Add(this._footerPanel);
            this.Controls.Add(this._bodyPanel);
            this.Controls.Add(this._headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmStockReconciliationDetail";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chi tiết đối chiếu kho";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel _headerPanel;
        private System.Windows.Forms.Panel _headerLine;
        private System.Windows.Forms.Panel _headerIconPanel;
        private FontAwesome.Sharp.IconPictureBox _headerIcon;
        private System.Windows.Forms.Label _titleLabel;
        private System.Windows.Forms.Label _subtitleLabel;
        private System.Windows.Forms.Label _statusLabel;
        private System.Windows.Forms.Panel _bodyPanel;
        private System.Windows.Forms.Panel _infoCard;
        private System.Windows.Forms.Label _productCodeCaption;
        private System.Windows.Forms.Label _productNameCaption;
        private System.Windows.Forms.Label _categoryCaption;
        private System.Windows.Forms.Label _sessionDateCaption;
        private System.Windows.Forms.Label _systemCaption;
        private System.Windows.Forms.Label _actualCaption;
        private System.Windows.Forms.Label _diffCaption;
        private System.Windows.Forms.Label _currentStockCaption;
        private System.Windows.Forms.Label _reasonCaption;
        private System.Windows.Forms.Label _createdByCaption;
        private System.Windows.Forms.Label _updatedAtCaption;
        private System.Windows.Forms.Label _productCodeValue;
        private System.Windows.Forms.Label _productNameValue;
        private System.Windows.Forms.Label _categoryValue;
        private System.Windows.Forms.Label _sessionDateValue;
        private System.Windows.Forms.Label _systemValue;
        private System.Windows.Forms.Label _actualValue;
        private System.Windows.Forms.Label _diffValue;
        private System.Windows.Forms.Label _currentStockValue;
        private System.Windows.Forms.Label _reasonValue;
        private System.Windows.Forms.Label _createdByValue;
        private System.Windows.Forms.Label _updatedAtValue;
        private System.Windows.Forms.Panel _footerPanel;
        private System.Windows.Forms.Panel _footerLine;
        private System.Windows.Forms.Label _footerCaptionLabel;
        private System.Windows.Forms.Label _footerDiffLabel;
        private System.Windows.Forms.Button _closeButton;
    }
}

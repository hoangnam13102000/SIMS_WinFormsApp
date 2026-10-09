using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Controls.Pos
{
    partial class PosPageControl
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel _root;
        private Panel _header;
        private Label _pageTitle;
        private Label _cashierNameLabel;
        private SplitContainer _body;
        private Panel _catalogCard;
        private TableLayoutPanel _catalogLayout;
        private Label _catalogTitle;
        private TableLayoutPanel _searchLayout;
        private TextBox _productSearch;
        private ComboBox _productCategory;
        private Button _scanBarcodeButton;
        private FlowLayoutPanel _productCardsPanel;
        private Panel _saleCard;
        private TableLayoutPanel _saleLayout;
        private SplitContainer _cartCheckoutSplit;
        private TableLayoutPanel _cartAreaLayout;
        private Panel _checkoutScrollPanel;
        private TableLayoutPanel _checkoutLayout;
        private Label _customerLabel;
        private TableLayoutPanel _customerLayout;
        private TextBox _customerTextBox;
        private Button _customerSearchButton;
        private Button _pickCustomerButton;
        private Button _clearCustomerButton;
        private CheckBox _usePointsCheckBox;
        private Label _cartLabel;
        private DataGridView _cartGrid;
        private DataGridViewTextBoxColumn _cartQuantity;
        private DataGridViewButtonColumn _cartRemove;
        private TableLayoutPanel _promoLayout;
        private Label _promoLabel;
        private TextBox _promoTextBox;
        private Button _applyPromoButton;
        private TableLayoutPanel _totalsLayout;
        private Label _subtotalCaption;
        private Label _subtotalValue;
        private Label _discountCaption;
        private Label _discountValue;
        private Label _vatCaption;
        private Label _vatValue;
        private Label _pointsCaption;
        private Label _pointsValue;
        private Label _grandTotalCaption;
        private Label _grandTotalValue;
        private TableLayoutPanel _paymentLayout;
        private Label _paymentCaption;
        private ComboBox _paymentMethod;
        private TableLayoutPanel _actionsLayout;
        private Button _holdButton;
        private Button _heldCartsButton;
        private Button _checkoutButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this._root = new System.Windows.Forms.TableLayoutPanel();
            this._header = new System.Windows.Forms.Panel();
            this._cashierNameLabel = new System.Windows.Forms.Label();
            this._pageTitle = new System.Windows.Forms.Label();
            this._body = new System.Windows.Forms.SplitContainer();
            this._catalogCard = new System.Windows.Forms.Panel();
            this._catalogLayout = new System.Windows.Forms.TableLayoutPanel();
            this._catalogTitle = new System.Windows.Forms.Label();
            this._searchLayout = new System.Windows.Forms.TableLayoutPanel();
            this._productSearch = new System.Windows.Forms.TextBox();
            this._productCategory = new System.Windows.Forms.ComboBox();
            this._scanBarcodeButton = new System.Windows.Forms.Button();
            this._productCardsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._saleCard = new System.Windows.Forms.Panel();
            this._saleLayout = new System.Windows.Forms.TableLayoutPanel();
            this._customerLayout = new System.Windows.Forms.TableLayoutPanel();
            this._customerLabel = new System.Windows.Forms.Label();
            this._customerTextBox = new System.Windows.Forms.TextBox();
            this._customerSearchButton = new System.Windows.Forms.Button();
            this._pickCustomerButton = new System.Windows.Forms.Button();
            this._clearCustomerButton = new System.Windows.Forms.Button();
            this._usePointsCheckBox = new System.Windows.Forms.CheckBox();
            this._cartCheckoutSplit = new System.Windows.Forms.SplitContainer();
            this._cartAreaLayout = new System.Windows.Forms.TableLayoutPanel();
            this._cartLabel = new System.Windows.Forms.Label();
            this._cartGrid = new System.Windows.Forms.DataGridView();
            this.CartProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CartUnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CartQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CartLineTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CartRemove = new System.Windows.Forms.DataGridViewButtonColumn();
            this._cartQuantity = this.CartQuantity;
            this._cartRemove = this.CartRemove;
            this._checkoutScrollPanel = new System.Windows.Forms.Panel();
            this._checkoutLayout = new System.Windows.Forms.TableLayoutPanel();
            this._promoLayout = new System.Windows.Forms.TableLayoutPanel();
            this._promoLabel = new System.Windows.Forms.Label();
            this._promoTextBox = new System.Windows.Forms.TextBox();
            this._applyPromoButton = new System.Windows.Forms.Button();
            this._totalsLayout = new System.Windows.Forms.TableLayoutPanel();
            this._subtotalCaption = new System.Windows.Forms.Label();
            this._subtotalValue = new System.Windows.Forms.Label();
            this._discountCaption = new System.Windows.Forms.Label();
            this._discountValue = new System.Windows.Forms.Label();
            this._vatCaption = new System.Windows.Forms.Label();
            this._vatValue = new System.Windows.Forms.Label();
            this._pointsCaption = new System.Windows.Forms.Label();
            this._pointsValue = new System.Windows.Forms.Label();
            this._grandTotalCaption = new System.Windows.Forms.Label();
            this._grandTotalValue = new System.Windows.Forms.Label();
            this._paymentLayout = new System.Windows.Forms.TableLayoutPanel();
            this._paymentCaption = new System.Windows.Forms.Label();
            this._paymentMethod = new System.Windows.Forms.ComboBox();
            this._actionsLayout = new System.Windows.Forms.TableLayoutPanel();
            this._holdButton = new System.Windows.Forms.Button();
            this._heldCartsButton = new System.Windows.Forms.Button();
            this._checkoutButton = new System.Windows.Forms.Button();
            this._root.SuspendLayout();
            this._header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._body)).BeginInit();
            this._body.Panel1.SuspendLayout();
            this._body.Panel2.SuspendLayout();
            this._body.SuspendLayout();
            this._catalogCard.SuspendLayout();
            this._catalogLayout.SuspendLayout();
            this._searchLayout.SuspendLayout();
            this._saleCard.SuspendLayout();
            this._saleLayout.SuspendLayout();
            this._customerLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._cartCheckoutSplit)).BeginInit();
            this._cartCheckoutSplit.Panel1.SuspendLayout();
            this._cartCheckoutSplit.Panel2.SuspendLayout();
            this._cartCheckoutSplit.SuspendLayout();
            this._cartAreaLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._cartGrid)).BeginInit();
            this._checkoutScrollPanel.SuspendLayout();
            this._checkoutLayout.SuspendLayout();
            this._promoLayout.SuspendLayout();
            this._totalsLayout.SuspendLayout();
            this._paymentLayout.SuspendLayout();
            this._actionsLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // _root
            // 
            this._root.ColumnCount = 1;
            this._root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Controls.Add(this._header, 0, 0);
            this._root.Controls.Add(this._body, 0, 1);
            this._root.Dock = System.Windows.Forms.DockStyle.Fill;
            this._root.Location = new System.Drawing.Point(0, 0);
            this._root.Name = "_root";
            this._root.Padding = new System.Windows.Forms.Padding(20);
            this._root.RowCount = 2;
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Size = new System.Drawing.Size(1180, 760);
            this._root.TabIndex = 0;
            // 
            // _header
            // 
            this._header.BackColor = System.Drawing.Color.White;
            this._header.Controls.Add(this._cashierNameLabel);
            this._header.Controls.Add(this._pageTitle);
            this._header.Dock = System.Windows.Forms.DockStyle.Fill;
            this._header.Location = new System.Drawing.Point(23, 23);
            this._header.Name = "_header";
            this._header.Size = new System.Drawing.Size(1134, 70);
            this._header.TabIndex = 0;
            // 
            // _cashierNameLabel
            // 
            this._cashierNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cashierNameLabel.AutoSize = true;
            this._cashierNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._cashierNameLabel.Location = new System.Drawing.Point(1584, 28);
            this._cashierNameLabel.Name = "_cashierNameLabel";
            this._cashierNameLabel.Size = new System.Drawing.Size(83, 20);
            this._cashierNameLabel.TabIndex = 0;
            this._cashierNameLabel.Text = "Nhân viên:";
            // 
            // _pageTitle
            // 
            this._pageTitle.AutoSize = true;
            this._pageTitle.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold);
            this._pageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._pageTitle.Location = new System.Drawing.Point(8, 4);
            this._pageTitle.Name = "_pageTitle";
            this._pageTitle.Size = new System.Drawing.Size(383, 57);
            this._pageTitle.TabIndex = 1;
            this._pageTitle.Text = "Bán hàng tại quầy";
            // 
            // _body
            // 
            this._body.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this._body.Dock = System.Windows.Forms.DockStyle.Fill;
            this._body.Location = new System.Drawing.Point(20, 110);
            this._body.Margin = new System.Windows.Forms.Padding(0, 14, 0, 0);
            this._body.Name = "_body";
            // 
            // _body.Panel1
            // 
            this._body.Panel1.Controls.Add(this._catalogCard);
            this._body.Panel1MinSize = 360;
            // 
            // _body.Panel2
            // 
            this._body.Panel2.Controls.Add(this._saleCard);
            this._body.Panel2MinSize = 320;
            this._body.Size = new System.Drawing.Size(1140, 630);
            this._body.SplitterDistance = 720;
            this._body.SplitterWidth = 8;
            this._body.TabIndex = 1;
            this._body.TabStop = false;
            // 
            // _catalogCard
            // 
            this._catalogCard.BackColor = System.Drawing.Color.White;
            this._catalogCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._catalogCard.Controls.Add(this._catalogLayout);
            this._catalogCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._catalogCard.Location = new System.Drawing.Point(0, 0);
            this._catalogCard.Margin = new System.Windows.Forms.Padding(0);
            this._catalogCard.Name = "_catalogCard";
            this._catalogCard.Padding = new System.Windows.Forms.Padding(12);
            this._catalogCard.Size = new System.Drawing.Size(720, 630);
            this._catalogCard.TabIndex = 0;
            // 
            // _catalogLayout
            // 
            this._catalogLayout.ColumnCount = 1;
            this._catalogLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._catalogLayout.Controls.Add(this._catalogTitle, 0, 0);
            this._catalogLayout.Controls.Add(this._searchLayout, 0, 1);
            this._catalogLayout.Controls.Add(this._productCardsPanel, 0, 2);
            this._catalogLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._catalogLayout.Location = new System.Drawing.Point(12, 12);
            this._catalogLayout.Name = "_catalogLayout";
            this._catalogLayout.RowCount = 3;
            this._catalogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this._catalogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this._catalogLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._catalogLayout.Size = new System.Drawing.Size(694, 604);
            this._catalogLayout.TabIndex = 0;
            // 
            // _catalogTitle
            // 
            this._catalogTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this._catalogTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._catalogTitle.Location = new System.Drawing.Point(3, 0);
            this._catalogTitle.Name = "_catalogTitle";
            this._catalogTitle.Size = new System.Drawing.Size(688, 28);
            this._catalogTitle.TabIndex = 0;
            this._catalogTitle.Text = "Danh sách sản phẩm";
            this._catalogTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _searchLayout
            // 
            this._searchLayout.ColumnCount = 3;
            this._searchLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._searchLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this._searchLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this._searchLayout.Controls.Add(this._productSearch, 0, 0);
            this._searchLayout.Controls.Add(this._productCategory, 1, 0);
            this._searchLayout.Controls.Add(this._scanBarcodeButton, 2, 0);
            this._searchLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._searchLayout.Location = new System.Drawing.Point(3, 31);
            this._searchLayout.Name = "_searchLayout";
            this._searchLayout.RowCount = 1;
            this._searchLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._searchLayout.Size = new System.Drawing.Size(688, 38);
            this._searchLayout.TabIndex = 1;
            // 
            // _productSearch
            // 
            this._productSearch.AccessibleName = "Tìm sản phẩm theo tên hoặc danh mục";
            this._productSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this._productSearch.Location = new System.Drawing.Point(0, 4);
            this._productSearch.Margin = new System.Windows.Forms.Padding(0, 4, 8, 4);
            this._productSearch.Name = "_productSearch";
            this._productSearch.Size = new System.Drawing.Size(408, 26);
            this._productSearch.TabIndex = 0;
            // 
            // _productCategory
            // 
            this._productCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this._productCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._productCategory.Items.AddRange(new object[] {
            "Tất cả danh mục"});
            this._productCategory.Location = new System.Drawing.Point(416, 4);
            this._productCategory.Margin = new System.Windows.Forms.Padding(0, 4, 8, 4);
            this._productCategory.Name = "_productCategory";
            this._productCategory.Size = new System.Drawing.Size(172, 28);
            this._productCategory.TabIndex = 1;
            // 
            // _scanBarcodeButton
            // 
            this._scanBarcodeButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._scanBarcodeButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._scanBarcodeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._scanBarcodeButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._scanBarcodeButton.Location = new System.Drawing.Point(596, 4);
            this._scanBarcodeButton.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this._scanBarcodeButton.Name = "_scanBarcodeButton";
            this._scanBarcodeButton.Size = new System.Drawing.Size(92, 30);
            this._scanBarcodeButton.TabIndex = 2;
            this._scanBarcodeButton.Text = "Quét mã";
            this._scanBarcodeButton.UseVisualStyleBackColor = true;
            // 
            // _productCardsPanel
            // 
            this._productCardsPanel.AutoScroll = true;
            this._productCardsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._productCardsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._productCardsPanel.Location = new System.Drawing.Point(3, 75);
            this._productCardsPanel.Name = "_productCardsPanel";
            this._productCardsPanel.Padding = new System.Windows.Forms.Padding(8);
            this._productCardsPanel.Size = new System.Drawing.Size(688, 526);
            this._productCardsPanel.TabIndex = 2;
            // 
            // _saleCard
            // 
            this._saleCard.BackColor = System.Drawing.Color.White;
            this._saleCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._saleCard.Controls.Add(this._saleLayout);
            this._saleCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._saleCard.Location = new System.Drawing.Point(0, 0);
            this._saleCard.Margin = new System.Windows.Forms.Padding(0);
            this._saleCard.Name = "_saleCard";
            this._saleCard.Padding = new System.Windows.Forms.Padding(12);
            this._saleCard.Size = new System.Drawing.Size(412, 630);
            this._saleCard.TabIndex = 0;
            // 
            // _saleLayout
            // 
            this._saleLayout.ColumnCount = 1;
            this._saleLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._saleLayout.Controls.Add(this._customerLayout, 0, 0);
            this._saleLayout.Controls.Add(this._cartCheckoutSplit, 0, 1);
            this._saleLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._saleLayout.Location = new System.Drawing.Point(12, 12);
            this._saleLayout.Name = "_saleLayout";
            this._saleLayout.RowCount = 2;
            this._saleLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this._saleLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._saleLayout.Size = new System.Drawing.Size(386, 604);
            this._saleLayout.TabIndex = 0;
            // 
            // _customerLayout
            // 
            this._customerLayout.ColumnCount = 4;
            this._customerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._customerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this._customerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this._customerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this._customerLayout.Controls.Add(this._customerLabel, 0, 0);
            this._customerLayout.Controls.Add(this._customerTextBox, 0, 1);
            this._customerLayout.Controls.Add(this._customerSearchButton, 1, 1);
            this._customerLayout.Controls.Add(this._pickCustomerButton, 2, 1);
            this._customerLayout.Controls.Add(this._clearCustomerButton, 3, 1);
            this._customerLayout.Controls.Add(this._usePointsCheckBox, 0, 2);
            this._customerLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._customerLayout.Location = new System.Drawing.Point(3, 3);
            this._customerLayout.Name = "_customerLayout";
            this._customerLayout.RowCount = 3;
            this._customerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this._customerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._customerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._customerLayout.Size = new System.Drawing.Size(380, 82);
            this._customerLayout.TabIndex = 0;
            // 
            // _customerLabel
            // 
            this._customerLabel.AutoSize = true;
            this._customerLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._customerLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._customerLabel.Location = new System.Drawing.Point(3, 0);
            this._customerLabel.Name = "_customerLabel";
            this._customerLabel.Size = new System.Drawing.Size(248, 22);
            this._customerLabel.TabIndex = 0;
            this._customerLabel.Text = "Khách hàng";
            // 
            // _customerTextBox
            // 
            this._customerTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._customerTextBox.Location = new System.Drawing.Point(0, 24);
            this._customerTextBox.Margin = new System.Windows.Forms.Padding(0, 2, 6, 2);
            this._customerTextBox.Name = "_customerTextBox";
            this._customerTextBox.Size = new System.Drawing.Size(248, 26);
            this._customerTextBox.TabIndex = 1;
            // 
            // _customerSearchButton
            // 
            this._customerSearchButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._customerSearchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._customerSearchButton.Location = new System.Drawing.Point(254, 24);
            this._customerSearchButton.Margin = new System.Windows.Forms.Padding(0, 2, 4, 2);
            this._customerSearchButton.Name = "_customerSearchButton";
            this._customerSearchButton.Size = new System.Drawing.Size(38, 26);
            this._customerSearchButton.TabIndex = 2;
            this._customerSearchButton.Text = "Tìm";
            // 
            // _pickCustomerButton
            // 
            this._pickCustomerButton.AccessibleName = "Chọn khách hàng";
            this._pickCustomerButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pickCustomerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._pickCustomerButton.Location = new System.Drawing.Point(296, 24);
            this._pickCustomerButton.Margin = new System.Windows.Forms.Padding(0, 2, 4, 2);
            this._pickCustomerButton.Name = "_pickCustomerButton";
            this._pickCustomerButton.Size = new System.Drawing.Size(38, 26);
            this._pickCustomerButton.TabIndex = 3;
            this._pickCustomerButton.Text = "...";
            // 
            // _clearCustomerButton
            // 
            this._clearCustomerButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._clearCustomerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._clearCustomerButton.Location = new System.Drawing.Point(338, 24);
            this._clearCustomerButton.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this._clearCustomerButton.Name = "_clearCustomerButton";
            this._clearCustomerButton.Size = new System.Drawing.Size(42, 26);
            this._clearCustomerButton.TabIndex = 4;
            this._clearCustomerButton.Text = "Xóa";
            // 
            // _usePointsCheckBox
            // 
            this._usePointsCheckBox.AutoSize = true;
            this._customerLayout.SetColumnSpan(this._usePointsCheckBox, 4);
            this._usePointsCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._usePointsCheckBox.Location = new System.Drawing.Point(3, 55);
            this._usePointsCheckBox.Name = "_usePointsCheckBox";
            this._usePointsCheckBox.Size = new System.Drawing.Size(374, 24);
            this._usePointsCheckBox.TabIndex = 5;
            this._usePointsCheckBox.Text = "Dùng điểm thưởng";
            this._usePointsCheckBox.Visible = false;
            // 
            // _cartCheckoutSplit
            // 
            this._cartCheckoutSplit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this._cartCheckoutSplit.Cursor = System.Windows.Forms.Cursors.HSplit;
            this._cartCheckoutSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cartCheckoutSplit.Location = new System.Drawing.Point(3, 91);
            this._cartCheckoutSplit.Name = "_cartCheckoutSplit";
            this._cartCheckoutSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _cartCheckoutSplit.Panel1
            // 
            this._cartCheckoutSplit.Panel1.Controls.Add(this._cartAreaLayout);
            this._cartCheckoutSplit.Panel1MinSize = 90;
            // 
            // _cartCheckoutSplit.Panel2
            // 
            this._cartCheckoutSplit.Panel2.Controls.Add(this._checkoutScrollPanel);
            this._cartCheckoutSplit.Panel2MinSize = 100;
            this._cartCheckoutSplit.Size = new System.Drawing.Size(380, 510);
            this._cartCheckoutSplit.SplitterDistance = 189;
            this._cartCheckoutSplit.SplitterWidth = 10;
            this._cartCheckoutSplit.TabIndex = 1;
            // 
            // _cartAreaLayout
            // 
            this._cartAreaLayout.ColumnCount = 1;
            this._cartAreaLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._cartAreaLayout.Controls.Add(this._cartLabel, 0, 0);
            this._cartAreaLayout.Controls.Add(this._cartGrid, 0, 1);
            this._cartAreaLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cartAreaLayout.Location = new System.Drawing.Point(0, 0);
            this._cartAreaLayout.Name = "_cartAreaLayout";
            this._cartAreaLayout.RowCount = 2;
            this._cartAreaLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._cartAreaLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._cartAreaLayout.Size = new System.Drawing.Size(380, 189);
            this._cartAreaLayout.TabIndex = 0;
            // 
            // _cartLabel
            // 
            this._cartLabel.AutoSize = true;
            this._cartLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cartLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._cartLabel.Location = new System.Drawing.Point(3, 0);
            this._cartLabel.Name = "_cartLabel";
            this._cartLabel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this._cartLabel.Size = new System.Drawing.Size(374, 30);
            this._cartLabel.TabIndex = 0;
            this._cartLabel.Text = "Giỏ hàng";
            // 
            // _cartGrid
            // 
            this._cartGrid.AllowUserToAddRows = false;
            this._cartGrid.AllowUserToDeleteRows = false;
            this._cartGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._cartGrid.BackgroundColor = System.Drawing.Color.White;
            this._cartGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._cartGrid.ColumnHeadersHeight = 34;
            this._cartGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CartProductName,
            this.CartUnitPrice,
            this.CartQuantity,
            this.CartLineTotal,
            this.CartRemove});
            this._cartGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cartGrid.Location = new System.Drawing.Point(0, 30);
            this._cartGrid.Margin = new System.Windows.Forms.Padding(0);
            this._cartGrid.MultiSelect = false;
            this._cartGrid.Name = "_cartGrid";
            this._cartGrid.RowHeadersVisible = false;
            this._cartGrid.RowHeadersWidth = 62;
            this._cartGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._cartGrid.Size = new System.Drawing.Size(380, 159);
            this._cartGrid.TabIndex = 1;
            // 
            // CartProductName
            // 
            this.CartProductName.FillWeight = 30F;
            this.CartProductName.HeaderText = "Sản phẩm";
            this.CartProductName.MinimumWidth = 8;
            this.CartProductName.Name = "CartProductName";
            this.CartProductName.ReadOnly = true;
            // 
            // CartUnitPrice
            // 
            this.CartUnitPrice.FillWeight = 20F;
            this.CartUnitPrice.HeaderText = "Đơn giá";
            this.CartUnitPrice.MinimumWidth = 8;
            this.CartUnitPrice.Name = "CartUnitPrice";
            this.CartUnitPrice.ReadOnly = true;
            // 
            // CartQuantity
            // 
            this.CartQuantity.FillWeight = 12F;
            this.CartQuantity.HeaderText = "SL";
            this.CartQuantity.MinimumWidth = 8;
            this.CartQuantity.Name = "CartQuantity";
            // 
            // CartLineTotal
            // 
            this.CartLineTotal.FillWeight = 22F;
            this.CartLineTotal.HeaderText = "Thành tiền";
            this.CartLineTotal.MinimumWidth = 8;
            this.CartLineTotal.Name = "CartLineTotal";
            this.CartLineTotal.ReadOnly = true;
            // 
            // CartRemove
            // 
            this.CartRemove.FillWeight = 16F;
            this.CartRemove.HeaderText = "";
            this.CartRemove.MinimumWidth = 8;
            this.CartRemove.Name = "CartRemove";
            this.CartRemove.Text = "Xóa";
            this.CartRemove.UseColumnTextForButtonValue = true;
            // 
            // _checkoutScrollPanel
            // 
            this._checkoutScrollPanel.AutoScroll = true;
            this._checkoutScrollPanel.AutoScrollMinSize = new System.Drawing.Size(0, 314);
            this._checkoutScrollPanel.BackColor = System.Drawing.Color.White;
            this._checkoutScrollPanel.Controls.Add(this._checkoutLayout);
            this._checkoutScrollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._checkoutScrollPanel.Location = new System.Drawing.Point(0, 0);
            this._checkoutScrollPanel.Name = "_checkoutScrollPanel";
            this._checkoutScrollPanel.Size = new System.Drawing.Size(380, 311);
            this._checkoutScrollPanel.TabIndex = 0;
            // 
            // _checkoutLayout
            // 
            this._checkoutLayout.ColumnCount = 1;
            this._checkoutLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._checkoutLayout.Controls.Add(this._promoLayout, 0, 0);
            this._checkoutLayout.Controls.Add(this._totalsLayout, 0, 1);
            this._checkoutLayout.Controls.Add(this._paymentLayout, 0, 2);
            this._checkoutLayout.Controls.Add(this._actionsLayout, 0, 3);
            this._checkoutLayout.Dock = System.Windows.Forms.DockStyle.Top;
            this._checkoutLayout.Location = new System.Drawing.Point(0, 0);
            this._checkoutLayout.Margin = new System.Windows.Forms.Padding(0);
            this._checkoutLayout.MinimumSize = new System.Drawing.Size(0, 314);
            this._checkoutLayout.Name = "_checkoutLayout";
            this._checkoutLayout.RowCount = 4;
            this._checkoutLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this._checkoutLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._checkoutLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this._checkoutLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this._checkoutLayout.Size = new System.Drawing.Size(354, 314);
            this._checkoutLayout.TabIndex = 0;
            // 
            // _promoLayout
            // 
            this._promoLayout.ColumnCount = 3;
            this._promoLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this._promoLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._promoLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this._promoLayout.Controls.Add(this._promoLabel, 0, 0);
            this._promoLayout.Controls.Add(this._promoTextBox, 1, 0);
            this._promoLayout.Controls.Add(this._applyPromoButton, 2, 0);
            this._promoLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._promoLayout.Location = new System.Drawing.Point(3, 3);
            this._promoLayout.Name = "_promoLayout";
            this._promoLayout.RowCount = 1;
            this._promoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._promoLayout.Size = new System.Drawing.Size(348, 36);
            this._promoLayout.TabIndex = 0;
            // 
            // _promoLabel
            // 
            this._promoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._promoLabel.Location = new System.Drawing.Point(3, 0);
            this._promoLabel.Name = "_promoLabel";
            this._promoLabel.Size = new System.Drawing.Size(58, 36);
            this._promoLabel.TabIndex = 0;
            this._promoLabel.Text = "Mã KM";
            this._promoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _promoTextBox
            // 
            this._promoTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._promoTextBox.Location = new System.Drawing.Point(64, 4);
            this._promoTextBox.Margin = new System.Windows.Forms.Padding(0, 4, 6, 4);
            this._promoTextBox.Name = "_promoTextBox";
            this._promoTextBox.Size = new System.Drawing.Size(190, 26);
            this._promoTextBox.TabIndex = 1;
            // 
            // _applyPromoButton
            // 
            this._applyPromoButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._applyPromoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._applyPromoButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._applyPromoButton.Location = new System.Drawing.Point(260, 4);
            this._applyPromoButton.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this._applyPromoButton.Name = "_applyPromoButton";
            this._applyPromoButton.Size = new System.Drawing.Size(88, 28);
            this._applyPromoButton.TabIndex = 2;
            this._applyPromoButton.Text = "Áp dụng";
            // 
            // _totalsLayout
            // 
            this._totalsLayout.ColumnCount = 2;
            this._totalsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this._totalsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this._totalsLayout.Controls.Add(this._subtotalCaption, 0, 0);
            this._totalsLayout.Controls.Add(this._subtotalValue, 1, 0);
            this._totalsLayout.Controls.Add(this._discountCaption, 0, 1);
            this._totalsLayout.Controls.Add(this._discountValue, 1, 1);
            this._totalsLayout.Controls.Add(this._vatCaption, 0, 2);
            this._totalsLayout.Controls.Add(this._vatValue, 1, 2);
            this._totalsLayout.Controls.Add(this._pointsCaption, 0, 3);
            this._totalsLayout.Controls.Add(this._pointsValue, 1, 3);
            this._totalsLayout.Controls.Add(this._grandTotalCaption, 0, 4);
            this._totalsLayout.Controls.Add(this._grandTotalValue, 1, 4);
            this._totalsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._totalsLayout.Location = new System.Drawing.Point(3, 45);
            this._totalsLayout.Name = "_totalsLayout";
            this._totalsLayout.RowCount = 5;
            this._totalsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this._totalsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this._totalsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this._totalsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this._totalsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this._totalsLayout.Size = new System.Drawing.Size(348, 122);
            this._totalsLayout.TabIndex = 1;
            // 
            // _subtotalCaption
            // 
            this._subtotalCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this._subtotalCaption.Location = new System.Drawing.Point(3, 0);
            this._subtotalCaption.Name = "_subtotalCaption";
            this._subtotalCaption.Size = new System.Drawing.Size(185, 24);
            this._subtotalCaption.TabIndex = 0;
            this._subtotalCaption.Text = "Tạm tính";
            this._subtotalCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _subtotalValue
            // 
            this._subtotalValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this._subtotalValue.Location = new System.Drawing.Point(194, 0);
            this._subtotalValue.Name = "_subtotalValue";
            this._subtotalValue.Size = new System.Drawing.Size(151, 24);
            this._subtotalValue.TabIndex = 1;
            this._subtotalValue.Text = "0 VNĐ";
            this._subtotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _discountCaption
            // 
            this._discountCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this._discountCaption.Location = new System.Drawing.Point(3, 24);
            this._discountCaption.Name = "_discountCaption";
            this._discountCaption.Size = new System.Drawing.Size(185, 24);
            this._discountCaption.TabIndex = 2;
            this._discountCaption.Text = "Giảm giá";
            this._discountCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _discountValue
            // 
            this._discountValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this._discountValue.Location = new System.Drawing.Point(194, 24);
            this._discountValue.Name = "_discountValue";
            this._discountValue.Size = new System.Drawing.Size(151, 24);
            this._discountValue.TabIndex = 3;
            this._discountValue.Text = "0 VNĐ";
            this._discountValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _vatCaption
            // 
            this._vatCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this._vatCaption.Location = new System.Drawing.Point(3, 48);
            this._vatCaption.Name = "_vatCaption";
            this._vatCaption.Size = new System.Drawing.Size(185, 24);
            this._vatCaption.TabIndex = 4;
            this._vatCaption.Text = "VAT";
            this._vatCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _vatValue
            // 
            this._vatValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this._vatValue.Location = new System.Drawing.Point(194, 48);
            this._vatValue.Name = "_vatValue";
            this._vatValue.Size = new System.Drawing.Size(151, 24);
            this._vatValue.TabIndex = 5;
            this._vatValue.Text = "0 VNĐ";
            this._vatValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _pointsCaption
            // 
            this._pointsCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pointsCaption.Location = new System.Drawing.Point(3, 72);
            this._pointsCaption.Name = "_pointsCaption";
            this._pointsCaption.Size = new System.Drawing.Size(185, 24);
            this._pointsCaption.TabIndex = 6;
            this._pointsCaption.Text = "Trừ điểm";
            this._pointsCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _pointsValue
            // 
            this._pointsValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pointsValue.Location = new System.Drawing.Point(194, 72);
            this._pointsValue.Name = "_pointsValue";
            this._pointsValue.Size = new System.Drawing.Size(151, 24);
            this._pointsValue.TabIndex = 7;
            this._pointsValue.Text = "0 VNĐ";
            this._pointsValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _grandTotalCaption
            // 
            this._grandTotalCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grandTotalCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._grandTotalCaption.Location = new System.Drawing.Point(3, 96);
            this._grandTotalCaption.Name = "_grandTotalCaption";
            this._grandTotalCaption.Size = new System.Drawing.Size(185, 26);
            this._grandTotalCaption.TabIndex = 8;
            this._grandTotalCaption.Text = "Tổng cộng";
            this._grandTotalCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _grandTotalValue
            // 
            this._grandTotalValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grandTotalValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._grandTotalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._grandTotalValue.Location = new System.Drawing.Point(194, 96);
            this._grandTotalValue.Name = "_grandTotalValue";
            this._grandTotalValue.Size = new System.Drawing.Size(151, 26);
            this._grandTotalValue.TabIndex = 9;
            this._grandTotalValue.Text = "0 VNĐ";
            this._grandTotalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _paymentLayout
            // 
            this._paymentLayout.ColumnCount = 2;
            this._paymentLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this._paymentLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._paymentLayout.Controls.Add(this._paymentCaption, 0, 0);
            this._paymentLayout.Controls.Add(this._paymentMethod, 1, 0);
            this._paymentLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._paymentLayout.Location = new System.Drawing.Point(3, 173);
            this._paymentLayout.Name = "_paymentLayout";
            this._paymentLayout.RowCount = 1;
            this._paymentLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._paymentLayout.Size = new System.Drawing.Size(348, 52);
            this._paymentLayout.TabIndex = 2;
            // 
            // _paymentCaption
            // 
            this._paymentCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this._paymentCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._paymentCaption.Location = new System.Drawing.Point(3, 0);
            this._paymentCaption.Name = "_paymentCaption";
            this._paymentCaption.Size = new System.Drawing.Size(154, 52);
            this._paymentCaption.TabIndex = 0;
            this._paymentCaption.Text = "Hình thức thanh toán";
            // 
            // _paymentMethod
            // 
            this._paymentMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this._paymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._paymentMethod.Location = new System.Drawing.Point(160, 14);
            this._paymentMethod.Margin = new System.Windows.Forms.Padding(0, 14, 0, 14);
            this._paymentMethod.Name = "_paymentMethod";
            this._paymentMethod.Size = new System.Drawing.Size(188, 28);
            this._paymentMethod.TabIndex = 1;
            // 
            // _actionsLayout
            // 
            this._actionsLayout.ColumnCount = 2;
            this._actionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._actionsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._actionsLayout.Controls.Add(this._holdButton, 0, 0);
            this._actionsLayout.Controls.Add(this._heldCartsButton, 1, 0);
            this._actionsLayout.Controls.Add(this._checkoutButton, 0, 1);
            this._actionsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._actionsLayout.Location = new System.Drawing.Point(3, 231);
            this._actionsLayout.Name = "_actionsLayout";
            this._actionsLayout.RowCount = 2;
            this._actionsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46F));
            this._actionsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54F));
            this._actionsLayout.Size = new System.Drawing.Size(348, 80);
            this._actionsLayout.TabIndex = 3;
            // 
            // _holdButton
            // 
            this._holdButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._holdButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._holdButton.Location = new System.Drawing.Point(0, 2);
            this._holdButton.Margin = new System.Windows.Forms.Padding(0, 2, 4, 4);
            this._holdButton.Name = "_holdButton";
            this._holdButton.Size = new System.Drawing.Size(170, 30);
            this._holdButton.TabIndex = 0;
            this._holdButton.Text = "Tạm giữ";
            // 
            // _heldCartsButton
            // 
            this._heldCartsButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._heldCartsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._heldCartsButton.Location = new System.Drawing.Point(178, 2);
            this._heldCartsButton.Margin = new System.Windows.Forms.Padding(4, 2, 0, 4);
            this._heldCartsButton.Name = "_heldCartsButton";
            this._heldCartsButton.Size = new System.Drawing.Size(170, 30);
            this._heldCartsButton.TabIndex = 1;
            this._heldCartsButton.Text = "Giỏ đã giữ";
            // 
            // _checkoutButton
            // 
            this._checkoutButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._actionsLayout.SetColumnSpan(this._checkoutButton, 2);
            this._checkoutButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this._checkoutButton.FlatAppearance.BorderSize = 0;
            this._checkoutButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._checkoutButton.ForeColor = System.Drawing.Color.White;
            this._checkoutButton.Location = new System.Drawing.Point(0, 38);
            this._checkoutButton.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this._checkoutButton.Name = "_checkoutButton";
            this._checkoutButton.Size = new System.Drawing.Size(348, 42);
            this._checkoutButton.TabIndex = 2;
            this._checkoutButton.Text = "Thanh toán";
            this._checkoutButton.UseVisualStyleBackColor = false;
            // 
            // PosPageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.Controls.Add(this._root);
            this.Name = "PosPageControl";
            this.Size = new System.Drawing.Size(1180, 760);
            this._root.ResumeLayout(false);
            this._header.ResumeLayout(false);
            this._header.PerformLayout();
            this._body.Panel1.ResumeLayout(false);
            this._body.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._body)).EndInit();
            this._body.ResumeLayout(false);
            this._catalogCard.ResumeLayout(false);
            this._catalogLayout.ResumeLayout(false);
            this._searchLayout.ResumeLayout(false);
            this._searchLayout.PerformLayout();
            this._saleCard.ResumeLayout(false);
            this._saleLayout.ResumeLayout(false);
            this._customerLayout.ResumeLayout(false);
            this._customerLayout.PerformLayout();
            this._cartCheckoutSplit.Panel1.ResumeLayout(false);
            this._cartCheckoutSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._cartCheckoutSplit)).EndInit();
            this._cartCheckoutSplit.ResumeLayout(false);
            this._cartAreaLayout.ResumeLayout(false);
            this._cartAreaLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._cartGrid)).EndInit();
            this._checkoutScrollPanel.ResumeLayout(false);
            this._checkoutLayout.ResumeLayout(false);
            this._promoLayout.ResumeLayout(false);
            this._promoLayout.PerformLayout();
            this._totalsLayout.ResumeLayout(false);
            this._paymentLayout.ResumeLayout(false);
            this._actionsLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private DataGridViewTextBoxColumn CartProductName;
        private DataGridViewTextBoxColumn CartUnitPrice;
        private DataGridViewTextBoxColumn CartQuantity;
        private DataGridViewTextBoxColumn CartLineTotal;
        private DataGridViewButtonColumn CartRemove;
    }
}

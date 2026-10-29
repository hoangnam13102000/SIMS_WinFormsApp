using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Sales.InvoiceManagement
{
    partial class InvoiceDetailsDialog
    {
        private IContainer components = null;
        private TableLayoutPanel _root;
        private Label _invoiceHeading;
        private Panel _detailsCard;
        private TableLayoutPanel _detailsLayout;
        private TableLayoutPanel _infoLayout;
        private Panel _summaryPanel;
        private DataGridView _linesGrid;
        private Label _invoiceCodeCaption;
        private Label _dateCaption;
        private Label _customerCaption;
        private Label _cashierCaption;
        private Label _paymentCaption;
        private Label _subtotalCaption;
        private Label _discountCaption;
        private Label _promotionCaption;
        private Label _pointsCaption;
        private Label _invoiceCodeValue;
        private Label _dateValue;
        private Label _customerValue;
        private Label _cashierValue;
        private Label _paymentValue;
        private Label _subtotalValue;
        private Label _discountValue;
        private Label _vatLabel;
        private Label _vatValue;
        private Label _promotionValue;
        private Label _pointsValue;
        private Label _statusValue;
        private Label _totalValue;
        private Panel _previewCard;
        private Label _previewHeading;
        private Panel _receiptPanel;
        private FlowLayoutPanel _receiptContent;
        private PictureBox _receiptLogo;
        private Label _receiptStoreName;
        private Label _receiptDocumentTitle;
        private Label _receiptCode;
        private Label _receiptDate;
        private Label _receiptCustomer;
        private Label _receiptProduct;
        private Label _receiptProductLine;
        private Label _receiptSubtotal;
        private Label _receiptDiscount;
        private Label _receiptVat;
        private Label _receiptTotal;
        private Label _receiptPayment;
        private Label _receiptThankYou;
        private FlowLayoutPanel _actionsPanel;
        private Button _closeButton;
        private Button _printButton;
        private Button _returnButton;
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this._root = new System.Windows.Forms.TableLayoutPanel();
            this._invoiceHeading = new System.Windows.Forms.Label();
            this._detailsCard = new System.Windows.Forms.Panel();
            this._detailsLayout = new System.Windows.Forms.TableLayoutPanel();
            this._infoLayout = new System.Windows.Forms.TableLayoutPanel();
            this._invoiceCodeCaption = new System.Windows.Forms.Label();
            this._invoiceCodeValue = new System.Windows.Forms.Label();
            this._dateCaption = new System.Windows.Forms.Label();
            this._dateValue = new System.Windows.Forms.Label();
            this._customerCaption = new System.Windows.Forms.Label();
            this._customerValue = new System.Windows.Forms.Label();
            this._cashierCaption = new System.Windows.Forms.Label();
            this._cashierValue = new System.Windows.Forms.Label();
            this._paymentCaption = new System.Windows.Forms.Label();
            this._paymentValue = new System.Windows.Forms.Label();
            this._subtotalCaption = new System.Windows.Forms.Label();
            this._subtotalValue = new System.Windows.Forms.Label();
            this._discountCaption = new System.Windows.Forms.Label();
            this._discountValue = new System.Windows.Forms.Label();
            this._vatLabel = new System.Windows.Forms.Label();
            this._vatValue = new System.Windows.Forms.Label();
            this._promotionCaption = new System.Windows.Forms.Label();
            this._promotionValue = new System.Windows.Forms.Label();
            this._pointsCaption = new System.Windows.Forms.Label();
            this._pointsValue = new System.Windows.Forms.Label();
            this._linesGrid = new System.Windows.Forms.DataGridView();
            this.Product = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LineTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._summaryPanel = new System.Windows.Forms.Panel();
            this._statusValue = new System.Windows.Forms.Label();
            this._totalValue = new System.Windows.Forms.Label();
            this._previewCard = new System.Windows.Forms.Panel();
            this._receiptPanel = new System.Windows.Forms.Panel();
            this._receiptContent = new System.Windows.Forms.FlowLayoutPanel();
            this._receiptStoreName = new System.Windows.Forms.Label();
            this._receiptDocumentTitle = new System.Windows.Forms.Label();
            this._receiptCode = new System.Windows.Forms.Label();
            this._receiptDate = new System.Windows.Forms.Label();
            this._receiptCustomer = new System.Windows.Forms.Label();
            this._receiptProduct = new System.Windows.Forms.Label();
            this._receiptProductLine = new System.Windows.Forms.Label();
            this._receiptSubtotal = new System.Windows.Forms.Label();
            this._receiptDiscount = new System.Windows.Forms.Label();
            this._receiptVat = new System.Windows.Forms.Label();
            this._receiptTotal = new System.Windows.Forms.Label();
            this._receiptPayment = new System.Windows.Forms.Label();
            this._receiptThankYou = new System.Windows.Forms.Label();
            this._previewHeading = new System.Windows.Forms.Label();
            this._actionsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._closeButton = new System.Windows.Forms.Button();
            this._printButton = new System.Windows.Forms.Button();
            this._returnButton = new System.Windows.Forms.Button();
            this._receiptLogo = new System.Windows.Forms.PictureBox();
            this._root.SuspendLayout();
            this._detailsCard.SuspendLayout();
            this._detailsLayout.SuspendLayout();
            this._infoLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._linesGrid)).BeginInit();
            this._summaryPanel.SuspendLayout();
            this._previewCard.SuspendLayout();
            this._receiptPanel.SuspendLayout();
            this._receiptContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._receiptLogo)).BeginInit();
            this._actionsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _root
            // 
            this._root.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this._root.ColumnCount = 2;
            this._root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this._root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this._root.Controls.Add(this._invoiceHeading, 0, 0);
            this._root.Controls.Add(this._detailsCard, 0, 1);
            this._root.Controls.Add(this._previewCard, 1, 1);
            this._root.Controls.Add(this._actionsPanel, 0, 2);
            this._root.SetColumnSpan(this._actionsPanel, 2);
            this._root.Dock = System.Windows.Forms.DockStyle.Fill;
            this._root.Location = new System.Drawing.Point(0, 0);
            this._root.Name = "_root";
            this._root.Padding = new System.Windows.Forms.Padding(24);
            this._root.RowCount = 3;
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this._root.Size = new System.Drawing.Size(1040, 760);
            this._root.TabIndex = 0;
            // 
            // _invoiceHeading
            // 
            this._root.SetColumnSpan(this._invoiceHeading, 2);
            this._invoiceHeading.Dock = System.Windows.Forms.DockStyle.Fill;
            this._invoiceHeading.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold);
            this._invoiceHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._invoiceHeading.Location = new System.Drawing.Point(27, 24);
            this._invoiceHeading.Name = "_invoiceHeading";
            this._invoiceHeading.Size = new System.Drawing.Size(986, 72);
            this._invoiceHeading.TabIndex = 0;
            this._invoiceHeading.Text = "Chi tiết hóa đơn";
            this._invoiceHeading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _detailsCard
            // 
            this._detailsCard.BackColor = System.Drawing.Color.White;
            this._detailsCard.Controls.Add(this._detailsLayout);
            this._detailsCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._detailsCard.Location = new System.Drawing.Point(24, 96);
            this._detailsCard.Margin = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this._detailsCard.Name = "_detailsCard";
            this._detailsCard.Padding = new System.Windows.Forms.Padding(16);
            this._detailsCard.Size = new System.Drawing.Size(563, 576);
            this._detailsCard.TabIndex = 1;
            // 
            // _detailsLayout
            // 
            this._detailsLayout.BackColor = System.Drawing.Color.White;
            this._detailsLayout.ColumnCount = 1;
            this._detailsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._detailsLayout.Controls.Add(this._infoLayout, 0, 0);
            this._detailsLayout.Controls.Add(this._linesGrid, 0, 1);
            this._detailsLayout.Controls.Add(this._summaryPanel, 0, 2);
            this._detailsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._detailsLayout.Location = new System.Drawing.Point(16, 16);
            this._detailsLayout.Name = "_detailsLayout";
            this._detailsLayout.RowCount = 3;
            this._detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this._detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._detailsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this._detailsLayout.Size = new System.Drawing.Size(531, 544);
            this._detailsLayout.TabIndex = 0;
            // 
            // _infoLayout
            // 
            this._infoLayout.BackColor = System.Drawing.Color.White;
            this._infoLayout.ColumnCount = 2;
            this._infoLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this._infoLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64F));
            this._infoLayout.Controls.Add(this._invoiceCodeCaption, 0, 0);
            this._infoLayout.Controls.Add(this._invoiceCodeValue, 1, 0);
            this._infoLayout.Controls.Add(this._dateCaption, 0, 1);
            this._infoLayout.Controls.Add(this._dateValue, 1, 1);
            this._infoLayout.Controls.Add(this._customerCaption, 0, 2);
            this._infoLayout.Controls.Add(this._customerValue, 1, 2);
            this._infoLayout.Controls.Add(this._cashierCaption, 0, 3);
            this._infoLayout.Controls.Add(this._cashierValue, 1, 3);
            this._infoLayout.Controls.Add(this._paymentCaption, 0, 4);
            this._infoLayout.Controls.Add(this._paymentValue, 1, 4);
            this._infoLayout.Controls.Add(this._subtotalCaption, 0, 5);
            this._infoLayout.Controls.Add(this._subtotalValue, 1, 5);
            this._infoLayout.Controls.Add(this._discountCaption, 0, 6);
            this._infoLayout.Controls.Add(this._discountValue, 1, 6);
            this._infoLayout.Controls.Add(this._vatLabel, 0, 7);
            this._infoLayout.Controls.Add(this._vatValue, 1, 7);
            this._infoLayout.Controls.Add(this._promotionCaption, 0, 8);
            this._infoLayout.Controls.Add(this._promotionValue, 1, 8);
            this._infoLayout.Controls.Add(this._pointsCaption, 0, 9);
            this._infoLayout.Controls.Add(this._pointsValue, 1, 9);
            this._infoLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._infoLayout.Location = new System.Drawing.Point(3, 3);
            this._infoLayout.Name = "_infoLayout";
            this._infoLayout.RowCount = 10;
            this._infoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._infoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._infoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._infoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._infoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._infoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._infoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._infoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._infoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._infoLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._infoLayout.Size = new System.Drawing.Size(525, 294);
            this._infoLayout.TabIndex = 0;
            // 
            // _invoiceCodeCaption
            // 
            this._invoiceCodeCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this._invoiceCodeCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._invoiceCodeCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._invoiceCodeCaption.Location = new System.Drawing.Point(3, 0);
            this._invoiceCodeCaption.Name = "_invoiceCodeCaption";
            this._invoiceCodeCaption.Size = new System.Drawing.Size(183, 30);
            this._invoiceCodeCaption.TabIndex = 0;
            this._invoiceCodeCaption.Text = "Mã hóa đơn";
            this._invoiceCodeCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _invoiceCodeValue
            // 
            this._invoiceCodeValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this._invoiceCodeValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._invoiceCodeValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._invoiceCodeValue.Location = new System.Drawing.Point(192, 0);
            this._invoiceCodeValue.Name = "_invoiceCodeValue";
            this._invoiceCodeValue.Size = new System.Drawing.Size(330, 30);
            this._invoiceCodeValue.TabIndex = 1;
            this._invoiceCodeValue.Text = "HD-20261028-0001";
            // 
            // _dateCaption
            // 
            this._dateCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dateCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._dateCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._dateCaption.Location = new System.Drawing.Point(3, 30);
            this._dateCaption.Name = "_dateCaption";
            this._dateCaption.Size = new System.Drawing.Size(183, 30);
            this._dateCaption.TabIndex = 2;
            this._dateCaption.Text = "Thời gian";
            this._dateCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _dateValue
            // 
            this._dateValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dateValue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._dateValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._dateValue.Location = new System.Drawing.Point(192, 30);
            this._dateValue.Name = "_dateValue";
            this._dateValue.Size = new System.Drawing.Size(330, 30);
            this._dateValue.TabIndex = 3;
            this._dateValue.Text = "28/10/2026 14:30";
            // 
            // _customerCaption
            // 
            this._customerCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this._customerCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._customerCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._customerCaption.Location = new System.Drawing.Point(3, 60);
            this._customerCaption.Name = "_customerCaption";
            this._customerCaption.Size = new System.Drawing.Size(183, 30);
            this._customerCaption.TabIndex = 4;
            this._customerCaption.Text = "Khách hàng";
            this._customerCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _customerValue
            // 
            this._customerValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this._customerValue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._customerValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._customerValue.Location = new System.Drawing.Point(192, 60);
            this._customerValue.Name = "_customerValue";
            this._customerValue.Size = new System.Drawing.Size(330, 30);
            this._customerValue.TabIndex = 5;
            this._customerValue.Text = "Khách hàng mẫu";
            // 
            // _cashierCaption
            // 
            this._cashierCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cashierCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._cashierCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._cashierCaption.Location = new System.Drawing.Point(3, 90);
            this._cashierCaption.Name = "_cashierCaption";
            this._cashierCaption.Size = new System.Drawing.Size(183, 30);
            this._cashierCaption.TabIndex = 6;
            this._cashierCaption.Text = "Nhân viên";
            this._cashierCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _cashierValue
            // 
            this._cashierValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cashierValue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._cashierValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._cashierValue.Location = new System.Drawing.Point(192, 90);
            this._cashierValue.Name = "_cashierValue";
            this._cashierValue.Size = new System.Drawing.Size(330, 30);
            this._cashierValue.TabIndex = 7;
            this._cashierValue.Text = "Nguyễn Văn An";
            // 
            // _paymentCaption
            // 
            this._paymentCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this._paymentCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._paymentCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._paymentCaption.Location = new System.Drawing.Point(3, 120);
            this._paymentCaption.Name = "_paymentCaption";
            this._paymentCaption.Size = new System.Drawing.Size(183, 30);
            this._paymentCaption.TabIndex = 8;
            this._paymentCaption.Text = "Thanh toán";
            this._paymentCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _paymentValue
            // 
            this._paymentValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this._paymentValue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._paymentValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._paymentValue.Location = new System.Drawing.Point(192, 120);
            this._paymentValue.Name = "_paymentValue";
            this._paymentValue.Size = new System.Drawing.Size(330, 30);
            this._paymentValue.TabIndex = 9;
            this._paymentValue.Text = "Tiền mặt";
            // 
            // _subtotalCaption
            // 
            this._subtotalCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this._subtotalCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._subtotalCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._subtotalCaption.Location = new System.Drawing.Point(3, 150);
            this._subtotalCaption.Name = "_subtotalCaption";
            this._subtotalCaption.Size = new System.Drawing.Size(183, 30);
            this._subtotalCaption.TabIndex = 10;
            this._subtotalCaption.Text = "Tạm tính";
            this._subtotalCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _subtotalValue
            // 
            this._subtotalValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this._subtotalValue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._subtotalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._subtotalValue.Location = new System.Drawing.Point(192, 150);
            this._subtotalValue.Name = "_subtotalValue";
            this._subtotalValue.Size = new System.Drawing.Size(330, 30);
            this._subtotalValue.TabIndex = 11;
            this._subtotalValue.Text = "250.000 đ";
            // 
            // _discountCaption
            // 
            this._discountCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this._discountCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._discountCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._discountCaption.Location = new System.Drawing.Point(3, 180);
            this._discountCaption.Name = "_discountCaption";
            this._discountCaption.Size = new System.Drawing.Size(183, 30);
            this._discountCaption.TabIndex = 12;
            this._discountCaption.Text = "Giảm giá";
            this._discountCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _discountValue
            // 
            this._discountValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this._discountValue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._discountValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._discountValue.Location = new System.Drawing.Point(192, 180);
            this._discountValue.Name = "_discountValue";
            this._discountValue.Size = new System.Drawing.Size(330, 30);
            this._discountValue.TabIndex = 13;
            this._discountValue.Text = "10.000 đ";
            // 
            // _vatLabel
            // 
            this._vatLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._vatLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._vatLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._vatLabel.Location = new System.Drawing.Point(3, 210);
            this._vatLabel.Name = "_vatLabel";
            this._vatLabel.Size = new System.Drawing.Size(183, 30);
            this._vatLabel.TabIndex = 14;
            this._vatLabel.Text = "VAT (8%)";
            this._vatLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _vatValue
            // 
            this._vatValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this._vatValue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._vatValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._vatValue.Location = new System.Drawing.Point(192, 210);
            this._vatValue.Name = "_vatValue";
            this._vatValue.Size = new System.Drawing.Size(330, 30);
            this._vatValue.TabIndex = 15;
            this._vatValue.Text = "19.200 đ";
            // 
            // _promotionCaption
            // 
            this._promotionCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this._promotionCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._promotionCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._promotionCaption.Location = new System.Drawing.Point(3, 240);
            this._promotionCaption.Name = "_promotionCaption";
            this._promotionCaption.Size = new System.Drawing.Size(183, 30);
            this._promotionCaption.TabIndex = 16;
            this._promotionCaption.Text = "Mã khuyến mãi";
            this._promotionCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _promotionValue
            // 
            this._promotionValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this._promotionValue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._promotionValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._promotionValue.Location = new System.Drawing.Point(192, 240);
            this._promotionValue.Name = "_promotionValue";
            this._promotionValue.Size = new System.Drawing.Size(330, 30);
            this._promotionValue.TabIndex = 17;
            this._promotionValue.Text = "WELCOME10";
            // 
            // _pointsCaption
            // 
            this._pointsCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pointsCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._pointsCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._pointsCaption.Location = new System.Drawing.Point(3, 270);
            this._pointsCaption.Name = "_pointsCaption";
            this._pointsCaption.Size = new System.Drawing.Size(183, 30);
            this._pointsCaption.TabIndex = 18;
            this._pointsCaption.Text = "Điểm đã dùng";
            this._pointsCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _pointsValue
            // 
            this._pointsValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pointsValue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._pointsValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._pointsValue.Location = new System.Drawing.Point(192, 270);
            this._pointsValue.Name = "_pointsValue";
            this._pointsValue.Size = new System.Drawing.Size(330, 30);
            this._pointsValue.TabIndex = 19;
            this._pointsValue.Text = "0";
            // 
            // _linesGrid
            // 
            this._linesGrid.AllowUserToAddRows = false;
            this._linesGrid.AllowUserToDeleteRows = false;
            this._linesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._linesGrid.BackgroundColor = System.Drawing.Color.White;
            this._linesGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._linesGrid.ColumnHeadersHeight = 36;
            this._linesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Product,
            this.Quantity,
            this.Price,
            this.LineTotal});
            this._linesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._linesGrid.EnableHeadersVisualStyles = false;
            this._linesGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this._linesGrid.Location = new System.Drawing.Point(0, 308);
            this._linesGrid.Margin = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this._linesGrid.Name = "_linesGrid";
            this._linesGrid.ReadOnly = true;
            this._linesGrid.RowHeadersVisible = false;
            this._linesGrid.RowHeadersWidth = 62;
            this._linesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._linesGrid.Size = new System.Drawing.Size(531, 150);
            this._linesGrid.TabIndex = 1;
            // 
            // Product
            // 
            this.Product.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Product.HeaderText = "Sản phẩm";
            this.Product.MinimumWidth = 8;
            this.Product.Name = "Product";
            this.Product.ReadOnly = true;
            // 
            // Quantity
            // 
            this.Quantity.FillWeight = 35F;
            this.Quantity.HeaderText = "SL";
            this.Quantity.MinimumWidth = 8;
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            // 
            // Price
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Price.DefaultCellStyle = dataGridViewCellStyle1;
            this.Price.FillWeight = 75F;
            this.Price.HeaderText = "Đơn giá";
            this.Price.MinimumWidth = 8;
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            // 
            // LineTotal
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.LineTotal.DefaultCellStyle = dataGridViewCellStyle2;
            this.LineTotal.FillWeight = 85F;
            this.LineTotal.HeaderText = "Thành tiền";
            this.LineTotal.MinimumWidth = 8;
            this.LineTotal.Name = "LineTotal";
            this.LineTotal.ReadOnly = true;
            // 
            // _summaryPanel
            // 
            this._summaryPanel.BackColor = System.Drawing.Color.White;
            this._summaryPanel.Controls.Add(this._statusValue);
            this._summaryPanel.Controls.Add(this._totalValue);
            this._summaryPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._summaryPanel.Location = new System.Drawing.Point(3, 469);
            this._summaryPanel.Name = "_summaryPanel";
            this._summaryPanel.Size = new System.Drawing.Size(525, 72);
            this._summaryPanel.TabIndex = 2;
            // 
            // _statusValue
            // 
            this._statusValue.Dock = System.Windows.Forms.DockStyle.Left;
            this._statusValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this._statusValue.Location = new System.Drawing.Point(0, 0);
            this._statusValue.Name = "_statusValue";
            this._statusValue.Size = new System.Drawing.Size(150, 72);
            this._statusValue.TabIndex = 0;
            // 
            // _totalValue
            // 
            this._totalValue.Dock = System.Windows.Forms.DockStyle.Right;
            this._totalValue.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this._totalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._totalValue.Location = new System.Drawing.Point(225, 0);
            this._totalValue.Name = "_totalValue";
            this._totalValue.Size = new System.Drawing.Size(300, 72);
            this._totalValue.TabIndex = 1;
            this._totalValue.Text = "TỔNG THANH TOÁN     259.200 đ";
            this._totalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _previewCard
            // 
            this._previewCard.BackColor = System.Drawing.Color.White;
            this._previewCard.Controls.Add(this._receiptPanel);
            this._previewCard.Controls.Add(this._previewHeading);
            this._previewCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this._previewCard.Location = new System.Drawing.Point(611, 96);
            this._previewCard.Margin = new System.Windows.Forms.Padding(12, 0, 0, 12);
            this._previewCard.Name = "_previewCard";
            this._previewCard.Padding = new System.Windows.Forms.Padding(12);
            this._previewCard.Size = new System.Drawing.Size(405, 576);
            this._previewCard.TabIndex = 2;
            // 
            // _receiptPanel
            // 
            this._receiptPanel.AutoScroll = true;
            this._receiptPanel.BackColor = System.Drawing.Color.White;
            this._receiptPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._receiptPanel.Controls.Add(this._receiptContent);
            this._receiptPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._receiptPanel.Location = new System.Drawing.Point(12, 48);
            this._receiptPanel.Name = "_receiptPanel";
            this._receiptPanel.Padding = new System.Windows.Forms.Padding(16);
            this._receiptPanel.Size = new System.Drawing.Size(381, 516);
            this._receiptPanel.TabIndex = 0;
            // 
            // _receiptContent
            // 
            this._receiptContent.AutoSize = true;
            this._receiptContent.BackColor = System.Drawing.Color.White;
            this._receiptContent.Controls.Add(this._receiptLogo);
            this._receiptContent.Controls.Add(this._receiptStoreName);
            this._receiptContent.Controls.Add(this._receiptDocumentTitle);
            this._receiptContent.Controls.Add(this._receiptCode);
            this._receiptContent.Controls.Add(this._receiptDate);
            this._receiptContent.Controls.Add(this._receiptCustomer);
            this._receiptContent.Controls.Add(this._receiptProduct);
            this._receiptContent.Controls.Add(this._receiptProductLine);
            this._receiptContent.Controls.Add(this._receiptSubtotal);
            this._receiptContent.Controls.Add(this._receiptDiscount);
            this._receiptContent.Controls.Add(this._receiptVat);
            this._receiptContent.Controls.Add(this._receiptTotal);
            this._receiptContent.Controls.Add(this._receiptPayment);
            this._receiptContent.Controls.Add(this._receiptThankYou);
            this._receiptContent.Dock = System.Windows.Forms.DockStyle.Top;
            this._receiptContent.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this._receiptContent.Location = new System.Drawing.Point(16, 16);
            this._receiptContent.Name = "_receiptContent";
            this._receiptContent.Size = new System.Drawing.Size(321, 491);
            this._receiptContent.TabIndex = 0;
            this._receiptContent.WrapContents = false;
            // 
            // _receiptStoreName
            // 
            this._receiptStoreName.AutoSize = true;
            this._receiptStoreName.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this._receiptStoreName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._receiptStoreName.Location = new System.Drawing.Point(2, 78);
            this._receiptStoreName.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this._receiptStoreName.Name = "_receiptStoreName";
            this._receiptStoreName.Size = new System.Drawing.Size(91, 41);
            this._receiptStoreName.TabIndex = 1;
            this._receiptStoreName.Text = "SIMS";
            // 
            // _receiptDocumentTitle
            // 
            this._receiptDocumentTitle.AutoSize = true;
            this._receiptDocumentTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._receiptDocumentTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._receiptDocumentTitle.Location = new System.Drawing.Point(2, 127);
            this._receiptDocumentTitle.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this._receiptDocumentTitle.Name = "_receiptDocumentTitle";
            this._receiptDocumentTitle.Size = new System.Drawing.Size(223, 28);
            this._receiptDocumentTitle.TabIndex = 2;
            this._receiptDocumentTitle.Text = "HÓA ĐƠN BÁN HÀNG";
            // 
            // _receiptCode
            // 
            this._receiptCode.AutoSize = true;
            this._receiptCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._receiptCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._receiptCode.Location = new System.Drawing.Point(2, 163);
            this._receiptCode.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this._receiptCode.Name = "_receiptCode";
            this._receiptCode.Size = new System.Drawing.Size(173, 25);
            this._receiptCode.TabIndex = 3;
            this._receiptCode.Text = "HD-20261028-0001";
            // 
            // _receiptDate
            // 
            this._receiptDate.AutoSize = true;
            this._receiptDate.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._receiptDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._receiptDate.Location = new System.Drawing.Point(2, 196);
            this._receiptDate.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this._receiptDate.Name = "_receiptDate";
            this._receiptDate.Size = new System.Drawing.Size(137, 21);
            this._receiptDate.TabIndex = 4;
            this._receiptDate.Text = "28/10/2026 14:30";
            // 
            // _receiptCustomer
            // 
            this._receiptCustomer.AutoSize = true;
            this._receiptCustomer.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._receiptCustomer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._receiptCustomer.Location = new System.Drawing.Point(2, 225);
            this._receiptCustomer.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this._receiptCustomer.Name = "_receiptCustomer";
            this._receiptCustomer.Size = new System.Drawing.Size(175, 21);
            this._receiptCustomer.TabIndex = 5;
            this._receiptCustomer.Text = "Khách: Khách hàng mẫu";
            // 
            // _receiptProduct
            // 
            this._receiptProduct.AutoSize = true;
            this._receiptProduct.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._receiptProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._receiptProduct.Location = new System.Drawing.Point(2, 254);
            this._receiptProduct.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this._receiptProduct.Name = "_receiptProduct";
            this._receiptProduct.Size = new System.Drawing.Size(124, 21);
            this._receiptProduct.TabIndex = 6;
            this._receiptProduct.Text = "Sản phẩm mẫu";
            // 
            // _receiptProductLine
            // 
            this._receiptProductLine.AutoSize = true;
            this._receiptProductLine.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._receiptProductLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._receiptProductLine.Location = new System.Drawing.Point(2, 283);
            this._receiptProductLine.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this._receiptProductLine.Name = "_receiptProductLine";
            this._receiptProductLine.Size = new System.Drawing.Size(194, 21);
            this._receiptProductLine.TabIndex = 7;
            this._receiptProductLine.Text = "2 x 125.000 đ     250.000 đ";
            // 
            // _receiptSubtotal
            // 
            this._receiptSubtotal.AutoSize = true;
            this._receiptSubtotal.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._receiptSubtotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._receiptSubtotal.Location = new System.Drawing.Point(2, 312);
            this._receiptSubtotal.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this._receiptSubtotal.Name = "_receiptSubtotal";
            this._receiptSubtotal.Size = new System.Drawing.Size(148, 21);
            this._receiptSubtotal.TabIndex = 8;
            this._receiptSubtotal.Text = "Tạm tính: 250.000 đ";
            // 
            // _receiptDiscount
            // 
            this._receiptDiscount.AutoSize = true;
            this._receiptDiscount.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._receiptDiscount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._receiptDiscount.Location = new System.Drawing.Point(2, 341);
            this._receiptDiscount.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this._receiptDiscount.Name = "_receiptDiscount";
            this._receiptDiscount.Size = new System.Drawing.Size(140, 21);
            this._receiptDiscount.TabIndex = 9;
            this._receiptDiscount.Text = "Giảm giá: 10.000 đ";
            // 
            // _receiptVat
            // 
            this._receiptVat.AutoSize = true;
            this._receiptVat.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._receiptVat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._receiptVat.Location = new System.Drawing.Point(2, 370);
            this._receiptVat.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this._receiptVat.Name = "_receiptVat";
            this._receiptVat.Size = new System.Drawing.Size(104, 21);
            this._receiptVat.TabIndex = 10;
            this._receiptVat.Text = "VAT: 19.200 đ";
            // 
            // _receiptTotal
            // 
            this._receiptTotal.AutoSize = true;
            this._receiptTotal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this._receiptTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._receiptTotal.Location = new System.Drawing.Point(2, 399);
            this._receiptTotal.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this._receiptTotal.Name = "_receiptTotal";
            this._receiptTotal.Size = new System.Drawing.Size(192, 30);
            this._receiptTotal.TabIndex = 11;
            this._receiptTotal.Text = "TỔNG: 259.200 đ";
            // 
            // _receiptPayment
            // 
            this._receiptPayment.AutoSize = true;
            this._receiptPayment.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._receiptPayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this._receiptPayment.Location = new System.Drawing.Point(2, 437);
            this._receiptPayment.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this._receiptPayment.Name = "_receiptPayment";
            this._receiptPayment.Size = new System.Drawing.Size(155, 21);
            this._receiptPayment.TabIndex = 12;
            this._receiptPayment.Text = "Thanh toán: Tiền mặt";
            // 
            // _receiptThankYou
            // 
            this._receiptThankYou.AutoSize = true;
            this._receiptThankYou.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this._receiptThankYou.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this._receiptThankYou.Location = new System.Drawing.Point(2, 466);
            this._receiptThankYou.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this._receiptThankYou.Name = "_receiptThankYou";
            this._receiptThankYou.Size = new System.Drawing.Size(156, 21);
            this._receiptThankYou.TabIndex = 13;
            this._receiptThankYou.Text = "Cảm ơn quý khách!";
            // 
            // _previewHeading
            // 
            this._previewHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this._previewHeading.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this._previewHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._previewHeading.Location = new System.Drawing.Point(12, 12);
            this._previewHeading.Name = "_previewHeading";
            this._previewHeading.Size = new System.Drawing.Size(381, 36);
            this._previewHeading.TabIndex = 1;
            this._previewHeading.Text = "XEM TRƯỚC HÓA ĐƠN";
            this._previewHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _closeButton
            // 
            this._actionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._actionsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._actionsPanel.WrapContents = false;
            this._actionsPanel.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this._actionsPanel.Controls.Add(this._closeButton);
            this._actionsPanel.Controls.Add(this._printButton);
            this._actionsPanel.Controls.Add(this._returnButton);
            // 
            // _closeButton
            // 
            this._closeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this._closeButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._closeButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._closeButton.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(100, 38);
            this._closeButton.TabIndex = 3;
            this._closeButton.Text = "Đóng";
            this._closeButton.UseVisualStyleBackColor = false;
            // 
            // _printButton
            // 
            this._printButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._printButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._printButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._printButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._printButton.ForeColor = System.Drawing.Color.White;
            this._printButton.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this._printButton.Name = "_printButton";
            this._printButton.Size = new System.Drawing.Size(170, 38);
            this._printButton.TabIndex = 4;
            this._printButton.Text = "Xem trước bản in";
            this._printButton.UseVisualStyleBackColor = false;
            // 
            // _returnButton
            // 
            this._returnButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this._returnButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._returnButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._returnButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._returnButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this._returnButton.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this._returnButton.Name = "_returnButton";
            this._returnButton.Size = new System.Drawing.Size(174, 38);
            this._returnButton.TabIndex = 5;
            this._returnButton.Text = "Trả hàng / hoàn tiền";
            this._returnButton.UseVisualStyleBackColor = false;
            // 
            // _receiptLogo
            // 
            this._receiptLogo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._receiptLogo.Image = global::SIMS_WinFormsApp.Properties.Resources.logo_icon;
            this._receiptLogo.Location = new System.Drawing.Point(79, 4);
            this._receiptLogo.Margin = new System.Windows.Forms.Padding(2, 4, 2, 2);
            this._receiptLogo.Name = "_receiptLogo";
            this._receiptLogo.Size = new System.Drawing.Size(68, 68);
            this._receiptLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._receiptLogo.TabIndex = 0;
            this._receiptLogo.TabStop = false;
            this._receiptLogo.Click += new System.EventHandler(this._receiptLogo_Click);
            // 
            // InvoiceDetailsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1040, 760);
            this.Controls.Add(this._root);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(920, 660);
            this.Name = "InvoiceDetailsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chi tiết hóa đơn";
            this._root.ResumeLayout(false);
            this._detailsCard.ResumeLayout(false);
            this._detailsLayout.ResumeLayout(false);
            this._infoLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._linesGrid)).EndInit();
            this._summaryPanel.ResumeLayout(false);
            this._previewCard.ResumeLayout(false);
            this._receiptPanel.ResumeLayout(false);
            this._receiptPanel.PerformLayout();
            this._receiptContent.ResumeLayout(false);
            this._receiptContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._receiptLogo)).EndInit();
            this._actionsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private DataGridViewTextBoxColumn Product;
        private DataGridViewTextBoxColumn Quantity;
        private DataGridViewTextBoxColumn Price;
        private DataGridViewTextBoxColumn LineTotal;
    }
}

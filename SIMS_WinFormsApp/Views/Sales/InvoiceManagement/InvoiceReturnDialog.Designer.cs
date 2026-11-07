using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Sales.InvoiceManagement
{
    partial class InvoiceReturnDialog
    {
        private IContainer components;
        private TableLayoutPanel _rootLayout;
        private Panel _headerPanel;
        private Label _headerTitle;
        private Label _policyLabel;
        private CheckBox _selectAllCheckBox;
        private DataGridView _linesGrid;
        private TableLayoutPanel _footerPanel;
        private FlowLayoutPanel _reasonPanel;
        private Label _reasonLabel;
        private ComboBox _reasonCombo;
        private TextBox _reasonDetails;
        private Label _summaryLabel;
        private FlowLayoutPanel _actionButtonsPanel;
        private Label _noteLabel;
        private Button _cancelButton;
        private Button _submitButton;

        private void InitializeComponent()
        {
            components = new Container();
            _rootLayout = new TableLayoutPanel();
            _headerPanel = new Panel();
            _headerTitle = new Label();
            _policyLabel = new Label();
            _selectAllCheckBox = new CheckBox();
            _linesGrid = new DataGridView();
            _footerPanel = new TableLayoutPanel();
            _reasonPanel = new FlowLayoutPanel();
            _reasonLabel = new Label();
            _reasonCombo = new ComboBox();
            _reasonDetails = new TextBox();
            _summaryLabel = new Label();
            _actionButtonsPanel = new FlowLayoutPanel();
            _noteLabel = new Label();
            _cancelButton = new Button();
            _submitButton = new Button();

            SuspendLayout();
            _rootLayout.SuspendLayout();
            _actionButtonsPanel.SuspendLayout();

            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(820, 600);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(820, 600);
            Name = "InvoiceReturnDialog";
            Text = "Tạo yêu cầu trả hàng";

            _rootLayout.Dock = DockStyle.Fill;
            _rootLayout.ColumnCount = 1;
            _rootLayout.RowCount = 3;
            _rootLayout.Padding = new Padding(20);
            _rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            _rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 130F));

            _headerPanel.Dock = DockStyle.Fill;
            _headerPanel.Controls.Add(_headerTitle);
            _headerPanel.Controls.Add(_policyLabel);
            _headerPanel.Controls.Add(_selectAllCheckBox);

            _headerTitle.AutoSize = true;
            _headerTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            _headerTitle.ForeColor = AppColors.TextTitle;
            _headerTitle.Location = new Point(0, 0);
            _headerTitle.Text = "Trả hàng từ hóa đơn " + "HD-MẪU-000001";

            _policyLabel.AutoSize = true;
            _policyLabel.Font = new Font("Segoe UI", 9F);
            _policyLabel.ForeColor = AppColors.TextMuted;
            _policyLabel.Location = new Point(2, 38);
            _policyLabel.Text = "Hóa đơn ngày 01/01/2026 • Thời hạn trả 7 ngày";

            _selectAllCheckBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _selectAllCheckBox.AutoSize = true;
            _selectAllCheckBox.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _selectAllCheckBox.Location = new Point(590, 43);
            _selectAllCheckBox.Text = "Chọn toàn bộ hóa đơn";
            this._selectAllCheckBox.CheckedChanged += new System.EventHandler(this.SelectAllCheckBox_CheckedChanged);

            _linesGrid.AllowUserToAddRows = false;
            _linesGrid.AllowUserToDeleteRows = false;
            _linesGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _linesGrid.BackgroundColor = AppColors.White;
            _linesGrid.BorderStyle = BorderStyle.None;
            _linesGrid.ColumnHeadersHeight = 38;
            _linesGrid.Dock = DockStyle.Fill;
            _linesGrid.EnableHeadersVisualStyles = false;
            _linesGrid.GridColor = AppColors.TableGrid;
            _linesGrid.RowHeadersVisible = false;
            _linesGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;
            _linesGrid.MultiSelect = false;
            this._linesGrid.CurrentCellDirtyStateChanged += new System.EventHandler(this.LinesGrid_CurrentCellDirtyStateChanged);
            this._linesGrid.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.OnLinesGridEditingControlShowing);
            this._linesGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.LinesGrid_CellValueChanged);
            this._linesGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.LinesGrid_CellEndEdit);
            this._linesGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.LinesGrid_DataError);
            _linesGrid.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "Selected",
                HeaderText = "Trả",
                FillWeight = 8,
                MinimumWidth = 48
            });
            _linesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Product",
                HeaderText = "Sản phẩm",
                FillWeight = 39,
                ReadOnly = true
            });
            _linesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Sold",
                HeaderText = "Đã mua",
                FillWeight = 10,
                ReadOnly = true
            });
            _linesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Returned",
                HeaderText = "Đã yêu cầu/đã trả",
                FillWeight = 17,
                ReadOnly = true
            });
            _linesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Quantity",
                HeaderText = "SL trả",
                FillWeight = 9
            });
            _linesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Price",
                HeaderText = "Đơn giá",
                FillWeight = 17,
                ReadOnly = true
            });
            _linesGrid.Columns["Sold"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            _linesGrid.Columns["Returned"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            _linesGrid.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            _linesGrid.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _linesGrid.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            _linesGrid.ColumnHeadersDefaultCellStyle.BackColor = AppColors.TableHeaderBg;
            _linesGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            _footerPanel.Dock = DockStyle.Fill;
            _footerPanel.ColumnCount = 2;
            _footerPanel.RowCount = 3;
            _footerPanel.Padding = new Padding(0, 10, 0, 0);
            _footerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _footerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 230F));
            _footerPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            _footerPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            _footerPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            _reasonPanel.Dock = DockStyle.Fill;
            _reasonPanel.WrapContents = false;
            _reasonPanel.Margin = Padding.Empty;

            _reasonLabel.AutoSize = true;
            _reasonLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _reasonLabel.Margin = new Padding(0, 6, 8, 0);
            _reasonLabel.Text = "Lý do:";

            _reasonCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _reasonCombo.Width = 160;
            _reasonCombo.Margin = new Padding(0, 2, 8, 0);
            _reasonCombo.Items.AddRange(new object[]
            {
                "Lỗi sản phẩm",
                "Giao sai sản phẩm",
                "Không còn nhu cầu",
                "Khác"
            });
            _reasonCombo.SelectedIndex = 0;

            _reasonDetails.Dock = DockStyle.Fill;
            _reasonDetails.MaxLength = 180;

            _reasonPanel.Controls.Add(_reasonLabel);
            _reasonPanel.Controls.Add(_reasonCombo);
            _reasonPanel.Controls.Add(_reasonDetails);
            _footerPanel.Controls.Add(_reasonPanel, 0, 0);
            _footerPanel.SetColumnSpan(_reasonPanel, 2);

            _summaryLabel.AutoSize = true;
            _summaryLabel.Dock = DockStyle.Fill;
            _summaryLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _summaryLabel.ForeColor = AppColors.Accent;
            _summaryLabel.TextAlign = ContentAlignment.MiddleLeft;
            _summaryLabel.Text = "Giá trị hàng chọn: 0 đ";
            _footerPanel.Controls.Add(_summaryLabel, 0, 1);

            _actionButtonsPanel.Dock = DockStyle.Fill;
            _actionButtonsPanel.FlowDirection = FlowDirection.RightToLeft;
            _actionButtonsPanel.WrapContents = false;
            _actionButtonsPanel.Controls.Add(_cancelButton);
            _actionButtonsPanel.Controls.Add(_submitButton);
            _footerPanel.Controls.Add(_actionButtonsPanel, 1, 1);

            _noteLabel.AutoSize = true;
            _noteLabel.Dock = DockStyle.Fill;
            _noteLabel.Font = new Font("Segoe UI", 8F);
            _noteLabel.ForeColor = AppColors.TextMuted;
            _noteLabel.Text = "Tiền hoàn được ghi nhận theo giá trị dự kiến; thanh toán thực tế xử lý bên ngoài.";
            _footerPanel.Controls.Add(_noteLabel, 0, 2);
            _footerPanel.SetColumnSpan(_noteLabel, 2);

            _cancelButton.DialogResult = DialogResult.Cancel;
            _cancelButton.FlatStyle = FlatStyle.Flat;
            _cancelButton.Margin = new Padding(8, 2, 0, 0);
            _cancelButton.Name = "_cancelButton";
            _cancelButton.Size = new Size(78, 34);
            _cancelButton.Text = "Hủy";
            this._cancelButton.Click += new System.EventHandler(this.CancelButton_Click);

            _submitButton.BackColor = AppColors.Accent;
            _submitButton.FlatStyle = FlatStyle.Flat;
            _submitButton.ForeColor = Color.White;
            _submitButton.Margin = new Padding(8, 2, 0, 0);
            _submitButton.Name = "_submitButton";
            _submitButton.Size = new Size(124, 34);
            _submitButton.Text = "Gửi yêu cầu";
            this._submitButton.Click += new System.EventHandler(this.SubmitButton_Click);

            _rootLayout.Controls.Add(_headerPanel, 0, 0);
            _rootLayout.Controls.Add(_linesGrid, 0, 1);
            _rootLayout.Controls.Add(_footerPanel, 0, 2);
            Controls.Add(_rootLayout);

            CancelButton = _cancelButton;
            _actionButtonsPanel.ResumeLayout(false);
            _rootLayout.ResumeLayout(false);
            ResumeLayout(false);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                components?.Dispose();
            base.Dispose(disposing);
        }
    }
}

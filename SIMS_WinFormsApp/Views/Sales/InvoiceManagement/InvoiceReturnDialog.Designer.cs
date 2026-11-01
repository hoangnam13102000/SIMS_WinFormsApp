using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Sales.InvoiceManagement
{
    partial class InvoiceReturnDialog
    {
        private IContainer components;
        private TableLayoutPanel _previewRoot;
        private Panel _previewHeader;
        private DataGridView _previewLinesGrid;
        private TableLayoutPanel _previewFooter;
        private FlowLayoutPanel _previewReasonPanel;
        private FlowLayoutPanel _previewButtons;
        private Label _previewHeading;
        private Label _previewPolicy;
        private CheckBox _previewSelectAll;
        private Label _previewReasonLabel;
        private ComboBox _previewReasonCombo;
        private TextBox _previewReasonDetails;
        private Label _previewSummary;
        private Button _previewCancelButton;
        private Button _previewSubmitButton;
        private Label _previewNote;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._previewRoot = new System.Windows.Forms.TableLayoutPanel();
            this._previewHeader = new System.Windows.Forms.Panel();
            this._previewHeading = new System.Windows.Forms.Label();
            this._previewPolicy = new System.Windows.Forms.Label();
            this._previewSelectAll = new System.Windows.Forms.CheckBox();
            this._previewLinesGrid = new System.Windows.Forms.DataGridView();
            this.PreviewReturn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PreviewProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PreviewSold = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PreviewReturned = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PreviewQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PreviewPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._previewFooter = new System.Windows.Forms.TableLayoutPanel();
            this._previewReasonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._previewReasonLabel = new System.Windows.Forms.Label();
            this._previewReasonCombo = new System.Windows.Forms.ComboBox();
            this._previewReasonDetails = new System.Windows.Forms.TextBox();
            this._previewSummary = new System.Windows.Forms.Label();
            this._previewButtons = new System.Windows.Forms.FlowLayoutPanel();
            this._previewCancelButton = new System.Windows.Forms.Button();
            this._previewSubmitButton = new System.Windows.Forms.Button();
            this._previewNote = new System.Windows.Forms.Label();
            this._previewRoot.SuspendLayout();
            this._previewHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._previewLinesGrid)).BeginInit();
            this._previewFooter.SuspendLayout();
            this._previewReasonPanel.SuspendLayout();
            this._previewButtons.SuspendLayout();
            this.SuspendLayout();
            //
            // _previewRoot
            //
            this._previewRoot.ColumnCount = 1;
            this._previewRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._previewRoot.Controls.Add(this._previewHeader, 0, 0);
            this._previewRoot.Controls.Add(this._previewLinesGrid, 0, 1);
            this._previewRoot.Controls.Add(this._previewFooter, 0, 2);
            this._previewRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this._previewRoot.Location = new System.Drawing.Point(0, 0);
            this._previewRoot.Name = "_previewRoot";
            this._previewRoot.Padding = new System.Windows.Forms.Padding(20);
            this._previewRoot.RowCount = 3;
            this._previewRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this._previewRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._previewRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this._previewRoot.Size = new System.Drawing.Size(820, 600);
            this._previewRoot.TabIndex = 0;
            //
            // _previewHeader
            //
            this._previewHeader.Controls.Add(this._previewHeading);
            this._previewHeader.Controls.Add(this._previewPolicy);
            this._previewHeader.Controls.Add(this._previewSelectAll);
            this._previewHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this._previewHeader.Location = new System.Drawing.Point(23, 23);
            this._previewHeader.Name = "_previewHeader";
            this._previewHeader.Size = new System.Drawing.Size(774, 66);
            this._previewHeader.TabIndex = 0;
            //
            // _previewHeading
            //
            this._previewHeading.AutoSize = true;
            this._previewHeading.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this._previewHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._previewHeading.Location = new System.Drawing.Point(0, 0);
            this._previewHeading.Name = "_previewHeading";
            this._previewHeading.Size = new System.Drawing.Size(595, 45);
            this._previewHeading.TabIndex = 0;
            this._previewHeading.Text = "Trả hàng từ hóa đơn HD-MẪU-000001";
            //
            // _previewPolicy
            //
            this._previewPolicy.AutoSize = true;
            this._previewPolicy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._previewPolicy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._previewPolicy.Location = new System.Drawing.Point(2, 38);
            this._previewPolicy.Name = "_previewPolicy";
            this._previewPolicy.Size = new System.Drawing.Size(371, 25);
            this._previewPolicy.TabIndex = 1;
            this._previewPolicy.Text = "Hóa đơn ngày hôm nay - Thời hạn trả 7 ngày";
            //
            // _previewSelectAll
            //
            this._previewSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._previewSelectAll.AutoSize = true;
            this._previewSelectAll.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._previewSelectAll.Location = new System.Drawing.Point(1040, 43);
            this._previewSelectAll.Name = "_previewSelectAll";
            this._previewSelectAll.Size = new System.Drawing.Size(228, 29);
            this._previewSelectAll.TabIndex = 2;
            this._previewSelectAll.Text = "Chọn toàn bộ hóa đơn";
            //
            // _previewLinesGrid
            //
            this._previewLinesGrid.AllowUserToAddRows = false;
            this._previewLinesGrid.AllowUserToDeleteRows = false;
            this._previewLinesGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._previewLinesGrid.BackgroundColor = System.Drawing.Color.White;
            this._previewLinesGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._previewLinesGrid.ColumnHeadersHeight = 38;
            this._previewLinesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PreviewReturn,
            this.PreviewProduct,
            this.PreviewSold,
            this.PreviewReturned,
            this.PreviewQuantity,
            this.PreviewPrice});
            this._previewLinesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._previewLinesGrid.EnableHeadersVisualStyles = false;
            this._previewLinesGrid.Location = new System.Drawing.Point(23, 95);
            this._previewLinesGrid.Name = "_previewLinesGrid";
            this._previewLinesGrid.ReadOnly = true;
            this._previewLinesGrid.RowHeadersVisible = false;
            this._previewLinesGrid.RowHeadersWidth = 62;
            this._previewLinesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this._previewLinesGrid.Size = new System.Drawing.Size(774, 352);
            this._previewLinesGrid.TabIndex = 1;
            //
            // PreviewReturn
            //
            this.PreviewReturn.FillWeight = 8F;
            this.PreviewReturn.HeaderText = "Trả";
            this.PreviewReturn.MinimumWidth = 8;
            this.PreviewReturn.Name = "PreviewReturn";
            this.PreviewReturn.ReadOnly = true;
            //
            // PreviewProduct
            //
            this.PreviewProduct.FillWeight = 39F;
            this.PreviewProduct.HeaderText = "Sản phẩm";
            this.PreviewProduct.MinimumWidth = 8;
            this.PreviewProduct.Name = "PreviewProduct";
            this.PreviewProduct.ReadOnly = true;
            //
            // PreviewSold
            //
            this.PreviewSold.FillWeight = 10F;
            this.PreviewSold.HeaderText = "Đã mua";
            this.PreviewSold.MinimumWidth = 8;
            this.PreviewSold.Name = "PreviewSold";
            this.PreviewSold.ReadOnly = true;
            //
            // PreviewReturned
            //
            this.PreviewReturned.FillWeight = 17F;
            this.PreviewReturned.HeaderText = "Đã yêu cầu/đã trả";
            this.PreviewReturned.MinimumWidth = 8;
            this.PreviewReturned.Name = "PreviewReturned";
            this.PreviewReturned.ReadOnly = true;
            //
            // PreviewQuantity
            //
            this.PreviewQuantity.FillWeight = 9F;
            this.PreviewQuantity.HeaderText = "SL trả";
            this.PreviewQuantity.MinimumWidth = 8;
            this.PreviewQuantity.Name = "PreviewQuantity";
            this.PreviewQuantity.ReadOnly = true;
            //
            // PreviewPrice
            //
            this.PreviewPrice.FillWeight = 17F;
            this.PreviewPrice.HeaderText = "Đơn giá";
            this.PreviewPrice.MinimumWidth = 8;
            this.PreviewPrice.Name = "PreviewPrice";
            this.PreviewPrice.ReadOnly = true;
            //
            // _previewFooter
            //
            this._previewFooter.ColumnCount = 2;
            this._previewFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._previewFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this._previewFooter.Controls.Add(this._previewReasonPanel, 0, 0);
            this._previewFooter.Controls.Add(this._previewSummary, 0, 1);
            this._previewFooter.Controls.Add(this._previewButtons, 1, 1);
            this._previewFooter.Controls.Add(this._previewNote, 0, 2);
            this._previewFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this._previewFooter.Location = new System.Drawing.Point(23, 453);
            this._previewFooter.Name = "_previewFooter";
            this._previewFooter.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this._previewFooter.RowCount = 3;
            this._previewFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._previewFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this._previewFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._previewFooter.Size = new System.Drawing.Size(774, 124);
            this._previewFooter.TabIndex = 2;
            //
            // _previewReasonPanel
            //
            this._previewFooter.SetColumnSpan(this._previewReasonPanel, 2);
            this._previewReasonPanel.Controls.Add(this._previewReasonLabel);
            this._previewReasonPanel.Controls.Add(this._previewReasonCombo);
            this._previewReasonPanel.Controls.Add(this._previewReasonDetails);
            this._previewReasonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._previewReasonPanel.Location = new System.Drawing.Point(3, 13);
            this._previewReasonPanel.Name = "_previewReasonPanel";
            this._previewReasonPanel.Size = new System.Drawing.Size(768, 24);
            this._previewReasonPanel.TabIndex = 0;
            this._previewReasonPanel.WrapContents = false;
            //
            // _previewReasonLabel
            //
            this._previewReasonLabel.AutoSize = true;
            this._previewReasonLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._previewReasonLabel.Location = new System.Drawing.Point(0, 6);
            this._previewReasonLabel.Margin = new System.Windows.Forms.Padding(0, 6, 8, 0);
            this._previewReasonLabel.Name = "_previewReasonLabel";
            this._previewReasonLabel.Size = new System.Drawing.Size(62, 25);
            this._previewReasonLabel.TabIndex = 0;
            this._previewReasonLabel.Text = "Lý do:";
            //
            // _previewReasonCombo
            //
            this._previewReasonCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._previewReasonCombo.Items.AddRange(new object[] {
            "Lỗi sản phẩm",
            "Giao sai sản phẩm",
            "Không còn nhu cầu",
            "Khác"});
            this._previewReasonCombo.Location = new System.Drawing.Point(70, 2);
            this._previewReasonCombo.Margin = new System.Windows.Forms.Padding(0, 2, 8, 0);
            this._previewReasonCombo.Name = "_previewReasonCombo";
            this._previewReasonCombo.Size = new System.Drawing.Size(160, 33);
            this._previewReasonCombo.TabIndex = 1;
            //
            // _previewReasonDetails
            //
            this._previewReasonDetails.Location = new System.Drawing.Point(241, 3);
            this._previewReasonDetails.Name = "_previewReasonDetails";
            this._previewReasonDetails.Size = new System.Drawing.Size(300, 31);
            this._previewReasonDetails.TabIndex = 2;
            //
            // _previewSummary
            //
            this._previewSummary.AutoSize = true;
            this._previewSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this._previewSummary.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._previewSummary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._previewSummary.Location = new System.Drawing.Point(3, 40);
            this._previewSummary.Name = "_previewSummary";
            this._previewSummary.Size = new System.Drawing.Size(538, 40);
            this._previewSummary.TabIndex = 1;
            this._previewSummary.Text = "Giá trị hàng chọn: 0 đ";
            this._previewSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // _previewButtons
            //
            this._previewButtons.Controls.Add(this._previewCancelButton);
            this._previewButtons.Controls.Add(this._previewSubmitButton);
            this._previewButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this._previewButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._previewButtons.Location = new System.Drawing.Point(547, 43);
            this._previewButtons.Name = "_previewButtons";
            this._previewButtons.Size = new System.Drawing.Size(224, 34);
            this._previewButtons.TabIndex = 2;
            this._previewButtons.WrapContents = false;
            //
            // _previewCancelButton
            //
            this._previewCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._previewCancelButton.Location = new System.Drawing.Point(143, 3);
            this._previewCancelButton.Name = "_previewCancelButton";
            this._previewCancelButton.Size = new System.Drawing.Size(78, 34);
            this._previewCancelButton.TabIndex = 0;
            this._previewCancelButton.Text = "Hủy";
            //
            // _previewSubmitButton
            //
            this._previewSubmitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._previewSubmitButton.ForeColor = System.Drawing.Color.White;
            this._previewSubmitButton.Location = new System.Drawing.Point(13, 3);
            this._previewSubmitButton.Name = "_previewSubmitButton";
            this._previewSubmitButton.Size = new System.Drawing.Size(124, 34);
            this._previewSubmitButton.TabIndex = 1;
            this._previewSubmitButton.Text = "Gửi yêu cầu";
            this._previewSubmitButton.UseVisualStyleBackColor = false;
            //
            // _previewNote
            //
            this._previewNote.AutoSize = true;
            this._previewFooter.SetColumnSpan(this._previewNote, 2);
            this._previewNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this._previewNote.Font = new System.Drawing.Font("Segoe UI", 8F);
            this._previewNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._previewNote.Location = new System.Drawing.Point(3, 80);
            this._previewNote.Name = "_previewNote";
            this._previewNote.Size = new System.Drawing.Size(768, 44);
            this._previewNote.TabIndex = 3;
            this._previewNote.Text = "Tiền hoàn được ghi nhận theo giá trị dự kiến; thanh toán thực tế xử lý bên ngoài." +
    "";
            //
            // InvoiceReturnDialog
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(820, 600);
            this.Controls.Add(this._previewRoot);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(820, 600);
            this.Name = "InvoiceReturnDialog";
            this.Text = "Tạo yêu cầu trả hàng";
            this._previewRoot.ResumeLayout(false);
            this._previewHeader.ResumeLayout(false);
            this._previewHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._previewLinesGrid)).EndInit();
            this._previewFooter.ResumeLayout(false);
            this._previewFooter.PerformLayout();
            this._previewReasonPanel.ResumeLayout(false);
            this._previewReasonPanel.PerformLayout();
            this._previewButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.components != null)
                    this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private DataGridViewCheckBoxColumn PreviewReturn;
        private DataGridViewTextBoxColumn PreviewProduct;
        private DataGridViewTextBoxColumn PreviewSold;
        private DataGridViewTextBoxColumn PreviewReturned;
        private DataGridViewTextBoxColumn PreviewQuantity;
        private DataGridViewTextBoxColumn PreviewPrice;
    }
}

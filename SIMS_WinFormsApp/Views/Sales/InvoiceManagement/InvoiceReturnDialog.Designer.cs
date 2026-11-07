using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Sales.InvoiceManagement
{
    partial class InvoiceReturnDialog
    {
        private IContainer components;
        private FlowLayoutPanel _actionButtonsPanel;
        private Button _cancelButton;
        private Button _submitButton;

        private void InitializeComponent()
        {
            components = new Container();
            _actionButtonsPanel = new FlowLayoutPanel();
            _cancelButton = new Button();
            _submitButton = new Button();
            SuspendLayout();
            _actionButtonsPanel.SuspendLayout();

            _actionButtonsPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _actionButtonsPanel.FlowDirection = FlowDirection.RightToLeft;
            _actionButtonsPanel.Location = new Point(500, 530);
            _actionButtonsPanel.Name = "_actionButtonsPanel";
            _actionButtonsPanel.Size = new Size(300, 40);
            _actionButtonsPanel.WrapContents = false;

            _cancelButton.DialogResult = DialogResult.Cancel;
            _cancelButton.FlatStyle = FlatStyle.Flat;
            _cancelButton.Margin = new Padding(8, 2, 0, 0);
            _cancelButton.Name = "_cancelButton";
            _cancelButton.Size = new Size(78, 34);
            _cancelButton.Text = "Hủy";

            _submitButton.BackColor = AppColors.Accent;
            _submitButton.FlatStyle = FlatStyle.Flat;
            _submitButton.ForeColor = Color.White;
            _submitButton.Margin = new Padding(8, 2, 0, 0);
            _submitButton.Name = "_submitButton";
            _submitButton.Size = new Size(124, 34);
            _submitButton.Text = "Gửi yêu cầu";
            _submitButton.Click += new System.EventHandler(this.SubmitButton_Click);

            _actionButtonsPanel.Controls.AddRange(new Control[] {
            _cancelButton,
            _submitButton});

            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(820, 600);
            CancelButton = _cancelButton;
            Controls.Add(_actionButtonsPanel);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(820, 600);
            Name = "InvoiceReturnDialog";
            Text = "Tạo yêu cầu trả hàng";
            _actionButtonsPanel.ResumeLayout(false);
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

using System.ComponentModel;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Sales
{
    partial class frmReturnRejectDialog
    {
        private IContainer components = null;
        private TextBox _reasonTextBox;
        private Button _rejectButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this._reasonTextBox = new System.Windows.Forms.TextBox();
            this._rejectButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _reasonTextBox
            // 
            this._reasonTextBox.Location = new System.Drawing.Point(12, 12);
            this._reasonTextBox.MaxLength = 500;
            this._reasonTextBox.Multiline = true;
            this._reasonTextBox.Name = "_reasonTextBox";
            this._reasonTextBox.Size = new System.Drawing.Size(396, 76);
            this._reasonTextBox.TabIndex = 0;
            // 
            // _rejectButton
            // 
            this._rejectButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._rejectButton.Location = new System.Drawing.Point(264, 104);
            this._rejectButton.Name = "_rejectButton";
            this._rejectButton.Size = new System.Drawing.Size(144, 32);
            this._rejectButton.TabIndex = 1;
            this._rejectButton.Text = "Từ chối yêu cầu";
            this._rejectButton.UseVisualStyleBackColor = true;
            this._rejectButton.Click += new System.EventHandler(this.RejectButton_Click);
            // 
            // frmReturnRejectDialog
            // 
            this.AcceptButton = this._rejectButton;
            this.ClientSize = new System.Drawing.Size(420, 150);
            this.Controls.Add(this._reasonTextBox);
            this.Controls.Add(this._rejectButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReturnRejectDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Từ chối yêu cầu trả";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

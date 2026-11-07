using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls.Pos
{
    partial class frmListPickerDialog
    {
        private ListBox _listBox;
        private PrimaryButton _cancelButton;
        private PrimaryButton _confirmButton;

        private void InitializeComponent()
        {
            this._listBox = new System.Windows.Forms.ListBox();
            this._cancelButton = new SIMS_WinFormsApp.UI.Controls.PrimaryButton();
            this._confirmButton = new SIMS_WinFormsApp.UI.Controls.PrimaryButton();
            this.SuspendLayout();
            this.FooterHost.SuspendLayout();
            // 
            // _listBox
            // 
            this._listBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._listBox.IntegralHeight = false;
            this._listBox.ItemHeight = 25;
            this._listBox.Items.AddRange(new object[] {
            "Mục thứ nhất",
            "Mục thứ hai",
            "Mục thứ ba"});
            this._listBox.Location = new System.Drawing.Point(28, 20);
            this._listBox.Name = "_listBox";
            this._listBox.Size = new System.Drawing.Size(500, 340);
            this._listBox.TabIndex = 0;
            this._listBox.DoubleClick += new System.EventHandler(this.ListBox_DoubleClick);
            this.ContentHost.Controls.Add(this._listBox);
            //
            // _cancelButton
            //
            this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cancelButton.CornerRadius = AppRadius.Medium;
            this._cancelButton.Font = AppFonts.Button;
            this._cancelButton.IsPrimary = false;
            this._cancelButton.Location = new System.Drawing.Point(360, 22);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(112, 44);
            this._cancelButton.Text = "Hủy";
            this._cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            //
            // _confirmButton
            //
            this._confirmButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._confirmButton.CornerRadius = AppRadius.Medium;
            this._confirmButton.Font = AppFonts.Button;
            this._confirmButton.IsPrimary = true;
            this._confirmButton.Location = new System.Drawing.Point(484, 22);
            this._confirmButton.Name = "_confirmButton";
            this._confirmButton.Size = new System.Drawing.Size(112, 44);
            this._confirmButton.Text = "Chọn";
            this._confirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            this.FooterHost.Controls.Add(this._cancelButton);
            this.FooterHost.Controls.Add(this._confirmButton);
            // 
            // frmListPickerDialog
            // 
            this.ClientSize = new System.Drawing.Size(560, 620);
            this.Name = "frmListPickerDialog";
            this.Text = "Chọn mục";
            this.Load += new System.EventHandler(this.frmListPickerDialog_Load);
            this.CloseRequested += new System.EventHandler(this.CloseRequestedHandler);
            this.FooterHost.ResumeLayout(true);
            this.ResumeLayout(false);

        }
    }
}

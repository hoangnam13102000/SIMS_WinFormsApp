using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Controls.Pos
{
    partial class frmListPickerDialog
    {
        private void InitializeComponent()
        {
            this._listBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // _listBox
            // 
            this._listBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._listBox.Dock = System.Windows.Forms.DockStyle.Top;
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
            // 
            // frmListPickerDialog
            // 
            this.ClientSize = new System.Drawing.Size(560, 620);
            this.Name = "frmListPickerDialog";
            this.Text = "Chọn mục";
            this.ResumeLayout(false);

        }
    }
}

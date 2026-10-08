using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Controls;

namespace SIMS_WinFormsApp.Forms.Catalog
{
    partial class frmCategoryEditor
    {
        private void InitializeComponent()
        {
            this._name = new SIMS_WinFormsApp.UI.Controls.LabeledIconField();
            this._status = new SIMS_WinFormsApp.UI.Controls.LabeledComboField();
            this.SuspendLayout();
            // 
            // _name
            // 
            this._name.AutoSize = true;
            this._name.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._name.BackColor = System.Drawing.Color.Transparent;
            this._name.Dock = System.Windows.Forms.DockStyle.Top;
            this._name.HintText = "";
            this._name.Icon = FontAwesome.Sharp.IconChar.Tags;
            this._name.IsRequired = true;
            this._name.LabelText = "Tên danh mục";
            this._name.Location = new System.Drawing.Point(28, 20);
            this._name.MaxLength = 32767;
            this._name.Name = "_name";
            this._name.PlaceholderText = "";
            this._name.Size = new System.Drawing.Size(560, 108);
            this._name.TabIndex = 1;
            this._name.UseThousandsSeparator = false;
            this._name.Value = "";
            // 
            // _status
            // 
            this._status.AutoSize = true;
            this._status.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._status.BackColor = System.Drawing.Color.Transparent;
            this._status.Dock = System.Windows.Forms.DockStyle.Top;
            this._status.IsRequired = true;
            this._status.LabelText = "Trạng thái";
            this._status.Location = new System.Drawing.Point(28, 128);
            this._status.Name = "_status";
            this._status.SelectedItem = null;
            this._status.Size = new System.Drawing.Size(560, 100);
            this._status.TabIndex = 0;
            // 
            // frmCategoryEditor
            // 
            this.ClientSize = new System.Drawing.Size(620, 620);
            this.Name = "frmCategoryEditor";
            this.Text = "Danh mục";
            this.ResumeLayout(false);

        }
    }
}

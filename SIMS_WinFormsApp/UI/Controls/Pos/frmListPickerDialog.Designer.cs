using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Controls.Pos
{
    partial class frmListPickerDialog
    {
        private void InitializeComponent()
        {
            _listBox = new ListBox();
            SuspendLayout();
            ContentHost.SuspendLayout();

            _listBox.BorderStyle = BorderStyle.FixedSingle;
            _listBox.Dock = DockStyle.Top;
            _listBox.Font = new Font("Segoe UI", 9F);
            _listBox.Height = 340;
            _listBox.IntegralHeight = false;
            _listBox.Name = "itemsList";
            _listBox.Items.AddRange(new object[] { "Mục thứ nhất", "Mục thứ hai", "Mục thứ ba" });

            ContentHost.Controls.Add(_listBox);
            ContentHost.ResumeLayout(false);
            ClientSize = new Size(420, 480);
            Name = "frmListPickerDialog";
            Text = "Chọn mục";
            ResumeLayout(false);
        }
    }
}

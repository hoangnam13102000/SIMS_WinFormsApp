using System;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Sales
{
    public partial class frmReturnRejectDialog : Form
    {
        public frmReturnRejectDialog()
        {
            InitializeComponent();
        }

        internal string RejectionReason
        {
            get { return _reasonTextBox.Text; }
        }

        private void RejectButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}

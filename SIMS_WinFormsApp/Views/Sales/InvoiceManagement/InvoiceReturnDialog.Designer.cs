using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Sales.InvoiceManagement
{
    partial class InvoiceReturnDialog
    {
        private IContainer components;

        private void InitializeComponent()
        {
            components = new Container();
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(820, 600);
            Font = new Font("Segoe UI", 9F);
            Name = "InvoiceReturnDialog";
            Text = "Tạo yêu cầu trả hàng";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                components?.Dispose();
            base.Dispose(disposing);
        }
    }
}

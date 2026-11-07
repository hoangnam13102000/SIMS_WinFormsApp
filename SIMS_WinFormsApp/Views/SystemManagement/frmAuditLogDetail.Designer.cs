using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.SystemManagement
{
    partial class frmAuditLogDetail
    {
        private PrimaryButton _closeButton;

        private void InitializeComponent()
        {
            _closeButton = new PrimaryButton();
            SuspendLayout();
            FooterHost.SuspendLayout();

            _closeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _closeButton.CornerRadius = AppRadius.Medium;
            _closeButton.Font = AppFonts.Button;
            _closeButton.IsPrimary = true;
            _closeButton.Location = new Point(484, 22);
            _closeButton.Name = "_closeButton";
            _closeButton.Size = new Size(112, 44);
            _closeButton.Text = "Đóng";
            _closeButton.Click += new System.EventHandler(CloseButton_Click);
            FooterHost.Controls.Add(_closeButton);

            ClientSize = new Size(760, 620);
            Name = "frmAuditLogDetail";
            Text = "Chi tiết nhật ký";
            FooterHost.ResumeLayout(true);
            ResumeLayout(false);
        }
    }
}

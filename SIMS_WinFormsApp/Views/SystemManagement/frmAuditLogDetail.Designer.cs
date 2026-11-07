using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.SystemManagement
{
    partial class frmAuditLogDetail
    {
        private PrimaryButton _closeButton;
        private TableLayoutPanel _detailInfoGrid;
        private Label _detailDiffHeader;
        private Panel _detailDiffHost;

        private void InitializeComponent()
        {
            _closeButton = new PrimaryButton();
            _detailInfoGrid = new TableLayoutPanel();
            _detailDiffHeader = new Label();
            _detailDiffHost = new Panel();

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
            this._closeButton.Click += new System.EventHandler(this.CloseButton_Click);
            FooterHost.Controls.Add(_closeButton);

            _detailInfoGrid.AutoSize = true;
            _detailInfoGrid.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _detailInfoGrid.ColumnCount = 2;
            _detailInfoGrid.Dock = DockStyle.Top;
            _detailInfoGrid.Margin = new Padding(0, 0, 0, 16);
            _detailInfoGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            _detailInfoGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            _detailDiffHeader.Dock = DockStyle.Top;
            _detailDiffHeader.Font = AppFonts.SmallBold;
            _detailDiffHeader.ForeColor = AppColors.TextMuted;
            _detailDiffHeader.Height = AppFonts.SmallBold.Height + 8;
            _detailDiffHeader.Name = "_detailDiffHeader";
            _detailDiffHeader.Text = "Thay đổi";
            _detailDiffHeader.TextAlign = ContentAlignment.MiddleLeft;

            _detailDiffHost.Dock = DockStyle.Fill;
            _detailDiffHost.BackColor = Color.Transparent;
            _detailDiffHost.Name = "_detailDiffHost";

            ClientSize = new Size(760, 620);
            Name = "frmAuditLogDetail";
            Text = "Chi tiết nhật ký";
            FooterHost.ResumeLayout(true);
            ResumeLayout(false);
        }
    }
}

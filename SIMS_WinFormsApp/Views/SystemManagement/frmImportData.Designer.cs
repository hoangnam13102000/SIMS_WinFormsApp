using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Dialogs;

namespace SIMS_WinFormsApp.Views.SystemManagement
{
    partial class frmImportData
    {
        private InfoBannerPanel _designerBanner;
        private Label _designerColumns;
        private Label _designerInstructions;
        private PrimaryButton _btnChooseFile;
        private Label _designerFileName;
        private Panel _designerFilePanel;
        private TextBox _designerResult;
        private PrimaryButton _btnClose;
        private PrimaryButton _btnStart;

        private void InitializeComponent()
        {
            _designerBanner = new InfoBannerPanel();
            _designerColumns = new Label();
            _designerInstructions = new Label();
            _btnChooseFile = new PrimaryButton();
            _designerFileName = new Label();
            _designerFilePanel = new Panel();
            _designerResult = new TextBox();
            _btnClose = new PrimaryButton();
            _btnStart = new PrimaryButton();
            SuspendLayout();
            ContentHost.SuspendLayout();
            FooterHost.SuspendLayout();

            _designerBanner.Dock = DockStyle.Top;
            _designerBanner.TitleText = "Nhập dữ liệu từ file";
            _designerBanner.DescriptionText = "Chọn file .xlsx hoặc .csv để bắt đầu.";
            _designerBanner.Name = "importInfoBanner";
            _designerBanner.Size = new Size(500, 84);

            _designerColumns.Dock = DockStyle.Top;
            _designerColumns.Height = 44;
            _designerColumns.Text = "Cột cần có: Tên, mã, thông tin";
            _designerColumns.Name = "requiredColumns";

            _designerInstructions.Dock = DockStyle.Top;
            _designerInstructions.Height = 40;
            _designerInstructions.Text = "Dòng đầu tiên trong file là tiêu đề cột.";
            _designerInstructions.Name = "importInstructions";

            _btnChooseFile.CornerRadius = AppRadius.Medium;
            _btnChooseFile.Font = AppFonts.Button;
            _btnChooseFile.IsPrimary = false;
            _btnChooseFile.Location = new Point(0, 0);
            _btnChooseFile.Name = "_btnChooseFile";
            _btnChooseFile.Size = new Size(140, 42);
            _btnChooseFile.Text = "Chọn file...";
            _btnChooseFile.Click += new System.EventHandler(ChooseFileButton_Click);
            _designerFileName.AutoSize = false;
            _designerFileName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            _designerFileName.Location = new Point(152, 0);
            _designerFileName.Name = "fileNameLabel";
            _designerFileName.Size = new Size(360, 42);
            _designerFileName.Text = "Chưa chọn file nào";
            _designerFileName.TextAlign = ContentAlignment.MiddleLeft;
            _designerFilePanel.Dock = DockStyle.Top;
            _designerFilePanel.Height = 46;
            _designerFilePanel.Name = "fileSelectionPanel";
            _designerFilePanel.Controls.Add(_designerFileName);
            _designerFilePanel.Controls.Add(_btnChooseFile);

            _designerResult.Dock = DockStyle.Top;
            _designerResult.Height = 160;
            _designerResult.Multiline = true;
            _designerResult.ReadOnly = true;
            _designerResult.ScrollBars = ScrollBars.Vertical;
            _designerResult.Name = "importResults";
            _designerResult.Text = "Kết quả nhập dữ liệu sẽ hiển thị tại đây.";

            _btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _btnClose.CornerRadius = AppRadius.Medium;
            _btnClose.Font = AppFonts.Button;
            _btnClose.IsPrimary = false;
            _btnClose.Location = new Point(302, 22);
            _btnClose.Name = "_btnClose";
            _btnClose.Size = new Size(112, 44);
            _btnClose.Text = "Đóng";
            _btnClose.Click += new System.EventHandler(CloseImportButton_Click);

            _btnStart.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _btnStart.CornerRadius = AppRadius.Medium;
            _btnStart.Enabled = false;
            _btnStart.Font = AppFonts.Button;
            _btnStart.IsPrimary = true;
            _btnStart.Location = new Point(426, 22);
            _btnStart.Name = "_btnStart";
            _btnStart.Size = new Size(170, 44);
            _btnStart.Text = "Bắt đầu nhập";
            _btnStart.Click += new System.EventHandler(StartImportButton_Click);

            ContentHost.Controls.Add(_designerResult);
            ContentHost.Controls.Add(_designerFilePanel);
            ContentHost.Controls.Add(_designerInstructions);
            ContentHost.Controls.Add(_designerColumns);
            ContentHost.Controls.Add(_designerBanner);
            FooterHost.Controls.Add(_btnClose);
            FooterHost.Controls.Add(_btnStart);
            FooterHost.ResumeLayout(false);
            ContentHost.ResumeLayout(false);
            ClientSize = new Size(800, 700);
            Name = "frmImportData";
            Text = "Nhập dữ liệu";
            ResumeLayout(false);
        }
    }
}

using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Controls;

namespace SIMS_WinFormsApp.Views.SystemManagement
{
    partial class frmImportData
    {
        private InfoBannerPanel _designerBanner;
        private Label _designerColumns;
        private Label _designerInstructions;
        private PrimaryButton _designerChooseFile;
        private Label _designerFileName;
        private TextBox _designerResult;

        private void InitializeComponent()
        {
            _designerBanner = new InfoBannerPanel();
            _designerColumns = new Label();
            _designerInstructions = new Label();
            _designerChooseFile = new PrimaryButton();
            _designerFileName = new Label();
            _designerResult = new TextBox();
            SuspendLayout();
            ContentHost.SuspendLayout();

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

            _designerChooseFile.Location = new Point(0, 0);
            _designerChooseFile.Name = "chooseFileButton";
            _designerChooseFile.Size = new Size(140, 42);
            _designerChooseFile.Text = "Chọn file...";
            _designerFileName.AutoSize = false;
            _designerFileName.Location = new Point(152, 0);
            _designerFileName.Name = "fileNameLabel";
            _designerFileName.Size = new Size(360, 42);
            _designerFileName.Text = "Chưa chọn file nào";
            _designerFileName.TextAlign = ContentAlignment.MiddleLeft;
            var filePanel = new Panel { Dock = DockStyle.Top, Height = 46, Name = "fileSelectionPanel" };
            filePanel.Controls.Add(_designerFileName);
            filePanel.Controls.Add(_designerChooseFile);

            _designerResult.Dock = DockStyle.Top;
            _designerResult.Height = 160;
            _designerResult.Multiline = true;
            _designerResult.ReadOnly = true;
            _designerResult.ScrollBars = ScrollBars.Vertical;
            _designerResult.Name = "importResults";
            _designerResult.Text = "Kết quả nhập dữ liệu sẽ hiển thị tại đây.";

            ContentHost.Controls.Add(_designerResult);
            ContentHost.Controls.Add(filePanel);
            ContentHost.Controls.Add(_designerInstructions);
            ContentHost.Controls.Add(_designerColumns);
            ContentHost.Controls.Add(_designerBanner);
            ContentHost.ResumeLayout(false);
            ClientSize = new Size(640, 700);
            Name = "frmImportData";
            Text = "Nhập dữ liệu";
            ResumeLayout(false);
        }
    }
}

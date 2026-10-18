using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace SIMS_WinFormsApp.Views.Dialogs
{
    partial class BaseFormDialogForm
    {
        private void InitializeComponent()
        {
            _headerPanel = new Panel();
            _headerIconHolder = new Panel();
            _headerIconBox = new IconPictureBox();
            _titleLabel = new Label();
            _closeButton = new DialogCloseButton();
            _bodyScrollPanel = new Panel();
            _footerPanel = new Panel();
            _designerPreviewContent = new Panel();
            _designerPreviewFooter = new Panel();
            _designerPreviewHeading = new Label();
            _designerPreviewDescription = new Label();
            _designerPreviewFieldLabel = new Label();
            _designerPreviewTextBox = new TextBox();
            _designerPreviewCancelButton = new Button();
            _designerPreviewSaveButton = new Button();
            SuspendLayout();
            _headerPanel.SuspendLayout();
            _headerIconHolder.SuspendLayout();
            _bodyScrollPanel.SuspendLayout();
            _footerPanel.SuspendLayout();

            _headerPanel.BackColor = Color.White;
            _headerPanel.Dock = DockStyle.Top;
            _headerPanel.Height = 64;
            _headerPanel.Name = "headerPanel";

            _headerIconHolder.BackColor = Color.Transparent;
            _headerIconHolder.Location = new Point(20, 12);
            _headerIconHolder.Name = "headerIconHolder";
            _headerIconHolder.Size = new Size(40, 40);
            _headerIconHolder.Paint += new PaintEventHandler(HeaderIconHolder_Paint);

            _headerIconBox.BackColor = Color.Transparent;
            _headerIconBox.IconChar = IconChar.UserPen;
            _headerIconBox.IconColor = System.Drawing.Color.DimGray;
            _headerIconBox.IconSize = 20;
            _headerIconBox.Location = new Point(9, 9);
            _headerIconBox.Name = "headerIcon";
            _headerIconBox.Size = new Size(22, 22);
            _headerIconBox.SizeMode = PictureBoxSizeMode.CenterImage;
            _headerIconHolder.Controls.Add(_headerIconBox);

            _titleLabel.AutoSize = true;
            _titleLabel.BackColor = Color.Transparent;
            _titleLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            _titleLabel.ForeColor = Color.FromArgb(15, 23, 42);
            _titleLabel.Location = new Point(72, 22);
            _titleLabel.Name = "titleLabel";
            _titleLabel.Text = "Hộp thoại";
            _titleLabel.UseMnemonic = false;
            _headerPanel.Controls.Add(_titleLabel);

            _closeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _closeButton.Location = new Point(564, 12);
            _closeButton.Name = "closeButton";
            _closeButton.Size = new Size(40, 40);
            _closeButton.Click += new System.EventHandler(CloseButton_Click);
            _headerPanel.Controls.Add(_closeButton);
            _headerPanel.Controls.Add(_headerIconHolder);

            _bodyScrollPanel.AutoScroll = true;
            _bodyScrollPanel.BackColor = Color.White;
            _bodyScrollPanel.Dock = DockStyle.Fill;
            _bodyScrollPanel.Name = "bodyScrollPanel";
            _bodyScrollPanel.Padding = new Padding(28, 20, 28, 20);

            _designerPreviewContent.BackColor = Color.White;
            _designerPreviewContent.Dock = DockStyle.Top;
            _designerPreviewContent.Height = 190;
            _designerPreviewContent.Name = "designerPreviewContent";

            _designerPreviewHeading.AutoSize = true;
            _designerPreviewHeading.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            _designerPreviewHeading.ForeColor = Color.FromArgb(30, 41, 59);
            _designerPreviewHeading.Location = new Point(0, 4);
            _designerPreviewHeading.Name = "designerPreviewHeading";
            _designerPreviewHeading.Text = "Thông tin mẫu";

            _designerPreviewDescription.AutoSize = true;
            _designerPreviewDescription.Font = new Font("Segoe UI", 9F);
            _designerPreviewDescription.ForeColor = Color.FromArgb(100, 116, 139);
            _designerPreviewDescription.Location = new Point(0, 32);
            _designerPreviewDescription.Name = "designerPreviewDescription";
            _designerPreviewDescription.Text = "Khu vực xem trước nội dung hộp thoại.";

            _designerPreviewFieldLabel.AutoSize = true;
            _designerPreviewFieldLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _designerPreviewFieldLabel.ForeColor = Color.FromArgb(51, 65, 85);
            _designerPreviewFieldLabel.Location = new Point(0, 76);
            _designerPreviewFieldLabel.Name = "designerPreviewFieldLabel";
            _designerPreviewFieldLabel.Text = "Tên hiển thị";

            _designerPreviewTextBox.Font = new Font("Segoe UI", 9.5F);
            _designerPreviewTextBox.Location = new Point(0, 101);
            _designerPreviewTextBox.Name = "designerPreviewTextBox";
            _designerPreviewTextBox.ReadOnly = true;
            _designerPreviewTextBox.Size = new Size(360, 24);
            _designerPreviewTextBox.Text = "Nguyễn Văn An";

            _designerPreviewContent.Controls.Add(_designerPreviewTextBox);
            _designerPreviewContent.Controls.Add(_designerPreviewFieldLabel);
            _designerPreviewContent.Controls.Add(_designerPreviewDescription);
            _designerPreviewContent.Controls.Add(_designerPreviewHeading);
            _bodyScrollPanel.Controls.Add(_designerPreviewContent);

            _footerPanel.BackColor = Color.FromArgb(248, 250, 252);
            _footerPanel.Dock = DockStyle.Bottom;
            _footerPanel.Height = 76;
            _footerPanel.Name = "footerPanel";
            _footerPanel.Paint += new PaintEventHandler(FooterPanel_Paint);

            _designerPreviewFooter.BackColor = Color.Transparent;
            _designerPreviewFooter.Dock = DockStyle.Fill;
            _designerPreviewFooter.Name = "designerPreviewFooter";

            _designerPreviewCancelButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _designerPreviewCancelButton.BackColor = Color.White;
            _designerPreviewCancelButton.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            _designerPreviewCancelButton.FlatStyle = FlatStyle.Flat;
            _designerPreviewCancelButton.Font = new Font("Segoe UI", 9F);
            _designerPreviewCancelButton.ForeColor = Color.FromArgb(51, 65, 85);
            _designerPreviewCancelButton.Location = new Point(342, 17);
            _designerPreviewCancelButton.Name = "designerPreviewCancelButton";
            _designerPreviewCancelButton.Size = new Size(104, 40);
            _designerPreviewCancelButton.Text = "Hủy";
            _designerPreviewCancelButton.UseVisualStyleBackColor = false;

            _designerPreviewSaveButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _designerPreviewSaveButton.BackColor = Color.FromArgb(37, 99, 235);
            _designerPreviewSaveButton.FlatAppearance.BorderSize = 0;
            _designerPreviewSaveButton.FlatStyle = FlatStyle.Flat;
            _designerPreviewSaveButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _designerPreviewSaveButton.ForeColor = Color.White;
            _designerPreviewSaveButton.Location = new Point(458, 17);
            _designerPreviewSaveButton.Name = "designerPreviewSaveButton";
            _designerPreviewSaveButton.Size = new Size(104, 40);
            _designerPreviewSaveButton.Text = "Lưu";
            _designerPreviewSaveButton.UseVisualStyleBackColor = false;

            _designerPreviewFooter.Controls.Add(_designerPreviewSaveButton);
            _designerPreviewFooter.Controls.Add(_designerPreviewCancelButton);
            _footerPanel.Controls.Add(_designerPreviewFooter);

            Controls.Add(_bodyScrollPanel);
            Controls.Add(_footerPanel);
            Controls.Add(_headerPanel);
            _footerPanel.ResumeLayout(false);
            _bodyScrollPanel.ResumeLayout(true);
            _headerIconHolder.ResumeLayout(false);
            _headerPanel.ResumeLayout(false);
            _headerPanel.PerformLayout();
            ClientSize = new Size(620, 720);
            Name = "BaseFormDialogForm";
            Text = string.Empty;
            ResumeLayout(false);
        }
    }
}

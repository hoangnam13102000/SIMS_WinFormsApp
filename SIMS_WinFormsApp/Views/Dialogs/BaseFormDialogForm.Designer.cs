using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace SIMS_WinFormsApp.UI.Controls
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
            _designPreviewLabel = new Label();
            _footerPanel = new Panel();
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
            _designPreviewLabel.Dock = DockStyle.Fill;
            _designPreviewLabel.ForeColor = Color.FromArgb(148, 163, 184);
            _designPreviewLabel.Text = "Vùng nội dung của hộp thoại";
            _designPreviewLabel.TextAlign = ContentAlignment.MiddleCenter;
            _designPreviewLabel.Name = "designPreviewLabel";
            _bodyScrollPanel.Controls.Add(_designPreviewLabel);

            _footerPanel.BackColor = Color.FromArgb(248, 250, 252);
            _footerPanel.Dock = DockStyle.Bottom;
            _footerPanel.Height = 76;
            _footerPanel.Name = "footerPanel";
            _footerPanel.Paint += new PaintEventHandler(FooterPanel_Paint);

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

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
            _footerPanel = new Panel();
            SuspendLayout();
            _headerPanel.SuspendLayout();
            _headerIconHolder.SuspendLayout();
            _bodyScrollPanel.SuspendLayout();
            _footerPanel.SuspendLayout();

            _headerPanel.BackColor = Color.Transparent;
            _headerPanel.Dock = DockStyle.Top;
            _headerPanel.Height = HeaderHeight;
            _headerPanel.Name = "headerPanel";

            _headerIconHolder.BackColor = Color.Transparent;
            _headerIconHolder.Location = new Point(20, (HeaderHeight - HeaderIconContainerSize) / 2);
            _headerIconHolder.Name = "headerIconHolder";
            _headerIconHolder.Size = new Size(HeaderIconContainerSize, HeaderIconContainerSize);
            _headerIconHolder.Paint += new PaintEventHandler(HeaderIconHolder_Paint);

            _headerIconBox.BackColor = Color.Transparent;
            _headerIconBox.IconChar = IconChar.UserPen;
            _headerIconBox.IconColor = System.Drawing.Color.DimGray;
            _headerIconBox.IconSize = HeaderIconSize;
            _headerIconBox.Location = new Point(
                (HeaderIconContainerSize - HeaderIconSize - 2) / 2,
                (HeaderIconContainerSize - HeaderIconSize - 2) / 2);
            _headerIconBox.Name = "headerIcon";
            _headerIconBox.Size = new Size(HeaderIconSize + 2, HeaderIconSize + 2);
            _headerIconBox.SizeMode = PictureBoxSizeMode.CenterImage;
            _headerIconHolder.Controls.Add(_headerIconBox);

            _titleLabel.AutoSize = true;
            _titleLabel.BackColor = Color.Transparent;
            _titleLabel.Location = new Point(_headerIconHolder.Right + 12, (HeaderHeight - 20) / 2);
            _titleLabel.Name = "titleLabel";
            _titleLabel.UseMnemonic = false;
            _headerPanel.Controls.Add(_titleLabel);

            _closeButton.Location = new Point(0, (HeaderHeight - CloseButtonSize) / 2);
            _closeButton.Name = "closeButton";
            _closeButton.Size = new Size(CloseButtonSize, CloseButtonSize);
            _closeButton.Click += new System.EventHandler(CloseButton_Click);
            _headerPanel.Controls.Add(_closeButton);
            _headerPanel.Controls.Add(_headerIconHolder);

            _bodyScrollPanel.AutoScroll = true;
            _bodyScrollPanel.BackColor = SystemColors.Window;
            _bodyScrollPanel.Dock = DockStyle.Fill;
            _bodyScrollPanel.Name = "bodyScrollPanel";
            _bodyScrollPanel.Padding = new Padding(ContentPadding, 20, ContentPadding, 20);

            _footerPanel.BackColor = Color.Transparent;
            _footerPanel.Dock = DockStyle.Bottom;
            _footerPanel.Height = FooterHeight;
            _footerPanel.Name = "footerPanel";
            _footerPanel.Paint += new PaintEventHandler(FooterPanel_Paint);

            Controls.Add(_bodyScrollPanel);
            Controls.Add(_footerPanel);
            Controls.Add(_headerPanel);
            _footerPanel.ResumeLayout(false);
            _bodyScrollPanel.ResumeLayout(false);
            _headerIconHolder.ResumeLayout(false);
            _headerPanel.ResumeLayout(false);
            _headerPanel.PerformLayout();
            ClientSize = DefaultDialogSize;
            Name = "BaseFormDialogForm";
            Text = string.Empty;
            ResumeLayout(false);
        }
    }
}

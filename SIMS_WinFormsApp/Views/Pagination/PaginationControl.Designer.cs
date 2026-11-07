using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Pagination
{
    partial class PaginationControl
    {
        private Panel _centerContainer;
        private FlowLayoutPanel _leftFlow;
        private FlowLayoutPanel _pagesFlow;
        private Label _pageLabel;
        private Panel _rightPanel;
        private FlowLayoutPanel _rightFlow;
        private Label _pageSizeLabel;
        private ComboBox _pageSizeBox;
        private Label _unitLabel;

        private void InitializeComponent()
        {
            _centerContainer = new Panel();
            _leftFlow = new FlowLayoutPanel();
            _pagesFlow = new FlowLayoutPanel();
            _pageLabel = new Label();
            _rightPanel = new Panel();
            _rightFlow = new FlowLayoutPanel();
            _pageSizeLabel = new Label();
            _pageSizeBox = new ComboBox();
            _unitLabel = new Label();
            _centerContainer.SuspendLayout();
            _leftFlow.SuspendLayout();
            _rightPanel.SuspendLayout();
            _rightFlow.SuspendLayout();
            SuspendLayout();

            _pagesFlow.AutoSize = true;
            _pagesFlow.FlowDirection = FlowDirection.LeftToRight;
            _pagesFlow.Margin = Padding.Empty;
            _pagesFlow.Name = "_pagesFlow";
            _pagesFlow.WrapContents = false;

            _pageLabel.AutoSize = true;
            _pageLabel.Font = InfoFont;
            _pageLabel.ForeColor = AppColors.TextMuted;
            _pageLabel.Margin = new Padding(14, 12, 0, 0);
            _pageLabel.Name = "_pageLabel";
            _pageLabel.TextAlign = ContentAlignment.MiddleLeft;

            _leftFlow.AutoSize = true;
            _leftFlow.Controls.Add(_pagesFlow);
            _leftFlow.Controls.Add(_pageLabel);
            _leftFlow.FlowDirection = FlowDirection.LeftToRight;
            _leftFlow.Margin = Padding.Empty;
            _leftFlow.Name = "_leftFlow";
            _leftFlow.WrapContents = false;
            _leftFlow.SizeChanged += LeftFlow_SizeChanged;

            _centerContainer.Controls.Add(_leftFlow);
            _centerContainer.Dock = DockStyle.Fill;
            _centerContainer.Margin = Padding.Empty;
            _centerContainer.Name = "_centerContainer";
            _centerContainer.Resize += CenterContainer_Resize;

            _pageSizeLabel.AutoSize = true;
            _pageSizeLabel.Font = InfoFont;
            _pageSizeLabel.ForeColor = AppColors.TextMuted;
            _pageSizeLabel.Margin = new Padding(0, 8, 8, 0);
            _pageSizeLabel.Name = "_pageSizeLabel";
            _pageSizeLabel.Text = "Hiển thị:";
            _pageSizeLabel.TextAlign = ContentAlignment.MiddleLeft;

            _pageSizeBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _pageSizeBox.Font = AppFonts.Input;
            _pageSizeBox.Margin = new Padding(0, 2, 8, 0);
            _pageSizeBox.Name = "_pageSizeBox";
            _pageSizeBox.Size = new Size(76, 32);
            _pageSizeBox.SelectedIndexChanged += PageSizeBox_SelectedIndexChanged;

            _unitLabel.AutoSize = true;
            _unitLabel.Font = InfoFont;
            _unitLabel.ForeColor = AppColors.TextMuted;
            _unitLabel.Margin = new Padding(0, 8, 0, 0);
            _unitLabel.Name = "_unitLabel";
            _unitLabel.Text = "dòng";
            _unitLabel.TextAlign = ContentAlignment.MiddleLeft;

            _rightFlow.AutoSize = true;
            _rightFlow.Controls.Add(_pageSizeLabel);
            _rightFlow.Controls.Add(_pageSizeBox);
            _rightFlow.Controls.Add(_unitLabel);
            _rightFlow.FlowDirection = FlowDirection.LeftToRight;
            _rightFlow.Margin = Padding.Empty;
            _rightFlow.Name = "_rightFlow";
            _rightFlow.WrapContents = false;

            _rightPanel.AutoSize = true;
            _rightPanel.Controls.Add(_rightFlow);
            _rightPanel.Dock = DockStyle.Right;
            _rightPanel.Margin = Padding.Empty;
            _rightPanel.Name = "_rightPanel";
            _rightPanel.Padding = new Padding(0, 4, 0, 0);

            AutoScaleMode = AutoScaleMode.None;
            BackColor = AppColors.White;
            Dock = DockStyle.Fill;
            Controls.Add(_centerContainer);
            Controls.Add(_rightPanel);
            Name = "PaginationControl";
            _rightFlow.ResumeLayout(false);
            _rightFlow.PerformLayout();
            _rightPanel.ResumeLayout(false);
            _rightPanel.PerformLayout();
            _leftFlow.ResumeLayout(false);
            _leftFlow.PerformLayout();
            _centerContainer.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}

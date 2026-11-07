using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Pagination
{
    partial class PaginationControl
    {
        private void InitializeComponent()
        {
            this._centerContainer = new System.Windows.Forms.Panel();
            this._leftFlow = new System.Windows.Forms.FlowLayoutPanel();
            this._pagesFlow = new System.Windows.Forms.FlowLayoutPanel();
            this._pageLabel = new System.Windows.Forms.Label();
            this._pageSizePanel = new System.Windows.Forms.Panel();
            this._pageSizeFlow = new System.Windows.Forms.FlowLayoutPanel();
            this._pageSizeLabel = new System.Windows.Forms.Label();
            this._pageSizeBox = new System.Windows.Forms.ComboBox();
            this._pageSizeUnitLabel = new System.Windows.Forms.Label();
            this._centerContainer.SuspendLayout();
            this._leftFlow.SuspendLayout();
            this._pageSizePanel.SuspendLayout();
            this._pageSizeFlow.SuspendLayout();
            this.SuspendLayout();
            // 
            // _centerContainer
            // 
            this._centerContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._centerContainer.Margin = System.Windows.Forms.Padding.Empty;
            this._centerContainer.Controls.Add(this._leftFlow);
            // 
            // _leftFlow
            // 
            this._leftFlow.AutoSize = true;
            this._leftFlow.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this._leftFlow.Margin = System.Windows.Forms.Padding.Empty;
            this._leftFlow.WrapContents = false;
            this._leftFlow.Controls.Add(this._pagesFlow);
            this._leftFlow.Controls.Add(this._pageLabel);
            // 
            // _pagesFlow
            // 
            this._pagesFlow.AutoSize = true;
            this._pagesFlow.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this._pagesFlow.Margin = System.Windows.Forms.Padding.Empty;
            this._pagesFlow.WrapContents = false;
            // 
            // _pageLabel
            // 
            this._pageLabel.AutoSize = true;
            this._pageLabel.Font = InfoFont;
            this._pageLabel.ForeColor = AppColors.TextMuted;
            this._pageLabel.Margin = new System.Windows.Forms.Padding(14, 12, 0, 0);
            this._pageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _pageSizePanel
            // 
            this._pageSizePanel.AutoSize = true;
            this._pageSizePanel.Dock = System.Windows.Forms.DockStyle.Right;
            this._pageSizePanel.Margin = System.Windows.Forms.Padding.Empty;
            this._pageSizePanel.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this._pageSizePanel.Controls.Add(this._pageSizeFlow);
            // 
            // _pageSizeFlow
            // 
            this._pageSizeFlow.AutoSize = true;
            this._pageSizeFlow.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this._pageSizeFlow.Margin = System.Windows.Forms.Padding.Empty;
            this._pageSizeFlow.WrapContents = false;
            this._pageSizeFlow.Controls.Add(this._pageSizeLabel);
            this._pageSizeFlow.Controls.Add(this._pageSizeBox);
            this._pageSizeFlow.Controls.Add(this._pageSizeUnitLabel);
            // 
            // _pageSizeLabel
            // 
            this._pageSizeLabel.AutoSize = true;
            this._pageSizeLabel.Font = InfoFont;
            this._pageSizeLabel.ForeColor = AppColors.TextMuted;
            this._pageSizeLabel.Margin = new System.Windows.Forms.Padding(0, 8, 8, 0);
            this._pageSizeLabel.Text = "Hiển thị:";
            this._pageSizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _pageSizeBox
            // 
            this._pageSizeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._pageSizeBox.Font = AppFonts.Input;
            this._pageSizeBox.Margin = new System.Windows.Forms.Padding(0, 2, 8, 0);
            this._pageSizeBox.Size = new System.Drawing.Size(76, 32);
            this._pageSizeBox.SelectedIndexChanged += new System.EventHandler(this._pageSizeBox_SelectedIndexChanged);
            // 
            // _pageSizeUnitLabel
            // 
            this._pageSizeUnitLabel.AutoSize = true;
            this._pageSizeUnitLabel.Font = InfoFont;
            this._pageSizeUnitLabel.ForeColor = AppColors.TextMuted;
            this._pageSizeUnitLabel.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this._pageSizeUnitLabel.Text = "dòng";
            this._pageSizeUnitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PaginationControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = AppColors.White;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Controls.Add(this._centerContainer);
            this.Controls.Add(this._pageSizePanel);
            this._centerContainer.Resize += new System.EventHandler(this._centerContainer_Resize);
            this._leftFlow.SizeChanged += new System.EventHandler(this._leftFlow_SizeChanged);
            this._pageSizeFlow.ResumeLayout(false);
            this._pageSizePanel.ResumeLayout(false);
            this._leftFlow.ResumeLayout(false);
            this._leftFlow.PerformLayout();
            this._centerContainer.ResumeLayout(false);
            this._centerContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Panel _centerContainer;
        private FlowLayoutPanel _leftFlow;
        private FlowLayoutPanel _pagesFlow;
        private Label _pageLabel;
        private Panel _pageSizePanel;
        private FlowLayoutPanel _pageSizeFlow;
        private Label _pageSizeLabel;
        private ComboBox _pageSizeBox;
        private Label _pageSizeUnitLabel;
    }
}
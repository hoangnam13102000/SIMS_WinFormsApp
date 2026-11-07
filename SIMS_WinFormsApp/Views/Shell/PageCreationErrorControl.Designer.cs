using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Shell
{
    partial class PageCreationErrorControl
    {
        private System.ComponentModel.IContainer components = null;
        private Label _errorLabel;
        private TextBox _errorDetails;
        private Button _retryButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this._errorLabel = new System.Windows.Forms.Label();
            this._errorDetails = new System.Windows.Forms.TextBox();
            this._retryButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // _errorLabel
            //
            this._errorLabel.AutoSize = false;
            this._errorLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._errorLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this._errorLabel.Height = 56;
            this._errorLabel.Name = "_errorLabel";
            this._errorLabel.Padding = new System.Windows.Forms.Padding(20);
            this._errorLabel.Size = new System.Drawing.Size(500, 56);
            this._errorLabel.TabIndex = 0;
            this._errorLabel.Text = "Không thể mở trang này.";
            //
            // _errorDetails
            //
            this._errorDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._errorDetails.Location = new System.Drawing.Point(20, 62);
            this._errorDetails.MinimumSize = new System.Drawing.Size(300, 0);
            this._errorDetails.Multiline = true;
            this._errorDetails.Name = "_errorDetails";
            this._errorDetails.ReadOnly = true;
            this._errorDetails.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._errorDetails.Size = new System.Drawing.Size(460, 220);
            this._errorDetails.TabIndex = 1;
            this._errorDetails.WordWrap = false;
            //
            // _retryButton
            //
            this._retryButton.AutoSize = true;
            this._retryButton.Location = new System.Drawing.Point(20, 290);
            this._retryButton.Name = "_retryButton";
            this._retryButton.Size = new System.Drawing.Size(75, 23);
            this._retryButton.TabIndex = 2;
            this._retryButton.Text = "Thử lại";
            this._retryButton.UseVisualStyleBackColor = true;
            this._retryButton.Click += new System.EventHandler(this.RetryButton_Click);
            //
            // PageCreationErrorControl
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this._errorLabel);
            this.Controls.Add(this._errorDetails);
            this.Controls.Add(this._retryButton);
            this.Name = "PageCreationErrorControl";
            this.Size = new System.Drawing.Size(500, 324);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

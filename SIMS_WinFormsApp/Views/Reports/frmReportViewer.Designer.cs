using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace SIMS_WinFormsApp.Views.Reports
{
    partial class frmReportViewer
    {
        private System.ComponentModel.IContainer components = null;
        private ReportViewer _reportViewer;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this._reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            this._reportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._reportViewer.Location = new System.Drawing.Point(0, 0);
            this._reportViewer.Name = "_reportViewer";
            this._reportViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            this._reportViewer.Size = new System.Drawing.Size(1100, 760);
            this._reportViewer.TabIndex = 0;
            this._reportViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 760);
            this.Controls.Add(this._reportViewer);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frmReportViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Báo cáo";
            this.ResumeLayout(false);
        }
    }
}

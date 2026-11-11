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
            // 
            // _reportViewer
            // 
            this._reportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._reportViewer.Location = new System.Drawing.Point(0, 0);
            this._reportViewer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._reportViewer.Name = "_reportViewer";
            this._reportViewer.ServerReport.BearerToken = null;
            this._reportViewer.Size = new System.Drawing.Size(978, 608);
            this._reportViewer.TabIndex = 0;
            this._reportViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            this._reportViewer.ReportExport += new Microsoft.Reporting.WinForms.ExportEventHandler(this.ReportViewer_ReportExport);
            // 
            // frmReportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 608);
            this.Controls.Add(this._reportViewer);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(802, 489);
            this.Name = "frmReportViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Báo cáo";
            this.Load += new System.EventHandler(this.frmReportViewer_Load);
            this.ResumeLayout(false);

        }
    }
}

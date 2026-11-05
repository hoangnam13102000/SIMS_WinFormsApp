using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace SIMS_WinFormsApp.Views.Reports
{
    public partial class frmReportViewer : Form
    {
        public frmReportViewer(
            string title,
            string embeddedReportName,
            IEnumerable<ReportDataSource> dataSources,
            IDictionary<string, string> parameters)
        {
            InitializeComponent();
            Text = string.IsNullOrWhiteSpace(title) ? "Báo cáo" : title;

            using (Stream stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream(embeddedReportName))
            {
                if (stream == null)
                    throw new FileNotFoundException(
                        "Không tìm thấy mẫu RDLC được nhúng: " + embeddedReportName,
                        embeddedReportName);

                _reportViewer.LocalReport.DataSources.Clear();
                if (dataSources != null)
                {
                    foreach (ReportDataSource dataSource in dataSources)
                    {
                        if (dataSource != null)
                            _reportViewer.LocalReport.DataSources.Add(dataSource);
                    }
                }

                _reportViewer.LocalReport.LoadReportDefinition(stream);
            }

            _reportViewer.ReportExport += ReportViewer_ReportExport;

            if (parameters != null)
            {
                var reportParameters = new List<ReportParameter>();
                foreach (KeyValuePair<string, string> parameter in parameters)
                {
                    string value = string.IsNullOrWhiteSpace(parameter.Value)
                        ? GetParameterFallback(parameter.Key)
                        : parameter.Value;
                    reportParameters.Add(new ReportParameter(parameter.Key, value));
                }

                _reportViewer.LocalReport.SetParameters(reportParameters);
            }

            _reportViewer.RefreshReport();
        }

        private void ReportViewer_ReportExport(object sender, ReportExportEventArgs e)
        {
            e.Cancel = true;

            string description;
            string fileExtension = GetFileExtension(e.Extension.Name, out description);
            using (var dialog = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = fileExtension,
                Filter = description + " (*." + fileExtension + ")|*." + fileExtension
                    + "|Tất cả tệp (*.*)|*.*",
                FileName = GetSafeReportFileName() + "." + fileExtension,
                OverwritePrompt = true,
                Title = "Lưu báo cáo"
            })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;

                try
                {
                    string mimeType;
                    string encoding;
                    string renderedExtension;
                    string[] streams;
                    Warning[] warnings;
                    byte[] content = _reportViewer.LocalReport.Render(
                        e.Extension.Name,
                        e.DeviceInfo,
                        out mimeType,
                        out encoding,
                        out renderedExtension,
                        out streams,
                        out warnings);
                    File.WriteAllBytes(dialog.FileName, content);

                    string title = string.Equals(e.Extension.Name, "PDF", StringComparison.OrdinalIgnoreCase)
                        ? "Xuất PDF thành công"
                        : "Lưu báo cáo thành công";
                    MessageBox.Show(this, "Đã lưu báo cáo thành công tại:\r\n" + dialog.FileName,
                        title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (LocalProcessingException exception)
                {
                    ShowExportError(exception.Message);
                }
                catch (IOException exception)
                {
                    ShowExportError(exception.Message);
                }
                catch (UnauthorizedAccessException exception)
                {
                    ShowExportError(exception.Message);
                }
                catch (InvalidOperationException exception)
                {
                    ShowExportError(exception.Message);
                }
            }
        }

        private static string GetFileExtension(string format, out string description)
        {
            switch (format.ToUpperInvariant())
            {
                case "PDF":
                    description = "Tệp PDF";
                    return "pdf";
                case "EXCELOPENXML":
                    description = "Tệp Excel";
                    return "xlsx";
                case "EXCEL":
                    description = "Tệp Excel";
                    return "xls";
                case "WORDOPENXML":
                    description = "Tệp Word";
                    return "docx";
                case "WORD":
                    description = "Tệp Word";
                    return "doc";
                case "CSV":
                    description = "Tệp CSV";
                    return "csv";
                case "XML":
                    description = "Tệp XML";
                    return "xml";
                default:
                    description = "Tệp báo cáo";
                    return format.ToLowerInvariant();
            }
        }

        private string GetSafeReportFileName()
        {
            string name = Text;
            foreach (char invalidCharacter in Path.GetInvalidFileNameChars())
                name = name.Replace(invalidCharacter, '_');
            return name + "_" + DateTime.Now.ToString("yyyyMMdd_HHmm");
        }

        private void ShowExportError(string details)
        {
            MessageBox.Show(this, "Không thể lưu hoặc xuất báo cáo.\r\n\r\n" + details,
                "Lỗi báo cáo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static string GetParameterFallback(string name)
        {
            switch (name)
            {
                case "TuNgay":
                case "DenNgay":
                case "NgayLap":
                    return DateTime.Today.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                case "TenCuaHang":
                    return "Chưa cấu hình tên cửa hàng";
                case "TenDanhMuc":
                    return "Tất cả danh mục";
                case "NguoiLap":
                    return "Hệ thống";
                case "GiamDoc":
                case "QuanLyKho":
                    return "Chưa phân công";
                default:
                    return "Theo khoảng ngày";
            }
        }
    }
}

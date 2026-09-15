using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.UI.Controls;

namespace SIMS_WinFormsApp.Services.Implementations.Export
{
    public static class TableExportRunner
    {
        public static void Run(
            IWin32Window owner,
            ITableDataExporter exporter,
            string entityLabel,
            Func<string[]> getHeaders,
            Func<IReadOnlyList<object[]>> fetchRows)
        {
            if (exporter == null) throw new ArgumentNullException(nameof(exporter));
            if (getHeaders == null) throw new ArgumentNullException(nameof(getHeaders));
            if (fetchRows == null) throw new ArgumentNullException(nameof(fetchRows));

            string defaultName = SanitizeFileName(entityLabel) + "_" + Timestamp() + "." + exporter.FileExtension;
            using (var dialog = new SaveFileDialog { Filter = exporter.SaveDialogFilter, FileName = defaultName })
            {
                if (dialog.ShowDialog(owner) != DialogResult.OK) return;
                string filePath = dialog.FileName;
                string[] headers = getHeaders();

                int rowCount = 0;
                Exception failure = null;

                var worker = new BackgroundWorker();
                worker.DoWork += (s, e) =>
                {
                    try
                    {
                        var rows = fetchRows() ?? new List<object[]>();
                        exporter.Export(filePath, entityLabel, headers, rows);
                        rowCount = rows.Count;
                    }
                    catch (Exception ex)
                    {
                        failure = ex;
                    }
                };
                worker.RunWorkerCompleted += (s, e) =>
                {
                    if (failure != null)
                    {
                        DialogHelper.ShowError(owner, "Lỗi", "Xuất file thất bại: " + failure.Message);
                        return;
                    }
                    if (rowCount == 0)
                    {
                        DialogHelper.ShowInfo(owner, "Không có dữ liệu",
                            "Đã tạo file \"" + Path.GetFileName(filePath) + "\" nhưng không có dòng dữ liệu nào để xuất.");
                    }
                    else
                    {
                        DialogHelper.ShowSuccess(owner, "Thành công",
                            "Đã xuất " + rowCount + " dòng vào file \"" + Path.GetFileName(filePath) + "\"");
                    }
                };
                worker.RunWorkerAsync();
            }
        }

        private static string SanitizeFileName(string name)
        {
            string cleaned = Regex.Replace(name ?? string.Empty, @"[^\p{L}\p{N}]+", "_");
            return cleaned.Length == 0 ? "du_lieu" : cleaned;
        }

        private static string Timestamp() => DateTime.Now.ToString("yyyyMMdd_HHmmss");
    }
}
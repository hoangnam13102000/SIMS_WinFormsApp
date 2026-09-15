using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Services.Implementations.Import;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.MVP.Presenters
{
    public sealed class ImportDataPresenter
    {
        private readonly IImportDataView _view;
        private readonly IReadOnlyList<ITableDataImporter> _importers;
        private readonly string[] _expectedColumns;
        private readonly ImportRowHandler _rowHandler;

        private string _selectedFilePath;

        public ImportDataPresenter(
            IImportDataView view,
            IReadOnlyList<ITableDataImporter> importers,
            string[] expectedColumns,
            ImportRowHandler rowHandler)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _importers = importers ?? throw new ArgumentNullException(nameof(importers));
            _expectedColumns = expectedColumns ?? Array.Empty<string>();
            _rowHandler = rowHandler ?? throw new ArgumentNullException(nameof(rowHandler));

            _view.FileChosen += OnFileChosen;
            _view.StartImportRequested += OnStartImportRequested;
            _view.SetStartEnabled(false);
        }

        private void OnFileChosen(object sender, string filePath)
        {
            var importer = _importers.FirstOrDefault(i => i.CanHandle(filePath));
            if (importer == null)
            {
                _view.ShowValidationError("File không hợp lệ", "Chỉ chấp nhận file .xlsx hoặc .csv.");
                _view.SetStartEnabled(false);
                return;
            }

            _selectedFilePath = filePath;
            long size = new FileInfo(filePath).Length;
            _view.SetSelectedFileLabel(Path.GetFileName(filePath) + " (" + FormatSize(size) + ")");
            _view.SetStartEnabled(true);
        }

        private async void OnStartImportRequested(object sender, EventArgs e)
        {
            if (_selectedFilePath == null) return;

            _view.SetChooseFileEnabled(false);
            _view.SetStartEnabled(false);
            _view.SetBusy(true, "Đang xử lý...");

            try
            {
                var summary = await Task.Run(() => RunImport(_selectedFilePath));
                _view.SetBusy(false, null);
                _view.ShowResult(summary.SuccessCount, summary.Errors);
            }
            catch (Exception ex)
            {
                _view.SetBusy(false, null);
                _view.ShowValidationError("Không thể nhập file", ex.Message);
            }
            finally
            {
                _view.SetChooseFileEnabled(true);
                _view.SetStartEnabled(true);
            }
        }

        private ImportSummaryDto RunImport(string filePath)
        {
            var importer = _importers.First(i => i.CanHandle(filePath));
            var allRows = importer.ReadRows(filePath);

            var errors = new List<string>();
            int successCount = 0;

            if (allRows.Count == 0)
                return new ImportSummaryDto { SuccessCount = 0, Errors = errors };

            string[] fileHeader = allRows[0];
            int[] mapping = ImportColumnMapper.BuildMapping(_expectedColumns, fileHeader);

            for (int i = 1; i < allRows.Count; i++)
            {
                string[] raw = allRows[i];
                if (IsBlankRow(raw)) continue;

                string[] mapped = mapping != null ? ImportColumnMapper.ApplyMapping(mapping, raw) : raw;
                int displayRowNumber = i + 1; // +1 vì dòng 1 là tiêu đề

                ImportRowResult result;
                try
                {
                    result = _rowHandler(mapped, displayRowNumber);
                }
                catch (Exception ex)
                {
                    result = ImportRowResult.Failure("Dòng " + displayRowNumber + ": " + ex.Message);
                }

                if (result.IsSuccess) successCount++;
                else errors.Add(result.ErrorMessage ?? ("Dòng " + displayRowNumber + ": lỗi không xác định"));
            }

            return new ImportSummaryDto { SuccessCount = successCount, Errors = errors };
        }

        private static bool IsBlankRow(string[] cells)
        {
            if (cells == null) return true;
            foreach (var cell in cells)
                if (!string.IsNullOrWhiteSpace(cell)) return false;
            return true;
        }

        private static string FormatSize(long bytes)
        {
            if (bytes < 1024) return bytes + " B";
            if (bytes < 1024 * 1024) return (bytes / 1024.0).ToString("0.0") + " KB";
            return (bytes / 1024.0 / 1024.0).ToString("0.0") + " MB";
        }

        public void Dispose()
        {
            _view.FileChosen -= OnFileChosen;
            _view.StartImportRequested -= OnStartImportRequested;
        }
    }
}
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.Services.Implementations.Import;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.Views.Toast;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Dialogs;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.Views.SystemManagement
{
    public sealed partial class frmImportData : BaseFormDialogForm, IImportDataView
    {
        private readonly ImportDataPresenter _presenter;
        private bool _hasAnySuccess;

        private Label _lblFileName;
        private TextBox _txtResult;

        public frmImportData()
        {
            InitializeComponent();
            HeaderTitle = "Nhập dữ liệu";
            SetHeaderIcon(IconChar.Upload, AppColors.Accent);
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
        }

        public frmImportData(
            IWin32Window owner,
            string pageTitle,
            string[] importColumns,
            string instructions,
            IReadOnlyList<ITableDataImporter> importers,
            ImportRowHandler rowHandler) : base(owner)
        {
            InitializeComponent();
            _lblFileName = _designerFileName;
            _txtResult = _designerResult;
            if (rowHandler == null) throw new ArgumentNullException(nameof(rowHandler));

            BuildContent(importColumns, instructions);
            HeaderTitle = "Nhập dữ liệu - " + pageTitle;
            SetHeaderIcon(IconChar.Upload, AppColors.Accent);

            CloseRequested += (s, e) =>
            {
                DialogResult = _hasAnySuccess ? DialogResult.OK : DialogResult.Cancel;
                Close();
            };

            _presenter = new ImportDataPresenter(this, importers, importColumns, rowHandler);
        }

        /// <summary>
        /// <c>frmImportData.Show(this, "Nhân viên", columns, instructions, importers, handler);</c>
        /// trả về DialogResult.OK nếu có ít nhất 1 dòng nhập thành công (để nơi gọi Reload lại
        /// bảng) - cùng quy ước với frmAddEmployee.Show(...).
        /// </summary>
        public static DialogResult Show(
            IWin32Window owner,
            string pageTitle,
            string[] importColumns,
            string instructions,
            IReadOnlyList<ITableDataImporter> importers,
            ImportRowHandler rowHandler)
        {
            using (var dialog = new frmImportData(owner, pageTitle, importColumns, instructions, importers, rowHandler))
            {
                return dialog.ShowDialog(owner);
            }
        }

        #region Dựng giao diện
        private void BuildContent(string[] importColumns, string instructions)
        {
            _designerColumns.Text = "Cột cần có: " + string.Join(", ", importColumns ?? Array.Empty<string>());
            _designerInstructions.Text = string.IsNullOrWhiteSpace(instructions) ? string.Empty : instructions;
            _designerInstructions.Visible = !string.IsNullOrWhiteSpace(instructions);
            _designerResult.Visible = false;
            _btnStart.Enabled = false;

            _designerBanner.Icon = IconChar.FileUpload;
            _designerBanner.TitleText = "Nhập dữ liệu từ file";
            _designerBanner.DescriptionText = "Chỉ chấp nhận file .xlsx hoặc .csv, dòng đầu tiên là tiêu đề cột.";
            _designerBanner.Margin = new Padding(0, 0, 0, 16);
        }

        private void ChooseFile()
        {
            using (var dialog = new OpenFileDialog { Filter = DefaultSpreadsheetImporters.CombinedSaveOrOpenFilter })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                FileChosen?.Invoke(this, dialog.FileName);
            }
        }

        private void ChooseFileButton_Click(object sender, EventArgs e)
        {
            ChooseFile();
        }

        private void CloseImportButton_Click(object sender, EventArgs e)
        {
            RaiseCloseRequested();
        }

        private void StartImportButton_Click(object sender, EventArgs e)
        {
            StartImportRequested?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region IImportDataView
        public event EventHandler<string> FileChosen;
        public event EventHandler StartImportRequested;

        public void SetChooseFileEnabled(bool enabled) => _btnChooseFile.Enabled = enabled;

        public void SetSelectedFileLabel(string text)
        {
            _lblFileName.Text = text;
            _lblFileName.ForeColor = AppColors.TextPrimary;
        }

        public void SetStartEnabled(bool enabled) => _btnStart.Enabled = enabled;

        public void SetBusy(bool isBusy, string message)
        {
            _btnClose.Enabled = !isBusy;
            _btnStart.Enabled = !isBusy;
            _btnStart.Text = isBusy ? (message ?? "Đang xử lý...") : "Bắt đầu nhập";
            Cursor = isBusy ? Cursors.WaitCursor : Cursors.Default;
        }

        public void ShowValidationError(string title, string message) => AppToast.Error(this, title, message);

        public void ShowResult(int successCount, IReadOnlyList<string> errors)
        {
            if (successCount > 0) _hasAnySuccess = true;

            var sb = new StringBuilder();
            sb.Append("Đã nhập thành công ").Append(successCount).Append(" dòng.");
            if (errors.Count > 0)
            {
                sb.Append("\r\nCó ").Append(errors.Count).Append(" dòng lỗi:\r\n");
                int shown = Math.Min(errors.Count, 20);
                for (int i = 0; i < shown; i++) sb.Append("• ").Append(errors[i]).Append("\r\n");
                if (errors.Count > shown) sb.Append("... và ").Append(errors.Count - shown).Append(" lỗi khác.");
            }
            _txtResult.Text = sb.ToString();
            _txtResult.Visible = true;

            if (errors.Count == 0)
                AppToast.Success(this, "Thành công", "Đã nhập " + successCount + " dòng dữ liệu.");
            else if (successCount > 0)
                AppToast.Warning(this, "Hoàn tất một phần",
                    successCount + " dòng thành công, " + errors.Count + " dòng lỗi (xem chi tiết trong hộp kết quả).");
            else
                AppToast.Error(this, "Nhập thất bại", "Không có dòng nào được nhập thành công.");
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing) _presenter?.Dispose();
            base.Dispose(disposing);
        }
    }
}
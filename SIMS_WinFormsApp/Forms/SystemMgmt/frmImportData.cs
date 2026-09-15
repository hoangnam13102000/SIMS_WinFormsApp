using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.Services.Implementations.Import;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.Forms.SystemMgmt
{
    /// <summary>
    /// Popup "Nhập dữ liệu" dùng chung cho mọi trang quản lý (Nhân viên, Sản phẩm,...) - chỉ là
    /// View thuần theo MVP; toàn bộ logic đọc file/đối chiếu cột/gọi rowHandler nằm ở
    /// ImportDataPresenter. Cùng khuôn mẫu BaseFormDialogForm + Show(...) tĩnh với frmAddEmployee.
    /// </summary>
    public sealed class frmImportData : BaseFormDialogForm, IImportDataView
    {
        private readonly ImportDataPresenter _presenter;
        private bool _hasAnySuccess;

        private Label _lblFileName;
        private PrimaryButton _btnChooseFile;
        private PrimaryButton _btnStart;
        private TextBox _txtResult;

        public frmImportData(
            IWin32Window owner,
            string pageTitle,
            string[] importColumns,
            string instructions,
            IReadOnlyList<ITableDataImporter> importers,
            ImportRowHandler rowHandler) : base(owner)
        {
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
            var resultBox = CreateResultBox();
            var fileRow = CreateFileRow();
            var instructionsLabel = CreateInstructionsLabel(instructions);
            var columnsLabel = CreateColumnsLabel(importColumns);
            var banner = CreateBanner();

            // Cùng quy ước với frmAddEmployee: add SAU CÙNG hiển thị TRÊN CÙNG, nên add theo thứ
            // tự NGƯỢC LẠI với thứ tự hiển thị mong muốn (banner -> cột cần có -> ghi chú -> chọn
            // file -> kết quả, từ trên xuống).
            ContentHost.Controls.Add(resultBox);
            ContentHost.Controls.Add(fileRow);
            if (instructionsLabel != null) ContentHost.Controls.Add(instructionsLabel);
            ContentHost.Controls.Add(columnsLabel);
            ContentHost.Controls.Add(banner);

            _txtResult = resultBox;

            AddFooterButton("Đóng", false, (s, e) => RaiseCloseRequested());
            _btnStart = AddFooterButton("Bắt đầu nhập", true, (s, e) => StartImportRequested?.Invoke(this, EventArgs.Empty));
            _btnStart.Enabled = false;
        }

        private static InfoBannerPanel CreateBanner()
        {
            return new InfoBannerPanel
            {
                Icon = IconChar.FileUpload,
                TitleText = "Nhập dữ liệu từ file",
                DescriptionText = "Chỉ chấp nhận file .xlsx hoặc .csv, dòng đầu tiên là tiêu đề cột.",
                Margin = new Padding(0, 0, 0, 16)
            };
        }

        private static Label CreateColumnsLabel(string[] importColumns)
        {
            return new Label
            {
                AutoSize = false,
                Text = "Cột cần có: " + string.Join(", ", importColumns ?? Array.Empty<string>()),
                Font = AppFonts.Body,
                ForeColor = AppColors.TextSecondary,
                Dock = DockStyle.Top,
                Height = 44,
                Margin = new Padding(0, 0, 0, 8)
            };
        }

        private static Label CreateInstructionsLabel(string instructions)
        {
            if (string.IsNullOrWhiteSpace(instructions)) return null;
            return new Label
            {
                AutoSize = false,
                Text = instructions,
                Font = AppFonts.Body,
                ForeColor = AppColors.TextSecondary,
                Dock = DockStyle.Top,
                Height = 40,
                Margin = new Padding(0, 0, 0, 8)
            };
        }

        private Panel CreateFileRow()
        {
            var panel = new Panel { Dock = DockStyle.Top, Height = 46, Margin = new Padding(0, 0, 0, 16) };

            _btnChooseFile = new PrimaryButton
            {
                Text = "Chọn file...",
                IsPrimary = false,
                Size = new Size(140, 42),
                CornerRadius = AppRadius.Medium,
                Font = AppFonts.Button,
                Location = new Point(0, 0)
            };
            _btnChooseFile.Click += (s, e) => ChooseFile();

            _lblFileName = new Label
            {
                AutoSize = false,
                Text = "Chưa chọn file nào",
                Font = AppFonts.Body,
                ForeColor = AppColors.TextMuted,
                Location = new Point(_btnChooseFile.Right + 12, 0),
                Size = new Size(360, 42),
                TextAlign = ContentAlignment.MiddleLeft
            };

            panel.Controls.Add(_btnChooseFile);
            panel.Controls.Add(_lblFileName);
            return panel;
        }

        private TextBox CreateResultBox()
        {
            return new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Dock = DockStyle.Top,
                Height = 160,
                Font = AppFonts.Small,
                BackColor = AppColors.BgLight,
                ForeColor = AppColors.TextSecondary,
                Visible = false,
                Margin = new Padding(0, 0, 0, 8)
            };
        }

        private void ChooseFile()
        {
            using (var dialog = new OpenFileDialog { Filter = DefaultSpreadsheetImporters.CombinedSaveOrOpenFilter })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                FileChosen?.Invoke(this, dialog.FileName);
            }
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
            SetFooterButtonsEnabled(!isBusy);
            _btnStart.Text = isBusy ? (message ?? "Đang xử lý...") : "Bắt đầu nhập";
            Cursor = isBusy ? Cursors.WaitCursor : Cursors.Default;
        }

        public void ShowValidationError(string title, string message) => DialogHelper.ShowError(this, title, message);

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
                DialogHelper.ShowSuccess(this, "Thành công", "Đã nhập " + successCount + " dòng dữ liệu.");
            else if (successCount > 0)
                DialogHelper.ShowWarning(this, "Hoàn tất một phần",
                    successCount + " dòng thành công, " + errors.Count + " dòng lỗi (xem chi tiết trong hộp kết quả).");
            else
                DialogHelper.ShowError(this, "Nhập thất bại", "Không có dòng nào được nhập thành công.");
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing) _presenter?.Dispose();
            base.Dispose(disposing);
        }
    }
}
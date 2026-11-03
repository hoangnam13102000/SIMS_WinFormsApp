using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Views.UserManager;
using SIMS_WinFormsApp.Views.SystemManagement;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.MVP.Presenters.Catalog;
using SIMS_WinFormsApp.Services.Implementations.Export;
using SIMS_WinFormsApp.Services.Implementations.Import;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Toast;
using SIMS_WinFormsApp.Views.Dialogs;

namespace SIMS_WinFormsApp.Views.Catalog
{
    public sealed partial class ucCategoryManagement : UserControl
    {
        private readonly CategoryManagementPresenter _presenter;
        private readonly ICatalogAdminService _service;
        private readonly Timer _searchTimer;
        private readonly ContextMenuStrip _optionsMenu;
        private readonly bool _canEditCategories;
        private readonly bool _canManageCategories;
        private int _pageIndex;
        private int _pageSize = 10;
        private int _totalCount;

        public ucCategoryManagement() : this(null, true, true)
        {
        }

        public ucCategoryManagement(ICatalogAdminService service) : this(service, true, true)
        {
        }

        public ucCategoryManagement(
            ICatalogAdminService service,
            bool canEditCategories,
            bool canManageCategories)
        {
            InitializeComponent();
            _canEditCategories = canEditCategories || canManageCategories;
            _canManageCategories = canManageCategories;
            _header.SizeChanged += (sender, args) => LayoutHeader();
            LayoutHeader();
            _previousButton.Text = string.Empty;
            _nextButton.Text = string.Empty;
            _previousButton.Paint += (sender, args) =>
                DrawPagingArrow(args.Graphics, _previousButton.ClientRectangle, false, _previousButton.Enabled);
            _nextButton.Paint += (sender, args) =>
                DrawPagingArrow(args.Graphics, _nextButton.ClientRectangle, true, _nextButton.Enabled);
            _refreshButton.Click += (sender, args) => LoadPage();
            _optionsMenu = new ContextMenuStrip(components);
            _optionsMenu.Items.Add("Xuất CSV", null, (sender, args) => ExportCategories(new CsvTableExporter()));
            _optionsMenu.Items.Add("Xuất Excel", null, (sender, args) => ExportCategories(new ExcelTableExporter()));
            if (_canManageCategories)
                _optionsMenu.Items.Add("Nhập dữ liệu", null, (sender, args) => ImportCategories());
            _optionsButton.Click += (sender, args) => _optionsMenu.Show(_optionsButton, 0, _optionsButton.Height);
            _addButton.Visible = _canManageCategories;
            if (service == null) return;

            _service = service;
            _presenter = new CategoryManagementPresenter(service);
            _searchTimer = new Timer { Interval = 350 };
            _searchTimer.Tick += (sender, args) =>
            {
                _searchTimer.Stop();
                _pageIndex = 0;
                LoadPage();
            };

            _searchTextBox.TextChanged += (sender, args) =>
            {
                _searchTimer.Stop();
                _searchTimer.Start();
            };
            _statusComboBox.SelectedIndexChanged += (sender, args) =>
            {
                _pageIndex = 0;
                LoadPage();
            };
            _pageSizeComboBox.SelectedIndexChanged += (sender, args) =>
            {
                _pageSize = _pageSizeComboBox.SelectedIndex == 1 ? 20 :
                    (_pageSizeComboBox.SelectedIndex == 2 ? 50 : 10);
                _pageIndex = 0;
                LoadPage();
            };
            _previousButton.Click += (sender, args) =>
            {
                if (_pageIndex <= 0) return;
                _pageIndex--;
                LoadPage();
            };
            _nextButton.Click += (sender, args) =>
            {
                if (_pageIndex + 1 >= PageCount) return;
                _pageIndex++;
                LoadPage();
            };
            _addButton.Click += (sender, args) => EditCategory(_presenter.NewDraft());
            _categoriesGrid.CellMouseClick += OnGridCellMouseClick;
            Load += (sender, args) => LoadPage();
            Disposed += (sender, args) => _searchTimer.Dispose();
        }

        private void LayoutHeader()
        {
            int right = _header.ClientSize.Width - 16;
            _refreshButton.Location = new Point(right - _refreshButton.Width, 29);
            right = _refreshButton.Left - 8;
            _optionsButton.Location = new Point(right - _optionsButton.Width, 29);
            right = _optionsButton.Left - 8;
            _addButton.Location = new Point(right - _addButton.Width, 29);
            _titleLabel.Width = Math.Max(180, _addButton.Left - _titleLabel.Left - 16);
            _addButton.BringToFront();
            _optionsButton.BringToFront();
            _refreshButton.BringToFront();
        }

        private static void DrawPagingArrow(Graphics graphics, Rectangle bounds, bool pointsRight, bool enabled)
        {
            int centerX = bounds.Width / 2;
            int centerY = bounds.Height / 2;
            int direction = pointsRight ? 1 : -1;
            Color color = enabled ? Color.FromArgb(51, 65, 85) : Color.FromArgb(148, 163, 184);

            SmoothingMode previousSmoothingMode = graphics.SmoothingMode;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (var pen = new Pen(color, 2F))
            {
                graphics.DrawLine(pen, centerX - direction * 3, centerY - 5, centerX + direction * 2, centerY);
                graphics.DrawLine(pen, centerX + direction * 2, centerY, centerX - direction * 3, centerY + 5);
            }
            graphics.SmoothingMode = previousSmoothingMode;
        }

        private int PageCount => Math.Max(1, (int)Math.Ceiling(_totalCount / (double)_pageSize));

        private void LoadPage()
        {
            if (_presenter == null || IsDisposed) return;
            _categoriesGrid.SuspendLayout();
            _categoriesGrid.Rows.Clear();
            try
            {
                string filter = _statusComboBox.SelectedIndex == 1
                    ? "ACTIVE"
                    : (_statusComboBox.SelectedIndex == 2 ? "DISABLED" : null);
                var result = _presenter.Load(_pageIndex, _pageSize, _searchTextBox.Text, filter);
                _totalCount = result.TotalCount;
                foreach (CategoryRowDto row in _presenter.CurrentRows)
                {
                    _categoriesGrid.Rows.Add(
                        row.Name,
                        row.ProductCount,
                        CatalogFormat.Status(row.IsActive),
                        IconBitmapCache.GetActionStrip(
                            _canEditCategories,
                            row.IsActive ? IconChar.CircleXmark : IconChar.CircleCheck,
                            row.IsActive ? AppColors.Error : AppColors.Success));
                }

                if (_pageIndex >= PageCount)
                {
                    _pageIndex = PageCount - 1;
                    LoadPage();
                    return;
                }
                    UpdatePaging();
                }
                catch (Exception ex)
                {
                    DialogHelper.ShowError(this, "Lỗi tải dữ liệu", "Không thể tải danh mục: " + ex.Message);
                }
                finally
                {
                    _categoriesGrid.ResumeLayout();
                }
        }

        private void UpdatePaging()
        {
                int firstItem = _totalCount == 0 ? 0 : _pageIndex * _pageSize + 1;
                int lastItem = Math.Min((_pageIndex + 1) * _pageSize, _totalCount);
                _resultsLabel.Text = _totalCount == 0
                    ? "Hiển thị 0 danh mục"
                    : string.Format("Hiển thị {0}-{1} / {2} danh mục", firstItem, lastItem, _totalCount);
                _pageLabel.Text = "Trang " + (_pageIndex + 1) + " / " + PageCount;
                _previousButton.Enabled = _pageIndex > 0;
                _nextButton.Enabled = _pageIndex + 1 < PageCount;
                _previousButton.BackColor = _previousButton.Enabled ? Color.White : Color.FromArgb(248, 250, 252);
                _nextButton.BackColor = _nextButton.Enabled ? Color.White : Color.FromArgb(248, 250, 252);
        }

        private void OnGridCellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
                if (e.Button != MouseButtons.Left || _presenter == null || e.RowIndex < 0 ||
                    e.RowIndex >= _presenter.CurrentRows.Count)
                    return;

                if (e.ColumnIndex != _actionsColumn.Index) return;
                var row = _presenter.CurrentRows[e.RowIndex];
                const int iconSize = 24;
                const int gap = 8;
                int stripWidth = iconSize * 3 + gap * 2;
                int offset = e.X - (_actionsColumn.Width - stripWidth) / 2;
                int actionIndex = offset / (iconSize + gap);
                if (offset < 0 || actionIndex > 2 || offset % (iconSize + gap) >= iconSize) return;
                if (actionIndex == 0)
                    EditCategory(_presenter.Get(row.CategoryId), true);
            else if (_canEditCategories && actionIndex == 1)
                    EditCategory(_presenter.Get(row.CategoryId), false);
            else if (_canEditCategories && actionIndex == 2)
            {
                bool enable = !row.IsActive;
                string verb = enable ? "mở lại" : "vô hiệu hóa";
                if (!DialogHelper.Confirm(this, "Xác nhận thao tác",
                    "Bạn có chắc muốn " + verb + " danh mục '" + row.Name + "' không?"))
                    return;

                var result = _presenter.SetActive(row.CategoryId, enable);
                Notify(result);
                if (result != null && result.Success) LoadPage();
            }
        }

        private void EditCategory(CategoryEditDto draft, bool readOnly = false)
        {
            if (!readOnly && !_canEditCategories)
                return;

            if (draft == null)
            {
                AppToast.Warning(this, "Không tìm thấy danh mục.");
                return;
            }
            if (!frmCategoryEditor.TryEdit(FindForm(), draft, readOnly, out var edited) || readOnly)
                return;

            while (edited != null)
            {
                var result = _presenter.Save(edited);
                if (result != null && result.Success)
                {
                    Notify(result);
                    _pageIndex = 0;
                    LoadPage();
                    return;
                }

                DialogHelper.ShowWarning(this, "Không thể lưu",
                    result == null ? "Không lưu được danh mục." : result.Message);
                draft = edited;
                if (!frmCategoryEditor.TryEdit(FindForm(), draft, false, out edited))
                    return;
            }
        }

        private void ImportCategories()
        {
            if (!_canManageCategories)
                return;

            var importer = new CategoryImportRowHandler(_service);
            var result = frmImportData.Show(
                FindForm(),
                "Danh mục",
                CategoryImportRowHandler.ExpectedColumns,
                "Trạng thái: Đang hoạt động hoặc Vô hiệu hóa. Để trống thì danh mục được mở.",
                DefaultSpreadsheetImporters.All,
                importer.Handle);
            if (result == DialogResult.OK) LoadPage();
        }

        private void ExportCategories(ITableDataExporter exporter)
        {
            string[] headers = { "Tên danh mục", "Số sản phẩm", "Trạng thái" };
            TableExportRunner.Run(
                FindForm(), exporter, "Danh mục", () => headers, _presenter.ExportRows);
        }

        private void Notify(CatalogSaveResult result)
        {
            if (result != null && result.Success)
                AppToast.Success(this, result.Message);
            else
                AppToast.Warning(this, result == null ? "Không lưu được dữ liệu." : result.Message);
        }
    }
}

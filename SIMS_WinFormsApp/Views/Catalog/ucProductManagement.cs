using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Views.UserManager;
using SIMS_WinFormsApp.Views.SystemManagement;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.MVP.Presenters.Catalog;
using SIMS_WinFormsApp.Services.Implementations.Export;
using SIMS_WinFormsApp.Services.Implementations.Import;
using SIMS_WinFormsApp.Services.Implementations.Catalog;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Controls.Filter;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.UI.Controls.Toast;

namespace SIMS_WinFormsApp.Views.Catalog
{
    public sealed partial class ucProductManagement : UserControl
    {
        private readonly ProductManagementPresenter _presenter;
        private readonly ICatalogAdminService _service;
        private readonly Timer _searchTimer;
        private readonly LocalProductImageStore _imageStore = new LocalProductImageStore();
        private readonly Dictionary<string, Image> _imageCache = new Dictionary<string, Image>(StringComparer.OrdinalIgnoreCase);
        private int _pageIndex;
        private int _pageSize = 10;
        private int _totalCount;

        public ucProductManagement() : this(null)
        {
        }

        public ucProductManagement(ICatalogAdminService service)
        {
            InitializeComponent();
            if (service == null) return;

            _service = service;
            _presenter = new ProductManagementPresenter(service);
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
            _addButton.Click += (sender, args) => EditProduct(_presenter.NewDraft());
            _importButton.Click += (sender, args) => ImportProducts();
            _exportCsvButton.Click += (sender, args) => ExportProducts(new CsvTableExporter());
            _exportExcelButton.Click += (sender, args) => ExportProducts(new ExcelTableExporter());
            _productsGrid.CellMouseClick += OnGridCellMouseClick;
            Load += (sender, args) => LoadPage();
            Disposed += (sender, args) =>
            {
                _searchTimer.Dispose();
                foreach (Image image in _imageCache.Values) image.Dispose();
                _imageCache.Clear();
            };
        }

        private int PageCount => Math.Max(1, (int)Math.Ceiling(_totalCount / (double)_pageSize));

        private void LoadPage()
        {
            if (_presenter == null || IsDisposed) return;
            _productsGrid.Rows.Clear();
            try
            {
                string filter = _statusComboBox.SelectedIndex == 1
                    ? "ACTIVE"
                    : (_statusComboBox.SelectedIndex == 2 ? "DISABLED" : null);
                _presenter.Load(_pageIndex, _pageSize, _searchTextBox.Text, filter);
                _totalCount = _presenter.TotalCount;

                if (_pageIndex >= PageCount)
                {
                    _pageIndex = PageCount - 1;
                    LoadPage();
                    return;
                }

                foreach (ProductRowDto row in _presenter.CurrentRows)
                {
                    bool imageUnavailable;
                    Image thumbnail = GetProductThumbnail(row.ImagePath, out imageUnavailable);
                    int rowIndex = _productsGrid.Rows.Add(
                        thumbnail,
                        row.Code,
                        row.Name,
                        row.CategoryName,
                        CatalogFormat.Money(row.SellPrice),
                        row.Stock,
                        CatalogFormat.Status(row.IsActive),
                        IconBitmapCache.GetActionStrip(
                            row.IsActive ? IconChar.CircleXmark : IconChar.CircleCheck,
                            row.IsActive ? AppColors.Error : AppColors.Success));
                    if (imageUnavailable)
                        _productsGrid.Rows[rowIndex].Cells[_imageColumn.Index].ToolTipText =
                            "Không tìm thấy hoặc không thể đọc ảnh sản phẩm.";
                }

                UpdatePaging();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Không thể tải sản phẩm: " + ex.Message,
                    "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdatePaging()
        {
            int firstItem = _totalCount == 0 ? 0 : _pageIndex * _pageSize + 1;
            int lastItem = Math.Min((_pageIndex + 1) * _pageSize, _totalCount);
            _resultsLabel.Text = _totalCount == 0
                ? "Hiển thị 0 sản phẩm"
                : string.Format("Hiển thị {0}-{1} / {2} sản phẩm", firstItem, lastItem, _totalCount);
            _pageLabel.Text = "Trang " + (_pageIndex + 1) + " / " + PageCount;
            _previousButton.Enabled = _pageIndex > 0;
            _nextButton.Enabled = _pageIndex + 1 < PageCount;
            _previousButton.BackColor = _previousButton.Enabled
                ? Color.White
                : Color.FromArgb(248, 250, 252);
            _nextButton.BackColor = _nextButton.Enabled
                ? Color.White
                : Color.FromArgb(248, 250, 252);
        }

        private Image GetProductThumbnail(string imagePath, out bool unavailable)
        {
            unavailable = false;
            if (string.IsNullOrWhiteSpace(imagePath)) return null;

            try
            {
                string path = _imageStore.Resolve(imagePath);
                if (string.IsNullOrWhiteSpace(path))
                {
                    unavailable = true;
                    return IconBitmapCache.Get(IconChar.Image, AppColors.TextMuted, new Size(32, 32));
                }

                Image cached;
                if (_imageCache.TryGetValue(path, out cached)) return cached;

                using (Image source = Image.FromFile(path))
                {
                    const int size = 40;
                    var thumbnail = new Bitmap(size, size);
                    double scale = Math.Min((double)size / source.Width, (double)size / source.Height);
                    int width = Math.Max(1, (int)(source.Width * scale));
                    int height = Math.Max(1, (int)(source.Height * scale));
                    using (Graphics graphics = Graphics.FromImage(thumbnail))
                    {
                        graphics.Clear(Color.White);
                        graphics.DrawImage(source, new Rectangle((size - width) / 2, (size - height) / 2, width, height));
                    }
                    _imageCache.Add(path, thumbnail);
                    return thumbnail;
                }
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException ||
                ex is ArgumentException || ex is OutOfMemoryException)
            {
                unavailable = true;
                return IconBitmapCache.Get(IconChar.Image, AppColors.TextMuted, new Size(32, 32));
            }
        }

        private void OnGridCellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || _presenter == null || e.RowIndex < 0 ||
                e.RowIndex >= _presenter.CurrentRows.Count)
                return;

            if (e.ColumnIndex != _actionsColumn.Index) return;

            ProductRowDto row = _presenter.CurrentRows[e.RowIndex];
            const int iconSize = 24;
            const int gap = 8;
            int stripWidth = iconSize * 3 + gap * 2;
            int offset = e.X - (_actionsColumn.Width - stripWidth) / 2;
            int actionIndex = offset / (iconSize + gap);
            if (offset < 0 || actionIndex > 2 || offset % (iconSize + gap) >= iconSize) return;

            if (actionIndex == 0)
                EditProduct(_presenter.Get(row.ProductId), true, row.CategoryName);
            else if (actionIndex == 1)
                EditProduct(_presenter.Get(row.ProductId), false, row.CategoryName);
            else
            {
                bool activate = !row.IsActive;
                string action = activate ? "mở bán lại" : "ngừng bán";
                if (!DialogHelper.Confirm(this, "Xác nhận thao tác",
                    "Bạn có chắc muốn " + action + " sản phẩm '" + row.Name + "' không?"))
                    return;

                CatalogSaveResult result = _presenter.SetActive(row.ProductId, activate);
                Notify(result);
                if (result != null && result.Success) LoadPage();
            }
        }

        private void EditProduct(ProductEditDto draft, bool readOnly = false, string categoryName = null)
        {
            if (draft == null)
            {
                AppToast.Warning(this, "Không tìm thấy sản phẩm.");
                return;
            }

            IWin32Window owner = FindForm();
            IReadOnlyList<LookupItem> categories = EnsureLookup(_presenter.Categories(), draft.CategoryId, categoryName);
            IReadOnlyList<LookupItem> suppliers = EnsureLookup(_presenter.Suppliers(), draft.SupplierId ?? 0, null);
            if (readOnly)
            {
                ProductEditDto ignored;
                frmProductEditor.TryEdit(owner, draft, categories, suppliers, true, out ignored);
                return;
            }

            ProductEditDto edited;
            while (frmProductEditor.TryEdit(owner, draft, categories, suppliers, false, out edited))
            {
                CatalogSaveResult result = _presenter.Save(edited);
                if (result != null && result.Success)
                {
                    Notify(result);
                    _pageIndex = 0;
                    LoadPage();
                    return;
                }

                DialogHelper.ShowWarning(owner ?? this,
                    result == null ? "Không lưu được sản phẩm." : result.Message);
                draft = edited;
            }
        }

        private void ImportProducts()
        {
            var importer = new ProductImportRowHandler(_service);
            DialogResult result = frmImportData.Show(
                FindForm(),
                "Sản phẩm",
                ProductImportRowHandler.ExpectedColumns,
                "Giá nhập và giá bán là số. Ảnh là đường dẫn file trên máy, có thể để trống. Danh mục và nhà cung cấp chưa có sẽ được tạo theo tên.",
                DefaultSpreadsheetImporters.All,
                importer.Handle);
            if (result == DialogResult.OK) LoadPage();
        }

        private void ExportProducts(ITableDataExporter exporter)
        {
            string[] headers = { "Mã", "Tên sản phẩm", "Danh mục", "Giá bán", "Tồn kho", "Trạng thái" };
            TableExportRunner.Run(
                FindForm(), exporter, "Sản phẩm", () => headers, _presenter.ExportRows);
        }

        private static IReadOnlyList<LookupItem> EnsureLookup(
            IReadOnlyList<LookupItem> items, int id, string name)
        {
            var list = new List<LookupItem>(items ?? Array.Empty<LookupItem>());
            if (id <= 0 || list.Exists(item => item.Id == id)) return list;
            list.Insert(0, new LookupItem
            {
                Id = id,
                Name = string.IsNullOrWhiteSpace(name) ? "Mục hiện tại" : name
            });
            return list;
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

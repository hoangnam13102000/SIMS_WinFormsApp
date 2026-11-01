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
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Toast;
using SIMS_WinFormsApp.Views.Dialogs;

namespace SIMS_WinFormsApp.Views.Catalog
{
    public sealed partial class ucProductManagement : UserControl
    {
        private readonly ProductManagementPresenter _presenter;
        private readonly ICatalogAdminService _service;
        private readonly bool _canEditProducts;
        private readonly bool _canManageProducts;
        private readonly Timer _searchTimer;
        private readonly ContextMenuStrip _optionsMenu;
        private readonly Action<int> _showExpiredBatches;
        private readonly LocalProductImageStore _imageStore = new LocalProductImageStore();
        private readonly Dictionary<string, Image> _imageCache = new Dictionary<string, Image>(StringComparer.OrdinalIgnoreCase);
        private int _pageIndex;
        private int _pageSize = 10;
        private int _totalCount;
        private bool _hasBeenVisible;
        private bool _isBindingCategoryFilter;
        private bool _categoryFilterLoaded;

        public ucProductManagement() : this(null)
        {
        }

        public ucProductManagement(ICatalogAdminService service)
            : this(service, true, true, null)
        {
        }

        public ucProductManagement(
            ICatalogAdminService service,
            bool canEditProducts,
            bool canManageProducts,
            Action<int> showExpiredBatches = null)
        {
            InitializeComponent();
            _canEditProducts = canEditProducts || canManageProducts;
            _canManageProducts = canManageProducts;
            _showExpiredBatches = showExpiredBatches;
            _expiredBatchesColumn.Visible = _showExpiredBatches != null;
            _optionsMenu = new ContextMenuStrip(components);
            _optionsMenu.Items.Add("Xuất CSV", null, (sender, args) => ExportProducts(new CsvTableExporter()));
            _optionsMenu.Items.Add("Xuất Excel", null, (sender, args) => ExportProducts(new ExcelTableExporter()));
            if (_canManageProducts)
                _optionsMenu.Items.Add("Nhập dữ liệu", null, (sender, args) => ImportProducts());
            _optionsButton.Click += (sender, args) => _optionsMenu.Show(_optionsButton, 0, _optionsButton.Height);
            _addButton.Visible = _canManageProducts;
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
            _categoryComboBox.SelectedIndexChanged += (sender, args) =>
            {
                if (_isBindingCategoryFilter) return;
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
            _productsGrid.CellMouseClick += OnGridCellMouseClick;
            Load += (sender, args) => LoadPage();
            VisibleChanged += (sender, args) =>
            {
                if (!Visible) return;
                if (_hasBeenVisible)
                    LoadPage();
                else
                    _hasBeenVisible = true;
            };
            VisibleChanged += (sender, args) =>
            {
                if (!Visible) return;
                if (_hasBeenVisible)
                    LoadPage();
                else
                    _hasBeenVisible = true;
            };
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
                if (!_categoryFilterLoaded)
                    LoadCategoryFilterOptions();

                string filter = _statusComboBox.SelectedIndex == 1
                    ? "ACTIVE"
                    : (_statusComboBox.SelectedIndex == 2 ? "DISABLED" : null);
                LookupItem selectedCategory = _categoryComboBox.SelectedItem as LookupItem;
                int? categoryId = selectedCategory == null || selectedCategory.Id == 0
                    ? (int?)null
                    : selectedCategory.Id;
                _presenter.Load(_pageIndex, _pageSize, _searchTextBox.Text, filter, categoryId);
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
                        CatalogFormat.StockSummary(row),
                        row.ExpiredStock > 0 ? "Xem lô (" + row.ExpiredStock.ToString("N0") + ")" : "—",
                        CatalogFormat.ProductStatus(row),
                        _canEditProducts
                            ? IconBitmapCache.GetActionStrip(
                                row.IsActive ? IconChar.CircleXmark : IconChar.CircleCheck,
                                row.IsActive ? AppColors.Error : AppColors.Success)
                            : IconBitmapCache.Get(IconChar.Eye, AppColors.TableViewAction, new Size(24, 24)));
                    if (imageUnavailable)
                        _productsGrid.Rows[rowIndex].Cells[_imageColumn.Index].ToolTipText =
                            "Không tìm thấy hoặc không thể đọc ảnh sản phẩm.";
                    if (row.ExpiredStock > 0)
                    {
                        _productsGrid.Rows[rowIndex].Cells[_expiredBatchesColumn.Index].Style.ForeColor =
                            Color.FromArgb(185, 28, 28);
                        _productsGrid.Rows[rowIndex].Cells[_expiredBatchesColumn.Index].ToolTipText =
                            "Mở danh sách lô hết hạn của sản phẩm này.";
                    }
                    if (row.IsActive && row.SellableStock <= 0)
                    {
                        _productsGrid.Rows[rowIndex].Cells[_statusColumn.Index].Style.ForeColor =
                            Color.FromArgb(185, 28, 28);
                        _productsGrid.Rows[rowIndex].Cells[_statusColumn.Index].ToolTipText =
                            "Sản phẩm đang hoạt động nhưng hiện không có lô nào đủ điều kiện bán.";
                    }
                    if (row.StockBatchDifference != 0)
                    {
                        _productsGrid.Rows[rowIndex].Cells[_stockColumn.Index].Style.ForeColor =
                            Color.FromArgb(185, 28, 28);
                        _productsGrid.Rows[rowIndex].Cells[_stockColumn.Index].ToolTipText =
                            "Tồn kho sản phẩm và tổng số lượng còn lại trên các lô đang hoạt động đang lệch " +
                            row.StockBatchDifference.ToString("+0;-0;0") +
                            ". Kiểm kê hiện chỉ cập nhật tồn tổng, không tự điều chỉnh số lượng từng lô. " +
                            "Cần đối chiếu thực tế và xử lý lô liên quan.";
                        _productsGrid.Rows[rowIndex].Cells[_statusColumn.Index].Style.ForeColor =
                            Color.FromArgb(185, 28, 28);
                    }
                }

                UpdatePaging();
            }
            catch (Exception ex)
            {
                DialogHelper.ShowError(this, "Lỗi tải dữ liệu", "Không thể tải sản phẩm: " + ex.Message);
            }
        }

        private void LoadCategoryFilterOptions()
        {
            var options = new List<LookupItem>
            {
                new LookupItem { Id = 0, Name = "Tất cả danh mục" }
            };
            options.AddRange(_presenter.CategoryFilterOptions());

            _isBindingCategoryFilter = true;
            try
            {
                _categoryComboBox.DisplayMember = nameof(LookupItem.Name);
                _categoryComboBox.ValueMember = nameof(LookupItem.Id);
                _categoryComboBox.DataSource = options;
                _categoryComboBox.SelectedValue = 0;
                _categoryFilterLoaded = true;
            }
            finally
            {
                _isBindingCategoryFilter = false;
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

            ProductRowDto row = _presenter.CurrentRows[e.RowIndex];
            if (e.ColumnIndex == _expiredBatchesColumn.Index)
            {
                if (row.ExpiredStock > 0)
                    _showExpiredBatches?.Invoke(row.ProductId);
                return;
            }
            if (e.ColumnIndex != _actionsColumn.Index) return;

            if (!_canEditProducts)
            {
                EditProduct(_presenter.Get(row.ProductId), true, row.CategoryName);
                return;
            }

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
                if (!_canEditProducts) return;
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
            string[] headers =
            {
                "Mã", "Tên sản phẩm", "Danh mục", "Giá bán",
                "Tồn kho và số lượng có thể bán", "Trạng thái"
            };
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

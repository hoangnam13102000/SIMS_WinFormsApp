using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs.Promotions;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Toast;

namespace SIMS_WinFormsApp.Views.Sales
{
    public partial class PromotionManagementPage : UserControl
    {
        private readonly IPromotionManagementService _service;
        private readonly List<PromotionDto> _currentRows = new List<PromotionDto>();
        private PromotionEditorDialog _editorDialog;
        private int _selectedId;
        private bool _themeSubscribed;
        private bool _languageSubscribed;

        public PromotionManagementPage()
        {
            InitializePage();
        }

        public PromotionManagementPage(IPromotionManagementService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            InitializePage();
        }

        private void InitializePage()
        {
            InitializeComponent();
            ApplyPageTheme();
            _grid.DataBindingComplete += (sender, args) => ApplyGridHeaders();

            if (_service != null)
            {
                ThemeManager.Instance.ThemeChanged += OnThemeChanged;
                _themeSubscribed = true;
                LanguageManager.Instance.LanguageChanged += OnLanguageChanged;
                _languageSubscribed = true;
                Load += (sender, args) => Reload();
                _reloadButton.Click += (sender, args) => Reload();
                _newPromotionButton.Click += (sender, args) => OpenEditor(null);
                _grid.SelectionChanged += (sender, args) => LoadSelected();
                _grid.CellDoubleClick += (sender, args) =>
                {
                    if (args.RowIndex >= 0)
                    {
                        _grid.CurrentCell = _grid.Rows[args.RowIndex].Cells["Code"];
                        ViewSelected();
                    }
                };
                _grid.CellMouseClick += Grid_CellMouseClick;
                _pager.PageChanged += (sender, args) => Reload();
                _search.TextChanged += (sender, args) => Reload(true);
                _includeDeleted.CheckedChanged += (sender, args) => Reload(true);
            }
            else if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                _grid.Rows.Add(101, "SUMMER10", "Ưu đãi mùa hè", "10%", "01/06/2026 - 30/06/2026", "12/100", "Có", null);
                _grid.Rows.Add(102, "WELCOME50", "Khách hàng mới", "50.000đ", "01/01/2026 - 31/12/2026", "28/∞", "Có", null);
                _grid.Rows.Add(103, "FREESHIP", "Miễn phí vận chuyển", "20.000đ", "01/10/2026 - 31/10/2026", "0/50", "Không", null);
                _pager.SetTotalCount(3);
            }

            ApplyGridHeaders();
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            ApplyPageTheme();
        }

        private void OnLanguageChanged(object sender, EventArgs e)
        {
            ApplyGridHeaders();
        }

        private void ApplyGridHeaders()
        {
            SetGridHeader("PromotionId", "promotion.grid.id");
            SetGridHeader("Code", "promotion.grid.code");
            SetGridHeader("Name", "promotion.grid.name");
            SetGridHeader("Discount", "promotion.grid.discount");
            SetGridHeader("Period", "promotion.grid.period");
            SetGridHeader("Usage", "promotion.grid.usage");
            SetGridHeader("IsActive", "promotion.grid.status");
            SetGridHeader("Actions", "promotion.grid.actions");
        }

        private void SetGridHeader(string columnName, string resourceKey)
        {
            if (_grid.Columns.Contains(columnName))
                _grid.Columns[columnName].HeaderText = Lang.Get(resourceKey);
        }

        private void ApplyPageTheme()
        {
            BackColor = AppColors.PageBg;
            _root.BackColor = AppColors.PageBg;
            _title.ForeColor = AppColors.TextTitle;
            _subtitle.ForeColor = AppColors.TextMuted;

            _reloadButton.BackColor = AppColors.CancelBg;
            _reloadButton.ForeColor = AppColors.TextPrimary;
            _reloadButton.FlatAppearance.BorderColor = AppColors.Border;
            _reloadButton.FlatAppearance.MouseOverBackColor = AppColors.CancelHover;

            _newPromotionButton.BackColor = AppColors.Accent;
            _newPromotionButton.ForeColor = Color.White;
            _newPromotionButton.FlatAppearance.BorderColor = AppColors.Accent;
            _newPromotionButton.FlatAppearance.MouseOverBackColor = AppColors.AccentHover;

            _gridPanel.BackColor = AppColors.White;
            _grid.BackgroundColor = AppColors.White;
            _grid.GridColor = AppColors.TableGrid;
            _grid.ColumnHeadersDefaultCellStyle.BackColor = AppColors.TableHeaderBg;
            _grid.DefaultCellStyle.BackColor = AppColors.White;
            _grid.DefaultCellStyle.ForeColor = AppColors.TableRowText;
            _grid.DefaultCellStyle.SelectionBackColor = AppColors.AccentSelectionBg;
            _grid.DefaultCellStyle.SelectionForeColor = AppColors.TextPrimary;
            _grid.AlternatingRowsDefaultCellStyle.BackColor = AppColors.TableRowOdd;
        }

        private void Reload(bool resetPage = false)
        {
            if (resetPage)
                _pager.ResetPage();
            try
            {
                PromotionPage page = _service.GetPage(
                    _search.Text.Trim(),
                    _includeDeleted.Checked,
                    _pager.PageIndex,
                    _pager.PageSize);
                if (_pager.SetTotalCount(page.TotalCount))
                {
                    Reload();
                    return;
                }

                _currentRows.Clear();
                _currentRows.AddRange(page.Rows);
                var rows = _currentRows.Select(ToRow).ToList();
                _grid.DataSource = rows;
            }
            catch (Exception ex) { ShowError(ex.Message); }
        }

        private static object ToRow(PromotionDto item) => new
        {
            item.PromotionId, item.Code, item.Name,
            Discount = item.DiscountType == "PERCENT" ? item.DiscountValue.ToString("0") + "%" : item.DiscountValue.ToString("N0") + "đ",
            Period = item.StartDate.ToString("dd/MM/yyyy") + " - " + item.EndDate.ToString("dd/MM/yyyy"),
            Usage = item.UsageLimit.HasValue ? item.UsedCount + "/" + item.UsageLimit.Value : item.UsedCount + "/∞",
            IsActive = item.IsDeleted ? "Đã xóa" : (item.IsActive ? "Có" : "Không"),
            Actions = IconBitmapCache.GetActionStrip(IconChar.Trash, AppColors.Error)
        };

        private void LoadSelected()
        {
            int rowIndex = _grid.CurrentRow == null ? -1 : _grid.CurrentRow.Index;
            _selectedId = rowIndex >= 0 && rowIndex < _currentRows.Count
                ? _currentRows[rowIndex].PromotionId
                : 0;
        }

        private void Grid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || e.RowIndex < 0 ||
                e.RowIndex >= _currentRows.Count || e.ColumnIndex < 0 ||
                _grid.Columns[e.ColumnIndex].Name != "Actions")
                return;

            _grid.CurrentCell = _grid.Rows[e.RowIndex].Cells["Code"];
            const int iconSize = 24;
            const int gap = 8;
            int stripWidth = iconSize * 3 + gap * 2;
            int offset = e.X - (_grid.Columns[e.ColumnIndex].Width - stripWidth) / 2;
            int actionIndex = offset / (iconSize + gap);
            if (offset < 0 || actionIndex > 2 || offset % (iconSize + gap) >= iconSize)
                return;

            if (actionIndex == 0)
                ViewSelected();
            else if (actionIndex == 1)
                EditSelected();
            else
                Delete();
        }

        private void ViewSelected()
        {
            if (_selectedId <= 0)
            {
                ShowError("Chọn mã giảm giá cần xem.");
                return;
            }

            PromotionDto promotion = _service.GetById(_selectedId);
            if (promotion == null)
            {
                ShowError("Không tìm thấy mã giảm giá đã chọn.");
                return;
            }
            ShowDetails(promotion);
        }

        private void ShowDetails(PromotionDto promotion)
        {
            using (var dialog = new PromotionDetailsDialog(promotion))
            {
            Form owner = FindForm();
            if (owner == null) dialog.ShowDialog();
            else dialog.ShowDialog(owner);
            }
        }

        private void EditSelected()
        {
            if (_selectedId <= 0)
            {
                ShowError("Chọn mã giảm giá cần sửa.");
                return;
            }

            PromotionDto dto = _service.GetById(_selectedId);
            if (dto == null)
            {
                ShowError("Không tìm thấy mã giảm giá đã chọn.");
                return;
            }
            OpenEditor(dto);
        }

        private void OpenEditor(PromotionDto promotion)
        {
            _selectedId = promotion == null ? 0 : promotion.PromotionId;
            var dialog = new PromotionEditorDialog(promotion == null);
            _editorDialog = dialog;

            if (promotion != null)
            {
                dialog.CodeInput.Text = promotion.Code;
                dialog.NameInput.Text = promotion.Name;
                dialog.DiscountTypeInput.SelectedItem = promotion.DiscountType;
                dialog.DiscountValueInput.Text = promotion.DiscountValue.ToString("0", CultureInfo.InvariantCulture);
                dialog.MaximumDiscountInput.Text = promotion.MaxDiscountAmount?.ToString("0", CultureInfo.InvariantCulture) ?? string.Empty;
                dialog.MinimumOrderInput.Text = promotion.MinOrderAmount.ToString("0", CultureInfo.InvariantCulture);
                dialog.StartDateInput.Value = promotion.StartDate;
                dialog.EndDateInput.Value = promotion.EndDate;
                dialog.UsageLimitInput.Text = promotion.UsageLimit?.ToString() ?? string.Empty;
                dialog.ActiveInput.Checked = promotion.IsActive;
                dialog.BannerInput.Checked = promotion.ShowOnBanner;
            }

            dialog.SaveButton.Click += (sender, args) => Save();
            Form owner = FindForm();
            if (owner == null)
                dialog.ShowDialog();
            else
                dialog.ShowDialog(owner);
            _editorDialog = null;
            dialog.Dispose();
        }

        private void Save()
        {
            decimal value, max, minimum;
            int limit;
            if (!decimal.TryParse(_editorDialog.DiscountValueInput.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out value)) { ShowError("Giá trị giảm không hợp lệ."); return; }
            if (!TryOptionalDecimal(_editorDialog.MaximumDiscountInput.Text, out max) || !decimal.TryParse(_editorDialog.MinimumOrderInput.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out minimum) || !TryOptionalInt(_editorDialog.UsageLimitInput.Text, out limit)) { ShowError("Thông tin tiền hoặc giới hạn lượt không hợp lệ."); return; }
            var dto = new PromotionEditDto { PromotionId = _selectedId, Code = _editorDialog.CodeInput.Text, Name = _editorDialog.NameInput.Text, DiscountType = Convert.ToString(_editorDialog.DiscountTypeInput.SelectedItem), DiscountValue = value, MaxDiscountAmount = string.IsNullOrWhiteSpace(_editorDialog.MaximumDiscountInput.Text) ? (decimal?)null : max, MinOrderAmount = minimum, StartDate = _editorDialog.StartDateInput.Value.Date, EndDate = _editorDialog.EndDateInput.Value.Date, UsageLimit = string.IsNullOrWhiteSpace(_editorDialog.UsageLimitInput.Text) ? (int?)null : limit, IsActive = _editorDialog.ActiveInput.Checked, ShowOnBanner = _editorDialog.BannerInput.Checked };
            PromotionSaveResult result = _service.Save(dto); if (!result.IsSuccess) { ShowError(result.Message); return; }
            ShowInfo(result.Message);
            Reload();
            _editorDialog?.Close();
        }

        private void Toggle()
        {
            if (_selectedId <= 0) { ShowError("Chọn mã cần bật hoặc tắt."); return; }
            PromotionDto dto = _service.GetById(_selectedId); if (dto == null) return;
            PromotionSaveResult result = _service.SetActive(_selectedId, !dto.IsActive); if (!result.IsSuccess) ShowError(result.Message); else { ShowInfo(result.Message); Reload(); }
        }

        private void Delete()
        {
            if (_selectedId <= 0) { ShowError("Chọn mã cần xóa."); return; }
            DialogResult confirm = MessageBox.Show(
                this,
                "Bạn có chắc muốn xóa mã giảm giá đã chọn?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes)
                return;
            PromotionSaveResult result = _service.Delete(_selectedId);
            if (!result.IsSuccess) { ShowError(result.Message); return; }
            ShowInfo(result.Message);
            _selectedId = 0;
            Reload();
        }

        private static bool TryOptionalDecimal(string text, out decimal value)
        {
            if (string.IsNullOrWhiteSpace(text)) { value = 0; return true; }
            return decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out value);
        }

        private static bool TryOptionalInt(string text, out int value)
        {
            if (string.IsNullOrWhiteSpace(text)) { value = 0; return true; }
            return int.TryParse(text, out value);
        }
        private void ShowInfo(string message) => AppToast.Info(this, message);
        private void ShowError(string message) => AppToast.Error(this, "Mã giảm giá", message);
    }
}

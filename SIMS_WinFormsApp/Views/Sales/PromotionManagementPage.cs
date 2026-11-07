using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs.Promotions;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;
using SIMS_WinFormsApp.Views.Toast;

namespace SIMS_WinFormsApp.Views.Sales
{
    public partial class PromotionManagementPage : UserControl, IPromotionManagementView
    {
        private readonly List<PromotionDto> _currentRows = new List<PromotionDto>();
        private PromotionEditorDialog _editorDialog;
        private int _selectedId;
        private bool _themeSubscribed;
        private bool _languageSubscribed;

        public event EventHandler ReloadRequested;
        public event EventHandler NewPromotionRequested;
        public event EventHandler ViewPromotionRequested;
        public event EventHandler EditPromotionRequested;
        public event EventHandler ToggleActiveRequested;
        public event EventHandler DeletePromotionRequested;
        public event EventHandler CancelEditorRequested;
        public event EventHandler<PromotionSaveRequestedEventArgs> SaveRequested;

        public PromotionManagementPage()
        {
            InitializePage();
        }

        public static PromotionManagementPage Create()
        {
            var view = new PromotionManagementPage();
            var presenter = new PromotionManagementPresenter(
                view,
                AppComposition.CreatePromotionManagementService());
            view.Disposed += (sender, args) => presenter.Dispose();
            return view;
        }

        public string Keyword { get { return _search.Text.Trim(); } }
        public bool IncludeDeleted { get { return _includeDeleted.Checked; } }
        public int PageIndex { get { return _pager.PageIndex; } }
        public int PageSize { get { return _pager.PageSize; } }
        public int SelectedPromotionId { get { return _selectedId; } }

        private void InitializePage()
        {
            InitializeComponent();
            ApplyPageTheme();
            _grid.DataBindingComplete += (sender, args) => ApplyGridHeaders();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                ThemeManager.Instance.ThemeChanged += OnThemeChanged;
                _themeSubscribed = true;
                LanguageManager.Instance.LanguageChanged += OnLanguageChanged;
                _languageSubscribed = true;
                Load += (sender, args) => ReloadRequested?.Invoke(this, EventArgs.Empty);
                _grid.CellMouseClick += Grid_CellMouseClick;
                _pager.PageChanged += (sender, args) => ReloadRequested?.Invoke(this, EventArgs.Empty);
                _search.TextChanged += (sender, args) => RequestReload(true);
                _includeDeleted.CheckedChanged += (sender, args) => RequestReload(true);
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

        private void RequestReload(bool resetPage)
        {
            if (resetPage)
                _pager.ResetPage();
            ReloadRequested?.Invoke(this, EventArgs.Empty);
        }

        private void _reloadButton_Click(object sender, EventArgs e)
        {
            // Presenter.OnReloadRequested sẽ xử lý.
            ReloadRequested?.Invoke(this, EventArgs.Empty);
        }

        private void _newPromotionButton_Click(object sender, EventArgs e)
        {
            // Presenter.OnNewPromotionRequested sẽ xử lý.
            NewPromotionRequested?.Invoke(this, EventArgs.Empty);
        }

        private void Grid_SelectionChanged(object sender, EventArgs e)
        {
            LoadSelected();
        }

        private void Grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _currentRows.Count)
                return;

            SelectPromotionRow(e.RowIndex);
            RequestViewSelected();
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

        public bool ShowItems(PromotionPage page)
        {
            if (page == null)
                throw new ArgumentNullException(nameof(page));
            if (_pager.SetTotalCount(page.TotalCount))
                return true;

            _currentRows.Clear();
            _currentRows.AddRange(page.Rows);
            var rows = _currentRows.Select(ToRow).ToList();
            _grid.DataSource = rows;
            _grid.ClearSelection();
            _grid.CurrentCell = null;
            LoadSelected();
            return false;
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
            int rowIndex = _grid.SelectedRows.Count == 0 ? -1 : _grid.SelectedRows[0].Index;
            _selectedId = rowIndex >= 0 && rowIndex < _currentRows.Count
                ? _currentRows[rowIndex].PromotionId
                : 0;
        }

        private void SelectPromotionRow(int rowIndex)
        {
            _grid.CurrentCell = _grid.Rows[rowIndex].Cells["Code"];
            _grid.Rows[rowIndex].Selected = true;
        }

        private void Grid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || e.RowIndex < 0 ||
                e.RowIndex >= _currentRows.Count || e.ColumnIndex < 0 ||
                _grid.Columns[e.ColumnIndex].Name != "Actions")
                return;

            SelectPromotionRow(e.RowIndex);
            const int iconSize = 24;
            const int gap = 8;
            int stripWidth = iconSize * 3 + gap * 2;
            int offset = e.X - (_grid.Columns[e.ColumnIndex].Width - stripWidth) / 2;
            int actionIndex = offset / (iconSize + gap);
            if (offset < 0 || actionIndex > 2 || offset % (iconSize + gap) >= iconSize)
                return;

            if (actionIndex == 0)
                RequestViewSelected();
            else if (actionIndex == 1)
                RequestEditSelected();
            else
                DeleteSelected();
        }

        private void RequestViewSelected()
        {
            ViewPromotionRequested?.Invoke(this, EventArgs.Empty);
        }

        public void ShowDetails(PromotionDto promotion)
        {
            using (var dialog = new PromotionDetailsDialog(promotion))
            {
                Form owner = FindForm();
                if (owner == null) dialog.ShowDialog();
                else dialog.ShowDialog(owner);
            }
        }

        // Edit: View request -> Presenter.OnEditPromotionRequested -> ShowEditor -> SaveRequested -> Service.Save.
        private void RequestEditSelected()
        {
            EditPromotionRequested?.Invoke(this, EventArgs.Empty);
        }

        public void ShowEditor(PromotionDto promotion)
        {
            _selectedId = promotion == null ? 0 : promotion.PromotionId;
            var dialog = new PromotionEditorDialog(promotion == null);
            dialog.PromotionId = _selectedId;
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

            dialog.SaveRequested += EditorSaveRequested;
            dialog.CancelRequested += EditorCancelRequested;
            try
            {
                Form owner = FindForm();
                if (owner == null)
                    dialog.ShowDialog();
                else
                    dialog.ShowDialog(owner);
            }
            finally
            {
                dialog.SaveRequested -= EditorSaveRequested;
                dialog.CancelRequested -= EditorCancelRequested;
                _editorDialog = null;
                dialog.Dispose();
            }
        }

        private void EditorSaveRequested(object sender, PromotionSaveRequestedEventArgs e)
        {
            SaveRequested?.Invoke(this, e);
        }

        private void EditorCancelRequested(object sender, EventArgs e)
        {
            CancelEditorRequested?.Invoke(this, EventArgs.Empty);
        }

        public void CloseEditor()
        {
            _editorDialog?.Close();
        }

        // Delete: View request -> Presenter.OnDeletePromotionRequested -> confirm -> Service.Delete -> Repository.SoftDelete.
        private void DeleteSelected()
        {
            DeletePromotionRequested?.Invoke(this, EventArgs.Empty);
        }

        private void ToggleSelected()
        {
            ToggleActiveRequested?.Invoke(this, EventArgs.Empty);
        }

        public bool ConfirmDelete()
        {
            return MessageBox.Show(
                this,
                "Bạn có chắc muốn xóa mã giảm giá đã chọn?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        public void ClearSelection()
        {
            _selectedId = 0;
            _grid.ClearSelection();
            _grid.CurrentCell = null;
        }

        public void ShowInfo(string message) => AppToast.Info(this, message);
        public void ShowError(string message) => AppToast.Error(this, "Mã giảm giá", message);
    }
}

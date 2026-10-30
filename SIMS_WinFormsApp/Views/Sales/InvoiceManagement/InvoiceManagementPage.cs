using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.Models.DTOs.Sales;
using SIMS_WinFormsApp.Services.Implementations.Sales;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Sales.InvoiceManagement
{
    public partial class InvoiceManagementPage : Form
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly int _currentUserId;
        private readonly bool _canViewAll;
        private readonly bool _canCreateReturns;
        private readonly bool _canApproveReturns;
        private readonly Action<int> _onInvoiceViewed;
        private readonly InvoiceManagementDataService _dataService;
        private bool _themeSubscribed;

        public InvoiceManagementPage()
            : this(null, 0, true)
        {
        }

        public InvoiceManagementPage(IDbConnectionFactory connectionFactory, int currentUserId, bool canViewAll)
            : this(connectionFactory, currentUserId, canViewAll, false, false)
        {
        }

        public InvoiceManagementPage(
            IDbConnectionFactory connectionFactory,
            int currentUserId,
            bool canViewAll,
            bool canCreateReturns,
            bool canApproveReturns,
            Action<int> onInvoiceViewed = null)
        {
            _connectionFactory = connectionFactory;
            _dataService = connectionFactory == null ? null : new InvoiceManagementDataService(connectionFactory);
            _currentUserId = currentUserId;
            _canViewAll = canViewAll;
            _canCreateReturns = canCreateReturns;
            _canApproveReturns = canApproveReturns;
            _onInvoiceViewed = onInvoiceViewed;

            InitializeComponent();
            ConfigurePage();
            ApplyThemeColors();

            if (_connectionFactory != null)
            {
                ThemeManager.Instance.ThemeChanged += OnThemeChanged;
                _themeSubscribed = true;
                VisibleChanged += (sender, args) =>
                {
                    if (Visible)
                        ReloadInvoices();
                };
                _searchBox.KeyDown += SearchBox_KeyDown;
                _exportButton.Click += (sender, args) => ReloadInvoices();
                _invoicesGrid.CellClick += InvoicesGrid_CellClick;
                _invoicesGrid.CellMouseMove += InvoicesGrid_CellMouseMove;
                _invoicesGrid.CellDoubleClick += (sender, args) =>
                {
                    if (args.RowIndex >= 0 &&
                        args.ColumnIndex != _invoicesGrid.Columns["Actions"].Index)
                        ShowSelectedInvoice();
                };
            }
            else
            {
                _invoicesGrid.Rows.Add(1, "HD-20261028-0001", DateTime.Today.ToString("dd/MM/yyyy HH:mm"),
                    "Khách hàng mẫu", "Nhân viên mẫu", "250.000 đ", "Hoàn thành",
                    IconBitmapCache.Get(IconChar.Eye, AppColors.TableViewAction, new Size(24, 24)),
                    IconBitmapCache.Get(IconChar.Print, AppColors.Accent, new Size(24, 24)));
                _invoicesGrid.Rows.Add(2, "HD-20261028-0002", DateTime.Today.ToString("dd/MM/yyyy HH:mm"),
                    "Khách lẻ", "Nhân viên mẫu", "75.000 đ", "Đã hủy",
                    IconBitmapCache.Get(IconChar.Eye, AppColors.TableViewAction, new Size(24, 24)),
                    IconBitmapCache.Get(IconChar.Print, AppColors.Accent, new Size(24, 24)));
            }
        }

        private void ConfigurePage()
        {
            _title.Text = "Quản lý đơn hàng";
            _subtitle.Text = "Tra cứu đơn hàng, xem chi tiết và in hóa đơn";
            _exportButton.Text = "Lọc đơn hàng";
            _searchBox.Text = string.Empty;
            _fromDate.Value = DateTime.Today.AddDays(-30);
            _toDate.Value = DateTime.Today;
            _statusFilter.SelectedIndex = 0;
            _invoicesGrid.EnableHeadersVisualStyles = false;
            _invoicesGrid.AutoGenerateColumns = false;
            _invoicesGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _invoicesGrid.MultiSelect = false;
            _invoicesGrid.RowTemplate.Height = 40;
            _invoicesGrid.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            _invoicesGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _invoicesGrid.Columns["InvoiceId"].Visible = false;
            _invoicesGrid.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _invoicesGrid.Columns["Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void ApplyThemeColors()
        {
            BackColor = AppColors.PageBg;
            _root.BackColor = AppColors.PageBg;
            _title.ForeColor = AppColors.TextTitle;
            _subtitle.ForeColor = AppColors.TextMuted;
            _invoicesGrid.BackgroundColor = AppColors.White;
            _invoicesGrid.GridColor = AppColors.TableGrid;
            _invoicesGrid.DefaultCellStyle.BackColor = AppColors.White;
            _invoicesGrid.DefaultCellStyle.ForeColor = AppColors.TableRowText;
            _invoicesGrid.DefaultCellStyle.SelectionBackColor = AppColors.AccentSelectionBg;
            _invoicesGrid.DefaultCellStyle.SelectionForeColor = AppColors.TextPrimary;
            _invoicesGrid.AlternatingRowsDefaultCellStyle.BackColor = AppColors.TableRowOdd;
            _invoicesGrid.ColumnHeadersDefaultCellStyle.BackColor = AppColors.TableHeaderBg;
            _invoicesGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            _exportButton.FlatStyle = FlatStyle.Flat;
            _exportButton.FlatAppearance.BorderColor = AppColors.Border;
            _exportButton.BackColor = AppColors.CancelBg;
            _exportButton.ForeColor = AppColors.TextPrimary;
            _exportButton.FlatAppearance.MouseOverBackColor = AppColors.CancelHover;
            _invoicesGrid.Columns["Actions"].DefaultCellStyle.BackColor = AppColors.White;
            _invoicesGrid.Columns["Actions"].DefaultCellStyle.SelectionBackColor = AppColors.AccentSelectionBg;
            _invoicesGrid.Columns["Print"].DefaultCellStyle.BackColor = AppColors.White;
            _invoicesGrid.Columns["Print"].DefaultCellStyle.SelectionBackColor = AppColors.AccentSelectionBg;
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            ApplyThemeColors();
        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            e.SuppressKeyPress = true;
            ReloadInvoices();
        }

        private void ReloadInvoices()
        {
            if (_connectionFactory == null || IsDisposed)
                return;

            if (_fromDate.Value.Date > _toDate.Value.Date)
            {
                MessageBox.Show(this, "Ngày bắt đầu không được sau ngày kết thúc.",
                    "Khoảng thời gian không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string status = _statusFilter.SelectedIndex == 1 ? "ACTIVE" :
                    _statusFilter.SelectedIndex == 2 ? "CANCELLED" : "ALL";
                var rows = _dataService.GetInvoices(_fromDate.Value.Date, _toDate.Value.Date,
                    _canViewAll, _currentUserId, status, _searchBox.Text);
                _invoicesGrid.Rows.Clear();
                foreach (var row in rows)
                {
                    bool cancelled = string.Equals(row.Status, "CANCELLED", StringComparison.OrdinalIgnoreCase);
                    int rowIndex = _invoicesGrid.Rows.Add(
                        row.InvoiceId, row.InvoiceCode, row.CreatedAt.ToString("dd/MM/yyyy HH:mm"),
                        row.CustomerName, row.CashierName, FormatMoney(row.TotalAmount),
                        cancelled ? "Đã hủy" : "Hoàn thành",
                        IconBitmapCache.Get(IconChar.Eye, AppColors.TableViewAction, new Size(24, 24)),
                        IconBitmapCache.Get(IconChar.Print, AppColors.Accent, new Size(24, 24)));
                    _invoicesGrid.Rows[rowIndex].Cells["Status"].Style.ForeColor =
                        cancelled ? AppColors.Error : AppColors.Success;
                    _invoicesGrid.Rows[rowIndex].Cells["Actions"].ToolTipText = "Xem chi tiết hóa đơn";
                    _invoicesGrid.Rows[rowIndex].Cells["Print"].ToolTipText = "In hóa đơn";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Không thể tải danh sách hóa đơn.\r\n" + ex.Message,
                    "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InvoicesGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            _invoicesGrid.CurrentCell = _invoicesGrid.Rows[e.RowIndex].Cells["Invoice"];
            if (e.ColumnIndex == _invoicesGrid.Columns["Actions"].Index)
                ShowSelectedInvoice();
            else if (e.ColumnIndex == _invoicesGrid.Columns["Print"].Index)
                PrintSelectedInvoice();
        }

        private void InvoicesGrid_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            _invoicesGrid.Cursor = e.RowIndex >= 0 &&
                (e.ColumnIndex == _invoicesGrid.Columns["Actions"].Index ||
                 e.ColumnIndex == _invoicesGrid.Columns["Print"].Index)
                ? Cursors.Hand
                : Cursors.Default;
        }

        private void PrintSelectedInvoice()
        {
            if (_invoicesGrid.CurrentRow == null || _connectionFactory == null)
                return;

            int invoiceId = Convert.ToInt32(_invoicesGrid.CurrentRow.Cells["InvoiceId"].Value);
            try
            {
                InvoiceDetails invoice = LoadInvoiceDetails(invoiceId);
                if (invoice == null)
                {
                    MessageBox.Show(this, "Hóa đơn không còn tồn tại hoặc bạn không có quyền in.",
                        "Không tìm thấy hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReloadInvoices();
                    return;
                }

                _onInvoiceViewed?.Invoke(invoiceId);
                using (var dialog = new InvoiceDetailsDialog(invoice))
                    dialog.OpenPrintPreview(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Không thể tải hóa đơn để in.\r\n" + ex.Message,
                    "Lỗi in hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowSelectedInvoice()
        {
            if (_invoicesGrid.CurrentRow == null || _connectionFactory == null)
                return;

            int invoiceId = Convert.ToInt32(_invoicesGrid.CurrentRow.Cells["InvoiceId"].Value);
            try
            {
                InvoiceDetails invoice = LoadInvoiceDetails(invoiceId);
                if (invoice == null)
                {
                    MessageBox.Show(this, "Hóa đơn không còn tồn tại hoặc bạn không có quyền xem.",
                        "Không tìm thấy hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReloadInvoices();
                    return;
                }

                _onInvoiceViewed?.Invoke(invoiceId);
                using (var dialog = new InvoiceDetailsDialog(invoice))
                {
                    if (_canCreateReturns)
                        dialog.ReturnRequestHandler = CreateReturnRequest;
                    dialog.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Không thể tải chi tiết hóa đơn.\r\n" + ex.Message,
                    "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private InvoiceDetails LoadInvoiceDetails(int invoiceId)
        {
            return _dataService.GetInvoiceDetails(invoiceId, _canViewAll, _currentUserId);
        }

        private bool CreateReturnRequest(InvoiceDetails invoice)
        {
            try
            {
                using (var dialog = new InvoiceReturnDialog(
                    _connectionFactory,
                    invoice,
                    _currentUserId,
                    _canApproveReturns))
                {
                    if (dialog.ShowDialog(this) != DialogResult.OK)
                        return false;
                }

                ReloadInvoices();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Không thể mở yêu cầu trả hàng.\r\n" + ex.Message,
                    "Lỗi trả hàng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static string FormatMoney(decimal amount)
        {
            return amount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + " đ";
        }

        private void _subtitle_Click(object sender, EventArgs e)
        {

        }
    }

}

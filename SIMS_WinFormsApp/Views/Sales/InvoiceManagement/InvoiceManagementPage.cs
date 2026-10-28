using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Sales.InvoiceManagement
{
    public partial class InvoiceManagementPage : Form
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly int _currentUserId;
        private readonly bool _canViewAll;
        private bool _themeSubscribed;

        public InvoiceManagementPage()
            : this(null, 0, true)
        {
        }

        public InvoiceManagementPage(IDbConnectionFactory connectionFactory, int currentUserId, bool canViewAll)
        {
            _connectionFactory = connectionFactory;
            _currentUserId = currentUserId;
            _canViewAll = canViewAll;

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
                    if (args.RowIndex >= 0 && args.ColumnIndex != _actionsColumn.Index)
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
            _title.Text = "Quản lý hóa đơn";
            _subtitle.Text = "Tra cứu, xem chi tiết và xem trước hóa đơn bán hàng";
            _exportButton.Text = "Lọc hóa đơn";
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
            _invoiceIdColumn.Visible = false;
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
            _actionsColumn.DefaultCellStyle.BackColor = AppColors.White;
            _actionsColumn.DefaultCellStyle.SelectionBackColor = AppColors.AccentSelectionBg;
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
                using (SqlConnection connection = _connectionFactory.CreateConnection())
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
SELECT i.InvoiceID, i.InvoiceCode, i.CreatedAt,
       ISNULL(customer.FullName, N'Khách lẻ') AS CustomerName,
       cashier.FullName AS CashierName, i.TotalAmount, i.Status
FROM Invoices i
INNER JOIN Users cashier ON cashier.UserID = i.CreatedBy
LEFT JOIN Users customer ON customer.UserID = i.CustomerID
WHERE i.CreatedAt >= @FromDate
  AND i.CreatedAt < DATEADD(DAY, 1, @ToDate)
  AND (@CanViewAll = 1 OR i.CreatedBy = @CurrentUserId)
  AND (@Status = 'ALL' OR i.Status = @Status)
  AND (@Search = '' OR i.InvoiceCode LIKE '%' + @Search + '%'
       OR customer.FullName LIKE '%' + @Search + '%'
       OR cashier.FullName LIKE '%' + @Search + '%')
ORDER BY i.CreatedAt DESC, i.InvoiceID DESC;";
                    command.Parameters.Add("@FromDate", SqlDbType.Date).Value = _fromDate.Value.Date;
                    command.Parameters.Add("@ToDate", SqlDbType.Date).Value = _toDate.Value.Date;
                    command.Parameters.Add("@CanViewAll", SqlDbType.Bit).Value = _canViewAll;
                    command.Parameters.Add("@CurrentUserId", SqlDbType.Int).Value = _currentUserId;
                    command.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value =
                        _statusFilter.SelectedIndex == 1 ? "ACTIVE" :
                        _statusFilter.SelectedIndex == 2 ? "CANCELLED" : "ALL";
                    command.Parameters.Add("@Search", SqlDbType.NVarChar, 100).Value = _searchBox.Text.Trim();

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        _invoicesGrid.Rows.Clear();
                        while (reader.Read())
                        {
                            bool cancelled = string.Equals(reader.GetString(6), "CANCELLED",
                                StringComparison.OrdinalIgnoreCase);
                            int rowIndex = _invoicesGrid.Rows.Add(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetDateTime(2).ToString("dd/MM/yyyy HH:mm"),
                                reader.GetString(3),
                                reader.GetString(4),
                                FormatMoney(reader.GetDecimal(5)),
                                cancelled ? "Đã hủy" : "Hoàn thành",
                                IconBitmapCache.Get(IconChar.Eye, AppColors.TableViewAction, new Size(24, 24)),
                                IconBitmapCache.Get(IconChar.Print, AppColors.Accent, new Size(24, 24)));
                            _invoicesGrid.Rows[rowIndex].Cells["Status"].Style.ForeColor =
                                cancelled ? AppColors.Error : AppColors.Success;
                            _invoicesGrid.Rows[rowIndex].Cells["Actions"].ToolTipText = "Xem chi tiết hóa đơn";
                            _invoicesGrid.Rows[rowIndex].Cells["Print"].ToolTipText = "In hóa đơn";
                        }
                    }
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
            if (e.ColumnIndex == _actionsColumn.Index)
                ShowSelectedInvoice();
            else if (e.ColumnIndex == _invoicesGrid.Columns["Print"].Index)
                PrintSelectedInvoice();
        }

        private void InvoicesGrid_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            _invoicesGrid.Cursor = e.RowIndex >= 0 &&
                (e.ColumnIndex == _actionsColumn.Index || e.ColumnIndex == _invoicesGrid.Columns["Print"].Index)
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

                using (var dialog = new InvoiceDetailsDialog(invoice))
                    dialog.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Không thể tải chi tiết hóa đơn.\r\n" + ex.Message,
                    "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private InvoiceDetails LoadInvoiceDetails(int invoiceId)
        {
            using (SqlConnection connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
SELECT i.InvoiceID, i.InvoiceCode, i.CreatedAt, cashier.FullName,
       ISNULL(customer.FullName, N'Khách lẻ'), i.PaymentMethod, i.SubTotal,
       i.DiscountAmount, i.PromotionCode, i.VATRate, i.VATAmount, i.TotalAmount,
       i.Status, i.CancelReason, i.CancelledAt, i.PointsUsed, i.PointsDiscountAmount
FROM Invoices i
INNER JOIN Users cashier ON cashier.UserID = i.CreatedBy
LEFT JOIN Users customer ON customer.UserID = i.CustomerID
WHERE i.InvoiceID = @InvoiceID
  AND (@CanViewAll = 1 OR i.CreatedBy = @CurrentUserId);";
                    command.Parameters.Add("@InvoiceID", SqlDbType.Int).Value = invoiceId;
                    command.Parameters.Add("@CanViewAll", SqlDbType.Bit).Value = _canViewAll;
                    command.Parameters.Add("@CurrentUserId", SqlDbType.Int).Value = _currentUserId;

                    InvoiceDetails invoice;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            return null;

                        invoice = new InvoiceDetails
                        {
                            InvoiceId = reader.GetInt32(0),
                            InvoiceCode = reader.GetString(1),
                            CreatedAt = reader.GetDateTime(2),
                            CashierName = reader.GetString(3),
                            CustomerName = reader.GetString(4),
                            PaymentMethod = reader.GetString(5),
                            Subtotal = reader.GetDecimal(6),
                            DiscountAmount = reader.GetDecimal(7),
                            PromotionCode = reader.IsDBNull(8) ? string.Empty : reader.GetString(8),
                            VatRate = reader.GetDecimal(9),
                            VatAmount = reader.GetDecimal(10),
                            TotalAmount = reader.GetDecimal(11),
                            IsCancelled = string.Equals(reader.GetString(12), "CANCELLED",
                                StringComparison.OrdinalIgnoreCase),
                            CancelReason = reader.IsDBNull(13) ? string.Empty : reader.GetString(13),
                            CancelledAt = reader.IsDBNull(14) ? (DateTime?)null : reader.GetDateTime(14),
                            PointsUsed = reader.GetInt32(15),
                            PointsDiscountAmount = reader.GetDecimal(16)
                        };
                    }

                    command.Parameters.Clear();
                    command.CommandText = @"
SELECT p.ProductName, d.Quantity, d.UnitPrice, d.LineTotal
FROM InvoiceDetails d
INNER JOIN Products p ON p.ProductID = d.ProductID
WHERE d.InvoiceID = @InvoiceID
ORDER BY d.InvoiceDetailID;";
                    command.Parameters.Add("@InvoiceID", SqlDbType.Int).Value = invoiceId;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            invoice.Lines.Add(new InvoiceLine
                            {
                                ProductName = reader.GetString(0),
                                Quantity = reader.GetInt32(1),
                                UnitPrice = reader.GetDecimal(2),
                                LineTotal = reader.GetDecimal(3)
                            });
                        }
                    }

                    return invoice;
                }
            }
        }

        private static string FormatMoney(decimal amount)
        {
            return amount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + " đ";
        }
    }

    internal sealed class InvoiceDetails
    {
        public int InvoiceId { get; set; }
        public string InvoiceCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CashierName { get; set; }
        public string CustomerName { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public string PromotionCode { get; set; }
        public decimal VatRate { get; set; }
        public decimal VatAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsCancelled { get; set; }
        public string CancelReason { get; set; }
        public DateTime? CancelledAt { get; set; }
        public int PointsUsed { get; set; }
        public decimal PointsDiscountAmount { get; set; }
        public List<InvoiceLine> Lines { get; } = new List<InvoiceLine>();
    }

    internal sealed class InvoiceLine
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }

}

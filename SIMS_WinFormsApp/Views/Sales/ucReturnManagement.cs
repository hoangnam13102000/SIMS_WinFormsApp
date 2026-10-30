using System;
using System.Globalization;
using System.Windows.Forms;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Dialogs;
using SIMS_WinFormsApp.Views.Toast;
using SIMS_WinFormsApp.Views.Sales.InvoiceManagement;
using SIMS_WinFormsApp.Services.Implementations.Sales;

namespace SIMS_WinFormsApp.Views.Sales
{
    public partial class ucReturnManagement : UserControl
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly int _currentUserId;
        private readonly bool _canApprove;
        private readonly ReturnManagementDataService _dataService;

        public ucReturnManagement()
            : this(null, 0, false)
        {
        }

        public ucReturnManagement(IDbConnectionFactory connectionFactory, int currentUserId, bool canApprove)
        {
            _connectionFactory = connectionFactory;
            _dataService = connectionFactory == null ? null : new ReturnManagementDataService(connectionFactory);
            _currentUserId = currentUserId;
            _canApprove = canApprove;

            InitializeComponent();
            ApplyLabels();
            _from.Value = DateTime.Today.AddDays(-30);
            _to.Value = DateTime.Today;
            _reason.SelectedIndex = 0;
            LanguageManager.Instance.LanguageChanged += OnLanguageChanged;
            _approve.Visible = canApprove;
            _reject.Visible = canApprove;
            _grid.CellClick += Grid_CellClick;
            _search.KeyDown += Search_KeyDown;
            _from.ValueChanged += (sender, args) => LoadRequests();
            _to.ValueChanged += (sender, args) => LoadRequests();
            _reason.SelectedIndexChanged += (sender, args) => LoadRequests();
            Load += (sender, args) => LoadRequests();
            Disposed += (sender, args) => LanguageManager.Instance.LanguageChanged -= OnLanguageChanged;
        }

        private void OnLanguageChanged(object sender, EventArgs e) => ApplyLabels();

        private void ApplyLabels()
        {
            _title.Text = Lang.Get("placeholder.returns.title");
            _subtitle.Text = Lang.Get("placeholder.returns.description");
        }

        private void Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            e.SuppressKeyPress = true;
            LoadRequests();
        }

        private void LoadRequests()
        {
            if (_connectionFactory == null || IsDisposed || _from.Value.Date > _to.Value.Date)
                return;

            try
            {
                string selectedReason = _reason.SelectedIndex <= 0
                    ? string.Empty
                    : Convert.ToString(_reason.SelectedItem, CultureInfo.CurrentCulture);
                var requests = _dataService.GetRequests(_from.Value.Date, _to.Value.Date,
                    _canApprove, _currentUserId, _search.Text, selectedReason);
                _grid.Rows.Clear();
                foreach (var request in requests)
                {
                    int rowIndex = _grid.Rows.Add(
                        "TR-" + request.ReturnId.ToString("D6", CultureInfo.InvariantCulture),
                        request.InvoiceCode, request.CustomerName,
                        request.CreatedAt.ToString("dd/MM/yyyy HH:mm"),
                        request.Reason, FormatMoney(request.TotalValue), StatusLabel(request.Status),
                        "Duyệt", "Từ chối");
                    DataGridViewRow row = _grid.Rows[rowIndex];
                    row.Cells["Status"].Style.ForeColor = request.Status == "APPROVED"
                        ? AppColors.Success : request.Status == "REJECTED" ? AppColors.Error : AppColors.Accent;
                    row.Tag = new ReturnRequest(request.ReturnId, request.Status);
                }
            }
            catch (Exception ex)
            {
                AppToast.Error(this, "Lỗi tải dữ liệu",
                    "Không thể tải danh sách yêu cầu trả hàng.\r\n" + ex.Message);
            }
        }

        private void Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!_canApprove || e.RowIndex < 0)
                return;

            var request = _grid.Rows[e.RowIndex].Tag as ReturnRequest;
            if (request == null || request.Status != "PENDING")
                return;

            if (e.ColumnIndex == _grid.Columns["Approve"].Index)
                UpdateStatus(request.ReturnId, true);
            else if (e.ColumnIndex == _grid.Columns["Reject"].Index)
                RejectRequest(request.ReturnId);
        }

        private void RejectRequest(int returnId)
        {
            using (var prompt = new Form
            {
                Text = "Từ chối yêu cầu trả",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false,
                ClientSize = new System.Drawing.Size(420, 150)
            })
            {
                var textBox = new TextBox
                {
                    Multiline = true,
                    MaxLength = 500,
                    Location = new System.Drawing.Point(12, 12),
                    Size = new System.Drawing.Size(396, 76)
                };
                var rejectButton = new Button
                {
                    Text = "Từ chối yêu cầu",
                    DialogResult = DialogResult.OK,
                    Location = new System.Drawing.Point(264, 104),
                    Size = new System.Drawing.Size(144, 32)
                };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(rejectButton);
                prompt.AcceptButton = rejectButton;
                if (prompt.ShowDialog(FindForm()) != DialogResult.OK)
                    return;

                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    AppToast.Warning(this, "Thiếu lý do", "Nhập lý do từ chối để tiếp tục.");
                    return;
                }

                UpdateStatus(returnId, false, textBox.Text.Trim());
            }
        }

        private void UpdateStatus(int returnId, bool approve, string rejectionReason = null)
        {
            string action = approve ? "duyệt" : "từ chối";
            if (!DialogHelper.Confirm(this, "Xác nhận",
                    "Xác nhận " + action + " yêu cầu trả hàng?"))
                return;

            try
            {
                if (!_dataService.UpdateRequest(returnId, approve, rejectionReason, _currentUserId))
                {
                    AppToast.Warning(this, "Không thể cập nhật",
                        "Yêu cầu đã được xử lý hoặc không còn chờ duyệt.");
                    LoadRequests();
                    return;
                }
                AppToast.Success(this, approve ? "Đã duyệt yêu cầu trả hàng." :
                    "Đã từ chối yêu cầu trả hàng.");
                LoadRequests();
            }
            catch (Exception ex)
            {
                AppToast.Error(this, "Lỗi xử lý",
                    "Không thể " + action + " yêu cầu.\r\n" + ex.Message);
            }
        }

        private static string StatusLabel(string status)
        {
            switch (status)
            {
                case "APPROVED": return "Đã duyệt";
                case "REJECTED": return "Đã từ chối";
                default: return "Chờ duyệt";
            }
        }

        private static string FormatMoney(decimal amount)
        {
            return amount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + " đ";
        }

        private sealed class ReturnRequest
        {
            public ReturnRequest(int returnId, string status)
            {
                ReturnId = returnId;
                Status = status;
            }

            public int ReturnId { get; }
            public string Status { get; }
        }
    }
}

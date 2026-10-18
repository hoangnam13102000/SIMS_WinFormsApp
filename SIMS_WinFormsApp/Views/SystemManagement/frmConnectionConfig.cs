using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.Services; 
using SIMS_WinFormsApp.Views.Dialogs;

namespace SIMS_WinFormsApp.Views.SystemManagement
{
    public partial class frmConnectionConfig : Form
    {
        private readonly IDialogService _dialogService; 
        private readonly IConnectionConfigurationService _configurationService;

        public frmConnectionConfig() : this(null)
        {
        }

        public frmConnectionConfig(IConnectionConfigurationService configurationService = null)
        {
            InitializeComponent();
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                _dialogService = null;
                _configurationService = configurationService;
                return;
            }

            _dialogService = AppComposition.CreateDialogService();
            _configurationService = configurationService ??
                AppComposition.CreateConnectionConfigurationService();
            LoadCurrentSettings();
            UpdateAuthUI();
            SetStatus("", null);
        }

        private void LoadCurrentSettings()
        {
            try
            {
                string connectionString = _configurationService.GetCurrentConnectionString();
                if (string.IsNullOrWhiteSpace(connectionString))
                    return;
                var builder = new SqlConnectionStringBuilder(connectionString);
                txtServer.Text = builder.DataSource;
                txtDatabase.Text = builder.InitialCatalog;
                if (builder.IntegratedSecurity)
                {
                    rbWindowsAuth.Checked = true;
                    txtUserId.Text = "";
                    txtPassword.Text = "";
                }
                else
                {
                    rbSqlAuth.Checked = true;
                    txtUserId.Text = builder.UserID;
                    txtPassword.Text = "";
                }
            }
            catch
            {
            }
        }

        private void UpdateAuthUI()
        {
            bool isSql = rbSqlAuth.Checked;
            lblUserId.Enabled = isSql;
            txtUserId.Enabled = isSql;
            lblPassword.Enabled = isSql;
            txtPassword.Enabled = isSql;
            if (!isSql)
            {
                txtUserId.Text = "";
                txtPassword.Text = "";
            }
        }

        private void rbWindowsAuth_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAuthUI();
        }

        private void rbSqlAuth_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAuthUI();
        }

        private string BuildConnectionStringFromUI()
        {
            if (string.IsNullOrWhiteSpace(txtServer.Text))
            {
                DialogHelper.ShowWarning(this, "Thiếu thông tin", "Vui lòng nhập Server.");
                txtServer.Focus();
                return null;
            }
            if (string.IsNullOrWhiteSpace(txtDatabase.Text))
            {
                DialogHelper.ShowWarning(this, "Thiếu thông tin", "Vui lòng nhập Database.");
                txtDatabase.Focus();
                return null;
            }
            if (rbSqlAuth.Checked && string.IsNullOrWhiteSpace(txtUserId.Text))
            {
                DialogHelper.ShowWarning(this, "Thiếu thông tin",
                    "Vui lòng nhập User ID khi dùng SQL Server Authentication.");
                txtUserId.Focus();
                return null;
            }
            return _configurationService.BuildConnectionString(
                txtServer.Text,
                txtDatabase.Text,
                rbWindowsAuth.Checked,
                txtUserId.Text,
                txtPassword.Text);
        }

        private void SetStatus(string message, bool? success)
        {
            lblStatus.Text = message ?? "";
            if (success == true)
            {
                iconStatus.IconChar = IconChar.CircleCheck;
                iconStatus.IconColor = Color.Green;
                lblStatus.ForeColor = Color.Green;
            }
            else if (success == false)
            {
                iconStatus.IconChar = IconChar.CircleXmark;
                iconStatus.IconColor = Color.Red;
                lblStatus.ForeColor = Color.Red;
            }
            else if (!string.IsNullOrEmpty(message))
            {
                iconStatus.IconChar = IconChar.Spinner;
                iconStatus.IconColor = Color.DarkOrange;
                lblStatus.ForeColor = Color.DarkOrange;
            }
            else
            {
                iconStatus.IconChar = IconChar.None;
                iconStatus.IconColor = Color.Gray;
                lblStatus.ForeColor = SystemColors.ControlText;
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string connStr = BuildConnectionStringFromUI();
            if (connStr == null) return;
            btnTest.Enabled = false;
            SetStatus("Đang kiểm tra kết nối...", null);
            Application.DoEvents();
            try
            {
                if (_configurationService.TestConnection(connStr, out string error))
                {
                    SetStatus("Kết nối thành công!", true);
                    DialogHelper.ShowSuccess(this, "Test Connection", "Kết nối SQL Server thành công!");
                }
                else
                {
                    SetStatus("Kết nối thất bại.", false);
                    DialogHelper.ShowError(this, "Test Connection", "Không thể kết nối:\n\n" + error);
                }
            }
            finally
            {
                btnTest.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string connStr = BuildConnectionStringFromUI();
            if (connStr == null) return;

            if (!_configurationService.TestConnection(connStr, out string error))
            {

                bool confirmed = _dialogService.ConfirmCustom(
                    owner: this,
                    title: "Cảnh báo",
                    message: "Kết nối hiện tại thất bại:\n\n" + error +
                             "\n\nBạn vẫn muốn lưu cấu hình này?",
                    confirmText: "Vẫn lưu",
                    cancelText: "Hủy",
                    accentColor: SIMS_WinFormsApp.UI.Theme.AppColors.Warning);

                if (!confirmed)
                    return;
            }

            try
            {
                _configurationService.SaveConnectionString(connStr);
                SetStatus("Đã lưu và mã hóa thành công.", true);
                DialogHelper.ShowInfo(this, "Lưu thành công",
                    "Đã lưu ConnectionString vào App.config và mã hóa thành công!\n\n" +
                    "Lưu ý: Nên khởi động lại ứng dụng để áp dụng hoàn toàn.");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                SetStatus("Lỗi khi lưu.", false);
                DialogHelper.ShowError(this, "Lỗi", "Không thể lưu cấu hình:\n\n" + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
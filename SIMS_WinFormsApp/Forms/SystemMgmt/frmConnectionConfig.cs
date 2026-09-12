using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.Infrastructure.Configuration;
using SIMS_WinFormsApp.Services; 

namespace SIMS_WinFormsApp.Forms.SystemMgmt
{
    public partial class frmConnectionConfig : Form
    {
        private readonly IDialogService _dialogService; 
        private readonly IConnectionConfigurationService _configurationService;

        public frmConnectionConfig(IConnectionConfigurationService configurationService = null)
        {
            InitializeComponent();
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
                MessageBox.Show("Vui lòng nhập Server.", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtServer.Focus();
                return null;
            }
            if (string.IsNullOrWhiteSpace(txtDatabase.Text))
            {
                MessageBox.Show("Vui lòng nhập Database.", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDatabase.Focus();
                return null;
            }
            if (rbSqlAuth.Checked && string.IsNullOrWhiteSpace(txtUserId.Text))
            {
                MessageBox.Show("Vui lòng nhập User ID khi dùng SQL Server Authentication.",
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show("Kết nối SQL Server thành công!", "Test Connection",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    SetStatus("Kết nối thất bại.", false);
                    MessageBox.Show("Không thể kết nối:\n\n" + error, "Test Connection",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(
                    "Đã lưu ConnectionString vào App.config và mã hóa thành công!\n\n" +
                    "Lưu ý: Nên khởi động lại ứng dụng để áp dụng hoàn toàn.",
                    "Lưu thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                SetStatus("Lỗi khi lưu.", false);
                MessageBox.Show("Không thể lưu cấu hình:\n\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
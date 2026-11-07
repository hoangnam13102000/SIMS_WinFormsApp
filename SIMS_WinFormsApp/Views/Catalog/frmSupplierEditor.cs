using System;
using System.Windows.Forms;
using SIMS_WinFormsApp.Models.DTOs.Catalog;

namespace SIMS_WinFormsApp.Views.Catalog
{
    public sealed partial class frmSupplierEditor : Form
    {
        private SupplierEditDto _model;
        private readonly Func<SupplierEditDto, CatalogSaveResult> _save;
        private CatalogSaveResult _saveResult;

        public frmSupplierEditor()
        {
            InitializeComponent();
            Text = "Nhà cung cấp";
        }

        private frmSupplierEditor(
            IWin32Window owner,
            SupplierEditDto model,
            bool readOnly,
            Func<SupplierEditDto, CatalogSaveResult> save)
        {
            InitializeComponent();
            _model = model ?? new SupplierEditDto();
            _save = save;
            Text = readOnly
                ? "Chi tiết nhà cung cấp"
                : (_model.SupplierId > 0 ? "Sửa nhà cung cấp" : "Thêm nhà cung cấp");
            _nameTextBox.Text = _model.Name ?? string.Empty;
            _phoneTextBox.Text = _model.Phone ?? string.Empty;
            _emailTextBox.Text = _model.Email ?? string.Empty;
            _addressTextBox.Text = _model.Address ?? string.Empty;
            _itemsTextBox.Text = _model.SuppliedItems ?? string.Empty;

            _nameTextBox.ReadOnly = readOnly;
            _phoneTextBox.ReadOnly = readOnly;
            _emailTextBox.ReadOnly = readOnly;
            _addressTextBox.ReadOnly = readOnly;
            _itemsTextBox.ReadOnly = readOnly;
            _saveButton.Visible = !readOnly;
            _cancelButton.Text = readOnly ? "Đóng" : "Hủy";
        }

        public static bool TryEdit(
            IWin32Window owner,
            SupplierEditDto model,
            bool readOnly,
            Func<SupplierEditDto, CatalogSaveResult> save,
            out SupplierEditDto result,
            out CatalogSaveResult saveResult)
        {
            result = null;
            saveResult = null;
            using (var dialog = new frmSupplierEditor(owner, model.Copy(), readOnly, save))
            {
                if (dialog.ShowDialog(owner) != DialogResult.OK) return false;
                result = dialog._model;
                saveResult = dialog._saveResult;
                return true;
            }
        }

        private void _saveButton_Click(object sender, System.EventArgs e)
        {
            _model.Name = _nameTextBox.Text;
            _model.Phone = _phoneTextBox.Text;
            _model.Email = _emailTextBox.Text;
            _model.Address = _addressTextBox.Text;
            _model.SuppliedItems = _itemsTextBox.Text;
            if (string.IsNullOrWhiteSpace(_model.Name))
            {
                MessageBox.Show(this, "Nhập tên nhà cung cấp.", "Thông tin chưa hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_model.Name.Trim().Length > 150)
            {
                MessageBox.Show(this, "Tên nhà cung cấp tối đa 150 ký tự.", "Thông tin chưa hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrWhiteSpace(_model.Phone) && _model.Phone.Trim().Length > 20)
            {
                MessageBox.Show(this, "Số điện thoại tối đa 20 ký tự.", "Thông tin chưa hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrWhiteSpace(_model.Email) && _model.Email.Trim().Length > 100)
            {
                MessageBox.Show(this, "Email tối đa 100 ký tự.", "Thông tin chưa hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_save == null)
            {
                MessageBox.Show(this, "Không thể lưu nhà cung cấp.", "Lỗi lưu dữ liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _saveResult = _save(_model);
            if (_saveResult == null || !_saveResult.Success)
            {
                MessageBox.Show(this,
                    _saveResult == null ? "Không thể lưu nhà cung cấp." : _saveResult.Message,
                    "Không thể lưu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

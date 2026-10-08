using System.Windows.Forms;
using SIMS_WinFormsApp.Models.DTOs.Catalog;

namespace SIMS_WinFormsApp.Views.Catalog
{
    public sealed partial class frmSupplierEditor : Form
    {
        private SupplierEditDto _model;

        public frmSupplierEditor()
        {
            InitializeComponent();
            Text = "Nhà cung cấp";
        }

        private frmSupplierEditor(IWin32Window owner, SupplierEditDto model, bool readOnly)
        {
            InitializeComponent();
            _model = model ?? new SupplierEditDto();
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
            _saveButton.Click += (sender, args) => SaveAndClose();
            _cancelButton.Click += (sender, args) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };
        }

        public static bool TryEdit(IWin32Window owner, SupplierEditDto model, bool readOnly, out SupplierEditDto result)
        {
            result = null;
            using (var dialog = new frmSupplierEditor(owner, model.Copy(), readOnly))
            {
                if (dialog.ShowDialog(owner) != DialogResult.OK) return false;
                result = dialog._model;
                return true;
            }
        }

        private void SaveAndClose()
        {
            _model.Name = _nameTextBox.Text;
            _model.Phone = _phoneTextBox.Text;
            _model.Email = _emailTextBox.Text;
            _model.Address = _addressTextBox.Text;
            _model.SuppliedItems = _itemsTextBox.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

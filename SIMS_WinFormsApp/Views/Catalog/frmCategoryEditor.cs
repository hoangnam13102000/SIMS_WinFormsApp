using System.ComponentModel;
using System.Windows.Forms;
using SIMS_WinFormsApp.Models.DTOs.Catalog;

namespace SIMS_WinFormsApp.Views.Catalog
{
    public sealed partial class frmCategoryEditor : Form
    {
        private CategoryEditDto _model;

        public frmCategoryEditor()
        {
            InitializeComponent();
            Text = "Danh mục";
        }

        private frmCategoryEditor(IWin32Window owner, CategoryEditDto model, bool readOnly)
        {
            InitializeComponent();
            _model = model ?? new CategoryEditDto();
            Text = readOnly
                ? "Chi tiết danh mục"
                : (_model.CategoryId > 0 ? "Sửa danh mục" : "Thêm danh mục");
            _nameTextBox.Text = _model.Name ?? string.Empty;
            _statusComboBox.SelectedIndex = _model.IsActive ? 0 : 1;

            _nameTextBox.ReadOnly = readOnly;
            _statusComboBox.Enabled = !readOnly;
            _saveButton.Visible = !readOnly;
            _saveButton.Click += (sender, args) => SaveAndClose();
            _cancelButton.Text = readOnly ? "Đóng" : "Hủy";
            _cancelButton.Click += (sender, args) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };
        }

        public static bool TryEdit(IWin32Window owner, CategoryEditDto model, bool readOnly, out CategoryEditDto result)
        {
            result = null;
            using (var dialog = new frmCategoryEditor(owner, model.Copy(), readOnly))
            {
                if (dialog.ShowDialog(owner) != DialogResult.OK) return false;
                result = dialog._model;
                return true;
            }
        }

        private void SaveAndClose()
        {
            _model.Name = _nameTextBox.Text;
            _model.IsActive = _statusComboBox.SelectedIndex == 0;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

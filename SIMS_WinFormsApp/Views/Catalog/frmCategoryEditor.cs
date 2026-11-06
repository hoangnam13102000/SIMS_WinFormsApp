using System;
using System.ComponentModel;
using System.Windows.Forms;
using SIMS_WinFormsApp.Models.DTOs.Catalog;

namespace SIMS_WinFormsApp.Views.Catalog
{
    public sealed partial class frmCategoryEditor : Form
    {
        private CategoryEditDto _model;
        private readonly Func<CategoryEditDto, CatalogSaveResult> _save;

        public frmCategoryEditor()
        {
            InitializeComponent();
            _model = new CategoryEditDto();
            _statusComboBox.SelectedIndex = 0;
            Text = "Danh mục";
        }

        private frmCategoryEditor(
            IWin32Window owner,
            CategoryEditDto model,
            bool readOnly,
            Func<CategoryEditDto, CatalogSaveResult> save)
        {
            InitializeComponent();
            _model = model ?? new CategoryEditDto();
            _save = save;
            Text = readOnly
                ? "Chi tiết danh mục"
                : (_model.CategoryId > 0 ? "Sửa danh mục" : "Thêm danh mục");
            _nameTextBox.Text = _model.Name ?? string.Empty;
            _statusComboBox.SelectedIndex = _model.IsActive ? 0 : 1;

            _nameTextBox.ReadOnly = readOnly;
            _statusComboBox.Enabled = !readOnly;
            _saveButton.Visible = !readOnly;
            _cancelButton.Text = readOnly ? "Đóng" : "Hủy";
        }

        public static bool TryEdit(
            IWin32Window owner,
            CategoryEditDto model,
            bool readOnly,
            Func<CategoryEditDto, CatalogSaveResult> save,
            out CategoryEditDto result,
            out CatalogSaveResult saveResult)
        {
            result = null;
            saveResult = null;
            using (var dialog = new frmCategoryEditor(owner, model.Copy(), readOnly, save))
            {
                if (dialog.ShowDialog(owner) != DialogResult.OK) return false;
                result = dialog._model;
                saveResult = dialog._saveResult;
                return true;
            }
        }

        private CatalogSaveResult _saveResult;

        private void _cancelButton_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void _saveButton_Click(object sender, System.EventArgs e)
        {
            _model.Name = _nameTextBox.Text;
            _model.IsActive = _statusComboBox.SelectedIndex == 0;
            if (string.IsNullOrWhiteSpace(_model.Name))
            {
                MessageBox.Show(this, "Nhập tên danh mục.", "Thông tin chưa hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_model.Name.Trim().Length > 100)
            {
                MessageBox.Show(this, "Tên danh mục tối đa 100 ký tự.", "Thông tin chưa hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_save == null)
            {
                MessageBox.Show(this, "Không thể lưu danh mục.", "Lỗi lưu dữ liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _saveResult = _save(_model);
            if (_saveResult == null || !_saveResult.Success)
            {
                MessageBox.Show(this,
                    _saveResult == null ? "Không thể lưu danh mục." : _saveResult.Message,
                    "Không thể lưu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

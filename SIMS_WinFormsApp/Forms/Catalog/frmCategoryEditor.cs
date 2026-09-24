using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Forms.Catalog
{
    public sealed class frmCategoryEditor : BaseFormDialogForm
    {
        private readonly CategoryEditDto _model;
        private readonly LabeledIconField _name;
        private readonly LabeledComboField _status;

        private frmCategoryEditor(IWin32Window owner, CategoryEditDto model, bool readOnly) : base(owner)
        {
            _model = model ?? new CategoryEditDto();
            HeaderTitle = readOnly ? "Chi tiết danh mục" : (_model.CategoryId > 0 ? "Sửa danh mục" : "Thêm danh mục");
            SetHeaderIcon(IconChar.Tags, AppColors.Accent);
            Size = new Size(620, 420);

            _name = new LabeledIconField { LabelText = "Tên danh mục", Icon = IconChar.Tags, IsRequired = true, Dock = DockStyle.Top };
            _status = new LabeledComboField { LabelText = "Trạng thái", IsRequired = true, Dock = DockStyle.Top };
            _status.SetItems(new[] { "Đang hoạt động", "Vô hiệu hóa" });
            _name.Value = _model.Name;
            _status.SelectedItem = _model.IsActive ? "Đang hoạt động" : "Vô hiệu hóa";
            if (readOnly) { _name.Enabled = false; _status.Enabled = false; }

            ContentHost.Controls.Add(_status);
            ContentHost.Controls.Add(_name);

            AddFooterButton("Đóng", false, (s, e) => { DialogResult = DialogResult.Cancel; Close(); });
            if (!readOnly)
                AddFooterButton("Lưu", true, (s, e) => SaveAndClose());
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
            _model.Name = _name.Value;
            _model.IsActive = !string.Equals(_status.SelectedItem as string, "Vô hiệu hóa");
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

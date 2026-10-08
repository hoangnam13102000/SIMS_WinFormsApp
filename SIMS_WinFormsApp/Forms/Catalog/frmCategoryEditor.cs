using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Forms.Catalog
{
    public sealed partial class frmCategoryEditor : BaseFormDialogForm
    {
        private CategoryEditDto _model;
        private LabeledIconField _name;
        private LabeledComboField _status;

        public frmCategoryEditor()
        {
            InitializeComponent();
            HeaderTitle = "Danh mục";
            SetHeaderIcon(IconChar.Tags, AppColors.Accent);
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
        }

        private frmCategoryEditor(IWin32Window owner, CategoryEditDto model, bool readOnly) : base(owner)
        {
            InitializeComponent();
            _model = model ?? new CategoryEditDto();
            HeaderTitle = readOnly ? "Chi tiết danh mục" : (_model.CategoryId > 0 ? "Sửa danh mục" : "Thêm danh mục");
            SetHeaderIcon(IconChar.Tags, AppColors.Accent);

            _status.SetItems(new[] { "Đang hoạt động", "Vô hiệu hóa" });
            _name.Value = _model.Name;
            _status.SelectedItem = _model.IsActive ? "Đang hoạt động" : "Vô hiệu hóa";
            if (readOnly) { _name.Enabled = false; _status.Enabled = false; }

            CloseRequested += CloseRequestedHandler;

            AddFooterButton("Đóng", false, CloseButton_Click);
            if (!readOnly)
                AddFooterButton("Lưu", true, SaveButton_Click);
        }

        private void CloseRequestedHandler(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void CloseButton_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void SaveButton_Click(object sender, System.EventArgs e)
        {
            SaveAndClose();
        }

        protected override void OnContentReady()
        {
            FitHeightToContent();
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

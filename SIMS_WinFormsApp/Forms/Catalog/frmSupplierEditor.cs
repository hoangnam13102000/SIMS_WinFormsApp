using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Forms.Catalog
{
    public sealed partial class frmSupplierEditor : BaseFormDialogForm
    {
        private SupplierEditDto _model;
        private LabeledIconField _name;
        private LabeledIconField _phone;
        private LabeledIconField _email;
        private LabeledIconField _address;
        private LabeledIconField _items;

        public frmSupplierEditor()
        {
            InitializeComponent();
            HeaderTitle = "Nhà cung cấp";
            SetHeaderIcon(IconChar.Building, AppColors.Accent);
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
        }

        private frmSupplierEditor(IWin32Window owner, SupplierEditDto model, bool readOnly) : base(owner)
        {
            InitializeComponent();
            _model = model ?? new SupplierEditDto();
            HeaderTitle = readOnly ? "Chi tiết nhà cung cấp" : (_model.SupplierId > 0 ? "Sửa nhà cung cấp" : "Thêm nhà cung cấp");
            SetHeaderIcon(IconChar.Building, AppColors.Accent);

            _name.Value = _model.Name;
            _phone.Value = _model.Phone;
            _email.Value = _model.Email;
            _address.Value = _model.Address;
            _items.Value = _model.SuppliedItems;
            if (readOnly)
            {
                _name.Enabled = false;
                _phone.Enabled = false;
                _email.Enabled = false;
                _address.Enabled = false;
                _items.Enabled = false;
            }

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
            _model.Name = _name.Value;
            _model.Phone = _phone.Value;
            _model.Email = _email.Value;
            _model.Address = _address.Value;
            _model.SuppliedItems = _items.Value;
            DialogResult = DialogResult.OK;
            Close();
        }

    }
}

using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Forms.Catalog
{
    public sealed class frmSupplierEditor : BaseFormDialogForm
    {
        private readonly SupplierEditDto _model;
        private readonly LabeledIconField _name;
        private readonly LabeledIconField _phone;
        private readonly LabeledIconField _email;
        private readonly LabeledIconField _address;
        private readonly LabeledIconField _items;

        private frmSupplierEditor(IWin32Window owner, SupplierEditDto model, bool readOnly) : base(owner)
        {
            _model = model ?? new SupplierEditDto();
            HeaderTitle = readOnly ? "Chi tiết nhà cung cấp" : (_model.SupplierId > 0 ? "Sửa nhà cung cấp" : "Thêm nhà cung cấp");
            SetHeaderIcon(IconChar.Building, AppColors.Accent);
            Size = new Size(640, 640);

            _name = Field("Tên nhà cung cấp", IconChar.Building, true);
            _phone = Field("Số điện thoại", IconChar.PhoneVolume, false);
            _email = Field("Email", IconChar.EnvelopeOpen, false);
            _address = Field("Địa chỉ", IconChar.MapMarkerAlt, false);
            _items = Field("Mặt hàng cung cấp", IconChar.Box, false);
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

            ContentHost.Controls.Add(_items);
            ContentHost.Controls.Add(_address);
            ContentHost.Controls.Add(_email);
            ContentHost.Controls.Add(_phone);
            ContentHost.Controls.Add(_name);

            CloseRequested += (s, e) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            AddFooterButton("Đóng", false, (s, e) => { DialogResult = DialogResult.Cancel; Close(); });
            if (!readOnly)
                AddFooterButton("Lưu", true, (s, e) => SaveAndClose());
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

        private static LabeledIconField Field(string label, IconChar icon, bool required)
        {
            return new LabeledIconField { LabelText = label, Icon = icon, IsRequired = required, Dock = DockStyle.Top };
        }
    }
}

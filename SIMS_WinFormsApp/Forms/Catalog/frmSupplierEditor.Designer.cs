using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Controls;

namespace SIMS_WinFormsApp.Forms.Catalog
{
    partial class frmSupplierEditor
    {
        private void InitializeComponent()
        {
            this._name = new SIMS_WinFormsApp.UI.Controls.LabeledIconField();
            this._phone = new SIMS_WinFormsApp.UI.Controls.LabeledIconField();
            this._email = new SIMS_WinFormsApp.UI.Controls.LabeledIconField();
            this._address = new SIMS_WinFormsApp.UI.Controls.LabeledIconField();
            this._items = new SIMS_WinFormsApp.UI.Controls.LabeledIconField();
            this.SuspendLayout();
            // 
            // _name
            // 
            this._name.AutoSize = true;
            this._name.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._name.BackColor = System.Drawing.Color.Transparent;
            this._name.Dock = System.Windows.Forms.DockStyle.Top;
            this._name.HintText = "";
            this._name.Icon = FontAwesome.Sharp.IconChar.Building;
            this._name.IsRequired = true;
            this._name.LabelText = "Tên nhà cung cấp";
            this._name.Location = new System.Drawing.Point(28, 20);
            this._name.MaxLength = 32767;
            this._name.Name = "_name";
            this._name.PlaceholderText = "";
            this._name.Size = new System.Drawing.Size(830, 108);
            this._name.TabIndex = 4;
            this._name.UseThousandsSeparator = false;
            this._name.Value = "";
            // 
            // _phone
            // 
            this._phone.AutoSize = true;
            this._phone.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._phone.BackColor = System.Drawing.Color.Transparent;
            this._phone.Dock = System.Windows.Forms.DockStyle.Top;
            this._phone.HintText = "";
            this._phone.Icon = FontAwesome.Sharp.IconChar.PhoneVolume;
            this._phone.IsRequired = false;
            this._phone.LabelText = "Số điện thoại";
            this._phone.Location = new System.Drawing.Point(28, 128);
            this._phone.MaxLength = 32767;
            this._phone.Name = "_phone";
            this._phone.PlaceholderText = "";
            this._phone.Size = new System.Drawing.Size(830, 108);
            this._phone.TabIndex = 3;
            this._phone.UseThousandsSeparator = false;
            this._phone.Value = "";
            // 
            // _email
            // 
            this._email.AutoSize = true;
            this._email.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._email.BackColor = System.Drawing.Color.Transparent;
            this._email.Dock = System.Windows.Forms.DockStyle.Top;
            this._email.HintText = "";
            this._email.Icon = FontAwesome.Sharp.IconChar.EnvelopeOpen;
            this._email.IsRequired = false;
            this._email.LabelText = "Email";
            this._email.Location = new System.Drawing.Point(28, 236);
            this._email.MaxLength = 32767;
            this._email.Name = "_email";
            this._email.PlaceholderText = "";
            this._email.Size = new System.Drawing.Size(830, 108);
            this._email.TabIndex = 2;
            this._email.UseThousandsSeparator = false;
            this._email.Value = "";
            // 
            // _address
            // 
            this._address.AutoSize = true;
            this._address.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._address.BackColor = System.Drawing.Color.Transparent;
            this._address.Dock = System.Windows.Forms.DockStyle.Top;
            this._address.HintText = "";
            this._address.Icon = FontAwesome.Sharp.IconChar.MapMarkerAlt;
            this._address.IsRequired = false;
            this._address.LabelText = "Địa chỉ";
            this._address.Location = new System.Drawing.Point(28, 344);
            this._address.MaxLength = 32767;
            this._address.Name = "_address";
            this._address.PlaceholderText = "";
            this._address.Size = new System.Drawing.Size(830, 108);
            this._address.TabIndex = 1;
            this._address.UseThousandsSeparator = false;
            this._address.Value = "";
            // 
            // _items
            // 
            this._items.AutoSize = true;
            this._items.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._items.BackColor = System.Drawing.Color.Transparent;
            this._items.Dock = System.Windows.Forms.DockStyle.Top;
            this._items.HintText = "";
            this._items.Icon = FontAwesome.Sharp.IconChar.Box;
            this._items.IsRequired = false;
            this._items.LabelText = "Mặt hàng cung cấp";
            this._items.Location = new System.Drawing.Point(28, 452);
            this._items.MaxLength = 32767;
            this._items.Name = "_items";
            this._items.PlaceholderText = "";
            this._items.Size = new System.Drawing.Size(830, 108);
            this._items.TabIndex = 0;
            this._items.UseThousandsSeparator = false;
            this._items.Value = "";
            // 
            // frmSupplierEditor
            // 
            this.ClientSize = new System.Drawing.Size(890, 736);
            this.Name = "frmSupplierEditor";
            this.Text = "Nhà cung cấp";
            this.ResumeLayout(false);

        }
    }
}

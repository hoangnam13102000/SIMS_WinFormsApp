using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Dialogs;

namespace SIMS_WinFormsApp.UI.Controls.Pos
{
    internal sealed partial class frmListPickerDialog : BaseFormDialogForm
    {
        private object _selected;
        private IEnumerable _items;
        private Func<object, string> _textSelector;

        public frmListPickerDialog()
        {
            InitializeComponent();
            HeaderTitle = "Chọn mục";
            SetHeaderIcon(IconChar.List, AppColors.Accent);
        }

        private frmListPickerDialog(IWin32Window owner, string title, IEnumerable items, Func<object, string> textSelector)
            : base(owner)
        {
            InitializeComponent();
            _items = items;
            _textSelector = textSelector;
            HeaderTitle = title;
            SetHeaderIcon(IconChar.List, AppColors.Accent);
        }

        private void frmListPickerDialog_Load(object sender, EventArgs e)
        {
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            _listBox.Items.Clear();
            foreach (var item in _items)
                _listBox.Items.Add(new PickerItem(item, _textSelector(item)));
        }

        private void ListBox_DoubleClick(object sender, EventArgs e) => ConfirmSelection();

        private void CancelButton_Click(object sender, EventArgs e) => RaiseCloseRequested();

        private void ConfirmButton_Click(object sender, EventArgs e) => ConfirmSelection();

        private void CloseRequestedHandler(object sender, EventArgs e) => Close();

        private void ConfirmSelection()
        {
            if (!(_listBox.SelectedItem is PickerItem picked)) return;
            _selected = picked.Value;
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>Trả về mục được chọn (kiểu gốc, chưa bọc PickerItem), hoặc null nếu người dùng hủy.</summary>
        public static object Pick(IWin32Window owner, string title, IEnumerable items, Func<object, string> textSelector)
        {
            using (var dialog = new frmListPickerDialog(owner, title, items, textSelector))
            {
                return dialog.ShowDialog(owner) == DialogResult.OK ? dialog._selected : null;
            }
        }

        private sealed class PickerItem
        {
            public object Value { get; }
            private readonly string _text;
            public PickerItem(object value, string text) { Value = value; _text = text; }
            public override string ToString() => _text;
        }
    }
}
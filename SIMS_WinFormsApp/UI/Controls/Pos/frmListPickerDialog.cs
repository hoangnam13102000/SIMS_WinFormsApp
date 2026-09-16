using System;
using System.Collections;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls.Pos
{
    internal sealed class frmListPickerDialog : BaseFormDialogForm
    {
        private readonly ListBox _listBox;
        private object _selected;

        private frmListPickerDialog(IWin32Window owner, string title, IEnumerable items, Func<object, string> textSelector)
            : base(owner)
        {
            Size = new System.Drawing.Size(420, 480);
            HeaderTitle = title;
            SetHeaderIcon(IconChar.List, AppColors.Accent);

            _listBox = new ListBox
            {
                Dock = DockStyle.Top,
                Height = 340,
                Font = AppFonts.Body,
                IntegralHeight = false,
                BorderStyle = BorderStyle.FixedSingle
            };
            foreach (var item in items)
                _listBox.Items.Add(new PickerItem(item, textSelector(item)));

            _listBox.DoubleClick += (s, e) => ConfirmSelection();

            ContentHost.Controls.Add(_listBox);

            AddFooterButton("Hủy", false, (s, e) => RaiseCloseRequested());
            AddFooterButton("Chọn", true, (s, e) => ConfirmSelection());
            CloseRequested += (s, e) => Close();
        }

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
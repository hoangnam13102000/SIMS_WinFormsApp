using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls.Filter
{

    public sealed class FilterComboBox : ComboBox, IFilterView
    {
        public event EventHandler OptionChanged;

        public FilterComboBox()
        {
            Font = AppFonts.Input;
            DropDownStyle = ComboBoxStyle.DropDownList;
            FlatStyle = FlatStyle.Flat;
            BackColor = AppColors.White;
            ForeColor = AppColors.TextPrimary;
            Height = 42;

            SelectedIndexChanged += (_, __) => OptionChanged?.Invoke(this, EventArgs.Empty);
            ThemeManager.Instance.ThemeChanged += OnThemeChanged;
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            BackColor = AppColors.White;
            ForeColor = AppColors.TextPrimary;
            Invalidate();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                ThemeManager.Instance.ThemeChanged -= OnThemeChanged;
            base.Dispose(disposing);
        }

        public void SetOptions(IList<FilterOption> options)
        {
            options = options ?? new List<FilterOption>();

            BeginUpdate();
            Items.Clear();
            foreach (var option in options)
                Items.Add(option);
            EndUpdate();

            if (Items.Count > 0)
                SelectedIndex = 0;
        }

        public FilterOption SelectedOption => SelectedItem as FilterOption;
    }
}
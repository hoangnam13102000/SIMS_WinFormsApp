using System;
using System.Collections.Generic;
using System.Drawing;
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

        // MỚI: ComboBox chuẩn của WinForms khi đặt FlatStyle = Flat KHÔNG tự vẽ viền quanh toàn
        // bộ ô trên Windows hiện đại (comctl32 v6) - chỉ còn lại khối xám quanh riêng mũi tên
        // sổ xuống, đúng như trong ảnh chụp màn "Danh sách nhân viên" (ô "Tất cả trạng thái"
        // không có viền bao quanh). Đây là hạn chế đã biết của control này, không phải lỗi do
        // logic SetOptions/SelectedOption. Vẽ đè thêm 1 viền mỏng ngay sau khi Windows vẽ xong
        // nội dung control (chặn đúng thông điệp WM_PAINT) - không owner-draw toàn bộ control
        // (DrawMode.OwnerDrawFixed) để khỏi phải tự vẽ lại text/mũi tên, tránh đụng tới cách
        // hiển thị dữ liệu đang có.
        private const int WM_PAINT = 0x000F;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_PAINT && IsHandleCreated)
            {
                using (var g = Graphics.FromHwnd(Handle))
                using (var pen = new Pen(AppColors.FieldBorder))
                {
                    g.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
                }
            }
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
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIMS_WinFormsApp.Infrastructure;


namespace SIMS_WinFormsApp.UI.Theme
{
    public sealed class ThemeManager
    {
        private const string PrefKeyMode = "sims.theme.mode";
        private const string PrefKeyAccent = "sims.theme.accent";

        private static readonly Lazy<ThemeManager> LazyInstance = new Lazy<ThemeManager>(() => new ThemeManager());
        public static ThemeManager Instance => LazyInstance.Value;

        public event EventHandler ThemeChanged;

        public ThemeMode Mode { get; private set; }
        public bool IsDark => Mode == ThemeMode.Dark;
        public AccentColor Accent => AppColors.CurrentAccent;

        private ThemeManager()
        {
            var savedMode = AppSettingsStore.Get(PrefKeyMode, ThemeMode.Light.ToString());
            Mode = Enum.TryParse(savedMode, out ThemeMode mode) ? mode : ThemeMode.Light;
            AppColors.ApplyTheme(Mode);

            var savedAccent = AppSettingsStore.Get(PrefKeyAccent, AccentColorName.Blue.ToString());
            AppColors.ApplyAccent(AccentColor.Parse(savedAccent));
        }

        /// <summary>Gọi 1 lần lúc khởi động app (trong Program.cs, trước khi mở Form đầu tiên).</summary>
        public void ApplyStartupTheme()
        {
            AppColors.ApplyTheme(Mode);
            AppColors.ApplyAccent(AppColors.CurrentAccent);
        }

        public void Toggle() => SetMode(Mode == ThemeMode.Dark ? ThemeMode.Light : ThemeMode.Dark);

        public void SetMode(ThemeMode mode)
        {
            if (mode == Mode) return;
            Mode = mode;

            AppSettingsStore.Set(PrefKeyMode, mode.ToString());
            AppColors.ApplyTheme(mode);

            ThemeChanged?.Invoke(this, EventArgs.Empty);
        }

        public void SetAccent(AccentColor accent)
        {
            if (accent.Name == AppColors.CurrentAccent.Name) return;

            AppSettingsStore.Set(PrefKeyAccent, accent.Name.ToString());
            AppColors.ApplyAccent(accent);

            ThemeChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Tô lại đệ quy toàn bộ control con của 1 Form/Control theo AppColors
        /// hiện tại. Đây là phần thay thế cho việc FlatLaf tự vẽ lại control
        /// mới bên Java - ở đây phải chủ động duyệt cây control.
        /// Chỉ set màu cho các control loại phổ biến; UI đặc thù (card, icon
        /// riêng...) nên tự xử lý thêm trong OnThemeChanged của Form đó.
        /// </summary>
        public void ApplyTheme(Control root)
        {
            if (root == null) return;

            switch (root)
            {
                case Form form:
                    form.BackColor = AppColors.PageBg;
                    form.ForeColor = AppColors.TextPrimary;
                    break;
                case Panel panel:
                    panel.BackColor = AppColors.White;
                    panel.ForeColor = AppColors.TextPrimary;
                    break;
                case GroupBox groupBox:
                    groupBox.BackColor = AppColors.White;
                    groupBox.ForeColor = AppColors.TextPrimary;
                    break;
                case LinkLabel linkLabel:
                    linkLabel.LinkColor = AppColors.Accent;
                    linkLabel.ForeColor = AppColors.TextPrimary;
                    break;
                case Label label:
                    label.ForeColor = AppColors.TextPrimary;
                    break;
                
                case Button button:
                    ApplyButtonTheme(button);
                    break;
                case TextBox textBox:
                    textBox.BackColor = AppColors.White;
                    textBox.ForeColor = AppColors.TextPrimary;
                    break;
                case ComboBox comboBox:
                    comboBox.BackColor = AppColors.White;
                    comboBox.ForeColor = AppColors.TextPrimary;
                    break;
                case DataGridView grid:
                    ApplyGridTheme(grid);
                    break;
            }

            foreach (Control child in root.Controls)
            {
                ApplyTheme(child);
            }
        }

        /// <summary>Nút "chính" (Tag == "primary") tô theo Accent, còn lại tô theo CancelBg - tự đặt Tag ở Designer nếu cần phân biệt.</summary>
        private static void ApplyButtonTheme(Button button)
        {
            bool isPrimary = string.Equals(button.Tag as string, "primary", StringComparison.OrdinalIgnoreCase);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = isPrimary ? AppColors.Accent : AppColors.Border;

            if (isPrimary)
            {
                button.BackColor = AppColors.Accent;
                button.ForeColor = Color.White;
                button.FlatAppearance.MouseOverBackColor = AppColors.AccentHover;
            }
            else
            {
                button.BackColor = AppColors.CancelBg;
                button.ForeColor = AppColors.TextPrimary;
                button.FlatAppearance.MouseOverBackColor = AppColors.CancelHover;
            }
        }

        private static void ApplyGridTheme(DataGridView grid)
        {
            grid.BackgroundColor = AppColors.White;
            grid.GridColor = AppColors.TableGrid;
            grid.DefaultCellStyle.BackColor = AppColors.White;
            grid.DefaultCellStyle.ForeColor = AppColors.TableRowText;
            grid.DefaultCellStyle.SelectionBackColor = AppColors.AccentSelectionBg;
            grid.DefaultCellStyle.SelectionForeColor = AppColors.TextPrimary;
            grid.AlternatingRowsDefaultCellStyle.BackColor = AppColors.TableRowOdd;
            grid.ColumnHeadersDefaultCellStyle.BackColor = AppColors.TableHeaderBg;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.EnableHeadersVisualStyles = false;
        }
    }
}

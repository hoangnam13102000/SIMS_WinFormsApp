using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
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
                case UserControl userControl:
                    userControl.BackColor = AppColors.PageBg;
                    userControl.ForeColor = AppColors.TextPrimary;
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
                case DateTimePicker dateTimePicker:
                    dateTimePicker.CalendarMonthBackground = AppColors.White;
                    dateTimePicker.CalendarForeColor = AppColors.TextPrimary;
                    dateTimePicker.CalendarTitleBackColor = AppColors.TableHeaderBg;
                    dateTimePicker.CalendarTitleForeColor = Color.White;
                    dateTimePicker.CalendarTrailingForeColor = AppColors.TextMuted;
                    dateTimePicker.BackColor = AppColors.White;
                    dateTimePicker.ForeColor = AppColors.TextPrimary;
                    break;
                case NumericUpDown numericUpDown:
                    numericUpDown.BackColor = AppColors.White;
                    numericUpDown.ForeColor = AppColors.TextPrimary;
                    break;
                case RichTextBox richTextBox:
                    richTextBox.BackColor = AppColors.White;
                    richTextBox.ForeColor = AppColors.TextPrimary;
                    break;
                case ListControl listControl:
                    listControl.BackColor = AppColors.White;
                    listControl.ForeColor = AppColors.TextPrimary;
                    break;
                case DataGridView grid:
                    ApplyGridTheme(grid);
                    break;
                case Chart chart:
                    ApplyChartTheme(chart);
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
            if (button.BackColor == Color.Transparent)
                return;

            float hue = button.BackColor.GetHue();
            float saturation = button.BackColor.GetSaturation();
            bool isRed = saturation >= 0.3f && (hue <= 15f || hue >= 345f);
            bool isBlue = saturation >= 0.3f && hue >= 190f && hue <= 250f;
            bool isGreen = saturation >= 0.3f && hue >= 80f && hue <= 170f;
            button.FlatStyle = FlatStyle.Flat;

            if (isPrimary || isBlue)
            {
                button.FlatAppearance.BorderColor = AppColors.Accent;
                button.BackColor = AppColors.Accent;
                button.ForeColor = Color.White;
                button.FlatAppearance.MouseOverBackColor = AppColors.AccentHover;
            }
            else if (isRed)
            {
                button.FlatAppearance.BorderColor = AppColors.Error;
                button.BackColor = AppColors.Error;
                button.ForeColor = Color.White;
                button.FlatAppearance.MouseOverBackColor = AppColors.ErrorHover;
            }
            else if (isGreen)
            {
                button.FlatAppearance.BorderColor = AppColors.Success;
                button.BackColor = AppColors.Success;
                button.ForeColor = AppColors.CurrentMode == ThemeMode.Dark
                    ? Color.FromArgb(15, 23, 42)
                    : Color.White;
                button.FlatAppearance.MouseOverBackColor = AppColors.Success;
            }
            else
            {
                button.FlatAppearance.BorderColor = AppColors.Border;
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

        private static void ApplyChartTheme(Chart chart)
        {
            chart.BackColor = AppColors.White;
            chart.BorderlineColor = AppColors.Border;
            foreach (ChartArea area in chart.ChartAreas)
            {
                area.BackColor = AppColors.White;
                area.AxisX.LabelStyle.ForeColor = AppColors.TextMuted;
                area.AxisX.LineColor = AppColors.Border;
                area.AxisX.MajorGrid.LineColor = AppColors.TableGrid;
                area.AxisY.LabelStyle.ForeColor = AppColors.TextMuted;
                area.AxisY.TitleForeColor = AppColors.TextMuted;
                area.AxisY.LineColor = AppColors.Border;
                area.AxisY.MajorGrid.LineColor = AppColors.TableGrid;
            }

            foreach (Legend legend in chart.Legends)
            {
                legend.BackColor = AppColors.White;
                legend.ForeColor = AppColors.TextPrimary;
            }

            foreach (Title title in chart.Titles)
            {
                title.BackColor = AppColors.White;
                title.ForeColor = AppColors.TextTitle;
            }
        }
    }
}

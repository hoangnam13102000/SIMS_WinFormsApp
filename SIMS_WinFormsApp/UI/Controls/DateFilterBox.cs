using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Toast;

namespace SIMS_WinFormsApp.UI.Controls
{
    [ToolboxItem(true)]
    public sealed class DateFilterBox : UserControl
    {
        private const int DefaultWidth = 160;
        private const int FieldHeight = 36;
        private const int CornerRadius = 5;
        private const uint EmSetCueBanner = 0x1501;
        private static readonly CultureInfo VietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");

        private readonly Panel _frame;
        private readonly TextBox _input;
        private readonly IconButton _calendarButton;
        private readonly CalendarPopupPanel _calendar;
        private readonly ToolStripDropDown _popup;
        private DateTime? _value;
        private string _placeholderText = "dd/mm/yyyy";
        private bool _hasInputError;
        private bool _settingInputText;

        public DateFilterBox()
        {
            Size = new Size(DefaultWidth, FieldHeight);
            MinimumSize = new Size(100, FieldHeight);
            TabStop = true;

            _frame = new Panel
            {
                BackColor = AppColors.White,
                Dock = DockStyle.Fill,
                Padding = new Padding(8, 1, 0, 1)
            };
            _frame.Paint += Frame_Paint;

            _input = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Dock = DockStyle.Fill,
                Font = AppFonts.Input,
                ForeColor = AppColors.TextPrimary,
                MaxLength = 10,
                TabStop = true
            };
            _input.TextChanged += Input_TextChanged;
            _input.KeyDown += Input_KeyDown;
            _input.Leave += (sender, args) => CommitInput();
            _input.HandleCreated += (sender, args) => ApplyPlaceholder();

            _calendarButton = new IconButton
            {
                BackColor = AppColors.White,
                Cursor = Cursors.Hand,
                Dock = DockStyle.Right,
                FlatStyle = FlatStyle.Flat,
                IconChar = IconChar.CalendarDays,
                IconColor = AppColors.IconMuted,
                IconSize = 18,
                ImageAlign = ContentAlignment.MiddleCenter,
                Size = new Size(32, 32),
                TabStop = false,
                Text = string.Empty,
                UseVisualStyleBackColor = false
            };
            _calendarButton.FlatAppearance.BorderSize = 0;
            _calendarButton.FlatAppearance.MouseDownBackColor = AppColors.White;
            _calendarButton.FlatAppearance.MouseOverBackColor = AppColors.White;
            _calendarButton.Click += (sender, args) => ShowPopup();
            _frame.Controls.Add(_input);
            _frame.Controls.Add(_calendarButton);
            Controls.Add(_frame);

            _calendar = new CalendarPopupPanel
            {
                Size = new Size(280, 330),
                Margin = Padding.Empty
            };
            _calendar.DateSelected += date => SelectDate(date);
            _calendar.TodaySelected += date => SelectDate(date);
            _calendar.ClearRequested += (sender, args) => SelectDate(null);

            _popup = new ToolStripDropDown
            {
                AutoSize = false,
                AutoClose = true,
                BackColor = AppColors.Border,
                DropShadowEnabled = true,
                Padding = new Padding(1),
                Size = new Size(282, 332)
            };
            var host = new ToolStripControlHost(_calendar)
            {
                AutoSize = false,
                Margin = Padding.Empty,
                Padding = Padding.Empty,
                Size = new Size(280, 330)
            };
            _popup.Items.Add(host);
            ApplyPlaceholder();
        }

        public event EventHandler ValueChanged;

        public DateTime? Value
        {
            get => _value;
            set
            {
                DateTime? normalizedValue = value.HasValue
                    ? value.Value.Date
                    : (DateTime?)null;
                if (_value == normalizedValue)
                {
                    UpdateInputText();
                    return;
                }

                _value = normalizedValue;
                UpdateInputText();
                OnValueChanged(EventArgs.Empty);
            }
        }

        [DefaultValue("dd/mm/yyyy")]
        public string PlaceholderText
        {
            get => _placeholderText;
            set
            {
                string normalizedText = value ?? "dd/mm/yyyy";
                if (_placeholderText == normalizedText)
                    return;

                _placeholderText = normalizedText;
                ApplyPlaceholder();
            }
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            _input.Enabled = Enabled;
            _calendarButton.Enabled = Enabled;
            _frame.Invalidate();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if (_input != null)
                _input.Font = Font;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _popup.Dispose();
            }

            base.Dispose(disposing);
        }

        private void Frame_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle bounds = new Rectangle(0, 0, _frame.Width - 1, _frame.Height - 1);
            Color borderColor = _hasInputError ? AppColors.Error : AppColors.FieldBorder;
            using (GraphicsPath path = CreateRoundedPath(bounds, CornerRadius))
            using (var backgroundBrush = new SolidBrush(AppColors.White))
            using (var borderPen = new Pen(borderColor, 1f))
            {
                e.Graphics.FillPath(backgroundBrush, path);
                e.Graphics.DrawPath(borderPen, path);
            }
        }

        private void ShowPopup()
        {
            if (!Enabled || _popup.Visible)
                return;

            _calendar.SelectedDate = _value;
            _calendar.DisplayedMonth = _value ?? DateTime.Today;

            Rectangle workingArea = Screen.FromControl(this).WorkingArea;
            Point belowField = PointToScreen(new Point(0, Height));
            int x = Math.Max(workingArea.Left,
                Math.Min(belowField.X, workingArea.Right - _popup.Width));
            int y = belowField.Y;
            if (y + _popup.Height > workingArea.Bottom)
                y = PointToScreen(Point.Empty).Y - _popup.Height;
            y = Math.Max(workingArea.Top,
                Math.Min(y, workingArea.Bottom - _popup.Height));
            _popup.Show(new Point(x, y));
        }

        private void SelectDate(DateTime? value)
        {
            Value = value;
            _popup.Close();
        }

        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            CommitInput();
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void Input_TextChanged(object sender, EventArgs e)
        {
            if (_settingInputText || !_hasInputError)
                return;

            _hasInputError = false;
            _frame.Invalidate();
        }

        private void CommitInput()
        {
            string text = _input.Text.Trim();
            if (text.Length == 0)
            {
                _hasInputError = false;
                Value = null;
                _frame.Invalidate();
                return;
            }

            string[] formats = { "dd/MM/yyyy", "d/M/yyyy", "d/MM/yyyy", "dd/M/yyyy" };
            DateTime parsedDate;
            if (DateTime.TryParseExact(text, formats, VietnameseCulture,
                DateTimeStyles.None, out parsedDate))
            {
                _hasInputError = false;
                Value = parsedDate.Date;
                _frame.Invalidate();
                return;
            }

            _hasInputError = true;
            UpdateInputText();
            _frame.Invalidate();
            AppToast.Warning(this, "Ngày không hợp lệ", "Nhập theo dạng dd/MM/yyyy");
        }

        private void UpdateInputText()
        {
            string text = _value.HasValue
                ? _value.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
                : string.Empty;
            if (!string.Equals(_input.Text, text, StringComparison.Ordinal))
            {
                _settingInputText = true;
                try
                {
                    _input.Text = text;
                }
                finally
                {
                    _settingInputText = false;
                }
            }
            ApplyPlaceholder();
        }

        private void ApplyPlaceholder()
        {
            if (_input != null && _input.IsHandleCreated)
                SendMessage(_input.Handle, EmSetCueBanner, new IntPtr(1), _placeholderText);
        }

        private void OnValueChanged(EventArgs e)
        {
            EventHandler handler = ValueChanged;
            if (handler != null)
                handler(this, e);
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint message, IntPtr wParam, string lParam);

        private static GraphicsPath CreateRoundedPath(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(bounds.Left, bounds.Top, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Top, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.Left, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
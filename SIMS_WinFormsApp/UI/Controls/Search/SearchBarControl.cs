using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls.Search
{
    /// <summary>
    /// "View" trong mô hình MVP cho ô tìm kiếm: khung bo góc trắng, icon kính lúp bên
    /// trái, chữ mờ placeholder, và danh sách gợi ý (autocomplete) đổ xuống bên dưới.
    /// Thuần UI — không debounce, không gọi dữ liệu; toàn bộ nghiệp vụ đó nằm ở
    /// SearchPresenter. Nhờ vậy control này dùng lại được cho bất kỳ ô tìm kiếm nào.
    /// </summary>
    public sealed class SearchBarControl : UserControl, ISearchView
    {
        public event EventHandler<string> TextEdited;
        public event EventHandler<string> SuggestionPicked;

        private readonly TextBox _textBox;
        private readonly Label _placeholderLabel;
        private readonly IconPictureBox _searchIcon;
        private readonly SuggestionPopup _popup;
        private bool _isFocused;

        public SearchBarControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                      ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            BackColor = Color.Transparent;
            Height = 42;

            _searchIcon = new IconPictureBox
            {
                IconChar = IconChar.MagnifyingGlass,
                IconColor = AppColors.IconMuted,
                IconSize = 16,
                BackColor = Color.Transparent,
                Size = new Size(18, 18)
            };

            _textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = AppFonts.Input,
                ForeColor = AppColors.TextPrimary,
                BackColor = AppColors.White
            };
            _textBox.GotFocus += (_, __) => { _isFocused = true; Invalidate(); };
            _textBox.LostFocus += (_, __) => { _isFocused = false; Invalidate(); HideSuggestions(); };
            _textBox.TextChanged += (_, __) =>
            {
                _placeholderLabel.Visible = _textBox.Text.Length == 0;
                TextEdited?.Invoke(this, _textBox.Text);
            };
            _textBox.KeyDown += (_, e) =>
            {
                if (e.KeyCode == Keys.Escape) HideSuggestions();
            };

            _placeholderLabel = new Label
            {
                AutoSize = false,
                BackColor = Color.Transparent,
                ForeColor = AppColors.TextMutedAlt,
                Font = AppFonts.Input,
                TextAlign = ContentAlignment.MiddleLeft,
                Enabled = false
            };

            Controls.Add(_placeholderLabel);
            Controls.Add(_textBox);
            Controls.Add(_searchIcon);

            _popup = new SuggestionPopup();
            _popup.ItemPicked += (_, item) => SuggestionPicked?.Invoke(this, item);

            Resize += (_, __) => LayoutInner();
            ThemeManager.Instance.ThemeChanged += OnThemeChanged;
            LayoutInner();
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            _textBox.BackColor = AppColors.White;
            _textBox.ForeColor = AppColors.TextPrimary;
            _placeholderLabel.ForeColor = AppColors.TextMutedAlt;
            _searchIcon.IconColor = AppColors.IconMuted;
            Invalidate(true);
        }

        private void LayoutInner()
        {
            const int iconLeft = 14;
            const int gap = 10;
            const int rightPadding = 14;

            _searchIcon.Location = new Point(iconLeft, (Height - _searchIcon.Height) / 2);

            int textLeft = iconLeft + _searchIcon.Width + gap;
            _textBox.Location = new Point(textLeft, (Height - _textBox.Height) / 2);
            _textBox.Width = Math.Max(20, Width - textLeft - rightPadding);

            _placeholderLabel.Location = new Point(textLeft + 1, 0);
            _placeholderLabel.Size = new Size(_textBox.Width, Height);
        }

        public override string Text
        {
            get => _textBox.Text;
            set => _textBox.Text = value ?? string.Empty;
        }

        public string PlaceholderText
        {
            set
            {
                _placeholderLabel.Text = value;
                _placeholderLabel.Visible = _textBox.Text.Length == 0;
            }
        }

        public void ShowSuggestions(IList<string> suggestions)
        {
            if (!IsHandleCreated || suggestions == null || suggestions.Count == 0)
            {
                HideSuggestions();
                return;
            }
            var screenLocation = PointToScreen(new Point(0, Height + 4));
            _popup.ShowItems(suggestions, screenLocation, Width);
        }

        public void HideSuggestions() => _popup.HideIfVisible();

        /// <summary>Đưa focus + con trỏ nhập liệu vào ô tìm kiếm. BaseTable gọi lại hàm này
        /// sau khi tải lại dữ liệu để tránh việc DataGridView cướp mất focus của ô tìm kiếm
        /// (nguyên nhân khiến người dùng gõ xong debounce thì không gõ tiếp được).</summary>
        public void FocusInput() => _textBox.Focus();

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            Color borderColor = _isFocused ? AppColors.Accent : AppColors.FieldBorder;
            int borderWidth = _isFocused ? 2 : 1;

            using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Medium))
            using (var fillBrush = new SolidBrush(AppColors.White))
            using (var pen = new Pen(borderColor, borderWidth))
            {
                g.FillPath(fillBrush, path);
                g.DrawPath(pen, path);
            }

            base.OnPaint(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeManager.Instance.ThemeChanged -= OnThemeChanged;
                _popup.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Popup danh sách gợi ý dạng cửa sổ rời (kỹ thuật kinh điển của WinForms cho autocomplete
        /// tuỳ biến), dùng WS_EX_NOACTIVATE để KHÔNG cướp focus của ô nhập khi hiển thị — nhờ vậy
        /// bấm chọn 1 gợi ý sẽ không làm TextBox mất focus giữa chừng (tránh việc popup tự ẩn
        /// trước khi kịp nhận click).
        /// </summary>
        private sealed class SuggestionPopup : Form
        {
            private readonly ListBox _list;

            public event EventHandler<string> ItemPicked;

            protected override CreateParams CreateParams
            {
                get
                {
                    const int WS_EX_NOACTIVATE = 0x08000000;
                    var cp = base.CreateParams;
                    cp.ExStyle |= WS_EX_NOACTIVATE;
                    return cp;
                }
            }

            public SuggestionPopup()
            {
                FormBorderStyle = FormBorderStyle.None;
                ShowInTaskbar = false;
                StartPosition = FormStartPosition.Manual;
                TopMost = true;

                _list = new ListBox
                {
                    Dock = DockStyle.Fill,
                    BorderStyle = BorderStyle.None,
                    Font = AppFonts.Input,
                    IntegralHeight = false,
                    ForeColor = AppColors.TextPrimary
                };
                _list.Click += (_, __) =>
                {
                    if (_list.SelectedItem is string picked)
                        ItemPicked?.Invoke(this, picked);
                };
                Controls.Add(_list);
            }

            public void ShowItems(IList<string> items, Point screenLocation, int width)
            {
                _list.Items.Clear();
                _list.Items.AddRange(items.Cast<object>().ToArray());

                int height = Math.Min(220, items.Count * 26 + 6);
                Bounds = new Rectangle(screenLocation, new Size(Math.Max(60, width), Math.Max(26, height)));
                if (!Visible) Show();
            }

            public void HideIfVisible()
            {
                if (Visible) Hide();
            }
        }
    }
}
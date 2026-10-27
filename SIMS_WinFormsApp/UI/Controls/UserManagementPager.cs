using System;
using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Controls
{
    public sealed class UserManagementPager : UserControl
    {
        private const int PageSizeValue = 20;
        private readonly Label _recordCountLabel;
        private readonly Button _previousButton;
        private readonly Button _nextButton;
        private readonly Label _pageLabel;
        private int _pageIndex;
        private int _totalCount;

        public UserManagementPager()
        {
            Height = 42;
            MinimumSize = new Size(0, 42);
            Dock = DockStyle.Fill;

            var layout = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 1,
                Dock = DockStyle.Fill,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            _recordCountLabel = new Label
            {
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(100, 116, 139),
                TextAlign = ContentAlignment.MiddleLeft
            };
            var navigation = new FlowLayoutPanel
            {
                AutoSize = true,
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            _previousButton = new Button
            {
                AutoSize = true,
                Text = "Trước",
                Font = new Font("Segoe UI", 9F),
                UseVisualStyleBackColor = true,
                Margin = new Padding(4, 3, 4, 3)
            };
            _pageLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(15, 23, 42),
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(8, 8, 8, 0)
            };
            _nextButton = new Button
            {
                AutoSize = true,
                Text = "Sau",
                Font = new Font("Segoe UI", 9F),
                UseVisualStyleBackColor = true,
                Margin = new Padding(4, 3, 0, 3)
            };

            navigation.Controls.Add(_previousButton);
            navigation.Controls.Add(_pageLabel);
            navigation.Controls.Add(_nextButton);
            layout.Controls.Add(_recordCountLabel, 0, 0);
            layout.Controls.Add(navigation, 1, 0);
            Controls.Add(layout);

            _previousButton.Click += (sender, args) => ChangePage(_pageIndex - 1);
            _nextButton.Click += (sender, args) => ChangePage(_pageIndex + 1);
            UpdateDisplay();
        }

        public event EventHandler PageChanged;

        public int PageIndex
        {
            get { return _pageIndex; }
        }

        public int PageSize
        {
            get { return PageSizeValue; }
        }

        public void ResetPage()
        {
            _pageIndex = 0;
            UpdateDisplay();
        }

        public bool SetTotalCount(int totalCount)
        {
            _totalCount = Math.Max(0, totalCount);
            int lastPageIndex = Math.Max(0, (int)Math.Ceiling((double)_totalCount / PageSizeValue) - 1);
            bool pageWasClamped = _pageIndex > lastPageIndex;
            if (pageWasClamped)
                _pageIndex = lastPageIndex;

            UpdateDisplay();
            return pageWasClamped;
        }

        private void ChangePage(int pageIndex)
        {
            int pageCount = Math.Max(1, (int)Math.Ceiling((double)_totalCount / PageSizeValue));
            if (pageIndex < 0 || pageIndex >= pageCount || pageIndex == _pageIndex)
                return;

            _pageIndex = pageIndex;
            UpdateDisplay();
            EventHandler handler = PageChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        private void UpdateDisplay()
        {
            int pageCount = Math.Max(1, (int)Math.Ceiling((double)_totalCount / PageSizeValue));
            _recordCountLabel.Text = string.Format("{0} bản ghi", _totalCount);
            _pageLabel.Text = string.Format("Trang {0} / {1}", _pageIndex + 1, pageCount);
            _previousButton.Enabled = _pageIndex > 0;
            _nextButton.Enabled = _pageIndex + 1 < pageCount;
        }
    }
}

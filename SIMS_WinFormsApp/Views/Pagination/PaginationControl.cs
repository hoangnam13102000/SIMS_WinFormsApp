using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Pagination
{

    public sealed partial class PaginationControl : UserControl, IPaginationView
    {
        public event EventHandler<int> PageRequested;
        public event EventHandler<int> PageSizeRequested;
        private const int ButtonWidth = 46;
        private const int ButtonHeight = 42;
        private static readonly Font PageFont = new Font(AppFonts.BodyBold.FontFamily, 12.5f, FontStyle.Bold);
        private static readonly Font InfoFont = new Font(AppFonts.Body.FontFamily, 11.5f, FontStyle.Regular);

        private bool _suppressPageSizeEvent;

        public PaginationControl()
        {
            InitializeComponent();
            ThemeManager.Instance.ThemeChanged += OnThemeChanged;
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            BackColor = AppColors.White;
            _pageLabel.ForeColor = AppColors.TextMuted;
            Invalidate(true);
            RenderPageButtonsFromCurrentState();
        }

        private PaginationState _lastState;

        private void RenderPageButtonsFromCurrentState()
        {
            if (_lastState != null)
                Render(_lastState);
        }

        private void PageSizeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressPageSizeEvent) return;
            if (_pageSizeBox.SelectedItem is int size)
                PageSizeRequested?.Invoke(this, size);
        }

        private void CenterLeftFlow()
        {
            int x = Math.Max(0, (_centerContainer.ClientSize.Width - _leftFlow.Width) / 2);
            int y = Math.Max(0, (_centerContainer.ClientSize.Height - _leftFlow.Height) / 2);
            _leftFlow.Location = new Point(x, y);
        }

        private void CenterContainer_Resize(object sender, EventArgs e)
        {
            CenterLeftFlow();
        }

        private void LeftFlow_SizeChanged(object sender, EventArgs e)
        {
            CenterLeftFlow();
        }

        public void Render(PaginationState state)
        {
            _lastState = state;
            RenderPageSizeOptions(state);
            RenderPageButtons(state);
            _pageLabel.Text = "Trang " + state.CurrentPage + " / " + state.TotalPages;
            CenterLeftFlow();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                ThemeManager.Instance.ThemeChanged -= OnThemeChanged;
            base.Dispose(disposing);
        }

        private void RenderPageSizeOptions(PaginationState state)
        {
            _suppressPageSizeEvent = true;
            var options = state.PageSizeOptions;
            bool needsRebuild = _pageSizeBox.Items.Count != options.Length
                || !_pageSizeBox.Items.Cast<int>().SequenceEqual(options);
            if (needsRebuild)
            {
                _pageSizeBox.Items.Clear();
                _pageSizeBox.Items.AddRange(options.Cast<object>().ToArray());
            }
            _pageSizeBox.SelectedItem = state.PageSize;
            _suppressPageSizeEvent = false;
        }

        private void RenderPageButtons(PaginationState state)
        {
            // These buttons are generated from the current page state and page count.
            _pagesFlow.SuspendLayout();
            _pagesFlow.Controls.Clear();

            _pagesFlow.Controls.Add(CreateArrowButton("‹", state.CurrentPage > 1, state.CurrentPage - 1));
            foreach (var token in BuildPageWindow(state.CurrentPage, state.TotalPages))
            {
                Control pageControl = token.HasValue
                    ? (Control)CreatePageNumberButton(token.Value, token.Value == state.CurrentPage)
                    : CreateEllipsis();
                _pagesFlow.Controls.Add(pageControl);
            }
            _pagesFlow.Controls.Add(CreateArrowButton("›", state.CurrentPage < state.TotalPages, state.CurrentPage + 1));

            _pagesFlow.ResumeLayout();
        }

        private static IEnumerable<int?> BuildPageWindow(int current, int total)
        {
            const int siblings = 1;
            var pages = new SortedSet<int> { 1, total, current };
            for (int i = 1; i <= siblings; i++)
            {
                if (current - i >= 1) pages.Add(current - i);
                if (current + i <= total) pages.Add(current + i);
            }

            int? previous = null;
            foreach (var page in pages)
            {
                if (previous.HasValue && page - previous.Value > 1)
                    yield return null;
                yield return page;
                previous = page;
            }
        }

        private Button CreateArrowButton(string text, bool enabled, int targetPage)
        {
            var button = CreateBaseButton(text);
            button.Enabled = enabled;
            button.Click += (_, __) => PageRequested?.Invoke(this, targetPage);
            return button;
        }

        private Button CreatePageNumberButton(int page, bool isCurrent)
        {
            var button = CreateBaseButton(page.ToString());
            if (isCurrent)
            {
                button.BackColor = AppColors.Accent;
                button.ForeColor = AppColors.White;
                button.FlatAppearance.BorderColor = AppColors.Accent;
                button.Enabled = true;
            }
            else
            {
                button.Click += (_, __) => PageRequested?.Invoke(this, page);
            }
            return button;
        }

        private static Label CreateEllipsis()
        {
            return new Label
            {
                Text = "…",
                AutoSize = false,
                Size = new Size(ButtonWidth, ButtonHeight),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = PageFont,
                ForeColor = AppColors.TextMuted,
                Margin = new Padding(2, 3, 2, 0)
            };
        }

        private static Button CreateBaseButton(string text)
        {
            var button = new Button
            {
                Text = text,
                FlatStyle = FlatStyle.Flat,
                Font = PageFont,
                BackColor = AppColors.White,
                ForeColor = AppColors.TextPrimary,
                Size = new Size(ButtonWidth, ButtonHeight),
                Cursor = Cursors.Hand,
                Margin = new Padding(3, 3, 3, 0),
                UseVisualStyleBackColor = false
            };
            button.FlatAppearance.BorderColor = AppColors.Border;
            button.FlatAppearance.BorderSize = 1;
            button.Paint += (_, pe) => DrawRoundedButton(pe.Graphics, button);
            return button;
        }

        private static void DrawRoundedButton(Graphics g, Button button)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            var rect = new Rectangle(0, 0, button.Width - 1, button.Height - 1);
            using (var path = RoundedRect(rect, 8))
            using (var fill = new SolidBrush(button.BackColor))
            using (var border = new Pen(button.FlatAppearance.BorderColor, 1f))
            {
                g.FillPath(fill, path);
                g.DrawPath(border, path);
            }

            TextRenderer.DrawText(
                g,
                button.Text,
                button.Font,
                new Rectangle(0, 0, button.Width, button.Height),
                button.ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis);
        }

        private static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
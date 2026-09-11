using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls.Pagination
{

    public sealed class PaginationControl : UserControl, IPaginationView
    {
        public event EventHandler<int> PageRequested;
        public event EventHandler<int> PageSizeRequested;
        private const int ButtonWidth = 46;
        private const int ButtonHeight = 42;
        private static readonly Font PageFont = new Font(AppFonts.BodyBold.FontFamily, 12.5f, FontStyle.Bold);
        private static readonly Font InfoFont = new Font(AppFonts.Body.FontFamily, 11.5f, FontStyle.Regular);

        private readonly Panel _centerContainer;
        private readonly FlowLayoutPanel _leftFlow;
        private readonly FlowLayoutPanel _pagesFlow;
        private readonly Label _pageLabel;
        private readonly ComboBox _pageSizeBox;
        private bool _suppressPageSizeEvent;

        public PaginationControl()
        {
            AutoScaleMode = AutoScaleMode.None;
            Dock = DockStyle.Fill;
            BackColor = AppColors.White;

            _pagesFlow = new FlowLayoutPanel
            {
                AutoSize = true,
                WrapContents = false,
                FlowDirection = FlowDirection.LeftToRight,
                Margin = Padding.Empty
            };

            _pageLabel = new Label
            {
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = InfoFont,
                ForeColor = AppColors.TextMuted,
                Margin = new Padding(14, 12, 0, 0)
            };

            _leftFlow = new FlowLayoutPanel
            {
                AutoSize = true,
                WrapContents = false,
                FlowDirection = FlowDirection.LeftToRight,
                Margin = Padding.Empty
            };
            _leftFlow.Controls.Add(_pagesFlow);
            _leftFlow.Controls.Add(_pageLabel);

            _centerContainer = new Panel { Dock = DockStyle.Fill, Margin = Padding.Empty };
            _centerContainer.Controls.Add(_leftFlow);
            _centerContainer.Resize += (_, __) => CenterLeftFlow();
            _leftFlow.SizeChanged += (_, __) => CenterLeftFlow();

            var rightPanel = CreatePageSizePanel(out _pageSizeBox);

            Controls.Add(_centerContainer);
            Controls.Add(rightPanel);
        }

        private Panel CreatePageSizePanel(out ComboBox pageSizeBox)
        {
            var rightPanel = new Panel { Dock = DockStyle.Right, AutoSize = true, Margin = Padding.Empty, Padding = new Padding(0, 4, 0, 0) };
            var flow = new FlowLayoutPanel
            {
                AutoSize = true,
                WrapContents = false,
                FlowDirection = FlowDirection.LeftToRight,
                Margin = Padding.Empty
            };

            var pageSizeLabel = new Label
            {
                AutoSize = true,
                Text = "Hiển thị:",
                Font = InfoFont,
                ForeColor = AppColors.TextMuted,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(0, 8, 8, 0)
            };
            pageSizeBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = AppFonts.Input,
                Size = new Size(76, 32),
                Margin = new Padding(0, 2, 8, 0)
            };
            var box = pageSizeBox;
            box.SelectedIndexChanged += (_, __) =>
            {
                if (_suppressPageSizeEvent) return;
                if (box.SelectedItem is int size)
                    PageSizeRequested?.Invoke(this, size);
            };
            var unitLabel = new Label
            {
                AutoSize = true,
                Text = "dòng",
                Font = InfoFont,
                ForeColor = AppColors.TextMuted,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(0, 8, 0, 0)
            };

            flow.Controls.Add(pageSizeLabel);
            flow.Controls.Add(pageSizeBox);
            flow.Controls.Add(unitLabel);
            rightPanel.Controls.Add(flow);
            return rightPanel;
        }

        private void CenterLeftFlow()
        {
            int x = Math.Max(0, (_centerContainer.ClientSize.Width - _leftFlow.Width) / 2);
            int y = Math.Max(0, (_centerContainer.ClientSize.Height - _leftFlow.Height) / 2);
            _leftFlow.Location = new Point(x, y);
        }

        public void Render(PaginationState state)
        {
            RenderPageSizeOptions(state);
            RenderPageButtons(state);
            _pageLabel.Text = "Trang " + state.CurrentPage + " / " + state.TotalPages;
            CenterLeftFlow();
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
                button.Enabled = false;
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
                Margin = new Padding(3, 3, 3, 0)
            };
            button.FlatAppearance.BorderColor = AppColors.Border;
            button.Region = new Region(RoundedRect(new Rectangle(Point.Empty, button.Size), 10));
            return button;
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
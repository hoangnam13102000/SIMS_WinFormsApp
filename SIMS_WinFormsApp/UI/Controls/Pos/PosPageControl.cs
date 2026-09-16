using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs.Pos;
using SIMS_WinFormsApp.MVP.Presenters.Pos;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Controls.Filter;
using SIMS_WinFormsApp.UI.Controls.Search;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.UI.Controls.Pos
{
    internal sealed class PosPageControl : UserControl, IPosView
    {
        private SearchBarControl _searchBar;
        private SearchPresenter _searchPresenter;
        private IReadOnlyList<ProductCatalogItemDto> _suggestionProducts = Array.Empty<ProductCatalogItemDto>();
        private FilterComboBox _categoryCombo;
        private FilterPresenter _categoryFilterPresenter;
        private FlowLayoutPanel _productsFlow;

        private CustomerFieldControl _customerField;
        private Label _hintLabel;
        private ModernCheckBox _pointsCheckbox;
        private int _customerPoints;

        private Label _cartHeaderLabel;
        private Panel _cartListHost;

        private RoundedTextBox _promoBox;

        private Label _lblSubtotal, _lblDiscount, _lblVat, _lblPoints, _lblGrandTotal;

        private LabeledComboField _paymentField;

        private PrimaryButton _holdButton;
        private PrimaryButton _heldCartsButton;
        private PrimaryButton _checkoutButton;
        private bool _checkoutAllowedByCart;
        private bool _isBusy;

        #region IPosView - events
        public event EventHandler ViewReady;
        public event EventHandler<string> ProductSearchChanged;
        public event EventHandler<int?> CategoryChanged;
        public event EventHandler ScanBarcodeRequested;

        public event EventHandler<int> AddToCartRequested;
        public event EventHandler<int> NotifyWhenRestockRequested;
        public event EventHandler<(int ProductId, int Quantity)> CartQuantityChanged;
        public event EventHandler<int> CartLineRemoved;

        public event EventHandler<string> CustomerSearchRequested;
        public event EventHandler CustomerPickRequested;
        public event EventHandler CustomerCleared;

        public event EventHandler<string> PromoApplyRequested;
        public event EventHandler<(bool UsePoints, int PointsToUse)> PointsRedemptionChanged;
        public event EventHandler<string> PaymentMethodChanged;

        public event EventHandler HoldCartRequested;
        public event EventHandler ViewHeldCartsRequested;
        public event EventHandler CheckoutRequested;
        #endregion

        public PosPageControl(string cashierDisplayName)
        {
            Dock = DockStyle.Fill;
            BackColor = AppColors.PageBg;
            Padding = new Padding(16);

            var root = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, BackColor = Color.Transparent };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 108));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            Controls.Add(root);

            var header = new HeaderSection { Dock = DockStyle.Fill };
            header.Set("Bán hàng tại quầy", "Nhân viên: " + cashierDisplayName, IconChar.CartShopping,
                Color.White, AppColors.Accent);
            root.Controls.Add(header, 0, 0);

            var bodyRow = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1, Margin = new Padding(0, 16, 0, 0), BackColor = Color.Transparent };
            bodyRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            bodyRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 380));
            bodyRow.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.Controls.Add(bodyRow, 0, 1);

            bodyRow.Controls.Add(BuildLeftCard(), 0, 0);
            bodyRow.Controls.Add(BuildRightCard(), 1, 0);

            _searchPresenter = new SearchPresenter(_searchBar, SuggestProductNames, 300);
            _searchPresenter.SearchCommitted += (s, text) => ProductSearchChanged?.Invoke(this, text);

            Load += (s, e) => ViewReady?.Invoke(this, EventArgs.Empty);
        }

        #region Thẻ trái: tìm kiếm + lưới sản phẩm
        private Panel BuildLeftCard()
        {
            var card = CreateCard();
            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, BackColor = Color.Transparent };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            card.Controls.Add(layout);

            layout.Controls.Add(BuildSearchRow(), 0, 0);

            _productsFlow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight,
                Margin = new Padding(0, 14, 0, 0),
                BackColor = Color.Transparent
            };
            layout.Controls.Add(_productsFlow, 0, 1);

            return card;
        }

        private Panel BuildSearchRow()
        {
            var panel = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };

            var cameraButton = CreateIconButton(IconChar.Camera, AppColors.Accent,
                (s, e) => ScanBarcodeRequested?.Invoke(this, EventArgs.Empty));

            _categoryCombo = new FilterComboBox();

            _searchBar = new SearchBarControl { PlaceholderText = "Tìm sản phẩm theo tên hoặc danh mục..." };

            panel.Controls.Add(_searchBar);
            panel.Controls.Add(_categoryCombo);
            panel.Controls.Add(cameraButton);

            void Reflow()
            {
                int h = panel.Height;
                cameraButton.Location = new Point(panel.Width - cameraButton.Width, (h - cameraButton.Height) / 2);
                _categoryCombo.Size = new Size(190, 42);
                _categoryCombo.Location = new Point(cameraButton.Left - 12 - _categoryCombo.Width, (h - 42) / 2);
                _searchBar.Location = new Point(0, (h - 42) / 2);
                _searchBar.Size = new Size(Math.Max(80, _categoryCombo.Left - 12), 42);
            }
            panel.Resize += (s, e) => Reflow();
            Reflow();
            return panel;
        }
        #endregion

        #region Thẻ phải: khách hàng + giỏ hàng + khuyến mãi + tổng tiền + thanh toán
        private Panel BuildRightCard()
        {
            var card = CreateCard();
            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 8, BackColor = Color.Transparent };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 150));  // khách hàng
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 26));   // "Giỏ hàng"
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));   // danh sách giỏ hàng
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 14));   // tay cầm trang trí
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46));   // mã KM
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 150));  // tổng tiền
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));   // hình thức thanh toán
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 96));   // tạm giữ / thanh toán
            card.Controls.Add(layout);

            layout.Controls.Add(BuildCustomerSection(), 0, 0);

            _cartHeaderLabel = new Label
            {
                Text = "Giỏ hàng",
                Font = AppFonts.BodyBold,
                ForeColor = AppColors.TextTitle,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.BottomLeft,
                BackColor = Color.Transparent
            };
            layout.Controls.Add(_cartHeaderLabel, 0, 1);

            _cartListHost = new Panel { Dock = DockStyle.Fill, AutoScroll = true, BackColor = Color.Transparent };
            layout.Controls.Add(_cartListHost, 0, 2);
            BindCart(Array.Empty<CartLineDto>());

            layout.Controls.Add(BuildGrip(), 0, 3);
            layout.Controls.Add(BuildPromoRow(), 0, 4);
            layout.Controls.Add(BuildTotalsBlock(), 0, 5);

            _paymentField = new LabeledComboField { LabelText = "Hình thức thanh toán" };
            layout.Controls.Add(_paymentField, 0, 6);

            layout.Controls.Add(BuildFooterButtons(), 0, 7);

            return card;
        }

        private Panel BuildCustomerSection()
        {
            var panel = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };

            var label = new Label
            {
                Text = "Khách hàng",
                Font = AppFonts.BodyBold,
                ForeColor = AppColors.TextTitle,
                AutoSize = true,
                Location = new Point(0, 0),
                BackColor = Color.Transparent
            };
            panel.Controls.Add(label);

            _customerField = new CustomerFieldControl { Location = new Point(0, label.Bottom + 8) };
            _customerField.PickRequested += (s, e) => CustomerPickRequested?.Invoke(this, EventArgs.Empty);
            _customerField.SearchRequested += (s, e) => CustomerSearchRequested?.Invoke(this, _customerField.Text);
            panel.Controls.Add(_customerField);

            _hintLabel = new Label
            {
                Text = "Khách lẻ (không lưu thông tin)",
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                AutoSize = true,
                Location = new Point(2, _customerField.Bottom + 8),
                Cursor = Cursors.Hand,
                BackColor = Color.Transparent
            };
            _hintLabel.Click += (s, e) => CustomerCleared?.Invoke(this, EventArgs.Empty);
            panel.Controls.Add(_hintLabel);

            _pointsCheckbox = new ModernCheckBox { Location = new Point(0, _hintLabel.Bottom + 8), Visible = false };
            _pointsCheckbox.CheckedChanged += (s, e) =>
                PointsRedemptionChanged?.Invoke(this, (_pointsCheckbox.Checked, _customerPoints));
            panel.Controls.Add(_pointsCheckbox);

            panel.Resize += (s, e) => { _customerField.Width = panel.Width; };
            _customerField.Width = panel.Width;
            return panel;
        }

        private Panel BuildGrip()
        {
            var panel = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };
            panel.Paint += (s, e) =>
            {
                const int barWidth = 40, barHeight = 4;
                var rect = new Rectangle((panel.Width - barWidth) / 2, (panel.Height - barHeight) / 2, barWidth, barHeight);
                using (var path = AppRadius.GetRoundedPath(rect, 2))
                using (var brush = new SolidBrush(AppColors.Border))
                    e.Graphics.FillPath(brush, path);
            };
            return panel;
        }

        private Panel BuildPromoRow()
        {
            var panel = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };

            var label = new Label
            {
                Text = "Mã KM",
                Font = AppFonts.BodyBold,
                ForeColor = AppColors.TextTitle,
                Size = new Size(52, 40),
                TextAlign = ContentAlignment.MiddleLeft,
                Location = new Point(0, 0),
                BackColor = Color.Transparent
            };
            _promoBox = new RoundedTextBox { PlaceholderText = "Nhập mã", Height = 40, Location = new Point(56, 0) };
            var applyButton = new PrimaryButton { Text = "Áp dụng", IsPrimary = true, Size = new Size(88, 40), CornerRadius = AppRadius.Medium };
            applyButton.Click += (s, e) =>
            {
                if (!string.IsNullOrWhiteSpace(_promoBox.Text)) PromoApplyRequested?.Invoke(this, _promoBox.Text);
            };

            panel.Controls.Add(label);
            panel.Controls.Add(_promoBox);
            panel.Controls.Add(applyButton);

            void Reflow()
            {
                applyButton.Location = new Point(panel.Width - applyButton.Width, 0);
                _promoBox.Width = Math.Max(40, applyButton.Left - 8 - _promoBox.Left);
            }
            panel.Resize += (s, e) => Reflow();
            Reflow();
            return panel;
        }

        private TableLayoutPanel BuildTotalsBlock()
        {
            var table = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 5, BackColor = Color.Transparent, Margin = new Padding(0, 8, 0, 0) };
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            for (int i = 0; i < 5; i++) table.RowStyles.Add(new RowStyle(SizeType.Percent, 20));

            _lblSubtotal = AddTotalRow(table, 0, "Tạm tính", AppColors.TextPrimary, false);
            _lblDiscount = AddTotalRow(table, 1, "Giảm giá", AppColors.Success, false);
            _lblVat = AddTotalRow(table, 2, "VAT (" + (CartCalculator.VatRate * 100m) + "%)", AppColors.TextPrimary, false);
            _lblPoints = AddTotalRow(table, 3, "Trừ điểm", AppColors.Success, false);
            _lblGrandTotal = AddTotalRow(table, 4, "Tổng cộng", AppColors.Accent, true);

            return table;
        }

        private static Label AddTotalRow(TableLayoutPanel table, int row, string caption, Color valueColor, bool bold)
        {
            var captionLabel = new Label
            {
                Text = caption,
                Font = bold ? AppFonts.BodyBold : AppFonts.Body,
                ForeColor = bold ? AppColors.TextTitle : AppColors.TextSecondary,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent
            };
            var valueLabel = new Label
            {
                Text = "0 đ",
                Font = bold ? AppFonts.Subtitle : AppFonts.BodyBold,
                ForeColor = valueColor,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                BackColor = Color.Transparent
            };
            table.Controls.Add(captionLabel, 0, row);
            table.Controls.Add(valueLabel, 1, row);
            return valueLabel;
        }

        private Panel BuildFooterButtons()
        {
            var panel = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };

            _holdButton = new PrimaryButton { Text = "Tạm giữ", IsPrimary = false, CornerRadius = AppRadius.Medium };
            _heldCartsButton = new PrimaryButton { Text = "Giỏ đã giữ", IsPrimary = false, CornerRadius = AppRadius.Medium };
            _checkoutButton = new PrimaryButton { Text = "Thanh toán", IsPrimary = true, CornerRadius = AppRadius.Medium };

            _holdButton.Click += (s, e) => HoldCartRequested?.Invoke(this, EventArgs.Empty);
            _heldCartsButton.Click += (s, e) => ViewHeldCartsRequested?.Invoke(this, EventArgs.Empty);
            _checkoutButton.Click += (s, e) => CheckoutRequested?.Invoke(this, EventArgs.Empty);

            panel.Controls.Add(_holdButton);
            panel.Controls.Add(_heldCartsButton);
            panel.Controls.Add(_checkoutButton);

            void Reflow()
            {
                int halfWidth = (panel.Width - 10) / 2;
                _holdButton.Size = new Size(halfWidth, 40);
                _holdButton.Location = new Point(0, 0);
                _heldCartsButton.Size = new Size(panel.Width - halfWidth - 10, 40);
                _heldCartsButton.Location = new Point(_holdButton.Right + 10, 0);
                _checkoutButton.Size = new Size(panel.Width, 44);
                _checkoutButton.Location = new Point(0, panel.Height - 44);
            }
            panel.Resize += (s, e) => Reflow();
            Reflow();
            return panel;
        }
        #endregion

        #region Tiện ích dựng UI dùng chung
        private static Panel CreateCard()
        {
            var card = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent, Padding = new Padding(16), Margin = new Padding(0) };
            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, card.Width - 1, card.Height - 1);
                using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Large))
                using (var brush = new SolidBrush(AppColors.White))
                using (var pen = new Pen(AppColors.Border, 1f))
                {
                    e.Graphics.FillPath(brush, path);
                    e.Graphics.DrawPath(pen, path);
                }
            };
            return card;
        }

        private static Panel CreateIconButton(IconChar icon, Color background, EventHandler onClick)
        {
            var btn = new Panel { Size = new Size(42, 42), Cursor = Cursors.Hand, BackColor = Color.Transparent };
            btn.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, btn.Width - 1, btn.Height - 1);
                using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Medium))
                using (var brush = new SolidBrush(background))
                    e.Graphics.FillPath(brush, path);
            };
            var iconBox = new IconPictureBox
            {
                IconChar = icon,
                IconColor = Color.White,
                IconSize = 18,
                Size = new Size(18, 18),
                Location = new Point(12, 12),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };
            btn.Controls.Add(iconBox);
            btn.Click += onClick;
            iconBox.Click += (s, e) => onClick?.Invoke(btn, EventArgs.Empty);
            return btn;
        }
        #endregion

        #region IPosView - methods
        public void BindCategories(IReadOnlyList<CategoryOptionDto> categories)
        {
            var options = new List<FilterOption> { new FilterOption("Tất cả danh mục", null) };
            if (categories != null)
                foreach (var c in categories) options.Add(new FilterOption(c.Name, c.CategoryId.ToString()));

            _categoryFilterPresenter = new FilterPresenter(_categoryCombo, options);
            _categoryFilterPresenter.FilterChanged += (s, opt) =>
            {
                int? categoryId = opt?.Value != null && int.TryParse(opt.Value, out var id) ? id : (int?)null;
                CategoryChanged?.Invoke(this, categoryId);
            };
        }

        public void BindProducts(IReadOnlyList<ProductCatalogItemDto> products)
        {
            _suggestionProducts = products ?? Array.Empty<ProductCatalogItemDto>();
            _productsFlow.SuspendLayout();
            foreach (Control c in _productsFlow.Controls) c.Dispose();
            _productsFlow.Controls.Clear();

            if (products != null)
            {
                foreach (var product in products)
                {
                    var tile = new ProductTileControl(product);
                    tile.AddToCartClicked += (s, e) => AddToCartRequested?.Invoke(this, tile.ProductId);
                    tile.NotifyRestockClicked += (s, e) => NotifyWhenRestockRequested?.Invoke(this, tile.ProductId);
                    _productsFlow.Controls.Add(tile);
                }
            }
            _productsFlow.ResumeLayout();
        }

        private IList<string> SuggestProductNames(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return Array.Empty<string>();

            string normalizedKeyword = keyword.Trim();
            return _suggestionProducts
                .Where(product => !string.IsNullOrWhiteSpace(product.Name) &&
                    product.Name.IndexOf(normalizedKeyword, StringComparison.OrdinalIgnoreCase) >= 0)
                .Select(product => product.Name)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Take(8)
                .ToList();
        }

        public void BindCart(IReadOnlyList<CartLineDto> lines)
        {
            _cartListHost.SuspendLayout();
            foreach (Control c in _cartListHost.Controls) c.Dispose();
            _cartListHost.Controls.Clear();

            if (lines == null || lines.Count == 0)
            {
                var empty = new Label
                {
                    Dock = DockStyle.Top,
                    Height = 60,
                    Text = "Giỏ hàng đang trống - bấm \"Thêm vào giỏ\" trên sản phẩm để bắt đầu.",
                    Font = AppFonts.Body,
                    ForeColor = AppColors.TextMuted,
                    TextAlign = ContentAlignment.TopLeft,
                    Padding = new Padding(2, 4, 2, 0),
                    BackColor = Color.Transparent
                };
                _cartListHost.Controls.Add(empty);
            }
            else
            {
                foreach (var line in lines)
                {
                    var row = new CartLineRowControl(line);
                    row.QuantityChanged += (s, qty) => CartQuantityChanged?.Invoke(this, (row.ProductId, qty));
                    row.RemoveClicked += (s, e) => CartLineRemoved?.Invoke(this, row.ProductId);
                    _cartListHost.Controls.Add(row);
                }
            }
            _cartListHost.ResumeLayout();
        }

        public void SetCustomer(CustomerLookupDto customer)
        {
            _customerField.Text = customer?.DisplayText ?? string.Empty;
            _customerPoints = customer?.LoyaltyPoints ?? 0;
            _pointsCheckbox.Checked = false;
            _pointsCheckbox.Text = _customerPoints > 0
                ? "Dùng " + _customerPoints + " điểm (" + PosFormat.Vnd(_customerPoints * CartCalculator.PointRedeemRateVnd) + ")"
                : string.Empty;
            _pointsCheckbox.Visible = _customerPoints > 0;
        }

        public void ClearCustomer()
        {
            _customerField.Text = string.Empty;
            _customerPoints = 0;
            _pointsCheckbox.Checked = false;
            _pointsCheckbox.Visible = false;
        }

        public void SetTotals(CartTotalsDto totals)
        {
            if (totals == null) return;
            _lblSubtotal.Text = PosFormat.Vnd(totals.Subtotal);
            _lblDiscount.Text = PosFormat.Vnd(totals.DiscountAmount);
            _lblVat.Text = PosFormat.Vnd(totals.VatAmount);
            _lblPoints.Text = PosFormat.Vnd(totals.PointsDiscountAmount);
            _lblGrandTotal.Text = PosFormat.Vnd(totals.GrandTotal);
        }

        public void SetPromoCode(string code) => _promoBox.Text = code ?? string.Empty;

        public void SetPaymentMethods(IReadOnlyList<string> methods) => _paymentField.SetItems(methods);

        public void SetCheckoutEnabled(bool enabled)
        {
            _checkoutAllowedByCart = enabled;
            _checkoutButton.Enabled = enabled && !_isBusy;
        }

        public void SetBusy(bool isBusy)
        {
            _isBusy = isBusy;
            _checkoutButton.Enabled = _checkoutAllowedByCart && !isBusy;
            _checkoutButton.Text = isBusy ? "Đang xử lý..." : "Thanh toán";
            _holdButton.Enabled = !isBusy;
            _heldCartsButton.Enabled = !isBusy;
        }

        public CustomerLookupDto PromptPickCustomer(IReadOnlyList<CustomerLookupDto> candidates)
        {
            var list = candidates ?? Array.Empty<CustomerLookupDto>();
            return frmListPickerDialog.Pick(FindForm(), "Chọn khách hàng", list, o => ((CustomerLookupDto)o).DisplayText)
                as CustomerLookupDto;
        }

        public HeldCartDto PromptPickHeldCart(IReadOnlyList<HeldCartDto> heldCarts)
        {
            var list = heldCarts ?? Array.Empty<HeldCartDto>();
            return frmListPickerDialog.Pick(FindForm(), "Giỏ hàng đã tạm giữ", list, o => ((HeldCartDto)o).DisplayText)
                as HeldCartDto;
        }
        #endregion
    }
}
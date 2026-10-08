using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.Models.DTOs.Pos;
using SIMS_WinFormsApp.MVP.Presenters.Pos;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.UI.Controls.Pos
{
    public sealed partial class PosPageControl : UserControl, IPosView
    {
        private readonly bool _designTime;
        private int _customerPoints;
        private bool _checkoutAllowedByCart;
        private bool _isBusy;
        private bool _bindingCart;

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
        public event EventHandler HoldCartRequested;
        public event EventHandler ViewHeldCartsRequested;
        public event EventHandler CheckoutRequested;

        public PosPageControl() : this(string.Empty)
        {
        }

        public PosPageControl(string cashierDisplayName)
        {
            InitializeComponent();
            _designTime = LicenseManager.UsageMode == LicenseUsageMode.Designtime;
            _cashierNameLabel.Text = string.IsNullOrWhiteSpace(cashierDisplayName)
                ? "Nhân viên:"
                : "Nhân viên: " + cashierDisplayName;

            _productSearch.TextChanged += (sender, args) =>
                ProductSearchChanged?.Invoke(this, _productSearch.Text);
            _productCategory.SelectedIndexChanged += OnCategorySelectedIndexChanged;
            _scanBarcodeButton.Click += (sender, args) =>
                ScanBarcodeRequested?.Invoke(this, EventArgs.Empty);
            _customerSearchButton.Click += (sender, args) => RequestCustomerSearch();
            _customerTextBox.KeyDown += OnCustomerTextBoxKeyDown;
            _pickCustomerButton.Click += (sender, args) =>
                CustomerPickRequested?.Invoke(this, EventArgs.Empty);
            _clearCustomerButton.Click += (sender, args) =>
                CustomerCleared?.Invoke(this, EventArgs.Empty);
            _usePointsCheckBox.CheckedChanged += (sender, args) =>
                PointsRedemptionChanged?.Invoke(this, (_usePointsCheckBox.Checked, _customerPoints));
            _cartGrid.CellValidating += OnCartCellValidating;
            _cartGrid.CellEndEdit += OnCartCellEndEdit;
            _cartGrid.CellContentClick += OnCartCellContentClick;
            _applyPromoButton.Click += (sender, args) =>
            {
                if (!string.IsNullOrWhiteSpace(_promoTextBox.Text))
                    PromoApplyRequested?.Invoke(this, _promoTextBox.Text.Trim());
            };
            _holdButton.Click += (sender, args) => HoldCartRequested?.Invoke(this, EventArgs.Empty);
            _heldCartsButton.Click += (sender, args) => ViewHeldCartsRequested?.Invoke(this, EventArgs.Empty);
            _checkoutButton.Click += (sender, args) => CheckoutRequested?.Invoke(this, EventArgs.Empty);
            Load += (sender, args) =>
            {
                if (!_designTime)
                {
                    BeginInvoke((Action)SetInitialCartSplitterDistance);
                    ViewReady?.Invoke(this, EventArgs.Empty);
                }
            };
            _cartCheckoutSplit.SplitterMoving += OnCartCheckoutSplitterMoving;
        }

        public void BindCategories(IReadOnlyList<CategoryOptionDto> categories)
        {
            _productCategory.SelectedIndexChanged -= OnCategorySelectedIndexChanged;
            _productCategory.Items.Clear();
            _productCategory.Items.Add(new CategoryChoice("Tất cả danh mục", null));
            if (categories != null)
            {
                foreach (var category in categories)
                    _productCategory.Items.Add(new CategoryChoice(category.Name, category.CategoryId));
            }
            _productCategory.SelectedIndex = 0;
            _productCategory.SelectedIndexChanged += OnCategorySelectedIndexChanged;
        }

        public void BindProducts(IReadOnlyList<ProductCatalogItemDto> products)
        {
            while (_productCardsPanel.Controls.Count > 0)
            {
                Control card = _productCardsPanel.Controls[0];
                _productCardsPanel.Controls.RemoveAt(0);
                card.Dispose();
            }

            if (products == null || products.Count == 0)
            {
                var emptyState = new Label
                {
                    AutoSize = false,
                    Dock = DockStyle.Top,
                    Height = 72,
                    Text = "Không tìm thấy sản phẩm phù hợp.",
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.FromArgb(100, 116, 139),
                    Font = new Font("Segoe UI", 10F)
                };
                _productCardsPanel.Controls.Add(emptyState);
                return;
            }

            foreach (ProductCatalogItemDto product in products)
            {
                var tile = new ProductTileControl(product);
                tile.AddToCartClicked += (sender, args) =>
                    AddToCartRequested?.Invoke(this, tile.ProductId);
                tile.NotifyRestockClicked += (sender, args) =>
                    NotifyWhenRestockRequested?.Invoke(this, tile.ProductId);
                _productCardsPanel.Controls.Add(tile);
            }
        }

        public void BindCart(IReadOnlyList<CartLineDto> lines)
        {
            _bindingCart = true;
            try
            {
                _cartGrid.Rows.Clear();
                if (lines == null) return;

                foreach (var line in lines)
                {
                    int rowIndex = _cartGrid.Rows.Add(
                        line.ProductName ?? string.Empty,
                        PosFormat.Vnd(line.UnitPrice),
                        line.Quantity,
                        PosFormat.Vnd(line.LineTotal),
                        "Xóa");
                    _cartGrid.Rows[rowIndex].Tag = line;
                }
            }
            finally
            {
                _bindingCart = false;
            }
        }

        public void SetCustomer(CustomerLookupDto customer)
        {
            _customerTextBox.Text = customer?.DisplayText ?? string.Empty;
            _customerPoints = customer?.LoyaltyPoints ?? 0;
            _usePointsCheckBox.Checked = false;
            _usePointsCheckBox.Text = _customerPoints > 0
                ? "Dùng " + _customerPoints + " điểm (" +
                  PosFormat.Vnd(_customerPoints * CartCalculator.PointRedeemRateVnd) + ")"
                : "Dùng điểm thưởng";
            _usePointsCheckBox.Visible = _customerPoints > 0;
        }

        public void ClearCustomer()
        {
            _customerTextBox.Clear();
            _customerPoints = 0;
            _usePointsCheckBox.Checked = false;
            _usePointsCheckBox.Visible = false;
        }

        public void SetTotals(CartTotalsDto totals)
        {
            if (totals == null) return;
            _subtotalValue.Text = PosFormat.Vnd(totals.Subtotal);
            _discountValue.Text = PosFormat.Vnd(totals.DiscountAmount);
            _vatValue.Text = PosFormat.Vnd(totals.VatAmount);
            _pointsValue.Text = PosFormat.Vnd(totals.PointsDiscountAmount);
            _grandTotalValue.Text = PosFormat.Vnd(totals.GrandTotal);
        }

        public void SetPromoCode(string code) => _promoTextBox.Text = code ?? string.Empty;

        public void SetPaymentMethods(IReadOnlyList<string> methods)
        {
            _paymentMethod.Items.Clear();
            if (methods == null) return;
            foreach (var method in methods)
                _paymentMethod.Items.Add(method);
            if (_paymentMethod.Items.Count > 0)
                _paymentMethod.SelectedIndex = 0;
        }

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
            return frmListPickerDialog.Pick(
                FindForm(),
                "Chọn khách hàng",
                list,
                item => ((CustomerLookupDto)item).DisplayText) as CustomerLookupDto;
        }

        public HeldCartDto PromptPickHeldCart(IReadOnlyList<HeldCartDto> heldCarts)
        {
            var list = heldCarts ?? Array.Empty<HeldCartDto>();
            return frmListPickerDialog.Pick(
                FindForm(),
                "Giỏ hàng đã tạm giữ",
                list,
                item => ((HeldCartDto)item).DisplayText) as HeldCartDto;
        }

        private void OnCategorySelectedIndexChanged(object sender, EventArgs e)
        {
            if (_productCategory.SelectedItem is CategoryChoice choice)
                CategoryChanged?.Invoke(this, choice.CategoryId);
        }

        private void SetInitialCartSplitterDistance()
        {
            if (IsDisposed || _cartCheckoutSplit.IsDisposed || _cartCheckoutSplit.ClientSize.Height <= 0)
                return;

            int availableHeight = _cartCheckoutSplit.ClientSize.Height - _cartCheckoutSplit.SplitterWidth;
            int maxDistance = availableHeight - _cartCheckoutSplit.Panel2MinSize;
            if (maxDistance < _cartCheckoutSplit.Panel1MinSize)
                return;

            _cartCheckoutSplit.SplitterDistance = Math.Max(
                _cartCheckoutSplit.Panel1MinSize,
                Math.Min(availableHeight * 2 / 5, maxDistance));
        }

        private void OnCartCheckoutSplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            int availableHeight = _cartCheckoutSplit.ClientSize.Height - _cartCheckoutSplit.SplitterWidth;
            int maxDistance = availableHeight - _cartCheckoutSplit.Panel2MinSize;
            if (maxDistance < _cartCheckoutSplit.Panel1MinSize ||
                e.SplitY < _cartCheckoutSplit.Panel1MinSize || e.SplitY > maxDistance)
                e.Cancel = true;
        }

        private void OnCartCellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (_bindingCart || e.RowIndex < 0 || e.ColumnIndex != _cartQuantity.Index)
                return;
            if (int.TryParse(Convert.ToString(e.FormattedValue), out int quantity) && quantity > 0)
            {
                _cartGrid.Rows[e.RowIndex].ErrorText = string.Empty;
                return;
            }

            e.Cancel = true;
            _cartGrid.Rows[e.RowIndex].ErrorText = "Số lượng phải là số nguyên lớn hơn 0.";
        }

        private void OnCartCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (_bindingCart || e.RowIndex < 0 || e.ColumnIndex != _cartQuantity.Index)
                return;
            _cartGrid.Rows[e.RowIndex].ErrorText = string.Empty;
            if (_cartGrid.Rows[e.RowIndex].Tag is CartLineDto line &&
                int.TryParse(Convert.ToString(_cartGrid.Rows[e.RowIndex].Cells[_cartQuantity.Index].Value), out int quantity) &&
                quantity > 0 && quantity != line.Quantity)
            {
                CartQuantityChanged?.Invoke(this, (line.ProductId, quantity));
            }
        }

        private void OnCartCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != _cartRemove.Index)
                return;
            if (_cartGrid.Rows[e.RowIndex].Tag is CartLineDto line)
                CartLineRemoved?.Invoke(this, line.ProductId);
        }

        private void OnCustomerTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            RequestCustomerSearch();
        }

        private void RequestCustomerSearch()
        {
            string search = _customerTextBox.Text?.Trim();
            if (!string.IsNullOrWhiteSpace(search))
                CustomerSearchRequested?.Invoke(this, search);
        }

        private sealed class CategoryChoice
        {
            public string Name { get; }
            public int? CategoryId { get; }

            public CategoryChoice(string name, int? categoryId)
            {
                Name = name;
                CategoryId = categoryId;
            }

            public override string ToString() => Name;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs.Pos;
using SIMS_WinFormsApp.MVP.Presenters.Pos;
using SIMS_WinFormsApp.UI.Controls;
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
        private bool _bindingCategories;

        public event EventHandler ViewReady;
        public event EventHandler<string> ProductSearchChanged;
        public event EventHandler<int?> CategoryChanged;
        public event EventHandler ScanBarcodeRequested;
        public event EventHandler CustomerBarcodeScanRequested;
        public event EventHandler<int> AddToCartRequested;
        public event EventHandler<int> NotifyWhenRestockRequested;
        public event EventHandler<(int ProductId, int Quantity)> CartQuantityChanged;
        public event EventHandler<int> CartLineRemoved;
        public event EventHandler<string> CustomerSearchRequested;
        public event EventHandler CustomerCleared;
        public event EventHandler<(bool UsePoints, int PointsToUse)> PointsRedemptionChanged;
        public event EventHandler HoldCartRequested;
        public event EventHandler ViewHeldCartsRequested;
        public event EventHandler CheckoutRequested;

        public string SelectedPaymentMethod
        {
            get
            {
                string selected = _paymentMethod.SelectedItem as string;
                if (selected == "Tiền mặt") return "CASH";
                if (selected == "Chuyển khoản") return "BANK_TRANSFER";
                if (selected == "Thẻ ngân hàng") return "CARD";
                if (selected == "Ví điện tử") return "PAYPAL";
                return null;
            }
        }

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
        }

        private void _usePointsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_usePointsCheckBox.Checked && _pointsToRedeemInput.Value == 0)
                _pointsToRedeemInput.Value = _pointsToRedeemInput.Maximum;

            _pointsToRedeemInput.Enabled =
                _usePointsCheckBox.Checked && _pointsToRedeemInput.Maximum > 0;
            PointsRedemptionChanged?.Invoke(
                this, (_usePointsCheckBox.Checked, decimal.ToInt32(_pointsToRedeemInput.Value)));
        }

        public void BindCategories(IReadOnlyList<CategoryOptionDto> categories)
        {
            _bindingCategories = true;
            try
            {
                _productCategory.Items.Clear();
                _productCategory.Items.Add(new CategoryChoice("Tất cả danh mục", null));
                if (categories != null)
                {
                    foreach (var category in categories)
                        _productCategory.Items.Add(new CategoryChoice(category.Name, category.CategoryId));
                }
                _productCategory.SelectedIndex = 0;
            }
            finally
            {
                _bindingCategories = false;
            }
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
                // Empty-state UI exists only when the current product query has no results.
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

                DataGridViewRow[] cartRows = lines
                    .Where(line => line != null)
                    .Select(line =>
                    {
                        var row = new DataGridViewRow();
                        row.CreateCells(
                            _cartGrid,
                            line.ProductName ?? string.Empty,
                            PosFormat.Vnd(line.UnitPrice),
                            line.Quantity,
                            PosFormat.Vnd(line.LineTotal),
                            IconBitmapCache.Get(
                                IconChar.TrashCan,
                                AppColors.Error,
                                new Size(18, 18)));
                        row.Tag = line;
                        return row;
                    })
                    .ToArray();
                _cartGrid.Rows.AddRange(cartRows);
            }
            finally
            {
                _bindingCart = false;
            }
        }

        public void SetCustomer(CustomerLookupDto customer)
        {
            _customerTextBox.Text = customer?.DisplayText ?? string.Empty;
            _customerPoints = Math.Max(0, customer?.LoyaltyPoints ?? 0);
            _usePointsCheckBox.Checked = false;
            _pointsToRedeemInput.Value = 0;
            _pointsToRedeemInput.Maximum = _customerPoints;
            _usePointsCheckBox.Enabled = _customerPoints > 0;
            _pointsToRedeemInput.Enabled = false;
            _pointsBalanceLabel.Text = "(có " + _customerPoints.ToString("N0") + ")";
            _pointsRedemptionPanel.Visible = customer != null;
        }

        public void ClearCustomer()
        {
            _customerTextBox.Clear();
            _customerPoints = 0;
            _usePointsCheckBox.Checked = false;
            _pointsToRedeemInput.Value = 0;
            _pointsToRedeemInput.Maximum = 0;
            _pointsToRedeemInput.Enabled = false;
            _usePointsCheckBox.Enabled = false;
            _pointsBalanceLabel.Text = "(có 0)";
            _pointsRedemptionPanel.Visible = false;
        }

        public void SetPointRedemptionLimit(int points)
        {
            int limit = Math.Min(_customerPoints, Math.Max(0, points));
            _pointsToRedeemInput.Maximum = limit;
            _usePointsCheckBox.Enabled = limit > 0;
            _pointsToRedeemInput.Enabled = _usePointsCheckBox.Checked && limit > 0;
            _pointsBalanceLabel.Text = "(có " + _customerPoints.ToString("N0") + ")";
        }

        public void SetTotals(CartTotalsDto totals)
        {
            if (totals == null) return;
            _subtotalValue.Text = PosFormat.Vnd(totals.Subtotal);
            _vatValue.Text = PosFormat.Vnd(totals.VatAmount);
            _pointsValue.Text = PosFormat.Vnd(totals.PointsDiscountAmount);
            _grandTotalValue.Text = PosFormat.Vnd(totals.GrandTotal);
        }

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

        public void SetHeldCartCount(int count)
        {
            _heldCartsButton.Text = "Giỏ đã giữ (" + Math.Max(0, count) + ")";
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

        private void _productCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_bindingCategories)
                return;

            if (_productCategory.SelectedItem is CategoryChoice choice)
                CategoryChanged?.Invoke(this, choice.CategoryId);
        }

        private void _productSearch_TextChanged(object sender, EventArgs e)
        {
            ProductSearchChanged?.Invoke(this, _productSearch.Text);
        }

        private void _scanBarcodeButton_Click(object sender, EventArgs e)
        {
            ScanBarcodeRequested?.Invoke(this, EventArgs.Empty);
        }

        private void _customerSearchButton_Click(object sender, EventArgs e)
        {
            RequestCustomerSearch();
        }

        private void _customerScanButton_Click(object sender, EventArgs e)
        {
            CustomerBarcodeScanRequested?.Invoke(this, EventArgs.Empty);
        }

        private void _clearCustomerButton_Click(object sender, EventArgs e)
        {
            CustomerCleared?.Invoke(this, EventArgs.Empty);
        }

        private void _pointsToRedeemInput_ValueChanged(object sender, EventArgs e)
        {
            PointsRedemptionChanged?.Invoke(
                this, (_usePointsCheckBox.Checked, decimal.ToInt32(_pointsToRedeemInput.Value)));
        }

        private void _holdButton_Click(object sender, EventArgs e)
        {
            HoldCartRequested?.Invoke(this, EventArgs.Empty);
        }

        private void _heldCartsButton_Click(object sender, EventArgs e)
        {
            ViewHeldCartsRequested?.Invoke(this, EventArgs.Empty);
        }

        private void _checkoutButton_Click(object sender, EventArgs e)
        {
            CheckoutRequested?.Invoke(this, EventArgs.Empty);
        }

        private void PosPageControl_Load(object sender, EventArgs e)
        {
            if (!_designTime)
            {
                BeginInvoke((Action)SetInitialCartSplitterDistance);
                ViewReady?.Invoke(this, EventArgs.Empty);
            }
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

        private void _cartCheckoutSplit_SplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            int availableHeight = _cartCheckoutSplit.ClientSize.Height - _cartCheckoutSplit.SplitterWidth;
            int maxDistance = availableHeight - _cartCheckoutSplit.Panel2MinSize;
            if (maxDistance < _cartCheckoutSplit.Panel1MinSize ||
                e.SplitY < _cartCheckoutSplit.Panel1MinSize || e.SplitY > maxDistance)
                e.Cancel = true;
        }

        private void _cartGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (_bindingCart || e.RowIndex < 0 || e.ColumnIndex != CartQuantity.Index)
                return;
            if (int.TryParse(NumberInputFormatter.DigitsOnly(Convert.ToString(e.FormattedValue)), out int quantity) &&
                quantity > 0)
            {
                _cartGrid.Rows[e.RowIndex].ErrorText = string.Empty;
                return;
            }

            e.Cancel = true;
            _cartGrid.Rows[e.RowIndex].ErrorText = "Số lượng phải là số nguyên lớn hơn 0.";
        }

        private void _cartGrid_EditingControlShowing(
            object sender,
            DataGridViewEditingControlShowingEventArgs e)
        {
            if (_cartGrid.CurrentCell?.ColumnIndex == CartQuantity.Index && e.Control is TextBox textBox)
            {
                NumberInputFormatter.Attach(textBox);
                NumberInputFormatter.SetMaximumDigits(textBox, 10);
            }
        }

        private void _cartGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (_bindingCart || e.RowIndex < 0 || e.ColumnIndex != CartQuantity.Index)
                return;
            _cartGrid.Rows[e.RowIndex].ErrorText = string.Empty;
            if (_cartGrid.Rows[e.RowIndex].Tag is CartLineDto line &&
                int.TryParse(NumberInputFormatter.DigitsOnly(
                    Convert.ToString(_cartGrid.Rows[e.RowIndex].Cells[CartQuantity.Index].Value)), out int quantity) &&
                quantity > 0 && quantity != line.Quantity)
            {
                CartQuantityChanged?.Invoke(this, (line.ProductId, quantity));
            }
        }

        private void _cartGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != CartRemove.Index)
                return;
            if (_cartGrid.Rows[e.RowIndex].Tag is CartLineDto line)
                CartLineRemoved?.Invoke(this, line.ProductId);
        }

        private void _customerTextBox_KeyDown(object sender, KeyEventArgs e)
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

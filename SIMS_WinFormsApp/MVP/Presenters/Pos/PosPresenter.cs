using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using SIMS_WinFormsApp.Models.DTOs.Pos;
using SIMS_WinFormsApp.Models.DTOs.Sales;
using SIMS_WinFormsApp.Services.Interfaces.Pos;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.UI.Controls.Barcode;
using SIMS_WinFormsApp.Views.Toast;
using SIMS_WinFormsApp.Views.Interfaces;
using SIMS_WinFormsApp.Views.Sales.InvoiceManagement;

namespace SIMS_WinFormsApp.MVP.Presenters.Pos
{
    public sealed class PosPresenter : IDisposable
    {
        private static readonly string[] PaymentMethods = { "Tiền mặt", "Chuyển khoản", "Thẻ ngân hàng", "Ví điện tử" };

        private readonly IPosView _view;
        private readonly IProductCatalogService _catalogService;
        private readonly ICustomerLookupService _customerService;
        private readonly IInvoiceService _invoiceService;
        private readonly IPromotionService _promotionService;
        private readonly IBarcodeScannerLauncher _scannerLauncher;
        private readonly HeldCartStore _heldCartStore;
        private readonly Func<IWin32Window> _ownerWindowProvider;
        private readonly IUserSession _userSession;
        private readonly Action _invoiceCreated;
        private readonly Action<string> _invoiceViewed;

        private readonly PosCart _cart = new PosCart();
        private readonly CartCalculator _calculator = new CartCalculator();

        private int? _selectedCategoryId;
        private string _searchKeyword = string.Empty;
        private CustomerLookupDto _selectedCustomer;
        private decimal _promotionDiscount;
        private int? _promotionId;
        private string _promotionCode;
        private bool _usePoints;
        private int _pointsToUse;
        private string _lastScannedBarcode;
        private DateTime _lastBarcodeScanUtc;

        public PosPresenter(
            IPosView view,
            IProductCatalogService catalogService,
            ICustomerLookupService customerService,
            IInvoiceService invoiceService,
            IPromotionService promotionService,
            IBarcodeScannerLauncher scannerLauncher,
            HeldCartStore heldCartStore,
            Func<IWin32Window> ownerWindowProvider,
            IUserSession userSession,
            Action invoiceCreated = null,
            Action<string> invoiceViewed = null)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _invoiceService = invoiceService ?? throw new ArgumentNullException(nameof(invoiceService));
            _promotionService = promotionService ?? throw new ArgumentNullException(nameof(promotionService));
            _scannerLauncher = scannerLauncher ?? throw new ArgumentNullException(nameof(scannerLauncher));
            _heldCartStore = heldCartStore ?? throw new ArgumentNullException(nameof(heldCartStore));
            _ownerWindowProvider = ownerWindowProvider ?? throw new ArgumentNullException(nameof(ownerWindowProvider));
            _userSession = userSession ?? throw new ArgumentNullException(nameof(userSession));
            _invoiceCreated = invoiceCreated;
            _invoiceViewed = invoiceViewed;

            _view.ViewReady += OnViewReady;
            _view.ProductSearchChanged += OnProductSearchChanged;
            _view.CategoryChanged += OnCategoryChanged;
            _view.ScanBarcodeRequested += OnScanBarcodeRequested;

            _view.AddToCartRequested += OnAddToCartRequested;
            _view.NotifyWhenRestockRequested += OnNotifyWhenRestockRequested;
            _view.CartQuantityChanged += OnCartQuantityChanged;
            _view.CartLineRemoved += OnCartLineRemoved;

            _view.CustomerSearchRequested += OnCustomerSearchRequested;
            _view.CustomerCleared += OnCustomerCleared;

            _view.PromoApplyRequested += OnPromoApplyRequested;
            _view.PointsRedemptionChanged += OnPointsRedemptionChanged;

            _view.HoldCartRequested += OnHoldCartRequested;
            _view.ViewHeldCartsRequested += OnViewHeldCartsRequested;
            _view.CheckoutRequested += OnCheckoutRequested;
        }

        #region Khởi tạo trang
        private void OnViewReady(object sender, EventArgs e)
        {
            _view.BindCategories(_catalogService.GetCategories());
            _view.BindPromotions(_promotionService.GetAvailablePromotions());
            _view.SetPaymentMethods(PaymentMethods);
            ReloadProducts();
            RefreshCart();
        }

        private void ReloadProducts()
        {
            _view.BindProducts(_catalogService.Search(_searchKeyword, _selectedCategoryId));
        }
        #endregion

        #region Tìm kiếm / lọc / quét mã vạch
        private void OnProductSearchChanged(object sender, string keyword)
        {
            _searchKeyword = keyword ?? string.Empty;
            ReloadProducts();
        }

        private void OnCategoryChanged(object sender, int? categoryId)
        {
            _selectedCategoryId = categoryId;
            ReloadProducts();
        }

        private void OnScanBarcodeRequested(object sender, EventArgs e)
        {
            IWin32Window owner = _ownerWindowProvider();
            string code = _scannerLauncher.Scan(owner);
            if (string.IsNullOrEmpty(code)) return;

            code = code.Trim();
            if (string.Equals(code, _lastScannedBarcode, StringComparison.OrdinalIgnoreCase) &&
                (DateTime.UtcNow - _lastBarcodeScanUtc).TotalMilliseconds < 1500)
                return;

            _lastScannedBarcode = code;
            _lastBarcodeScanUtc = DateTime.UtcNow;

            var product = _catalogService.FindByBarcode(code);
            if (product == null)
            {
                AppToast.Warning(owner as Control, "Không tìm thấy sản phẩm",
                    "Không có sản phẩm nào khớp với mã vừa quét: " + code);
                return;
            }
            AddProductToCart(product, owner);
        }
        #endregion

        #region Giỏ hàng
        private void OnAddToCartRequested(object sender, int productId)
        {
            var product = _catalogService.GetById(productId);
            if (product != null) AddProductToCart(product, _ownerWindowProvider());
        }

        private void AddProductToCart(ProductCatalogItemDto product, IWin32Window owner)
        {
            if (product.IsOutOfStock)
            {
                AppToast.Warning(owner as Control, "Hết hàng", "\"" + product.Name + "\" hiện đã hết hàng.");
                return;
            }
            _cart.AddOrIncrement(product.ProductId, product.Name, product.Price);
            RefreshCart();
        }

        private void OnNotifyWhenRestockRequested(object sender, int productId)
        {
            var product = _catalogService.GetById(productId);
            string name = product?.Name ?? "sản phẩm này";
            AppToast.Info(_ownerWindowProvider() as Control, "Đã ghi nhận",
                "Sẽ thông báo cho bạn khi \"" + name + "\" có hàng trở lại.");
        }

        private void OnCartQuantityChanged(object sender, (int ProductId, int Quantity) e)
        {
            _cart.SetQuantity(e.ProductId, e.Quantity);
            RefreshCart();
        }

        private void OnCartLineRemoved(object sender, int productId)
        {
            _cart.Remove(productId);
            RefreshCart();
        }

        private void RefreshCart()
        {
            _view.BindCart(_cart.Lines);
            RefreshTotals();
        }

        private void RefreshTotals()
        {
            var totals = _calculator.Calculate(_cart.Subtotal, _promotionDiscount, _usePoints ? _pointsToUse : 0);
            _view.SetTotals(totals);
            _view.SetCheckoutEnabled(!_cart.IsEmpty);
        }
        #endregion

        #region Khách hàng
        private void OnCustomerSearchRequested(object sender, string keyword)
        {
            var found = _customerService.FindByPhoneOrCode(keyword);
            if (found == null)
            {
                AppToast.Warning(_ownerWindowProvider() as Control, "Không tìm thấy khách hàng",
                    "Không có khách hàng nào khớp với \"" + keyword + "\".");
                return;
            }
            SelectCustomer(found);
        }

        private void SelectCustomer(CustomerLookupDto customer)
        {
            _selectedCustomer = customer;
            _usePoints = false;
            _pointsToUse = 0;
            _view.SetCustomer(customer);
            RefreshTotals();
        }

        private void OnCustomerCleared(object sender, EventArgs e)
        {
            _selectedCustomer = null;
            _usePoints = false;
            _pointsToUse = 0;
            _view.ClearCustomer();
            RefreshTotals();
        }
        #endregion

        #region Khuyến mãi & điểm thưởng
        private void OnPromoApplyRequested(object sender, string code)
        {
            var result = _promotionService.Apply(code, _cart.Subtotal);
            IWin32Window owner = _ownerWindowProvider();

            if (!result.IsSuccess)
            {
                _promotionDiscount = 0m;
                AppToast.Error(owner as Control, "Không áp dụng được", result.Message);
            }
            else
            {
                _promotionDiscount = result.DiscountAmount;
                _promotionId = result.PromotionId;
                _promotionCode = result.PromotionCode ?? code.Trim().ToUpperInvariant();
                AppToast.Success(owner as Control, "Áp dụng thành công", result.Message);
            }
            RefreshTotals();
        }

        private void OnPointsRedemptionChanged(object sender, (bool UsePoints, int PointsToUse) e)
        {
            _usePoints = e.UsePoints;
            _pointsToUse = e.UsePoints ? Math.Max(0, e.PointsToUse) : 0;
            RefreshTotals();
        }
        #endregion

        #region Tạm giữ / Giỏ đã giữ / Thanh toán
        private void OnHoldCartRequested(object sender, EventArgs e)
        {
            IWin32Window owner = _ownerWindowProvider();
            if (_cart.IsEmpty)
            {
                AppToast.Warning(owner as Control, "Giỏ hàng đang trống", "Chưa có sản phẩm nào để tạm giữ.");
                return;
            }

            _heldCartStore.Add(_selectedCustomer?.DisplayText, _cart.Lines);
            ResetForNewSale();
            AppToast.Success(owner as Control, "Đã tạm giữ giỏ hàng.");
        }

        private void OnViewHeldCartsRequested(object sender, EventArgs e)
        {
            IWin32Window owner = _ownerWindowProvider();
            var all = _heldCartStore.GetAll();
            if (all.Count == 0)
            {
                AppToast.Info(owner as Control, "Chưa có giỏ hàng nào được tạm giữ.");
                return;
            }

            var picked = _view.PromptPickHeldCart(all);
            if (picked == null) return;

            _cart.Restore(picked.Lines);
            _heldCartStore.Remove(picked.HeldCartId);
            RefreshCart();
            AppToast.Success(owner as Control, "Đã mở lại giỏ hàng đã giữ.");
        }

        private void OnCheckoutRequested(object sender, EventArgs e)
        {
            IWin32Window owner = _ownerWindowProvider();
            if (_cart.IsEmpty)
            {
                AppToast.Warning(owner as Control, "Giỏ hàng đang trống", "Chưa có sản phẩm nào để thanh toán.");
                return;
            }

            if (!_userSession.IsLoggedIn)
            {
                AppToast.Error(owner as Control, "Không thể thanh toán", "Phiên đăng nhập đã hết hạn.");
                return;
            }

            InvoiceDetails receiptInvoice;
            string invoiceCode;
            _view.SetBusy(true);
            try
            {
                var totals = _calculator.Calculate(_cart.Subtotal, _promotionDiscount, _usePoints ? _pointsToUse : 0);
                string paymentMethod = _view.SelectedPaymentMethod;
                receiptInvoice = CreateReceiptInvoice(paymentMethod, totals);
                invoiceCode = _invoiceService.CreateInvoice(
                    _userSession.CurrentUser.UserId,
                    _selectedCustomer == null ? (int?)null : _selectedCustomer.CustomerId,
                    paymentMethod,
                    totals,
                    _promotionId,
                    _promotionCode,
                    _cart.Lines);
                receiptInvoice.InvoiceCode = invoiceCode;
            }
            catch (SqlException exception)
            {
                _view.SetBusy(false);
                AppToast.Error(owner as Control, "Không thể lưu hóa đơn", exception.Message);
                return;
            }
            catch (InvalidOperationException exception)
            {
                _view.SetBusy(false);
                AppToast.Warning(owner as Control, "Chưa thể thanh toán", exception.Message);
                return;
            }
            catch (ArgumentException exception)
            {
                _view.SetBusy(false);
                AppToast.Warning(owner as Control, "Thông tin thanh toán không hợp lệ", exception.Message);
                return;
            }

            ResetForNewSale();
            _view.SetBusy(false);
            _invoiceCreated?.Invoke();
            DialogResult printChoice = MessageBox.Show(
                owner,
                "Hóa đơn " + invoiceCode + " đã được lưu thành công.\r\nBạn có muốn xem trước và in hóa đơn không?",
                "Thanh toán thành công",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (printChoice == DialogResult.Yes)
            {
                _invoiceViewed?.Invoke(invoiceCode);
                using (var dialog = new InvoiceDetailsDialog(receiptInvoice))
                    dialog.OpenPrintPreview(owner);
            }
            else
            {
                AppToast.Success(owner as Control, "Thanh toán thành công",
                    "Hóa đơn " + invoiceCode + " đã được lưu. Giỏ hàng đã sẵn sàng cho giao dịch mới.");
            }
        }

        private InvoiceDetails CreateReceiptInvoice(string paymentMethod, CartTotalsDto totals)
        {
            decimal taxableAmount = Math.Max(0m, totals.Subtotal - totals.DiscountAmount);
            decimal vatRate = taxableAmount == 0m
                ? 0m
                : Math.Round(totals.VatAmount * 100m / taxableAmount, 2, MidpointRounding.AwayFromZero);

            var invoice = new InvoiceDetails
            {
                InvoiceCode = string.Empty,
                CreatedAt = DateTime.Now,
                CashierName = _userSession.CurrentUser.FullName,
                CustomerName = _selectedCustomer == null ? "Khách lẻ" : _selectedCustomer.FullName,
                PaymentMethod = paymentMethod,
                Subtotal = totals.Subtotal,
                DiscountAmount = totals.DiscountAmount,
                VatRate = vatRate,
                VatAmount = totals.VatAmount,
                TotalAmount = totals.GrandTotal,
                PointsUsed = decimal.ToInt32(Math.Round(
                    totals.PointsDiscountAmount / CartCalculator.PointRedeemRateVnd, 0)),
                PointsDiscountAmount = totals.PointsDiscountAmount
            };

            foreach (var line in _cart.Lines)
            {
                invoice.Lines.Add(new InvoiceLine
                {
                    ProductName = line.ProductName,
                    Quantity = line.Quantity,
                    UnitPrice = line.UnitPrice,
                    LineTotal = line.LineTotal
                });
            }

            return invoice;
        }

        private void ResetForNewSale()
        {
            _cart.Clear();
            _promotionDiscount = 0m;
            _promotionId = null;
            _promotionCode = null;
            _selectedCustomer = null;
            _usePoints = false;
            _pointsToUse = 0;

            _view.ClearCustomer();
            _view.SetPromoCode(string.Empty);
            RefreshCart();
        }
        #endregion

        public void Dispose()
        {
            _view.ViewReady -= OnViewReady;
            _view.ProductSearchChanged -= OnProductSearchChanged;
            _view.CategoryChanged -= OnCategoryChanged;
            _view.ScanBarcodeRequested -= OnScanBarcodeRequested;

            _view.AddToCartRequested -= OnAddToCartRequested;
            _view.NotifyWhenRestockRequested -= OnNotifyWhenRestockRequested;
            _view.CartQuantityChanged -= OnCartQuantityChanged;
            _view.CartLineRemoved -= OnCartLineRemoved;

            _view.CustomerSearchRequested -= OnCustomerSearchRequested;
            _view.CustomerCleared -= OnCustomerCleared;

            _view.PromoApplyRequested -= OnPromoApplyRequested;
            _view.PointsRedemptionChanged -= OnPointsRedemptionChanged;

            _view.HoldCartRequested -= OnHoldCartRequested;
            _view.ViewHeldCartsRequested -= OnViewHeldCartsRequested;
            _view.CheckoutRequested -= OnCheckoutRequested;
        }
    }
}
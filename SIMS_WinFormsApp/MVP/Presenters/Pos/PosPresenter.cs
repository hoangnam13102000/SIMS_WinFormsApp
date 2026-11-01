using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using SIMS_WinFormsApp.Models.DTOs.Pos;
using SIMS_WinFormsApp.Models.DTOs.Sales;
using SIMS_WinFormsApp.Services.Interfaces.Pos;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.UI.Controls.Barcode;
using SIMS_WinFormsApp.Views.Dialogs;
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
        private readonly IPosStockReservationService _stockReservationService;
        private readonly IPromotionService _promotionService;
        private readonly IBarcodeScannerLauncher _scannerLauncher;
        private readonly HeldCartStore _heldCartStore;
        private readonly Func<IWin32Window> _ownerWindowProvider;
        private readonly IUserSession _userSession;
        private readonly Action _invoiceCreated;
        private readonly Action<string> _invoiceViewed;

        private readonly PosCart _cart = new PosCart();
        private readonly CartCalculator _calculator = new CartCalculator();
        private readonly Dictionary<int, ProductCatalogItemDto> _catalogProducts =
            new Dictionary<int, ProductCatalogItemDto>();

        private int? _activeReservationId;
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
            IPosStockReservationService stockReservationService,
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
            _stockReservationService = stockReservationService ??
                throw new ArgumentNullException(nameof(stockReservationService));
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
            _heldCartStore.Load(_userSession.IsLoggedIn
                ? _stockReservationService.GetPendingReservations(_userSession.CurrentUser.UserId)
                : Array.Empty<HeldCartDto>());
            _view.SetHeldCartCount(_heldCartStore.GetAll().Count);
            RefreshCart();
        }

        private void ReloadProducts()
        {
            IReadOnlyList<ProductCatalogItemDto> products =
                _catalogService.Search(_searchKeyword, _selectedCategoryId);
            _catalogProducts.Clear();
            foreach (ProductCatalogItemDto product in products)
            {
                product.AvailableToAddQuantity = product.SellableQuantity;
                _catalogProducts[product.ProductId] = product;
            }
            _view.BindProducts(products);
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
            _catalogProducts.TryGetValue(productId, out ProductCatalogItemDto product);
            if (product != null) AddProductToCart(product, _ownerWindowProvider());
        }

        private void AddProductToCart(ProductCatalogItemDto product, IWin32Window owner)
        {
            if (!_userSession.IsLoggedIn)
            {
                AppToast.Error(owner as Control, "Không thể thêm sản phẩm", "Phiên đăng nhập đã hết hạn.");
                return;
            }
            try
            {
                _activeReservationId = _stockReservationService.ReserveStock(
                    _activeReservationId,
                    product.ProductId,
                    product.Name,
                    product.Price,
                    1,
                    _userSession.CurrentUser.UserId,
                    _selectedCustomer?.DisplayText);
            }
            catch (InvalidOperationException exception)
            {
                AppToast.Warning(owner as Control, "Không đủ tồn kho", exception.Message);
                ReloadProducts();
                return;
            }
            catch (SqlException exception)
            {
                AppToast.Error(owner as Control, "Không thể giữ hàng",
                    "Cơ sở dữ liệu không thể cập nhật tồn kho. Vui lòng thử lại.");
                System.Diagnostics.Debug.WriteLine("[PosPresenter] Stock reservation failed: " + exception);
                return;
            }

            _cart.AddOrIncrement(product.ProductId, product.Name, product.Price);
            UpdateProductStock(product.ProductId, -1);
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
            CartLineDto line = _cart.Lines.FirstOrDefault(item => item.ProductId == e.ProductId);
            if (line == null || !_activeReservationId.HasValue)
                return;

            int difference = e.Quantity - line.Quantity;
            try
            {
                if (difference > 0)
                    _activeReservationId = _stockReservationService.ReserveStock(
                        _activeReservationId,
                        line.ProductId,
                        line.ProductName,
                        line.UnitPrice,
                        difference,
                        _userSession.CurrentUser.UserId,
                        _selectedCustomer?.DisplayText);
                else if (difference < 0)
                    _stockReservationService.ReleaseReservedStock(
                        _activeReservationId.Value, line.ProductId, -difference);
            }
            catch (InvalidOperationException exception)
            {
                AppToast.Warning(_ownerWindowProvider() as Control,
                    difference > 0 ? "Không đủ tồn kho" : "Không thể hoàn tồn",
                    exception.Message);
                ReloadProducts();
                return;
            }
            catch (SqlException exception)
            {
                AppToast.Error(_ownerWindowProvider() as Control, "Không thể cập nhật tồn kho",
                    "Cơ sở dữ liệu không thể cập nhật tồn kho. Vui lòng thử lại.");
                System.Diagnostics.Debug.WriteLine("[PosPresenter] Stock quantity update failed: " + exception);
                return;
            }

            _cart.SetQuantity(e.ProductId, e.Quantity);
            UpdateProductStock(e.ProductId, -difference);
            RefreshCart();
        }

        private void OnCartLineRemoved(object sender, int productId)
        {
            CartLineDto line = _cart.Lines.FirstOrDefault(item => item.ProductId == productId);
            if (line == null || !_activeReservationId.HasValue)
                return;

            try
            {
                _stockReservationService.ReleaseReservedStock(
                    _activeReservationId.Value, productId, line.Quantity);
            }
            catch (SqlException exception)
            {
                AppToast.Error(_ownerWindowProvider() as Control, "Không thể hoàn tồn",
                    "Cơ sở dữ liệu không thể hoàn lại số hàng. Vui lòng thử lại.");
                System.Diagnostics.Debug.WriteLine("[PosPresenter] Stock release failed: " + exception);
                return;
            }
            catch (InvalidOperationException exception)
            {
                AppToast.Error(_ownerWindowProvider() as Control, "Không thể hoàn tồn", exception.Message);
                return;
            }

            _cart.Remove(productId);
            if (_cart.IsEmpty)
                _activeReservationId = null;
            UpdateProductStock(productId, line.Quantity);
            RefreshCart();
        }

        private void UpdateProductStock(int productId, int quantityChange)
        {
            if (!_catalogProducts.TryGetValue(productId, out ProductCatalogItemDto product))
                return;

            product.StockQuantity = Math.Max(0, product.StockQuantity + quantityChange);
            _view.UpdateProductStock(productId, product.StockQuantity);
        }

        private void RefreshCart()
        {
            _view.BindCart(_cart.Lines);
            ReloadProducts();
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
            var found = _customerService.FindCustomer(keyword);
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

            if (!_activeReservationId.HasValue)
            {
                AppToast.Error(owner as Control, "Không thể tạm giữ giỏ hàng",
                    "Không tìm thấy mã giữ hàng trong cơ sở dữ liệu. Vui lòng tải lại trang POS.");
                return;
            }

            _heldCartStore.Add(CreateHeldCart(_activeReservationId.Value));
            _view.SetHeldCartCount(_heldCartStore.GetAll().Count);
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

            if (!_cart.IsEmpty && _activeReservationId.HasValue)
                _heldCartStore.Add(CreateHeldCart(_activeReservationId.Value));

            ResetForNewSale();
            _cart.Restore(picked.Lines);
            _activeReservationId = picked.ReservationId;
            _heldCartStore.Remove(picked.HeldCartId);
            _view.SetHeldCartCount(_heldCartStore.GetAll().Count);
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
            if (!_activeReservationId.HasValue)
            {
                AppToast.Error(owner as Control, "Không thể thanh toán",
                    "Không tìm thấy mã giữ hàng của giỏ. Vui lòng tải lại trang POS.");
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
                    _activeReservationId.Value,
                    _cart.Lines);
                receiptInvoice.InvoiceCode = invoiceCode;
            }
            catch (SqlException exception)
            {
                ReportCheckoutFailure(owner, "Không thể lưu hóa đơn", exception, false);
                return;
            }
            catch (InvalidOperationException exception)
            {
                ReportCheckoutFailure(owner, "Chưa thể thanh toán", exception, true);
                return;
            }
            catch (ArgumentException exception)
            {
                ReportCheckoutFailure(owner, "Thông tin thanh toán không hợp lệ", exception, true);
                return;
            }
            catch (Exception exception)
            {
                ReportCheckoutFailure(owner, "Không thể hoàn tất thanh toán", exception, false);
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

        private void ReportCheckoutFailure(
            IWin32Window owner,
            string title,
            Exception exception,
            bool isWarning)
        {
            _view.SetBusy(false);
            System.Diagnostics.Debug.WriteLine("[PosPresenter] Checkout failed: " + exception);

            Exception root = exception.GetBaseException();
            string detail = root.Message;
            if (exception is SqlException sqlException)
                detail = "Mã lỗi SQL: " + sqlException.Number + Environment.NewLine + detail;
            if (string.IsNullOrWhiteSpace(detail))
                detail = root.GetType().FullName;

            string message = "Hóa đơn chưa được tạo." + Environment.NewLine +
                Environment.NewLine + "Chi tiết lỗi:" + Environment.NewLine + detail;
            if (isWarning)
                DialogHelper.ShowWarning(owner, title, message);
            else
                DialogHelper.ShowError(owner, title, message);
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
            _activeReservationId = null;
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

        private HeldCartDto CreateHeldCart(int reservationId)
        {
            return new HeldCartDto
            {
                HeldCartId = reservationId,
                ReservationId = reservationId,
                HeldAt = DateTime.Now,
                CustomerLabel = _selectedCustomer?.DisplayText,
                Lines = _cart.Lines
            };
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
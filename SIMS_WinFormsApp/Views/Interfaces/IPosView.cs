using System;
using System.Collections.Generic;
using SIMS_WinFormsApp.Models.DTOs.Pos;

namespace SIMS_WinFormsApp.Views.Interfaces
{
    public interface IPosView
    {
        event EventHandler ViewReady;
        event EventHandler<string> ProductSearchChanged;
        event EventHandler<int?> CategoryChanged;
        event EventHandler ScanBarcodeRequested;

        event EventHandler<int> AddToCartRequested;
        event EventHandler<int> NotifyWhenRestockRequested;
        event EventHandler<(int ProductId, int Quantity)> CartQuantityChanged;
        event EventHandler<int> CartLineRemoved;

        event EventHandler<string> CustomerSearchRequested;
        event EventHandler CustomerPickRequested;
        event EventHandler CustomerCleared;

        event EventHandler<string> PromoApplyRequested;
        event EventHandler<(bool UsePoints, int PointsToUse)> PointsRedemptionChanged;
        event EventHandler<string> PaymentMethodChanged;

        event EventHandler HoldCartRequested;
        event EventHandler ViewHeldCartsRequested;
        event EventHandler CheckoutRequested;

        void BindCategories(IReadOnlyList<CategoryOptionDto> categories);
        void BindProducts(IReadOnlyList<ProductCatalogItemDto> products);
        void BindCart(IReadOnlyList<CartLineDto> lines);

        void SetCustomer(CustomerLookupDto customer);
        void ClearCustomer();

        void SetTotals(CartTotalsDto totals);
        void SetPromoCode(string code);
        void SetPaymentMethods(IReadOnlyList<string> methods);
        void SetCheckoutEnabled(bool enabled);
        void SetBusy(bool isBusy);

        /// <summary>Mở popup chọn khách hàng từ danh sách gợi ý, trả về lựa chọn hoặc null nếu hủy.</summary>
        CustomerLookupDto PromptPickCustomer(IReadOnlyList<CustomerLookupDto> candidates);

        /// <summary>Mở popup chọn 1 giỏ hàng đã tạm giữ, trả về lựa chọn hoặc null nếu hủy.</summary>
        HeldCartDto PromptPickHeldCart(IReadOnlyList<HeldCartDto> heldCarts);
    }
}
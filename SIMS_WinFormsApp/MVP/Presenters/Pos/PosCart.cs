using System.Collections.Generic;
using System.Linq;
using SIMS_WinFormsApp.Models.DTOs.Pos;

namespace SIMS_WinFormsApp.MVP.Presenters.Pos
{
    public sealed class PosCart
    {
        private readonly Dictionary<int, CartLineDto> _linesByProductId = new Dictionary<int, CartLineDto>();
        private readonly List<int> _insertionOrder = new List<int>();

        public IReadOnlyList<CartLineDto> Lines => _insertionOrder.Select(id => _linesByProductId[id]).ToList();

        public bool IsEmpty => _linesByProductId.Count == 0;

        public decimal Subtotal => _linesByProductId.Values.Sum(l => l.LineTotal);

        public void AddOrIncrement(int productId, string productName, decimal unitPrice, int quantity = 1)
        {
            if (quantity <= 0) return;

            if (_linesByProductId.TryGetValue(productId, out var existing))
            {
                _linesByProductId[productId] = new CartLineDto(productId, existing.ProductName, existing.UnitPrice, existing.Quantity + quantity);
            }
            else
            {
                _linesByProductId[productId] = new CartLineDto(productId, productName, unitPrice, quantity);
                _insertionOrder.Add(productId);
            }
        }

        public void SetQuantity(int productId, int quantity)
        {
            if (!_linesByProductId.TryGetValue(productId, out var existing)) return;

            if (quantity <= 0)
            {
                Remove(productId);
                return;
            }
            _linesByProductId[productId] = new CartLineDto(productId, existing.ProductName, existing.UnitPrice, quantity);
        }

        public void Remove(int productId)
        {
            if (_linesByProductId.Remove(productId))
                _insertionOrder.Remove(productId);
        }

        public void Clear()
        {
            _linesByProductId.Clear();
            _insertionOrder.Clear();
        }

        /// <summary>Thay toàn bộ nội dung giỏ bằng 1 danh sách dòng có sẵn - dùng khi mở lại 1
        /// giỏ hàng đã tạm giữ trước đó (xem <see cref="HeldCartStore"/>).</summary>
        public void Restore(IReadOnlyList<CartLineDto> lines)
        {
            Clear();
            if (lines == null) return;
            foreach (var line in lines)
                AddOrIncrement(line.ProductId, line.ProductName, line.UnitPrice, line.Quantity);
        }
    }
}
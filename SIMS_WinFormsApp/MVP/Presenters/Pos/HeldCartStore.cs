using System;
using System.Collections.Generic;
using System.Linq;
using SIMS_WinFormsApp.Models.DTOs.Pos;

namespace SIMS_WinFormsApp.MVP.Presenters.Pos
{
    public sealed class HeldCartStore
    {
        private readonly List<HeldCartDto> _held = new List<HeldCartDto>();
        public IReadOnlyList<HeldCartDto> GetAll() => _held.OrderByDescending(c => c.HeldAt).ToList();

        public void Load(IEnumerable<HeldCartDto> carts)
        {
            _held.Clear();
            if (carts != null)
                _held.AddRange(carts);
        }

        public void Add(HeldCartDto cart)
        {
            if (cart == null) throw new ArgumentNullException(nameof(cart));
            _held.RemoveAll(existing => existing.ReservationId == cart.ReservationId);
            _held.Add(cart);
        }

        public void Remove(int heldCartId)
        {
            _held.RemoveAll(c => c.HeldCartId == heldCartId || c.ReservationId == heldCartId);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using SIMS_WinFormsApp.Models.DTOs.Pos;

namespace SIMS_WinFormsApp.MVP.Presenters.Pos
{
    public sealed class HeldCartStore
    {
        private readonly List<HeldCartDto> _held = new List<HeldCartDto>();
        private int _nextId = 1;

        public IReadOnlyList<HeldCartDto> GetAll() => _held.OrderByDescending(c => c.HeldAt).ToList();

        public HeldCartDto Add(string customerLabel, IReadOnlyList<CartLineDto> lines)
        {
            var cart = new HeldCartDto
            {
                HeldCartId = _nextId++,
                HeldAt = DateTime.Now,
                CustomerLabel = customerLabel,
                Lines = lines
            };
            _held.Add(cart);
            return cart;
        }

        public void Remove(int heldCartId)
        {
            _held.RemoveAll(c => c.HeldCartId == heldCartId);
        }
    }
}
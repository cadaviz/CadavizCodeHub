using CadavizCodeHub.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CadavizCodeHub.Orders.Domain.Entities
{
    public class Order : EntityBase
    {
        private readonly IEnumerable<Item> _items = [];

        protected Order() : base() { }

        public Order(IEnumerable<Item>? items) : this()
        {
            _items = items ?? [];
        }

        public IReadOnlyList<Item> Items => [.. _items];
        public decimal Total => Items.Sum(item => item.Total);
    }
}

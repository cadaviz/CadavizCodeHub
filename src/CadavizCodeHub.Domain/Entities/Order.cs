using CadavizCodeHub.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CadavizCodeHub.Domain.Entities
{
    public class Order : EntityBase
    {
        private readonly IEnumerable<Item> _items = [];

        protected Order() : base() { }

        public Order(IEnumerable<Item>? items) : this()
        {
            Id = Guid.NewGuid();
            _items = items ?? [];
        }

        public IReadOnlyList<Item> Items => _items.ToList();
        public decimal Total => Items.Sum(item => item.Total);
    }
}

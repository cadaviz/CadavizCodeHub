using System;
using System.Collections.Generic;
using System.Linq;
using CadavizCodeHub.Framework.Domain;

namespace CadavizCodeHub.Domain.Entities
{
    public class Order : EntityBase
    {
        private readonly IEnumerable<Item> _items = Enumerable.Empty<Item>();

        protected Order() : base() { }

        public Order(IEnumerable<Item>? items) : this()
        {
            Id = Guid.NewGuid();
            _items = items ?? Enumerable.Empty<Item>();
        }

        public IReadOnlyList<Item> Items => _items.ToList();
        public decimal Total => _items.Sum(item => item.Total);
    }
}

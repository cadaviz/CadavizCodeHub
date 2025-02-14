using CadavizCodeHub.Api.Requests;
using CadavizCodeHub.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CadavizCodeHub.Api.Mappers
{
    internal static class CreateOrderRequestItemMapper
    {
        internal static IEnumerable<Item> Map(this IEnumerable<CreateOrderRequestItem> items)
        {
            return items?.Select(x => x.Map()).ToList() ?? Enumerable.Empty<Item>();
        }

        internal static Item Map(this CreateOrderRequestItem item)
        {
            return new Item
            {
                Product = item.Product.Map(),
                Quantity = item.Quantity
            };
        }
    }
}

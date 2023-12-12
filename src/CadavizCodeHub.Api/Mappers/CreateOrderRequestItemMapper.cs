using System.Collections.Generic;
using System.Linq;
using CadavizCodeHub.Api.Requests;
using CadavizCodeHub.Domain.Entities;

namespace CadavizCodeHub.Api.Mappers
{
    internal static class CreateOrderRequestItemMapper
    {
        internal static IList<Item> Map(this IList<CreateOrderRequestItem> items)
        {
            return items?.Select(x => x.Map()).ToList() ?? new List<Item>();
        }

        internal static Item Map(this CreateOrderRequestItem item)
        {
            return new Item(product: item.Product.Map(),
                            quantity: item.Quantity);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using CadavizCodeHub.Api.Responses;
using CadavizCodeHub.Domain.Entities;

namespace CadavizCodeHub.Api.Mappers
{
    internal static class OrderResponseItemMapper
    {
        internal static IList<OrderResponseItem> Map(this IReadOnlyList<Item> items)
        {
            return items?.Select(x => x.Map()).ToList() ?? new List<OrderResponseItem>();
        }

        internal static OrderResponseItem Map(this Item item)
        {
            return new OrderResponseItem(Product: item.Product.Map(),
                                         Quantity: item.Quantity);
        }
    }
}

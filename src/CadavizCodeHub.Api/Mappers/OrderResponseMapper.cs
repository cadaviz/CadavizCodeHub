using CadavizCodeHub.Api.Responses;
using CadavizCodeHub.Domain.Entities;

namespace CadavizCodeHub.Api.Mappers
{
    internal static class OrderResponseMapper
    {
        internal static OrderResponse? MapNullable(this Order? order)
        {
            if (order is null)
                return null;

            return Map(order);
        }

        internal static OrderResponse Map(this Order order)
        {
            return new OrderResponse(order.Items.Map());
        }
    }
}

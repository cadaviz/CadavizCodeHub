using CadavizCodeHub.Api.Responses;
using CadavizCodeHub.Domain.Entities;

namespace CadavizCodeHub.Api.Mappers
{
    internal static class OrderResponseMapper
    {
        internal static OrderResponse? Map(this Order order)
        {
            if (order is null)
                return null;

            return new OrderResponse(order.Items.Map());
        }
    }
}

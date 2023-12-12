using CadavizCodeHub.Api.Requests;
using CadavizCodeHub.Domain.Entities;

namespace CadavizCodeHub.Api.Mappers
{
    internal static class CreateOrderRequestMapper
    {
        internal static Order Map(this CreateOrderRequest request)
        {
            return new Order(request.Items.Map());
        }
    }
}

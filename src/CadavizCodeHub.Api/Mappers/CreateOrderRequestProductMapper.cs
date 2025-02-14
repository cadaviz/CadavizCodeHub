using CadavizCodeHub.Api.Requests;
using CadavizCodeHub.Domain.Entities;

namespace CadavizCodeHub.Api.Mappers
{
    internal static class CreateOrderRequestProductMapper
    {
        internal static Product Map(this CreateOrderRequestProduct product)
        {
            return new Product
            {
                Description = product.Description,
                Price = product.Price,
            };
        }
    }
}

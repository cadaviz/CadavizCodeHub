using CadavizCodeHub.Api.Responses;
using CadavizCodeHub.Domain.Entities;

namespace CadavizCodeHub.Api.Mappers
{
    internal static class OrderResponseProductMapper
    {
        internal static OrderResponseProduct Map(this Product product)
        {
            return new OrderResponseProduct(Description: product.Description,
                                            Price: product.Price);
        }
    }
}

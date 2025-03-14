using System.Diagnostics.CodeAnalysis;

namespace CadavizCodeHub.Orders.WebApi.Requests
{
    /// <summary>
    /// Data necessary to create a new order
    /// </summary>
    /// <param name="Product">Product information</param>
    /// <param name="Quantity">Item quantity</param>
    [ExcludeFromCodeCoverage]
    public record CreateOrderRequestItem(CreateOrderRequestProduct Product, int Quantity);
}

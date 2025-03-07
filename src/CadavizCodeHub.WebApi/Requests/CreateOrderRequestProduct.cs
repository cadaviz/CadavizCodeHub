using System.Diagnostics.CodeAnalysis;

namespace CadavizCodeHub.WebApi.Requests
{
    /// <summary>
    /// Data necessary to create a new order
    /// </summary>
    /// <param name="Description">Description of the Product</param>
    /// <param name="Price">Product price</param>
    [ExcludeFromCodeCoverage]
    public record CreateOrderRequestProduct(string Description, decimal Price);
}

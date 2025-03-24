using System.Diagnostics.CodeAnalysis;

namespace CadavizCodeHub.Orders.WebApi.Responses
{
    /// <summary>
    /// Product representation
    /// </summary>
    /// <param name="Description">Description of the Product</param>
    /// <param name="Price">Product price</param>
    [ExcludeFromCodeCoverage]
    public record OrderResponseProduct(string Description, decimal Price);
}

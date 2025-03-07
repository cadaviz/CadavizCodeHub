using System.Diagnostics.CodeAnalysis;

namespace CadavizCodeHub.WebApi.Responses
{
    /// <summary>
    /// Item representation
    /// </summary>
    /// <param name="Product">Product information</param>
    /// <param name="Quantity">Item quantity</param>
    /// <param name="Total">Total cost of this item</param>
    [ExcludeFromCodeCoverage]
    public record OrderResponseItem(OrderResponseProduct Product, int Quantity, decimal Total);
}

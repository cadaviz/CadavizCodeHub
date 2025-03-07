using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CadavizCodeHub.WebApi.Requests
{
    /// <summary>
    /// Data necessary to create a new order
    /// </summary>
    /// <param name="Items">List of order items</param>
    [ExcludeFromCodeCoverage]
    public record CreateOrderRequest(IEnumerable<CreateOrderRequestItem> Items) : IRequest;
}

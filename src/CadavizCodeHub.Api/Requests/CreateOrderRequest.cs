using System.Collections.Generic;

namespace CadavizCodeHub.Api.Requests
{
    /// <summary>
    /// Data necessary to create a new order
    /// </summary>
    /// <param name="Items">List of order items</param>
    public record CreateOrderRequest(IEnumerable<CreateOrderRequestItem> Items) : IRequest;
}

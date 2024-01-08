using System.Collections.Generic;

namespace CadavizCodeHub.Api.Responses
{
    /// <summary>
    /// Order representation
    /// </summary>
    /// <param name="Items">List of order items</param>
    public record OrderResponse(IList<OrderResponseItem> Items) : IResponse;
}

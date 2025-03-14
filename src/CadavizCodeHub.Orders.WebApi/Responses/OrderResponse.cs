using CadavizCodeHub.Core.WebApi.Responses;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CadavizCodeHub.Orders.WebApi.Responses
{
    /// <summary>
    /// Order representation
    /// </summary>
    /// <param name="Id">Order identifier</param>
    /// <param name="Items">List of order items</param>
    /// <param name="Total">Total cost of order</param>
    [ExcludeFromCodeCoverage]
    public record OrderResponse(Guid Id, IEnumerable<OrderResponseItem> Items, decimal Total) : IResponse;
}

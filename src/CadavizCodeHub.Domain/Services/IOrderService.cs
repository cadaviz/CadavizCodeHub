using CadavizCodeHub.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CadavizCodeHub.Domain.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken);

        Task<Order?> GetOrderAsync(Guid id, CancellationToken cancellationToken);
    }
}

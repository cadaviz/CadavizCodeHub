using CadavizCodeHub.Domain.Entities;

namespace CadavizCodeHub.Application.Services
{
    public interface IOrderApplicationService
    {
        Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken);

        Task<Order?> GetOrderAsync(Guid id, CancellationToken cancellationToken);
    }
}

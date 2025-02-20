using CadavizCodeHub.Domain.DomainEvents;
using CadavizCodeHub.Domain.Entities;
using CadavizCodeHub.Domain.Repositories;

namespace CadavizCodeHub.Application.Services
{
    public class OrderApplicationService : IOrderApplicationService
    {
        private readonly IOrderCrudRepository _orderRepository;

        public OrderApplicationService(IOrderCrudRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken)
        {
            await _orderRepository.CreateAsync(order, cancellationToken);

            var orderCreatedEvent = new OrderCreatedEvent(order.Id, DateTime.Now);
            //Trigger the event

            return order;
        }

        public Task<Order?> GetOrderAsync(Guid id, CancellationToken cancellationToken)
        {
            return _orderRepository.GetByIdAsync(id, cancellationToken);
        }
    }
}

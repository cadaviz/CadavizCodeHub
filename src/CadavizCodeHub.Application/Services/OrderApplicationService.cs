using CadavizCodeHub.Domain.DomainEvents;
using CadavizCodeHub.Domain.Entities;
using CadavizCodeHub.Domain.Repositories;
using MediatR;

namespace CadavizCodeHub.Application.Services
{
    internal class OrderApplicationService : IOrderApplicationService
    {
        private readonly IMediator _mediator;
        private readonly IOrderCrudRepository _orderRepository;

        public OrderApplicationService(IMediator mediator, IOrderCrudRepository orderRepository)
        {
            _mediator = mediator;
            _orderRepository = orderRepository;
        }

        public async Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken)
        {
            await _orderRepository.CreateAsync(order, cancellationToken);

            await _mediator.Publish(new OrderCreatedEvent(order.Id, DateTime.Now), cancellationToken);

            return order;
        }

        public Task<Order?> GetOrderAsync(Guid id, CancellationToken cancellationToken)
        {
            return _orderRepository.GetByIdAsync(id, cancellationToken);
        }
    }
}

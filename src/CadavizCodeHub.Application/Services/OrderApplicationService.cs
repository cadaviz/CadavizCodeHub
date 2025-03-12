using CadavizCodeHub.Domain.DomainEvents;
using CadavizCodeHub.Domain.Entities;
using CadavizCodeHub.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CadavizCodeHub.Application.Services
{
    internal class OrderApplicationService : IOrderApplicationService
    {
        private readonly ILogger<OrderApplicationService> _logger;
        private readonly IMediator _mediator;
        private readonly IOrderCrudRepository _orderRepository;

        public OrderApplicationService(ILogger<OrderApplicationService> logger, IMediator mediator, IOrderCrudRepository orderRepository)
        {
            _logger = logger;
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
            using (_logger.BeginScope("OrderId: {OrderId}", id))
            {
                return _orderRepository.GetByIdAsync(id, cancellationToken);
            }
        }
    }
}

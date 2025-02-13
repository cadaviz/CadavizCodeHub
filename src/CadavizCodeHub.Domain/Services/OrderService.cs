using CadavizCodeHub.Domain.Entities;
using CadavizCodeHub.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CadavizCodeHub.Domain.Services
{
    internal class OrderService : IOrderService
    {
        private readonly IOrderCrudRepository _orderRepository;

        public OrderService(IOrderCrudRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken)
        {
            return _orderRepository.CreateAsync(order, cancellationToken);
        }

        public Task<Order?> GetOrderAsync(Guid id, CancellationToken cancellationToken)
        {
            return _orderRepository.GetByIdAsync(id, cancellationToken);
        }
    }
}

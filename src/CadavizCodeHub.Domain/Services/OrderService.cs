using System;
using System.Threading.Tasks;
using CadavizCodeHub.Domain.Entities;
using CadavizCodeHub.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace CadavizCodeHub.Domain.Services
{
    internal class OrderService : IOrderService
    {
        private readonly ILogger<IOrderService> _logger;
        private readonly IOrderCrudRepository _orderRepository;

        public OrderService(ILogger<IOrderService> logger,
                            IOrderCrudRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        public Task<Order> CreateOrderAsync(Order order)
        {
            return _orderRepository.CreateAsync(order);
        }

        public Task<Order?> GetOrderAsync(Guid id)
        {
            return _orderRepository.GetByIdAsync(id);
        }
    }
}

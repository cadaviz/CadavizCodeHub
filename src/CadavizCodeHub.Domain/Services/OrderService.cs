using System;
using CadavizCodeHub.Domain.Entities;
using CadavizCodeHub.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace CadavizCodeHub.Domain.Services
{
    internal class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IOrderCrudRepository _orderRepository;

        public OrderService(ILogger<OrderService> logger, 
                            IOrderCrudRepository orderRepository)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        public Order CreateOrder(Order order)
        {
            return _orderRepository.CreateAsync(order).Result;
        }

        public Order GetOrder(Guid id)
        {
            return _orderRepository.GetByIdAsync(id).Result;
        }
    }
}

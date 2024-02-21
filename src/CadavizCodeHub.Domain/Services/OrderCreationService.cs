using System;
using CadavizCodeHub.Domain.Entities;
using CadavizCodeHub.Domain.Repositories;

namespace CadavizCodeHub.Domain.Services
{
    internal class OrderCreationService : IOrderCreationService
    {
        //private readonly ILogger<OrderCreationService> _logger;
        private readonly IOrderCrudRepository _orderRepository;

        public OrderCreationService(IOrderCrudRepository orderRepository)
        {
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

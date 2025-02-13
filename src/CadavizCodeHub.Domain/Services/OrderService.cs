using CadavizCodeHub.Domain.Entities;
using CadavizCodeHub.Domain.Repositories;
using System;
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

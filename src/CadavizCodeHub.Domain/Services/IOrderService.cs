using System;
using CadavizCodeHub.Domain.Entities;

namespace CadavizCodeHub.Domain.Services
{
    public interface IOrderService
    {
        Order CreateOrder(Order order);

        Order GetOrder(Guid id);
    }
}

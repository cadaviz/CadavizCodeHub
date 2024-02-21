using System;
using CadavizCodeHub.Domain.Entities;

namespace CadavizCodeHub.Domain.Services
{
    public interface IOrderCreationService
    {
        Order CreateOrder(Order order);

        Order GetOrder(Guid id);
    }
}

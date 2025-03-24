using CadavizCodeHub.Core.Domain.Repositories;
using CadavizCodeHub.Orders.Domain.Entities;

namespace CadavizCodeHub.Orders.Domain.Repositories
{
    public interface IOrderCrudRepository : ICrudRepositoryBase<Order>
    { }
}

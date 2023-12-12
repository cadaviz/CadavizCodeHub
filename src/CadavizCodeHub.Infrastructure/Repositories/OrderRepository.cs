using System;
using System.Linq;
using System.Threading.Tasks;
using CadavizCodeHub.Domain.Entities;
using CadavizCodeHub.Domain.Repositories;
using MongoDB.Driver;

namespace CadavizCodeHub.Infrastructure.Repositories
{
    internal class OrderRepository : MongodbRepositoryBase<Order>, IOrderReadRepository, IOrderCrudRepository
    {
        //TODO: dar um dinamismo
        //TODO: Implementar unit of work
        public OrderRepository(DatabaseSettings databaseSettings) : base(databaseSettings, "order") { }

        public new async Task<Order> CreateAsync(Order order)
        {
            await base.CreateAsync(order);

            return order;
        }

        public new Task<Order> GetByIdAsync(Guid id)
        {
            return base.GetByIdAsync(id);
        }

        public async Task<Order?> GetByIdAsync()
        {
            var filter = Builders<Order>.Filter.Eq(r => r.Id, Guid.NewGuid());
            var a = await base.GetByFilterAsync(filter);

            return a.SingleOrDefault();
        }

        public Task<Order> UpdateAsync(Order order) => throw new NotImplementedException();
        public Task<Order> DeleteAsync(Guid id) => throw new NotImplementedException();
    }
}

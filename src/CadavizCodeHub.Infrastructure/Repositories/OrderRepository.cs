using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CadavizCodeHub.Domain.Entities;
using CadavizCodeHub.Domain.Repositories;
using MongoDB.Driver;

namespace CadavizCodeHub.Infrastructure.Repositories
{
    [ExcludeFromCodeCoverage]
    internal class OrderRepository : MongodbRepositoryBase<Order>, IOrderReadRepository, IOrderCrudRepository
    {
        public OrderRepository(DatabaseSettings databaseSettings) : base(databaseSettings, "order") { }

        public new async Task<Order> CreateAsync(Order order, CancellationToken cancellationToken)
        {
            await base.CreateAsync(order, cancellationToken);

            return order;
        }

        public new Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return base.GetByIdAsync(id, cancellationToken);
        }

        public async Task<Order?> GetByIdAsync(CancellationToken cancellationToken)
        {
            var filter = Builders<Order>.Filter.Eq(r => r.Id, Guid.NewGuid());
            var a = await base.GetByFilterAsync(filter, cancellationToken);

            return a.SingleOrDefault();
        }

        public Task<Order> UpdateAsync(Order order, CancellationToken cancellationToken) => throw new NotImplementedException();
        public Task<Order> DeleteAsync(Guid id, CancellationToken cancellationToken) => throw new NotImplementedException();
    }
}

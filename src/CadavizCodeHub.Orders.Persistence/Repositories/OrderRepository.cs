using CadavizCodeHub.Core.Persistence.MongoDB.Repositories;
using CadavizCodeHub.Core.Persistence.Setup;
using CadavizCodeHub.Orders.Domain.Entities;
using CadavizCodeHub.Orders.Domain.Repositories;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace CadavizCodeHub.Orders.Persistence.Repositories
{
    [ExcludeFromCodeCoverage]
    internal class OrderRepository : MongodbRepositoryBase<Order>, IOrderReadRepository, IOrderCrudRepository
    {
        public OrderRepository(DatabaseSettings databaseSettings, IMongoClient mongoClient, ILogger<OrderRepository> logger) 
            : base(databaseSettings, mongoClient, "order", logger) { }        
    }
}

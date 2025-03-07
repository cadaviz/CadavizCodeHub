using CadavizCodeHub.Domain.Entities;
using CadavizCodeHub.Domain.Repositories;
using CadavizCodeHub.Persistence.Database;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace CadavizCodeHub.Persistence.Repositories
{
    [ExcludeFromCodeCoverage]
    internal class OrderRepository : MongodbRepositoryBase<Order>, IOrderReadRepository, IOrderCrudRepository
    {
        public OrderRepository(DatabaseSettings databaseSettings, IMongoClient mongoClient) : base(databaseSettings, mongoClient, "order") { }        
    }
}

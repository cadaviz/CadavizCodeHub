﻿using CadavizCodeHub.Core.Persistence.MongoDB.Repositories;
using CadavizCodeHub.Core.Persistence.Setup;
using CadavizCodeHub.Orders.Domain.Entities;
using CadavizCodeHub.Orders.Domain.Repositories;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace CadavizCodeHub.Orders.Persistence.Repositories
{
    internal class OrderRepository : MongodbRepositoryBase<Order>, IOrderReadRepository, IOrderCrudRepository
    {
        protected override string CollectionName => "order";

        public OrderRepository(DatabaseSettings databaseSettings, IMongoClient mongoClient, ILogger<OrderRepository> logger)
            : base(databaseSettings, mongoClient, logger) { }
    }
}

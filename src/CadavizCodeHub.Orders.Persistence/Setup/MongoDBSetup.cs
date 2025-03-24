using CadavizCodeHub.Core.Persistence.Setup;
using CadavizCodeHub.Orders.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using System.Diagnostics.CodeAnalysis;
using MongoDBCore = CadavizCodeHub.Core.Persistence.MongoDB.Setup;

namespace CadavizCodeHub.Orders.Persistence.Setup
{
    [ExcludeFromCodeCoverage]
    internal static class MongoDBSetup
    {
        public static void Setup(IServiceCollection services, IConfiguration configuration, DatabaseSettings databaseSettings)
        {
            MongoDBCore.MongoDBSetup.ConfigureMongoDB(services, configuration, databaseSettings);

            RegisterClassMap();
        }

        private static void RegisterClassMap()
        {
            BsonClassMap.TryRegisterClassMap<Order>(cm =>
            {
                cm.MapProperty(order => order.Items);
                cm.MapCreator(order => new Order(order.Items));
            });
            BsonClassMap.TryRegisterClassMap<Item>(cm =>
            {
                cm.AutoMap();
            });
            BsonClassMap.TryRegisterClassMap<Product>(cm =>
            {
                cm.AutoMap();
            });
        }
    }
}

using CadavizCodeHub.Domain.Entities;
using MongoDB.Bson.Serialization;

namespace CadavizCodeHub.Infrastructure.Database
{
    internal static class DatabaseConfiguration
    {
        public static void RegisterClassMap()
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

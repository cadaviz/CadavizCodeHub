using CadavizCodeHub.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Diagnostics.CodeAnalysis;

namespace CadavizCodeHub.Persistence.Database
{
    [ExcludeFromCodeCoverage]
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

        public static void RegisterSerializer()
        {
            BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(GuidRepresentation.Standard));
        }
    }
}

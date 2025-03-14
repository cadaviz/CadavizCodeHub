using CadavizCodeHub.Core.Persistence.Setup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace CadavizCodeHub.Core.Persistence.MongoDB.Setup
{
    [ExcludeFromCodeCoverage]
    public static class MongoDBSetup
    {
        public static IServiceCollection ConfigureMongoDB(this IServiceCollection services, IConfiguration configuration, DatabaseSettings databaseSettings)
        {
            services.AddSingleton<IMongoClient>(sp =>
            {
                return new MongoClient(databaseSettings.ConnectionString);
            });

            RegisterSerializer();

            return services;
        }

        public static void RegisterSerializer()
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        }
    }
}

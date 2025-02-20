using CadavizCodeHub.Domain.Repositories;
using CadavizCodeHub.Infrastructure.Database;
using CadavizCodeHub.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Diagnostics.CodeAnalysis;

namespace CadavizCodeHub.Infrastructure.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class ServicesExtensions
    {
        public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories(configuration);

            ConfigureDatabase();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IOrderCrudRepository, OrderRepository>();

            var dbSettings = configuration.GetRequiredSection("DatabaseSettings").Get<DatabaseSettings>();
            ArgumentNullException.ThrowIfNull(dbSettings);

            services.AddSingleton(dbSettings);

            return services;
        }

        private static void ConfigureDatabase()
        {
            DatabaseConfiguration.RegisterClassMap();
            BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(GuidRepresentation.Standard));
        }
    }
}

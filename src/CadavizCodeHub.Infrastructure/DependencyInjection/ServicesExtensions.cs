using CadavizCodeHub.Domain.Repositories;
using CadavizCodeHub.Persistence.Database;
using CadavizCodeHub.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Diagnostics.CodeAnalysis;

namespace CadavizCodeHub.Persistence.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class ServicesExtensions
    {
        public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDatabase(configuration);

            services.AddRepositories();

            return services;
        }

        private static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var dbSettings = configuration.GetRequiredSection("DatabaseSettings").Get<DatabaseSettings>();
            ArgumentNullException.ThrowIfNull(dbSettings);

            services.AddSingleton(dbSettings);

            services.AddSingleton<IMongoClient>(sp =>
            {
                return new MongoClient(dbSettings.ConnectionString);
            });

            DatabaseConfiguration.RegisterClassMap();
            DatabaseConfiguration.RegisterSerializer();
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOrderCrudRepository, OrderRepository>();

            return services;
        }
    }
}

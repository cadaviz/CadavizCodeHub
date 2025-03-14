using CadavizCodeHub.Core.Persistence.Setup;
using CadavizCodeHub.Orders.Domain.Repositories;
using CadavizCodeHub.Orders.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace CadavizCodeHub.Orders.Persistence.Setup
{
    [ExcludeFromCodeCoverage]
    public static class PersistenceSetup
    {
        public static IServiceCollection ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
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

            MongoDBSetup.Setup(services, configuration, dbSettings);
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOrderCrudRepository, OrderRepository>();

            return services;
        }
    }
}

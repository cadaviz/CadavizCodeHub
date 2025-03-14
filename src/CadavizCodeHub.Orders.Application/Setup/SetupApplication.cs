using CadavizCodeHub.Application.Services;
using CadavizCodeHub.Application.Setup;
using CadavizCodeHub.Orders.Application.Services;
using CadavizCodeHub.Orders.Persistence.Setup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using CoreSetup = CadavizCodeHub.Core.Application.Setup;

namespace CadavizCodeHub.Orders.Application.Setup
{
    [ExcludeFromCodeCoverage]
    public static class SetupApplication
    {
        public static IServiceCollection ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
        {
            CoreSetup.SetupApplication.ConfigureApplication(services, configuration);

            services.AddApplicationServices();

            services.AddEventManager();

            services.ConfigurePersistence(configuration);

            return services;
        }

        private static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderApplicationService, OrderApplicationService>();

            return services;
        }
    }
}

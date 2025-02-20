using CadavizCodeHub.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace CadavizCodeHub.Application.Setup
{
    [ExcludeFromCodeCoverage]
    public static class SetupApplication
    {
        public static IServiceCollection ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationServices();

            services.AddEventManager();

            return services;
        }

        private static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderApplicationService, OrderApplicationService>();

            return services;
        }
    }
}

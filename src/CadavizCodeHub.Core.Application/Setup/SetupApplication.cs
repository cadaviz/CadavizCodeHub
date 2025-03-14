using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace CadavizCodeHub.Core.Application.Setup
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
            return services;
        }
    }
}

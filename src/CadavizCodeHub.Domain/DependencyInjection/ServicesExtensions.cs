using System.Diagnostics.CodeAnalysis;
using CadavizCodeHub.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CadavizCodeHub.Domain.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class ServicesExtensions
    {
        public static IServiceCollection ConfigureDomain(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServices(configuration);

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}

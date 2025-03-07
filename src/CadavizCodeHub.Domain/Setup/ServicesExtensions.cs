using CadavizCodeHub.Domain.DomainServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace CadavizCodeHub.Domain.Setup
{
    [ExcludeFromCodeCoverage]
    public static class ServicesExtensions
    {
        public static IServiceCollection ConfigureDomain(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDomainServices(configuration);

            return services;
        }

        private static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IOrderDomainService, OrderDomainService>();

            return services;
        }
    }
}

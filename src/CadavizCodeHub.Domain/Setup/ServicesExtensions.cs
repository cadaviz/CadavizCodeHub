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
            services.AddDomainServices();

            return services;
        }

        private static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderDomainService, OrderDomainService>();

            return services;
        }
    }
}

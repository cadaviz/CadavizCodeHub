using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace CadavizCodeHub.Api.Setup.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    internal static class ServicesExtensions
    {
        public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
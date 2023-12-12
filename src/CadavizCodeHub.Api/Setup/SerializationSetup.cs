using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace CadavizCodeHub.Api.Setup
{
    [ExcludeFromCodeCoverage]
    internal static class SerializationSetup
    {
        public static IServiceCollection ConfigureSerialization(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace CadavizCodeHub.Core.Application.Setup
{
    [ExcludeFromCodeCoverage]
    internal static class EventManagerSetup
    {
        internal static IServiceCollection AddEventManager(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}

using CadavizCodeHub.Core.Logging.Setup;
using CadavizCodeHub.Core.WebApi.Setup;
using CadavizCodeHub.Orders.Application.Setup;
using CadavizCodeHub.Orders.Domain.Setup;
using CadavizCodeHub.WebApi.Setup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace CadavizCodeHub.Orders.WebApi.Setup
{
    [ExcludeFromCodeCoverage]
    internal class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureLogs(Configuration);
            services.ConfigureWebApi();
            services.ConfigureApplication(Configuration);
            services.ConfigureDomain(Configuration);

            services.AddMappers();

            bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), out bool runningInContainer);

            if (runningInContainer)
            {
                services.Configure<KestrelServerOptions>(Configuration.GetSection("Kestrel"));
            }
        }

        public static void Configure(WebApplication app)
        {
            app.ConfigureWebApi();

            app.ConfigureLogs();

        }
    }
}

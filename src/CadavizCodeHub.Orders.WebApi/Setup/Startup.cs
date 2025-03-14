using CadavizCodeHub.Core.Logging.Setup;
using CadavizCodeHub.Core.WebApi.Setup;
using CadavizCodeHub.Domain.Setup;
using CadavizCodeHub.Orders.Application.Setup;
using CadavizCodeHub.WebApi.Setup;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        }

        public static void Configure(WebApplication app)
        {
            app.ConfigureWebApi();

            app.ConfigureLogs();
        }
    }
}

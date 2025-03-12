using CadavizCodeHub.Application.Setup;
using CadavizCodeHub.Domain.Setup;
using CadavizCodeHub.Persistence.DependencyInjection;
using CadavizCodeHub.WebApi.Exceptions;
using CadavizCodeHub.WebApi.Setup.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace CadavizCodeHub.WebApi.Setup
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

            services.AddControllers(options =>
                {
                    options.Filters.Add<HttpResponseExceptionFilter>();
                })
            .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                })
             .ConfigureSerialization();

            services.AddEndpointsApiExplorer();

            services.AddSettings(Configuration);
            services.AddMappers();
            services.ConfigureSwagger();

            services.ConfigureApplication(Configuration);
            services.ConfigureDomain(Configuration);
            services.ConfigureInfrastructure(Configuration);
        }

        public static void Configure(WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllers();
            app.ConfigureLogs();
            app.ConfigureSwagger();
        }
    }
}

using CadavizCodeHub.Api.Exceptions;
using CadavizCodeHub.Api.Setup.DependencyInjection;
using CadavizCodeHub.Domain.DependencyInjection;
using CadavizCodeHub.Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CadavizCodeHub.Api.Setup
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<HttpResponseExceptionFilter>();
            });

            services.AddEndpointsApiExplorer();

            services.AddSettings(Configuration);

            services.ConfigureSerialization(Configuration);
            services.ConfigureSwagger(Configuration);

            services.ConfigureDomain(Configuration);
            services.ConfigureInfrastructure(Configuration);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ConfigureSwagger();
        }
    }
}

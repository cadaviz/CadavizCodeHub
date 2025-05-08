using CadavizCodeHub.Core.WebApi.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace CadavizCodeHub.Core.WebApi.Setup
{
    [ExcludeFromCodeCoverage]
    public static class WebApiSetup
    {
        public static IServiceCollection ConfigureWebApi(this IServiceCollection services) {
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
            services.ConfigureSwagger();

            return services;
        }

        public static IApplicationBuilder ConfigureWebApi(this WebApplication app)
        {
            bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), out bool runningInContainer);
            
            if (!runningInContainer)
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();
            app.UseAuthorization();

            app.MapControllers();
            app.ConfigureSwagger();

            return app;
        }
    }
}

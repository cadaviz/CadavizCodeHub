using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Diagnostics.CodeAnalysis;

namespace CadavizCodeHub.Api.Setup
{
    [ExcludeFromCodeCoverage]
    public static class SwaggerSetup
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(setup =>
            {
                setup.IncludeXmlComments(GetXmlCommentsPath());
                setup.OperationFilter<AddResponseHeadersFilter>();

                setup.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Cadaviz CodeHub",
                        Description = "",
                        Contact = new OpenApiContact
                        {
                            Name = "Miguel Cadaviz",
                            Email = "miguelcadaviz@gmail.com",
                            Url = new Uri("https://github.com/cadaviz")
                        }
                    });
            });

            return services;
        }

        private static string GetXmlCommentsPath()
        {
            return System.AppDomain.CurrentDomain.BaseDirectory + @"CadavizCodeHub.xml";
        }

        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderAPI");
                options.RoutePrefix = string.Empty;
            });

            return app;
        }
    }
}

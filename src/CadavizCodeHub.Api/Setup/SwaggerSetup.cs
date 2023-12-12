using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

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
            //app.UseSwagger(setup =>
            //{ 

            //});
            //app.UseSwaggerUI();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderAPI");
                options.RoutePrefix = string.Empty;
            });

            return app;
        }

        //private static void Customize(this ISwaggerConfigurator swaggerConfig)
        //{
        //    swaggerConfig.ConfigureUI((context, options) =>
        //    {
        //        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API Endpoints");
        //        options.SwaggerEndpoint("/swagger/v0/swagger.json", "Maintenance Endpoints");
        //    });

        //    swaggerConfig.ConfigureGen((context, options) =>
        //    {
        //        var pbn = context.Configuration.GetSection("Service").Get<ServiceSettings>().PBN;

        //        options.SwaggerGeneratorOptions.SwaggerDocs.Add("v0", new OpenApiInfo { Title = pbn, Version = "v0" });

        //        options.SwaggerGeneratorOptions.SwaggerDocs.Add(
        //            "v1", new OpenApiInfo
        //            {
        //                Title = pbn,
        //                Version = $"v1 - {Assembly.GetExecutingAssembly().GetName().Version}",
        //                Description = "commerce fulfillment foreman",
        //                Contact = new OpenApiContact
        //                {
        //                    Email = "dl-pt-ftech-cluster-ordercreation@farfetch.com",
        //                    Name = "Order Creation Cluster",
        //                    Url = new Uri("https://farfetch.atlassian.net/wiki/spaces/OCC/overview")
        //                }
        //            });

        //        options.DocInclusionPredicate((docName, apiDesc) => apiDesc.RelativePath.StartsWith(docName));
        //    });
        //}
    }
}
